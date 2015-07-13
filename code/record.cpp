/*******************************************************************************
INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.
*******************************************************************************/
/// record.cpp
/// User/Background Segmentation (PXC3DSeg) sample application

#include "pxc3dseg.h" 
#include "pxcvideomodule.h"
#include "pxcsensemanager.h"
#include "pxccapture.h"
#include "pxcmetadata.h"
#include "util_cmdline.h"
#include "util_render.h"
#include <windows.h>
#include <conio.h>

bool LightenBackground(PXCImage* segmented_image);

class MyPXC3DSegEventHandler : public PXC3DSeg::AlertHandler
{
public:
	MyPXC3DSegEventHandler() {};
	~MyPXC3DSegEventHandler() {};
	void PXCAPI OnAlert(const PXC3DSeg::AlertData& data)
	{
		switch (data.label)
		{
			case PXC3DSeg::ALERT_USER_TOO_FAR:
			{
				printf("You are outside the ideal operating range, please move closer.\n");
				break;
			}
			case PXC3DSeg::ALERT_USER_TOO_CLOSE:
			{
				printf("You are outside the ideal operating range, please move back.\n");
				break;
			}
			case PXC3DSeg::ALERT_USER_IN_RANGE:
			{
				printf("You are now in the ideal operating range.\n");
				break;
			}
		}
	};
} g_handler;

void FocusHandler(void *data)
{
	PXCCapture::Device *device = (PXCCapture::Device *) data;
	device->RestorePropertiesUponFocus();
}

