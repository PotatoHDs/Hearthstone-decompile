using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class FiresideGatheringSignAnimationCallbackBehaviour : MonoBehaviour
{
	// Token: 0x06002833 RID: 10291 RVA: 0x000CA347 File Offset: 0x000C8547
	public void EnableShadowOnSign()
	{
		this.m_ParentSign.SetSignShadowEnabled(true);
	}

	// Token: 0x06002834 RID: 10292 RVA: 0x000CA355 File Offset: 0x000C8555
	public void DisableShadowOnSign()
	{
		this.m_ParentSign.SetSignShadowEnabled(false);
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x000CA363 File Offset: 0x000C8563
	public void OnSignSocketAnimationComplete()
	{
		this.m_ParentSign.FireSignSocketAnimationCompleteListener();
	}

	// Token: 0x040016D2 RID: 5842
	public FiresideGatheringSign m_ParentSign;
}
