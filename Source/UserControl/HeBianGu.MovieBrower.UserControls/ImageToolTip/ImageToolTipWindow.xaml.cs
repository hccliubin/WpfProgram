using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HeBianGu.MovieBrower.UserControls.ImageToolTip
{
    /// <summary>
    /// ImageToolTipWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageToolTipWindow 
    {
        public ImageToolTipWindow()
        {
            InitializeComponent();
        }

        static ImageToolTipWindow _window = new ImageToolTipWindow();


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
 
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

public static void ShowWindow(ObservableCollection<string> files)
        {


            double x = SystemParameters.WorkArea.Width;//得到屏幕工作区域宽度
            double y = SystemParameters.WorkArea.Height;//得到屏幕工作区域高度
            double x1 = SystemParameters.PrimaryScreenWidth;//得到屏幕整体宽度
            double y1 = SystemParameters.PrimaryScreenHeight;//得到屏幕整体高度

            double screeHeight = SystemParameters.FullPrimaryScreenHeight;

            double screeWidth = SystemParameters.FullPrimaryScreenWidth;

            double top = (screeHeight - _window.Height) / 2;


            POINT pit = new POINT();
            GetCursorPos(out pit);   //获取鼠标绝对位置


            _window.imageView.ImagePaths = files;
            _window.WindowStartupLocation = WindowStartupLocation.Manual;
            _window.Left = pit.X+30;
            _window.Top = top;
            _window.Show();
        }

        public static void HideWindow()
        {
            _window.Hide();
        }
    }
}
