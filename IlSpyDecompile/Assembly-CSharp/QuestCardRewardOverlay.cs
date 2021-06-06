using UnityEngine;

public class QuestCardRewardOverlay : MonoBehaviour
{
	public MeshRenderer m_RewardOverlayRenderer;

	[Header("Reward Overlay Texture Settings")]
	public Texture m_MinionRewardOverlayTexture;

	public Texture m_LegendaryMinionRewardOverlayTexture;

	public Texture m_SpellRewardOverlayTexture;

	public Texture m_GoldenSpellRewardOverlayTexture;

	public Texture m_WeaponRewardOverlayTexture;

	public Texture m_LegendaryWeaponRewardOverlayTexture;

	public Texture m_HeroPowerRewardOverlayTexture;

	[Header("Reward Overlay Position Settings")]
	public Vector3 m_MinionRewardPosition;

	public Vector3 m_SpellRewardPosition;

	public Vector3 m_WeaponRewardPosition;

	public Vector3 m_HeroPowerRewardPosition;

	[Header("Reward Background Glow Reference Settings")]
	public MeshRenderer m_RewardBackGlowRenderer;

	public Material m_DefaultRewardBackGlowMaterial;

	public Material m_HeroPowerRewardBackGlowMaterial;

	public void SetEntityType(EntityDef def, bool isPremium)
	{
		if (m_RewardOverlayRenderer != null)
		{
			TAG_CARDTYPE cardType = def.GetCardType();
			Texture overlayTextureForCardType = GetOverlayTextureForCardType(cardType, isPremium, def.IsElite());
			Material material = m_RewardOverlayRenderer.GetMaterial();
			material.SetTexture("_MainTex", overlayTextureForCardType);
			material.SetTexture("_AddTex", overlayTextureForCardType);
			m_RewardOverlayRenderer.transform.localPosition = GetPositionForCardType(cardType);
		}
		if (m_RewardBackGlowRenderer != null)
		{
			m_RewardBackGlowRenderer.SetMaterial(def.IsHeroPower() ? m_HeroPowerRewardBackGlowMaterial : m_DefaultRewardBackGlowMaterial);
		}
	}

	private Texture GetOverlayTextureForCardType(TAG_CARDTYPE cardType, bool isPremium, bool isElite)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			if (!isElite)
			{
				return m_MinionRewardOverlayTexture;
			}
			return m_LegendaryMinionRewardOverlayTexture;
		case TAG_CARDTYPE.SPELL:
			if (!isPremium)
			{
				return m_SpellRewardOverlayTexture;
			}
			return m_GoldenSpellRewardOverlayTexture;
		case TAG_CARDTYPE.WEAPON:
			if (!isElite)
			{
				return m_WeaponRewardOverlayTexture;
			}
			return m_LegendaryWeaponRewardOverlayTexture;
		case TAG_CARDTYPE.HERO_POWER:
			return m_HeroPowerRewardOverlayTexture;
		default:
			Debug.LogErrorFormat("Could not get quest overlay texture, unsupported type {0}", cardType.ToString());
			return null;
		}
	}

	private Vector3 GetPositionForCardType(TAG_CARDTYPE cardType)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			return m_MinionRewardPosition;
		case TAG_CARDTYPE.SPELL:
			return m_SpellRewardPosition;
		case TAG_CARDTYPE.WEAPON:
			return m_WeaponRewardPosition;
		case TAG_CARDTYPE.HERO_POWER:
			return m_HeroPowerRewardPosition;
		default:
			Debug.LogErrorFormat("Could not get quest overlay position, unsupported type {0}", cardType.ToString());
			return default(Vector3);
		}
	}
}
