using System;
using System.Collections;
using UnityEngine;

public class CollectionDeckTileActor : Actor
{
	[Serializable]
	public class DeckTileFrameColorSet
	{
		public Color m_desatColor = Color.white;

		public float m_desatContrast;

		public float m_desatAmount;

		public Color m_costTextColor = Color.white;

		public Color m_countTextColor = new Color(1f, 0.9f, 0f, 1f);

		public Color m_nameTextColor = Color.white;

		public Color m_sliderColor = new Color(0.62f, 0.62f, 0.62f, 1f);

		public Color m_outlineColor = Color.black;

		public Material m_highlightMaterial;

		public Material m_highlightGlowMaterial;
	}

	public enum GhostedState
	{
		NONE,
		BLUE,
		RED
	}

	public enum TileIconState
	{
		CARD_COUNT,
		UNIQUE_STAR,
		MULTI_CARD
	}

	public Material m_standardFrameMaterial;

	public Material m_halfPremiumFrameMaterial;

	public Material m_premiumFrameMaterial;

	public Material m_standardFrameInteriorMaterial;

	public Material m_diamondFrameMaterial;

	public GameObject m_frame;

	public GameObject m_frameInterior;

	public GameObject m_uniqueStar;

	public GameObject m_multiCardIcon;

	public GameObject m_highlight;

	public GameObject m_highlightGlow;

	public UberText m_countText;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_ghostedFrameMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_redFrameMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public MeshRenderer m_manaGem;

	[CustomEditField(Sections = "Ghosting Effect")]
	public MeshRenderer m_slider;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemNormalMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemGhostedMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemRedGhostedMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_redFrameInteriorMaterial;

	[CustomEditField(Sections = "Ghosting Effect")]
	public DeckTileFrameColorSet m_normalColorSet = new DeckTileFrameColorSet();

	[CustomEditField(Sections = "Ghosting Effect")]
	public DeckTileFrameColorSet m_ghostedColorSet = new DeckTileFrameColorSet();

	[CustomEditField(Sections = "Ghosting Effect")]
	public DeckTileFrameColorSet m_redColorSet = new DeckTileFrameColorSet();

	private const float SLIDER_ANIM_TIME = 0.35f;

	private UberText m_countTextMesh;

	private bool m_sliderIsOpen;

	private Vector3 m_originalSliderLocalPos;

	private Vector3 m_openSliderLocalPos;

	private GhostedState m_ghosted;

	private CollectionDeckSlot m_slot;

	public override void Awake()
	{
		base.Awake();
		AssignSlider();
		AssignCardCount();
	}

	public static TileIconState GetCorrectTileIconState(bool isUnique, bool isMulticard)
	{
		if (isMulticard)
		{
			return TileIconState.MULTI_CARD;
		}
		if (isUnique)
		{
			return TileIconState.UNIQUE_STAR;
		}
		return TileIconState.CARD_COUNT;
	}

	public void UpdateDeckCardProperties(bool isUnique, bool isMultiCard, int numCards, bool useSliderAnimations)
	{
		TileIconState correctTileIconState = GetCorrectTileIconState(isUnique, isMultiCard);
		UpdateDeckCardProperties(correctTileIconState, numCards, useSliderAnimations);
	}

	public void UpdateDeckCardProperties(TileIconState iconState, int numCards, bool useSliderAnimations)
	{
		switch (iconState)
		{
		case TileIconState.MULTI_CARD:
			m_uniqueStar.SetActive(value: false);
			m_countTextMesh.gameObject.SetActive(value: false);
			m_multiCardIcon.SetActive(m_shown);
			break;
		case TileIconState.UNIQUE_STAR:
			m_uniqueStar.SetActive(m_shown);
			m_countTextMesh.gameObject.SetActive(value: false);
			m_multiCardIcon.SetActive(value: false);
			break;
		case TileIconState.CARD_COUNT:
			m_uniqueStar.SetActive(value: false);
			m_countTextMesh.gameObject.SetActive(m_shown);
			m_multiCardIcon.SetActive(value: false);
			m_countTextMesh.Text = Convert.ToString(numCards);
			break;
		}
		if (iconState == TileIconState.UNIQUE_STAR || numCards > 1)
		{
			OpenSlider(useSliderAnimations);
		}
		else
		{
			CloseSlider(useSliderAnimations);
		}
	}

	public void UpdateMaterial(Material material)
	{
		if (material == null)
		{
			Debug.LogErrorFormat("Null portrait material specified for {0}", GetEntityDef().GetCardId());
			Material material2 = m_portraitMesh.GetComponent<MeshRenderer>().GetMaterial();
			material2.SetFloat("_OffsetX", 0f);
			material2.SetFloat("_OffsetY", 0f);
		}
		else
		{
			m_portraitMesh.GetComponent<MeshRenderer>().SetMaterial(material);
		}
	}

	public void SetGhosted(GhostedState state)
	{
		m_ghosted = state;
	}

	public override void SetPremium(TAG_PREMIUM premium)
	{
		base.SetPremium(premium);
		UpdateFrameMaterial();
	}

	public CollectionDeckSlot GetSlot()
	{
		return m_slot;
	}

	public void SetSlot(CollectionDeckSlot slot)
	{
		m_slot = slot;
	}

