using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class BoxDisk : MonoBehaviour
{
	// Token: 0x06000C38 RID: 3128 RVA: 0x00047C27 File Offset: 0x00045E27
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x00047C30 File Offset: 0x00045E30
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00047C38 File Offset: 0x00045E38
	public BoxDiskStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00047C40 File Offset: 0x00045E40
	public void SetInfo(BoxDiskStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00047C4C File Offset: 0x00045E4C
	public bool ChangeState(BoxDisk.State state)
	{
		if (this.m_state == state)
		{
			return false;
		}
		this.m_state = state;
		if (state == BoxDisk.State.LOADING)
		{
			this.m_parent.OnAnimStarted();
			Vector3 vector = this.m_info.m_LoadingRotation - base.transform.localRotation.eulerAngles;
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				vector,
				"delay",
				this.m_info.m_LoadingDelaySec,
				"time",
				this.m_info.m_LoadingRotateSec,
				"easeType",
				this.m_info.m_LoadingRotateEaseType,
				"space",
				Space.Self,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.RotateAdd(base.gameObject, args);
			this.m_parent.GetEventSpell(BoxEventType.DISK_LOADING).ActivateState(SpellStateType.BIRTH);
		}
		else if (state == BoxDisk.State.MAINMENU)
		{
			this.m_parent.OnAnimStarted();
			Vector3 vector2 = this.m_info.m_MainMenuRotation - base.transform.localRotation.eulerAngles;
			Hashtable args2 = iTween.Hash(new object[]
			{
				"amount",
				vector2,
				"delay",
				this.m_info.m_MainMenuDelaySec,
				"time",
				this.m_info.m_MainMenuRotateSec,
				"easeType",
				this.m_info.m_MainMenuRotateEaseType,
				"space",
				Space.Self,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.RotateAdd(base.gameObject, args2);
			this.m_parent.GetEventSpell(BoxEventType.DISK_MAIN_MENU).ActivateState(SpellStateType.BIRTH);
		}
		return true;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00047E70 File Offset: 0x00046070
	public void UpdateState(BoxDisk.State state)
	{
		this.m_state = state;
		if (state == BoxDisk.State.LOADING)
		{
			base.transform.localRotation = Quaternion.Euler(this.m_info.m_LoadingRotation);
			this.m_parent.GetEventSpell(BoxEventType.DISK_LOADING).ActivateState(SpellStateType.ACTION);
			return;
		}
		if (state == BoxDisk.State.MAINMENU)
		{
			base.transform.localRotation = Quaternion.Euler(this.m_info.m_MainMenuRotation);
			this.m_parent.GetEventSpell(BoxEventType.DISK_MAIN_MENU).ActivateState(SpellStateType.ACTION);
		}
	}

	// Token: 0x04000843 RID: 2115
	private Box m_parent;

	// Token: 0x04000844 RID: 2116
	private BoxDiskStateInfo m_info;

	// Token: 0x04000845 RID: 2117
	private BoxDisk.State m_state;

	// Token: 0x020013DB RID: 5083
	public enum State
	{
		// Token: 0x0400A815 RID: 43029
		LOADING,
		// Token: 0x0400A816 RID: 43030
		MAINMENU
	}
}
