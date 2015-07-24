using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hands_viewer.cs
{
    struct Coordinate
    {
        public int handside;
        public int finger;
        public float x, y, z;

    };

    class Handsinfo
    {
        public static string AlertToString(PXCMHandData.AlertType label)
        {
            string alertLabel = "";
            switch (label)
            {
                case PXCMHandData.AlertType.ALERT_HAND_DETECTED: { alertLabel = "ALERT_HAND_DETECTED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_NOT_DETECTED: { alertLabel = "ALERT_HAND_NOT_DETECTED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_TRACKED: { alertLabel = "ALERT_HAND_TRACKED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_NOT_TRACKED: { alertLabel = "ALERT_HAND_NOT_TRACKED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_CALIBRATED: { alertLabel = "ALERT_HAND_CALIBRATED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_NOT_CALIBRATED: { alertLabel = "ALERT_HAND_NOT_CALIBRATED"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BORDERS: { alertLabel = "ALERT_HAND_OUT_OF_BORDERS"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_INSIDE_BORDERS: { alertLabel = "ALERT_HAND_INSIDE_BORDERS"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_LEFT_BORDER: { alertLabel = "ALERT_HAND_OUT_OF_LEFT_BORDER"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_RIGHT_BORDER: { alertLabel = "ALERT_HAND_OUT_OF_RIGHT_BORDER"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_TOP_BORDER: { alertLabel = "ALERT_HAND_OUT_OF_TOP_BORDER"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BOTTOM_BORDER: { alertLabel = "ALERT_HAND_OUT_OF_BOTTOM_BORDER"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_TOO_FAR: { alertLabel = "ALERT_HAND_TOO_FAR"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_TOO_CLOSE: { alertLabel = "ALERT_HAND_TOO_CLOSE"; break; }
                case PXCMHandData.AlertType.ALERT_HAND_LOW_CONFIDENCE: { alertLabel = "ALERT_HAND_LOW_CONFIDENCE"; break; }
            }
            return alertLabel;
        }

        public static int JointToInt(PXCMHandData.JointType label)
        {
            int jointLabel = -1;
            switch (label)
            {
                case PXCMHandData.JointType.JOINT_THUMB_TIP: { jointLabel = 0; break; }
                case PXCMHandData.JointType.JOINT_INDEX_BASE: { jointLabel = 1; break; }
                case PXCMHandData.JointType.JOINT_MIDDLE_TIP: { jointLabel = 2; break; }
                case PXCMHandData.JointType.JOINT_RING_TIP: { jointLabel = 3; break; }
                case PXCMHandData.JointType.JOINT_PINKY_TIP: { jointLabel = 4; break; }

                case PXCMHandData.JointType.JOINT_WRIST:
                case PXCMHandData.JointType.JOINT_CENTER:

                case PXCMHandData.JointType.JOINT_THUMB_BASE:
                case PXCMHandData.JointType.JOINT_THUMB_JT1:
                case PXCMHandData.JointType.JOINT_THUMB_JT2:
                case PXCMHandData.JointType.JOINT_INDEX_TIP:
                case PXCMHandData.JointType.JOINT_INDEX_JT1:
                case PXCMHandData.JointType.JOINT_INDEX_JT2:
                case PXCMHandData.JointType.JOINT_MIDDLE_BASE:
                case PXCMHandData.JointType.JOINT_MIDDLE_JT1:
                case PXCMHandData.JointType.JOINT_MIDDLE_JT2:
                case PXCMHandData.JointType.JOINT_RING_BASE:
                case PXCMHandData.JointType.JOINT_RING_JT1:
                case PXCMHandData.JointType.JOINT_RING_JT2:
                case PXCMHandData.JointType.JOINT_PINKY_BASE:
                case PXCMHandData.JointType.JOINT_PINKY_JT1:
                case PXCMHandData.JointType.JOINT_PINKY_JT2: { jointLabel = -1; break; }

            }
            return jointLabel;
        }

        //pxcCHAR* GestureStateToString(PXCMHandData.GestureStateType label)
        //{
        //    pxcCHAR* gestureState = L"";
        //    switch (label)
        //    {
        //    case PXCMHandData.GESTURE_STATE_START:			        {gestureState = L"GESTURE_STATE_START";break;}
        //    case PXCMHandData.GESTURE_STATE_IN_PROGRESS:			{gestureState = L"GESTURE_STATE_IN_PROGRESS";break;}
        //    case PXCMHandData.GESTURE_STATE_END:				    {gestureState = L"GESTURE_STATE_END";break;}        
        //    }
        //    return gestureState;
        //}
    }
}
