using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000B2C RID: 2860
[ExecuteAlways]
public class StandardPegButtonNew : PegUIElement
{
	// Token: 0x0600979C RID: 38812 RVA: 0x00310600 File Offset: 0x0030E800
	public void SetText(string t)
	{
		this.m_buttonText.Text = t;
	}

	// Token: 0x0600979D RID: 38813 RVA: 0x00310610 File Offset: 0x0030E810
	public void SetWidth(float globalWidth)
	{
		this.m_button.SetWidth(globalWidth * 0.88f);
		if (this.m_border != null)
		{
			this.m_border.SetWidth(globalWidth);
		}
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.Euler(Vector3.zero);
		Vector3 size = this.m_button.GetSize();
		Vector3 vector = TransformUtil.ComputeWorldScale(base.transform);
		Vector3 size2 = new Vector3(size.x / vector.x, size.z / vector.z, size.y / vector.y);
		base.GetComponent<BoxCollider>().size = size2;
		base.transform.rotation = rotation;
	}

	// Token: 0x0600979E RID: 38814 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600979F RID: 38815 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060097A0 RID: 38816 RVA: 0x003106C8 File Offset: 0x0030E8C8
	public void Disable()
	{
		this.m_button.transform.localRotation = Quaternion.Euler(new Vector3(180f, 180f, 0f));
		this.SetEnabled(false, false);
	}

	// Token: 0x060097A1 RID: 38817 RVA: 0x003106FB File Offset: 0x0030E8FB
	public void Enable()
	{
		this.m_button.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this.SetEnabled(true, false);
	}

	// Token: 0x060097A2 RID: 38818 RVA: 0x0031072E File Offset: 0x0030E92E
	public void Reset()
	{
		iTween.StopByName(this.m_button.gameObject, "rotation");
		this.m_button.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
	}

	// Token: 0x060097A3 RID: 38819 RVA: 0x0031076E File Offset: 0x0030E96E
	public void LockHighlight()
	{
		this.m_highlight.gameObject.SetActive(true);
		this.m_highlightLocked = true;
	}

	// Token: 0x060097A4 RID: 38820 RVA: 0x00310788 File Offset: 0x0030E988
	public void UnlockHighlight()
	{
		this.m_highlight.gameObject.SetActive(false);
		this.m_highlightLocked = false;
	}

	// Token: 0x060097A5 RID: 38821 RVA: 0x003107A4 File Offset: 0x0030E9A4
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_highlightLocked)
		{
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(90f, 0f, 0f),
			"time",
			0.5f,
			"name",
			"rotation"
		});
		iTween.StopByName(this.m_button.gameObject, "rotation");
		this.m_button.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		iTween.PunchRotation(this.m_button.gameObject, args);
		this.m_highlight.gameObject.SetActive(true);
		if (SoundManager.Get() != null && SoundManager.Get().GetConfig() != null)
		{
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		}
	}

	// Token: 0x060097A6 RID: 38822 RVA: 0x0031089C File Offset: 0x0030EA9C
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_button.transform.localPosition = this.m_upBone.transform.localPosition;
		if (this.m_highlightLocked)
		{
			return;
		}
		this.m_highlight.gameObject.SetActive(false);
	}

	// Token: 0x060097A7 RID: 38823 RVA: 0x003108D8 File Offset: 0x0030EAD8
	protected override void OnPress()
	{
		this.m_button.transform.localPosition = this.m_downBone.transform.localPosition;
		if (SoundManager.Get() != null && SoundManager.Get().GetConfig() != null)
		{
			SoundManager.Get().LoadAndPlay("Back_Click.prefab:f7df4bfeab7ccff4198e670ca516da2e");
		}
	}

	// Token: 0x060097A8 RID: 38824 RVA: 0x00310932 File Offset: 0x0030EB32
	protected override void OnRelease()
	{
		this.m_button.transform.localPosition = this.m_upBone.transform.localPosition;
	}

	// Token: 0x04007EF5 RID: 32501
	public UberText m_buttonText;

	// Token: 0x04007EF6 RID: 32502
	public ThreeSliceElement m_button;

	// Token: 0x04007EF7 RID: 32503
	public ThreeSliceElement m_border;

	// Token: 0x04007EF8 RID: 32504
	public ThreeSliceElement m_highlight;

	// Token: 0x04007EF9 RID: 32505
	public GameObject m_upBone;

	// Token: 0x04007EFA RID: 32506
	public GameObject m_downBone;

	// Token: 0x04007EFB RID: 32507
	public float m_buttonWidth;

	// Token: 0x04007EFC RID: 32508
	public bool m_ExecuteInEditMode;

	// Token: 0x04007EFD RID: 32509
	private bool m_highlightLocked;

	// Token: 0x04007EFE RID: 32510
	private const float HIGHLIGHT_SCALE = 1.2f;

	// Token: 0x04007EFF RID: 32511
	private const float GRAY_FRAME_SCALE = 0.88f;
}
