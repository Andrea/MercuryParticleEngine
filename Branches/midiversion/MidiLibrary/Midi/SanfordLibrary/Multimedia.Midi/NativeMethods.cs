using System;
using System.Text;
using System.Runtime.InteropServices;
using Sanford.Multimedia.Timers;

namespace Sanford.Multimedia.Midi
{
    internal sealed class NativeMethods
    {
        [DllImport("winmm.dll")]
        public static extern int midiInOpen(out IntPtr handle, int deviceID,
            MidiInProc proc, IntPtr instance, int flags);

        [DllImport("winmm.dll")]
        public static extern int midiInClose(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiInStart(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiInStop(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiInReset(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiInPrepareHeader(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiInUnprepareHeader(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiInAddBuffer(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiInGetDevCaps(int deviceID,
            ref MidiInCaps caps, int sizeOfMidiInCaps);

        [DllImport("winmm.dll")]
        public static extern int midiInGetNumDevs();

        [DllImport("winmm.dll")]
        public static extern int midiOutReset(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiOutShortMsg(IntPtr handle, int message);

        [DllImport("winmm.dll")]
        public static extern int midiOutPrepareHeader(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutUnprepareHeader(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutLongMsg(IntPtr handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutGetDevCaps(int deviceID,
            ref MidiOutCaps caps, int sizeOfMidiOutCaps);

        [DllImport("winmm.dll")]
        public static extern int midiOutGetNumDevs();

        [DllImport("winmm.dll")]
        public static extern int midiOutOpen(out IntPtr handle, int deviceID,
            MidiOutProc proc, IntPtr instance, int flags);

        [DllImport("winmm.dll")]
        public static extern int midiOutClose(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiStreamOpen(out IntPtr handle, ref int deviceID, int reserved,
            MidiOutProc proc, int instance, uint flag);

        [DllImport("winmm.dll")]
        public static extern int midiStreamClose(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiStreamOut(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiStreamPause(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiStreamPosition(IntPtr handle, ref Time t, int sizeOfTime);

        [DllImport("winmm.dll")]
        public static extern int midiStreamProperty(IntPtr handle, ref OutputStream.Property p, uint flags);

        [DllImport("winmm.dll")]
        public static extern int midiStreamRestart(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiStreamStop(IntPtr handle);

        [DllImport("winmm.dll")]
        public static extern int midiConnect(IntPtr handleA, IntPtr handleB, int reserved);

        [DllImport("winmm.dll")]
        public static extern int midiDisconnect(IntPtr handleA, IntPtr handleB, int reserved);    

        public NativeMethods()
        {
        }
    }
}
