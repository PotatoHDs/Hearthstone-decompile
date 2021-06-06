using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class MassDisenchantTab : PegUIElement
{
	// Token: 0x06001452 RID: 5202 RVA: 0x00074AD2 File Offset: 0x00072CD2
	protected override void Awake()
	{
		base.Awake();
		this.m_highlight.SetActive(false);
		this.m_originalLocalPos = base.transform.localPosition;
		this.m_originalScale = base.transform.localScale;
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x00074B08 File Offset: 0x00072D08
	private void Start()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnRollover));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnRollout));
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x00074B32 File Offset: 0x00072D32
	public void Show()
	{
		this.m_isVisible = true;
		this.m_root.SetActive(true);
		this.SetEnabled(true, false);
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x00074B4F File Offset: 0x00072D4F
	public void Hide()
	{
		this.m_isVisible = false;
		this.m_root.SetActive(false);
		this.SetEnabled(false, false);
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x00074B6C File Offset: 0x00072D6C
	public bool IsVisible()
	{
		return this.m_isVisible;
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x00074B74 File Offset: 0x00072D74
	public void SetAmount(int amount)
	{
		this.m_amount.Text = amount.ToString();
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x00074B88 File Offset: 0x00072D88
	public void Select()
	{
		if (this.m_isSelected)
		{
			return;
		}
		this.m_isSelected = true;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			MassDisenchantTab.SELECTED_SCALE,
			"time",
			CollectionPageManager.SELECT_TAB_ANIM_TIME,
			"name",
			"scale"
		});
		iTween.StopByName(base.gameObject, "scale");
		iTween.ScaleTo(base.gameObject, args);
		Vector3 originalLocalPos = this.m_originalLocalPos;
		originalLocalPos.y += MassDisenchantTab.SELECTED_LOCAL_Y_OFFSET;
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			originalLocalPos,
			"isLocal",
			true,
			"time",
			CollectionPageManager.SELECT_TAB_ANIM_TIME,
			"name",
			"position"
		});
		iTween.StopByName(base.gameObject, "position");
		iTween.MoveTo(base.gameObject, args2);
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x00074C90 File Offset: 0x00072E90
	public void Deselect()
	{
		if (!this.m_isSelected)
		{
			return;
		}
		this.m_isSelected = false;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			this.m_originalScale,
			"time",
			CollectionPageManager.SELECT_TAB_ANIM_TIME,
			"name",
			"scale"
		});
		iTween.StopByName(base.gameObject, "scale");
		iTween.ScaleTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			this.m_originalLocalPos,
			"isLocal",
			true,
			"time",
			CollectionPageManager.SELECT_TAB_ANIM_TIME,
			"name",
			"position"
		});
		iTween.StopByName(base.gameObject, "position");
		iTween.MoveTo(base.gameObject, args2);
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x00074D84 File Offset: 0x00072F84
	private void OnRollover(UIEvent e)
	{
		this.m_highlight.SetActive(true);
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x00074D92 File Offset: 0x00072F92
	private void OnRollout(UIEvent e)
	{
		this.m_highlight.SetActive(false);
	}

	// Token: 0x04000D89 RID: 3465
	public GameObject m_root;

	// Token: 0x04000D8A RID: 3466
	public GameObject m_highlight;

	// Token: 0x04000D8B RID: 3467
	public UberText m_amount;

	// Token: 0x04000D8C RID: 3468
	private static readonly Vector3 SELECTED_SCALE = new Vector3(0.6f, 0.6f, 0.6f);

	// Token: 0x04000D8D RID: 3469
	private static readonly float SELECTED_LOCAL_Y_OFFSET = 0.3822131f;

	// Token: 0x04000D8E RID: 3470
	private bool m_isSelected;

	// Token: 0x04000D8F RID: 3471
	private Vector3 m_originalLocalPos;

	// Token: 0x04000D90 RID: 3472
	private Vector3 m_originalScale;

	// Token: 0x04000D91 RID: 3473
	private bool m_isVisible;
}
