using System;
using UnityEngine;

// Token: 0x02000AF9 RID: 2809
public class BeveledButton : PegUIElement
{
	// Token: 0x060095A7 RID: 38311 RVA: 0x00307A19 File Offset: 0x00305C19
	protected override void Awake()
	{
		base.Awake();
		base.SetOriginalLocalPosition();
		this.m_highlight.SetActive(false);
	}

	// Token: 0x060095A8 RID: 38312 RVA: 0x00307A34 File Offset: 0x00305C34
	protected override void OnPress()
	{
		Vector3 originalLocalPosition = base.GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y - 0.3f * base.transform.localScale.y, originalLocalPosition.z);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			vector,
			"isLocal",
			true,
			"time",
			0.15f
		}));
	}

	// Token: 0x060095A9 RID: 38313 RVA: 0x00307AC8 File Offset: 0x00305CC8
	protected override void OnRelease()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.GetOriginalLocalPosition(),
			"isLocal",
			true,
			"time",
			0.15f
		}));
	}

	// Token: 0x060095AA RID: 38314 RVA: 0x00307B28 File Offset: 0x00305D28
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		Vector3 originalLocalPosition = base.GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y + 0.5f * base.transform.localScale.y, originalLocalPosition.z);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			vector,
			"isLocal",
			true,
			"time",
			0.15f
		}));
		this.m_highlight.SetActive(true);
	}

	// Token: 0x060095AB RID: 38315 RVA: 0x00307BC8 File Offset: 0x00305DC8
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.GetOriginalLocalPosition(),
			"isLocal",
			true,
			"time",
			0.15f
		}));
		this.m_highlight.SetActive(false);
	}

	// Token: 0x060095AC RID: 38316 RVA: 0x00307C33 File Offset: 0x00305E33
	public void SetText(string text)
	{
		if (this.m_uberLabel != null)
		{
			this.m_uberLabel.Text = text;
			return;
		}
		this.m_label.text = text;
	}

	// Token: 0x060095AD RID: 38317 RVA: 0x00307C5C File Offset: 0x00305E5C
	public void Show()
	{
		base.gameObject.SetActive(true);
		this.m_highlight.SetActive(false);
	}

	// Token: 0x060095AE RID: 38318 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060095AF RID: 38319 RVA: 0x00307C76 File Offset: 0x00305E76
	public UberText GetUberText()
	{
		return this.m_uberLabel;
	}

	// Token: 0x04007D6A RID: 32106
	public TextMesh m_label;

	// Token: 0x04007D6B RID: 32107
	public UberText m_uberLabel;

	// Token: 0x04007D6C RID: 32108
	public GameObject m_highlight;
}
