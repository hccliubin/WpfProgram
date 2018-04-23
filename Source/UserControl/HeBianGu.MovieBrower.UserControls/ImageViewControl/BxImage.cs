using System;
using System.Collections.Generic;
using System.IO;
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

namespace HeBianGu.MovieBrower.UserControls.ImageViewControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:HeBianGu.MovieBrower.UserControls.ImageViewControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:HeBianGu.MovieBrower.UserControls.ImageViewControl;assembly=HeBianGu.MovieBrower.UserControls.ImageViewControl"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    /// <summary>  
    /// ID:为解决WPF BitmapImage图片资源无法删除，文件正在被另一个进程使用问题的方案   
    /// Describe:自定义图片，加载后释放图片资源  
    /// Author:ybx  
    /// Date:2016-12-7 17:51:33  
    /// </summary>  
    public class BxImage : Image
    {
        public new string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BxSource.  This enables animation, styling, binding, etc...  
        public new static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(BxImage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BxImage.OnSourceChanged), null), null);

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Image image = (Image)d;
            if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()) || !File.Exists(e.NewValue.ToString()))
                return;
            using (BinaryReader reader = new BinaryReader(File.Open(e.NewValue.ToString(), FileMode.Open)))
            {
                try
                {
                    FileInfo fi = new FileInfo(e.NewValue.ToString());
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    image.Source = bitmapImage;
                }
                catch (Exception) { }
            }
        }
    }
}
