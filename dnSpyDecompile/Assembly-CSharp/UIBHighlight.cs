using System;
using UnityEngine;

// Token: 0x02000AF1 RID: 2801
[CustomEditClass]
[RequireComponent(typeof(PegUIElement))]
public class UIBHighlight : MonoBehaviour
{
	// Token: 0x1700087E RID: 2174
	// (get) Token: 0x060094FC RID: 38140 RVA: 0x00304606 File Offset: 0x00302806
	// (set) Token: 0x060094FD RID: 38141 RVA: 0x0030460E File Offset: 0x0030280E
	[CustomEditField(Sections = "Behavior Settings")]
	public bool AlwaysOver
	{
		get
		{
			return this.m_AlwaysOver;
		}
		set
		{
			this.m_AlwaysOver = value;
			this.ResetState();
		}
	}

	// Token: 0x1700087F RID: 2175
	// (get) Token: 0x060094FE RID: 38142 RVA: 0x0030461D File Offset: 0x0030281D
	// (set) Token: 0x060094FF RID: 38143 RVA: 0x00304625 File Offset: 0x00302825
	[CustomEditField(Sections = "Behavior Settings")]
	public bool EnableResponse
	{
		get
		{
			return this.m_EnableResponse;
		}
		set
		{
			this.m_EnableResponse = value;
			this.ResetState();
		}
	}

