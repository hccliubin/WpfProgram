using HeBianGu.Controls.ArthasControl;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Controls.ArthasControl
{
    public class Theme
    {
        public static void Switch(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                if (childVisual != null)
                {
                    Utility.Refresh(childVisual as FrameworkElement);
                    Switch(childVisual);
                }
            }
        }
    }
}