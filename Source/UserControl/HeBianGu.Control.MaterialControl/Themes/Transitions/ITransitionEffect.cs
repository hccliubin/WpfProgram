using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Controls.MaterialControl
{
    public interface ITransitionEffect
    {
        Timeline Build<TSubject>(TSubject effectSubject) where TSubject : FrameworkElement, ITransitionEffectSubject;
    }
}