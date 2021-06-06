using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class BigCardEnchantmentPanel : MonoBehaviour
{
	// Token: 0x060028C7 RID: 10439 RVA: 0x000CEA74 File Offset: 0x000CCC74
	private void Awake()
	{
		this.m_initialScale = base.transform.localScale;
		this.m_initialBackgroundHeight = this.m_Background.GetComponentInChildren<MeshRenderer>().bounds.size.z;
		this.m_initialBackgroundScale = this.m_Background.transform.localScale;
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x000CEACB File Offset: 0x000CCCCB
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef enchantmentCardDef = this.m_enchantmentCardDef;
		if (enchantmentCardDef != null)
		{
			enchantmentCardDef.Dispose();
		}
		this.m_enchantmentCardDef = null;
		DefLoader.DisposableCardDef creatorCardDef = this.m_creatorCardDef;
		if (creatorCardDef != null)
		{
			creatorCardDef.Dispose();
		}
		this.m_creatorCardDef = null;
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x000CEB00 File Offset: 0x000CCD00
	public void SetEnchantment(Entity enchantment)
	{
		this.m_enchantment = enchantment;
		string cardId = this.m_enchantment.GetCardId();
		DefLoader.Get().LoadCardDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnEnchantmentCardDefLoaded), null, new CardPortraitQuality(1, this.m_enchantment.GetPremiumType()));
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x000CEB49 File Offset: 0x000CCD49
	public void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		base.gameObject.SetActive(true);
		this.UpdateLayout();
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x000CEB6D File Offset: 0x000CCD6D
	public void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060028CC RID: 10444 RVA: 0x000CEB8B File Offset: 0x000CCD8B
	public void ResetScale()
	{
		base.transform.localScale = this.m_initialScale;
		this.m_Background.transform.localScale = this.m_initialBackgroundScale;
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x000CEBB4 File Offset: 0x000CCDB4
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060028CE RID: 10446 RVA: 0x000CEBBC File Offset: 0x000CCDBC
	public float GetHeight()
	{
		return this.m_Background.GetComponentInChildren<MeshRenderer>().bounds.size.z;
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x000CEBE8 File Offset: 0x000CCDE8
	private void OnEnchantmentCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		bool flag = false;
		if (cardDef != null)
		{
			DefLoader.DisposableCardDef enchantmentCardDef = this.m_enchantmentCardDef;
			if (enchantmentCardDef != null)
			{
				enchantmentCardDef.Dispose();
			}
			this.m_enchantmentCardDef = cardDef;
			if (this.m_enchantmentCardDef.CardDef.GetEnchantmentPortrait() != null)
			{
				this.m_Actor.GetMeshRenderer(false).SetMaterial(this.m_enchantmentCardDef.CardDef.GetEnchantmentPortrait());
				flag = true;
			}
			else if (this.m_enchantmentCardDef.CardDef.GetHistoryTileFullPortrait() != null)
			{
				this.m_Actor.GetMeshRenderer(false).SetMaterial(this.m_enchantmentCardDef.CardDef.GetHistoryTileFullPortrait());
				flag = true;
			}
			else if (this.m_enchantmentCardDef.CardDef.GetPortraitTexture() != null)
			{
				this.m_Actor.SetPortraitTextureOverride(this.m_enchantmentCardDef.CardDef.GetPortraitTexture());
				flag = true;
			}
		}
		this.m_HeaderText.Text = this.m_enchantment.GetName();
		this.m_header = this.m_enchantment.GetName();
		this.SetMultiplier(Mathf.Max(this.m_enchantment.GetTag(GAME_TAG.SPAWN_TIME_COUNT), 1));
		this.m_BodyText.Text = this.m_enchantment.GetCardTextInHand();
		if (!flag)
		{
			this.LoadCreatorCardDef();
		}
	}

	// Token: 0x060028D0 RID: 10448 RVA: 0x000CED28 File Offset: 0x000CCF28
	private void LoadCreatorCardDef()
	{
		if (this.m_enchantment == null)
		{
			return;
		}
		string enchantmentCreatorCardIDForPortrait = this.m_enchantment.GetEnchantmentCreatorCardIDForPortrait();
		if (string.IsNullOrEmpty(enchantmentCreatorCardIDForPortrait))
		{
			this.m_Actor.GetMeshRenderer(false).SetMaterial(this.m_FallbackEnchantmentPortrait);
			return;
		}
		DefLoader.Get().LoadCardDef(enchantmentCreatorCardIDForPortrait, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnCreatorCardDefLoaded), null, new CardPortraitQuality(1, this.m_enchantment.GetPremiumType()));
	}

	// Token: 0x060028D1 RID: 10449 RVA: 0x000CED94 File Offset: 0x000CCF94
	private void OnCreatorCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		if (cardDef == null)
		{
			return;
		}
		DefLoader.DisposableCardDef creatorCardDef = this.m_creatorCardDef;
		if (creatorCardDef != null)
		{
			creatorCardDef.Dispose();
		}
		this.m_creatorCardDef = cardDef;
		if (this.m_creatorCardDef.CardDef.GetEnchantmentPortrait() != null)
		{
			this.m_Actor.GetMeshRenderer(false).SetMaterial(this.m_creatorCardDef.CardDef.GetEnchantmentPortrait());
			return;
		}
		if (this.m_creatorCardDef.CardDef.GetHistoryTileFullPortrait() != null)
		{
			this.m_Actor.GetMeshRenderer(false).SetMaterial(this.m_creatorCardDef.CardDef.GetHistoryTileFullPortrait());
			return;
		}
		if (this.m_creatorCardDef.CardDef.GetPortraitTexture() != null)
		{
			this.m_Actor.SetPortraitTextureOverride(this.m_creatorCardDef.CardDef.GetPortraitTexture());
			return;
		}
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x000CEE68 File Offset: 0x000CD068
	private void UpdateLayout()
	{
		this.m_HeaderText.UpdateNow(false);
		this.m_BodyText.UpdateNow(false);
		Bounds bounds = this.m_Actor.GetMeshRenderer(false).bounds;
		Bounds textWorldSpaceBounds = this.m_HeaderText.GetTextWorldSpaceBounds();
		Bounds textWorldSpaceBounds2 = this.m_BodyText.GetTextWorldSpaceBounds();
		float z = bounds.min.z;
		float z2 = bounds.max.z;
		float z3 = textWorldSpaceBounds.min.z;
		float z4 = textWorldSpaceBounds.max.z;
		float z5 = textWorldSpaceBounds2.min.z;
		float z6 = textWorldSpaceBounds2.max.z;
		float num = Mathf.Min(Mathf.Min(z, z3), z5);
		float num2 = Mathf.Max(Mathf.Max(z2, z4), z6) - num + 0.1f;
		base.transform.localScale = this.m_initialScale;
		base.transform.localEulerAngles = Vector3.zero;
		TransformUtil.SetLocalScaleZ(this.m_Background, this.m_initialBackgroundScale.z * (num2 / this.m_initialBackgroundHeight));
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x000CEF73 File Offset: 0x000CD173
	public string GetEnchantmentId()
	{
		if (this.m_enchantment == null)
		{
			return null;
		}
		return this.m_enchantment.GetCardId();
	}

	// Token: 0x060028D4 RID: 10452 RVA: 0x000CEF8A File Offset: 0x000CD18A
	public void IncrementEnchantmentMultiplier(uint amount = 1U)
	{
		this.SetMultiplier(this.m_multiplier + (int)amount);
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x000CEF9C File Offset: 0x000CD19C
	public void SetMultiplier(int multiplier)
	{
		this.m_multiplier = multiplier;
		if (this.m_multiplier > 1)
		{
			this.m_HeaderText.Text = GameStrings.Format("GAMEPLAY_ENCHANTMENT_MULTIPLIER_HEADER", new object[]
			{
				this.m_multiplier,
				this.m_header
			});
			return;
		}
		this.m_HeaderText.Text = this.m_header;
	}

	// Token: 0x0400173F RID: 5951
	public Actor m_Actor;

	// Token: 0x04001740 RID: 5952
	public UberText m_HeaderText;

	// Token: 0x04001741 RID: 5953
	public UberText m_BodyText;

	// Token: 0x04001742 RID: 5954
	public GameObject m_Background;

	// Token: 0x04001743 RID: 5955
	public Material m_FallbackEnchantmentPortrait;

	// Token: 0x04001744 RID: 5956
	private Entity m_enchantment;

	// Token: 0x04001745 RID: 5957
	private DefLoader.DisposableCardDef m_enchantmentCardDef;

	// Token: 0x04001746 RID: 5958
	private DefLoader.DisposableCardDef m_creatorCardDef;

	// Token: 0x04001747 RID: 5959
	private Vector3 m_initialScale;

	// Token: 0x04001748 RID: 5960
	private float m_initialBackgroundHeight;

	// Token: 0x04001749 RID: 5961
	private Vector3 m_initialBackgroundScale;

	// Token: 0x0400174A RID: 5962
	private bool m_shown;

	// Token: 0x0400174B RID: 5963
	private int m_multiplier = 1;

	// Token: 0x0400174C RID: 5964
	private string m_header = "";
}
