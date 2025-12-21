using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace RailwayIDCardMaker.Forms
{
    /// <summary>
    /// Form for capturing photos from webcam using AForge
    /// </summary>
    public partial class WebcamCaptureForm : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public Image CapturedPhoto { get; private set; }

        public WebcamCaptureForm()
        {
            InitializeComponent();
        }

        private void WebcamCaptureForm_Load(object sender, EventArgs e)
        {
            LoadCameras();
        }

        private void LoadCameras()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("No webcam devices found!", "No Camera",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmbCameras.Items.Clear();
                foreach (FilterInfo device in videoDevices)
                {
                    cmbCameras.Items.Add(device.Name);
                }

                if (cmbCameras.Items.Count > 0)
                {
                    cmbCameras.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cameras: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbCameras.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a camera", "Select Camera",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                videoSource = new VideoCaptureDevice(videoDevices[cmbCameras.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                videoSource.Start();

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnCapture.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting camera: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                if (picWebcam.InvokeRequired)
                {
                    picWebcam.Invoke(new Action(() =>
                    {
                        if (picWebcam.Image != null)
                            picWebcam.Image.Dispose();
                        picWebcam.Image = bitmap;
                    }));
                }
                else
                {
                    if (picWebcam.Image != null)
                        picWebcam.Image.Dispose();
                    picWebcam.Image = bitmap;
                }
            }
            catch { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null;

                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnCapture.Enabled = false;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (picWebcam.Image != null)
            {
                CapturedPhoto = (Image)picWebcam.Image.Clone();
                picPreview.Image = CapturedPhoto;

                MessageBox.Show("Photo captured! Click OK to use this photo.", "Photo Captured",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CapturedPhoto == null)
            {
                MessageBox.Show("Please capture a photo first", "No Photo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StopCamera();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StopCamera();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void WebcamCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }
    }
}
