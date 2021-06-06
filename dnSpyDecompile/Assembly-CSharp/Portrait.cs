using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000013 RID: 19
[CustomEditClass]
[CreateAssetMenu]
public class Portrait : ScriptableObject
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000066 RID: 102 RVA: 0x000031EF File Offset: 0x000013EF
	public AssetHandle<Texture> LoadedPortraitTexture
	{
		get
		{
			return this.m_loadedPortraitTexture;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000067 RID: 103 RVA: 0x000031F7 File Offset: 0x000013F7
	// (set) Token: 0x06000068 RID: 104 RVA: 0x000031FF File Offset: 0x000013FF
	public Material m_LoadedPremiumPortraitMaterial { get; private set; }

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000069 RID: 105 RVA: 0x00003208 File Offset: 0x00001408
	// (set) Token: 0x0600006A RID: 106 RVA: 0x00003210 File Offset: 0x00001410
	public Material m_LoadedDeckCardBarPortrait { get; private set; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600006B RID: 107 RVA: 0x00003219 File Offset: 0x00001419
	// (set) Token: 0x0600006C RID: 108 RVA: 0x00003221 File Offset: 0x00001421
	public Material m_LoadedEnchantmentPortrait { get; private set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600006D RID: 109 RVA: 0x0000322A File Offset: 0x0000142A
	// (set) Token: 0x0600006E RID: 110 RVA: 0x00003232 File Offset: 0x00001432
	public Material m_LoadedHistoryTileFullPortrait { get; private set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600006F RID: 111 RVA: 0x0000323B File Offset: 0x0000143B
	// (set) Token: 0x06000070 RID: 112 RVA: 0x00003243 File Offset: 0x00001443
	public Material m_LoadedHistoryTileHalfPortrait { get; private set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000071 RID: 113 RVA: 0x0000324C File Offset: 0x0000144C
	// (set) Token: 0x06000072 RID: 114 RVA: 0x00003254 File Offset: 0x00001454
	public Material m_LoadedLeaderboardTileFullPortrait { get; private set; }

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000073 RID: 115 RVA: 0x0000325D File Offset: 0x0000145D
	// (set) Token: 0x06000074 RID: 116 RVA: 0x00003265 File Offset: 0x00001465
	public Material m_LoadedCustomDeckPortrait { get; private set; }

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000075 RID: 117 RVA: 0x0000326E File Offset: 0x0000146E
	// (set) Token: 0x06000076 RID: 118 RVA: 0x00003276 File Offset: 0x00001476
	public Material m_LoadedDeckPickerPortrait { get; private set; }

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000077 RID: 119 RVA: 0x0000327F File Offset: 0x0000147F
	// (set) Token: 0x06000078 RID: 120 RVA: 0x00003287 File Offset: 0x00001487
	public Material m_LoadedPracticeAIPortrait { get; private set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000079 RID: 121 RVA: 0x00003290 File Offset: 0x00001490
	// (set) Token: 0x0600007A RID: 122 RVA: 0x00003298 File Offset: 0x00001498
	public Material m_LoadedDeckBoxPortrait { get; private set; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600007B RID: 123 RVA: 0x000032A1 File Offset: 0x000014A1
	public AssetHandle<UberShaderAnimation> PremiumPortraitAnimation
	{
		get
		{
			return this.m_premiumPortraitAnimation;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600007C RID: 124 RVA: 0x000032A9 File Offset: 0x000014A9
	// (set) Token: 0x0600007D RID: 125 RVA: 0x000032B1 File Offset: 0x000014B1
	public Material m_LoadedPremiumClassMaterial { get; set; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600007E RID: 126 RVA: 0x000032BA File Offset: 0x000014BA
	// (set) Token: 0x0600007F RID: 127 RVA: 0x000032C2 File Offset: 0x000014C2
	public Material m_LoadedClassPickerPortrait { get; set; }

	// Token: 0x06000080 RID: 128 RVA: 0x000032CC File Offset: 0x000014CC
	public void OnDestroy()
	{
		if (this.m_LoadedPremiumPortraitMaterial)
		{
			UnityEngine.Object.Destroy(this.m_LoadedPremiumPortraitMaterial);
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
		if (this.m_LoadedPremiumClassMaterial)
		{
			UnityEngine.Object.Destroy(this.m_LoadedPremiumClassMaterial);
		}
		if (this.m_LoadedClassPickerPortrait)
		{
			UnityEngine.Object.Destroy(this.m_LoadedClassPickerPortrait);
		}
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedPortraitTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedPremiumPortraitTexture);
		AssetHandle.SafeDispose<Material>(ref this.m_premiumMaterialHandle);
		AssetHandle.SafeDispose<UberShaderAnimation>(ref this.m_premiumPortraitAnimation);
		AssetHandle.SafeDispose<Texture>(ref this.m_lowQualityPortrait);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003430 File Offset: 0x00001630
	private void SetTextureIfNotNull(Material baseMat, Material targetMat, Texture tex)
	{
		if (baseMat == null)
		{
			return;
		}
		if (targetMat == null)
		{
			targetMat = UnityEngine.Object.Instantiate<Material>(baseMat);
		}
		targetMat.mainTexture = tex;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003454 File Offset: 0x00001654
	public void OnPortraitLoaded(AssetHandle<Texture> portrait, int quality)
	{
		if (quality <= this.m_portraitQuality.TextureQuality)
		{
			Debug.LogWarning(string.Format("Loaded texture of quality lower or equal to what was was already available ({0} <= {1}), texture={2}", quality, this.m_portraitQuality, portrait));
			return;
		}
		this.m_portraitQuality.TextureQuality = quality;
		if (this.m_loadedPortraitTexture)
		{
			AssetHandle.Set<Texture>(ref this.m_lowQualityPortrait, this.m_loadedPortraitTexture);
		}
		AssetHandle.Set<Texture>(ref this.m_loadedPortraitTexture, portrait);
		if (this.m_LoadedPremiumPortraitMaterial != null && string.IsNullOrEmpty(this.m_PremiumPortraitTexturePath))
		{
			this.m_LoadedPremiumPortraitMaterial.mainTexture = portrait;
			this.m_portraitQuality.LoadPremium = true;
			if (this.m_LoadedClassPickerPortrait != null)
			{
				this.m_LoadedClassPickerPortrait.mainTexture = portrait;
			}
		}
		this.SetTextureIfNotNull(this.m_DeckCardBarPortrait, this.m_LoadedDeckCardBarPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_EnchantmentPortrait, this.m_LoadedEnchantmentPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_HistoryTileFullPortrait, this.m_LoadedHistoryTileFullPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_HistoryTileHalfPortrait, this.m_LoadedHistoryTileHalfPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_LeaderboardTileFullPortrait, this.m_LoadedLeaderboardTileFullPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_CustomDeckPortrait, this.m_LoadedCustomDeckPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_DeckPickerPortrait, this.m_LoadedDeckPickerPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_PracticeAIPortrait, this.m_LoadedPracticeAIPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_DeckBoxPortrait, this.m_LoadedDeckBoxPortrait, this.m_loadedPortraitTexture);
		this.SetTextureIfNotNull(this.m_ClassPickerPortrait, this.m_LoadedClassPickerPortrait, this.m_loadedPortraitTexture);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000364C File Offset: 0x0000184C
	public void OnPremiumMaterialLoaded(AssetHandle<Material> material, AssetHandle<Texture> portrait, AssetHandle<UberShaderAnimation> portraitAnimation)
	{
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
		}
		AssetHandle.Set<UberShaderAnimation>(ref this.m_premiumPortraitAnimation, portraitAnimation);
		if (this.m_loadedPortraitTexture)
		{
			if (this.m_LoadedPremiumPortraitMaterial != null)
			{
				this.m_LoadedPremiumPortraitMaterial.mainTexture = this.m_loadedPortraitTexture;
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
			this.m_portraitQuality.LoadPremium = true;
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003731 File Offset: 0x00001931
	public bool IsPremiumLoaded()
	{
		return this.m_portraitQuality.LoadPremium;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000373E File Offset: 0x0000193E
	public AssetReference GetPortraitRef()
	{
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PortraitTextureOverride))
		{
			return this.m_currentSpecialEvent.m_PortraitTextureOverride;
		}
		return this.m_PortraitTexturePath;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003776 File Offset: 0x00001976
	public AssetReference GetPremiumMaterialRef()
	{
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumPortraitMaterialOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumPortraitMaterialOverride;
		}
		return this.m_PremiumPortraitMaterialPath;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000037AE File Offset: 0x000019AE
	public AssetReference GetPremiumPortraitRef()
	{
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumPortraitTextureOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumPortraitTextureOverride;
		}
		return this.m_PremiumPortraitTexturePath;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000037E6 File Offset: 0x000019E6
	public AssetReference GetPremiumAnimationRef()
	{
		if (this.m_currentSpecialEvent != null && !string.IsNullOrEmpty(this.m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride))
		{
			return this.m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride;
		}
		return this.m_PremiumUberShaderAnimationPath;
	}

	// Token: 0x04000030 RID: 48
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PortraitTexturePath;

	// Token: 0x04000031 RID: 49
	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_PremiumPortraitMaterialPath;

	// Token: 0x04000032 RID: 50
	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_PremiumUberShaderAnimationPath;

	// Token: 0x04000033 RID: 51
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PremiumPortraitTexturePath;

	// Token: 0x04000034 RID: 52
	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_DiamondPortraitMaterialPath;

	// Token: 0x04000035 RID: 53
	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_DiamondUberShaderAnimationPath;

	// Token: 0x04000036 RID: 54
	[CustomEditField(Sections = "Portrait", T = EditType.MESH)]
	public string m_DiamondPlaneRTT_Play;

	// Token: 0x04000037 RID: 55
	[CustomEditField(Sections = "Portrait", T = EditType.MESH)]
	public string m_DiamondPlaneRTT_Hand;

	// Token: 0x04000038 RID: 56
	[CustomEditField(Sections = "Portrait")]
	public Color m_DiamondPlaneRTT_CearColor = Color.clear;

	// Token: 0x04000039 RID: 57
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_DiamondPortraitTexturePath;

	// Token: 0x0400003A RID: 58
	[CustomEditField(Sections = "Portrait", T = EditType.GAME_OBJECT)]
	public string m_DiamondModel;

	// Token: 0x0400003B RID: 59
	[CustomEditField(Sections = "Portrait")]
	public Material m_DeckCardBarPortrait;

	// Token: 0x0400003C RID: 60
	[CustomEditField(Sections = "Portrait")]
	public Material m_EnchantmentPortrait;

	// Token: 0x0400003D RID: 61
	[CustomEditField(Sections = "Portrait")]
	public Material m_HistoryTileHalfPortrait;

	// Token: 0x0400003E RID: 62
	[CustomEditField(Sections = "Portrait")]
	public Material m_HistoryTileFullPortrait;

	// Token: 0x0400003F RID: 63
	[CustomEditField(Sections = "Portrait")]
	public Material m_LeaderboardTileFullPortrait;

	// Token: 0x04000040 RID: 64
	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_CustomDeckPortrait;

	// Token: 0x04000041 RID: 65
	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_DeckPickerPortrait;

	// Token: 0x04000042 RID: 66
	[CustomEditField(Sections = "Portrait")]
	public Material m_PracticeAIPortrait;

	// Token: 0x04000043 RID: 67
	[CustomEditField(Sections = "Portrait")]
	public Material m_DeckBoxPortrait;

	// Token: 0x04000044 RID: 68
	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_ClassPickerPortrait;

	// Token: 0x04000045 RID: 69
	public CardPortraitQuality m_portraitQuality = CardPortraitQuality.GetUnloaded();

	// Token: 0x04000046 RID: 70
	public CardDefSpecialEvent m_currentSpecialEvent;

	// Token: 0x04000053 RID: 83
	private AssetHandle<Texture> m_loadedPortraitTexture;

	// Token: 0x04000054 RID: 84
	private AssetHandle<Texture> m_loadedPremiumPortraitTexture;

	// Token: 0x04000055 RID: 85
	private AssetHandle<Material> m_premiumMaterialHandle;

	// Token: 0x04000056 RID: 86
	private AssetHandle<UberShaderAnimation> m_premiumPortraitAnimation;

	// Token: 0x04000057 RID: 87
	private AssetHandle<Texture> m_lowQualityPortrait;
}
