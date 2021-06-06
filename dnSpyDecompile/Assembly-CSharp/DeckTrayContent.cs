using System;
using UnityEngine;

// Token: 0x0200088F RID: 2191
public class DeckTrayContent : MonoBehaviour
{
	// Token: 0x06007803 RID: 30723 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Awake()
	{
	}

	// Token: 0x06007804 RID: 30724 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x06007805 RID: 30725 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void Show(bool showAll = false)
	{
	}

	// Token: 0x06007806 RID: 30726 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void Hide(bool hideAll = false)
	{
	}

	// Token: 0x06007807 RID: 30727 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnContentLoaded()
	{
	}

	// Token: 0x06007808 RID: 30728 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool IsContentLoaded()
	{
		return true;
	}

	// Token: 0x06007809 RID: 30729 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool PreAnimateContentEntrance()
	{
		return true;
	}

	// Token: 0x0600780A RID: 30730 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool PostAnimateContentEntrance()
	{
		return true;
	}

	// Token: 0x0600780B RID: 30731 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateContentEntranceStart()
	{
		return true;
	}

	// Token: 0x0600780C RID: 30732 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateContentEntranceEnd()
	{
		return true;
	}

	// Token: 0x0600780D RID: 30733 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateContentExitStart()
	{
		return true;
	}

	// Token: 0x0600780E RID: 30734 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateContentExitEnd()
	{
		return true;
	}

	// Token: 0x0600780F RID: 30735 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool PreAnimateContentExit()
	{
		return true;
	}

	// Token: 0x06007810 RID: 30736 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool PostAnimateContentExit()
	{
		return true;
	}

	// Token: 0x06007811 RID: 30737 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
	}

	// Token: 0x06007812 RID: 30738 RVA: 0x00272DD7 File Offset: 0x00270FD7
	public bool IsModeActive()
	{
		return this.m_isModeActive;
	}

	// Token: 0x06007813 RID: 30739 RVA: 0x00272DDF File Offset: 0x00270FDF
	public bool IsModeTryingOrActive()
	{
		return this.m_isModeTrying || this.m_isModeActive;
	}

	// Token: 0x06007814 RID: 30740 RVA: 0x00272DF1 File Offset: 0x00270FF1
	public void SetModeActive(bool active)
	{
		this.m_isModeActive = active;
	}

	// Token: 0x06007815 RID: 30741 RVA: 0x00272DFA File Offset: 0x00270FFA
	public void SetModeTrying(bool trying)
	{
		this.m_isModeTrying = trying;
	}

	// Token: 0x04005DD5 RID: 24021
	private bool m_isModeActive;

	// Token: 0x04005DD6 RID: 24022
	private bool m_isModeTrying;
}
