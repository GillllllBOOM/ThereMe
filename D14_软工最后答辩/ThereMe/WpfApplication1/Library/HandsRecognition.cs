using System;
using System.IO;
using System.Threading;
using System.Windows.Forms; // MessageBox
using WpfApplication1.Library;



namespace WpfApplication1
{
   
	public delegate void HandClickHandler(int song, int duration);
	public delegate void HandMoveHandler(double x, double y);
    class HandsRecognition
    { 
		public event HandClickHandler HandClick;
		public event HandMoveHandler RHandMove;
        public event HandMoveHandler LHandMove;

        private int mode;
		private bool stop;
        private bool _disconnected = false;

        private PXCMSession session = null;
        private PXCMSenseManager instance = null;
        private PXCMHandModule handAnalysis = null;
        private PXCMSenseManager.Handler handler = null;
        private PXCMHandData handData = null;

        //Queue containing depth image - for synchronization purposes
        private const int NumberOfFramesToDelay = 3;
        //private int _framesCounter = 0;
        private float _maxRange;

        // Acquiring frames from input device
        public static string FILE_NAME = "test.txt";
        public static StreamWriter sr = null;

        //for hands
        checkPoint rightcp, leftcp;
        int Rnum = 0, Lnum = 0; 

        Gettime tim = new Gettime();
        DateTime starttmp = DateTime.Now;
        double diff;


        public HandsRecognition(int mod)
        {
			try
			{
				session = PXCMSession.CreateInstance();
				mode = mod;
				if (mod == 0)
				{
					rightcp = new checkPiano();
					leftcp = new checkPiano();
				}
				else if (mod == 1)
				{
					rightcp = new checkDrum();
					leftcp = new checkDrum();
				}

				_disconnected = false;

				instance = session.CreateSenseManager();
				if (instance == null)
				{
					MessageBox.Show("Failed creating SenseManager", "OnAlert");
					return;
				}

				/* Set Module */
				pxcmStatus status = instance.EnableHand();//form.GetCheckedModule());
				handAnalysis = instance.QueryHand();

				if (status != pxcmStatus.PXCM_STATUS_NO_ERROR || handAnalysis == null)
				{
					MessageBox.Show("Failed Loading Module", "OnAlert");
					return;
				}

				handler = new PXCMSenseManager.Handler();
				handler.onModuleProcessedFrame = new PXCMSenseManager.Handler.OnModuleProcessedFrameDelegate(OnNewFrame);

				handData = handAnalysis.CreateOutput();
			}
			catch
			{
				MessageBox.Show("Init Failed.");
				Environment.Exit(0);
			}
        }

        public void Dispose()
        {
            this.stop = true;
            lock (this.instance)
                lock (this.handData)
                {
                    if (handData != null) handData.Dispose();
                    if (instance != null)
                    {
                        instance.Close();
                        instance.Dispose();
                    }
                    if (session != null)
                    {
                        session.Dispose();
                    }
                }
        }

        ~HandsRecognition()
        {
        }

        /* Checking if sensor device connect or not */
        private bool DisplayDeviceConnection(bool state)
        {
            if (state)
            {
                if (!_disconnected) //form.UpdateStatus("Device Disconnected");
                    _disconnected = true;
            }
            else
            {
                if (_disconnected) //form.UpdateStatus("Device Reconnected");
                    _disconnected = false;
            }
            return _disconnected;
        }


