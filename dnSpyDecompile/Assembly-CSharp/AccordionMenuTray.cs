using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AC7 RID: 2759
public class AccordionMenuTray : MonoBehaviour
{
	// Token: 0x1700086F RID: 2159
	// (get) Token: 0x06009334 RID: 37684 RVA: 0x002FBB71 File Offset: 0x002F9D71
	// (set) Token: 0x06009335 RID: 37685 RVA: 0x002FBB79 File Offset: 0x002F9D79
	[CustomEditField(Sections = "Behavior Settings")]
	public float ButtonOffset
	{
		get
		{
			return this.m_ButtonOffset;
		}
		set
		{
			this.m_ButtonOffset = value;
			this.OnButtonVisualUpdated();
		}
	}

	// Token: 0x06009336 RID: 37686 RVA: 0x002FBB88 File Offset: 0x002F9D88
	protected void OnButtonVisualUpdated()
	{
		float num = 0f;
		ChooserButton[] array = this.m_ChooserButtons.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			TransformUtil.SetLocalPosZ(array[i].transform, -num);
			num += array[i].GetFullButtonHeight() + this.m_ButtonOffset;
		}
	}

	// Token: 0x06009337 RID: 37687 RVA: 0x002FBBD8 File Offset: 0x002F9DD8
	protected void OnChooserButtonToggled(ChooserButton btn, bool toggled, int index)
	{
		btn.SetSelectSubButtonOnToggle(this.m_OnlyOneExpands);
		if (this.m_OnlyOneExpands)
		{
			if (toggled)
			{
				this.ToggleScrollable(false);
				ChooserButton[] array = this.m_ChooserButtons.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (i != index)
					{
						array[i].Toggle = false;
					}
				}
				return;
			}
		}
		else if (this.m_SelectedSubButton != null)
		{
			btn = this.m_ChooserButtons[index];
			if (btn.ContainsSubButton(this.m_SelectedSubButton))
			{
				this.m_SelectedSubButton.SetHighlight(toggled);
				if (!toggled)
				{
					this.m_ChooseButton.Disable(false);
					return;
				}
				if (!this.m_AttemptedLoad)
				{
					this.m_ChooseButton.Enable();
				}
			}
		}
	}

	// Token: 0x06009338 RID: 37688 RVA: 0x002FBC88 File Offset: 0x002F9E88
	protected void ToggleScrollable(bool enable)
	{
		if (this.m_ChooseFrameScroller != null && this.m_ChooseFrameScroller.enabled != enable)
		{
			Log.FiresideGatherings.Print("FiresideGatheringChooserTray.ToggleScrollable: " + enable.ToString(), Array.Empty<object>());
			this.m_ChooseFrameScroller.enabled = enable;
		}
	}

	// Token: 0x04007B56 RID: 31574
	[CustomEditField(Sections = "Description")]
	public UberText m_DescriptionTitleObject;

	// Token: 0x04007B57 RID: 31575
	[CustomEditField(Sections = "Description")]
	public GameObject m_DescriptionContainer;

	// Token: 0x04007B58 RID: 31576
	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public PlayButton m_ChooseButton;

	// Token: 0x04007B59 RID: 31577
	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UIBButton m_BackButton;

	// Token: 0x04007B5A RID: 31578
	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserButtonPrefab;

	// Token: 0x04007B5B RID: 31579
	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserSubButtonPrefab;

	// Token: 0x04007B5C RID: 31580
	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserComingSoonSubButtonPrefab;

	// Token: 0x04007B5D RID: 31581
	[CustomEditField(Sections = "Choose Frame")]
	public UIBScrollable m_ChooseFrameScroller;

	// Token: 0x04007B5E RID: 31582
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_OnlyOneExpands;

	// Token: 0x04007B5F RID: 31583
	[SerializeField]
	private float m_ButtonOffset = -2.5f;

	// Token: 0x04007B60 RID: 31584
	protected ChooserSubButton m_SelectedSubButton;

	// Token: 0x04007B61 RID: 31585
	protected List<ChooserButton> m_ChooserButtons = new List<ChooserButton>();

	// Token: 0x04007B62 RID: 31586
	protected bool m_isStarted;

	// Token: 0x04007B63 RID: 31587
	protected bool m_AttemptedLoad;
}
