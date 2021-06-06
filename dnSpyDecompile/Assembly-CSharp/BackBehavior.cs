using System;
using UnityEngine;

// Token: 0x02000AED RID: 2797
public class BackBehavior : MonoBehaviour
{
	// Token: 0x060094BE RID: 38078 RVA: 0x00302FCC File Offset: 0x003011CC
	public void Awake()
	{
		PegUIElement component = base.gameObject.GetComponent<PegUIElement>();
		if (component != null)
		{
			component.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnRelease();
			});
		}
	}

	// Token: 0x060094BF RID: 38079 RVA: 0x00004EB5 File Offset: 0x000030B5
	public void OnRelease()
	{
		Navigation.GoBack();
	}
}
