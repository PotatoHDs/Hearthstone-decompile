using System;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class CardTypeBanner : MonoBehaviour
{
	// Token: 0x060028EF RID: 10479 RVA: 0x000CFAEB File Offset: 0x000CDCEB
	private void Awake()
	{
		CardTypeBanner.s_instance = this;
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x000CFAF3 File Offset: 0x000CDCF3
	private void OnDestroy()
	{
		CardTypeBanner.s_instance = null;
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x000CFAFB File Offset: 0x000CDCFB
	private void Update()
	{
		if (this.m_card != null)
		{
			if (this.m_card.GetActor().IsShown())
			{
				this.UpdatePosition();
				return;
			}
			this.Hide();
		}
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x000CFB2A File Offset: 0x000CDD2A
	public static CardTypeBanner Get()
	{
		return CardTypeBanner.s_instance;
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x000CFB31 File Offset: 0x000CDD31
	public bool IsShown()
	{
		return this.m_card;
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x000CFB3E File Offset: 0x000CDD3E
	public void Show(Card card)
	{
		this.m_card = card;
		this.ShowImpl();
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x000CFB4D File Offset: 0x000CDD4D
	public void Hide()
	{
		this.m_card = null;
		this.HideImpl();
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x000CFB5C File Offset: 0x000CDD5C
	public void Hide(Card card)
	{
		if (this.m_card == card)
		{
			this.Hide();
		}
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x000CFB72 File Offset: 0x000CDD72
	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		Card card = this.m_card;
		if (card == null)
		{
			return null;
		}
		return card.ShareDisposableCardDef();
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x000CFB88 File Offset: 0x000CDD88
	private void ShowImpl()
	{
		this.m_root.gameObject.SetActive(true);
		TAG_CARDTYPE cardType = this.m_card.GetEntity().GetCardType();
		this.m_text.gameObject.SetActive(true);
		this.m_text.Text = GameStrings.GetCardTypeName(cardType);
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			this.m_text.TextColor = this.MINION_COLOR;
			this.m_minionBanner.SetActive(true);
			break;
		case TAG_CARDTYPE.SPELL:
			this.m_text.TextColor = this.SPELL_COLOR;
			this.m_spellBanner.SetActive(true);
			break;
		case TAG_CARDTYPE.WEAPON:
			this.m_text.TextColor = this.WEAPON_COLOR;
			this.m_weaponBanner.SetActive(true);
			break;
		}
		this.UpdatePosition();
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x000CFC54 File Offset: 0x000CDE54
	private void HideImpl()
	{
		this.m_root.gameObject.SetActive(false);
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x000CFC68 File Offset: 0x000CDE68
	private void UpdatePosition()
	{
		GameObject cardTypeBannerAnchor = this.m_card.GetActor().GetCardTypeBannerAnchor();
		this.m_root.transform.position = cardTypeBannerAnchor.transform.position;
	}

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x060028FB RID: 10491 RVA: 0x000CFCA1 File Offset: 0x000CDEA1
	public bool HasCardDef
	{
		get
		{
			return this.m_card != null && this.m_card.HasCardDef;
		}
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x000CFCBE File Offset: 0x000CDEBE
	public bool HasSameCardDef(CardDef cardDef)
	{
		return this.m_card != null && this.m_card.HasSameCardDef(cardDef);
	}

	// Token: 0x04001753 RID: 5971
	public GameObject m_root;

	// Token: 0x04001754 RID: 5972
	public UberText m_text;

	// Token: 0x04001755 RID: 5973
	public GameObject m_spellBanner;

	// Token: 0x04001756 RID: 5974
	public GameObject m_minionBanner;

	// Token: 0x04001757 RID: 5975
	public GameObject m_weaponBanner;

	// Token: 0x04001758 RID: 5976
	private static CardTypeBanner s_instance;

	// Token: 0x04001759 RID: 5977
	private Card m_card;

	// Token: 0x0400175A RID: 5978
	private readonly Color MINION_COLOR = new Color(0.15294118f, 0.1254902f, 0.03529412f);

	// Token: 0x0400175B RID: 5979
	private readonly Color SPELL_COLOR = new Color(0.8745098f, 0.7882353f, 0.5254902f);

	// Token: 0x0400175C RID: 5980
	private readonly Color WEAPON_COLOR = new Color(0.8745098f, 0.7882353f, 0.5254902f);
}
