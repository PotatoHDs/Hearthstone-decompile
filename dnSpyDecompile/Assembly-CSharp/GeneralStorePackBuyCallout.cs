using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020006FE RID: 1790
[CustomEditClass]
public class GeneralStorePackBuyCallout : MonoBehaviour
{
	// Token: 0x060063EF RID: 25583 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x060063F0 RID: 25584 RVA: 0x00208A7E File Offset: 0x00206C7E
	public void Init()
	{
		this.m_multiSlice = base.GetComponent<MultiSliceElement>();
		this.m_origScale = base.transform.localScale;
		this.m_initialized = true;
	}

	// Token: 0x060063F1 RID: 25585 RVA: 0x00208AA4 File Offset: 0x00206CA4
	public bool IsShown()
	{
		return this.m_isShown;
	}

	// Token: 0x060063F2 RID: 25586 RVA: 0x00208AAC File Offset: 0x00206CAC
	public void ShowCallout(GeneralStorePackBuyButton firstButton, GeneralStorePackBuyButton lastButton, int numButtons)
	{
		if (!this.m_initialized)
		{
			return;
		}
		if (this.m_isShown)
		{
			return;
		}
		if (firstButton == null || lastButton == null || numButtons <= 0)
		{
			return;
		}
		this.m_isShown = true;
		base.gameObject.SetActive(true);
		this.m_text.Text = GameStrings.Get("GLUE_STORE_LIMITED_TIME_OFFER");
		this.ToggleCalloutSections(numButtons);
		this.m_multiSlice.UpdateSlices();
		int indexToActivate = numButtons - 1;
		this.ToggleGameObjectActive(this.m_glowSizeVariations, indexToActivate);
		float x = firstButton.transform.position.x;
		float num = (lastButton.transform.position.x - x) / 2f;
		float x2 = x + num;
		TransformUtil.SetPosX(base.gameObject, x2);
		if (UniversalInputManager.UsePhoneUI)
		{
			if (numButtons <= this.m_phonePerspectiveOffsetX.Count)
			{
				float num2 = this.m_phonePerspectiveOffsetX[numButtons - 1];
				TransformUtil.SetLocalPosX(base.gameObject, base.transform.localPosition.x + num2);
			}
			if (numButtons <= this.m_phoneTextWidth.Count)
			{
				this.m_text.Width = (float)this.m_phoneTextWidth[numButtons - 1];
			}
		}
		this.AnimateIn();
		base.InvokeRepeating("AnimatePulse", 3f, 3f);
	}

	// Token: 0x060063F3 RID: 25587 RVA: 0x00208BF2 File Offset: 0x00206DF2
	public void HideCallout()
	{
		if (!this.m_isShown)
		{
			return;
		}
		base.CancelInvoke("AnimatePulse");
		this.AnimateOut();
	}

	// Token: 0x060063F4 RID: 25588 RVA: 0x00208C0E File Offset: 0x00206E0E
	public void DeactivateCallout()
	{
		base.CancelInvoke("AnimatePulse");
		base.gameObject.SetActive(false);
		this.m_isShown = false;
	}

	// Token: 0x060063F5 RID: 25589 RVA: 0x00208C30 File Offset: 0x00206E30
	private void ToggleCalloutSections(int numCalloutSectionsNeeded)
	{
		bool flag = numCalloutSectionsNeeded == 1;
		if (flag && UniversalInputManager.UsePhoneUI && this.m_phoneSingleButtonMeshesToActivate.Count > 0)
		{
			this.ToggleCalloutSectionsForPhoneSingleButton();
			return;
		}
		for (int i = 0; i < this.m_sections.Count; i++)
		{
			GeneralStorePackBuyCallout.CalloutSection calloutSection = this.m_sections[i];
			if (i < numCalloutSectionsNeeded)
			{
				bool active = flag || i > 0;
				calloutSection.m_centerMesh.SetActive(active);
				calloutSection.m_arrowDownMesh1.SetActive(false);
				calloutSection.m_arrowDownMesh2.SetActive(false);
				if (GeneralUtils.IsEven(i))
				{
					calloutSection.m_arrowDownMesh1.SetActive(true);
				}
				else
				{
					calloutSection.m_arrowDownMesh2.SetActive(true);
				}
			}
			else
			{
				if (flag && i == 1)
				{
					calloutSection.m_centerMesh.SetActive(true);
				}
				else
				{
					calloutSection.m_centerMesh.SetActive(false);
				}
				calloutSection.m_arrowDownMesh1.SetActive(false);
				calloutSection.m_arrowDownMesh2.SetActive(false);
			}
		}
	}

