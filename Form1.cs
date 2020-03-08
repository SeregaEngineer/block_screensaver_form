using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace block_screensaver_form

{
    public partial class Form1 : Form
    {
        Timer timer;
        bool flag = true;
        int time_interval;



        public Form1()
        {
            textBox1.Text = time_interval.ToString();
            InitializeComponent();
            timer = new Timer();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int CursorX = Cursor.Position.X;
            int CursorY = Cursor.Position.Y;

            Console.WriteLine(last().ToString());
            Console.WriteLine(CursorX.ToString());
            Console.WriteLine(CursorY.ToString());

            //MouseSim.ClickRightMouseButton();
            MouseSim.MouseMove();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ON_Click(object sender, EventArgs e)

        {

            if (flag)
            {
                try { time_interval = Int32.Parse(textBox1.Text) * 60000; }
                catch
                {
                    MessageBox.Show("попробуй целые числа");
                    flag = false;

                }
                timer.Interval = time_interval;
                timer.Tick += Timer_Tick;
                timer.Start();
                ON.BackColor = Color.Green;
                flag = false;

            }
            else
            {
                timer.Stop();
                ON.BackColor = default(Color);
                flag = true;

            }

        }




        // Возвращает в сек сколько пользователь не активен
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        static int last()
        {
            int t = 0;
            LASTINPUTINFO l = new LASTINPUTINFO();
            l.cbSize = (UInt32)Marshal.SizeOf(l);
            l.dwTime = 0;
            int e = Environment.TickCount;
            if (GetLastInputInfo(ref l))
            {
                int inp = (Int32)l.dwTime;
                t = e - inp;
            }
            return ((t > 0) ? (t / 1000) : 0);
        }

    }
}
