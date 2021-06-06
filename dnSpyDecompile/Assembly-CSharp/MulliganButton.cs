using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AD1 RID: 2769
public class MulliganButton : MonoBehaviour
{
	// Token: 0x0600939E RID: 37790 RVA: 0x002FDDB1 File Offset: 0x002FBFB1
	public void SetText(string text)
	{
		this.uberText.Text = text;
		this.uberText.UpdateText();
	}

	// Token: 0x0600939F RID: 37791 RVA: 0x002FDDCC File Offset: 0x002FBFCC
	public void SetEnabled(bool active)
	{
		VisualController component = this.buttonContainer.GetComponent<VisualController>();
		if (active)
		{
			component.SetState("Active");
			return;
		}
		component.SetState("Inactive");
	}

	// Token: 0x060093A0 RID: 37792 RVA: 0x002FDE01 File Offset: 0x002FC001
	public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
	{
		return this.buttonContainer.GetComponent<Clickable>().AddEventListener(type, handler);
	}

	// Token: 0x04007BA6 RID: 31654
	public UberText uberText;

	// Token: 0x04007BA7 RID: 31655
	public GameObject buttonContainer;
}
