using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Pegasus")]
[HutongGames.PlayMaker.Tooltip("Shake Minions")]
public class ShakeMinionsAction : FsmStateAction
{
	public enum MinionsToShakeEnum
	{
		All,
		Target,
		SelectedGameObject
	}

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Impact Object Location")]
	public FsmOwnerDefault gameObject;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Shake Type")]
	public ShakeMinionType shakeType = ShakeMinionType.RandomDirection;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Minions To Shake")]
	public MinionsToShakeEnum MinionsToShake;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Shake Intensity")]
	public ShakeMinionIntensity shakeSize = ShakeMinionIntensity.SmallShake;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Custom Shake Intensity 0-1. Used when Shake Size is Custom")]
	public FsmFloat customShakeIntensity;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Radius - 0 = for all objects")]
	public FsmFloat radius;

	public override void Reset()
	{
		gameObject = null;
		MinionsToShake = MinionsToShakeEnum.All;
		shakeType = ShakeMinionType.RandomDirection;
		shakeSize = ShakeMinionIntensity.SmallShake;
		customShakeIntensity = 0.1f;
		radius = 0f;
	}

	public override void OnEnter()
	{
		DoShakeMinions();
		Finish();
	}

	private void DoShakeMinions()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
		if (ownerDefaultTarget == null)
		{
			Finish();
		}
		else if (MinionsToShake == MinionsToShakeEnum.All)
		{
			MinionShake.ShakeAllMinions(ownerDefaultTarget, shakeType, ownerDefaultTarget.transform.position, shakeSize, customShakeIntensity.Value, radius.Value, 0f);
		}
		else if (MinionsToShake == MinionsToShakeEnum.Target)
		{
			MinionShake.ShakeTargetMinion(ownerDefaultTarget, shakeType, ownerDefaultTarget.transform.position, shakeSize, customShakeIntensity.Value, 0f, 0f);
		}
		else if (MinionsToShake == MinionsToShakeEnum.SelectedGameObject)
		{
			MinionShake.ShakeObject(ownerDefaultTarget, shakeType, ownerDefaultTarget.transform.position, shakeSize, customShakeIntensity.Value, 0f, 0f);
		}
	}
}
