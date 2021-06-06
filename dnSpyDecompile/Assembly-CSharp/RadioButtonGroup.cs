using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000ADF RID: 2783
public class RadioButtonGroup : MonoBehaviour
{
	// Token: 0x06009451 RID: 37969 RVA: 0x00301648 File Offset: 0x002FF848
	public void ShowButtons(List<RadioButtonGroup.ButtonData> buttonData, RadioButtonGroup.DelButtonSelected buttonSelectedCallback, RadioButtonGroup.DelButtonDoubleClicked buttonDoubleClickedCallback)
	{
		this.m_buttonContainer.SetActive(true);
		int count = buttonData.Count;
		while (this.m_framedRadioButtons.Count > count)
		{
			UnityEngine.Object obj = this.m_framedRadioButtons[0];
			this.m_framedRadioButtons.RemoveAt(0);
			UnityEngine.Object.DestroyImmediate(obj);
		}
		bool flag = 1 == count;
		Vector3 position = this.m_buttonContainer.transform.position;
		GameObject relative = new GameObject();
		RadioButton radioButton = null;
		for (int i = 0; i < count; i++)
		{
			FramedRadioButton framedRadioButton;
			if (this.m_framedRadioButtons.Count > i)
			{
				framedRadioButton = this.m_framedRadioButtons[i];
			}
			else
			{
				framedRadioButton = this.CreateNewFramedRadioButton();
				this.m_framedRadioButtons.Add(framedRadioButton);
			}
			FramedRadioButton.FrameType frameType;
			if (flag)
			{
				frameType = FramedRadioButton.FrameType.SINGLE;
			}
			else if (i == 0)
			{
				frameType = FramedRadioButton.FrameType.MULTI_LEFT_END;
			}
			else if (count - 1 == i)
			{
				frameType = FramedRadioButton.FrameType.MULTI_RIGHT_END;
			}
			else
			{
				frameType = FramedRadioButton.FrameType.MULTI_MIDDLE;
			}
			RadioButtonGroup.ButtonData buttonData2 = buttonData[i];
			framedRadioButton.Show();
			framedRadioButton.Init(frameType, buttonData2.m_text, buttonData2.m_id, buttonData2.m_userData);
			if (buttonData2.m_selected)
			{
				if (radioButton != null)
				{
					Debug.LogWarning("RadioButtonGroup.WaitThenShowButtons(): more than one button was set as selected. Selecting the FIRST provided option.");
					framedRadioButton.m_radioButton.SetSelected(false);
				}
				else
				{
					radioButton = framedRadioButton.m_radioButton;
					radioButton.SetSelected(true);
				}
			}
			else
			{
				framedRadioButton.m_radioButton.SetSelected(false);
			}
			if (i == 0)
			{
				TransformUtil.SetPoint(framedRadioButton.gameObject, Anchor.LEFT, this.m_firstRadioButtonBone, Anchor.LEFT);
			}
			else
			{
				TransformUtil.SetPoint(framedRadioButton.gameObject, new Vector3(0f, 1f, 0.5f), relative, new Vector3(1f, 1f, 0.5f), this.m_spacingFudgeFactor);
			}
			relative = framedRadioButton.m_frameFill;
		}
		position.x -= TransformUtil.GetBoundsOfChildren(this.m_buttonContainer).size.x / 2f;
		this.m_buttonContainer.transform.position = position;
		this.m_buttonSelectedCB = buttonSelectedCallback;
		this.m_buttonDoubleClickedCB = buttonDoubleClickedCallback;
		if (radioButton == null)
		{
			return;
		}
		if (this.m_buttonSelectedCB == null)
		{
			return;
		}
		this.m_buttonSelectedCB(radioButton.GetButtonID(), radioButton.GetUserData());
	}

	// Token: 0x06009452 RID: 37970 RVA: 0x0030186D File Offset: 0x002FFA6D
	public void Hide()
	{
		this.m_buttonContainer.SetActive(false);
	}

	// Token: 0x06009453 RID: 37971 RVA: 0x0030187B File Offset: 0x002FFA7B
	public void SetSpacingFudgeFactor(Vector3 amount)
	{
		this.m_spacingFudgeFactor = amount;
	}

