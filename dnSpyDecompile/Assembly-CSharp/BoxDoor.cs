using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class BoxDoor : MonoBehaviour
{
	// Token: 0x06000C40 RID: 3136 RVA: 0x00047F36 File Offset: 0x00046136
	private void Awake()
	{
		this.m_startingPosition = base.gameObject.transform.localPosition;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00047F4E File Offset: 0x0004614E
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00047F56 File Offset: 0x00046156
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00047F5F File Offset: 0x0004615F
	public BoxDoorStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00047F67 File Offset: 0x00046167
	public void SetInfo(BoxDoorStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00047F70 File Offset: 0x00046170
	public void EnableMain(bool enable)
	{
		this.m_main = enable;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00047F79 File Offset: 0x00046179
	private bool IsMain()
	{
		return this.m_main;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00047F84 File Offset: 0x00046184
	public bool ChangeState(BoxDoor.State state)
	{
		if (this.m_state == state)
		{
			return false;
		}
		this.m_state = state;
		if (state == BoxDoor.State.CLOSED)
		{
			this.m_parent.OnAnimStarted();
			Vector3 vector = this.m_info.m_ClosedRotation - this.m_info.m_OpenedRotation;
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				vector,
				"delay",
				this.m_info.m_ClosedDelaySec,
				"time",
				this.m_info.m_ClosedRotateSec,
				"easeType",
				this.m_info.m_ClosedRotateEaseType,
				"space",
				Space.Self,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.RotateAdd(base.gameObject, args);
			if (UniversalInputManager.UsePhoneUI)
			{
				args = iTween.Hash(new object[]
				{
					"position",
					this.m_startingPosition,
					"isLocal",
					true,
					"delay",
					this.m_info.m_ClosedDelaySec,
					"time",
					this.m_info.m_ClosedRotateSec,
					"easeType",
					this.m_info.m_ClosedRotateEaseType
				});
				iTween.MoveTo(base.gameObject, args);
			}
			if (this.IsMain())
			{
				this.m_parent.GetEventSpell(BoxEventType.DOORS_CLOSE).Activate();
				this.m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_IN).ActivateState(SpellStateType.BIRTH);
			}
		}
		else if (state == BoxDoor.State.OPENED)
		{
			this.m_parent.OnAnimStarted();
			Vector3 vector2 = this.m_info.m_OpenedRotation - this.m_info.m_ClosedRotation;
			Hashtable args2 = iTween.Hash(new object[]
			{
				"amount",
				vector2,
				"delay",
				this.m_info.m_OpenedDelaySec,
				"time",
				this.m_info.m_OpenedRotateSec,
				"easeType",
				this.m_info.m_OpenedRotateEaseType,
				"space",
				Space.Self,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.RotateAdd(base.gameObject, args2);
			if (UniversalInputManager.UsePhoneUI)
			{
				Vector3 startingPosition = this.m_startingPosition;
				startingPosition.x *= 1.038f;
				args2 = iTween.Hash(new object[]
				{
					"position",
					startingPosition,
					"isLocal",
					true,
					"delay",
					this.m_info.m_ClosedDelaySec,
					"time",
					this.m_info.m_ClosedRotateSec,
					"easeType",
					this.m_info.m_ClosedRotateEaseType
				});
				iTween.MoveTo(base.gameObject, args2);
			}
			if (this.IsMain())
			{
				this.m_parent.GetEventSpell(BoxEventType.DOORS_OPEN).Activate();
				this.m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_OUT).ActivateState(SpellStateType.BIRTH);
			}
		}
		return true;
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00048324 File Offset: 0x00046524
	public void UpdateState(BoxDoor.State state)
	{
		this.m_state = state;
		if (state == BoxDoor.State.CLOSED)
		{
			base.transform.localRotation = Quaternion.Euler(this.m_info.m_ClosedRotation);
			this.m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_IN).ActivateState(SpellStateType.ACTION);
			return;
		}
		if (state == BoxDoor.State.OPENED)
		{
			base.transform.localRotation = Quaternion.Euler(this.m_info.m_OpenedRotation);
			this.m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_OUT).ActivateState(SpellStateType.ACTION);
		}
	}

	// Token: 0x0400084E RID: 2126
	private const float BOX_SLIDE_PERCENTAGE_PHONE = 1.038f;

	// Token: 0x0400084F RID: 2127
	private Box m_parent;

	// Token: 0x04000850 RID: 2128
	private BoxDoorStateInfo m_info;

	// Token: 0x04000851 RID: 2129
	private BoxDoor.State m_state;

	// Token: 0x04000852 RID: 2130
	private bool m_main;

	// Token: 0x04000853 RID: 2131
	private Vector3 m_startingPosition;

	// Token: 0x020013DC RID: 5084
	public enum State
	{
		// Token: 0x0400A818 RID: 43032
		CLOSED,
		// Token: 0x0400A819 RID: 43033
		OPENED
	}
}
