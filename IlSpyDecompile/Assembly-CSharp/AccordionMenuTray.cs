using System.Collections.Generic;
using UnityEngine;

public class AccordionMenuTray : MonoBehaviour
{
	[CustomEditField(Sections = "Description")]
	public UberText m_DescriptionTitleObject;

	[CustomEditField(Sections = "Description")]
	public GameObject m_DescriptionContainer;

	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public PlayButton m_ChooseButton;

	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UIBButton m_BackButton;

	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserButtonPrefab;

	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserSubButtonPrefab;

	[CustomEditField(Sections = "Choose Frame", T = EditType.GAME_OBJECT)]
	[SerializeField]
	public string m_DefaultChooserComingSoonSubButtonPrefab;

	[CustomEditField(Sections = "Choose Frame")]
	public UIBScrollable m_ChooseFrameScroller;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_OnlyOneExpands;

	[SerializeField]
	private float m_ButtonOffset = -2.5f;

	protected ChooserSubButton m_SelectedSubButton;

	protected List<ChooserButton> m_ChooserButtons = new List<ChooserButton>();

	protected bool m_isStarted;

	protected bool m_AttemptedLoad;

	[CustomEditField(Sections = "Behavior Settings")]
	public float ButtonOffset
	{
		get
		{
			return m_ButtonOffset;
		}
		set
		{
			m_ButtonOffset = value;
			OnButtonVisualUpdated();
		}
	}

	protected void OnButtonVisualUpdated()
	{
		float num = 0f;
		ChooserButton[] array = m_ChooserButtons.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			TransformUtil.SetLocalPosZ(array[i].transform, 0f - num);
			num += array[i].GetFullButtonHeight() + m_ButtonOffset;
		}
	}

	protected void OnChooserButtonToggled(ChooserButton btn, bool toggled, int index)
	{
		btn.SetSelectSubButtonOnToggle(m_OnlyOneExpands);
		if (m_OnlyOneExpands)
		{
			if (!toggled)
			{
				return;
			}
			ToggleScrollable(enable: false);
			ChooserButton[] array = m_ChooserButtons.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (i != index)
				{
					array[i].Toggle = false;
				}
			}
		}
		else
		{
			if (!(m_SelectedSubButton != null))
			{
				return;
			}
			btn = m_ChooserButtons[index];
			if (btn.ContainsSubButton(m_SelectedSubButton))
			{
				m_SelectedSubButton.SetHighlight(toggled);
				if (!toggled)
				{
					m_ChooseButton.Disable();
				}
				else if (!m_AttemptedLoad)
				{
					m_ChooseButton.Enable();
				}
			}
		}
	}

	protected void ToggleScrollable(bool enable)
	{
		if (m_ChooseFrameScroller != null && m_ChooseFrameScroller.enabled != enable)
		{
			Log.FiresideGatherings.Print("FiresideGatheringChooserTray.ToggleScrollable: " + enable);
			m_ChooseFrameScroller.enabled = enable;
		}
	}
}
