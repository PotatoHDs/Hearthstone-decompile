using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone.Extensions;
using UnityEngine;

[CustomEditClass]
public class CardDef : MonoBehaviour
{
	[CustomEditField(Sections = "Portrait", T = EditType.ARTBUNDLE)]
	public Portrait m_Portrait;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_PortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL, HidePredicate = "HideIfPortrait")]
	public string m_PremiumPortraitMaterialPath;

	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION, HidePredicate = "HideIfPortrait")]
	public string m_PremiumUberShaderAnimationPath;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_PremiumPortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.MESH, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPlaneRTT_Hand;

	[CustomEditField(Sections = "Portrait", T = EditType.MESH, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPlaneRTT_Play;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Color m_DiamondPlaneRTT_CearColor = Color.clear;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.GAME_OBJECT, HidePredicate = "HideIfPortrait")]
	public string m_DiamondModel;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_DeckCardBarPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_EnchantmentPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_HistoryTileHalfPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_HistoryTileFullPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_LeaderboardTileFullPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material_MobileOverride m_CustomDeckPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material_MobileOverride m_DeckPickerPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_LockedClassPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_PracticeAIPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_DeckBoxPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public bool m_AlwaysRenderPremiumPortrait;

	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public CardSilhouetteOverride m_CardSilhouetteOverride;

	[CustomEditField(Sections = "Play")]
	public CardEffectDef m_PlayEffectDef;

	[CustomEditField(Sections = "Play")]
	public List<CardEffectDef> m_AdditionalPlayEffectDefs;

	[CustomEditField(Sections = "Attack")]
	public CardEffectDef m_AttackEffectDef;

	[CustomEditField(Sections = "Death")]
	public CardEffectDef m_DeathEffectDef;

	[CustomEditField(Sections = "Lifetime")]
	public CardEffectDef m_LifetimeEffectDef;

	[CustomEditField(Sections = "Trigger")]
	public List<CardEffectDef> m_TriggerEffectDefs;

	[CustomEditField(Sections = "SubOption")]
	public List<CardEffectDef> m_SubOptionEffectDefs;

	[CustomEditField(Sections = "SubOption")]
	public List<List<CardEffectDef>> m_AdditionalSubOptionEffectDefs;

	[CustomEditField(Sections = "ResetGame")]
	public List<CardEffectDef> m_ResetGameEffectDefs;

	[CustomEditField(Sections = "Sub-Spells")]
	public List<CardEffectDef> m_SubSpellEffectDefs;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomSummonSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomSummonSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomSummonSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomSpawnSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomSpawnSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomSpawnSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomDeathSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomDeathSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomDeathSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomDiscardSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomDiscardSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomDiscardSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomKeywordSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomChoiceRevealSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomChoiceConcealSpellPath;

	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public List<SpellTableOverride> m_SpellTableOverrides;

	[CustomEditField(Sections = "Hero", T = EditType.GAME_OBJECT)]
	public string m_CollectionHeroDefPath;

	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_CustomHeroArmorSpell;

	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectFriendly;

	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectOpponent;

	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectFriendlyPhone;

	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectOpponentPhone;

	[CustomEditField(Sections = "Hero")]
	public bool m_SocketInOverrideHeroAnimation;

	[CustomEditField(Sections = "Hero")]
	public bool m_SocketInParentEffectToHero = true;

	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroTray;

	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroTrayGolden;

	[CustomEditField(Sections = "Hero")]
	public List<Board.CustomTraySettings> m_CustomHeroTraySettings;

	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroPhoneTray;

	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroPhoneManaGem;

	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLinePath;

	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLineBeforeVersusPath;

	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLineAfterVersusPath;

	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_HeroPickerSelectedPrefab;

	[CustomEditField(Sections = "Hero")]
	public List<EmoteEntryDef> m_EmoteDefs;

	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressDeathrattleDeath;

	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressPlaySoundsOnSummon;

	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressPlaySoundsDuringMulligan;

	[CustomEditField(Sections = "Special Events")]
	public List<CardDefSpecialEvent> m_SpecialEvents;

	private static MaterialManagerService s_MaterialManagerService;

	private Material m_LoadedPremiumPortraitMaterial;

	private Material m_LoadedPremiumClassMaterial;

	private Material m_LoadedDeckCardBarPortrait;

	private Material m_LoadedEnchantmentPortrait;

	private Material m_LoadedHistoryTileFullPortrait;

	private Material m_LoadedHistoryTileHalfPortrait;

	private Material m_LoadedLeaderboardTileFullPortrait;

	private Material m_LoadedCustomDeckPortrait;

	private Material m_LoadedDeckPickerPortrait;

	private Material m_LoadedPracticeAIPortrait;

	private Material m_LoadedDeckBoxPortrait;

	private CardPortraitQuality m_portraitQuality = CardPortraitQuality.GetUnloaded();

	private CardDefSpecialEvent m_currentSpecialEvent;

	private AssetHandle<Texture> m_LoadedPortraitTexture;

	private AssetHandle<Texture> m_loadedPremiumPortraitTexture;

	private AssetHandle<Material> m_premiumMaterialHandle;

	private AssetHandle<UberShaderAnimation> m_premiumPortraitAnimation;

	private AssetHandle<Texture> m_lowQualityPortrait;

	protected const int LARGE_MINION_COST = 7;

	protected const int MEDIUM_MINION_COST = 4;

	public string PortraitTexturePath
	{
		get
		{
			if (m_Portrait != null)
			{
				return m_Portrait.m_PortraitTexturePath;
			}
			return m_PortraitTexturePath;
		}
	}

	public string GoldenPortraitMaterialPath
	{
		get
		{
			if (m_Portrait != null)
			{
				return m_Portrait.m_PremiumPortraitMaterialPath;
			}
			return m_PremiumPortraitMaterialPath;
		}
	}

	public string GoldenUberShaderAnimationPath
	{
		get
		{
			if (m_Portrait != null)
			{
				return m_Portrait.m_PremiumUberShaderAnimationPath;
			}
			return m_PremiumUberShaderAnimationPath;
		}
	}

	public bool HideIfPortrait()
	{
		return m_Portrait != null;
	}

	public void Awake()
	{
		if (string.IsNullOrEmpty(m_PortraitTexturePath))
		{
			m_portraitQuality.TextureQuality = 3;
			m_portraitQuality.LoadPremium = true;
		}
		else if (string.IsNullOrEmpty(m_PremiumPortraitMaterialPath))
		{
			m_portraitQuality.LoadPremium = true;
		}
	}

	public virtual string DetermineActorPathForZone(Entity entity, TAG_ZONE zoneTag)
	{
		return ActorNames.GetZoneActor(entity, zoneTag);
	}

	public void OnDestroy()
	{
		if (m_Portrait != null)
		{
			Object.Destroy(m_Portrait);
		}
		if ((bool)m_LoadedPremiumPortraitMaterial)
		{
			Object.Destroy(m_LoadedPremiumPortraitMaterial);
		}
		if ((bool)m_LoadedPremiumClassMaterial)
		{
			Object.Destroy(m_LoadedPremiumClassMaterial);
		}
		if ((bool)m_LoadedDeckCardBarPortrait)
		{
			Object.Destroy(m_LoadedDeckCardBarPortrait);
		}
		if ((bool)m_LoadedEnchantmentPortrait)
		{
			Object.Destroy(m_LoadedEnchantmentPortrait);
		}
		if ((bool)m_LoadedHistoryTileFullPortrait)
		{
			Object.Destroy(m_LoadedHistoryTileFullPortrait);
		}
		if ((bool)m_LoadedHistoryTileHalfPortrait)
		{
			Object.Destroy(m_LoadedHistoryTileHalfPortrait);
		}
		if ((bool)m_LoadedLeaderboardTileFullPortrait)
		{
			Object.Destroy(m_LoadedLeaderboardTileFullPortrait);
		}
		if ((bool)m_LoadedCustomDeckPortrait)
		{
			Object.Destroy(m_LoadedCustomDeckPortrait);
		}
		if ((bool)m_LoadedDeckPickerPortrait)
		{
			Object.Destroy(m_LoadedDeckPickerPortrait);
		}
		if ((bool)m_LoadedPracticeAIPortrait)
		{
			Object.Destroy(m_LoadedPracticeAIPortrait);
		}
		if ((bool)m_LoadedDeckBoxPortrait)
		{
			Object.Destroy(m_LoadedDeckBoxPortrait);
		}
		AssetHandle.SafeDispose(ref m_LoadedPortraitTexture);
		AssetHandle.SafeDispose(ref m_premiumPortraitAnimation);
		AssetHandle.SafeDispose(ref m_premiumMaterialHandle);
		AssetHandle.SafeDispose(ref m_loadedPremiumPortraitTexture);
		AssetHandle.SafeDispose(ref m_lowQualityPortrait);
	}

	public virtual SpellType DetermineSummonInSpell_HandToPlay(Card card, bool useFastAnimations)
	{
		Entity entity = card.GetEntity();
		if (entity.IsHero())
		{
			return SpellType.SUMMON_IN_HERO;
		}
		int cost = entity.GetEntityDef().GetCost();
		TAG_PREMIUM premiumType = entity.GetPremiumType();
		bool flag = entity.GetController().IsFriendlySide();
		if (useFastAnimations)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_DIAMOND_FAST;
				}
				return SpellType.SUMMON_IN_OPPONENT_FAST;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_PREMIUM_FAST;
				}
				return SpellType.SUMMON_IN_OPPONENT_FAST;
			case TAG_PREMIUM.NORMAL:
				break;
			default:
				Debug.LogWarning($"CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {premiumType}");
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_FAST;
			}
			return SpellType.SUMMON_IN_OPPONENT_FAST;
		}
		if (cost >= 7)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_LARGE_DIAMOND;
				}
				return SpellType.SUMMON_IN_OPPONENT_LARGE_DIAMOND;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_LARGE_PREMIUM;
				}
				return SpellType.SUMMON_IN_OPPONENT_LARGE_PREMIUM;
			case TAG_PREMIUM.NORMAL:
				break;
			default:
				Debug.LogWarning($"CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {premiumType}");
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_LARGE;
			}
			return SpellType.SUMMON_IN_OPPONENT_LARGE;
		}
		if (cost >= 4)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_MEDIUM_DIAMOND;
				}
				return SpellType.SUMMON_IN_OPPONENT_MEDIUM_DIAMOND;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_MEDIUM_PREMIUM;
				}
				return SpellType.SUMMON_IN_OPPONENT_MEDIUM_PREMIUM;
			case TAG_PREMIUM.NORMAL:
				break;
			default:
				Debug.LogWarning($"CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {premiumType}");
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_MEDIUM;
			}
			return SpellType.SUMMON_IN_OPPONENT_MEDIUM;
		}
		switch (premiumType)
		{
		case TAG_PREMIUM.DIAMOND:
			if (flag)
			{
				return SpellType.SUMMON_IN_DIAMOND;
			}
			return SpellType.SUMMON_IN_OPPONENT_DIAMOND;
		case TAG_PREMIUM.GOLDEN:
			if (flag)
			{
				return SpellType.SUMMON_IN_PREMIUM;
			}
			return SpellType.SUMMON_IN_OPPONENT_PREMIUM;
		case TAG_PREMIUM.NORMAL:
			break;
		default:
			Debug.LogWarning($"CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {premiumType}");
			break;
		}
		if (flag)
		{
			return SpellType.SUMMON_IN;
		}
		return SpellType.SUMMON_IN_OPPONENT;
	}

	public virtual SpellType DetermineSummonOutSpell_HandToPlay(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity.IsHero())
		{
			return SpellType.SUMMON_OUT_HERO;
		}
		if (!entity.GetController().IsFriendlySide())
		{
			return SpellType.SUMMON_OUT;
		}
		int cost = entity.GetEntityDef().GetCost();
		TAG_PREMIUM premiumType = entity.GetPremiumType();
		if (card.GetActor() != null && card.GetActor().UseTechLevelManaGem())
		{
			if (premiumType != 0)
			{
				if (premiumType == TAG_PREMIUM.GOLDEN)
				{
					return SpellType.SUMMON_OUT_TECH_LEVEL_PREMIUM;
				}
				Debug.LogWarning($"CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {premiumType}");
			}
			return SpellType.SUMMON_OUT_TECH_LEVEL;
		}
		if (cost >= 7)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.DIAMOND:
				return SpellType.SUMMON_OUT_DIAMOND;
			case TAG_PREMIUM.GOLDEN:
				return SpellType.SUMMON_OUT_PREMIUM;
			case TAG_PREMIUM.NORMAL:
				break;
			default:
				Debug.LogWarning($"CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {premiumType}");
				break;
			}
			return SpellType.SUMMON_OUT_LARGE;
		}
		if (cost >= 4)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.DIAMOND:
				return SpellType.SUMMON_OUT_DIAMOND;
			case TAG_PREMIUM.GOLDEN:
				return SpellType.SUMMON_OUT_PREMIUM;
			case TAG_PREMIUM.NORMAL:
				break;
			default:
				Debug.LogWarning($"CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {premiumType}");
				break;
			}
			return SpellType.SUMMON_OUT_MEDIUM;
		}
		switch (premiumType)
		{
		case TAG_PREMIUM.DIAMOND:
			return SpellType.SUMMON_OUT_DIAMOND;
		case TAG_PREMIUM.GOLDEN:
			return SpellType.SUMMON_OUT_PREMIUM;
		case TAG_PREMIUM.NORMAL:
			break;
		default:
			Debug.LogWarning($"CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {premiumType}");
			break;
		}
		return SpellType.SUMMON_OUT;
	}

	private static void SetTextureIfNotNull(Material baseMat, ref Material targetMat, Texture tex)
	{
		if (!(baseMat == null))
		{
			if (targetMat == null)
			{
				targetMat = Object.Instantiate(baseMat);
				GetMaterialManagerService()?.IgnoreMaterial(targetMat);
			}
			targetMat.mainTexture = tex;
		}
	}

	private static MaterialManagerService GetMaterialManagerService()
	{
		if (s_MaterialManagerService == null)
		{
			s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return s_MaterialManagerService;
	}

	public void OnPortraitLoaded(AssetHandle<Texture> portrait, int quality)
	{
		if (m_Portrait != null)
		{
			m_Portrait.OnPortraitLoaded(portrait, quality);
			return;
		}
		if (quality <= m_portraitQuality.TextureQuality)
		{
			Debug.LogWarning($"Loaded texture of quality lower or equal to what was was already available ({quality} <= {m_portraitQuality}), texture={portrait}");
			return;
		}
		m_portraitQuality.TextureQuality = quality;
		if ((bool)m_LoadedPortraitTexture)
		{
			AssetHandle.Set(ref m_lowQualityPortrait, m_LoadedPortraitTexture);
		}
		AssetHandle.Set(ref m_LoadedPortraitTexture, portrait);
		if (m_LoadedPremiumPortraitMaterial != null && string.IsNullOrEmpty(m_PremiumPortraitTexturePath))
		{
			m_LoadedPremiumPortraitMaterial.mainTexture = m_LoadedPortraitTexture;
			m_portraitQuality.LoadPremium = true;
		}
		if (m_LoadedPremiumClassMaterial != null && string.IsNullOrEmpty(m_PremiumPortraitTexturePath))
		{
			m_LoadedPremiumClassMaterial.mainTexture = m_LoadedPortraitTexture;
		}
		SetTextureIfNotNull(m_DeckCardBarPortrait, ref m_LoadedDeckCardBarPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_EnchantmentPortrait, ref m_LoadedEnchantmentPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_HistoryTileFullPortrait, ref m_LoadedHistoryTileFullPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_HistoryTileHalfPortrait, ref m_LoadedHistoryTileHalfPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_LeaderboardTileFullPortrait, ref m_LoadedLeaderboardTileFullPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_CustomDeckPortrait, ref m_LoadedCustomDeckPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_DeckPickerPortrait, ref m_LoadedDeckPickerPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_PracticeAIPortrait, ref m_LoadedPracticeAIPortrait, m_LoadedPortraitTexture);
		SetTextureIfNotNull(m_DeckBoxPortrait, ref m_LoadedDeckBoxPortrait, m_LoadedPortraitTexture);
	}

	public void OnPremiumMaterialLoaded(AssetHandle<Material> material, AssetHandle<Texture> portrait, AssetHandle<UberShaderAnimation> portraitAnimation)
	{
		if (m_Portrait != null)
		{
			m_Portrait.OnPremiumMaterialLoaded(material, portrait, portraitAnimation);
			return;
		}
		if (m_LoadedPremiumPortraitMaterial != null)
		{
			if (Application.isPlaying)
			{
				Debug.LogWarning($"Loaded premium material twice: {material}");
			}
			return;
		}
		if ((bool)material)
		{
			AssetHandle.Set(ref m_premiumMaterialHandle, material);
			m_LoadedPremiumPortraitMaterial = (Material)Object.Instantiate((Object)(Material)material);
			m_LoadedPremiumClassMaterial = (Material)Object.Instantiate((Object)(Material)material);
			MaterialManagerService materialManagerService = GetMaterialManagerService();
			if (materialManagerService != null)
			{
				materialManagerService.IgnoreMaterial(m_LoadedPremiumPortraitMaterial);
				materialManagerService.IgnoreMaterial(m_LoadedPremiumClassMaterial);
			}
		}
		AssetHandle.Set(ref m_premiumPortraitAnimation, portraitAnimation);
		if ((bool)m_LoadedPortraitTexture)
		{
			if (m_LoadedPremiumPortraitMaterial != null)
			{
				m_LoadedPremiumPortraitMaterial.mainTexture = m_LoadedPortraitTexture;
			}
			if (m_LoadedPremiumClassMaterial != null)
			{
				m_LoadedPremiumClassMaterial.mainTexture = m_LoadedPortraitTexture;
			}
			m_portraitQuality.LoadPremium = true;
		}
		if ((bool)portrait)
		{
			AssetHandle.Set(ref m_loadedPremiumPortraitTexture, portrait);
			if (m_LoadedPremiumPortraitMaterial != null)
			{
				m_LoadedPremiumPortraitMaterial.mainTexture = portrait;
			}
			if (m_LoadedPremiumClassMaterial != null)
			{
				m_LoadedPremiumClassMaterial.mainTexture = portrait;
			}
			m_portraitQuality.LoadPremium = true;
		}
	}

	public CardPortraitQuality GetPortraitQuality()
	{
		return m_portraitQuality;
	}

	public Texture GetPortraitTexture()
	{
		return GetPortraitTextureHandle();
	}

	public AssetHandle<Texture> GetPortraitTextureHandle()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.LoadedPortraitTexture;
		}
		return m_LoadedPortraitTexture;
	}

	public bool IsPremiumLoaded()
	{
		return m_portraitQuality.LoadPremium;
	}

	public Material GetPremiumPortraitMaterial()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedPremiumPortraitMaterial;
		}
		return m_LoadedPremiumPortraitMaterial;
	}

	public UberShaderAnimation GetPremiumPortraitAnimation()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.PremiumPortraitAnimation;
		}
		return m_premiumPortraitAnimation;
	}

	public Material GetDeckCardBarPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedDeckCardBarPortrait;
		}
		return m_LoadedDeckCardBarPortrait;
	}

	public Material GetEnchantmentPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedEnchantmentPortrait;
		}
		return m_LoadedEnchantmentPortrait;
	}

	public Material GetHistoryTileFullPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedHistoryTileFullPortrait;
		}
		return m_LoadedHistoryTileFullPortrait;
	}

	public Material GetHistoryTileHalfPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedHistoryTileHalfPortrait;
		}
		return m_LoadedHistoryTileHalfPortrait;
	}

	public Material GetLeaderboardTileFullPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedLeaderboardTileFullPortrait;
		}
		return m_LoadedLeaderboardTileFullPortrait;
	}

	public Material GetCustomDeckPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedCustomDeckPortrait;
		}
		return m_LoadedCustomDeckPortrait;
	}

	public Material GetDeckPickerPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedDeckPickerPortrait;
		}
		return m_LoadedDeckPickerPortrait;
	}

	public Material GetPracticeAIPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedPracticeAIPortrait;
		}
		return m_LoadedPracticeAIPortrait;
	}

	public Material GetDeckBoxPortrait()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedDeckBoxPortrait;
		}
		return m_LoadedDeckBoxPortrait;
	}

	public AssetReference GetPortraitRef()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.GetPortraitRef();
		}
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PortraitTextureOverride))
		{
			return m_currentSpecialEvent.m_PortraitTextureOverride;
		}
		return m_PortraitTexturePath;
	}

	public AssetReference GetPremiumMaterialRef()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.GetPremiumMaterialRef();
		}
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumPortraitMaterialOverride))
		{
			return m_currentSpecialEvent.m_PremiumPortraitMaterialOverride;
		}
		return m_PremiumPortraitMaterialPath;
	}

	public AssetReference GetPremiumPortraitRef()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.GetPremiumPortraitRef();
		}
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumPortraitTextureOverride))
		{
			return m_currentSpecialEvent.m_PremiumPortraitTextureOverride;
		}
		return m_PremiumPortraitTexturePath;
	}

	public AssetReference GetPremiumAnimationRef()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.GetPremiumAnimationRef();
		}
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride))
		{
			return m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride;
		}
		return m_PremiumUberShaderAnimationPath;
	}

	public Material GetPremiumClassMaterial()
	{
		if (m_Portrait != null)
		{
			return m_Portrait.m_LoadedPremiumClassMaterial;
		}
		return m_LoadedPremiumClassMaterial;
	}

	public void UpdateSpecialEvent()
	{
		m_currentSpecialEvent = CardDefSpecialEvent.FindActiveEvent(this);
	}
}
