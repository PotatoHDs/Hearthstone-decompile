using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Misdirection : Spell
{
	private enum TargetingMetadata
	{
		DestinationTarget,
		AttackingEntity,
		InitialTarget
	}

	public float m_ReticleFadeInTime = 0.8f;

	public float m_ReticleFadeOutTime = 0.4f;

	public float m_ReticlePathTime = 3f;

	public float m_ReticleBlur = 0.005f;

	public float m_ReticleBlurFocusTime = 0.8f;

	public Color m_ReticleAttackColor = Color.red;

	public float m_ReticleAttackScale = 1.1f;

	public float m_ReticleAttackTime = 0.3f;

	public Vector3 m_ReticleAttackRotate = new Vector3(0f, 90f, 0f);

	public float m_ReticleAttackHold = 0.25f;

	public GameObject m_Reticle;

	public bool m_AllowTargetingInitialTarget;

	public int m_ReticlePathDesiredMinimumTargets = 3;

	public int m_ReticlePathDesiredMaximumTargets = 4;

	private GameObject m_ReticleInstance;

	private Card m_AttackingEntityCard;

	private Card m_InitialTargetCard;

	private Color m_OrgAmbient;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		AddMultiplePowerTargets();
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartAnimation();
	}

	private void ResolveTargets()
	{
		List<GameObject> targets = GetTargets();
		if (targets.Count < 3)
		{
			return;
		}
		m_AttackingEntityCard = targets[1].GetComponent<Card>();
		GameState gameState = GameState.Get();
		GameEntity gameEntity = gameState.GetGameEntity();
		Entity entity = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_DEFENDER));
		if (entity != null)
		{
			m_InitialTargetCard = entity.GetCard();
			return;
		}
		Entity entity2 = gameState.GetEntity(m_AttackingEntityCard.GetEntity().GetTag(GAME_TAG.CARD_TARGET));
		if (entity2 != null)
		{
			m_InitialTargetCard = entity2.GetCard();
		}
		else
		{
			m_InitialTargetCard = targets[2].GetComponent<Card>();
		}
	}

	private void StartAnimation()
	{
		ResolveTargets();
		if (m_InitialTargetCard == null)
		{
			OnSpellFinished();
			return;
		}
		m_ReticleInstance = UnityEngine.Object.Instantiate(m_Reticle, m_InitialTargetCard.transform.position, Quaternion.identity);
		Material renderMaterial = m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial();
		renderMaterial.SetFloat("_Alpha", 0f);
		renderMaterial.SetFloat("_blur", m_ReticleBlur);
		StartCoroutine(ReticleFadeIn());
		StartCoroutine(AnimateReticle());
		AudioSource component = GetComponent<AudioSource>();
		if (component != null)
		{
			SoundManager.Get().Play(component);
		}
	}

	private IEnumerator ReticleFadeIn()
	{
		Action<object> action = delegate(object amount)
		{
			m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", (float)amount);
		};
		Hashtable args = iTween.Hash("time", m_ReticleFadeInTime, "from", 0f, "to", 1f, "onupdate", action, "onupdatetarget", m_ReticleInstance.gameObject);
		iTween.ValueTo(m_ReticleInstance.gameObject, args);
		Hashtable args2 = iTween.Hash("time", m_ReticleFadeInTime, "scale", Vector3.one, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_ReticleInstance.gameObject, args2);
		yield break;
	}

	private void SetReticleAlphaValue(float val)
	{
		m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", val);
	}

	private IEnumerator AnimateReticle()
	{
		yield return new WaitForSeconds(m_ReticleFadeInTime);
		Hashtable args = iTween.Hash("path", BuildAnimationPath(), "time", m_ReticlePathTime, "easetype", iTween.EaseType.easeInOutQuad, "oncomplete", "ReticleAnimationComplete", "oncompletetarget", base.gameObject, "orienttopath", false);
		iTween.MoveTo(m_ReticleInstance, args);
	}

	private void ReticleAnimationComplete()
	{
		StartCoroutine(ReticleAttackAnimation());
	}

	private IEnumerator ReticleAttackAnimation()
	{
		Action<object> action = delegate(object col)
		{
			if (m_ReticleInstance != null)
			{
				m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetColor("_Color", (Color)col);
			}
		};
		Hashtable args = iTween.Hash("time", m_ReticleAttackTime, "from", m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().color, "to", m_ReticleAttackColor, "onupdate", action, "onupdatetarget", base.gameObject);
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("time", m_ReticleAttackTime, "scale", m_ReticleAttackScale, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_ReticleInstance, args2);
		Hashtable args3 = iTween.Hash("time", m_ReticleAttackTime, "rotation", m_ReticleAttackRotate, "easetype", iTween.EaseType.easeOutBounce);
		iTween.RotateTo(m_ReticleInstance, args3);
		Action<object> action2 = delegate(object amount)
		{
			if (m_ReticleInstance != null)
			{
				m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Blur", (float)amount);
			}
		};
		Hashtable args4 = iTween.Hash("time", m_ReticleBlurFocusTime, "from", m_ReticleBlur, "to", 0f, "onupdate", action2, "onupdatetarget", base.gameObject);
		iTween.ValueTo(base.gameObject, args4);
		yield return new WaitForSeconds(m_ReticleBlurFocusTime + m_ReticleAttackHold);
		StartCoroutine(ReticleFadeOut());
	}

	private IEnumerator ReticleFadeOut()
	{
		OnSpellFinished();
		Action<object> action = delegate(object amount)
		{
			if (m_ReticleInstance != null)
			{
				m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", (float)amount);
			}
		};
		Hashtable args = iTween.Hash("time", m_ReticleFadeOutTime, "from", 1f, "to", 0f, "onupdate", action, "onupdatetarget", base.gameObject);
		iTween.ValueTo(base.gameObject, args);
		yield return new WaitForSeconds(m_ReticleFadeOutTime);
		UnityEngine.Object.Destroy(m_ReticleInstance);
	}

	private Vector3[] BuildAnimationPath()
	{
		Card[] array = FindPossibleTargetCards();
		int num = UnityEngine.Random.Range(m_ReticlePathDesiredMinimumTargets, m_ReticlePathDesiredMaximumTargets);
		if (num >= array.Length + 2)
		{
			num = array.Length + 2;
		}
		if (array.Length <= 1)
		{
			return new Vector3[2]
			{
				m_InitialTargetCard.transform.position,
				GetTarget().transform.position
			};
		}
		List<Vector3> list = new List<Vector3>();
		list.Add(m_InitialTargetCard.transform.position);
		GameObject gameObject = m_InitialTargetCard.gameObject;
		for (int i = 1; i < num; i++)
		{
			GameObject gameObject2 = array[UnityEngine.Random.Range(0, array.Length - 1)].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2 = array[UnityEngine.Random.Range(0, array.Length - 1)].gameObject;
				if (gameObject2 == gameObject)
				{
					gameObject2 = ((!(gameObject2 == array[array.Length - 1])) ? array[array.Length - 1].gameObject : array[0].gameObject);
				}
			}
			if (i == num - 1 && gameObject2 == GetTarget() && gameObject2 == gameObject)
			{
				gameObject2 = ((!(gameObject2 == array[array.Length - 1])) ? array[array.Length - 1].gameObject : array[0].gameObject);
			}
			list.Add(gameObject2.transform.position);
		}
		list.Add(GetTarget().transform.position);
		return list.ToArray();
	}

	private Card[] FindPossibleTargetCards()
	{
		List<Card> list = new List<Card>();
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			return list.ToArray();
		}
		foreach (ZonePlay item in zoneMgr.FindZonesOfType<ZonePlay>())
		{
			foreach (Card card in item.GetCards())
			{
				if (!(card == m_AttackingEntityCard) && (!(card == m_InitialTargetCard) || m_AllowTargetingInitialTarget))
				{
					list.Add(card);
				}
			}
		}
		foreach (ZoneHero item2 in zoneMgr.FindZonesOfType<ZoneHero>())
		{
			foreach (Card card2 in item2.GetCards())
			{
				if (!(card2 == m_AttackingEntityCard) && (!(card2 == m_InitialTargetCard) || m_AllowTargetingInitialTarget))
				{
					list.Add(card2);
				}
			}
		}
		return list.ToArray();
	}

	private Card[] GetOpponentZoneMinions()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in GameState.Get().GetFirstOpponentPlayer(GetSourceCard().GetController()).GetBattlefieldZone()
			.GetCards())
		{
			if (!(card == m_AttackingEntityCard))
			{
				list.Add(card);
			}
		}
		return list.ToArray();
	}

	private Card GetCurrentPlayerHeroCard()
	{
		return GetSourceCard().GetController().GetHeroCard();
	}

	private Card GetOpponentHeroCard()
	{
		return GameState.Get().GetFirstOpponentPlayer(GetSourceCard().GetController()).GetHeroCard();
	}
}
