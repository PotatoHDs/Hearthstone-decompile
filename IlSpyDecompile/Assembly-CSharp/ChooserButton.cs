using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public abstract class ChooserButton : AdventureGenericButton
{
	public delegate void VisualUpdated();

	public delegate void Toggled(bool toggle);

	public delegate void ModeSelection(ChooserSubButton btn);

	public delegate void Expanded(ChooserButton button, bool expand);

	private const string s_EventButtonExpand = "Expand";

	private const string s_EventButtonContract = "Contract";

	[CustomEditField(Sections = "Button State Table")]
	[SerializeField]
	public StateEventTable m_ButtonStateTable;

	[SerializeField]
	private float m_ButtonBottomPadding;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public GameObject m_SubButtonContainer;

	[SerializeField]
	private float m_SubButtonHeight = 3.75f;

	[SerializeField]
	private float m_SubButtonContainerBtmPadding = 0.1f;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public iTween.EaseType m_ActivateEaseType = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public iTween.EaseType m_DeactivateEaseType = iTween.EaseType.easeOutSine;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonVisibilityPadding = 5f;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonAnimationTime = 0.25f;

	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonShowPosZ;

	private bool m_Toggled;

	private bool m_SelectSubButtonOnToggle;

	private Vector3 m_MainButtonExtents = Vector3.zero;

	protected List<ChooserSubButton> m_SubButtons = new List<ChooserSubButton>();

	protected List<VisualUpdated> m_VisualUpdatedEventList = new List<VisualUpdated>();

	protected List<Toggled> m_ToggleEventList = new List<Toggled>();

	protected List<ModeSelection> m_ModeSelectionEventList = new List<ModeSelection>();

	protected List<Expanded> m_ExpandedEventList = new List<Expanded>();

	protected ChooserSubButton m_LastSelectedSubButton;

	[CustomEditField(Sections = "Button Settings")]
	public float ButtonBottomPadding
	{
		get
		{
			return m_ButtonBottomPadding;
		}
		set
		{
			m_ButtonBottomPadding = value;
			UpdateButtonPositions();
		}
	}

	[CustomEditField(Sections = "Sub Button Settings")]
	public float SubButtonHeight
	{
		get
		{
			return m_SubButtonHeight;
		}
		set
		{
			m_SubButtonHeight = value;
			UpdateButtonPositions();
		}
	}

	[CustomEditField(Sections = "Sub Button Settings")]
	public float SubButtonContainerBtmPadding
	{
		get
		{
			return m_SubButtonContainerBtmPadding;
		}
		set
		{
			m_SubButtonContainerBtmPadding = value;
			UpdateButtonPositions();
		}
	}

	[CustomEditField(Sections = "Button Settings")]
	public bool Toggle
	{
		get
		{
			return m_Toggled;
		}
		set
		{
			ToggleButton(value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		m_SubButtonContainer.SetActive(Toggle);
		m_SubButtonContainer.transform.localPosition = GetHiddenPosition();
		if (m_PortraitRenderer != null)
		{
			m_MainButtonExtents = m_PortraitRenderer.bounds.extents;
		}
		AddEventListener(UIEventType.RELEASE, delegate
		{
			ToggleButton(!Toggle);
		});
	}

	public ChooserSubButton[] GetSubButtons()
	{
		return m_SubButtons.ToArray();
	}

	public void SetSelectSubButtonOnToggle(bool flag)
	{
		m_SelectSubButtonOnToggle = flag;
	}

	public void UpdateButtonPositions()
	{
		float subButtonHeight = m_SubButtonHeight;
		for (int i = 1; i < m_SubButtons.Count; i++)
		{
			TransformUtil.SetLocalPosZ(m_SubButtons[i], subButtonHeight * (float)i);
		}
	}

	public void AddVisualUpdatedListener(VisualUpdated dlg)
	{
		m_VisualUpdatedEventList.Add(dlg);
	}

	public void AddToggleListener(Toggled dlg)
	{
		m_ToggleEventList.Add(dlg);
	}

	public void AddModeSelectionListener(ModeSelection dlg)
	{
		m_ModeSelectionEventList.Add(dlg);
	}

	public void AddExpandedListener(Expanded dlg)
	{
		m_ExpandedEventList.Add(dlg);
	}

	public float GetFullButtonHeight()
	{
		if (m_PortraitRenderer == null || m_SubButtonContainer == null)
		{
			return TransformUtil.GetBoundsOfChildren(base.gameObject).size.z;
		}
		float val = m_SubButtonContainer.transform.localPosition.z + m_SubButtonHeight * (float)m_SubButtons.Count + m_SubButtonContainerBtmPadding;
		float num = m_PortraitRenderer.transform.localPosition.z - m_MainButtonExtents.z;
		return Math.Max(m_PortraitRenderer.transform.localPosition.z + m_MainButtonExtents.z, val) - num - m_ButtonBottomPadding;
	}

	public void DisableSubButtonHighlights()
	{
		foreach (ChooserSubButton subButton in m_SubButtons)
		{
			subButton.SetHighlight(enable: false);
		}
	}

	public bool ContainsSubButton(ChooserSubButton btn)
	{
		return m_SubButtons.Exists((ChooserSubButton x) => x == btn);
	}

	public void ToggleButton(bool toggle)
	{
		if (toggle != m_Toggled)
		{
			m_Toggled = toggle;
			m_ButtonStateTable.CancelQueuedStates();
			m_ButtonStateTable.TriggerState(Toggle ? "Expand" : "Contract");
			if (Toggle)
			{
				m_SubButtonContainer.SetActive(value: true);
			}
			Vector3 hiddenPosition = GetHiddenPosition();
			Vector3 showPosition = GetShowPosition();
			Vector3 vector = (Toggle ? hiddenPosition : showPosition);
			Vector3 vector2 = (Toggle ? showPosition : hiddenPosition);
			m_SubButtonContainer.transform.localPosition = vector;
			UpdateSubButtonsVisibility(vector, m_SubButtonShowPosZ);
			Hashtable args = iTween.Hash("islocal", true, "from", vector, "to", vector2, "time", m_SubButtonAnimationTime, "easeType", Toggle ? m_ActivateEaseType : m_DeactivateEaseType, "oncomplete", "OnExpandAnimationComplete", "oncompletetarget", base.gameObject, "onupdate", (Action<object>)delegate(object newVal)
			{
				OnButtonAnimating((Vector3)newVal, m_SubButtonShowPosZ);
			}, "onupdatetarget", base.gameObject);
			iTween.ValueTo(base.gameObject, args);
			FireToggleEvent();
			if (Toggle && m_SelectSubButtonOnToggle && m_LastSelectedSubButton != null)
			{
				OnSubButtonClicked(m_LastSelectedSubButton);
			}
		}
	}

	protected ChooserSubButton CreateSubButton(string subButtonPrefab, bool useAsLastSelected)
	{
		if (m_SubButtonContainer == null)
		{
			Debug.LogError("m_SubButtonContainer cannot be null. Unable to create subbutton.", this);
			return null;
		}
		ChooserSubButton newSubButton = GameUtils.LoadGameObjectWithComponent<ChooserSubButton>(subButtonPrefab);
		if (newSubButton == null)
		{
			return null;
		}
		GameUtils.SetParent(newSubButton, m_SubButtonContainer);
		if (useAsLastSelected || m_LastSelectedSubButton == null)
		{
			m_LastSelectedSubButton = newSubButton;
		}
		newSubButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnSubButtonClicked(newSubButton);
		});
		m_SubButtons.Add(newSubButton);
		UpdateButtonPositions();
		m_SubButtonContainer.transform.localPosition = GetHiddenPosition();
		return newSubButton;
	}

	protected Vector3 GetHiddenPosition()
	{
		Vector3 localPosition = m_SubButtonContainer.transform.localPosition;
		return new Vector3(localPosition.x, localPosition.y, m_SubButtonShowPosZ - m_SubButtonHeight * (float)m_SubButtons.Count - m_SubButtonContainerBtmPadding);
	}

	private Vector3 GetShowPosition()
	{
		Vector3 localPosition = m_SubButtonContainer.transform.localPosition;
		return new Vector3(localPosition.x, localPosition.y, m_SubButtonShowPosZ);
	}

	private void OnButtonAnimating(Vector3 curr, float zposshowlimit)
	{
		m_SubButtonContainer.transform.localPosition = curr;
		UpdateSubButtonsVisibility(curr, zposshowlimit);
		FireVisualUpdatedEvent();
	}

	private void UpdateSubButtonsVisibility(Vector3 curr, float zposshowlimit)
	{
		float subButtonHeight = m_SubButtonHeight;
		for (int i = 0; i < m_SubButtons.Count; i++)
		{
			float num = subButtonHeight * (float)(i + 1) + curr.z;
			bool flag = zposshowlimit - num <= m_SubButtonVisibilityPadding;
			GameObject gameObject = m_SubButtons[i].gameObject;
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
		}
	}

	private void OnExpandAnimationComplete()
	{
		if (m_SubButtonContainer.activeSelf != m_Toggled)
		{
			m_SubButtonContainer.SetActive(Toggle);
		}
		FireExpandedEvent(Toggle);
	}

	public void FireVisualUpdatedEvent()
	{
		VisualUpdated[] array = m_VisualUpdatedEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private void FireToggleEvent()
	{
		Toggled[] array = m_ToggleEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](Toggle);
		}
	}

	private void FireModeSelectedEvent(ChooserSubButton btn)
	{
		ModeSelection[] array = m_ModeSelectionEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](btn);
		}
	}

	private void FireExpandedEvent(bool expand)
	{
		Expanded[] array = m_ExpandedEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this, expand);
		}
	}

	protected void OnSubButtonClicked(ChooserSubButton btn)
	{
		m_LastSelectedSubButton = btn;
		FireModeSelectedEvent(btn);
		foreach (ChooserSubButton subButton in m_SubButtons)
		{
			subButton.SetHighlight(subButton == btn);
		}
	}
}
