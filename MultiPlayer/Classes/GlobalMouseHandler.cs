using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MultiPlayer {
    /// <summary>
    /// Interaction logic for MouseHooker.xaml
    /// Derived from http://blogs.msdn.com/b/toub/archive/2006/05/03/589468.aspx
    /// </summary>
    public class MouseHooker {

        private readonly LowLevelMouseProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;

        public MouseHooker() {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public event EventHandler MouseMove;
        public event EventHandler MouseLMB;
        public event EventHandler MouseRMB;
        public event EventHandler MouseWheelUp;
        public event EventHandler MouseWheelDown;
        public event EventHandler MouseWheelClick;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [Flags]
        private enum MouseEventFlags {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private enum MouseMessages {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages) wParam) {
                var hookStruct = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                EventHandler OnMouseLMB = MouseLMB;
                OnMouseLMB?.Invoke(null, EventArgs.Empty);

                UnhookWindowsHookEx(_hookID);
                mouse_event((int) (MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int) (MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                _hookID = SetHook(_proc);
        } else if (nCode >= 0 && MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam) {
                EventHandler OnMouseLMB = MouseLMB;
                OnMouseLMB?.Invoke(null, EventArgs.Empty);

                UnhookWindowsHookEx(_hookID);
                mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
                _hookID = SetHook(_proc);
            } else if (nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages) wParam) {
                EventHandler OnMouseMove = MouseMove;
                OnMouseMove?.Invoke(null, EventArgs.Empty);

                UnhookWindowsHookEx(_hookID);
                mouse_event((int) (MouseEventFlags.MOVE), 0, 0, 0, 0);
                _hookID = SetHook(_proc);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    //using System.Windows.Forms;

    //public delegate void MouseMovedEvent();

    //public class GlobalMouseHandler : IMessageFilter {
    //    private const int WmMousemove      = 0x0200;
    //    private const int WmMousewheel     = 0x020A;

    //    private const int WmLbuttondown    = 0x0201;
    //    private const int WmLbuttonup      = 0x0202;
    //    private const int WmLbuttondblclk  = 0x0203;

    //    private const int WmRbuttondown    = 0x0204;
    //    private const int WmRbuttonup      = 0x0205;
    //    private const int WmRbuttondblclk  = 0x0206;

    //    private const int WmMbuttondown    = 0x0207;
    //    private const int WmMbuttonup      = 0x0208;
    //    private const int WmMbuttondblclk  = 0x0209;

    //    public event MouseMovedEvent MouseMoved;
    //    public event MouseMovedEvent LmbDoubleClick;

    //    #region IMessageFilter Members

    //    public bool PreFilterMessage(ref Message m) {
    //        if (m.Msg == WmMousemove) {
    //            if (MouseMoved != null) {
    //                MouseMoved();
    //            }
    //        } else if (m.Msg == WmLbuttondblclk) {
    //            if (LmbDoubleClick != null) {
    //                LmbDoubleClick();
    //            }
    //        }
    //        // Always allow message to continue to the next filter control
    //        return false;
    //    }

    //    #endregion
    //}
}


