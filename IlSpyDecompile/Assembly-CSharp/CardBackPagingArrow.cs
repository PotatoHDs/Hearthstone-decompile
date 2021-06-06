public class CardBackPagingArrow : CardBackPagingArrowBase
{
	public ArrowModeButton button;

	public override void EnablePaging(bool enable)
	{
		button.Activate(enable);
	}

	public override void AddEventListener(UIEventType eventType, UIEvent.Handler handler)
	{
		button.AddEventListener(eventType, handler);
	}
}
