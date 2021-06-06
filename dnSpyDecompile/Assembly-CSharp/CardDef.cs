using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone.Extensions;
using UnityEngine;

// Token: 0x02000870 RID: 2160
[CustomEditClass]
public class CardDef : MonoBehaviour
{
	// Token: 0x060075C0 RID: 30144 RVA: 0x0025CAF8 File Offset: 0x0025ACF8
	public bool HideIfPortrait()
	{
		return this.m_Portrait != null;
	}

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x060075C1 RID: 30145 RVA: 0x0025CB06 File Offset: 0x0025AD06
	public string PortraitTexturePath
	{
		get
		{
			if (this.m_Portrait != null)
			{
				return this.m_Portrait.m_PortraitTexturePath;
			}
			return this.m_PortraitTexturePath;
		}
	}

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x060075C2 RID: 30146 RVA: 0x0025CB28 File Offset: 0x0025AD28
	public string GoldenPortraitMaterialPath
	{
		get
		{
			if (this.m_Portrait != null)
			{
				return this.m_Portrait.m_PremiumPortraitMaterialPath;
			}
			return this.m_PremiumPortraitMaterialPath;
		}
	}

	// Token: 0x170006F2 RID: 1778
	// (get) Token: 0x060075C3 RID: 30147 RVA: 0x0025CB4A File Offset: 0x0025AD4A
	public string GoldenUberShaderAnimationPath
	{
		get
		{
			if (this.m_Portrait != null)
			{
				return this.m_Portrait.m_PremiumUberShaderAnimationPath;
			}
			return this.m_PremiumUberShaderAnimationPath;
		}
	}

	// Token: 0x060075C4 RID: 30148 RVA: 0x0025CB6C File Offset: 0x0025AD6C
	public void Awake()
	{
		if (string.IsNullOrEmpty(this.m_PortraitTexturePath))
		{
			this.m_portraitQuality.TextureQuality = 3;
			this.m_portraitQuality.LoadPremium = true;
			return;
		}
		if (string.IsNullOrEmpty(this.m_PremiumPortraitMaterialPath))
		{
			this.m_portraitQuality.LoadPremium = true;
		}
	}

	// Token: 0x060075C5 RID: 30149 RVA: 0x0025CBB8 File Offset: 0x0025ADB8
	public virtual string DetermineActorPathForZone(Entity entity, TAG_ZONE zoneTag)
	{
		return ActorNames.GetZoneActor(entity, zoneTag);
	}

