using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class BoxDrawer : MonoBehaviour
{
	// Token: 0x06000C4B RID: 3147 RVA: 0x000483E8 File Offset: 0x000465E8
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x000483F0 File Offset: 0x000465F0
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x000483F9 File Offset: 0x000465F9
	public BoxDrawerStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00048401 File Offset: 0x00046601
	public void SetInfo(BoxDrawerStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0004840C File Offset: 0x0004660C
	public bool ChangeState(BoxDrawer.State state)
	{
		if (DemoMgr.Get().GetMode() == DemoMode.PAX_EAST_2013)
		{
			return true;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2013)
		{
			return true;
		}
		if (this.m_state == state)
		{
			return false;
		}
		BoxDrawer.State state2 = this.m_state;
		this.m_state = state;
		if (this.IsInactiveState(state2) && this.IsInactiveState(this.m_state))
		{
			return true;
		}
		base.gameObject.SetActive(true);
		if (state == BoxDrawer.State.CLOSED)
		{
			this.m_parent.OnAnimStarted();
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_info.m_ClosedBone.transform.position,
				"delay",
				this.m_info.m_ClosedDelaySec,
				"time",
				this.m_info.m_ClosedMoveSec,
				"easeType",
				this.m_info.m_ClosedMoveEaseType,
				"oncomplete",
				"OnClosedAnimFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.MoveTo(base.gameObject, args);
			this.m_parent.GetEventSpell(BoxEventType.DRAWER_CLOSE).Activate();
		}
		else if (state == BoxDrawer.State.CLOSED_BOX_OPENED)
		{
			this.m_parent.OnAnimStarted();
			Hashtable args2 = iTween.Hash(new object[]
			{
				"position",
				this.m_info.m_ClosedBoxOpenedBone.transform.position,
				"delay",
				this.m_info.m_ClosedBoxOpenedDelaySec,
				"time",
				this.m_info.m_ClosedBoxOpenedMoveSec,
				"easeType",
				this.m_info.m_ClosedBoxOpenedMoveEaseType,
				"oncomplete",
				"OnClosedBoxOpenedAnimFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.MoveTo(base.gameObject, args2);
			this.m_parent.GetEventSpell(BoxEventType.DRAWER_CLOSE).Activate();
		}
		else if (state == BoxDrawer.State.OPENED)
		{
			this.m_parent.OnAnimStarted();
			Hashtable args3 = iTween.Hash(new object[]
			{
				"position",
				this.m_info.m_OpenedBone.transform.position,
				"delay",
				this.m_info.m_OpenedDelaySec,
				"time",
				this.m_info.m_OpenedMoveSec,
				"easeType",
				this.m_info.m_OpenedMoveEaseType,
				"oncomplete",
				"OnOpenedAnimFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.MoveTo(base.gameObject, args3);
			this.m_parent.GetEventSpell(BoxEventType.DRAWER_OPEN).Activate();
		}
		return true;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00048704 File Offset: 0x00046904
	public void UpdateState(BoxDrawer.State state)
	{
		this.m_state = state;
		if (state == BoxDrawer.State.CLOSED)
		{
			base.transform.position = this.m_info.m_ClosedBone.transform.position;
			base.gameObject.SetActive(false);
			return;
		}
		if (state == BoxDrawer.State.CLOSED_BOX_OPENED)
		{
			base.transform.position = this.m_info.m_ClosedBoxOpenedBone.transform.position;
			base.gameObject.SetActive(false);
			return;
		}
		if (state == BoxDrawer.State.OPENED)
		{
			base.transform.position = this.m_info.m_OpenedBone.transform.position;
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x000487A9 File Offset: 0x000469A9
	private bool IsInactiveState(BoxDrawer.State state)
	{
		return state == BoxDrawer.State.CLOSED || state == BoxDrawer.State.CLOSED_BOX_OPENED;
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x000487B4 File Offset: 0x000469B4
	private void OnClosedAnimFinished()
	{
		base.gameObject.SetActive(false);
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000487B4 File Offset: 0x000469B4
	private void OnClosedBoxOpenedAnimFinished()
	{
		base.gameObject.SetActive(false);
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000487CD File Offset: 0x000469CD
	private void OnOpenedAnimFinished()
	{
		base.gameObject.SetActive(true);
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x04000860 RID: 2144
	private Box m_parent;

	// Token: 0x04000861 RID: 2145
	private BoxDrawerStateInfo m_info;

	// Token: 0x04000862 RID: 2146
	private BoxDrawer.State m_state;

	// Token: 0x020013DD RID: 5085
	public enum State
	{
		// Token: 0x0400A81B RID: 43035
		CLOSED,
		// Token: 0x0400A81C RID: 43036
		CLOSED_BOX_OPENED,
		// Token: 0x0400A81D RID: 43037
		OPENED
	}
}
