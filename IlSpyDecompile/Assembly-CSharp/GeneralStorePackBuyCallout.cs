using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePackBuyCallout : MonoBehaviour
{
	[Serializable]
	public class CalloutSection
	{
		public GameObject m_centerMesh;

		public GameObject m_arrowDownMesh1;

		public GameObject m_arrowDownMesh2;
	}

	[CustomEditField(Sections = "MultiSliceElement", ListTable = true)]
	public List<CalloutSection> m_sections = new List<CalloutSection>();

	[CustomEditField(Sections = "Size Variations")]
	public List<float> m_phonePerspectiveOffsetX = new List<float>();

	[CustomEditField(Sections = "Size Variations")]
	public List<int> m_phoneTextWidth = new List<int>();

	[CustomEditField(Sections = "Size Variations")]
	public List<GameObject> m_glowSizeVariations = new List<GameObject>();

	[CustomEditField(Sections = "Size Variations")]
	public List<GameObject> m_phoneSingleButtonMeshesToActivate = new List<GameObject>();

	[CustomEditField(Sections = "Text")]
	public UberText m_text;

	[CustomEditField(Sections = "Animation")]
	public Vector3 m_punchAmount = new Vector3(0.2f, 0.2f, 0.2f);

	private bool m_isShown;

	private bool m_initialized;

	private MultiSliceElement m_multiSlice;

	private const string ANIMATE_PULSE_FUNC = "AnimatePulse";

	private Vector3 m_origScale;

	private void Awake()
	{
	}

	public void Init()
	{
		m_multiSlice = GetComponent<MultiSliceElement>();
		m_origScale = base.transform.localScale;
		m_initialized = true;
	}

	public bool IsShown()
	{
		return m_isShown;
	}

	public void ShowCallout(GeneralStorePackBuyButton firstButton, GeneralStorePackBuyButton lastButton, int numButtons)
	{
		if (!m_initialized || m_isShown || firstButton == null || lastButton == null || numButtons <= 0)
		{
			return;
		}
		m_isShown = true;
		base.gameObject.SetActive(value: true);
		m_text.Text = GameStrings.Get("GLUE_STORE_LIMITED_TIME_OFFER");
		ToggleCalloutSections(numButtons);
		m_multiSlice.UpdateSlices();
		int indexToActivate = numButtons - 1;
		ToggleGameObjectActive(m_glowSizeVariations, indexToActivate);
		float x = firstButton.transform.position.x;
		float num = (lastButton.transform.position.x - x) / 2f;
		float x2 = x + num;
		TransformUtil.SetPosX(base.gameObject, x2);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (numButtons <= m_phonePerspectiveOffsetX.Count)
			{
				float num2 = m_phonePerspectiveOffsetX[numButtons - 1];
				TransformUtil.SetLocalPosX(base.gameObject, base.transform.localPosition.x + num2);
			}
			if (numButtons <= m_phoneTextWidth.Count)
			{
				m_text.Width = m_phoneTextWidth[numButtons - 1];
			}
		}
		AnimateIn();
		InvokeRepeating("AnimatePulse", 3f, 3f);
	}

	public void HideCallout()
	{
		if (m_isShown)
		{
			CancelInvoke("AnimatePulse");
			AnimateOut();
		}
	}

	public void DeactivateCallout()
	{
		CancelInvoke("AnimatePulse");
		base.gameObject.SetActive(value: false);
		m_isShown = false;
	}

	private void ToggleCalloutSections(int numCalloutSectionsNeeded)
	{
		bool flag = numCalloutSectionsNeeded == 1;
		if (flag && (bool)UniversalInputManager.UsePhoneUI && m_phoneSingleButtonMeshesToActivate.Count > 0)
		{
			ToggleCalloutSectionsForPhoneSingleButton();
			return;
		}
		for (int i = 0; i < m_sections.Count; i++)
		{
			CalloutSection calloutSection = m_sections[i];
			if (i < numCalloutSectionsNeeded)
			{
				bool active = flag || i > 0;
				calloutSection.m_centerMesh.SetActive(active);
				calloutSection.m_arrowDownMesh1.SetActive(value: false);
				calloutSection.m_arrowDownMesh2.SetActive(value: false);
				if (GeneralUtils.IsEven(i))
				{
					calloutSection.m_arrowDownMesh1.SetActive(value: true);
				}
				else
				{
					calloutSection.m_arrowDownMesh2.SetActive(value: true);
				}
			}
			else
			{
				if (flag && i == 1)
				{
					calloutSection.m_centerMesh.SetActive(value: true);
				}
				else
				{
					calloutSection.m_centerMesh.SetActive(value: false);
				}
				calloutSection.m_arrowDownMesh1.SetActive(value: false);
				calloutSection.m_arrowDownMesh2.SetActive(value: false);
			}
		}
	}

	private void ToggleCalloutSectionsForPhoneSingleButton()
	{
		foreach (CalloutSection section in m_sections)
		{
			section.m_centerMesh.SetActive(value: false);
			section.m_arrowDownMesh1.SetActive(value: false);
			section.m_arrowDownMesh2.SetActive(value: false);
		}
		foreach (GameObject item in m_phoneSingleButtonMeshesToActivate)
		{
			if (item != null)
			{
				item.SetActive(value: true);
			}
		}
	}

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

	private void AnimateIn()
	{
		iTween.Stop(base.gameObject);
		AnimationUtil.ShowWithPunch(base.gameObject, m_origScale * 0.01f, m_origScale * 1.2f, m_origScale);
	}

	private void AnimateOut()
	{
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", base.transform.localScale * 0.01f, "time", 0.25f, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "DeactivateCallout"));
	}

	private void AnimatePulse()
	{
		iTween.PunchScale(base.gameObject, iTween.Hash("amount", m_punchAmount, "time", 1f));
	}
}