        /* Displaying current frames hand joints */
        private void DisplayJoints(PXCMHandData handOutput, long timeStamp = 0)
        {
            //Iterate hands
            PXCMHandData.JointData[][] nodes = new PXCMHandData.JointData[][] { new PXCMHandData.JointData[0x20], new PXCMHandData.JointData[0x20] };
            int numOfHands = handOutput.QueryNumberOfHands();
            for (int i = 0; i < numOfHands; i++)
            {
                //Get hand by time of appearence
                PXCMHandData.IHand handData;
                if (handOutput.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_TIME, i, out handData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    if (handData != null)
                    {
                        Coordinate coor;
                        string handSide = "Unknown Hand";
                        handSide = handData.QueryBodySide() == PXCMHandData.BodySideType.BODY_SIDE_LEFT ? "Left Hand" : "Right Hand";
                        switch (handSide)
                        {
                            case ("Left Hand"): { coor.handside = 0; Rnum++; break; }
                            case ("Right Hand"): { coor.handside = 1; Lnum++; break; }
                            default: { coor.handside = 2; break; }
                        }

                        //Iterate Joints
                        for (int j = 0; j < 0x20; j++)
                        {
                            sr = File.CreateText(FILE_NAME);
                            PXCMHandData.JointData jointData;
                            handData.QueryTrackedJoint((PXCMHandData.JointType)j, out jointData);
                            nodes[i][j] = jointData;
                            if (handData.QueryTrackedJoint((PXCMHandData.JointType)j, out jointData) == pxcmStatus.PXCM_STATUS_NO_ERROR && Handsinfo.JointToInt((PXCMHandData.JointType)j) != -1)
                            {
                                coor.finger = Handsinfo.JointToInt((PXCMHandData.JointType)j);
                                coor.x = jointData.positionWorld.x;
                                coor.y = jointData.positionWorld.y;
                                coor.z = jointData.positionWorld.z;

                                //right 
                                if (coor.finger == 1 && coor.handside == 1)
                                {
                                    this.RHandMove(coor.x, coor.y);
                                    sr.WriteLine(coor.x + " " + coor.y + " " + coor.z + "\n");
                                    MYPoint p = new MYPoint(coor.x, coor.y, coor.z);
                                    int num = rightcp.checkNote(p);
                                    if (num != 0 && Rnum < 2 && Lnum < 2)
                                    {
                                        DateTime DateTime2 = DateTime.Now;
                                        diff = tim.ExecTimeDiff(starttmp, DateTime2);
                                        this.HandClick(num, (int)diff);
                                    }
                                }

                                //left
                                if (coor.finger == 1 && coor.handside == 0)
                                {
                                    this.LHandMove(coor.x, coor.y);
                                    sr.WriteLine(coor.x + " " + coor.y + " " + coor.z + "\n");
                                    MYPoint p = new MYPoint(coor.x, coor.y, coor.z);
                                    int num = leftcp.checkNote(p);
                                    if (num != 0 && Rnum < 2 && Lnum < 2)
                                    {
                                        DateTime DateTime2 = DateTime.Now;
                                        diff = tim.ExecTimeDiff(starttmp, DateTime2);
                                        this.HandClick(num, (int)diff);
                                    }
                                }
                            }
                            sr.Close();
                        } // end iterating over joints
                    }
                } // end itrating over hands
            }
            Lnum = 0; Rnum = 0;
        }

        public static pxcmStatus OnNewFrame(Int32 mid, PXCMBase module, PXCMCapture.Sample sample)
        {
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        /* Using PXCMSenseManager to handle data */
		public void Start()
		{
			this.stop = false;
            Thread pipeline_start = new Thread(new ThreadStart(this.SimplePipeline));
            pipeline_start.Start();
		}
		public void Stop()
		{
			this.stop = true;
		}

        private void SimplePipeline()
        {
            if (handData == null)
            {
                MessageBox.Show("Failed Create Output", "OnAlert");
                return;
            }
           
            if (handAnalysis != null && instance.Init(handler) == pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.DeviceInfo dinfo;

                PXCMCapture.Device device = instance.captureManager.device;
                if (device != null)
                {
                    device.QueryDeviceInfo(out dinfo);
                    _maxRange = device.QueryDepthSensorRange().max;
                }

                while (!this.stop)
                {
                    lock(this.instance)
                        lock(this.handData)
                        {
                            if (instance.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                            {
                                break;
                            }

                            if (!DisplayDeviceConnection(!instance.IsConnected()))
                            {
                                if (handData != null)
                                {
                                    handData.Update();
                                }

                                PXCMCapture.Sample sample = instance.QueryHandSample();
                                if (sample != null && sample.depth != null)
                                {
                                    if (handData != null)
                                    {
                                        DisplayJoints(handData);
                                    }
                                }
                            }
                            instance.ReleaseFrame();
                        }
                    //DispatcherHelper.DoEvents();
                }
                /*if (handData != null) handData.Dispose();
                if (instance != null)
                {
                    instance.Close();
                    instance.Dispose();
                }
                if (session != null)
                {
                    session.Dispose();
                }*/
            }
            else
            {
                MessageBox.Show("Init Failed", "OnAlert");
            }
        }
    }
}