	// Token: 0x06009454 RID: 37972 RVA: 0x00301884 File Offset: 0x002FFA84
	private FramedRadioButton CreateNewFramedRadioButton()
	{
		FramedRadioButton framedRadioButton = UnityEngine.Object.Instantiate<FramedRadioButton>(this.m_framedRadioButtonPrefab);
		framedRadioButton.transform.parent = this.m_buttonContainer.transform;
		framedRadioButton.transform.localPosition = Vector3.zero;
		framedRadioButton.transform.localScale = Vector3.one;
		framedRadioButton.transform.localRotation = Quaternion.identity;
		framedRadioButton.m_radioButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRadioButtonReleased));
		framedRadioButton.m_radioButton.AddEventListener(UIEventType.DOUBLECLICK, new UIEvent.Handler(this.OnRadioButtonDoubleClicked));
		return framedRadioButton;
	}

	// Token: 0x06009455 RID: 37973 RVA: 0x00301914 File Offset: 0x002FFB14
	private void OnRadioButtonReleased(UIEvent e)
	{
		RadioButton radioButton = e.GetElement() as RadioButton;
		if (radioButton == null)
		{
			Debug.LogWarning(string.Format("RadioButtonGroup.OnRadioButtonReleased(): UIEvent {0} source is not a RadioButton!", e));
			return;
		}
		bool flag = radioButton.IsSelected();
		foreach (FramedRadioButton framedRadioButton in this.m_framedRadioButtons)
		{
			RadioButton radioButton2 = framedRadioButton.m_radioButton;
			bool selected = radioButton == radioButton2;
			radioButton2.SetSelected(selected);
		}
		if (this.m_buttonSelectedCB == null)
		{
			return;
		}
		this.m_buttonSelectedCB(radioButton.GetButtonID(), radioButton.GetUserData());
		if (UniversalInputManager.Get().IsTouchMode() && flag)
		{
			this.OnRadioButtonDoubleClicked(e);
		}
	}

	// Token: 0x06009456 RID: 37974 RVA: 0x003019D8 File Offset: 0x002FFBD8
	private void OnRadioButtonDoubleClicked(UIEvent e)
	{
		if (this.m_buttonDoubleClickedCB == null)
		{
			return;
		}
		RadioButton radioButton = e.GetElement() as RadioButton;
		if (radioButton == null)
		{
			Debug.LogWarning(string.Format("RadioButtonGroup.OnRadioButtonDoubleClicked(): UIEvent {0} source is not a RadioButton!", e));
			return;
		}
		FramedRadioButton framedRadioButton = null;
		foreach (FramedRadioButton framedRadioButton2 in this.m_framedRadioButtons)
		{
			if (!(radioButton != framedRadioButton2.m_radioButton))
			{
				framedRadioButton = framedRadioButton2;
				break;
			}
		}
		if (framedRadioButton == null)
		{
			Debug.LogWarning(string.Format("RadioButtonGroup.OnRadioButtonDoubleClicked(): could not find framed radio button for radio button ID {0}", radioButton.GetButtonID()));
			return;
		}
		this.m_buttonDoubleClickedCB(framedRadioButton);
	}

	// Token: 0x04007C4C RID: 31820
	public GameObject m_buttonContainer;

	// Token: 0x04007C4D RID: 31821
	public FramedRadioButton m_framedRadioButtonPrefab;

	// Token: 0x04007C4E RID: 31822
	public GameObject m_firstRadioButtonBone;

	// Token: 0x04007C4F RID: 31823
	private List<FramedRadioButton> m_framedRadioButtons = new List<FramedRadioButton>();

	// Token: 0x04007C50 RID: 31824
	private RadioButtonGroup.DelButtonSelected m_buttonSelectedCB;

	// Token: 0x04007C51 RID: 31825
	private RadioButtonGroup.DelButtonDoubleClicked m_buttonDoubleClickedCB;

	// Token: 0x04007C52 RID: 31826
	private Vector3 m_spacingFudgeFactor = Vector3.zero;

	// Token: 0x02002717 RID: 10007
	// (Invoke) Token: 0x060138DB RID: 80091
	public delegate void DelButtonSelected(int buttonID, object userData);

	// Token: 0x02002718 RID: 10008
	// (Invoke) Token: 0x060138DF RID: 80095
	public delegate void DelButtonDoubleClicked(FramedRadioButton framedRadioButton);

	// Token: 0x02002719 RID: 10009
	public struct ButtonData
	{
		// Token: 0x060138E2 RID: 80098 RVA: 0x00537CE4 File Offset: 0x00535EE4
		public ButtonData(int id, string text, object userData, bool selected)
		{
			this.m_id = id;
			this.m_text = text;
			this.m_userData = userData;
			this.m_selected = selected;
		}

		// Token: 0x0400F362 RID: 62306
		public int m_id;

		// Token: 0x0400F363 RID: 62307
		public string m_text;

		// Token: 0x0400F364 RID: 62308
		public bool m_selected;

		// Token: 0x0400F365 RID: 62309
		public object m_userData;
	}
}
