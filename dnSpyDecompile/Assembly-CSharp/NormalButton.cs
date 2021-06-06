using System;
using UnityEngine;

// Token: 0x02000AD6 RID: 2774
[CustomEditClass]
public class NormalButton : PegUIElement
{
	// Token: 0x060093C4 RID: 37828 RVA: 0x002FFCC6 File Offset: 0x002FDEC6
	protected override void Awake()
	{
		this.SetOriginalButtonPosition();
	}

	// Token: 0x060093C5 RID: 37829 RVA: 0x002FFCD0 File Offset: 0x002FDED0
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_mouseOverBone != null)
		{
			this.m_button.transform.position = this.m_mouseOverBone.transform.position;
			return;
		}
		TransformUtil.SetLocalPosY(this.m_button.gameObject, this.m_originalButtonPosition.y + this.m_userOverYOffset);
	}

	// Token: 0x060093C6 RID: 37830 RVA: 0x002FFD2E File Offset: 0x002FDF2E
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_button.gameObject.transform.localPosition = this.m_originalButtonPosition;
	}

	// Token: 0x060093C7 RID: 37831 RVA: 0x002FFD4B File Offset: 0x002FDF4B
	public void SetUserOverYOffset(float userOverYOffset)
	{
		this.m_userOverYOffset = userOverYOffset;
	}

	// Token: 0x060093C8 RID: 37832 RVA: 0x002FFD54 File Offset: 0x002FDF54
	public void SetButtonID(int newID)
	{
		this.buttonID = newID;
	}

	// Token: 0x060093C9 RID: 37833 RVA: 0x002FFD5D File Offset: 0x002FDF5D
	public int GetButtonID()
	{
		return this.buttonID;
	}

	// Token: 0x060093CA RID: 37834 RVA: 0x002FFD65 File Offset: 0x002FDF65
	public void SetText(string t)
	{
		if (this.m_buttonUberText == null)
		{
			this.m_buttonText.text = t;
			return;
		}
		this.m_buttonUberText.Text = t;
	}

	// Token: 0x060093CB RID: 37835 RVA: 0x002FFD90 File Offset: 0x002FDF90
	public float GetTextWidth()
	{
		if (this.m_buttonUberText == null)
		{
			return this.m_buttonText.GetComponent<Renderer>().bounds.extents.x * 2f;
		}
		return this.m_buttonUberText.Width;
	}

	// Token: 0x060093CC RID: 37836 RVA: 0x002FFDDC File Offset: 0x002FDFDC
	public float GetTextHeight()
	{
		if (this.m_buttonUberText == null)
		{
			return this.m_buttonText.GetComponent<Renderer>().bounds.extents.y * 2f;
		}
		return this.m_buttonUberText.Height;
	}

	// Token: 0x060093CD RID: 37837 RVA: 0x002FFE28 File Offset: 0x002FE028
	public float GetRight()
	{
		return base.GetComponent<BoxCollider>().bounds.max.x;
	}

	// Token: 0x060093CE RID: 37838 RVA: 0x002FFE50 File Offset: 0x002FE050
	public float GetLeft()
	{
		Bounds bounds = base.GetComponent<BoxCollider>().bounds;
		return bounds.center.x - bounds.extents.x;
	}

	// Token: 0x060093CF RID: 37839 RVA: 0x002FFE84 File Offset: 0x002FE084
	public float GetTop()
	{
		Bounds bounds = base.GetComponent<BoxCollider>().bounds;
		return bounds.center.y + bounds.extents.y;
	}

	// Token: 0x060093D0 RID: 37840 RVA: 0x002FFEB8 File Offset: 0x002FE0B8
	public float GetBottom()
	{
		Bounds bounds = base.GetComponent<BoxCollider>().bounds;
		return bounds.center.y - bounds.extents.y;
	}

	// Token: 0x060093D1 RID: 37841 RVA: 0x002FFEEA File Offset: 0x002FE0EA
	public void SetOriginalButtonPosition()
	{
		this.m_originalButtonPosition = this.m_button.transform.localPosition;
	}

	// Token: 0x060093D2 RID: 37842 RVA: 0x002FFF02 File Offset: 0x002FE102
	public GameObject GetButtonTextGO()
	{
		if (this.m_buttonUberText == null)
		{
			return this.m_buttonText.gameObject;
		}
		return this.m_buttonUberText.gameObject;
	}

	// Token: 0x060093D3 RID: 37843 RVA: 0x002FFF29 File Offset: 0x002FE129
	public UberText GetButtonUberText()
	{
		return this.m_buttonUberText;
	}

	// Token: 0x060093D4 RID: 37844 RVA: 0x002FFF31 File Offset: 0x002FE131
	public string GetText()
	{
		if (this.m_buttonUberText == null)
		{
			return this.m_buttonText.text;
		}
		return this.m_buttonUberText.Text;
	}

	// Token: 0x04007BEA RID: 31722
	[CustomEditField(Sections = "Button Properties")]
	public GameObject m_button;

	// Token: 0x04007BEB RID: 31723
	[CustomEditField(Sections = "Button Properties")]
	public TextMesh m_buttonText;

	// Token: 0x04007BEC RID: 31724
	[CustomEditField(Sections = "Button Properties")]
	public UberText m_buttonUberText;

	// Token: 0x04007BED RID: 31725
	[CustomEditField(Sections = "Mouse Over Settings")]
	public GameObject m_mouseOverBone;

	// Token: 0x04007BEE RID: 31726
	[CustomEditField(Sections = "Mouse Over Settings")]
	public float m_userOverYOffset = -0.05f;

	// Token: 0x04007BEF RID: 31727
	private Vector3 m_originalButtonPosition;

	// Token: 0x04007BF0 RID: 31728
	private int buttonID;
}
