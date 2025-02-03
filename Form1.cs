using System;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SoundTest4
{
    public partial class Form1 : Form
    {
        private WaveOutEvent waveOutLeft;
        private WaveOutEvent waveOutRight;
        private SignalGenerator signalGeneratorLeft;
        private SignalGenerator signalGeneratorRight;
        private double frequency = 1000;

        public Form1()
        {
            InitializeComponent();
            stop05.Enabled = false; // Disable the stop button initially
        }

        private void start05_Click(object sender, EventArgs e)
        {
            stop05_Click(sender, e); // Stop any existing playback
            stop05.Enabled = true; // Enable the stop button
            start05.Enabled = false; // Disable the start button

            // Initialize the signal generator for the right channel
            signalGeneratorRight = new SignalGenerator(44100, 1)
            {
                Gain = 0.2,
                Frequency = frequency,
                Type = SignalGeneratorType.Sin
            };

            // Create a stereo sample provider with silence in the left channel
            var stereo = new MonoToStereoSampleProvider(signalGeneratorRight);
            stereo.LeftVolume = 0.0f; // Silence in left channel
            stereo.RightVolume = (float)RightVolumeSlider05.Value / 50; // Volume in right channel

            // Initialize and play the wave output for the right channel
            waveOutRight = new WaveOutEvent();
            waveOutRight.Init(stereo);
            waveOutRight.Play();
        }

        private void stop05_Click(object sender, EventArgs e)
        {
            stop05.Enabled = false; // Disable the stop button
            start05.Enabled = true; // Enable the start button
            waveOutRight?.Stop(); // Stop playback
            waveOutRight?.Dispose(); // Dispose of the wave output
        }

        private void RightVolumeSlider05_Scroll(object sender, EventArgs e)
        {
            if (signalGeneratorRight != null)
            {
                // Adjust the gain of the signal generator based on the slider value
                signalGeneratorRight.Gain = RightVolumeSlider05.Value / 50;
            }
        }

        private void HandleRadioButtonCheckedChanged(RadioButton radioButton, int frequencyValue)
        {
            if (radioButton.Checked)
            {
                // Update the frequency and reset the volume slider
                frequency = frequencyValue;
                RightVolumeSlider05.Value = RightVolumeSlider05.Minimum;
            }
        }

        // Event handlers for radio button checked changes
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton1, 200);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton2, 500);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton3, 1000);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton4, 2000);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton5, 3000);
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton9, 4000);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton6, 5000);
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton10, 6000);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton7, 7000);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonCheckedChanged(radioButton8, 10000);
        }

        private void RightVolumeSlider05_ValueChanged(object sender, EventArgs e)
        {
            start05_Click(sender, e); // Restart playback with the new volume
        }
    }
}
