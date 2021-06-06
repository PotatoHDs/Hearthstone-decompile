using System;
using UnityEngine;

// Token: 0x02000A0B RID: 2571
public class AnimationEventDispatcher : MonoBehaviour
{
	// Token: 0x14000091 RID: 145
	// (add) Token: 0x06008B21 RID: 35617 RVA: 0x002C7BA4 File Offset: 0x002C5DA4
	// (remove) Token: 0x06008B22 RID: 35618 RVA: 0x002C7BDC File Offset: 0x002C5DDC
	private event OnAnimationEvent AnimationEventRecieved;

	// Token: 0x06008B23 RID: 35619 RVA: 0x002C7C11 File Offset: 0x002C5E11
	public void RegisterAnimationEventListener(OnAnimationEvent listener)
	{
		this.AnimationEventRecieved += listener;
	}

	// Token: 0x06008B24 RID: 35620 RVA: 0x002C7C1A File Offset: 0x002C5E1A
	public void UnregisterAnimationEventListener(OnAnimationEvent listener)
	{
		this.AnimationEventRecieved -= listener;
	}

	// Token: 0x06008B25 RID: 35621 RVA: 0x002C7C23 File Offset: 0x002C5E23
	public void FireAnimationEvent(UnityEngine.Object obj)
	{
		if (this.AnimationEventRecieved != null)
		{
			this.AnimationEventRecieved(obj);
		}
	}
}
