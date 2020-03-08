using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace block_screensaver_form
{
   
    class MouseSim
    {
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        //public static void ClickLeftMouseButton()
        //{
        //    INPUT mouseDownInput = new INPUT();
        //    mouseDownInput.type = SendInputEventType.InputMouse;
        //    mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
        //    SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

        //    INPUT mouseUpInput = new INPUT();
        //    mouseUpInput.type = SendInputEventType.InputMouse;
        //    mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
        //    SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        //}

        //public static void ClickRightMouseButton()
        //{
        //    INPUT mouseDownInput = new INPUT();
        //    mouseDownInput.type = SendInputEventType.InputMouse;
        //    mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
        //    SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

        //    INPUT mouseUpInput = new INPUT();
        //    mouseUpInput.type = SendInputEventType.InputMouse;
        //    mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
        //    SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        //}

        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

    

        public static void MouseMove()
        {
           
            int CursorX = Cursor.Position.X;
            int CursorY = Cursor.Position.Y;

            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(CursorX - 1);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(CursorY - 1);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

        }



       static int CalculateAbsoluteCoordinateX(int x)
        {
            if (x < 5)
            {
                Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                return x = ((resolution.Width / 2) * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            }

            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        static int CalculateAbsoluteCoordinateY(int y)
        {
                if (y < 5)
            {
                Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                return y = ((resolution.Height / 2) * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
            }

                return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }


        





    }
}
