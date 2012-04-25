using System;
using System.ComponentModel;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;


namespace BindingLibrary
{
    public partial class MidiSettingsControl : UserControl
    {

        public BindingList<MidiEventBase> MidiEventList { get; set; }
        public event EventHandler SelectionChanged;


        private IBindableObject selectedObject;
        private string selectedProperty;


        #region Initializers

        public MidiSettingsControl()
        {
            InitializeComponent();

        }

        public void Initizalize()
        {


            // midi devices
            midiDeviceBindingSource.DataSource = MidiDeviceManager.Instance;
            midiDeviceBindingSource.DataMember = "MidiDeviceList";


            // midi events
            MidiEventList = new BindingList<MidiEventBase>();
            midiEventsBindingSource.DataSource = MidiEventList;
            midiEventsBindingSource.CurrentChanged += midiEventsBindingSource_CurrentChanged;


            MidiDeviceManager.Instance.ChannelMessageReceived += Instance_ChannelMessageReceived;


        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                midiEventsBindingSource.CurrentChanged -= midiEventsBindingSource_CurrentChanged;
                MidiDeviceManager.Instance.ChannelMessageReceived -= Instance_ChannelMessageReceived;
            }
            base.Dispose(disposing);
        }



        public BindingSource MidiDeviceBindingSource
        {
            get { return midiDeviceBindingSource; }
        }

        public BindingSource MidiEventsBindingSource
        {
            get { return midiEventsBindingSource; }
        }


        public string SelectedProperty
        {
            get { return selectedProperty; }
        }

        public IBindableObject SelectedObject
        {
            get { return selectedObject; }
        }


        private delegate void Instance_ChannelMessageReceivedDel(object sender, ChannelMessageEventArgs e);
        void Instance_ChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke((Instance_ChannelMessageReceivedDel) Instance_ChannelMessageReceived, sender, e);
            }
            else
            {
                // Add event
                var midiInputEvent = MidiFactory.Instance.CreateMidiEvent(e.Message.Command, e.Message.MidiChannel, e.Message.Data1, e.Message.Data2);
                if (midiInputEvent != null)
                {
                    MidiEventList.Add(midiInputEvent);

                    // select event
                    midiEventsBindingSource.MoveLast();



                }
            }
        }

        void midiEventsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            selectedObject = midiEventsBindingSource.Current as IBindableObject;
            propertyGrid1.SelectedObject = selectedObject;


            selectedProperty = GetPropertyGridSelectionName();


            if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);
        }




        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            selectedProperty = GetPropertyGridSelectionName();


            if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);
        }



        #endregion


  
        private void clearListButton_Click(object sender, EventArgs e)
        {
            MidiEventList.Clear();
        }

        private void Enabled_CheckedChanged(object sender, EventArgs e)
        {
            var device = midiDeviceBindingSource.Current as InputDevice;
            if (device != null && device.Enabled != Enabled.Checked)
            {
                device.Enabled = Enabled.Checked;
            }
        }

 
        private string GetPropertyGridSelectionName()
        {
            GridItem item = propertyGrid1.SelectedGridItem;
            if (item != null && item.PropertyDescriptor != null)
            {
                return item.PropertyDescriptor.Name;
            }
            return "";
        }
    }
}
