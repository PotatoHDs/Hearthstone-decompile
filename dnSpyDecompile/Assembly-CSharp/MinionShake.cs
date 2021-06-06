using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A52 RID: 2642
public class MinionShake : MonoBehaviour
{
	// Token: 0x06008E73 RID: 36467 RVA: 0x002DF52C File Offset: 0x002DD72C
	private void LateUpdate()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RenderQualityLevel == GraphicsQuality.Low)
		{
			return;
		}
		if (!this.m_Animating)
		{
			return;
		}
		if (this.m_Animator == null)
		{
			return;
		}
		if (this.m_MinionShakeInstance == null)
		{
			return;
		}
		if (this.m_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == MinionShake.s_IdleState && !this.m_Animator.GetBool("shake"))
		{
			base.transform.localPosition = this.m_MinionOrgPos;
			base.transform.localRotation = this.m_MinionOrgRot;
			this.m_Animating = false;
			return;
		}
		base.transform.localPosition = this.m_CardPlayAllyTransform.localPosition + this.m_MinionOrgPos;
		base.transform.localRotation = this.m_MinionOrgRot;
		base.transform.Rotate(this.m_CardPlayAllyTransform.localRotation.eulerAngles);
	}

	// Token: 0x06008E74 RID: 36468 RVA: 0x002DF619 File Offset: 0x002DD819
	private void OnDestroy()
	{
		if (this.m_MinionShakeInstance)
		{
			UnityEngine.Object.Destroy(this.m_MinionShakeInstance);
		}
	}

	// Token: 0x06008E75 RID: 36469 RVA: 0x002DF633 File Offset: 0x002DD833
	public bool isShaking()
	{
		return this.m_Animating;
	}

	// Token: 0x06008E76 RID: 36470 RVA: 0x002DF63C File Offset: 0x002DD83C
	public static void ShakeAllMinions(GameObject shakeTrigger, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay)
	{
		foreach (MinionShake minionShake in MinionShake.FindAllMinionShakers(shakeTrigger))
		{
			minionShake.m_StartDelay = startDelay;
			minionShake.m_ShakeType = shakeType;
			minionShake.m_ImpactPosition = impactPoint;
			minionShake.m_ShakeIntensityType = intensityType;
			minionShake.m_IntensityValue = intensityValue;
			minionShake.m_Radius = radius;
			minionShake.ShakeMinion();
			BoardEvents boardEvents = BoardEvents.Get();
			if (boardEvents != null)
			{
				boardEvents.MinionShakeEvent(intensityType, intensityValue);
			}
		}
	}

	// Token: 0x06008E77 RID: 36471 RVA: 0x002DF6AC File Offset: 0x002DD8AC
	public static void ShakeTargetMinion(GameObject shakeTarget, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay)
	{
		Spell spell = SceneUtils.FindComponentInThisOrParents<Spell>(shakeTarget);
		if (spell == null)
		{
			Debug.LogWarning("MinionShake: failed to locate Spell component");
			return;
		}
		GameObject visualTarget = spell.GetVisualTarget();
		if (visualTarget == null)
		{
			Debug.LogWarning("MinionShake: failed to Spell GetVisualTarget");
			return;
		}
		MinionShake componentInChildren = visualTarget.GetComponentInChildren<MinionShake>();
		if (componentInChildren == null)
		{
			Debug.LogWarning("MinionShake: failed to locate MinionShake component");
			return;
		}
		componentInChildren.m_StartDelay = startDelay;
		componentInChildren.m_ShakeType = shakeType;
		componentInChildren.m_ImpactPosition = impactPoint;
		componentInChildren.m_ShakeIntensityType = intensityType;
		componentInChildren.m_IntensityValue = intensityValue;
		componentInChildren.m_Radius = radius;
		componentInChildren.ShakeMinion();
	}

	// Token: 0x06008E78 RID: 36472 RVA: 0x002DF73D File Offset: 0x002DD93D
	public static void ShakeObject(GameObject shakeObject, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay)
	{
		MinionShake.ShakeObject(shakeObject, shakeType, impactPoint, intensityType, intensityValue, radius, startDelay, false);
	}

	// Token: 0x06008E79 RID: 36473 RVA: 0x002DF750 File Offset: 0x002DD950
	public static void ShakeObject(GameObject shakeObject, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay, bool ignoreAnimationPlaying)
	{
		MinionShake.ShakeObject(shakeObject, shakeType, impactPoint, intensityType, intensityValue, radius, startDelay, false, ignoreAnimationPlaying);
	}

	// Token: 0x06008E7A RID: 36474 RVA: 0x002DF770 File Offset: 0x002DD970
	public static void ShakeObject(GameObject shakeObject, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay, bool ignoreAnimationPlaying, bool ignoreHeight)
	{
		MinionShake componentInChildren = shakeObject.GetComponentInChildren<MinionShake>();
		if (componentInChildren == null)
		{
			Actor actor = SceneUtils.FindComponentInParents<Actor>(shakeObject);
			if (actor == null)
			{
				return;
			}
			componentInChildren = actor.gameObject.GetComponentInChildren<MinionShake>();
			if (componentInChildren == null)
			{
				return;
			}
		}
		componentInChildren.m_StartDelay = startDelay;
		componentInChildren.m_ShakeType = shakeType;
		componentInChildren.m_ImpactPosition = impactPoint;
		componentInChildren.m_ShakeIntensityType = intensityType;
		componentInChildren.m_IntensityValue = intensityValue;
		componentInChildren.m_Radius = radius;
		componentInChildren.m_IgnoreAnimationPlaying = ignoreAnimationPlaying;
		componentInChildren.ShakeMinion();
	}

	// Token: 0x06008E7B RID: 36475 RVA: 0x002DF7F0 File Offset: 0x002DD9F0
	public void ShakeAllMinionsRandomMedium()
	{
		Vector3 impactPoint = Vector3.zero;
		Board board = Board.Get();
		if (board != null)
		{
			Transform transform = board.FindBone("CenterPointBone");
			if (transform != null)
			{
				impactPoint = transform.position;
			}
		}
		MinionShake.ShakeAllMinions(base.gameObject, ShakeMinionType.RandomDirection, impactPoint, ShakeMinionIntensity.MediumShake, 0.5f, 0f, 0f);
	}

	// Token: 0x06008E7C RID: 36476 RVA: 0x002DF84C File Offset: 0x002DDA4C
	public void ShakeAllMinionsRandomLarge()
	{
		Vector3 impactPoint = Vector3.zero;
		Board board = Board.Get();
		if (board != null)
		{
			Transform transform = board.FindBone("CenterPointBone");
			if (transform != null)
			{
				impactPoint = transform.position;
			}
		}
		MinionShake.ShakeAllMinions(base.gameObject, ShakeMinionType.RandomDirection, impactPoint, ShakeMinionIntensity.LargeShake, 1f, 0f, 0f);
	}

	// Token: 0x06008E7D RID: 36477 RVA: 0x002DF8A7 File Offset: 0x002DDAA7
	public void RandomShake(float impact)
	{
		this.m_ShakeIntensityType = ShakeMinionIntensity.Custom;
		this.m_IntensityValue = impact;
		this.m_ShakeType = ShakeMinionType.Angle;
		this.m_ShakeType = ShakeMinionType.RandomDirection;
		this.ShakeMinion();
	}

	// Token: 0x06008E7E RID: 36478 RVA: 0x002DF8CC File Offset: 0x002DDACC
	private void ShakeMinion()
	{
		if (GraphicsManager.Get() == null || GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			return;
		}
		if (this.m_MinionShakeAnimator == null)
		{
			Debug.LogWarning("MinionShake: failed to locate MinionShake Animator");
			return;
		}
		Animation component = base.GetComponent<Animation>();
		if (component != null && component.isPlaying && !this.m_IgnoreAnimationPlaying)
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		Board board = Board.Get();
		if (board != null)
		{
			Transform transform = board.FindBone("CenterPointBone");
			if (transform != null)
			{
				vector = transform.position;
			}
		}
		if (vector.y - base.transform.position.y > 0.1f && !this.m_IgnoreHeight)
		{
			return;
		}
		if (this.m_MinionShakeInstance == null)
		{
			this.m_MinionShakeInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_MinionShakeAnimator, this.OFFSCREEN_POSITION, base.transform.rotation);
			this.m_CardPlayAllyTransform = this.m_MinionShakeInstance.transform.Find("Card_Play_Ally").gameObject.transform;
		}
		if (this.m_Animator == null)
		{
			this.m_Animator = this.m_MinionShakeInstance.GetComponent<Animator>();
		}
		if (!this.m_Animating)
		{
			this.m_MinionOrgPos = base.transform.localPosition;
			this.m_MinionOrgRot = base.transform.localRotation;
		}
		if (this.m_ShakeType == ShakeMinionType.Angle)
		{
			this.m_ImpactDirection = this.AngleToVector(this.m_Angle);
		}
		else if (this.m_ShakeType == ShakeMinionType.ImpactDirection)
		{
			this.m_ImpactDirection = Vector3.Normalize(base.transform.position - this.m_ImpactPosition);
		}
		else if (this.m_ShakeType == ShakeMinionType.RandomDirection)
		{
			this.m_ImpactDirection = this.AngleToVector(UnityEngine.Random.Range(0f, 360f));
		}
		float d = this.m_IntensityValue;
		if (this.m_ShakeIntensityType == ShakeMinionIntensity.SmallShake)
		{
			d = 0.1f;
		}
		else if (this.m_ShakeIntensityType == ShakeMinionIntensity.MediumShake)
		{
			d = 0.5f;
		}
		else if (this.m_ShakeIntensityType == ShakeMinionIntensity.LargeShake)
		{
			d = 1f;
		}
		this.m_ImpactDirection *= d;
		this.m_Animator.SetFloat("posx", this.m_ImpactDirection.x);
		this.m_Animator.SetFloat("posy", this.m_ImpactDirection.y);
		if (this.m_Radius > 0f && Vector3.Distance(base.transform.position, this.m_ImpactPosition) > this.m_Radius)
		{
			return;
		}
		if (this.m_StartDelay > 0f)
		{
			base.StartCoroutine(this.StartShakeAnimation());
		}
		else
		{
			this.m_Animating = true;
			this.m_Animator.SetBool("shake", true);
		}
		base.StartCoroutine(this.ResetShakeAnimator());
	}

	// Token: 0x06008E7F RID: 36479 RVA: 0x002DFB80 File Offset: 0x002DDD80
	private Vector2 AngleToVector(float angle)
	{
		Vector3 vector = Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, 0f, -1f);
		return new Vector2(vector.x, vector.z);
	}

	// Token: 0x06008E80 RID: 36480 RVA: 0x002DFBC8 File Offset: 0x002DDDC8
	private IEnumerator StartShakeAnimation()
	{
		yield return new WaitForSeconds(this.m_StartDelay);
		this.m_Animating = true;
		this.m_Animator.SetBool("shake", true);
		yield break;
	}

	// Token: 0x06008E81 RID: 36481 RVA: 0x002DFBD7 File Offset: 0x002DDDD7
	private IEnumerator ResetShakeAnimator()
	{
		yield return new WaitForSeconds(this.m_StartDelay);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.m_Animator.SetBool("shake", false);
		yield break;
	}

	// Token: 0x06008E82 RID: 36482 RVA: 0x002DFBE8 File Offset: 0x002DDDE8
	private static MinionShake[] FindAllMinionShakers(GameObject shakeTrigger)
	{
		Card y = null;
		Spell spell = SceneUtils.FindComponentInThisOrParents<Spell>(shakeTrigger);
		if (spell != null)
		{
			y = spell.GetSourceCard();
		}
		List<MinionShake> list = new List<MinionShake>();
		foreach (Zone zone in ZoneMgr.Get().FindZonesForTag(TAG_ZONE.PLAY))
		{
			if (!(zone.GetType() == typeof(ZoneHero)))
			{
				foreach (Card card in zone.GetCards())
				{
					if (!(card == y) && card.IsActorReady())
					{
						MinionShake componentInChildren = card.GetComponentInChildren<MinionShake>();
						if (!(componentInChildren == null))
						{
							list.Add(componentInChildren);
						}
					}
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x0400769F RID: 30367
	private readonly Vector3 OFFSCREEN_POSITION = new Vector3(-400f, -400f, -400f);

	// Token: 0x040076A0 RID: 30368
	private const float INTENSITY_SMALL = 0.1f;

	// Token: 0x040076A1 RID: 30369
	private const float INTENSITY_MEDIUM = 0.5f;

	// Token: 0x040076A2 RID: 30370
	private const float INTENSITY_LARGE = 1f;

	// Token: 0x040076A3 RID: 30371
	private const float DISABLE_HEIGHT = 0.1f;

	// Token: 0x040076A4 RID: 30372
	public GameObject m_MinionShakeAnimator;

	// Token: 0x040076A5 RID: 30373
	private bool m_Animating;

	// Token: 0x040076A6 RID: 30374
	private Animator m_Animator;

	// Token: 0x040076A7 RID: 30375
	private Vector2 m_ImpactDirection;

	// Token: 0x040076A8 RID: 30376
	private Vector3 m_ImpactPosition;

	// Token: 0x040076A9 RID: 30377
	private float m_Angle;

	// Token: 0x040076AA RID: 30378
	private ShakeMinionIntensity m_ShakeIntensityType = ShakeMinionIntensity.MediumShake;

	// Token: 0x040076AB RID: 30379
	private float m_IntensityValue = 0.5f;

	// Token: 0x040076AC RID: 30380
	private ShakeMinionType m_ShakeType = ShakeMinionType.RandomDirection;

	// Token: 0x040076AD RID: 30381
	private GameObject m_MinionShakeInstance;

	// Token: 0x040076AE RID: 30382
	private Transform m_CardPlayAllyTransform;

	// Token: 0x040076AF RID: 30383
	private Vector3 m_MinionOrgPos;

	// Token: 0x040076B0 RID: 30384
	private Quaternion m_MinionOrgRot;

	// Token: 0x040076B1 RID: 30385
	private float m_StartDelay;

	// Token: 0x040076B2 RID: 30386
	private float m_Radius;

	// Token: 0x040076B3 RID: 30387
	private bool m_IgnoreAnimationPlaying;

	// Token: 0x040076B4 RID: 30388
	private bool m_IgnoreHeight;

	// Token: 0x040076B5 RID: 30389
	private static int s_IdleState = Animator.StringToHash("Base.Idle");
}
