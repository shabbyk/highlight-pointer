using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CursorHighlighter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Win32Point pt);

        public MainWindow()
        {
            InitializeComponent();
            StartHighLight();
        }

        private void StartHighLight()
        {
            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0,0,0,0,20);
            t.Tick += new EventHandler(PaintCircle);
            t.Start();
        }

        private void PaintCircle(object sender, EventArgs e)
        {
            var pt = GetMousePosition();
            using(var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                using(var b = new SolidBrush(System.Drawing.Color.FromArgb(100, 154,205,50)))
                {
                    g.FillEllipse(b, pt.X, pt.Y, 100, 100);
                }
            }
        }

        public System.Drawing.Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new System.Drawing.Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}
