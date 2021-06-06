using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionShake : MonoBehaviour
{
	private readonly Vector3 OFFSCREEN_POSITION = new Vector3(-400f, -400f, -400f);

	private const float INTENSITY_SMALL = 0.1f;

	private const float INTENSITY_MEDIUM = 0.5f;

	private const float INTENSITY_LARGE = 1f;

	private const float DISABLE_HEIGHT = 0.1f;

	public GameObject m_MinionShakeAnimator;

	private bool m_Animating;

	private Animator m_Animator;

	private Vector2 m_ImpactDirection;

	private Vector3 m_ImpactPosition;

	private float m_Angle;

	private ShakeMinionIntensity m_ShakeIntensityType = ShakeMinionIntensity.MediumShake;

	private float m_IntensityValue = 0.5f;

	private ShakeMinionType m_ShakeType = ShakeMinionType.RandomDirection;

	private GameObject m_MinionShakeInstance;

	private Transform m_CardPlayAllyTransform;

	private Vector3 m_MinionOrgPos;

	private Quaternion m_MinionOrgRot;

	private float m_StartDelay;

	private float m_Radius;

	private bool m_IgnoreAnimationPlaying;

	private bool m_IgnoreHeight;

	private static int s_IdleState = Animator.StringToHash("Base.Idle");

	private void LateUpdate()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if ((graphicsManager == null || graphicsManager.RenderQualityLevel != 0) && m_Animating && !(m_Animator == null) && !(m_MinionShakeInstance == null))
		{
			if (m_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == s_IdleState && !m_Animator.GetBool("shake"))
			{
				base.transform.localPosition = m_MinionOrgPos;
				base.transform.localRotation = m_MinionOrgRot;
				m_Animating = false;
			}
			else
			{
				base.transform.localPosition = m_CardPlayAllyTransform.localPosition + m_MinionOrgPos;
				base.transform.localRotation = m_MinionOrgRot;
				base.transform.Rotate(m_CardPlayAllyTransform.localRotation.eulerAngles);
			}
		}
	}

	private void OnDestroy()
	{
		if ((bool)m_MinionShakeInstance)
		{
			Object.Destroy(m_MinionShakeInstance);
		}
	}

	public bool isShaking()
	{
		return m_Animating;
	}

	public static void ShakeAllMinions(GameObject shakeTrigger, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay)
	{
		MinionShake[] array = FindAllMinionShakers(shakeTrigger);
		foreach (MinionShake obj in array)
		{
			obj.m_StartDelay = startDelay;
			obj.m_ShakeType = shakeType;
			obj.m_ImpactPosition = impactPoint;
			obj.m_ShakeIntensityType = intensityType;
			obj.m_IntensityValue = intensityValue;
			obj.m_Radius = radius;
			obj.ShakeMinion();
			BoardEvents boardEvents = BoardEvents.Get();
			if (boardEvents != null)
			{
				boardEvents.MinionShakeEvent(intensityType, intensityValue);
			}
		}
	}

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

	public static void ShakeObject(GameObject shakeObject, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay)
	{
		ShakeObject(shakeObject, shakeType, impactPoint, intensityType, intensityValue, radius, startDelay, ignoreAnimationPlaying: false);
	}

	public static void ShakeObject(GameObject shakeObject, ShakeMinionType shakeType, Vector3 impactPoint, ShakeMinionIntensity intensityType, float intensityValue, float radius, float startDelay, bool ignoreAnimationPlaying)
	{
		ShakeObject(shakeObject, shakeType, impactPoint, intensityType, intensityValue, radius, startDelay, ignoreAnimationPlaying: false, ignoreAnimationPlaying);
	}

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
		ShakeAllMinions(base.gameObject, ShakeMinionType.RandomDirection, impactPoint, ShakeMinionIntensity.MediumShake, 0.5f, 0f, 0f);
	}

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
		ShakeAllMinions(base.gameObject, ShakeMinionType.RandomDirection, impactPoint, ShakeMinionIntensity.LargeShake, 1f, 0f, 0f);
	}

	public void RandomShake(float impact)
	{
		m_ShakeIntensityType = ShakeMinionIntensity.Custom;
		m_IntensityValue = impact;
		m_ShakeType = ShakeMinionType.Angle;
		m_ShakeType = ShakeMinionType.RandomDirection;
		ShakeMinion();
	}

	private void ShakeMinion()
	{
		if (GraphicsManager.Get() == null || GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			return;
		}
		if (m_MinionShakeAnimator == null)
		{
			Debug.LogWarning("MinionShake: failed to locate MinionShake Animator");
			return;
		}
		Animation component = GetComponent<Animation>();
		if (component != null && component.isPlaying && !m_IgnoreAnimationPlaying)
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
		if (vector.y - base.transform.position.y > 0.1f && !m_IgnoreHeight)
		{
			return;
		}
		if (m_MinionShakeInstance == null)
		{
			m_MinionShakeInstance = Object.Instantiate(m_MinionShakeAnimator, OFFSCREEN_POSITION, base.transform.rotation);
			m_CardPlayAllyTransform = m_MinionShakeInstance.transform.Find("Card_Play_Ally").gameObject.transform;
		}
		if (m_Animator == null)
		{
			m_Animator = m_MinionShakeInstance.GetComponent<Animator>();
		}
		if (!m_Animating)
		{
			m_MinionOrgPos = base.transform.localPosition;
			m_MinionOrgRot = base.transform.localRotation;
		}
		if (m_ShakeType == ShakeMinionType.Angle)
		{
			m_ImpactDirection = AngleToVector(m_Angle);
		}
		else if (m_ShakeType == ShakeMinionType.ImpactDirection)
		{
			m_ImpactDirection = Vector3.Normalize(base.transform.position - m_ImpactPosition);
		}
		else if (m_ShakeType == ShakeMinionType.RandomDirection)
		{
			m_ImpactDirection = AngleToVector(Random.Range(0f, 360f));
		}
		float num = m_IntensityValue;
		if (m_ShakeIntensityType == ShakeMinionIntensity.SmallShake)
		{
			num = 0.1f;
		}
		else if (m_ShakeIntensityType == ShakeMinionIntensity.MediumShake)
		{
			num = 0.5f;
		}
		else if (m_ShakeIntensityType == ShakeMinionIntensity.LargeShake)
		{
			num = 1f;
		}
		m_ImpactDirection *= num;
		m_Animator.SetFloat("posx", m_ImpactDirection.x);
		m_Animator.SetFloat("posy", m_ImpactDirection.y);
		if (!(m_Radius > 0f) || !(Vector3.Distance(base.transform.position, m_ImpactPosition) > m_Radius))
		{
			if (m_StartDelay > 0f)
			{
				StartCoroutine(StartShakeAnimation());
			}
			else
			{
				m_Animating = true;
				m_Animator.SetBool("shake", value: true);
			}
			StartCoroutine(ResetShakeAnimator());
		}
	}

	private Vector2 AngleToVector(float angle)
	{
		Vector3 vector = Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, 0f, -1f);
		return new Vector2(vector.x, vector.z);
	}

	private IEnumerator StartShakeAnimation()
	{
		yield return new WaitForSeconds(m_StartDelay);
		m_Animating = true;
		m_Animator.SetBool("shake", value: true);
	}

	private IEnumerator ResetShakeAnimator()
	{
		yield return new WaitForSeconds(m_StartDelay);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		m_Animator.SetBool("shake", value: false);
	}

	private static MinionShake[] FindAllMinionShakers(GameObject shakeTrigger)
	{
		Card card = null;
		Spell spell = SceneUtils.FindComponentInThisOrParents<Spell>(shakeTrigger);
		if (spell != null)
		{
			card = spell.GetSourceCard();
		}
		List<MinionShake> list = new List<MinionShake>();
		foreach (Zone item in ZoneMgr.Get().FindZonesForTag(TAG_ZONE.PLAY))
		{
			if (item.GetType() == typeof(ZoneHero))
			{
				continue;
			}
			foreach (Card card2 in item.GetCards())
			{
				if (!(card2 == card) && card2.IsActorReady())
				{
					MinionShake componentInChildren = card2.GetComponentInChildren<MinionShake>();
					if (!(componentInChildren == null))
					{
						list.Add(componentInChildren);
					}
				}
			}
		}
		return list.ToArray();
	}
}
