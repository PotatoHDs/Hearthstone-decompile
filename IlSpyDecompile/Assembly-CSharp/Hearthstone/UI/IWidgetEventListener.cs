namespace Hearthstone.UI
{
	public interface IWidgetEventListener
	{
		WidgetTemplate OwningWidget { get; }

		WidgetEventListenerResponse EventReceived(string eventName);
	}
}
