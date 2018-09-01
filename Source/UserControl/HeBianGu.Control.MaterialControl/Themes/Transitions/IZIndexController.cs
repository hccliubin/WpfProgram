namespace HeBianGu.Controls.MaterialControl.Transitions
{
    public interface IZIndexController
    {
        void Stack(params TransitionerSlide[] highestToLowest);
    }
}