	// Token: 0x060063F6 RID: 25590 RVA: 0x00208D24 File Offset: 0x00206F24
	private void ToggleCalloutSectionsForPhoneSingleButton()
	{
		foreach (GeneralStorePackBuyCallout.CalloutSection calloutSection in this.m_sections)
		{
			calloutSection.m_centerMesh.SetActive(false);
			calloutSection.m_arrowDownMesh1.SetActive(false);
			calloutSection.m_arrowDownMesh2.SetActive(false);
		}
		foreach (GameObject gameObject in this.m_phoneSingleButtonMeshesToActivate)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x060063F7 RID: 25591 RVA: 0x00208DE0 File Offset: 0x00206FE0
	private void ToggleGameObjectActive(List<GameObject> gameObjects, int indexToActivate)
	{
		for (int i = 0; i < gameObjects.Count; i++)
		{
			if (gameObjects[i] != null)
			{
				gameObjects[i].SetActive(i == indexToActivate);
			}
		}
	}

	// Token: 0x060063F8 RID: 25592 RVA: 0x00208E20 File Offset: 0x00207020
	private void AnimateIn()
	{
		iTween.Stop(base.gameObject);
		AnimationUtil.ShowWithPunch(base.gameObject, this.m_origScale * 0.01f, this.m_origScale * 1.2f, this.m_origScale, "", false, null, null, null);
	}

	// Token: 0x060063F9 RID: 25593 RVA: 0x00208E74 File Offset: 0x00207074
	private void AnimateOut()
	{
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			base.transform.localScale * 0.01f,
			"time",
			0.25f,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"oncomplete",
			"DeactivateCallout"
		}));
	}

	// Token: 0x060063FA RID: 25594 RVA: 0x00208F00 File Offset: 0x00207100
	private void AnimatePulse()
	{
		iTween.PunchScale(base.gameObject, iTween.Hash(new object[]
		{
			"amount",
			this.m_punchAmount,
			"time",
			1f
		}));
	}

	// Token: 0x040052F3 RID: 21235
	[CustomEditField(Sections = "MultiSliceElement", ListTable = true)]
	public List<GeneralStorePackBuyCallout.CalloutSection> m_sections = new List<GeneralStorePackBuyCallout.CalloutSection>();

	// Token: 0x040052F4 RID: 21236
	[CustomEditField(Sections = "Size Variations")]
	public List<float> m_phonePerspectiveOffsetX = new List<float>();

	// Token: 0x040052F5 RID: 21237
	[CustomEditField(Sections = "Size Variations")]
	public List<int> m_phoneTextWidth = new List<int>();

	// Token: 0x040052F6 RID: 21238
	[CustomEditField(Sections = "Size Variations")]
	public List<GameObject> m_glowSizeVariations = new List<GameObject>();

	// Token: 0x040052F7 RID: 21239
	[CustomEditField(Sections = "Size Variations")]
	public List<GameObject> m_phoneSingleButtonMeshesToActivate = new List<GameObject>();

	// Token: 0x040052F8 RID: 21240
	[CustomEditField(Sections = "Text")]
	public UberText m_text;

	// Token: 0x040052F9 RID: 21241
	[CustomEditField(Sections = "Animation")]
	public Vector3 m_punchAmount = new Vector3(0.2f, 0.2f, 0.2f);

	// Token: 0x040052FA RID: 21242
	private bool m_isShown;

	// Token: 0x040052FB RID: 21243
	private bool m_initialized;

	// Token: 0x040052FC RID: 21244
	private MultiSliceElement m_multiSlice;

	// Token: 0x040052FD RID: 21245
	private const string ANIMATE_PULSE_FUNC = "AnimatePulse";

	// Token: 0x040052FE RID: 21246
	private Vector3 m_origScale;

	// Token: 0x02002277 RID: 8823
	[Serializable]
	public class CalloutSection
	{
		// Token: 0x0400E393 RID: 58259
		public GameObject m_centerMesh;

		// Token: 0x0400E394 RID: 58260
		public GameObject m_arrowDownMesh1;

		// Token: 0x0400E395 RID: 58261
		public GameObject m_arrowDownMesh2;
	}
}
