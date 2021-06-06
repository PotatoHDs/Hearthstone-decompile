using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
[CustomEditClass]
public class AdventureWingProgressDisplay : MonoBehaviour
{
	// Token: 0x06000574 RID: 1396 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void UpdateProgress(WingDbId wingDbId, bool linearComplete)
	{
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool HasProgressAnimationToPlay()
	{
		return false;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001FA68 File Offset: 0x0001DC68
	public virtual void PlayProgressAnimation(AdventureWingProgressDisplay.OnAnimationComplete onAnimComplete = null)
	{
		if (onAnimComplete != null)
		{
			onAnimComplete();
		}
	}

	// Token: 0x02001352 RID: 4946
	// (Invoke) Token: 0x0600D72A RID: 55082
	public delegate void OnAnimationComplete();
}
