using System;
using System.ComponentModel;

using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    public class MidiDeviceManager
    {
        #region Fields

        private static MidiDeviceManager _mInstance;

        private bool _initialized;

        

        #endregion

        #region events

        public event EventHandler<ChannelMessageEventArgs> ChannelMessageReceived;

        #endregion


        #region Initialization

        protected MidiDeviceManager()
        {
            MidiDeviceList = new BindingList<InputDevice>();
        }

        /// <summary>
        /// Get the an instance of MidiDeviceListModel
        /// </summary>
        public static MidiDeviceManager Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new MidiDeviceManager();
                }
                return _mInstance;
            }
        }

        #endregion

        #region Public Properties


        public BindingList<InputDevice> MidiDeviceList { get; set; }


        public void InitializeDevices()
        {
            lock (this)
            {
                if (!_initialized)
                {
                    for (int i = 0; i < InputDevice.DeviceCount; i++)
                    {
                        var model = new InputDevice(i);

                        // add to list
                        MidiDeviceList.Add(model);

                        // register event
                        model.ChannelMessageReceived += HandleChannelMessageReceived;

                        model.Enabled = true;
                    }

                    _initialized = true;
                }
            }
        }

      


        public void DisposeDevices()
        {
            lock (this)
            {
                if (_initialized)
                {
                    // Released unmanaged Resources
                    foreach (InputDevice device in MidiDeviceList)
                    {
                        // register event
                        device.ChannelMessageReceived -= HandleChannelMessageReceived;

                        device.Close();
                    }

                    MidiDeviceList.Clear();

                    _initialized = false;
                }
            }
        }

        #endregion

        #region Protected Methods


        private void HandleChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            if (ChannelMessageReceived != null) ChannelMessageReceived(sender, e);
        }

        #endregion

    }
}