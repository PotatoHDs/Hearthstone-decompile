using UnityEngine;

public class BigCardEnchantmentPanel : MonoBehaviour
{
	public Actor m_Actor;

	public UberText m_HeaderText;

	public UberText m_BodyText;

	public GameObject m_Background;

	public Material m_FallbackEnchantmentPortrait;

	private Entity m_enchantment;

	private DefLoader.DisposableCardDef m_enchantmentCardDef;

	private DefLoader.DisposableCardDef m_creatorCardDef;

	private Vector3 m_initialScale;

	private float m_initialBackgroundHeight;

	private Vector3 m_initialBackgroundScale;

	private bool m_shown;

	private int m_multiplier = 1;

	private string m_header = "";

	private void Awake()
	{
		m_initialScale = base.transform.localScale;
		m_initialBackgroundHeight = m_Background.GetComponentInChildren<MeshRenderer>().bounds.size.z;
		m_initialBackgroundScale = m_Background.transform.localScale;
	}

	private void OnDestroy()
	{
		m_enchantmentCardDef?.Dispose();
		m_enchantmentCardDef = null;
		m_creatorCardDef?.Dispose();
		m_creatorCardDef = null;
	}

	public void SetEnchantment(Entity enchantment)
	{
		m_enchantment = enchantment;
		string cardId = m_enchantment.GetCardId();
		DefLoader.Get().LoadCardDef(cardId, OnEnchantmentCardDefLoaded, null, new CardPortraitQuality(1, m_enchantment.GetPremiumType()));
	}

	public void Show()
	{
		if (!m_shown)
		{
			m_shown = true;
			base.gameObject.SetActive(value: true);
			UpdateLayout();
		}
	}

	public void Hide()
	{
		if (m_shown)
		{
			m_shown = false;
			base.gameObject.SetActive(value: false);
		}
	}

	public void ResetScale()
	{
		base.transform.localScale = m_initialScale;
		m_Background.transform.localScale = m_initialBackgroundScale;
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public float GetHeight()
	{
		return m_Background.GetComponentInChildren<MeshRenderer>().bounds.size.z;
	}

	private void OnEnchantmentCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		bool flag = false;
		if (cardDef != null)
		{
			m_enchantmentCardDef?.Dispose();
			m_enchantmentCardDef = cardDef;
			if (m_enchantmentCardDef.CardDef.GetEnchantmentPortrait() != null)
			{
				m_Actor.GetMeshRenderer().SetMaterial(m_enchantmentCardDef.CardDef.GetEnchantmentPortrait());
				flag = true;
			}
			else if (m_enchantmentCardDef.CardDef.GetHistoryTileFullPortrait() != null)
			{
				m_Actor.GetMeshRenderer().SetMaterial(m_enchantmentCardDef.CardDef.GetHistoryTileFullPortrait());
				flag = true;
			}
			else if (m_enchantmentCardDef.CardDef.GetPortraitTexture() != null)
			{
				m_Actor.SetPortraitTextureOverride(m_enchantmentCardDef.CardDef.GetPortraitTexture());
				flag = true;
			}
		}
		m_HeaderText.Text = m_enchantment.GetName();
		m_header = m_enchantment.GetName();
		SetMultiplier(Mathf.Max(m_enchantment.GetTag(GAME_TAG.SPAWN_TIME_COUNT), 1));
		m_BodyText.Text = m_enchantment.GetCardTextInHand();
		if (!flag)
		{
			LoadCreatorCardDef();
		}
	}

	private void LoadCreatorCardDef()
	{
		if (m_enchantment != null)
		{
			string enchantmentCreatorCardIDForPortrait = m_enchantment.GetEnchantmentCreatorCardIDForPortrait();
			if (string.IsNullOrEmpty(enchantmentCreatorCardIDForPortrait))
			{
				m_Actor.GetMeshRenderer().SetMaterial(m_FallbackEnchantmentPortrait);
			}
			else
			{
				DefLoader.Get().LoadCardDef(enchantmentCreatorCardIDForPortrait, OnCreatorCardDefLoaded, null, new CardPortraitQuality(1, m_enchantment.GetPremiumType()));
			}
		}
	}

	private void OnCreatorCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		if (cardDef != null)
		{
			m_creatorCardDef?.Dispose();
			m_creatorCardDef = cardDef;
			if (m_creatorCardDef.CardDef.GetEnchantmentPortrait() != null)
			{
				m_Actor.GetMeshRenderer().SetMaterial(m_creatorCardDef.CardDef.GetEnchantmentPortrait());
			}
			else if (m_creatorCardDef.CardDef.GetHistoryTileFullPortrait() != null)
			{
				m_Actor.GetMeshRenderer().SetMaterial(m_creatorCardDef.CardDef.GetHistoryTileFullPortrait());
			}
			else if (m_creatorCardDef.CardDef.GetPortraitTexture() != null)
			{
				m_Actor.SetPortraitTextureOverride(m_creatorCardDef.CardDef.GetPortraitTexture());
			}
		}
	}

	private void UpdateLayout()
	{
		m_HeaderText.UpdateNow();
		m_BodyText.UpdateNow();
		Bounds bounds = m_Actor.GetMeshRenderer().bounds;
		Bounds textWorldSpaceBounds = m_HeaderText.GetTextWorldSpaceBounds();
		Bounds textWorldSpaceBounds2 = m_BodyText.GetTextWorldSpaceBounds();
		float z = bounds.min.z;
		float z2 = bounds.max.z;
		float z3 = textWorldSpaceBounds.min.z;
		float z4 = textWorldSpaceBounds.max.z;
		float z5 = textWorldSpaceBounds2.min.z;
		float z6 = textWorldSpaceBounds2.max.z;
		float num = Mathf.Min(Mathf.Min(z, z3), z5);
		float num2 = Mathf.Max(Mathf.Max(z2, z4), z6) - num + 0.1f;
		base.transform.localScale = m_initialScale;
		base.transform.localEulerAngles = Vector3.zero;
		TransformUtil.SetLocalScaleZ(m_Background, m_initialBackgroundScale.z * (num2 / m_initialBackgroundHeight));
	}

	public string GetEnchantmentId()
	{
		if (m_enchantment == null)
		{
			return null;
		}
		return m_enchantment.GetCardId();
	}

	public void IncrementEnchantmentMultiplier(uint amount = 1u)
	{
		SetMultiplier(m_multiplier + (int)amount);
	}

	public void SetMultiplier(int multiplier)
	{
		m_multiplier = multiplier;
		if (m_multiplier > 1)
		{
			m_HeaderText.Text = GameStrings.Format("GAMEPLAY_ENCHANTMENT_MULTIPLIER_HEADER", m_multiplier, m_header);
		}
		else
		{
			m_HeaderText.Text = m_header;
		}
	}
}