	// Token: 0x06009500 RID: 38144 RVA: 0x00304634 File Offset: 0x00302834
	private void Awake()
	{
		PegUIElement component = base.gameObject.GetComponent<PegUIElement>();
		if (component != null)
		{
			component.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				this.OnRollOver(false);
			});
			component.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.OnPress(true);
			});
			component.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnRelease();
			});
			component.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				this.OnRollOut(false);
			});
			this.ResetState();
		}
	}

	// Token: 0x06009501 RID: 38145 RVA: 0x003046AC File Offset: 0x003028AC
	public void HighlightOnce()
	{
		this.OnRollOver(true);
	}

	// Token: 0x06009502 RID: 38146 RVA: 0x003046B5 File Offset: 0x003028B5
	public void Select()
	{
		if (this.m_SelectOnRelease)
		{
			this.OnRelease(true);
			return;
		}
		this.OnPress(true);
	}

	// Token: 0x06009503 RID: 38147 RVA: 0x003046CE File Offset: 0x003028CE
	public void SelectNoSound()
	{
		if (this.m_SelectOnRelease)
		{
			this.OnRelease(false);
			return;
		}
		this.OnPress(false);
	}

	// Token: 0x06009504 RID: 38148 RVA: 0x003046E7 File Offset: 0x003028E7
	public void Reset()
	{
		this.ResetState();
		this.ShowHighlightObject(this.m_SelectedHighlight, false);
		this.ShowHighlightObject(this.m_MouseOverSelectedHighlight, false);
		this.ShowHighlightObject(this.m_MouseOverHighlight, false);
	}

	// Token: 0x06009505 RID: 38149 RVA: 0x00304716 File Offset: 0x00302916
	private void ResetState()
	{
		if (this.m_AlwaysOver)
		{
			this.OnRollOver(true);
			return;
		}
		this.OnRollOut(true);
	}

	// Token: 0x06009506 RID: 38150 RVA: 0x00304730 File Offset: 0x00302930
	private void OnRollOver(bool force = false)
	{
		if (!this.m_EnableResponse && !force)
		{
			return;
		}
		if (!this.m_AlwaysOver)
		{
			this.PlaySound(this.m_MouseOverSound);
		}
		if (this.m_AllowSelection && (this.m_SelectedHighlight == null || this.m_SelectedHighlight.activeSelf))
		{
			this.ShowHighlightObject(this.m_MouseOverSelectedHighlight, true);
			this.ShowHighlightObject(this.m_SelectedHighlight, false);
			this.ShowHighlightObject(this.m_MouseOverHighlight, false);
			this.ShowHighlightObject(this.m_MouseUpHighlight, false);
			this.ShowHighlightObject(this.m_MouseDownHighlight, false);
			return;
		}
		this.ShowHighlightObject(this.m_MouseDownHighlight, false);
		this.ShowHighlightObject(this.m_MouseOverHighlight, true);
		this.ShowHighlightObject(this.m_MouseUpHighlight, false);
	}

	// Token: 0x06009507 RID: 38151 RVA: 0x003047EC File Offset: 0x003029EC
	private void OnRollOut(bool force = false)
	{
		if (!this.m_EnableResponse && !force)
		{
			return;
		}
		this.PlaySound(this.m_MouseOutSound);
		if (this.m_AllowSelection && (this.m_MouseOverSelectedHighlight == null || this.m_MouseOverSelectedHighlight.activeSelf))
		{
			this.ShowHighlightObject(this.m_SelectedHighlight, true);
			this.ShowHighlightObject(this.m_MouseOverSelectedHighlight, false);
			this.ShowHighlightObject(this.m_MouseOverHighlight, false);
			this.ShowHighlightObject(this.m_MouseUpHighlight, false);
			this.ShowHighlightObject(this.m_MouseDownHighlight, false);
			return;
		}
		this.ShowHighlightObject(this.m_MouseDownHighlight, false);
		this.ShowHighlightObject(this.m_MouseOverHighlight, this.m_AlwaysOver);
		this.ShowHighlightObject(this.m_MouseUpHighlight, !this.m_AlwaysOver);
	}

	// Token: 0x06009508 RID: 38152 RVA: 0x003048AA File Offset: 0x00302AAA
	private void OnPress()
	{
		this.OnPress(true);
	}

	// Token: 0x06009509 RID: 38153 RVA: 0x003048B4 File Offset: 0x00302AB4
	private void OnPress(bool playSound)
	{
		if (!this.m_EnableResponse)
		{
			return;
		}
		if (playSound)
		{
			this.PlaySound(this.m_MouseDownSound);
		}
		if (this.m_AllowSelection && !this.m_SelectOnRelease)
		{
			this.ShowHighlightObject(this.m_SelectedHighlight, true);
			this.ShowHighlightObject(this.m_MouseOverSelectedHighlight, false);
			this.ShowHighlightObject(this.m_MouseOverHighlight, false);
			this.ShowHighlightObject(this.m_MouseUpHighlight, false);
			this.ShowHighlightObject(this.m_MouseDownHighlight, false);
			return;
		}
		this.ShowHighlightObject(this.m_MouseDownHighlight, true);
		this.ShowHighlightObject(this.m_MouseOverHighlight, this.m_AlwaysOver || !this.m_HideMouseOverOnPress);
		this.ShowHighlightObject(this.m_MouseUpHighlight, !this.m_AlwaysOver);
	}

	// Token: 0x0600950A RID: 38154 RVA: 0x0030496D File Offset: 0x00302B6D
	private void OnRelease()
	{
		this.OnRelease(true);
	}

	// Token: 0x0600950B RID: 38155 RVA: 0x00304978 File Offset: 0x00302B78
	private void OnRelease(bool playSound)
	{
		if (!this.m_EnableResponse)
		{
			return;
		}
		if (playSound)
		{
			this.PlaySound(this.m_MouseUpSound);
		}
		if (this.m_AllowSelection && this.m_SelectOnRelease)
		{
			this.ShowHighlightObject(this.m_SelectedHighlight, true);
			this.ShowHighlightObject(this.m_MouseOverSelectedHighlight, false);
			this.ShowHighlightObject(this.m_MouseOverHighlight, false);
			this.ShowHighlightObject(this.m_MouseUpHighlight, false);
			this.ShowHighlightObject(this.m_MouseDownHighlight, false);
			return;
		}
		this.ShowHighlightObject(this.m_MouseDownHighlight, false);
		this.ShowHighlightObject(this.m_MouseOverHighlight, true);
		this.ShowHighlightObject(this.m_MouseUpHighlight, false);
	}

	// Token: 0x0600950C RID: 38156 RVA: 0x00304A16 File Offset: 0x00302C16
	private void ShowHighlightObject(GameObject obj, bool show)
	{
		if (obj != null && obj.activeSelf != show)
		{
			obj.SetActive(show);
		}
	}

	// Token: 0x0600950D RID: 38157 RVA: 0x00304A31 File Offset: 0x00302C31
	private void PlaySound(string soundFilePath)
	{
		if (SoundManager.Get() == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(soundFilePath))
		{
			SoundManager.Get().LoadAndPlay(soundFilePath);
		}
	}

	// Token: 0x04007CF6 RID: 31990
	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseOverHighlight;

	// Token: 0x04007CF7 RID: 31991
	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseDownHighlight;

	// Token: 0x04007CF8 RID: 31992
	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseUpHighlight;

	// Token: 0x04007CF9 RID: 31993
	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseOverSound = "Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9";

	// Token: 0x04007CFA RID: 31994
	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseOutSound;

	// Token: 0x04007CFB RID: 31995
	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseDownSound = "Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681";

	// Token: 0x04007CFC RID: 31996
	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseUpSound;

	// Token: 0x04007CFD RID: 31997
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_SelectOnRelease;

	// Token: 0x04007CFE RID: 31998
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_HideMouseOverOnPress;

	// Token: 0x04007CFF RID: 31999
	[SerializeField]
	private bool m_AlwaysOver;

	// Token: 0x04007D00 RID: 32000
	[SerializeField]
	private bool m_EnableResponse = true;

	// Token: 0x04007D01 RID: 32001
	[CustomEditField(Sections = "Allow Selection", Label = "Enable")]
	public bool m_AllowSelection;

	// Token: 0x04007D02 RID: 32002
	[CustomEditField(Parent = "m_AllowSelection")]
	public GameObject m_SelectedHighlight;

	// Token: 0x04007D03 RID: 32003
	[CustomEditField(Parent = "m_AllowSelection")]
	public GameObject m_MouseOverSelectedHighlight;

	// Token: 0x04007D04 RID: 32004
	private PegUIElement m_PegUIElement;
}
