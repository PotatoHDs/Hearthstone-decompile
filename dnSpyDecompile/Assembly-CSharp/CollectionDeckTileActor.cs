using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class CollectionDeckTileActor : Actor
{
	// Token: 0x06000FD3 RID: 4051 RVA: 0x000584E6 File Offset: 0x000566E6
	public override void Awake()
	{
		base.Awake();
		this.AssignSlider();
		this.AssignCardCount();
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000584FA File Offset: 0x000566FA
	public static CollectionDeckTileActor.TileIconState GetCorrectTileIconState(bool isUnique, bool isMulticard)
	{
		if (isMulticard)
		{
			return CollectionDeckTileActor.TileIconState.MULTI_CARD;
		}
		if (isUnique)
		{
			return CollectionDeckTileActor.TileIconState.UNIQUE_STAR;
		}
		return CollectionDeckTileActor.TileIconState.CARD_COUNT;
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00058508 File Offset: 0x00056708
	public void UpdateDeckCardProperties(bool isUnique, bool isMultiCard, int numCards, bool useSliderAnimations)
	{
		CollectionDeckTileActor.TileIconState correctTileIconState = CollectionDeckTileActor.GetCorrectTileIconState(isUnique, isMultiCard);
		this.UpdateDeckCardProperties(correctTileIconState, numCards, useSliderAnimations);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00058528 File Offset: 0x00056728
	public void UpdateDeckCardProperties(CollectionDeckTileActor.TileIconState iconState, int numCards, bool useSliderAnimations)
	{
		switch (iconState)
		{
		case CollectionDeckTileActor.TileIconState.CARD_COUNT:
			this.m_uniqueStar.SetActive(false);
			this.m_countTextMesh.gameObject.SetActive(this.m_shown);
			this.m_multiCardIcon.SetActive(false);
			this.m_countTextMesh.Text = Convert.ToString(numCards);
			break;
		case CollectionDeckTileActor.TileIconState.UNIQUE_STAR:
			this.m_uniqueStar.SetActive(this.m_shown);
			this.m_countTextMesh.gameObject.SetActive(false);
			this.m_multiCardIcon.SetActive(false);
			break;
		case CollectionDeckTileActor.TileIconState.MULTI_CARD:
			this.m_uniqueStar.SetActive(false);
			this.m_countTextMesh.gameObject.SetActive(false);
			this.m_multiCardIcon.SetActive(this.m_shown);
			break;
		}
		if (iconState == CollectionDeckTileActor.TileIconState.UNIQUE_STAR || numCards > 1)
		{
			this.OpenSlider(useSliderAnimations);
			return;
		}
		this.CloseSlider(useSliderAnimations);
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x0005860C File Offset: 0x0005680C
	public void UpdateMaterial(Material material)
	{
		if (material == null)
		{
			Debug.LogErrorFormat("Null portrait material specified for {0}", new object[]
			{
				base.GetEntityDef().GetCardId()
			});
			Material material2 = this.m_portraitMesh.GetComponent<MeshRenderer>().GetMaterial();
			material2.SetFloat("_OffsetX", 0f);
			material2.SetFloat("_OffsetY", 0f);
			return;
		}
		this.m_portraitMesh.GetComponent<MeshRenderer>().SetMaterial(material);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00058681 File Offset: 0x00056881
	public void SetGhosted(CollectionDeckTileActor.GhostedState state)
	{
		this.m_ghosted = state;
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0005868A File Offset: 0x0005688A
	public override void SetPremium(TAG_PREMIUM premium)
	{
		base.SetPremium(premium);
		this.UpdateFrameMaterial();
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00058699 File Offset: 0x00056899
	public CollectionDeckSlot GetSlot()
	{
		return this.m_slot;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x000586A1 File Offset: 0x000568A1
	public void SetSlot(CollectionDeckSlot slot)
	{
		this.m_slot = slot;
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x000586AC File Offset: 0x000568AC
	public void UpdateGhostTileEffect()
	{
		if (this.m_manaGem == null)
		{
			return;
		}
		this.UpdateFrameMaterial();
		CollectionDeckTileActor.DeckTileFrameColorSet deckTileFrameColorSet;
		Material material;
		if (this.m_ghosted == CollectionDeckTileActor.GhostedState.NONE)
		{
			deckTileFrameColorSet = this.m_normalColorSet;
			material = this.m_manaGemNormalMaterial;
		}
		else if (this.m_ghosted == CollectionDeckTileActor.GhostedState.BLUE)
		{
			deckTileFrameColorSet = this.m_ghostedColorSet;
			material = this.m_manaGemGhostedMaterial;
		}
		else
		{
			deckTileFrameColorSet = this.m_redColorSet;
			material = this.m_manaGemRedGhostedMaterial;
		}
		this.m_manaGem.SetMaterial(material);
		this.m_countText.TextColor = deckTileFrameColorSet.m_countTextColor;
		this.m_nameTextMesh.TextColor = deckTileFrameColorSet.m_nameTextColor;
		this.m_costTextMesh.TextColor = deckTileFrameColorSet.m_costTextColor;
		if (this.m_countText.Outline)
		{
			this.m_countText.OutlineColor = deckTileFrameColorSet.m_outlineColor;
		}
		if (this.m_nameTextMesh.Outline)
		{
			this.m_nameTextMesh.OutlineColor = deckTileFrameColorSet.m_outlineColor;
		}
		if (this.m_costTextMesh.Outline)
		{
			this.m_costTextMesh.OutlineColor = deckTileFrameColorSet.m_outlineColor;
		}
		if (this.m_highlight && deckTileFrameColorSet.m_highlightMaterial)
		{
			this.m_highlight.GetComponent<Renderer>().SetMaterial(deckTileFrameColorSet.m_highlightMaterial);
		}
		if (this.m_highlightGlow && deckTileFrameColorSet.m_highlightGlowMaterial)
		{
			this.m_highlightGlow.GetComponent<Renderer>().SetMaterial(deckTileFrameColorSet.m_highlightGlowMaterial);
		}
		this.SetDesaturationAmount(this.GetPortraitMaterial(), deckTileFrameColorSet);
		this.SetDesaturationAmount(this.m_uniqueStar.GetComponent<MeshRenderer>().GetMaterial(), deckTileFrameColorSet);
		this.SetDesaturationAmount(this.m_multiCardIcon.GetComponent<MeshRenderer>().GetMaterial(), deckTileFrameColorSet);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool IsPremiumPortraitEnabled()
	{
		return false;
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00058845 File Offset: 0x00056A45
	private void SetDesaturationAmount(Material material, CollectionDeckTileActor.DeckTileFrameColorSet colorSet)
	{
		material.SetColor("_Color", colorSet.m_desatColor);
		material.SetFloat("_Desaturate", colorSet.m_desatAmount);
		material.SetFloat("_Contrast", colorSet.m_desatContrast);
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0005887C File Offset: 0x00056A7C
	private void UpdateFrameMaterial()
	{
		Material material = this.m_standardFrameInteriorMaterial;
		Material material2;
		if (this.m_ghosted == CollectionDeckTileActor.GhostedState.BLUE)
		{
			material2 = this.m_ghostedFrameMaterial;
		}
		else if (this.m_ghosted == CollectionDeckTileActor.GhostedState.RED)
		{
			material2 = this.m_redFrameMaterial;
			material = this.m_redFrameInteriorMaterial;
		}
		else
		{
			material2 = this.m_standardFrameMaterial;
			if (this.m_slot != null)
			{
				int count = this.m_slot.GetCount(TAG_PREMIUM.NORMAL);
				int count2 = this.m_slot.GetCount(TAG_PREMIUM.GOLDEN);
				int count3 = this.m_slot.GetCount(TAG_PREMIUM.DIAMOND);
				if (count > 0 && count2 > 0)
				{
					material2 = this.m_halfPremiumFrameMaterial;
				}
				else if (count2 > 0 && count <= 0)
				{
					material2 = this.m_premiumFrameMaterial;
				}
				else if (count3 > 0)
				{
					material2 = this.m_diamondFrameMaterial;
				}
			}
			else if (this.m_premiumType == TAG_PREMIUM.GOLDEN)
			{
				material2 = this.m_premiumFrameMaterial;
			}
			else if (this.m_premiumType == TAG_PREMIUM.DIAMOND)
			{
				material2 = this.m_diamondFrameMaterial;
			}
		}
		if (material2 != null)
		{
			this.m_frame.GetComponent<Renderer>().SetMaterial(material2);
		}
		if (material != null)
		{
			this.m_frameInterior.GetComponent<Renderer>().SetMaterial(material);
		}
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00058980 File Offset: 0x00056B80
	private void AssignSlider()
	{
		this.m_originalSliderLocalPos = this.m_slider.transform.localPosition;
		this.m_openSliderLocalPos = this.m_rootObject.transform.Find("OpenSliderPosition").transform.localPosition;
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000589BD File Offset: 0x00056BBD
	private void AssignCardCount()
	{
		this.m_countTextMesh = this.m_rootObject.transform.Find("CardCountText").GetComponent<UberText>();
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000589E0 File Offset: 0x00056BE0
	private void OpenSlider(bool useSliderAnimations)
	{
		if (this.m_sliderIsOpen)
		{
			return;
		}
		this.m_sliderIsOpen = true;
		iTween.StopByName(this.m_slider.gameObject, "position");
		if (useSliderAnimations)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_openSliderLocalPos,
				"isLocal",
				true,
				"time",
				0.35f,
				"easetype",
				iTween.EaseType.easeOutBounce,
				"name",
				"position"
			});
			iTween.MoveTo(this.m_slider.gameObject, args);
			return;
		}
		this.m_slider.transform.localPosition = this.m_openSliderLocalPos;
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00058AAC File Offset: 0x00056CAC
	private void CloseSlider(bool useSliderAnimations)
	{
		if (!this.m_sliderIsOpen)
		{
			return;
		}
		this.m_sliderIsOpen = false;
		iTween.StopByName(this.m_slider.gameObject, "position");
		if (useSliderAnimations)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_originalSliderLocalPos,
				"isLocal",
				true,
				"time",
				0.35f,
				"easetype",
				iTween.EaseType.easeOutBounce,
				"name",
				"position"
			});
			iTween.MoveTo(this.m_slider.gameObject, args);
			return;
		}
		this.m_slider.transform.localPosition = this.m_originalSliderLocalPos;
	}

	// Token: 0x04000AAB RID: 2731
	public Material m_standardFrameMaterial;

	// Token: 0x04000AAC RID: 2732
	public Material m_halfPremiumFrameMaterial;

	// Token: 0x04000AAD RID: 2733
	public Material m_premiumFrameMaterial;

	// Token: 0x04000AAE RID: 2734
	public Material m_standardFrameInteriorMaterial;

	// Token: 0x04000AAF RID: 2735
	public Material m_diamondFrameMaterial;

	// Token: 0x04000AB0 RID: 2736
	public GameObject m_frame;

	// Token: 0x04000AB1 RID: 2737
	public GameObject m_frameInterior;

	// Token: 0x04000AB2 RID: 2738
	public GameObject m_uniqueStar;

	// Token: 0x04000AB3 RID: 2739
	public GameObject m_multiCardIcon;

	// Token: 0x04000AB4 RID: 2740
	public GameObject m_highlight;

	// Token: 0x04000AB5 RID: 2741
	public GameObject m_highlightGlow;

	// Token: 0x04000AB6 RID: 2742
	public UberText m_countText;

	// Token: 0x04000AB7 RID: 2743
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_ghostedFrameMaterial;

	// Token: 0x04000AB8 RID: 2744
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_redFrameMaterial;

	// Token: 0x04000AB9 RID: 2745
	[CustomEditField(Sections = "Ghosting Effect")]
	public MeshRenderer m_manaGem;

	// Token: 0x04000ABA RID: 2746
	[CustomEditField(Sections = "Ghosting Effect")]
	public MeshRenderer m_slider;

	// Token: 0x04000ABB RID: 2747
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemNormalMaterial;

	// Token: 0x04000ABC RID: 2748
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemGhostedMaterial;

	// Token: 0x04000ABD RID: 2749
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_manaGemRedGhostedMaterial;

	// Token: 0x04000ABE RID: 2750
	[CustomEditField(Sections = "Ghosting Effect")]
	public Material m_redFrameInteriorMaterial;

	// Token: 0x04000ABF RID: 2751
	[CustomEditField(Sections = "Ghosting Effect")]
	public CollectionDeckTileActor.DeckTileFrameColorSet m_normalColorSet = new CollectionDeckTileActor.DeckTileFrameColorSet();

	// Token: 0x04000AC0 RID: 2752
	[CustomEditField(Sections = "Ghosting Effect")]
	public CollectionDeckTileActor.DeckTileFrameColorSet m_ghostedColorSet = new CollectionDeckTileActor.DeckTileFrameColorSet();

	// Token: 0x04000AC1 RID: 2753
	[CustomEditField(Sections = "Ghosting Effect")]
	public CollectionDeckTileActor.DeckTileFrameColorSet m_redColorSet = new CollectionDeckTileActor.DeckTileFrameColorSet();

	// Token: 0x04000AC2 RID: 2754
	private const float SLIDER_ANIM_TIME = 0.35f;

	// Token: 0x04000AC3 RID: 2755
	private UberText m_countTextMesh;

	// Token: 0x04000AC4 RID: 2756
	private bool m_sliderIsOpen;

	// Token: 0x04000AC5 RID: 2757
	private Vector3 m_originalSliderLocalPos;

	// Token: 0x04000AC6 RID: 2758
	private Vector3 m_openSliderLocalPos;

	// Token: 0x04000AC7 RID: 2759
	private CollectionDeckTileActor.GhostedState m_ghosted;

	// Token: 0x04000AC8 RID: 2760
	private CollectionDeckSlot m_slot;

	// Token: 0x02001431 RID: 5169
	[Serializable]
	public class DeckTileFrameColorSet
	{
		// Token: 0x0400A936 RID: 43318
		public Color m_desatColor = Color.white;

		// Token: 0x0400A937 RID: 43319
		public float m_desatContrast;

		// Token: 0x0400A938 RID: 43320
		public float m_desatAmount;

		// Token: 0x0400A939 RID: 43321
		public Color m_costTextColor = Color.white;

		// Token: 0x0400A93A RID: 43322
		public Color m_countTextColor = new Color(1f, 0.9f, 0f, 1f);

		// Token: 0x0400A93B RID: 43323
		public Color m_nameTextColor = Color.white;

		// Token: 0x0400A93C RID: 43324
		public Color m_sliderColor = new Color(0.62f, 0.62f, 0.62f, 1f);

		// Token: 0x0400A93D RID: 43325
		public Color m_outlineColor = Color.black;

		// Token: 0x0400A93E RID: 43326
		public Material m_highlightMaterial;

		// Token: 0x0400A93F RID: 43327
		public Material m_highlightGlowMaterial;
	}

	// Token: 0x02001432 RID: 5170
	public enum GhostedState
	{
		// Token: 0x0400A941 RID: 43329
		NONE,
		// Token: 0x0400A942 RID: 43330
		BLUE,
		// Token: 0x0400A943 RID: 43331
		RED
	}

	// Token: 0x02001433 RID: 5171
	public enum TileIconState
	{
		// Token: 0x0400A945 RID: 43333
		CARD_COUNT,
		// Token: 0x0400A946 RID: 43334
		UNIQUE_STAR,
		// Token: 0x0400A947 RID: 43335
		MULTI_CARD
	}
}