	// Token: 0x060075C6 RID: 30150 RVA: 0x0025CBC4 File Offset: 0x0025ADC4
	public void OnDestroy()
	{
		if (this.m_Portrait != null)
		{
			UnityEngine.Object.Destroy(this.m_Portrait);
		}
		if (this.m_LoadedPremiumPortraitMaterial)
		{
			UnityEngine.Object.Destroy(this.m_LoadedPremiumPortraitMaterial);
		}
		if (this.m_LoadedPremiumClassMaterial)
		{
			UnityEngine.Object.Destroy(this.m_LoadedPremiumClassMaterial);
		}
		if (this.m_LoadedDeckCardBarPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedDeckCardBarPortrait);
		}
		if (this.m_LoadedEnchantmentPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedEnchantmentPortrait);
		}
		if (this.m_LoadedHistoryTileFullPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedHistoryTileFullPortrait);
		}
		if (this.m_LoadedHistoryTileHalfPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedHistoryTileHalfPortrait);
		}
		if (this.m_LoadedLeaderboardTileFullPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedLeaderboardTileFullPortrait);
		}
		if (this.m_LoadedCustomDeckPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedCustomDeckPortrait);
		}
		if (this.m_LoadedDeckPickerPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedDeckPickerPortrait);
		}
		if (this.m_LoadedPracticeAIPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedPracticeAIPortrait);
		}
		if (this.m_LoadedDeckBoxPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedDeckBoxPortrait);
		}
		AssetHandle.SafeDispose<Texture>(ref this.m_LoadedPortraitTexture);
		AssetHandle.SafeDispose<UberShaderAnimation>(ref this.m_premiumPortraitAnimation);
		AssetHandle.SafeDispose<Material>(ref this.m_premiumMaterialHandle);
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedPremiumPortraitTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_lowQualityPortrait);
	}

	// Token: 0x060075C7 RID: 30151 RVA: 0x0025CD2C File Offset: 0x0025AF2C
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
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_PREMIUM_FAST;
				}
				return SpellType.SUMMON_IN_OPPONENT_FAST;
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_DIAMOND_FAST;
				}
				return SpellType.SUMMON_IN_OPPONENT_FAST;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {0}", premiumType));
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_FAST;
			}
			return SpellType.SUMMON_IN_OPPONENT_FAST;
		}
		else if (cost >= 7)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_LARGE_PREMIUM;
				}
				return SpellType.SUMMON_IN_OPPONENT_LARGE_PREMIUM;
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_LARGE_DIAMOND;
				}
				return SpellType.SUMMON_IN_OPPONENT_LARGE_DIAMOND;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {0}", premiumType));
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_LARGE;
			}
			return SpellType.SUMMON_IN_OPPONENT_LARGE;
		}
		else if (cost >= 4)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_MEDIUM_PREMIUM;
				}
				return SpellType.SUMMON_IN_OPPONENT_MEDIUM_PREMIUM;
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_MEDIUM_DIAMOND;
				}
				return SpellType.SUMMON_IN_OPPONENT_MEDIUM_DIAMOND;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {0}", premiumType));
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN_MEDIUM;
			}
			return SpellType.SUMMON_IN_OPPONENT_MEDIUM;
		}
		else
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				if (flag)
				{
					return SpellType.SUMMON_IN_PREMIUM;
				}
				return SpellType.SUMMON_IN_OPPONENT_PREMIUM;
			case TAG_PREMIUM.DIAMOND:
				if (flag)
				{
					return SpellType.SUMMON_IN_DIAMOND;
				}
				return SpellType.SUMMON_IN_OPPONENT_DIAMOND;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonInSpell_HandToPlay() - unexpected premium type {0}", premiumType));
				break;
			}
			if (flag)
			{
				return SpellType.SUMMON_IN;
			}
			return SpellType.SUMMON_IN_OPPONENT;
		}
	}

	// Token: 0x060075C8 RID: 30152 RVA: 0x0025CEB4 File Offset: 0x0025B0B4
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
			if (premiumType != TAG_PREMIUM.NORMAL)
			{
				if (premiumType == TAG_PREMIUM.GOLDEN)
				{
					return SpellType.SUMMON_OUT_TECH_LEVEL_PREMIUM;
				}
				Debug.LogWarning(string.Format("CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {0}", premiumType));
			}
			return SpellType.SUMMON_OUT_TECH_LEVEL;
		}
		if (cost >= 7)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				return SpellType.SUMMON_OUT_PREMIUM;
			case TAG_PREMIUM.DIAMOND:
				return SpellType.SUMMON_OUT_DIAMOND;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {0}", premiumType));
				break;
			}
			return SpellType.SUMMON_OUT_LARGE;
		}
		if (cost >= 4)
		{
			switch (premiumType)
			{
			case TAG_PREMIUM.NORMAL:
				break;
			case TAG_PREMIUM.GOLDEN:
				return SpellType.SUMMON_OUT_PREMIUM;
			case TAG_PREMIUM.DIAMOND:
				return SpellType.SUMMON_OUT_DIAMOND;
			default:
				Debug.LogWarning(string.Format("CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {0}", premiumType));
				break;
			}
			return SpellType.SUMMON_OUT_MEDIUM;
		}
		switch (premiumType)
		{
		case TAG_PREMIUM.NORMAL:
			break;
		case TAG_PREMIUM.GOLDEN:
			return SpellType.SUMMON_OUT_PREMIUM;
		case TAG_PREMIUM.DIAMOND:
			return SpellType.SUMMON_OUT_DIAMOND;
		default:
			Debug.LogWarning(string.Format("CardDef.DetermineSummonOutSpell_HandToPlay(): unexpected premium type {0}", premiumType));
			break;
		}
		return SpellType.SUMMON_OUT;
	}

	// Token: 0x060075C9 RID: 30153 RVA: 0x0025CFE5 File Offset: 0x0025B1E5
	private static void SetTextureIfNotNull(Material baseMat, ref Material targetMat, Texture tex)
	{
		if (baseMat == null)
		{
			return;
		}
		if (targetMat == null)
		{
			targetMat = UnityEngine.Object.Instantiate<Material>(baseMat);
			MaterialManagerService materialManagerService = CardDef.GetMaterialManagerService();
			if (materialManagerService != null)
			{
				materialManagerService.IgnoreMaterial(targetMat);
			}
		}
		targetMat.mainTexture = tex;
	}

	// Token: 0x060075CA RID: 30154 RVA: 0x0025D01D File Offset: 0x0025B21D
	private static MaterialManagerService GetMaterialManagerService()
	{
		if (CardDef.s_MaterialManagerService == null)
		{
			CardDef.s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return CardDef.s_MaterialManagerService;
	}

	// Token: 0x060075CB RID: 30155 RVA: 0x0025D038 File Offset: 0x0025B238
	public void OnPortraitLoaded(AssetHandle<Texture> portrait, int quality)
	{
		if (this.m_Portrait != null)
		{
			this.m_Portrait.OnPortraitLoaded(portrait, quality);
			return;
		}
		if (quality <= this.m_portraitQuality.TextureQuality)
		{
			Debug.LogWarning(string.Format("Loaded texture of quality lower or equal to what was was already available ({0} <= {1}), texture={2}", quality, this.m_portraitQuality, portrait));
			return;
		}
		this.m_portraitQuality.TextureQuality = quality;
		if (this.m_LoadedPortraitTexture)
		{
			AssetHandle.Set<Texture>(ref this.m_lowQualityPortrait, this.m_LoadedPortraitTexture);
		}
		AssetHandle.Set<Texture>(ref this.m_LoadedPortraitTexture, portrait);
		if (this.m_LoadedPremiumPortraitMaterial != null && string.IsNullOrEmpty(this.m_PremiumPortraitTexturePath))
		{
			this.m_LoadedPremiumPortraitMaterial.mainTexture = this.m_LoadedPortraitTexture;
			this.m_portraitQuality.LoadPremium = true;
		}
		if (this.m_LoadedPremiumClassMaterial != null && string.IsNullOrEmpty(this.m_PremiumPortraitTexturePath))
		{
			this.m_LoadedPremiumClassMaterial.mainTexture = this.m_LoadedPortraitTexture;
		}
		CardDef.SetTextureIfNotNull(this.m_DeckCardBarPortrait, ref this.m_LoadedDeckCardBarPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_EnchantmentPortrait, ref this.m_LoadedEnchantmentPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_HistoryTileFullPortrait, ref this.m_LoadedHistoryTileFullPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_HistoryTileHalfPortrait, ref this.m_LoadedHistoryTileHalfPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_LeaderboardTileFullPortrait, ref this.m_LoadedLeaderboardTileFullPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_CustomDeckPortrait, ref this.m_LoadedCustomDeckPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_DeckPickerPortrait, ref this.m_LoadedDeckPickerPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_PracticeAIPortrait, ref this.m_LoadedPracticeAIPortrait, this.m_LoadedPortraitTexture);
		CardDef.SetTextureIfNotNull(this.m_DeckBoxPortrait, ref this.m_LoadedDeckBoxPortrait, this.m_LoadedPortraitTexture);
	}

	// Token: 0x060075CC RID: 30156 RVA: 0x0025D238 File Offset: 0x0025B438
	public void OnPremiumMaterialLoaded(AssetHandle<Material> material, AssetHandle<Texture> portrait, AssetHandle<UberShaderAnimation> portraitAnimation)
	{
		if (this.m_Portrait != null)
		{
			this.m_Portrait.OnPremiumMaterialLoaded(material, portrait, portraitAnimation);
			return;
		}
		if (this.m_LoadedPremiumPortraitMaterial != null)
		{
			if (Application.isPlaying)
			{
				Debug.LogWarning(string.Format("Loaded premium material twice: {0}", material));
			}
			return;
		}
		if (material)
		{
			AssetHandle.Set<Material>(ref this.m_premiumMaterialHandle, material);
			this.m_LoadedPremiumPortraitMaterial = (Material)UnityEngine.Object.Instantiate(material);
			this.m_LoadedPremiumClassMaterial = (Material)UnityEngine.Object.Instantiate(material);
			MaterialManagerService materialManagerService = CardDef.GetMaterialManagerService();
			if (materialManagerService != null)
			{
				materialManagerService.IgnoreMaterial(this.m_LoadedPremiumPortraitMaterial);
				materialManagerService.IgnoreMaterial(this.m_LoadedPremiumClassMaterial);
			}
		}
		AssetHandle.Set<UberShaderAnimation>(ref this.m_premiumPortraitAnimation, portraitAnimation);
		if (this.m_LoadedPortraitTexture)
		{
			if (this.m_LoadedPremiumPortraitMaterial != null)
			{
				this.m_LoadedPremiumPortraitMaterial.mainTexture = this.m_LoadedPortraitTexture;
			}
			if (this.m_LoadedPremiumClassMaterial != null)
			{
				this.m_LoadedPremiumClassMaterial.mainTexture = this.m_LoadedPortraitTexture;
			}
			this.m_portraitQuality.LoadPremium = true;
		}
		if (portrait)
		{
			AssetHandle.Set<Texture>(ref this.m_loadedPremiumPortraitTexture, portrait);
			if (this.m_LoadedPremiumPortraitMaterial != null)
			{
				this.m_LoadedPremiumPortraitMaterial.mainTexture = portrait;
			}
			if (this.m_LoadedPremiumClassMaterial != null)
			{
				this.m_LoadedPremiumClassMaterial.mainTexture = portrait;
			}
			this.m_portraitQuality.LoadPremium = true;
		}
	}

	// Token: 0x060075CD RID: 30157 RVA: 0x0025D3B4 File Offset: 0x0025B5B4
	public CardPortraitQuality GetPortraitQuality()
	{
		return this.m_portraitQuality;
	}

	// Token: 0x060075CE RID: 30158 RVA: 0x0025D3BC File Offset: 0x0025B5BC
	public Texture GetPortraitTexture()
	{
		return this.GetPortraitTextureHandle();
	}

	// Token: 0x060075CF RID: 30159 RVA: 0x0025D3C9 File Offset: 0x0025B5C9
	public AssetHandle<Texture> GetPortraitTextureHandle()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.LoadedPortraitTexture;
		}
		return this.m_LoadedPortraitTexture;
	}

	// Token: 0x060075D0 RID: 30160 RVA: 0x0025D3EB File Offset: 0x0025B5EB
	public bool IsPremiumLoaded()
	{
		return this.m_portraitQuality.LoadPremium;
	}

	// Token: 0x060075D1 RID: 30161 RVA: 0x0025D3F8 File Offset: 0x0025B5F8
	public Material GetPremiumPortraitMaterial()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedPremiumPortraitMaterial;
		}
		return this.m_LoadedPremiumPortraitMaterial;
	}

	// Token: 0x060075D2 RID: 30162 RVA: 0x0025D41A File Offset: 0x0025B61A
	public UberShaderAnimation GetPremiumPortraitAnimation()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.PremiumPortraitAnimation;
		}
		return this.m_premiumPortraitAnimation;
	}

	// Token: 0x060075D3 RID: 30163 RVA: 0x0025D446 File Offset: 0x0025B646
	public Material GetDeckCardBarPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedDeckCardBarPortrait;
		}
		return this.m_LoadedDeckCardBarPortrait;
	}

	// Token: 0x060075D4 RID: 30164 RVA: 0x0025D468 File Offset: 0x0025B668
	public Material GetEnchantmentPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedEnchantmentPortrait;
		}
		return this.m_LoadedEnchantmentPortrait;
	}

	// Token: 0x060075D5 RID: 30165 RVA: 0x0025D48A File Offset: 0x0025B68A
	public Material GetHistoryTileFullPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedHistoryTileFullPortrait;
		}
		return this.m_LoadedHistoryTileFullPortrait;
	}

	// Token: 0x060075D6 RID: 30166 RVA: 0x0025D4AC File Offset: 0x0025B6AC
	public Material GetHistoryTileHalfPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedHistoryTileHalfPortrait;
		}
		return this.m_LoadedHistoryTileHalfPortrait;
	}

	// Token: 0x060075D7 RID: 30167 RVA: 0x0025D4CE File Offset: 0x0025B6CE
	public Material GetLeaderboardTileFullPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedLeaderboardTileFullPortrait;
		}
		return this.m_LoadedLeaderboardTileFullPortrait;
	}

	// Token: 0x060075D8 RID: 30168 RVA: 0x0025D4F0 File Offset: 0x0025B6F0
	public Material GetCustomDeckPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedCustomDeckPortrait;
		}
		return this.m_LoadedCustomDeckPortrait;
	}

	// Token: 0x060075D9 RID: 30169 RVA: 0x0025D512 File Offset: 0x0025B712
	public Material GetDeckPickerPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedDeckPickerPortrait;
		}
		return this.m_LoadedDeckPickerPortrait;
	}

	// Token: 0x060075DA RID: 30170 RVA: 0x0025D534 File Offset: 0x0025B734
	public Material GetPracticeAIPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedPracticeAIPortrait;
		}
		return this.m_LoadedPracticeAIPortrait;
	}

	// Token: 0x060075DB RID: 30171 RVA: 0x0025D556 File Offset: 0x0025B756
	public Material GetDeckBoxPortrait()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedDeckBoxPortrait;
		}
		return this.m_LoadedDeckBoxPortrait;
	}

	// Token: 0x060075DC RID: 30172 RVA: 0x0025D578 File Offset: 0x0025B778
	public AssetReference GetPortraitRef()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.GetPortraitRef();
		}
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PortraitTextureOverride))
		{
			return this.m_currentSpecialEvent.m_PortraitTextureOverride;
		}
		return this.m_PortraitTexturePath;
	}

	// Token: 0x060075DD RID: 30173 RVA: 0x0025D5D8 File Offset: 0x0025B7D8
	public AssetReference GetPremiumMaterialRef()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.GetPremiumMaterialRef();
		}
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumPortraitMaterialOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumPortraitMaterialOverride;
		}
		return this.m_PremiumPortraitMaterialPath;
	}

	// Token: 0x060075DE RID: 30174 RVA: 0x0025D638 File Offset: 0x0025B838
	public AssetReference GetPremiumPortraitRef()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.GetPremiumPortraitRef();
		}
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumPortraitTextureOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumPortraitTextureOverride;
		}
		return this.m_PremiumPortraitTexturePath;
	}

	// Token: 0x060075DF RID: 30175 RVA: 0x0025D698 File Offset: 0x0025B898
	public AssetReference GetPremiumAnimationRef()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.GetPremiumAnimationRef();
		}
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride;
		}
		return this.m_PremiumUberShaderAnimationPath;
	}

	// Token: 0x060075E0 RID: 30176 RVA: 0x0025D6F5 File Offset: 0x0025B8F5
	public Material GetPremiumClassMaterial()
	{
		if (this.m_Portrait != null)
		{
			return this.m_Portrait.m_LoadedPremiumClassMaterial;
		}
		return this.m_LoadedPremiumClassMaterial;
	}

	// Token: 0x060075E1 RID: 30177 RVA: 0x0025D717 File Offset: 0x0025B917
	public void UpdateSpecialEvent()
	{
		this.m_currentSpecialEvent = CardDefSpecialEvent.FindActiveEvent(this);
	}

	// Token: 0x04005CC5 RID: 23749
	[CustomEditField(Sections = "Portrait", T = EditType.ARTBUNDLE)]
	public Portrait m_Portrait;

	// Token: 0x04005CC6 RID: 23750
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_PortraitTexturePath;

	// Token: 0x04005CC7 RID: 23751
	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL, HidePredicate = "HideIfPortrait")]
	public string m_PremiumPortraitMaterialPath;

	// Token: 0x04005CC8 RID: 23752
	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION, HidePredicate = "HideIfPortrait")]
	public string m_PremiumUberShaderAnimationPath;

	// Token: 0x04005CC9 RID: 23753
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_PremiumPortraitTexturePath;

	// Token: 0x04005CCA RID: 23754
	[CustomEditField(Sections = "Portrait", T = EditType.MESH, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPlaneRTT_Hand;

	// Token: 0x04005CCB RID: 23755
	[CustomEditField(Sections = "Portrait", T = EditType.MESH, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPlaneRTT_Play;

	// Token: 0x04005CCC RID: 23756
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Color m_DiamondPlaneRTT_CearColor = Color.clear;

	// Token: 0x04005CCD RID: 23757
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE, HidePredicate = "HideIfPortrait")]
	public string m_DiamondPortraitTexturePath;

	// Token: 0x04005CCE RID: 23758
	[CustomEditField(Sections = "Portrait", T = EditType.GAME_OBJECT, HidePredicate = "HideIfPortrait")]
	public string m_DiamondModel;

	// Token: 0x04005CCF RID: 23759
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_DeckCardBarPortrait;

	// Token: 0x04005CD0 RID: 23760
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_EnchantmentPortrait;

	// Token: 0x04005CD1 RID: 23761
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_HistoryTileHalfPortrait;

	// Token: 0x04005CD2 RID: 23762
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_HistoryTileFullPortrait;

	// Token: 0x04005CD3 RID: 23763
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_LeaderboardTileFullPortrait;

	// Token: 0x04005CD4 RID: 23764
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material_MobileOverride m_CustomDeckPortrait;

	// Token: 0x04005CD5 RID: 23765
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material_MobileOverride m_DeckPickerPortrait;

	// Token: 0x04005CD6 RID: 23766
	[CustomEditField(Sections = "Portrait")]
	public Material m_LockedClassPortrait;

	// Token: 0x04005CD7 RID: 23767
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_PracticeAIPortrait;

	// Token: 0x04005CD8 RID: 23768
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public Material m_DeckBoxPortrait;

	// Token: 0x04005CD9 RID: 23769
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public bool m_AlwaysRenderPremiumPortrait;

	// Token: 0x04005CDA RID: 23770
	[CustomEditField(Sections = "Portrait", HidePredicate = "HideIfPortrait")]
	public CardSilhouetteOverride m_CardSilhouetteOverride;

	// Token: 0x04005CDB RID: 23771
	[CustomEditField(Sections = "Play")]
	public CardEffectDef m_PlayEffectDef;

	// Token: 0x04005CDC RID: 23772
	[CustomEditField(Sections = "Play")]
	public List<CardEffectDef> m_AdditionalPlayEffectDefs;

	// Token: 0x04005CDD RID: 23773
	[CustomEditField(Sections = "Attack")]
	public CardEffectDef m_AttackEffectDef;

	// Token: 0x04005CDE RID: 23774
	[CustomEditField(Sections = "Death")]
	public CardEffectDef m_DeathEffectDef;

	// Token: 0x04005CDF RID: 23775
	[CustomEditField(Sections = "Lifetime")]
	public CardEffectDef m_LifetimeEffectDef;

	// Token: 0x04005CE0 RID: 23776
	[CustomEditField(Sections = "Trigger")]
	public List<CardEffectDef> m_TriggerEffectDefs;

	// Token: 0x04005CE1 RID: 23777
	[CustomEditField(Sections = "SubOption")]
	public List<CardEffectDef> m_SubOptionEffectDefs;

	// Token: 0x04005CE2 RID: 23778
	[CustomEditField(Sections = "SubOption")]
	public List<List<CardEffectDef>> m_AdditionalSubOptionEffectDefs;

	// Token: 0x04005CE3 RID: 23779
	[CustomEditField(Sections = "ResetGame")]
	public List<CardEffectDef> m_ResetGameEffectDefs;

	// Token: 0x04005CE4 RID: 23780
	[CustomEditField(Sections = "Sub-Spells")]
	public List<CardEffectDef> m_SubSpellEffectDefs;

	// Token: 0x04005CE5 RID: 23781
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomSummonSpellPath;

	// Token: 0x04005CE6 RID: 23782
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomSummonSpellPath;

	// Token: 0x04005CE7 RID: 23783
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomSummonSpellPath;

	// Token: 0x04005CE8 RID: 23784
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomSpawnSpellPath;

	// Token: 0x04005CE9 RID: 23785
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomSpawnSpellPath;

	// Token: 0x04005CEA RID: 23786
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomSpawnSpellPath;

	// Token: 0x04005CEB RID: 23787
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomDeathSpellPath;

	// Token: 0x04005CEC RID: 23788
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomDeathSpellPath;

	// Token: 0x04005CED RID: 23789
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomDeathSpellPath;

	// Token: 0x04005CEE RID: 23790
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomDiscardSpellPath;

	// Token: 0x04005CEF RID: 23791
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_GoldenCustomDiscardSpellPath;

	// Token: 0x04005CF0 RID: 23792
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_DiamondCustomDiscardSpellPath;

	// Token: 0x04005CF1 RID: 23793
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomKeywordSpellPath;

	// Token: 0x04005CF2 RID: 23794
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomChoiceRevealSpellPath;

	// Token: 0x04005CF3 RID: 23795
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public string m_CustomChoiceConcealSpellPath;

	// Token: 0x04005CF4 RID: 23796
	[CustomEditField(Sections = "Custom", T = EditType.SPELL)]
	public List<SpellTableOverride> m_SpellTableOverrides;

	// Token: 0x04005CF5 RID: 23797
	[CustomEditField(Sections = "Hero", T = EditType.GAME_OBJECT)]
	public string m_CollectionHeroDefPath;

	// Token: 0x04005CF6 RID: 23798
	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_CustomHeroArmorSpell;

	// Token: 0x04005CF7 RID: 23799
	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectFriendly;

	// Token: 0x04005CF8 RID: 23800
	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectOpponent;

	// Token: 0x04005CF9 RID: 23801
	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectFriendlyPhone;

	// Token: 0x04005CFA RID: 23802
	[CustomEditField(Sections = "Hero", T = EditType.SPELL)]
	public string m_SocketInEffectOpponentPhone;

	// Token: 0x04005CFB RID: 23803
	[CustomEditField(Sections = "Hero")]
	public bool m_SocketInOverrideHeroAnimation;

	// Token: 0x04005CFC RID: 23804
	[CustomEditField(Sections = "Hero")]
	public bool m_SocketInParentEffectToHero = true;

	// Token: 0x04005CFD RID: 23805
	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroTray;

	// Token: 0x04005CFE RID: 23806
	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroTrayGolden;

	// Token: 0x04005CFF RID: 23807
	[CustomEditField(Sections = "Hero")]
	public List<Board.CustomTraySettings> m_CustomHeroTraySettings;

	// Token: 0x04005D00 RID: 23808
	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroPhoneTray;

	// Token: 0x04005D01 RID: 23809
	[CustomEditField(Sections = "Hero", T = EditType.TEXTURE)]
	public string m_CustomHeroPhoneManaGem;

	// Token: 0x04005D02 RID: 23810
	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLinePath;

	// Token: 0x04005D03 RID: 23811
	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLineBeforeVersusPath;

	// Token: 0x04005D04 RID: 23812
	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_AnnouncerLineAfterVersusPath;

	// Token: 0x04005D05 RID: 23813
	[CustomEditField(Sections = "Hero", T = EditType.SOUND_PREFAB)]
	public string m_HeroPickerSelectedPrefab;

	// Token: 0x04005D06 RID: 23814
	[CustomEditField(Sections = "Hero")]
	public List<EmoteEntryDef> m_EmoteDefs;

	// Token: 0x04005D07 RID: 23815
	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressDeathrattleDeath;

	// Token: 0x04005D08 RID: 23816
	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressPlaySoundsOnSummon;

	// Token: 0x04005D09 RID: 23817
	[CustomEditField(Sections = "Misc")]
	public bool m_SuppressPlaySoundsDuringMulligan;

	// Token: 0x04005D0A RID: 23818
	[CustomEditField(Sections = "Special Events")]
	public List<CardDefSpecialEvent> m_SpecialEvents;

	// Token: 0x04005D0B RID: 23819
	private static MaterialManagerService s_MaterialManagerService;

	// Token: 0x04005D0C RID: 23820
	private Material m_LoadedPremiumPortraitMaterial;

	// Token: 0x04005D0D RID: 23821
	private Material m_LoadedPremiumClassMaterial;

	// Token: 0x04005D0E RID: 23822
	private Material m_LoadedDeckCardBarPortrait;

	// Token: 0x04005D0F RID: 23823
	private Material m_LoadedEnchantmentPortrait;

	// Token: 0x04005D10 RID: 23824
	private Material m_LoadedHistoryTileFullPortrait;

	// Token: 0x04005D11 RID: 23825
	private Material m_LoadedHistoryTileHalfPortrait;

	// Token: 0x04005D12 RID: 23826
	private Material m_LoadedLeaderboardTileFullPortrait;

	// Token: 0x04005D13 RID: 23827
	private Material m_LoadedCustomDeckPortrait;

	// Token: 0x04005D14 RID: 23828
	private Material m_LoadedDeckPickerPortrait;

	// Token: 0x04005D15 RID: 23829
	private Material m_LoadedPracticeAIPortrait;

	// Token: 0x04005D16 RID: 23830
	private Material m_LoadedDeckBoxPortrait;

	// Token: 0x04005D17 RID: 23831
	private CardPortraitQuality m_portraitQuality = CardPortraitQuality.GetUnloaded();

	// Token: 0x04005D18 RID: 23832
	private CardDefSpecialEvent m_currentSpecialEvent;

	// Token: 0x04005D19 RID: 23833
	private AssetHandle<Texture> m_LoadedPortraitTexture;

	// Token: 0x04005D1A RID: 23834
	private AssetHandle<Texture> m_loadedPremiumPortraitTexture;

	// Token: 0x04005D1B RID: 23835
	private AssetHandle<Material> m_premiumMaterialHandle;

	// Token: 0x04005D1C RID: 23836
	private AssetHandle<UberShaderAnimation> m_premiumPortraitAnimation;

	// Token: 0x04005D1D RID: 23837
	private AssetHandle<Texture> m_lowQualityPortrait;

	// Token: 0x04005D1E RID: 23838
	protected const int LARGE_MINION_COST = 7;

	// Token: 0x04005D1F RID: 23839
	protected const int MEDIUM_MINION_COST = 4;
}
