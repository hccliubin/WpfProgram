namespace HeBianGu.Controls.MaterialControl
{
    public interface IZIndexController
    {
        void Stack(params TransitionerSlide[] highestToLowest);
    }
}