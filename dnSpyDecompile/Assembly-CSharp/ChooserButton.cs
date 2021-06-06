using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B02 RID: 2818
[CustomEditClass]
public abstract class ChooserButton : AdventureGenericButton
{
	// Token: 0x17000886 RID: 2182
	// (get) Token: 0x06009600 RID: 38400 RVA: 0x0030968F File Offset: 0x0030788F
	// (set) Token: 0x06009601 RID: 38401 RVA: 0x00309697 File Offset: 0x00307897
	[CustomEditField(Sections = "Button Settings")]
	public float ButtonBottomPadding
	{
		get
		{
			return this.m_ButtonBottomPadding;
		}
		set
		{
			this.m_ButtonBottomPadding = value;
			this.UpdateButtonPositions();
		}
	}

	// Token: 0x17000887 RID: 2183
	// (get) Token: 0x06009602 RID: 38402 RVA: 0x003096A6 File Offset: 0x003078A6
	// (set) Token: 0x06009603 RID: 38403 RVA: 0x003096AE File Offset: 0x003078AE
	[CustomEditField(Sections = "Sub Button Settings")]
	public float SubButtonHeight
	{
		get
		{
			return this.m_SubButtonHeight;
		}
		set
		{
			this.m_SubButtonHeight = value;
			this.UpdateButtonPositions();
		}
	}

	// Token: 0x17000888 RID: 2184
	// (get) Token: 0x06009604 RID: 38404 RVA: 0x003096BD File Offset: 0x003078BD
	// (set) Token: 0x06009605 RID: 38405 RVA: 0x003096C5 File Offset: 0x003078C5
	[CustomEditField(Sections = "Sub Button Settings")]
	public float SubButtonContainerBtmPadding
	{
		get
		{
			return this.m_SubButtonContainerBtmPadding;
		}
		set
		{
			this.m_SubButtonContainerBtmPadding = value;
			this.UpdateButtonPositions();
		}
	}

	// Token: 0x17000889 RID: 2185
	// (get) Token: 0x06009606 RID: 38406 RVA: 0x003096D4 File Offset: 0x003078D4
	// (set) Token: 0x06009607 RID: 38407 RVA: 0x003096DC File Offset: 0x003078DC
	[CustomEditField(Sections = "Button Settings")]
	public bool Toggle
	{
		get
		{
			return this.m_Toggled;
		}
		set
		{
			this.ToggleButton(value);
		}
	}

