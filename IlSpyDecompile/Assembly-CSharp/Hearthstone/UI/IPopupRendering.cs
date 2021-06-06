namespace Hearthstone.UI
{
	public interface IPopupRendering
	{
		void EnablePopupRendering(PopupRoot popupRoot);

		void DisablePopupRendering();

		bool ShouldPropagatePopupRendering();
	}
}
