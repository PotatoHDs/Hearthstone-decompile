using System;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x02000761 RID: 1889
[ActionCategory("Pegasus")]
[HutongGames.PlayMaker.Tooltip("Shake Minions")]
public class ShakeMinionsAction : FsmStateAction
{
	// Token: 0x060069F2 RID: 27122 RVA: 0x00228B0A File Offset: 0x00226D0A
	public override void Reset()
	{
		this.gameObject = null;
		this.MinionsToShake = ShakeMinionsAction.MinionsToShakeEnum.All;
		this.shakeType = ShakeMinionType.RandomDirection;
		this.shakeSize = ShakeMinionIntensity.SmallShake;
		this.customShakeIntensity = 0.1f;
		this.radius = 0f;
	}

	// Token: 0x060069F3 RID: 27123 RVA: 0x00228B48 File Offset: 0x00226D48
	public override void OnEnter()
	{
		this.DoShakeMinions();
		base.Finish();
	}

	// Token: 0x060069F4 RID: 27124 RVA: 0x00228B58 File Offset: 0x00226D58
	private void DoShakeMinions()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
		if (ownerDefaultTarget == null)
		{
			base.Finish();
			return;
		}
		if (this.MinionsToShake == ShakeMinionsAction.MinionsToShakeEnum.All)
		{
			MinionShake.ShakeAllMinions(ownerDefaultTarget, this.shakeType, ownerDefaultTarget.transform.position, this.shakeSize, this.customShakeIntensity.Value, this.radius.Value, 0f);
			return;
		}
		if (this.MinionsToShake == ShakeMinionsAction.MinionsToShakeEnum.Target)
		{
			MinionShake.ShakeTargetMinion(ownerDefaultTarget, this.shakeType, ownerDefaultTarget.transform.position, this.shakeSize, this.customShakeIntensity.Value, 0f, 0f);
			return;
		}
		if (this.MinionsToShake == ShakeMinionsAction.MinionsToShakeEnum.SelectedGameObject)
		{
			MinionShake.ShakeObject(ownerDefaultTarget, this.shakeType, ownerDefaultTarget.transform.position, this.shakeSize, this.customShakeIntensity.Value, 0f, 0f);
		}
	}

	// Token: 0x040056C4 RID: 22212
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Impact Object Location")]
	public FsmOwnerDefault gameObject;

	// Token: 0x040056C5 RID: 22213
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Shake Type")]
	public ShakeMinionType shakeType = ShakeMinionType.RandomDirection;

	// Token: 0x040056C6 RID: 22214
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Minions To Shake")]
	public ShakeMinionsAction.MinionsToShakeEnum MinionsToShake;

	// Token: 0x040056C7 RID: 22215
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Shake Intensity")]
	public ShakeMinionIntensity shakeSize = ShakeMinionIntensity.SmallShake;

	// Token: 0x040056C8 RID: 22216
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Custom Shake Intensity 0-1. Used when Shake Size is Custom")]
	public FsmFloat customShakeIntensity;

	// Token: 0x040056C9 RID: 22217
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Radius - 0 = for all objects")]
	public FsmFloat radius;

	// Token: 0x02002335 RID: 9013
	public enum MinionsToShakeEnum
	{
		// Token: 0x0400E60A RID: 58890
		All,
		// Token: 0x0400E60B RID: 58891
		Target,
		// Token: 0x0400E60C RID: 58892
		SelectedGameObject
	}
}
