using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000806 RID: 2054
[RequireComponent(typeof(Animation))]
public class Misdirection : Spell
{
	// Token: 0x06006F51 RID: 28497 RVA: 0x0023E59D File Offset: 0x0023C79D
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		base.AddMultiplePowerTargets();
		return true;
	}

	// Token: 0x06006F52 RID: 28498 RVA: 0x0023E5B1 File Offset: 0x0023C7B1
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.StartAnimation();
	}

	// Token: 0x06006F53 RID: 28499 RVA: 0x0023E5BC File Offset: 0x0023C7BC
	private void ResolveTargets()
	{
		List<GameObject> targets = base.GetTargets();
		if (targets.Count < 3)
		{
			return;
		}
		this.m_AttackingEntityCard = targets[1].GetComponent<Card>();
		GameState gameState = GameState.Get();
		GameEntity gameEntity = gameState.GetGameEntity();
		Entity entity = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_DEFENDER));
		if (entity != null)
		{
			this.m_InitialTargetCard = entity.GetCard();
			return;
		}
		Entity entity2 = gameState.GetEntity(this.m_AttackingEntityCard.GetEntity().GetTag(GAME_TAG.CARD_TARGET));
		if (entity2 != null)
		{
			this.m_InitialTargetCard = entity2.GetCard();
			return;
		}
		this.m_InitialTargetCard = targets[2].GetComponent<Card>();
	}

	// Token: 0x06006F54 RID: 28500 RVA: 0x0023E65C File Offset: 0x0023C85C
	private void StartAnimation()
	{
		this.ResolveTargets();
		if (this.m_InitialTargetCard == null)
		{
			this.OnSpellFinished();
			return;
		}
		this.m_ReticleInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_Reticle, this.m_InitialTargetCard.transform.position, Quaternion.identity);
		Material renderMaterial = this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial();
		renderMaterial.SetFloat("_Alpha", 0f);
		renderMaterial.SetFloat("_blur", this.m_ReticleBlur);
		base.StartCoroutine(this.ReticleFadeIn());
		base.StartCoroutine(this.AnimateReticle());
		AudioSource component = base.GetComponent<AudioSource>();
		if (component != null)
		{
			SoundManager.Get().Play(component, null, null, null);
		}
	}

	// Token: 0x06006F55 RID: 28501 RVA: 0x0023E713 File Offset: 0x0023C913
	private IEnumerator ReticleFadeIn()
	{
		Action<object> action = delegate(object amount)
		{
			this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleFadeInTime,
			"from",
			0f,
			"to",
			1f,
			"onupdate",
			action,
			"onupdatetarget",
			this.m_ReticleInstance.gameObject
		});
		iTween.ValueTo(this.m_ReticleInstance.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleFadeInTime,
			"scale",
			Vector3.one,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_ReticleInstance.gameObject, args2);
		yield break;
	}

	// Token: 0x06006F56 RID: 28502 RVA: 0x0023E722 File Offset: 0x0023C922
	private void SetReticleAlphaValue(float val)
	{
		this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", val);
	}

	// Token: 0x06006F57 RID: 28503 RVA: 0x0023E73F File Offset: 0x0023C93F
	private IEnumerator AnimateReticle()
	{
		yield return new WaitForSeconds(this.m_ReticleFadeInTime);
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			this.BuildAnimationPath(),
			"time",
			this.m_ReticlePathTime,
			"easetype",
			iTween.EaseType.easeInOutQuad,
			"oncomplete",
			"ReticleAnimationComplete",
			"oncompletetarget",
			base.gameObject,
			"orienttopath",
			false
		});
		iTween.MoveTo(this.m_ReticleInstance, args);
		yield break;
	}

	// Token: 0x06006F58 RID: 28504 RVA: 0x0023E74E File Offset: 0x0023C94E
	private void ReticleAnimationComplete()
	{
		base.StartCoroutine(this.ReticleAttackAnimation());
	}

	// Token: 0x06006F59 RID: 28505 RVA: 0x0023E75D File Offset: 0x0023C95D
	private IEnumerator ReticleAttackAnimation()
	{
		Action<object> action = delegate(object col)
		{
			if (this.m_ReticleInstance != null)
			{
				this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetColor("_Color", (Color)col);
			}
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleAttackTime,
			"from",
			this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().color,
			"to",
			this.m_ReticleAttackColor,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleAttackTime,
			"scale",
			this.m_ReticleAttackScale,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_ReticleInstance, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleAttackTime,
			"rotation",
			this.m_ReticleAttackRotate,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.RotateTo(this.m_ReticleInstance, args3);
		Action<object> action2 = delegate(object amount)
		{
			if (this.m_ReticleInstance != null)
			{
				this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Blur", (float)amount);
			}
		};
		Hashtable args4 = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleBlurFocusTime,
			"from",
			this.m_ReticleBlur,
			"to",
			0f,
			"onupdate",
			action2,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args4);
		yield return new WaitForSeconds(this.m_ReticleBlurFocusTime + this.m_ReticleAttackHold);
		base.StartCoroutine(this.ReticleFadeOut());
		yield break;
	}

	// Token: 0x06006F5A RID: 28506 RVA: 0x0023E76C File Offset: 0x0023C96C
	private IEnumerator ReticleFadeOut()
	{
		this.OnSpellFinished();
		Action<object> action = delegate(object amount)
		{
			if (this.m_ReticleInstance != null)
			{
				this.m_ReticleInstance.GetComponent<RenderToTexture>().GetRenderMaterial().SetFloat("_Alpha", (float)amount);
			}
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_ReticleFadeOutTime,
			"from",
			1f,
			"to",
			0f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		yield return new WaitForSeconds(this.m_ReticleFadeOutTime);
		UnityEngine.Object.Destroy(this.m_ReticleInstance);
		yield break;
	}

	// Token: 0x06006F5B RID: 28507 RVA: 0x0023E77C File Offset: 0x0023C97C
	private Vector3[] BuildAnimationPath()
	{
		Card[] array = this.FindPossibleTargetCards();
		int num = UnityEngine.Random.Range(this.m_ReticlePathDesiredMinimumTargets, this.m_ReticlePathDesiredMaximumTargets);
		if (num >= array.Length + 2)
		{
			num = array.Length + 2;
		}
		if (array.Length <= 1)
		{
			return new Vector3[]
			{
				this.m_InitialTargetCard.transform.position,
				base.GetTarget().transform.position
			};
		}
		List<Vector3> list = new List<Vector3>();
		list.Add(this.m_InitialTargetCard.transform.position);
		GameObject gameObject = this.m_InitialTargetCard.gameObject;
		for (int i = 1; i < num; i++)
		{
			GameObject gameObject2 = array[UnityEngine.Random.Range(0, array.Length - 1)].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2 = array[UnityEngine.Random.Range(0, array.Length - 1)].gameObject;
				if (gameObject2 == gameObject)
				{
					if (gameObject2 == array[array.Length - 1])
					{
						gameObject2 = array[0].gameObject;
					}
					else
					{
						gameObject2 = array[array.Length - 1].gameObject;
					}
				}
			}
			if (i == num - 1 && gameObject2 == base.GetTarget() && gameObject2 == gameObject)
			{
				if (gameObject2 == array[array.Length - 1])
				{
					gameObject2 = array[0].gameObject;
				}
				else
				{
					gameObject2 = array[array.Length - 1].gameObject;
				}
			}
			list.Add(gameObject2.transform.position);
		}
		list.Add(base.GetTarget().transform.position);
		return list.ToArray();
	}

	// Token: 0x06006F5C RID: 28508 RVA: 0x0023E908 File Offset: 0x0023CB08
	private Card[] FindPossibleTargetCards()
	{
		List<Card> list = new List<Card>();
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			return list.ToArray();
		}
		foreach (ZonePlay zonePlay in zoneMgr.FindZonesOfType<ZonePlay>())
		{
			foreach (Card card in zonePlay.GetCards())
			{
				if (!(card == this.m_AttackingEntityCard) && (!(card == this.m_InitialTargetCard) || this.m_AllowTargetingInitialTarget))
				{
					list.Add(card);
				}
			}
		}
		foreach (ZoneHero zoneHero in zoneMgr.FindZonesOfType<ZoneHero>())
		{
			foreach (Card card2 in zoneHero.GetCards())
			{
				if (!(card2 == this.m_AttackingEntityCard) && (!(card2 == this.m_InitialTargetCard) || this.m_AllowTargetingInitialTarget))
				{
					list.Add(card2);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06006F5D RID: 28509 RVA: 0x0023EA84 File Offset: 0x0023CC84
	private Card[] GetOpponentZoneMinions()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in GameState.Get().GetFirstOpponentPlayer(base.GetSourceCard().GetController()).GetBattlefieldZone().GetCards())
		{
			if (!(card == this.m_AttackingEntityCard))
			{
				list.Add(card);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06006F5E RID: 28510 RVA: 0x0023EB0C File Offset: 0x0023CD0C
	private Card GetCurrentPlayerHeroCard()
	{
		return base.GetSourceCard().GetController().GetHeroCard();
	}

	// Token: 0x06006F5F RID: 28511 RVA: 0x0023EB1E File Offset: 0x0023CD1E
	private Card GetOpponentHeroCard()
	{
		return GameState.Get().GetFirstOpponentPlayer(base.GetSourceCard().GetController()).GetHeroCard();
	}

	// Token: 0x04005944 RID: 22852
	public float m_ReticleFadeInTime = 0.8f;

	// Token: 0x04005945 RID: 22853
	public float m_ReticleFadeOutTime = 0.4f;

	// Token: 0x04005946 RID: 22854
	public float m_ReticlePathTime = 3f;

	// Token: 0x04005947 RID: 22855
	public float m_ReticleBlur = 0.005f;

	// Token: 0x04005948 RID: 22856
	public float m_ReticleBlurFocusTime = 0.8f;

	// Token: 0x04005949 RID: 22857
	public Color m_ReticleAttackColor = Color.red;

	// Token: 0x0400594A RID: 22858
	public float m_ReticleAttackScale = 1.1f;

	// Token: 0x0400594B RID: 22859
	public float m_ReticleAttackTime = 0.3f;

	// Token: 0x0400594C RID: 22860
	public Vector3 m_ReticleAttackRotate = new Vector3(0f, 90f, 0f);

	// Token: 0x0400594D RID: 22861
	public float m_ReticleAttackHold = 0.25f;

	// Token: 0x0400594E RID: 22862
	public GameObject m_Reticle;

	// Token: 0x0400594F RID: 22863
	public bool m_AllowTargetingInitialTarget;

	// Token: 0x04005950 RID: 22864
	public int m_ReticlePathDesiredMinimumTargets = 3;

	// Token: 0x04005951 RID: 22865
	public int m_ReticlePathDesiredMaximumTargets = 4;

	// Token: 0x04005952 RID: 22866
	private GameObject m_ReticleInstance;

	// Token: 0x04005953 RID: 22867
	private Card m_AttackingEntityCard;

	// Token: 0x04005954 RID: 22868
	private Card m_InitialTargetCard;

	// Token: 0x04005955 RID: 22869
	private Color m_OrgAmbient;

	// Token: 0x020023BD RID: 9149
	private enum TargetingMetadata
	{
		// Token: 0x0400E7E6 RID: 59366
		DestinationTarget,
		// Token: 0x0400E7E7 RID: 59367
		AttackingEntity,
		// Token: 0x0400E7E8 RID: 59368
		InitialTarget
	}
}
