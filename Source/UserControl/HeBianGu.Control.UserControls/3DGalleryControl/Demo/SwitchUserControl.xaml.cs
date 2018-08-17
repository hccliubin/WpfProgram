using linhang.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Control.UserControls._3DGalleryControl.Demo
{
    /// <summary>
    /// SwitchUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchUserControl : UserControl
    {
        private String[] IMAGES = { "images/image1.png", "images/image2.png", "images/image3.png", "images/image4.png", "images/image5.png", "images/image6.png", "images/image7.png", "images/image8.png", "images/image9.png", "images/image1.png", "images/image2.png", "images/image3.png", "images/image4.png", "images/image5.png", "images/image6.png", "images/image7.png", "images/image8.png", "images/image9.png" };    // images
        public SwitchUserControl()
        {
            InitializeComponent();
            imageSwitchView1.AddImages(IMAGES);
            imageSwitchView1.OnTouchDownEvent += new ImageSwitchView.TouchDownHander(imageSwitchView1_OnTouchDownEvent);
        }

        void imageSwitchView1_OnTouchDownEvent(UIElement view, int index)
        {

        }
    }
}
