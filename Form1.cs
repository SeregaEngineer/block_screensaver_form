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
using System.IO;
using System.Diagnostics;

namespace block_screensaver_form

{
    public partial class Form1 : Form
    {
        Timer timer;
        bool flag = true;

        public Form1()
        {

            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.stime_interval.ToString();
            timer = new Timer();

            timer.Tick += Timer_Tick;
            //ToolTip toolTip = new ToolTip();
            //toolTip.SetToolTip(pictureBox1, "sbalymov@yandex.ru\ngithub.com/sbalymov");

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (last() > 60)
            {
                MouseSim.MouseMove();
            }
        }



        private void ON_Click(object sender, EventArgs e)

        {
            if (flag)
            {
                try
                {
                    timer.Interval = Convert.ToInt32(textBox1.Text) * 60000;
                    ON.Text = "OFF";
                    flag = false;
                    timer.Start();
                    label2.Text = "Enable";
                }
                catch
                {
                    MessageBox.Show("попробуй целые числа");
                    ON.Text = "ON";
                    flag = false;

                }

            }
            else
            {
                timer.Stop();
                ON.Text = "ON";
                label2.Text = "Disable";
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


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.stime_interval = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.Save();
        }


    }
}
