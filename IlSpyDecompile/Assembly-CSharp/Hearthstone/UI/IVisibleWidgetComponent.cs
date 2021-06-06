namespace Hearthstone.UI
{
	public interface IVisibleWidgetComponent
	{
		bool IsDesiredHidden { get; }

		bool IsDesiredHiddenInHierarchy { get; }

		bool HandlesChildVisibility { get; }

		void SetVisibility(bool isVisible, bool isInternal);
	}
}
