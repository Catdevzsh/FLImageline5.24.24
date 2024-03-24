using System;
using System.Windows.Forms;
using NAudio.Midi;

namespace FamicomSynth
{
    public class MainForm : Form
    {
        private Button buttonA;
        private Button buttonB;
        private Button buttonStart;
        private Button buttonSelect;
        private MidiOut midiOut;

        public MainForm()
        {
            InitializeComponent();
            midiOut = new MidiOut(0); // Initialize MIDI output, 0 is typically the first MIDI device
        }

        private void InitializeComponent()
        {
            this.buttonA = new Button();
            this.buttonB = new Button();
            this.buttonStart = new Button();
            this.buttonSelect = new Button();

            // buttonA
            this.buttonA.Text = "A";
            this.buttonA.Location = new System.Drawing.Point(150, 100);
            this.buttonA.Click += (sender, e) => SendMidiNote(60); // Middle C

            // buttonB
            this.buttonB.Text = "B";
            this.buttonB.Location = new System.Drawing.Point(100, 100);
            this.buttonB.Click += (sender, e) => SendMidiNote(62); // D note

            // buttonStart
            this.buttonStart.Text = "Start";
            this.buttonStart.Location = new System.Drawing.Point(100, 150);
            this.buttonStart.Click += (sender, e) => SendMidiNote(64); // E note

            // buttonSelect
            this.buttonSelect.Text = "Select";
            this.buttonSelect.Location = new System.Drawing.Point(150, 150);
            this.buttonSelect.Click += (sender, e) => SendMidiNote(65); // F note

            // MainForm setup
            this.Text = "Famicom Synth";
            this.Controls.Add(this.buttonA);
            this.Controls.Add(this.buttonB);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonSelect);
            this.ClientSize = new System.Drawing.Size(284, 261);
        }

        private void SendMidiNote(int note)
        {
            midiOut.Send(MidiMessage.StartNote(note, 127, 1).RawData); // Start note
            System.Threading.Thread.Sleep(200); // Duration of the note
            midiOut.Send(MidiMessage.StopNote(note, 0, 1).RawData); // Stop note
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            midiOut.Dispose(); // Clean up MIDI out
            base.OnFormClosed(e);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