int wmain(int argc, WCHAR* argv[])
{
	// Report the name of the executable and the provided arguments to stdout
	wprintf_s(L"Starting %s", argv[0]);
	for (int arg = 1; arg < argc; arg++) wprintf_s(L" %s", argv[arg]);
	wprintf_s(L"\n");

	// Create a pipeline construct
	PXCSenseManager* pSenseManager = PXCSenseManager::CreateInstance();
	if (!pSenseManager)
	{
		wprintf_s(L"Error: PXCSenseManager is unavailable\n");
		return PXC_STATUS_ITEM_UNAVAILABLE;
	}

	// Enable the User Segmentation video module
	pxcStatus result = pSenseManager->Enable3DSeg();
	if (result < PXC_STATUS_NO_ERROR)
	{
		wprintf_s(L"Error: Enable3DSeg failed (%d)\n", result);
		return result;
	}

	PXC3DSeg* pSeg = pSenseManager->Query3DSeg();
	if (!pSeg) return PXC_STATUS_ITEM_UNAVAILABLE;

	// Register to receive alerts (event notifications)
	pSeg->Subscribe(&g_handler);

	// Process command line arguments. For example,
	// 1. Set the number of frames (e.g. 3dseg.exe -nframes 50)
	// 2. Stream redirection "from" file (e.g. 3dseg.exe -file myfile.ext) 
	// 3. Stream redirection "to" file (e.g. 3dseg.exe -file myfile.ext -record)
	// 4. Disable rendering (i.e. -noRender)
	UtilCmdLine cmdl(pSenseManager->QuerySession());
	if (!cmdl.Parse(L"-nframes-file-record-noRender-realtime", argc, argv)) return -1;

	// If a file was specified, configure the Capture Manager to use it.
	if (cmdl.m_recordedFile)
	{
		result = pSenseManager->QueryCaptureManager()->
			SetFileName(cmdl.m_recordedFile, cmdl.m_bRecord);
		if (result < PXC_STATUS_NO_ERROR)
		{
			wprintf_s(L"Error: The specified file (%s) was not found (%d)\n",
				cmdl.m_recordedFile, result);
			return result;
		}
	}

	// Optional steps to send feedback to Intel Corporation to understand how often each SDK sample is used.
	PXCMetadata * md = pSenseManager->QuerySession()->QueryInstance<PXCMetadata>();
	if (md)
	{
		pxcCHAR sample_name[] = L"3D Segmentation";
		md->AttachBuffer(PXCSessionService::FEEDBACK_SAMPLE_INFO, (pxcBYTE*)sample_name, sizeof(sample_name));
	}

	// If we are not playing back a file, and the control key is held down,
	// ask the user to select from a list of supported profiles. Otherwise,
	// the first profile will be used.
	if (!cmdl.m_recordedFile || cmdl.m_bRecord)
	{
		// Provide some time for the user to press the Ctrl key.
		if (!GetAsyncKeyState(VK_CONTROL)) Sleep(1000);
		if (!GetAsyncKeyState(VK_CONTROL))
		{
			// Provide a hint of Ctrl mechanism
			wprintf_s(L"<Hold Ctrl during startup to select from menu of video profiles>\n");
		}
		else
		{
			wprintf_s(L"<Ctrl held during startup>\n");

			PXCVideoModule::DataDesc VideoProfile;
			int profile = 0;
			while (1)
			{
				result = pSeg->QueryInstance<PXCVideoModule>()->
					QueryCaptureProfile(profile, &VideoProfile);
				if (result < PXC_STATUS_NO_ERROR) break; // Reached end of profiles
				else // Report the profiles to stdout
				{
					wprintf_s(L"Profile %d: color %4dx%dx%0.f, depth %dx%dx%0.f\n",
						profile,
						VideoProfile.streams.color.sizeMax.width,
						VideoProfile.streams.color.sizeMax.height,
						VideoProfile.streams.color.frameRate.max,
						VideoProfile.streams.depth.sizeMax.width,
						VideoProfile.streams.depth.sizeMax.height,
						VideoProfile.streams.depth.frameRate.max);
				}
				profile++;
			}

			// Ask the user to select from a list of supported profiles
			const int LAST_PROFILE = profile - 1;
			wprintf_s(L"Select profile number (0-%d): ", LAST_PROFILE);
			// Convert the key press into a profile index
			profile = -1;
			while (profile < 0 || profile > LAST_PROFILE) profile = _getch() - '0';
			wprintf_s(L"%d\n", profile);

			// Request the selected profile
			pSeg->QueryInstance<PXCVideoModule>()->QueryCaptureProfile(profile, &VideoProfile);
			pSenseManager->EnableStreams(&VideoProfile);
		}
	}

	// Initialize and report the resulting stream source and profile to stdout
	result = pSenseManager->Init();
	if (result < PXC_STATUS_NO_ERROR)
	{
		wprintf_s(L"Error: Init failed (%d)\n", result);
		return result;
	}
	// Report the resulting stream source
	//play
	if (cmdl.m_recordedFile && !cmdl.m_bRecord)
	{
		wprintf_s(L"Streaming from %s \n", cmdl.m_recordedFile);
		// Disable realtime mode if it was disabled using a cmd line arg (i.e. -realtime off)
		if (!cmdl.m_realtime)
		{
			pSenseManager->QueryCaptureManager()->SetRealtime(false);
			wprintf_s(L"Realtime disabled\n");
		}
	}
	//camero
	else
	{
		PXCCapture::DeviceInfo device_info;
		pSenseManager->QueryCaptureManager()->QueryCapture()->QueryDeviceInfo(0, &device_info);
		wprintf_s(L"Streaming from %s\nFirmware: %d.%d.%d.%d\n",
			device_info.name,
			device_info.firmware[0], device_info.firmware[1],
			device_info.firmware[2], device_info.firmware[3]);
	}
	// Report the resulting profile
	{
		PXCVideoModule::DataDesc VideoProfile;
		result = pSeg->QueryInstance<PXCVideoModule>()->
			QueryCaptureProfile(PXCBase::WORKING_PROFILE, &VideoProfile);
		if (result < PXC_STATUS_NO_ERROR) return result;
		else // Report the profiles to stdout
		{
			wprintf_s(L"Color: %dx%dx%0.f\nDepth: %dx%dx%0.f\n",
				VideoProfile.streams.color.sizeMax.width,
				VideoProfile.streams.color.sizeMax.height,
				VideoProfile.streams.color.frameRate.max,
				VideoProfile.streams.depth.sizeMax.width,
				VideoProfile.streams.depth.sizeMax.height,
				VideoProfile.streams.depth.frameRate.max);
		}
	}

	// Mirror mode
	PXCCapture::Device* device = pSenseManager->QueryCaptureManager()->QueryDevice();
	if (!device)
	{
		wprintf_s(L"Error: QueryDevice failed on line %d\n", __LINE__);
		return -1;
	}
	PXCCapture::Device::MirrorMode MirrorMode = device->QueryMirrorMode();
	device->SetMirrorMode(PXCCapture::Device::MIRROR_MODE_HORIZONTAL);

	static UtilRender window(L"User Segmentation");
	window.SetOnFocusCallback(FocusHandler, device);

	// The main loop processes some number color and depth frames
	// If the number of frames is not provided as a command line argument,
	// a large default value is used (e.g. cmdl.m_nframes = 50000). 
	// The loop exits early in the following three cases:
	// 1. The end (of a playback file) is reached.
	// 2. The rendering window is closed by the user.
	// 3. The user presses the Esc key.
	bool bRenderWindowClosed = false;
	for (int frame = 1; frame <= (int)cmdl.m_nframes; frame++)
	{
		// Get the segmented image from the PXC3DSeg video module
		result = pSenseManager->AcquireFrame(true);
		// If we are playing back a file, suppress the error and exit normaly
		if ((cmdl.m_recordedFile && !cmdl.m_bRecord)
			&& (result < PXC_STATUS_NO_ERROR)) break; // (early exit case 1)
		else if (result < PXC_STATUS_NO_ERROR)
		{
			wprintf_s(L"Error: AcquireFrame failed (%d)\n", result);
			return result;
		}

		if (!cmdl.m_bNoRender)
		{
			PXCImage* segmented_image = pSeg->AcquireSegmentedImage();
			if (!segmented_image)
			{
				wprintf_s(L"Error: The segmentation image was not returned\n");
				return PXC_STATUS_PROCESS_FAILED;
			}

			// Render the color portion of the segmented image after 
			// lightening the background regions using the mask (alpha)
			bool bMaskIsSegmented = LightenBackground(segmented_image);
			bRenderWindowClosed = !window.RenderFrame(segmented_image);
			segmented_image->Release();

			// Report every 50 frames
			if (frame % 50 == 0)
			{
				const int fps = window.GetCurrentFPS();
				wprintf_s(L"Rendered frame %d", frame);
				if (fps) wprintf_s(L" at %d fps", fps);
				if (bMaskIsSegmented) wprintf_s(L" - User Detected");
				wprintf_s(L"\n");
			}

			// If the rendering window is closed, exit the loop
			if (bRenderWindowClosed) break;  // (early exit case 2)
		}
		else
		{
			// Report every 50 frames
			if (frame % 50 == 0) wprintf_s(L"%d frames processed\n", frame);
		}
		pSenseManager->ReleaseFrame();

		if (GetAsyncKeyState(VK_ESCAPE)) break; // (early exit case 3)
	}

	// Report normal exit
	wprintf_s(L"Exiting %s\n", argv[0]);

	// Restore the mirror mode
	device->SetMirrorMode(MirrorMode);
	pSenseManager->Release();
	return 0;
}

