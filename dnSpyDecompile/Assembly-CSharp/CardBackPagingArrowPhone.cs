using System;

// Token: 0x0200060D RID: 1549
public class CardBackPagingArrowPhone : CardBackPagingArrowBase
{
	// Token: 0x06005697 RID: 22167 RVA: 0x001C616D File Offset: 0x001C436D
	private void Start()
	{
		this.button.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnButtonReleased));
	}

	// Token: 0x06005698 RID: 22168 RVA: 0x001C6188 File Offset: 0x001C4388
	private void OnButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("deck_select_button_press.prefab:46d62875e039445439070cc9f7480d48");
	}

	// Token: 0x06005699 RID: 22169 RVA: 0x001C619E File Offset: 0x001C439E
	public override void EnablePaging(bool enable)
	{
		this.button.gameObject.SetActive(enable);
	}

	// Token: 0x0600569A RID: 22170 RVA: 0x001C61B1 File Offset: 0x001C43B1
	public override void AddEventListener(UIEventType eventType, UIEvent.Handler handler)
	{
		this.button.AddEventListener(eventType, handler);
	}

	// Token: 0x04004A99 RID: 19097
	public PegUIElement button;
}
