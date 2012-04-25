using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingLibrary;

namespace BindingApplication.MidiSettings
{
    public partial class MidiSettingsForm : Form
    {
        public MidiSettingsForm()
        {
            InitializeComponent();
        }


        public void Initialize()
        {

            midiSettingsControl1.Initizalize();

        }
    }
}