// This function takes a raw color image with an alpha channel (mask)
// and lightens the appearance of the background pixels. It also returns
// a value to indicate that a segmented region was detected.
bool LightenBackground(PXCImage* segmented_image)
{
	// If the segmented_image contains a segmented region, 
	// this return code is set to true.
	bool bMaskIsSegmented = false;

	// Iterate over the pixels in the image
	PXCImage::ImageData segmented_image_data;
	segmented_image->AcquireAccess(PXCImage::ACCESS_READ_WRITE,
		PXCImage::PIXEL_FORMAT_RGB32, &segmented_image_data);
	const pxcI32 height = segmented_image->QueryInfo().height;
	const pxcI32 width = segmented_image->QueryInfo().width;
	for (int y = 0; y < height; y++)
	{
		// Get the address of the row of pixels
		pxcBYTE* p = segmented_image_data.planes[0]
			+ y * segmented_image_data.pitches[0];

		// For each pixel in the row...
		const char GREY = 0x7f;
		for (int x = 0; x < width; x++)
		{
			// ...if the pixel is part of the segmented region...
			if (p[3] > 0)
			{
				// set the return flag, if it's not already set
				if (!bMaskIsSegmented) bMaskIsSegmented = true;

				// When the user moves into the near/far extent, the alpha values will drop from 255 to 1.
				// This can be used to fade the user in/out as a cue to move into the ideal operating range.
				if (p[3] != 255)
				{
					const float blend_factor = (float)p[3] / 255.0f;
					for (int ch = 0; ch < 3; ch++)
					{
						pxcBYTE not_visible = ((p[ch] >> 4) + GREY);
						p[ch] = (pxcBYTE)(p[ch] * blend_factor + not_visible * (1.0f - blend_factor));
					}
				}
			}
			else for (int ch = 0; ch < 3; ch++) p[ch] = (p[ch] >> 4) + GREY;

			// Move the pointer to the next pixel (RGBA)
			p += 4;
		}
	}
	segmented_image->ReleaseAccess(&segmented_image_data);

	return bMaskIsSegmented;
}
