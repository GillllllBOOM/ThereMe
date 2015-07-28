using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Reflection;



namespace hands_viewer.cs
{
    class HandsRecognition
    {
        byte[] LUT;
        private Monitor mon;
        static int mode;
        private bool _disconnected = false;

        //Queue containing depth image - for synchronization purposes
        private Queue<PXCMImage> m_images;
        private const int NumberOfFramesToDelay = 3;
        //private int _framesCounter = 0;
        private float _maxRange;

        // Acquiring frames from input device
        public static string FILE_NAME = "test.txt";
        public static StreamWriter sr = File.CreateText(FILE_NAME);

        //for hands
        checkPoint rightcp, leftcp;
        int Rnum = 0, Lnum = 0; 

        //XY event
        public static XYManager monitor = new XYManager();
        public static XYReceiver eventReceiver = new XYReceiver(monitor);

        //play event
        public static ClickManager monitor2 = new ClickManager();
        public static ClickReceiver eventReceiver2 = new ClickReceiver(monitor2);

        Gettime tim = new Gettime();
        DateTime starttmp = DateTime.Now;
        double diff;


        public HandsRecognition(Monitor mon)
        {
            m_images = new Queue<PXCMImage>();
            this.mon = mon;
            LUT = Enumerable.Repeat((byte)0, 256).ToArray();
            LUT[255] = 1;
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

        /* Displaying current frame gestures */
        private void DisplayGesture(PXCMHandData handAnalysis, int frameNumber)
        {

            int firedGesturesNumber = handAnalysis.QueryFiredGesturesNumber();
            string gestureStatusLeft = string.Empty;
            string gestureStatusRight = string.Empty;
            if (firedGesturesNumber == 0)
            {

                return;
            }

            for (int i = 0; i < firedGesturesNumber; i++)
            {
                PXCMHandData.GestureData gestureData;
                if (handAnalysis.QueryFiredGestureData(i, out gestureData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    PXCMHandData.IHand handData;
                    if (handAnalysis.QueryHandDataById(gestureData.handId, out handData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                        return;

                    PXCMHandData.BodySideType bodySideType = handData.QueryBodySide();
                    if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_LEFT)
                    {
                        gestureStatusLeft += "Left Hand Gesture: " + gestureData.name;
                    }
                    else if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_RIGHT)
                    {
                        gestureStatusRight += "Right Hand Gesture: " + gestureData.name;
                    }

                }

            }
            //if (gestureStatusLeft == String.Empty)
            //form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusRight + "\n", Color.SeaGreen);
            //else
            //form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusLeft + ", " + gestureStatusRight + "\n", Color.SeaGreen);

        }

        /* Displaying current frames hand joints */
        private void DisplayJoints(PXCMHandData handOutput, long timeStamp = 0)
        {
            if (true)//form.GetJointsState() || form.GetSkeletonState())
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
                                PXCMHandData.JointData jointData;
                                handData.QueryTrackedJoint((PXCMHandData.JointType)j, out jointData);
                                nodes[i][j] = jointData;
                                if (handData.QueryTrackedJoint((PXCMHandData.JointType)j, out jointData) == pxcmStatus.PXCM_STATUS_NO_ERROR && Handsinfo.JointToInt((PXCMHandData.JointType)j) != -1)
                                {
                                    coor.finger = Handsinfo.JointToInt((PXCMHandData.JointType)j);
                                    coor.x = jointData.positionWorld.x;
                                    coor.y = jointData.positionWorld.y;
                                    coor.z = jointData.positionWorld.z;

                                    monitor.SimulateXY(coor.x, coor.y);
                                    
                                    //right 
                                    if (coor.finger == 1 && coor.handside == 1)
                                    {
                                        sr.WriteLine(coor.x + " " + coor.y + " " + coor.z + "\n");
                                        MYPoint p = new MYPoint(coor.x, coor.y, coor.z);
                                        int num = rightcp.checkNote(p);
                                        if (num != 0 && Rnum < 2 && Lnum < 2)
                                        {
                                            DateTime DateTime2 = DateTime.Now;
                                            diff = tim.ExecTimeDiff(starttmp, DateTime2);
                                            monitor2.SimulateClick(num, (int)diff);
                                        }
                                    }

                                    //left
                                    if (coor.finger == 1 && coor.handside == 0)
                                    {
                                        sr.WriteLine(coor.x + " " + coor.y + " " + coor.z + "\n");
                                        MYPoint p = new MYPoint(coor.x, coor.y, coor.z);
                                        int num = leftcp.checkNote(p);
                                        if (num != 0 && Rnum < 2 && Lnum < 2)
                                        {
                                            DateTime DateTime2 = DateTime.Now;
                                            diff = tim.ExecTimeDiff(starttmp, DateTime2);
                                            monitor2.SimulateClick(num, (int)diff);
                                        }
                                    }
                                }
                            } // end iterating over joints
                        }
                    } // end itrating over hands
                }
                Lnum = 0; Rnum = 0;
            }
        }

        public static pxcmStatus OnNewFrame(Int32 mid, PXCMBase module, PXCMCapture.Sample sample)
        {
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        /* Using PXCMSenseManager to handle data */
        public void SimplePipeline(int mod)
        {
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

            bool liveCamera = false;

            bool flag = true;
            PXCMSenseManager instance = null;
            _disconnected = false;
            instance = Program.session.CreateSenseManager();
            if (instance == null)
            {
                Console.WriteLine("Failed creating SenseManager");
                return;
            }
                        
            /* Set Module */
            pxcmStatus status = instance.EnableHand();//form.GetCheckedModule());
            PXCMHandModule handAnalysis = instance.QueryHand();

            if (status != pxcmStatus.PXCM_STATUS_NO_ERROR || handAnalysis == null)
            {
                Console.WriteLine("Failed Loading Module");
                return;
            }

            PXCMSenseManager.Handler handler = new PXCMSenseManager.Handler();
            handler.onModuleProcessedFrame = new PXCMSenseManager.Handler.OnModuleProcessedFrameDelegate(OnNewFrame);

            PXCMHandConfiguration handConfiguration = handAnalysis.CreateActiveConfiguration();
            PXCMHandData handData = handAnalysis.CreateOutput();

            if (handConfiguration == null)
            {
                Console.WriteLine("Failed Create Configuration");
                return;
            }

            if (handData == null)
            {
                Console.WriteLine("Failed Create Output");
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

                if (handConfiguration != null)
                {
                    handConfiguration.EnableAllAlerts();
                    handConfiguration.EnableSegmentationImage(true);

                    handConfiguration.ApplyChanges();
                    handConfiguration.Update();
                }
                Console.WriteLine("PXCSenseManager Initializing OK\n");
                int frameCounter = 0;
                int frameNumber = 0;

                //for thread 
                //ThreadPlay tp = new ThreadPlay();
                //int t = tp.play("music.txt");
                
                while (!mon.stop)
                {
                    
                    if (instance.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        break;
                    }

                    frameCounter++;

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
                                frameNumber = liveCamera ? frameCounter : instance.captureManager.QueryFrameIndex();

                                DisplayJoints(handData);
                                DisplayGesture(handData, frameNumber);
                            }
                        }
                    }
                    instance.ReleaseFrame();
                }
            }
            else
            {
                Console.WriteLine("Init Failed");
                flag = false;
            }
            foreach (PXCMImage pxcmImage in m_images)
            {
                pxcmImage.Dispose();
            }


            //store as a file
            sr.Close();
            //string filename = System.Guid.NewGuid().ToString() + ".txt";
            eventReceiver2.ClickStore("temp.txt");

            // Clean Up
            if (handData != null) handData.Dispose();
            if (handConfiguration != null) handConfiguration.Dispose();
            instance.Close();
            instance.Dispose();

            if (flag)
            {
                Console.WriteLine("Stopped");
            }
        }
    }
}