	// Token: 0x06009608 RID: 38408 RVA: 0x003096E8 File Offset: 0x003078E8
	protected override void Awake()
	{
		base.Awake();
		this.m_SubButtonContainer.SetActive(this.Toggle);
		this.m_SubButtonContainer.transform.localPosition = this.GetHiddenPosition();
		if (this.m_PortraitRenderer != null)
		{
			this.m_MainButtonExtents = this.m_PortraitRenderer.bounds.extents;
		}
		this.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ToggleButton(!this.Toggle);
		});
	}

	// Token: 0x06009609 RID: 38409 RVA: 0x0030975D File Offset: 0x0030795D
	public ChooserSubButton[] GetSubButtons()
	{
		return this.m_SubButtons.ToArray();
	}

	// Token: 0x0600960A RID: 38410 RVA: 0x0030976A File Offset: 0x0030796A
	public void SetSelectSubButtonOnToggle(bool flag)
	{
		this.m_SelectSubButtonOnToggle = flag;
	}

	// Token: 0x0600960B RID: 38411 RVA: 0x00309774 File Offset: 0x00307974
	public void UpdateButtonPositions()
	{
		float subButtonHeight = this.m_SubButtonHeight;
		for (int i = 1; i < this.m_SubButtons.Count; i++)
		{
			TransformUtil.SetLocalPosZ(this.m_SubButtons[i], subButtonHeight * (float)i);
		}
	}

	// Token: 0x0600960C RID: 38412 RVA: 0x003097B3 File Offset: 0x003079B3
	public void AddVisualUpdatedListener(ChooserButton.VisualUpdated dlg)
	{
		this.m_VisualUpdatedEventList.Add(dlg);
	}

	// Token: 0x0600960D RID: 38413 RVA: 0x003097C1 File Offset: 0x003079C1
	public void AddToggleListener(ChooserButton.Toggled dlg)
	{
		this.m_ToggleEventList.Add(dlg);
	}

	// Token: 0x0600960E RID: 38414 RVA: 0x003097CF File Offset: 0x003079CF
	public void AddModeSelectionListener(ChooserButton.ModeSelection dlg)
	{
		this.m_ModeSelectionEventList.Add(dlg);
	}

	// Token: 0x0600960F RID: 38415 RVA: 0x003097DD File Offset: 0x003079DD
	public void AddExpandedListener(ChooserButton.Expanded dlg)
	{
		this.m_ExpandedEventList.Add(dlg);
	}

	// Token: 0x06009610 RID: 38416 RVA: 0x003097EC File Offset: 0x003079EC
	public float GetFullButtonHeight()
	{
		if (this.m_PortraitRenderer == null || this.m_SubButtonContainer == null)
		{
			return TransformUtil.GetBoundsOfChildren(base.gameObject).size.z;
		}
		float val = this.m_SubButtonContainer.transform.localPosition.z + this.m_SubButtonHeight * (float)this.m_SubButtons.Count + this.m_SubButtonContainerBtmPadding;
		float num = this.m_PortraitRenderer.transform.localPosition.z - this.m_MainButtonExtents.z;
		return Math.Max(this.m_PortraitRenderer.transform.localPosition.z + this.m_MainButtonExtents.z, val) - num - this.m_ButtonBottomPadding;
	}

	// Token: 0x06009611 RID: 38417 RVA: 0x003098B4 File Offset: 0x00307AB4
	public void DisableSubButtonHighlights()
	{
		foreach (ChooserSubButton chooserSubButton in this.m_SubButtons)
		{
			chooserSubButton.SetHighlight(false);
		}
	}

	// Token: 0x06009612 RID: 38418 RVA: 0x00309908 File Offset: 0x00307B08
	public bool ContainsSubButton(ChooserSubButton btn)
	{
		return this.m_SubButtons.Exists((ChooserSubButton x) => x == btn);
	}

	// Token: 0x06009613 RID: 38419 RVA: 0x0030993C File Offset: 0x00307B3C
	public void ToggleButton(bool toggle)
	{
		if (toggle == this.m_Toggled)
		{
			return;
		}
		this.m_Toggled = toggle;
		this.m_ButtonStateTable.CancelQueuedStates();
		this.m_ButtonStateTable.TriggerState(this.Toggle ? "Expand" : "Contract", true, null);
		if (this.Toggle)
		{
			this.m_SubButtonContainer.SetActive(true);
		}
		Vector3 hiddenPosition = this.GetHiddenPosition();
		Vector3 showPosition = this.GetShowPosition();
		Vector3 vector = this.Toggle ? hiddenPosition : showPosition;
		Vector3 vector2 = this.Toggle ? showPosition : hiddenPosition;
		this.m_SubButtonContainer.transform.localPosition = vector;
		this.UpdateSubButtonsVisibility(vector, this.m_SubButtonShowPosZ);
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"from",
			vector,
			"to",
			vector2,
			"time",
			this.m_SubButtonAnimationTime,
			"easeType",
			this.Toggle ? this.m_ActivateEaseType : this.m_DeactivateEaseType,
			"oncomplete",
			"OnExpandAnimationComplete",
			"oncompletetarget",
			base.gameObject,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.OnButtonAnimating((Vector3)newVal, this.m_SubButtonShowPosZ);
			}),
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		this.FireToggleEvent();
		if (this.Toggle && this.m_SelectSubButtonOnToggle && this.m_LastSelectedSubButton != null)
		{
			this.OnSubButtonClicked(this.m_LastSelectedSubButton);
		}
	}

	// Token: 0x06009614 RID: 38420 RVA: 0x00309AEC File Offset: 0x00307CEC
	protected ChooserSubButton CreateSubButton(string subButtonPrefab, bool useAsLastSelected)
	{
		if (this.m_SubButtonContainer == null)
		{
			Debug.LogError("m_SubButtonContainer cannot be null. Unable to create subbutton.", this);
			return null;
		}
		ChooserSubButton newSubButton = GameUtils.LoadGameObjectWithComponent<ChooserSubButton>(subButtonPrefab);
		if (newSubButton == null)
		{
			return null;
		}
		GameUtils.SetParent(newSubButton, this.m_SubButtonContainer, false);
		if (useAsLastSelected || this.m_LastSelectedSubButton == null)
		{
			this.m_LastSelectedSubButton = newSubButton;
		}
		newSubButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnSubButtonClicked(newSubButton);
		});
		this.m_SubButtons.Add(newSubButton);
		this.UpdateButtonPositions();
		this.m_SubButtonContainer.transform.localPosition = this.GetHiddenPosition();
		return newSubButton;
	}

	// Token: 0x06009615 RID: 38421 RVA: 0x00309BB8 File Offset: 0x00307DB8
	protected Vector3 GetHiddenPosition()
	{
		Vector3 localPosition = this.m_SubButtonContainer.transform.localPosition;
		return new Vector3(localPosition.x, localPosition.y, this.m_SubButtonShowPosZ - this.m_SubButtonHeight * (float)this.m_SubButtons.Count - this.m_SubButtonContainerBtmPadding);
	}

	// Token: 0x06009616 RID: 38422 RVA: 0x00309C08 File Offset: 0x00307E08
	private Vector3 GetShowPosition()
	{
		Vector3 localPosition = this.m_SubButtonContainer.transform.localPosition;
		return new Vector3(localPosition.x, localPosition.y, this.m_SubButtonShowPosZ);
	}

	// Token: 0x06009617 RID: 38423 RVA: 0x00309C3D File Offset: 0x00307E3D
	private void OnButtonAnimating(Vector3 curr, float zposshowlimit)
	{
		this.m_SubButtonContainer.transform.localPosition = curr;
		this.UpdateSubButtonsVisibility(curr, zposshowlimit);
		this.FireVisualUpdatedEvent();
	}

	// Token: 0x06009618 RID: 38424 RVA: 0x00309C60 File Offset: 0x00307E60
	private void UpdateSubButtonsVisibility(Vector3 curr, float zposshowlimit)
	{
		float subButtonHeight = this.m_SubButtonHeight;
		for (int i = 0; i < this.m_SubButtons.Count; i++)
		{
			float num = subButtonHeight * (float)(i + 1) + curr.z;
			bool flag = zposshowlimit - num <= this.m_SubButtonVisibilityPadding;
			GameObject gameObject = this.m_SubButtons[i].gameObject;
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
		}
	}

	// Token: 0x06009619 RID: 38425 RVA: 0x00309CCC File Offset: 0x00307ECC
	private void OnExpandAnimationComplete()
	{
		if (this.m_SubButtonContainer.activeSelf != this.m_Toggled)
		{
			this.m_SubButtonContainer.SetActive(this.Toggle);
		}
		this.FireExpandedEvent(this.Toggle);
	}

	// Token: 0x0600961A RID: 38426 RVA: 0x00309D00 File Offset: 0x00307F00
	public void FireVisualUpdatedEvent()
	{
		ChooserButton.VisualUpdated[] array = this.m_VisualUpdatedEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x0600961B RID: 38427 RVA: 0x00309D30 File Offset: 0x00307F30
	private void FireToggleEvent()
	{
		ChooserButton.Toggled[] array = this.m_ToggleEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.Toggle);
		}
	}

	// Token: 0x0600961C RID: 38428 RVA: 0x00309D68 File Offset: 0x00307F68
	private void FireModeSelectedEvent(ChooserSubButton btn)
	{
		ChooserButton.ModeSelection[] array = this.m_ModeSelectionEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](btn);
		}
	}

	// Token: 0x0600961D RID: 38429 RVA: 0x00309D98 File Offset: 0x00307F98
	private void FireExpandedEvent(bool expand)
	{
		ChooserButton.Expanded[] array = this.m_ExpandedEventList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this, expand);
		}
	}

	// Token: 0x0600961E RID: 38430 RVA: 0x00309DCC File Offset: 0x00307FCC
	protected void OnSubButtonClicked(ChooserSubButton btn)
	{
		this.m_LastSelectedSubButton = btn;
		this.FireModeSelectedEvent(btn);
		foreach (ChooserSubButton chooserSubButton in this.m_SubButtons)
		{
			chooserSubButton.SetHighlight(chooserSubButton == btn);
		}
	}

	// Token: 0x04007DC8 RID: 32200
	private const string s_EventButtonExpand = "Expand";

	// Token: 0x04007DC9 RID: 32201
	private const string s_EventButtonContract = "Contract";

	// Token: 0x04007DCA RID: 32202
	[CustomEditField(Sections = "Button State Table")]
	[SerializeField]
	public StateEventTable m_ButtonStateTable;

	// Token: 0x04007DCB RID: 32203
	[SerializeField]
	private float m_ButtonBottomPadding;

	// Token: 0x04007DCC RID: 32204
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public GameObject m_SubButtonContainer;

	// Token: 0x04007DCD RID: 32205
	[SerializeField]
	private float m_SubButtonHeight = 3.75f;

	// Token: 0x04007DCE RID: 32206
	[SerializeField]
	private float m_SubButtonContainerBtmPadding = 0.1f;

	// Token: 0x04007DCF RID: 32207
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public iTween.EaseType m_ActivateEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x04007DD0 RID: 32208
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public iTween.EaseType m_DeactivateEaseType = iTween.EaseType.easeOutSine;

	// Token: 0x04007DD1 RID: 32209
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonVisibilityPadding = 5f;

	// Token: 0x04007DD2 RID: 32210
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonAnimationTime = 0.25f;

	// Token: 0x04007DD3 RID: 32211
	[CustomEditField(Sections = "Sub Button Settings")]
	[SerializeField]
	public float m_SubButtonShowPosZ;

	// Token: 0x04007DD4 RID: 32212
	private bool m_Toggled;

	// Token: 0x04007DD5 RID: 32213
	private bool m_SelectSubButtonOnToggle;

	// Token: 0x04007DD6 RID: 32214
	private Vector3 m_MainButtonExtents = Vector3.zero;

	// Token: 0x04007DD7 RID: 32215
	protected List<ChooserSubButton> m_SubButtons = new List<ChooserSubButton>();

	// Token: 0x04007DD8 RID: 32216
	protected List<ChooserButton.VisualUpdated> m_VisualUpdatedEventList = new List<ChooserButton.VisualUpdated>();

	// Token: 0x04007DD9 RID: 32217
	protected List<ChooserButton.Toggled> m_ToggleEventList = new List<ChooserButton.Toggled>();

	// Token: 0x04007DDA RID: 32218
	protected List<ChooserButton.ModeSelection> m_ModeSelectionEventList = new List<ChooserButton.ModeSelection>();

	// Token: 0x04007DDB RID: 32219
	protected List<ChooserButton.Expanded> m_ExpandedEventList = new List<ChooserButton.Expanded>();

	// Token: 0x04007DDC RID: 32220
	protected ChooserSubButton m_LastSelectedSubButton;

	// Token: 0x0200274C RID: 10060
	// (Invoke) Token: 0x06013978 RID: 80248
	public delegate void VisualUpdated();

	// Token: 0x0200274D RID: 10061
	// (Invoke) Token: 0x0601397C RID: 80252
	public delegate void Toggled(bool toggle);

	// Token: 0x0200274E RID: 10062
	// (Invoke) Token: 0x06013980 RID: 80256
	public delegate void ModeSelection(ChooserSubButton btn);

	// Token: 0x0200274F RID: 10063
	// (Invoke) Token: 0x06013984 RID: 80260
	public delegate void Expanded(ChooserButton button, bool expand);
}
