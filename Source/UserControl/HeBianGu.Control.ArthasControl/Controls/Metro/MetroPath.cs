
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Controls.ArthasControl
{
    public class MetroPath : ContentControl
    {
        public static readonly DependencyProperty DataProperty = ElementBase.Property<MetroPath, Geometry>(nameof(DataProperty));

        public Geometry Data { get { return (Geometry)GetValue(DataProperty); } set { SetValue(DataProperty, value); } }

        static MetroPath()
        {
            ElementBase.DefaultStyle<MetroPath>(DefaultStyleKeyProperty);
        }
    }
}