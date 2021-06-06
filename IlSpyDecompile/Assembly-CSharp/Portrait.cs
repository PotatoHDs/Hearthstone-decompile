using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
[CreateAssetMenu]
public class Portrait : ScriptableObject
{
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_PremiumPortraitMaterialPath;

	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_PremiumUberShaderAnimationPath;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PremiumPortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_DiamondPortraitMaterialPath;

	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_DiamondUberShaderAnimationPath;

	[CustomEditField(Sections = "Portrait", T = EditType.MESH)]
	public string m_DiamondPlaneRTT_Play;

	[CustomEditField(Sections = "Portrait", T = EditType.MESH)]
	public string m_DiamondPlaneRTT_Hand;

	[CustomEditField(Sections = "Portrait")]
	public Color m_DiamondPlaneRTT_CearColor = Color.clear;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_DiamondPortraitTexturePath;

	[CustomEditField(Sections = "Portrait", T = EditType.GAME_OBJECT)]
	public string m_DiamondModel;

	[CustomEditField(Sections = "Portrait")]
	public Material m_DeckCardBarPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_EnchantmentPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_HistoryTileHalfPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_HistoryTileFullPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_LeaderboardTileFullPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_CustomDeckPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_DeckPickerPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_PracticeAIPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material m_DeckBoxPortrait;

	[CustomEditField(Sections = "Portrait")]
	public Material_MobileOverride m_ClassPickerPortrait;

	public CardPortraitQuality m_portraitQuality = CardPortraitQuality.GetUnloaded();

	public CardDefSpecialEvent m_currentSpecialEvent;

	private AssetHandle<Texture> m_loadedPortraitTexture;

	private AssetHandle<Texture> m_loadedPremiumPortraitTexture;

	private AssetHandle<Material> m_premiumMaterialHandle;

	private AssetHandle<UberShaderAnimation> m_premiumPortraitAnimation;

	private AssetHandle<Texture> m_lowQualityPortrait;

	public AssetHandle<Texture> LoadedPortraitTexture => m_loadedPortraitTexture;

	public Material m_LoadedPremiumPortraitMaterial { get; private set; }

	public Material m_LoadedDeckCardBarPortrait { get; private set; }

	public Material m_LoadedEnchantmentPortrait { get; private set; }

	public Material m_LoadedHistoryTileFullPortrait { get; private set; }

	public Material m_LoadedHistoryTileHalfPortrait { get; private set; }

	public Material m_LoadedLeaderboardTileFullPortrait { get; private set; }

	public Material m_LoadedCustomDeckPortrait { get; private set; }

	public Material m_LoadedDeckPickerPortrait { get; private set; }

	public Material m_LoadedPracticeAIPortrait { get; private set; }

	public Material m_LoadedDeckBoxPortrait { get; private set; }

	public AssetHandle<UberShaderAnimation> PremiumPortraitAnimation => m_premiumPortraitAnimation;

	public Material m_LoadedPremiumClassMaterial { get; set; }

	public Material m_LoadedClassPickerPortrait { get; set; }

