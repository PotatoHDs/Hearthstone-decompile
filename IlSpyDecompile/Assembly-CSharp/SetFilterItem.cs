using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public class SetFilterItem : PegUIElement
{
	public delegate void ItemSelectedCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, SetFilterItem selectedItem, bool transitionPage);

	public UberText m_uberText;

	public GameObject m_selectedGlow;

	public MeshRenderer m_icon;

	public GameObject m_mouseOverGlow;

	public GameObject m_pressedShadow;

	public GameObject m_iconFX;

	public TooltipZone m_tooltipZone;

	private bool m_isHeader;

	private FormatType m_formatType;

	private bool m_isAllStandard;

	private List<TAG_CARD_SET> m_cardSets;

	private List<int> m_metaShakeupEvents;

	private float m_height;

	private ItemSelectedCallback m_callback;

	private bool m_isSelected;

	private string m_tooltipHeadline;

	private string m_tooltipDescription;

	private bool m_showTooltip;

	public bool IsHeader
	{
		get
		{
			return m_isHeader;
		}
		set
		{
			m_isHeader = value;
		}
	}

	public string Text
	{
		get
		{
			return m_uberText.Text;
		}
		set
		{
			m_uberText.Text = value;
		}
	}

	public FormatType FormatType
	{
		get
		{
			return m_formatType;
		}
		set
		{
			m_formatType = value;
		}
	}

	public bool IsAllStandard
	{
		get
		{
			return m_isAllStandard;
		}
		set
		{
			m_isAllStandard = value;
		}
	}

	public List<TAG_CARD_SET> CardSets
	{
		get
		{
			return m_cardSets;
		}
		set
		{
			m_cardSets = value;
		}
	}

	public List<int> SpecificCards
	{
		get
		{
			return m_metaShakeupEvents;
		}
		set
		{
			m_metaShakeupEvents = value;
		}
	}

	public float Height
	{
		get
		{
			return m_height;
		}
		set
		{
			m_height = value;
		}
	}

	public ItemSelectedCallback Callback
	{
		get
		{
			return m_callback;
		}
		set
		{
			m_callback = value;
		}
	}

	public string TooltipHeadline
	{
		get
		{
			return m_tooltipHeadline;
		}
		set
		{
			m_tooltipHeadline = value;
		}
	}

	public string TooltipDescription
	{
		get
		{
			return m_tooltipDescription;
		}
		set
		{
			m_tooltipDescription = value;
		}
	}

	public bool ShowTooltip
	{
		get
		{
			return m_showTooltip;
		}
		set
		{
			m_showTooltip = value;
		}
	}

	public Texture IconTexture
	{
		get
		{
			return m_icon.GetMaterial().GetTexture("_MainTex");
		}
		set
		{
			if (value == null)
			{
				m_icon.gameObject.SetActive(value: false);
			}
			else
			{
				m_icon.gameObject.SetActive(value: true);
			}
			m_icon.GetMaterial().SetTexture("_MainTex", value);
		}
	}

	public UnityEngine.Vector2? IconOffset
	{
		get
		{
			return m_icon.GetMaterial().GetTextureOffset("_MainTex");
		}
		set
		{
			if (!value.HasValue || IconTexture == null)
			{
				m_icon.gameObject.SetActive(value: false);
				return;
			}
			m_icon.gameObject.SetActive(IconTexture != null);
			m_icon.GetMaterial().SetTextureOffset("_MainTex", value.Value);
		}
	}

	public TooltipZone Tooltip
	{
		get
		{
			return m_tooltipZone;
		}
		private set
		{
			m_tooltipZone = value;
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (m_mouseOverGlow != null && !UniversalInputManager.Get().IsTouchMode())
		{
			m_mouseOverGlow.SetActive(value: true);
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9", base.gameObject);
		}
		if (m_tooltipZone != null && m_showTooltip)
		{
			m_tooltipZone.ShowBoxTooltip(m_tooltipHeadline, m_tooltipDescription);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (m_mouseOverGlow != null)
		{
			m_mouseOverGlow.SetActive(value: false);
		}
		if (!m_isSelected && m_pressedShadow != null)
		{
			m_pressedShadow.SetActive(value: false);
		}
		if (m_tooltipZone != null)
		{
			m_tooltipZone.HideTooltip();
		}
	}

	public void SetSelected(bool selected)
	{
		m_selectedGlow.SetActive(selected);
		if (m_pressedShadow != null)
		{
			m_pressedShadow.SetActive(selected);
		}
		m_isSelected = selected;
	}

	protected override void OnPress()
	{
		if (m_pressedShadow != null && !UniversalInputManager.Get().IsTouchMode())
		{
			m_pressedShadow.SetActive(value: true);
		}
	}

	protected override void OnRelease()
	{
		if (!m_isSelected && m_pressedShadow != null)
		{
			m_pressedShadow.SetActive(value: false);
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		}
	}

	public void SetIconFxActive(bool active)
	{
		if (!(m_iconFX == null))
		{
			m_iconFX.SetActive(active);
		}
	}
}
