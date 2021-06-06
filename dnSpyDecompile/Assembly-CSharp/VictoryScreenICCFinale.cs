using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class VictoryScreenICCFinale : VictoryScreen
{
	// Token: 0x06002613 RID: 9747 RVA: 0x000BF368 File Offset: 0x000BD568
	protected override void Awake()
	{
		base.Awake();
		Card card = GameState.Get().GetFriendlySidePlayer().GetStartingHero().GetCard();
		Entity entity = card.GetEntity();
		TAG_CLASS @class = card.GetEntity().GetClass();
		string text;
		this.m_heroMap.TryGetValue(@class, out text);
		if (!string.IsNullOrEmpty(text))
		{
			if (text == entity.GetCardId())
			{
				this.m_LichPortraitRenderer.GetMaterial().SetTexture(this.m_PortraitTextureName, card.GetPortraitTexture());
			}
			else
			{
				DefLoader.DisposableCardDef heroSkinCardDef = this.m_heroSkinCardDef;
				if (heroSkinCardDef != null)
				{
					heroSkinCardDef.Dispose();
				}
				this.m_heroSkinCardDef = DefLoader.Get().GetCardDef(text, null);
				DefLoader.DisposableCardDef heroSkinCardDef2 = this.m_heroSkinCardDef;
				if (((heroSkinCardDef2 != null) ? heroSkinCardDef2.CardDef : null) != null)
				{
					this.m_LichPortraitRenderer.GetMaterial().SetTexture(this.m_PortraitTextureName, this.m_heroSkinCardDef.CardDef.GetPortraitTexture());
				}
			}
		}
		VictoryTwoScoop victoryTwoScoop = this.m_twoScoop as VictoryTwoScoop;
		if (victoryTwoScoop == null)
		{
			Log.Gameplay.PrintError("VictoryScreenICCPrologue.Awake() - m_twoScoop is not an instance of VictoryTwoScoop!", Array.Empty<object>());
		}
		string text2;
		this.m_dkHeroMap.TryGetValue(@class, out text2);
		if (!string.IsNullOrEmpty(text2))
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(text2);
			if (entityDef != null)
			{
				this.m_HeroNameText.Text = entityDef.GetName();
				victoryTwoScoop.SetOverrideHero(entityDef);
			}
		}
		string text3;
		this.m_heroDesriptions.TryGetValue(@class, out text3);
		if (!string.IsNullOrEmpty(text3))
		{
			this.m_HeroDescriptionText.Text = GameStrings.Get(text3);
		}
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x000BF4E6 File Offset: 0x000BD6E6
	protected override void OnDestroy()
	{
		DefLoader.DisposableCardDef heroSkinCardDef = this.m_heroSkinCardDef;
		if (heroSkinCardDef != null)
		{
			heroSkinCardDef.Dispose();
		}
		this.m_heroSkinCardDef = null;
		base.OnDestroy();
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000BF31B File Offset: 0x000BD51B
	protected override void ShowStandardFlow()
	{
		base.ShowTwoScoop();
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x000BF506 File Offset: 0x000BD706
	protected override void OnTwoScoopShown()
	{
		base.OnTwoScoopShown();
		base.StartCoroutine(this.PlayAnim());
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x000BF51B File Offset: 0x000BD71B
	private IEnumerator PlayAnim()
	{
		yield return new WaitForSeconds(VictoryScreenICCFinale.TIRION_VO_DELAY);
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor().GetSpell(SpellType.ENDGAME_WIN).ActivateState(SpellStateType.DEATH);
		VictoryTwoScoop victoryTwoScoop = this.m_twoScoop as VictoryTwoScoop;
		if (victoryTwoScoop != null)
		{
			victoryTwoScoop.StopFireworksAudio();
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCFinale.PlayAnim(): m_twoScoop is not of type VictoryTwoScoop!", Array.Empty<object>());
		}
		ICC_08_Finale missionEntity = GameState.Get().GetGameEntity() as ICC_08_Finale;
		if (missionEntity != null)
		{
			yield return base.StartCoroutine(missionEntity.PlayTirionVictoryVO());
			Coroutine enumerator = base.StartCoroutine(missionEntity.PlayFriendlyHeroVictoryVO(this.m_twoScoop.m_heroActor, this.m_PreLichTransformationAudio, this.m_MidLichTransformationAudio));
			yield return new WaitForSeconds(1f);
			this.m_BurnAwayAnimation["LichHeroBurnAway"].speed = 0.4f;
			this.m_BurnAwayAnimation.Play("LichHeroBurnAway");
			yield return enumerator;
			enumerator = null;
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCEpilogue.PlayAnim(): GameEntity is not an instance of ICC_08_Finale!.", Array.Empty<object>());
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_continueText.gameObject.SetActive(true);
		}
		this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_HideTwoScoop));
		while (!this.m_dismissedTwoScoops)
		{
			yield return null;
		}
		this.m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_HideTwoScoop));
		this.m_hitbox.gameObject.SetActive(false);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_continueText.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(VictoryScreenICCFinale.POSTCARD_DELAY);
		if (this.m_HeroPostcard != null)
		{
			if (this.m_HeroPostcardRenderer != null)
			{
				Material postcardMaterialForClass = this.GetPostcardMaterialForClass(GameState.Get().GetFriendlySidePlayer().GetStartingHero().GetClass());
				this.m_HeroPostcardRenderer.SetMaterial(postcardMaterialForClass);
			}
			this.m_HeroPostcard.SetActive(true);
			foreach (AudioSource audioSource in this.m_HeroPostcardShowAudio)
			{
				if (audioSource != null)
				{
					SoundManager.Get().Play(audioSource, null, null, null);
				}
			}
			Hashtable args = iTween.Hash(new object[]
			{
				"scale",
				new Vector3(0.01f, 0.01f, 0.01f),
				"time",
				0.5f,
				"easetype",
				iTween.EaseType.easeOutBounce
			});
			iTween.ScaleFrom(this.m_HeroPostcard, args);
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCFinale.PlayAnim(): m_HeroPostcard is null!", Array.Empty<object>());
		}
		this.m_ContinueButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_DismissPostcard));
		yield break;
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x000BF52A File Offset: 0x000BD72A
	private void ContinueButtonPress_HideTwoScoop(UIEvent e)
	{
		if (this.m_HideTwoScoopsAudio != null)
		{
			SoundManager.Get().Play(this.m_HideTwoScoopsAudio, null, null, null);
		}
		this.m_twoScoop.Hide();
		this.m_dismissedTwoScoops = true;
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x000BF560 File Offset: 0x000BD760
	private void ContinueButtonPress_DismissPostcard(UIEvent e)
	{
		this.m_ContinueButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_DismissPostcard));
		base.StartCoroutine(this.DismissPostcard());
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x000BF588 File Offset: 0x000BD788
	private IEnumerator DismissPostcard()
	{
		if (this.m_HeroPostcardHideAudio != null)
		{
			SoundManager.Get().Play(this.m_HeroPostcardHideAudio, null, null, null);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			0.25f,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.ScaleTo(this.m_HeroPostcard, args);
		while (iTween.HasTween(this.m_HeroPostcard))
		{
			yield return null;
		}
		this.m_HeroPostcard.SetActive(false);
		base.ContinueEvents();
		yield break;
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x000BF598 File Offset: 0x000BD798
	private Material GetPostcardMaterialForClass(TAG_CLASS classType)
	{
		switch (classType)
		{
		case TAG_CLASS.DRUID:
			return this.m_DruidPostcardMaterial;
		case TAG_CLASS.HUNTER:
			return this.m_HunterPostcardMaterial;
		case TAG_CLASS.MAGE:
			return this.m_MagePostcardMaterial;
		case TAG_CLASS.PALADIN:
			return this.m_PaladinPostcardMaterial;
		case TAG_CLASS.PRIEST:
			return this.m_PriestPostcardMaterial;
		case TAG_CLASS.ROGUE:
			return this.m_RoguePostcardMaterial;
		case TAG_CLASS.SHAMAN:
			return this.m_ShamanPostcardMaterial;
		case TAG_CLASS.WARLOCK:
			return this.m_WarlockPostcardMaterial;
		case TAG_CLASS.WARRIOR:
			return this.m_WarriorPostcardMaterial;
		default:
			return this.m_MagePostcardMaterial;
		}
	}

	// Token: 0x0400155D RID: 5469
	public Animation m_BurnAwayAnimation;

	// Token: 0x0400155E RID: 5470
	public Renderer m_LichPortraitRenderer;

	// Token: 0x0400155F RID: 5471
	public string m_PortraitTextureName;

	// Token: 0x04001560 RID: 5472
	public AudioSource m_PreLichTransformationAudio;

	// Token: 0x04001561 RID: 5473
	public AudioSource m_MidLichTransformationAudio;

	// Token: 0x04001562 RID: 5474
	public AudioSource m_HideTwoScoopsAudio;

	// Token: 0x04001563 RID: 5475
	public List<AudioSource> m_HeroPostcardShowAudio;

	// Token: 0x04001564 RID: 5476
	public AudioSource m_HeroPostcardHideAudio;

	// Token: 0x04001565 RID: 5477
	public UberText m_HeroNameText;

	// Token: 0x04001566 RID: 5478
	public UberText m_HeroDescriptionText;

	// Token: 0x04001567 RID: 5479
	public UIBButton m_ContinueButton;

	// Token: 0x04001568 RID: 5480
	public GameObject m_HeroPostcard;

	// Token: 0x04001569 RID: 5481
	public MeshRenderer m_HeroPostcardRenderer;

	// Token: 0x0400156A RID: 5482
	public Material m_DruidPostcardMaterial;

	// Token: 0x0400156B RID: 5483
	public Material m_HunterPostcardMaterial;

	// Token: 0x0400156C RID: 5484
	public Material m_MagePostcardMaterial;

	// Token: 0x0400156D RID: 5485
	public Material m_PaladinPostcardMaterial;

	// Token: 0x0400156E RID: 5486
	public Material m_PriestPostcardMaterial;

	// Token: 0x0400156F RID: 5487
	public Material m_RoguePostcardMaterial;

	// Token: 0x04001570 RID: 5488
	public Material m_ShamanPostcardMaterial;

	// Token: 0x04001571 RID: 5489
	public Material m_WarlockPostcardMaterial;

	// Token: 0x04001572 RID: 5490
	public Material m_WarriorPostcardMaterial;

	// Token: 0x04001573 RID: 5491
	private Map<TAG_CLASS, string> m_heroDesriptions = new Map<TAG_CLASS, string>
	{
		{
			TAG_CLASS.DRUID,
			"ICCDruidVictory_01"
		},
		{
			TAG_CLASS.HUNTER,
			"ICCHunterVictory_01"
		},
		{
			TAG_CLASS.MAGE,
			"ICCMageVictory_01"
		},
		{
			TAG_CLASS.PALADIN,
			"ICCPaladinVictory_01"
		},
		{
			TAG_CLASS.PRIEST,
			"ICCPriestVictory_01"
		},
		{
			TAG_CLASS.ROGUE,
			"ICCRogueVictory_01"
		},
		{
			TAG_CLASS.SHAMAN,
			"ICCShamanVictory_01"
		},
		{
			TAG_CLASS.WARLOCK,
			"ICCWarlockVictory_01"
		},
		{
			TAG_CLASS.WARRIOR,
			"ICCWarriorVictory_01"
		}
	};

	// Token: 0x04001574 RID: 5492
	private Map<TAG_CLASS, string> m_heroMap = new Map<TAG_CLASS, string>
	{
		{
			TAG_CLASS.DRUID,
			"HERO_06"
		},
		{
			TAG_CLASS.HUNTER,
			"HERO_05"
		},
		{
			TAG_CLASS.MAGE,
			"HERO_08"
		},
		{
			TAG_CLASS.PALADIN,
			"HERO_04"
		},
		{
			TAG_CLASS.PRIEST,
			"HERO_09"
		},
		{
			TAG_CLASS.ROGUE,
			"HERO_03"
		},
		{
			TAG_CLASS.SHAMAN,
			"HERO_02"
		},
		{
			TAG_CLASS.WARLOCK,
			"HERO_07"
		},
		{
			TAG_CLASS.WARRIOR,
			"HERO_01"
		}
	};

	// Token: 0x04001575 RID: 5493
	private Map<TAG_CLASS, string> m_dkHeroMap = new Map<TAG_CLASS, string>
	{
		{
			TAG_CLASS.DRUID,
			"ICC_832"
		},
		{
			TAG_CLASS.HUNTER,
			"ICC_828"
		},
		{
			TAG_CLASS.MAGE,
			"ICC_833"
		},
		{
			TAG_CLASS.PALADIN,
			"ICC_829"
		},
		{
			TAG_CLASS.PRIEST,
			"ICC_830"
		},
		{
			TAG_CLASS.ROGUE,
			"ICC_827"
		},
		{
			TAG_CLASS.SHAMAN,
			"ICC_481"
		},
		{
			TAG_CLASS.WARLOCK,
			"ICC_831"
		},
		{
			TAG_CLASS.WARRIOR,
			"ICC_834"
		}
	};

	// Token: 0x04001576 RID: 5494
	private bool m_dismissedTwoScoops;

	// Token: 0x04001577 RID: 5495
	private DefLoader.DisposableCardDef m_heroSkinCardDef;

	// Token: 0x04001578 RID: 5496
	private static readonly float TIRION_VO_DELAY = 5f;

	// Token: 0x04001579 RID: 5497
	private static readonly float POSTCARD_DELAY = 2f;
}
