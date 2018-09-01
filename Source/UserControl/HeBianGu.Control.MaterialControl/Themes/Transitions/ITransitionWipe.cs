using System.Windows;

namespace HeBianGu.Controls.MaterialControl.Transitions
{
    public interface ITransitionWipe
    {
        void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController);
    }
}