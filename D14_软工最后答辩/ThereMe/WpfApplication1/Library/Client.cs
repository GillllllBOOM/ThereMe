using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using g711audio;
using System.Windows.Interop;
using System.Windows.Forms;

namespace WpfApplication1.Library
{
	public delegate void ClientConnectedHandler(string UserName);
	public delegate void ClientDisconnectedHandler();
	public delegate void ClientKeyPlayedHandler(int Key);
	class Client
	{
		// Private Members
		private CaptureBufferDescription captureBufferDescription;
		private AutoResetEvent autoResetEvent;
		private Notify notify;
		private WaveFormat waveFormat;
		private Capture capture;
		private int bufferSize;
		private CaptureBuffer captureBuffer;
		private UdpClient udpClient;
		private Device device;
		private SecondaryBuffer playbackBuffer;
		private BufferDescription playbackBufferDescription;
		private Socket clientSocket;
		private bool bStop; 
		private IPEndPoint otherPartyIP;
		private EndPoint otherPartyEP;
		private volatile bool bIsConnectActive;
		private Vocoder vocoder;
		private byte[] byteData = new byte[1024];
		private volatile int nUdpClientFlag;

		// Public Members
		public event ClientConnectedHandler ClientConnectedEvent;
		public event ClientDisconnectedHandler ClientDisconnectedEvent;
		public event ClientKeyPlayedHandler ClientKeyPlayedEvent;

		public string Codec
		{ get; set; }

		public string IP
		{ get; set; }

		public string Name
		{ get; set; }

		#region Public Functions
		public Client(/*System.Windows.Window window*/)
		{
			try
			{
				//windowInteropHelper = new WindowInteropHelper(window);

				device = new Device();
				device.SetCooperativeLevel(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, CooperativeLevel.Normal);

				CaptureDevicesCollection captureDeviceCollection = new CaptureDevicesCollection();

				DeviceInformation deviceInfo = captureDeviceCollection[0];

				capture = new Capture(deviceInfo.DriverGuid);

				short channels = 1; //Stereo.
				short bitsPerSample = 16; //16Bit, alternatively use 8Bits.
				int samplesPerSecond = 22050; //11KHz use 11025 , 22KHz use 22050, 44KHz use 44100 etc.

				//Set up the wave format to be captured.
				waveFormat = new WaveFormat();
				waveFormat.Channels = channels;
				waveFormat.FormatTag = WaveFormatTag.Pcm;
				waveFormat.SamplesPerSecond = samplesPerSecond;
				waveFormat.BitsPerSample = bitsPerSample;
				waveFormat.BlockAlign = (short)(channels * (bitsPerSample / (short)8));
				waveFormat.AverageBytesPerSecond = waveFormat.BlockAlign * samplesPerSecond;

				captureBufferDescription = new CaptureBufferDescription();
				captureBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5;//approx 200 milliseconds of PCM data.
				captureBufferDescription.Format = waveFormat;

				playbackBufferDescription = new BufferDescription();
				playbackBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5;
				playbackBufferDescription.Format = waveFormat;
				playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);

				bufferSize = captureBufferDescription.BufferBytes;

				bIsConnectActive = false;
				nUdpClientFlag = 0;

				//Using UDP sockets
				clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				EndPoint ourEP = new IPEndPoint(IPAddress.Any, 1450);
				//Listen asynchronously on port 1450 for coming messages (Invite, Bye, etc).
				clientSocket.Bind(ourEP);

				//Receive data from any IP.
				EndPoint remoteEP = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));

