using System;
using UnityEngine;

// Token: 0x02000ACE RID: 2766
public class FramedRadioButton : MonoBehaviour
{
	// Token: 0x06009384 RID: 37764 RVA: 0x002FD1CB File Offset: 0x002FB3CB
	public int GetButtonID()
	{
		return this.m_radioButton.GetButtonID();
	}

	// Token: 0x06009385 RID: 37765 RVA: 0x002FD1D8 File Offset: 0x002FB3D8
	public float GetLeftEdgeOffset()
	{
		return this.m_leftEdgeOffset;
	}

	// Token: 0x06009386 RID: 37766 RVA: 0x002FD1E0 File Offset: 0x002FB3E0
	public virtual void Init(FramedRadioButton.FrameType frameType, string text, int buttonID, object userData)
	{
		this.m_radioButton.SetButtonID(buttonID);
		this.m_radioButton.SetUserData(userData);
		this.m_text.Text = text;
		this.m_text.UpdateNow(false);
		this.m_frameFill.SetActive(true);
		bool flag = false;
		bool active = false;
		switch (frameType)
		{
		case FramedRadioButton.FrameType.SINGLE:
			flag = true;
			active = true;
			break;
		case FramedRadioButton.FrameType.MULTI_LEFT_END:
			flag = true;
			active = false;
			break;
		case FramedRadioButton.FrameType.MULTI_RIGHT_END:
			flag = false;
			active = true;
			break;
		case FramedRadioButton.FrameType.MULTI_MIDDLE:
			flag = false;
			active = false;
			break;
		}
		this.m_frameEndLeft.SetActive(flag);
		this.m_frameLeft.SetActive(!flag);
		this.m_frameEndRight.SetActive(active);
		Transform transform = flag ? this.m_frameEndLeft.transform : this.m_frameLeft.transform;
		this.m_leftEdgeOffset = transform.position.x - base.transform.position.x;
	}

	// Token: 0x06009387 RID: 37767 RVA: 0x002FD2C1 File Offset: 0x002FB4C1
	public void Show()
	{
		this.m_root.SetActive(true);
	}

	// Token: 0x06009388 RID: 37768 RVA: 0x002FD2CF File Offset: 0x002FB4CF
	public void Hide()
	{
		this.m_root.SetActive(false);
	}

	// Token: 0x06009389 RID: 37769 RVA: 0x002FD2E0 File Offset: 0x002FB4E0
	public Bounds GetBounds()
	{
		Bounds bounds = this.m_frameFill.GetComponent<Renderer>().bounds;
		this.IncludeBoundsOfGameObject(this.m_frameEndLeft, ref bounds);
		this.IncludeBoundsOfGameObject(this.m_frameEndRight, ref bounds);
		this.IncludeBoundsOfGameObject(this.m_frameLeft, ref bounds);
		return bounds;
	}

	// Token: 0x0600938A RID: 37770 RVA: 0x002FD32C File Offset: 0x002FB52C
	private void IncludeBoundsOfGameObject(GameObject go, ref Bounds bounds)
	{
		if (!go.activeSelf)
		{
			return;
		}
		Bounds bounds2 = go.GetComponent<Renderer>().bounds;
		Vector3 max = Vector3.Max(bounds2.max, bounds.max);
		Vector3 min = Vector3.Min(bounds2.min, bounds.min);
		bounds.SetMinMax(min, max);
	}

	// Token: 0x04007B92 RID: 31634
	public GameObject m_root;

	// Token: 0x04007B93 RID: 31635
	public GameObject m_frameEndLeft;

	// Token: 0x04007B94 RID: 31636
	public GameObject m_frameEndRight;

	// Token: 0x04007B95 RID: 31637
	public GameObject m_frameLeft;

	// Token: 0x04007B96 RID: 31638
	public GameObject m_frameFill;

	// Token: 0x04007B97 RID: 31639
	public RadioButton m_radioButton;

	// Token: 0x04007B98 RID: 31640
	public UberText m_text;

	// Token: 0x04007B99 RID: 31641
	private float m_leftEdgeOffset;

	// Token: 0x020026FE RID: 9982
	public enum FrameType
	{
		// Token: 0x0400F2F7 RID: 62199
		SINGLE,
		// Token: 0x0400F2F8 RID: 62200
		MULTI_LEFT_END,
		// Token: 0x0400F2F9 RID: 62201
		MULTI_RIGHT_END,
		// Token: 0x0400F2FA RID: 62202
		MULTI_MIDDLE
	}
}
