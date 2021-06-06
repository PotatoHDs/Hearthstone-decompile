public class CardBackPagingArrowPhone : CardBackPagingArrowBase
{
	public PegUIElement button;

	private void Start()
	{
		button.AddEventListener(UIEventType.RELEASE, OnButtonReleased);
	}

	private void OnButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("deck_select_button_press.prefab:46d62875e039445439070cc9f7480d48");
	}

	public override void EnablePaging(bool enable)
	{
		button.gameObject.SetActive(enable);
	}

	public override void AddEventListener(UIEventType eventType, UIEvent.Handler handler)
	{
		button.AddEventListener(eventType, handler);
	}
}
