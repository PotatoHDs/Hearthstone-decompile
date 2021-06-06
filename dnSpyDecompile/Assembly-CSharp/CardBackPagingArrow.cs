using System;

// Token: 0x0200060B RID: 1547
public class CardBackPagingArrow : CardBackPagingArrowBase
{
	// Token: 0x06005691 RID: 22161 RVA: 0x001C6147 File Offset: 0x001C4347
	public override void EnablePaging(bool enable)
	{
		this.button.Activate(enable);
	}

	// Token: 0x06005692 RID: 22162 RVA: 0x001C6155 File Offset: 0x001C4355
	public override void AddEventListener(UIEventType eventType, UIEvent.Handler handler)
	{
		this.button.AddEventListener(eventType, handler);
	}

	// Token: 0x04004A98 RID: 19096
	public ArrowModeButton button;
}