	public void OnDestroy()
	{
		if ((bool)m_LoadedPremiumPortraitMaterial)
		{
			Object.Destroy(m_LoadedPremiumPortraitMaterial);
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
		if ((bool)m_LoadedPremiumClassMaterial)
		{
			Object.Destroy(m_LoadedPremiumClassMaterial);
		}
		if ((bool)m_LoadedClassPickerPortrait)
		{
			Object.Destroy(m_LoadedClassPickerPortrait);
		}
		AssetHandle.SafeDispose(ref m_loadedPortraitTexture);
		AssetHandle.SafeDispose(ref m_loadedPremiumPortraitTexture);
		AssetHandle.SafeDispose(ref m_premiumMaterialHandle);
		AssetHandle.SafeDispose(ref m_premiumPortraitAnimation);
		AssetHandle.SafeDispose(ref m_lowQualityPortrait);
	}

	private void SetTextureIfNotNull(Material baseMat, Material targetMat, Texture tex)
	{
		if (!(baseMat == null))
		{
			if (targetMat == null)
			{
				targetMat = Object.Instantiate(baseMat);
			}
			targetMat.mainTexture = tex;
		}
	}

	public void OnPortraitLoaded(AssetHandle<Texture> portrait, int quality)
	{
		if (quality <= m_portraitQuality.TextureQuality)
		{
			Debug.LogWarning($"Loaded texture of quality lower or equal to what was was already available ({quality} <= {m_portraitQuality}), texture={portrait}");
			return;
		}
		m_portraitQuality.TextureQuality = quality;
		if ((bool)m_loadedPortraitTexture)
		{
			AssetHandle.Set(ref m_lowQualityPortrait, m_loadedPortraitTexture);
		}
		AssetHandle.Set(ref m_loadedPortraitTexture, portrait);
		if (m_LoadedPremiumPortraitMaterial != null && string.IsNullOrEmpty(m_PremiumPortraitTexturePath))
		{
			m_LoadedPremiumPortraitMaterial.mainTexture = portrait;
			m_portraitQuality.LoadPremium = true;
			if (m_LoadedClassPickerPortrait != null)
			{
				m_LoadedClassPickerPortrait.mainTexture = portrait;
			}
		}
		SetTextureIfNotNull(m_DeckCardBarPortrait, m_LoadedDeckCardBarPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_EnchantmentPortrait, m_LoadedEnchantmentPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_HistoryTileFullPortrait, m_LoadedHistoryTileFullPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_HistoryTileHalfPortrait, m_LoadedHistoryTileHalfPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_LeaderboardTileFullPortrait, m_LoadedLeaderboardTileFullPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_CustomDeckPortrait, m_LoadedCustomDeckPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_DeckPickerPortrait, m_LoadedDeckPickerPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_PracticeAIPortrait, m_LoadedPracticeAIPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_DeckBoxPortrait, m_LoadedDeckBoxPortrait, m_loadedPortraitTexture);
		SetTextureIfNotNull(m_ClassPickerPortrait, m_LoadedClassPickerPortrait, m_loadedPortraitTexture);
	}

	public void OnPremiumMaterialLoaded(AssetHandle<Material> material, AssetHandle<Texture> portrait, AssetHandle<UberShaderAnimation> portraitAnimation)
	{
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
		}
		AssetHandle.Set(ref m_premiumPortraitAnimation, portraitAnimation);
		if ((bool)m_loadedPortraitTexture)
		{
			if (m_LoadedPremiumPortraitMaterial != null)
			{
				m_LoadedPremiumPortraitMaterial.mainTexture = m_loadedPortraitTexture;
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
			m_portraitQuality.LoadPremium = true;
		}
	}

	public bool IsPremiumLoaded()
	{
		return m_portraitQuality.LoadPremium;
	}

	public AssetReference GetPortraitRef()
	{
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PortraitTextureOverride))
		{
			return m_currentSpecialEvent.m_PortraitTextureOverride;
		}
		return m_PortraitTexturePath;
	}

	public AssetReference GetPremiumMaterialRef()
	{
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumPortraitMaterialOverride))
		{
			return m_currentSpecialEvent.m_PremiumPortraitMaterialOverride;
		}
		return m_PremiumPortraitMaterialPath;
	}

	public AssetReference GetPremiumPortraitRef()
	{
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumPortraitTextureOverride))
		{
			return m_currentSpecialEvent.m_PremiumPortraitTextureOverride;
		}
		return m_PremiumPortraitTexturePath;
	}

	public AssetReference GetPremiumAnimationRef()
	{
		if (m_currentSpecialEvent != null && !string.IsNullOrEmpty(m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride))
		{
			return m_currentSpecialEvent.m_PremiumUberShaderAnimationOverride;
		}
		return m_PremiumUberShaderAnimationPath;
	}
}
