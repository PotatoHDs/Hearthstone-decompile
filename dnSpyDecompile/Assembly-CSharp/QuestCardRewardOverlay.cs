using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class QuestCardRewardOverlay : MonoBehaviour
{
	// Token: 0x06002897 RID: 10391 RVA: 0x000CBD94 File Offset: 0x000C9F94
	public void SetEntityType(EntityDef def, bool isPremium)
	{
		if (this.m_RewardOverlayRenderer != null)
		{
			TAG_CARDTYPE cardType = def.GetCardType();
			Texture overlayTextureForCardType = this.GetOverlayTextureForCardType(cardType, isPremium, def.IsElite());
			Material material = this.m_RewardOverlayRenderer.GetMaterial();
			material.SetTexture("_MainTex", overlayTextureForCardType);
			material.SetTexture("_AddTex", overlayTextureForCardType);
			this.m_RewardOverlayRenderer.transform.localPosition = this.GetPositionForCardType(cardType);
		}
		if (this.m_RewardBackGlowRenderer != null)
		{
			this.m_RewardBackGlowRenderer.SetMaterial(def.IsHeroPower() ? this.m_HeroPowerRewardBackGlowMaterial : this.m_DefaultRewardBackGlowMaterial);
		}
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x000CBE30 File Offset: 0x000CA030
	private Texture GetOverlayTextureForCardType(TAG_CARDTYPE cardType, bool isPremium, bool isElite)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			if (!isElite)
			{
				return this.m_MinionRewardOverlayTexture;
			}
			return this.m_LegendaryMinionRewardOverlayTexture;
		case TAG_CARDTYPE.SPELL:
			if (!isPremium)
			{
				return this.m_SpellRewardOverlayTexture;
			}
			return this.m_GoldenSpellRewardOverlayTexture;
		case TAG_CARDTYPE.WEAPON:
			if (!isElite)
			{
				return this.m_WeaponRewardOverlayTexture;
			}
			return this.m_LegendaryWeaponRewardOverlayTexture;
		case TAG_CARDTYPE.HERO_POWER:
			return this.m_HeroPowerRewardOverlayTexture;
		}
		Debug.LogErrorFormat("Could not get quest overlay texture, unsupported type {0}", new object[]
		{
			cardType.ToString()
		});
		return null;
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x000CBEC0 File Offset: 0x000CA0C0
	private Vector3 GetPositionForCardType(TAG_CARDTYPE cardType)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			return this.m_MinionRewardPosition;
		case TAG_CARDTYPE.SPELL:
			return this.m_SpellRewardPosition;
		case TAG_CARDTYPE.WEAPON:
			return this.m_WeaponRewardPosition;
		case TAG_CARDTYPE.HERO_POWER:
			return this.m_HeroPowerRewardPosition;
		}
		Debug.LogErrorFormat("Could not get quest overlay position, unsupported type {0}", new object[]
		{
			cardType.ToString()
		});
		return default(Vector3);
	}

	// Token: 0x04001717 RID: 5911
	public MeshRenderer m_RewardOverlayRenderer;

	// Token: 0x04001718 RID: 5912
	[Header("Reward Overlay Texture Settings")]
	public Texture m_MinionRewardOverlayTexture;

	// Token: 0x04001719 RID: 5913
	public Texture m_LegendaryMinionRewardOverlayTexture;

	// Token: 0x0400171A RID: 5914
	public Texture m_SpellRewardOverlayTexture;

	// Token: 0x0400171B RID: 5915
	public Texture m_GoldenSpellRewardOverlayTexture;

	// Token: 0x0400171C RID: 5916
	public Texture m_WeaponRewardOverlayTexture;

	// Token: 0x0400171D RID: 5917
	public Texture m_LegendaryWeaponRewardOverlayTexture;

	// Token: 0x0400171E RID: 5918
	public Texture m_HeroPowerRewardOverlayTexture;

	// Token: 0x0400171F RID: 5919
	[Header("Reward Overlay Position Settings")]
	public Vector3 m_MinionRewardPosition;

	// Token: 0x04001720 RID: 5920
	public Vector3 m_SpellRewardPosition;

	// Token: 0x04001721 RID: 5921
	public Vector3 m_WeaponRewardPosition;

	// Token: 0x04001722 RID: 5922
	public Vector3 m_HeroPowerRewardPosition;

	// Token: 0x04001723 RID: 5923
	[Header("Reward Background Glow Reference Settings")]
	public MeshRenderer m_RewardBackGlowRenderer;

	// Token: 0x04001724 RID: 5924
	public Material m_DefaultRewardBackGlowMaterial;

	// Token: 0x04001725 RID: 5925
	public Material m_HeroPowerRewardBackGlowMaterial;
}
