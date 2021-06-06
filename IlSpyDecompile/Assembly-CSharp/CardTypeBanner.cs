using UnityEngine;

public class CardTypeBanner : MonoBehaviour
{
	public GameObject m_root;

	public UberText m_text;

	public GameObject m_spellBanner;

	public GameObject m_minionBanner;

	public GameObject m_weaponBanner;

	private static CardTypeBanner s_instance;

	private Card m_card;

	private readonly Color MINION_COLOR = new Color(13f / 85f, 32f / 255f, 3f / 85f);

	private readonly Color SPELL_COLOR = new Color(223f / 255f, 67f / 85f, 134f / 255f);

	private readonly Color WEAPON_COLOR = new Color(223f / 255f, 67f / 85f, 134f / 255f);

	public bool HasCardDef
	{
		get
		{
			if (!(m_card != null))
			{
				return false;
			}
			return m_card.HasCardDef;
		}
	}

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Update()
	{
		if (m_card != null)
		{
			if (m_card.GetActor().IsShown())
			{
				UpdatePosition();
			}
			else
			{
				Hide();
			}
		}
	}

	public static CardTypeBanner Get()
	{
		return s_instance;
	}

	public bool IsShown()
	{
		return m_card;
	}

	public void Show(Card card)
	{
		m_card = card;
		ShowImpl();
	}

	public void Hide()
	{
		m_card = null;
		HideImpl();
	}

	public void Hide(Card card)
	{
		if (m_card == card)
		{
			Hide();
		}
	}

	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		return m_card?.ShareDisposableCardDef();
	}

	private void ShowImpl()
	{
		m_root.gameObject.SetActive(value: true);
		TAG_CARDTYPE cardType = m_card.GetEntity().GetCardType();
		m_text.gameObject.SetActive(value: true);
		m_text.Text = GameStrings.GetCardTypeName(cardType);
		switch (cardType)
		{
		case TAG_CARDTYPE.SPELL:
			m_text.TextColor = SPELL_COLOR;
			m_spellBanner.SetActive(value: true);
			break;
		case TAG_CARDTYPE.MINION:
			m_text.TextColor = MINION_COLOR;
			m_minionBanner.SetActive(value: true);
			break;
		case TAG_CARDTYPE.WEAPON:
			m_text.TextColor = WEAPON_COLOR;
			m_weaponBanner.SetActive(value: true);
			break;
		}
		UpdatePosition();
	}

	private void HideImpl()
	{
		m_root.gameObject.SetActive(value: false);
	}

	private void UpdatePosition()
	{
		GameObject cardTypeBannerAnchor = m_card.GetActor().GetCardTypeBannerAnchor();
		m_root.transform.position = cardTypeBannerAnchor.transform.position;
	}

	public bool HasSameCardDef(CardDef cardDef)
	{
		if (!(m_card != null))
		{
			return false;
		}
		return m_card.HasSameCardDef(cardDef);
	}
}
