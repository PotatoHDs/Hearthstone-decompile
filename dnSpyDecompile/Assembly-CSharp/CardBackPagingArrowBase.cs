using System;
using UnityEngine;

// Token: 0x0200060C RID: 1548
public abstract class CardBackPagingArrowBase : MonoBehaviour
{
	// Token: 0x06005694 RID: 22164
	public abstract void EnablePaging(bool enable);

	// Token: 0x06005695 RID: 22165
	public abstract void AddEventListener(UIEventType eventType, UIEvent.Handler handler);
}
