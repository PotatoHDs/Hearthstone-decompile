using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class BoxCamera : MonoBehaviour
{
	// Token: 0x06000C2A RID: 3114 RVA: 0x00047414 File Offset: 0x00045614
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004741D File Offset: 0x0004561D
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00047425 File Offset: 0x00045625
	public BoxCameraStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0004742D File Offset: 0x0004562D
	public void SetInfo(BoxCameraStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00047436 File Offset: 0x00045636
	public BoxCameraEventTable GetEventTable()
	{
		return this.m_EventTable;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00047440 File Offset: 0x00045640
	public Vector3 GetCameraPosition(BoxCamera.State state)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			Transform transform;
			Transform transform2;
			Transform transform3;
			if (state != BoxCamera.State.CLOSED)
			{
				if (state != BoxCamera.State.CLOSED_WITH_DRAWER)
				{
					transform = this.m_info.m_OpenedMinAspectRatioBone.transform;
					transform2 = this.m_info.m_OpenedBone.transform;
					transform3 = this.m_info.m_OpenedExtraWideAspectRatioBone.transform;
				}
				else
				{
					transform = this.m_info.m_ClosedWithDrawerMinAspectRatioBone.transform;
					transform2 = this.m_info.m_ClosedWithDrawerBone.transform;
					transform3 = this.m_info.m_ClosedWithDrawerExtraWideAspectRatioBone.transform;
				}
			}
			else
			{
				transform = this.m_info.m_ClosedMinAspectRatioBone.transform;
				transform2 = this.m_info.m_ClosedBone.transform;
				transform3 = this.m_info.m_ClosedExtraWideAspectRatioBone.transform;
			}
			return TransformUtil.GetAspectRatioDependentPosition(transform.position, transform2.position, transform3.position);
		}
		if (state == BoxCamera.State.CLOSED)
		{
			return this.m_info.m_ClosedBone.transform.position;
		}
		if (state != BoxCamera.State.CLOSED_WITH_DRAWER)
		{
			return this.m_info.m_OpenedBone.transform.position;
		}
		return this.m_info.m_ClosedWithDrawerBone.transform.position;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00047564 File Offset: 0x00045764
	public BoxCamera.State GetState()
	{
		return this.m_state;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0004756C File Offset: 0x0004576C
	public bool ChangeState(BoxCamera.State state)
	{
		if (this.m_state == state)
		{
			return false;
		}
		Vector3 cameraPosition = this.GetCameraPosition(state);
		this.m_parent.OnAnimStarted();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_applyAccelerometer = false;
			this.m_basePosition = base.transform.parent.InverseTransformPoint(cameraPosition);
			this.m_lookAtPoint = base.transform.parent.InverseTransformPoint(new Vector3(cameraPosition.x, 1.5f, cameraPosition.z));
			if (cameraPosition == base.gameObject.transform.position)
			{
				this.OnAnimFinished(state);
				return true;
			}
		}
		Hashtable args = null;
		if (state == BoxCamera.State.CLOSED)
		{
			args = iTween.Hash(new object[]
			{
				"position",
				cameraPosition,
				"delay",
				this.m_info.m_ClosedDelaySec,
				"time",
				this.m_info.m_ClosedMoveSec,
				"easeType",
				this.m_info.m_ClosedMoveEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompleteparams",
				state,
				"oncompletetarget",
				base.gameObject
			});
		}
		else if (state == BoxCamera.State.CLOSED_WITH_DRAWER)
		{
			args = iTween.Hash(new object[]
			{
				"position",
				cameraPosition,
				"delay",
				this.m_info.m_ClosedWithDrawerDelaySec,
				"time",
				this.m_info.m_ClosedWithDrawerMoveSec,
				"easeType",
				this.m_info.m_ClosedWithDrawerMoveEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompleteparams",
				state,
				"oncompletetarget",
				base.gameObject
			});
		}
		else if (state == BoxCamera.State.OPENED)
		{
			args = iTween.Hash(new object[]
			{
				"position",
				cameraPosition,
				"delay",
				this.m_info.m_OpenedDelaySec,
				"time",
				this.m_info.m_OpenedMoveSec,
				"easeType",
				this.m_info.m_OpenedMoveEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompleteparams",
				state,
				"oncompletetarget",
				base.gameObject
			});
		}
		else if (state == BoxCamera.State.SET_ROTATION_OPENED)
		{
			args = iTween.Hash(new object[]
			{
				"position",
				cameraPosition,
				"delay",
				this.m_info.m_OpenedDelaySec,
				"time",
				1.5f,
				"easeType",
				this.m_info.m_OpenedMoveEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompleteparams",
				state,
				"oncompletetarget",
				base.gameObject
			});
		}
		CameraShakeMgr.Stop(base.GetComponent<Camera>(), 0f);
		iTween.MoveTo(base.gameObject, args);
		return true;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void EnableAccelerometer()
	{
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x000478EC File Offset: 0x00045AEC
	public void Update()
	{
		if (this.m_disableAccelerometer || base.transform.parent.gameObject.GetComponent<LoadingScreen>() != null)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			if (this.m_applyAccelerometer)
			{
				this.m_gyroRotation.x = Input.gyro.rotationRateUnbiased.x;
				this.m_gyroRotation.y = -Input.gyro.rotationRateUnbiased.y;
				this.m_currentAngle.x = this.m_currentAngle.x + this.m_gyroRotation.y * this.ROTATION_SCALE;
				this.m_currentAngle.y = this.m_currentAngle.y + this.m_gyroRotation.x * this.ROTATION_SCALE;
				this.m_currentAngle.x = Mathf.Clamp(this.m_currentAngle.x, -this.MAX_GYRO_RANGE, this.MAX_GYRO_RANGE);
				this.m_currentAngle.y = Mathf.Clamp(this.m_currentAngle.y, -this.MAX_GYRO_RANGE, this.MAX_GYRO_RANGE);
				base.gameObject.transform.localPosition = new Vector3(this.m_basePosition.x, this.m_basePosition.y, this.m_basePosition.z + this.m_currentAngle.y);
			}
			Vector3 worldUp = new Vector3(0f, 0f, 1f);
			Vector3 worldPosition = base.gameObject.transform.parent.TransformPoint(this.m_lookAtPoint);
			base.gameObject.transform.LookAt(worldPosition, worldUp);
			if (this.m_applyAccelerometer)
			{
				this.m_IgnoreFullscreenEffectsCamera.transform.position = base.gameObject.transform.parent.TransformPoint(this.m_basePosition);
				this.m_IgnoreFullscreenEffectsCamera.transform.LookAt(worldPosition, worldUp);
				this.m_TooltipCamera.transform.position = base.gameObject.transform.parent.TransformPoint(this.m_basePosition);
				this.m_TooltipCamera.transform.LookAt(worldPosition, worldUp);
				return;
			}
			TransformUtil.Identity(this.m_TooltipCamera);
			TransformUtil.Identity(this.m_IgnoreFullscreenEffectsCamera);
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00047B20 File Offset: 0x00045D20
	public void OnAnimFinished(BoxCamera.State state)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_applyAccelerometer = (this.m_state != BoxCamera.State.OPENED);
			this.m_currentAngle = new Vector2(0f, 0f);
		}
		this.m_state = state;
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00047B72 File Offset: 0x00045D72
	public void UpdateState(BoxCamera.State state)
	{
		this.m_state = state;
		base.transform.position = this.GetCameraPosition(state);
	}

	// Token: 0x0400082C RID: 2092
	public BoxCameraEventTable m_EventTable;

	// Token: 0x0400082D RID: 2093
	public GameObject m_IgnoreFullscreenEffectsCamera;

	// Token: 0x0400082E RID: 2094
	public GameObject m_TooltipCamera;

	// Token: 0x0400082F RID: 2095
	private Box m_parent;

	// Token: 0x04000830 RID: 2096
	private BoxCameraStateInfo m_info;

	// Token: 0x04000831 RID: 2097
	private BoxCamera.State m_state;

	// Token: 0x04000832 RID: 2098
	private bool m_disableAccelerometer = true;

	// Token: 0x04000833 RID: 2099
	private bool m_applyAccelerometer;

	// Token: 0x04000834 RID: 2100
	private Vector2 m_currentAngle;

	// Token: 0x04000835 RID: 2101
	private Vector3 m_basePosition;

	// Token: 0x04000836 RID: 2102
	private Vector2 m_gyroRotation;

	// Token: 0x04000837 RID: 2103
	private float m_offset;

	// Token: 0x04000838 RID: 2104
	private float MAX_GYRO_RANGE = 2.1f;

	// Token: 0x04000839 RID: 2105
	private float ROTATION_SCALE = 0.085f;

	// Token: 0x0400083A RID: 2106
	private Vector3 m_lookAtPoint;

	// Token: 0x020013DA RID: 5082
	public enum State
	{
		// Token: 0x0400A80F RID: 43023
		UNKNOWN = -1,
		// Token: 0x0400A810 RID: 43024
		CLOSED,
		// Token: 0x0400A811 RID: 43025
		CLOSED_WITH_DRAWER,
		// Token: 0x0400A812 RID: 43026
		OPENED,
		// Token: 0x0400A813 RID: 43027
		SET_ROTATION_OPENED
	}
}