	public void UpdateGhostTileEffect()
	{
		if (!(m_manaGem == null))
		{
			UpdateFrameMaterial();
			DeckTileFrameColorSet deckTileFrameColorSet = null;
			Material material = null;
			if (m_ghosted == GhostedState.NONE)
			{
				deckTileFrameColorSet = m_normalColorSet;
				material = m_manaGemNormalMaterial;
			}
			else if (m_ghosted == GhostedState.BLUE)
			{
				deckTileFrameColorSet = m_ghostedColorSet;
				material = m_manaGemGhostedMaterial;
			}
			else
			{
				deckTileFrameColorSet = m_redColorSet;
				material = m_manaGemRedGhostedMaterial;
			}
			m_manaGem.SetMaterial(material);
			m_countText.TextColor = deckTileFrameColorSet.m_countTextColor;
			m_nameTextMesh.TextColor = deckTileFrameColorSet.m_nameTextColor;
			m_costTextMesh.TextColor = deckTileFrameColorSet.m_costTextColor;
			if (m_countText.Outline)
			{
				m_countText.OutlineColor = deckTileFrameColorSet.m_outlineColor;
			}
			if (m_nameTextMesh.Outline)
			{
				m_nameTextMesh.OutlineColor = deckTileFrameColorSet.m_outlineColor;
			}
			if (m_costTextMesh.Outline)
			{
				m_costTextMesh.OutlineColor = deckTileFrameColorSet.m_outlineColor;
			}
			if ((bool)m_highlight && (bool)deckTileFrameColorSet.m_highlightMaterial)
			{
				m_highlight.GetComponent<Renderer>().SetMaterial(deckTileFrameColorSet.m_highlightMaterial);
			}
			if ((bool)m_highlightGlow && (bool)deckTileFrameColorSet.m_highlightGlowMaterial)
			{
				m_highlightGlow.GetComponent<Renderer>().SetMaterial(deckTileFrameColorSet.m_highlightGlowMaterial);
			}
			SetDesaturationAmount(GetPortraitMaterial(), deckTileFrameColorSet);
			SetDesaturationAmount(m_uniqueStar.GetComponent<MeshRenderer>().GetMaterial(), deckTileFrameColorSet);
			SetDesaturationAmount(m_multiCardIcon.GetComponent<MeshRenderer>().GetMaterial(), deckTileFrameColorSet);
		}
	}

	protected override bool IsPremiumPortraitEnabled()
	{
		return false;
	}

	private void SetDesaturationAmount(Material material, DeckTileFrameColorSet colorSet)
	{
		material.SetColor("_Color", colorSet.m_desatColor);
		material.SetFloat("_Desaturate", colorSet.m_desatAmount);
		material.SetFloat("_Contrast", colorSet.m_desatContrast);
	}

	private void UpdateFrameMaterial()
	{
		Material material = null;
		Material material2 = m_standardFrameInteriorMaterial;
		if (m_ghosted == GhostedState.BLUE)
		{
			material = m_ghostedFrameMaterial;
		}
		else if (m_ghosted == GhostedState.RED)
		{
			material = m_redFrameMaterial;
			material2 = m_redFrameInteriorMaterial;
		}
		else
		{
			material = m_standardFrameMaterial;
			if (m_slot != null)
			{
				int count = m_slot.GetCount(TAG_PREMIUM.NORMAL);
				int count2 = m_slot.GetCount(TAG_PREMIUM.GOLDEN);
				int count3 = m_slot.GetCount(TAG_PREMIUM.DIAMOND);
				if (count > 0 && count2 > 0)
				{
					material = m_halfPremiumFrameMaterial;
				}
				else if (count2 > 0 && count <= 0)
				{
					material = m_premiumFrameMaterial;
				}
				else if (count3 > 0)
				{
					material = m_diamondFrameMaterial;
				}
			}
			else if (m_premiumType == TAG_PREMIUM.GOLDEN)
			{
				material = m_premiumFrameMaterial;
			}
			else if (m_premiumType == TAG_PREMIUM.DIAMOND)
			{
				material = m_diamondFrameMaterial;
			}
		}
		if (material != null)
		{
			m_frame.GetComponent<Renderer>().SetMaterial(material);
		}
		if (material2 != null)
		{
			m_frameInterior.GetComponent<Renderer>().SetMaterial(material2);
		}
	}

	private void AssignSlider()
	{
		m_originalSliderLocalPos = m_slider.transform.localPosition;
		m_openSliderLocalPos = m_rootObject.transform.Find("OpenSliderPosition").transform.localPosition;
	}

	private void AssignCardCount()
	{
		m_countTextMesh = m_rootObject.transform.Find("CardCountText").GetComponent<UberText>();
	}

	private void OpenSlider(bool useSliderAnimations)
	{
		if (!m_sliderIsOpen)
		{
			m_sliderIsOpen = true;
			iTween.StopByName(m_slider.gameObject, "position");
			if (useSliderAnimations)
			{
				Hashtable args = iTween.Hash("position", m_openSliderLocalPos, "isLocal", true, "time", 0.35f, "easetype", iTween.EaseType.easeOutBounce, "name", "position");
				iTween.MoveTo(m_slider.gameObject, args);
			}
			else
			{
				m_slider.transform.localPosition = m_openSliderLocalPos;
			}
		}
	}

	private void CloseSlider(bool useSliderAnimations)
	{
		if (m_sliderIsOpen)
		{
			m_sliderIsOpen = false;
			iTween.StopByName(m_slider.gameObject, "position");
			if (useSliderAnimations)
			{
				Hashtable args = iTween.Hash("position", m_originalSliderLocalPos, "isLocal", true, "time", 0.35f, "easetype", iTween.EaseType.easeOutBounce, "name", "position");
				iTween.MoveTo(m_slider.gameObject, args);
			}
			else
			{
				m_slider.transform.localPosition = m_originalSliderLocalPos;
			}
		}
	}
}
