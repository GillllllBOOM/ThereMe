/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hands_viewer.cs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static public PXCMSession session = null;
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            session = PXCMSession.CreateInstance();

            if (session != null)
            {
                // Optional steps to send feedback to Intel Corporation to understand how often each SDK sample is used.
                PXCMMetadata md = session.QueryInstance<PXCMMetadata>();
                if (md != null)
                {
                    string sample_name = "Hands Viewer CS";
                    md.AttachBuffer(1297303632, System.Text.Encoding.Unicode.GetBytes(sample_name));
                }
                Monitor first = new Monitor();
                Console.WriteLine("Start monitor\n");
                Console.WriteLine("Choose instrument:\n0 for piano\n1 for drum");
                int mode = Int32.Parse(Console.ReadLine());

                first.Start(mode);
                session.Dispose();
            }
        }
    }
}
