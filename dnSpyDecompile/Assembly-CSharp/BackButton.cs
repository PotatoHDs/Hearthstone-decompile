using System;
using UnityEngine;

// Token: 0x02000ACC RID: 2764
public class BackButton : PegUIElement
{
	// Token: 0x06009371 RID: 37745 RVA: 0x002FCE41 File Offset: 0x002FB041
	protected override void Awake()
	{
		base.Awake();
		base.SetOriginalLocalPosition();
		this.m_highlight.SetActive(false);
		if (this.m_backText)
		{
			this.m_backText.Text = GameStrings.Get("GLOBAL_BACK");
		}
	}

	// Token: 0x06009372 RID: 37746 RVA: 0x002FCE80 File Offset: 0x002FB080
	protected override void OnPress()
	{
		Vector3 originalLocalPosition = base.GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y - 0.3f, originalLocalPosition.z);
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

	// Token: 0x06009373 RID: 37747 RVA: 0x002FCF00 File Offset: 0x002FB100
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

	// Token: 0x06009374 RID: 37748 RVA: 0x002FCF60 File Offset: 0x002FB160
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		Vector3 originalLocalPosition = base.GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y + 0.5f, originalLocalPosition.z);
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

	// Token: 0x06009375 RID: 37749 RVA: 0x002FCFEC File Offset: 0x002FB1EC
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

	// Token: 0x04007B88 RID: 31624
	public GameObject m_highlight;

	// Token: 0x04007B89 RID: 31625
	public UberText m_backText;

	// Token: 0x04007B8A RID: 31626
	public static KeyCode backKey;
}