				byteData = new byte[1024];
				//Receive data asynchornously.
				clientSocket.BeginReceiveFrom(byteData,
										   0, byteData.Length,
										   SocketFlags.None,
										   ref remoteEP,
										   new AsyncCallback(OnReceive),
										   null);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - Initialize ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}        
		}

		public void Connect()
		{
			try
			{
				//Get the IP we want to connect.
				otherPartyIP = new IPEndPoint(IPAddress.Parse(IP), 1450);
				otherPartyEP = (EndPoint)otherPartyIP;

				//Get the vocoder to be used.
				if (Codec == "A-Law")
				{
					vocoder = Vocoder.ALaw;
				}
				else if (Codec == "u-Law")
				{
					vocoder = Vocoder.uLaw;
				}
				else if (Codec == "None")
				{
					vocoder = Vocoder.None;
				}

				//Send an invite message.
				SendMessage(Command.Invite, otherPartyEP);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - Connect ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void Exit()
		{
			DropConnect();
			lock (clientSocket)
			{
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
				clientSocket = null;
			}
		}
		private void DropConnect()
		{
			try
			{
				//Send a Bye message to the user to end the connect.
				if (otherPartyEP != null)
				{
					SendMessage(Command.Bye, otherPartyEP);
					otherPartyEP = null;
					otherPartyIP = null;
				}
				UninitializeConnect();
				ClientDisconnectedEvent();
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - DropConnect ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void SendKey(int key)
		{
			try
			{
				//Send a Key Play message to the user to end the connect.
				SendMessage(Command.Key, otherPartyEP, key.ToString());
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - SendKey ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		#region Private Functions
		/*
		 * Commands are received asynchronously. OnReceive is the handler for them.
		 */
		private void OnReceive(IAsyncResult ar)
		{
			try
			{
				EndPoint receivedFromEP = new IPEndPoint(IPAddress.Any, 0);

				if (clientSocket == null)
					return;
				//Get the IP from where we got a message.
				clientSocket.EndReceiveFrom(ar, ref receivedFromEP);

				//Convert the bytes received into an object of type Data.
				Data msgReceived = new Data(byteData);

				//Act according to the received message.
				switch (msgReceived.cmdCommand)
				{
					//We have an incoming connect.
					case Command.Invite:
						{
							if (bIsConnectActive == false)
							{
								//We have no active connect.

								//Ask the user to accept the connect or not.
								if (MessageBox.Show("Invite from " + msgReceived.strName + ".\r\n\r\nMusic On?",
									"VoiceChat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									SendMessage(Command.OK, receivedFromEP);
									vocoder = msgReceived.vocoder;
									otherPartyEP = receivedFromEP;
									otherPartyIP = (IPEndPoint)receivedFromEP;
									InitializeConnect();
									ClientConnectedEvent(msgReceived.strName);
								}
								else
								{
									//The connect is declined. Send a busy response.
									SendMessage(Command.Busy, receivedFromEP);
								}
							}
							else
							{
								//We already have an existing connect. Send a busy response.
								SendMessage(Command.Busy, receivedFromEP);
							}
							break;
						}

					//Partner played a key.
					case Command.Key:
						{
							ClientKeyPlayedEvent(Convert.ToInt32(msgReceived.strName));
							break;
						}

					//OK is received in response to an Invite.
					case Command.OK:
						{
							//Start a connect.
							InitializeConnect();
							ClientConnectedEvent(msgReceived.strName);
							break;
						}

					//Remote party is busy.
					case Command.Busy:
						{
							MessageBox.Show("User busy.", "VoiceChat", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							break;
						}

					case Command.Bye:
						{
							//Check if the Bye command has indeed come from the user/IP with which we have
							//a connect established. This is used to prevent other users from sending a Bye, which
							//would otherwise end the connect.
							if (receivedFromEP.Equals(otherPartyEP) == true)
							{
								//End the connect.
								UninitializeConnect();
								ClientDisconnectedEvent();
							}
							break;
						}
				}

				byteData = new byte[1024];
				//Get ready to receive more commands.
				clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref receivedFromEP, new AsyncCallback(OnReceive), null);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - OnReceive ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OnSend(IAsyncResult ar)
		{
			try
			{
				clientSocket.EndSendTo(ar);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - OnSend ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/*
		 * Send synchronously sends data captured from microphone across the network on port 1550.
		 */
		private void Send()
		{
			try
			{
				//The following lines get audio from microphone and then send them 
				//across network.

				captureBuffer = new CaptureBuffer(captureBufferDescription, capture);

				CreateNotifyPositions();

				int halfBuffer = bufferSize / 2;

				captureBuffer.Start(true);

				bool readFirstBufferPart = true;
				int offset = 0;

				MemoryStream memStream = new MemoryStream(halfBuffer);
				bStop = false;
				while (!bStop)
				{
					autoResetEvent.WaitOne();
					memStream.Seek(0, SeekOrigin.Begin);
					captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None);
					readFirstBufferPart = !readFirstBufferPart;
					offset = readFirstBufferPart ? 0 : halfBuffer;

					//Choose the vocoder. And then send the data to other party at port 1550.

					if (vocoder == Vocoder.ALaw)
					{
						byte[] dataToWrite = ALawEncoder.ALawEncode(memStream.GetBuffer());
						udpClient.Send(dataToWrite, dataToWrite.Length, otherPartyIP.Address.ToString(), 1550);
					}
					else if (vocoder == Vocoder.uLaw)
					{
						byte[] dataToWrite = MuLawEncoder.MuLawEncode(memStream.GetBuffer());
						udpClient.Send(dataToWrite, dataToWrite.Length, otherPartyIP.Address.ToString(), 1550);
					}
					else
					{
						byte[] dataToWrite = memStream.GetBuffer();
						udpClient.Send(dataToWrite, dataToWrite.Length, otherPartyIP.Address.ToString(), 1550);
					}
				}
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - Send ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				captureBuffer.Stop();

				//Increment flag by one.
				nUdpClientFlag += 1;

				//When flag is two then it means we have got out of loops in Send and Receive.
				while (nUdpClientFlag != 2)
				{ }

				//Clear the flag.
				nUdpClientFlag = 0;

				//Close the socket.
				udpClient.Close();
			}
		}

		/*
		 * Receive audio data coming on port 1550 and feed it to the speakers to be played.
		 */
		private void Receive()
		{
			try
			{
				bStop = false;
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

				while (!bStop)
				{
					//Receive data.
					byte[] byteData = udpClient.Receive(ref remoteEP);

					//G711 compresses the data by 50%, so we allocate a buffer of double
					//the size to store the decompressed data.
					byte[] byteDecodedData = new byte[byteData.Length * 2];

					//Decompress data using the proper vocoder.
					if (vocoder == Vocoder.ALaw)
					{
						ALawDecoder.ALawDecode(byteData, out byteDecodedData);
					}
					else if (vocoder == Vocoder.uLaw)
					{
						MuLawDecoder.MuLawDecode(byteData, out byteDecodedData);
					}
					else
					{
						byteDecodedData = new byte[byteData.Length];
						byteDecodedData = byteData;
					}


					//Play the data received to the user.
					playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
					playbackBuffer.Write(0, byteDecodedData, LockFlag.None);
					playbackBuffer.Play(0, BufferPlayFlags.Default);
				}
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - Receive ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				nUdpClientFlag += 1;
			}
		}

		private void CreateNotifyPositions()
		{
			try
			{
				autoResetEvent = new AutoResetEvent(false);
				notify = new Notify(captureBuffer);
				BufferPositionNotify bufferPositionNotify1 = new BufferPositionNotify();
				bufferPositionNotify1.Offset = bufferSize / 2 - 1;
				bufferPositionNotify1.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();
				BufferPositionNotify bufferPositionNotify2 = new BufferPositionNotify();
				bufferPositionNotify2.Offset = bufferSize - 1;
				bufferPositionNotify2.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();

				notify.SetNotificationPositions(new BufferPositionNotify[] { bufferPositionNotify1, bufferPositionNotify2 });
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - CreateNotifyPositions ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UninitializeConnect()
		{
			//Set the flag to end the Send and Receive threads.
			bStop = true;

			bIsConnectActive = false;
		}

		private void InitializeConnect()
		{
			try
			{
				//Start listening on port 1500.
				udpClient = new UdpClient(1550);

				Thread senderThread = new Thread(new ThreadStart(Send));
				Thread receiverThread = new Thread(new ThreadStart(Receive));
				bIsConnectActive = true;

				//Start the receiver and sender thread.
				receiverThread.Start();
				senderThread.Start();
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - InitializeConnect ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/*
		 * Send a message to the remote party.
		 */
		private void SendMessage(Command cmd, EndPoint sendToEP, string sendName = "")
		{
			try
			{
				if (sendName == "")
					sendName = Name;
				//Create the message to send.
				Data msgToSend = new Data();

				msgToSend.strName = sendName;   //Name of the user.
				msgToSend.cmdCommand = cmd;         //Message to send.
				msgToSend.vocoder = vocoder;        //Vocoder to be used.

				byte[] message = msgToSend.ToByte();

				//Send the message asynchronously.
				clientSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, sendToEP, new AsyncCallback(OnSend), null);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message, "ThereMe - SendMessage ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
	}

	//The commands for interaction between the two parties.
	enum Command
	{
		Invite, //Make a connect.
		Bye,    //End a connect.
		Busy,   //User busy.
		OK,     //Response to an invite message. OK is send to indicate that connect is accepted.
		Key,    //A key to play.
		Null,   //No command.
	}

	//Vocoder
	enum Vocoder
	{
		ALaw,   //A-Law vocoder.
		uLaw,   //u-Law vocoder.
		None,   //Don't use any vocoder.
	}

	//The data structure by which the server and the client interact with 
	//each other.
	class Data
	{
		//Default constructor.
		public Data()
		{
			this.cmdCommand = Command.Null;
			this.strName = null;
			vocoder = Vocoder.ALaw;
		}

		//Converts the bytes into an object of type Data.
		public Data(byte[] data)
		{
			//The first four bytes are for the Command.
			this.cmdCommand = (Command)BitConverter.ToInt32(data, 0);

			//The next four store the length of the name.
			int nameLen = BitConverter.ToInt32(data, 4);

			//This check makes sure that strName has been passed in the array of bytes.
			if (nameLen > 0)
				this.strName = Encoding.UTF8.GetString(data, 8, nameLen);
			else
				this.strName = null;
		}

		//Converts the Data structure into an array of bytes.
		public byte[] ToByte()
		{
			List<byte> result = new List<byte>();

			//First four are for the Command.
			result.AddRange(BitConverter.GetBytes((int)cmdCommand));

			//Add the length of the name.
			if (strName != null)
				result.AddRange(BitConverter.GetBytes(strName.Length));
			else
				result.AddRange(BitConverter.GetBytes(0));

			//Add the name.
			if (strName != null)
				result.AddRange(Encoding.UTF8.GetBytes(strName));

			return result.ToArray();
		}

		public string strName;      //Name by which the client logs into the room.
		public Command cmdCommand;  //Command type (login, logout, send message, etc).
		public Vocoder vocoder;
	}
}
