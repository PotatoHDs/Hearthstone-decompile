using System.Collections;
using UnityEngine;

public class BoxCamera : MonoBehaviour
{
	public enum State
	{
		UNKNOWN = -1,
		CLOSED,
		CLOSED_WITH_DRAWER,
		OPENED,
		SET_ROTATION_OPENED
	}

	public BoxCameraEventTable m_EventTable;

	public GameObject m_IgnoreFullscreenEffectsCamera;

	public GameObject m_TooltipCamera;

	private Box m_parent;

	private BoxCameraStateInfo m_info;

	private State m_state;

	private bool m_disableAccelerometer = true;

	private bool m_applyAccelerometer;

	private Vector2 m_currentAngle;

	private Vector3 m_basePosition;

	private Vector2 m_gyroRotation;

	private float m_offset;

	private float MAX_GYRO_RANGE = 2.1f;

	private float ROTATION_SCALE = 0.085f;

	private Vector3 m_lookAtPoint;

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public Box GetParent()
	{
		return m_parent;
	}

	public BoxCameraStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxCameraStateInfo info)
	{
		m_info = info;
	}

	public BoxCameraEventTable GetEventTable()
	{
		return m_EventTable;
	}

	public Vector3 GetCameraPosition(State state)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Transform transform;
			Transform transform2;
			Transform transform3;
			switch (state)
			{
			case State.CLOSED:
				transform = m_info.m_ClosedMinAspectRatioBone.transform;
				transform2 = m_info.m_ClosedBone.transform;
				transform3 = m_info.m_ClosedExtraWideAspectRatioBone.transform;
				break;
			case State.CLOSED_WITH_DRAWER:
				transform = m_info.m_ClosedWithDrawerMinAspectRatioBone.transform;
				transform2 = m_info.m_ClosedWithDrawerBone.transform;
				transform3 = m_info.m_ClosedWithDrawerExtraWideAspectRatioBone.transform;
				break;
			default:
				transform = m_info.m_OpenedMinAspectRatioBone.transform;
				transform2 = m_info.m_OpenedBone.transform;
				transform3 = m_info.m_OpenedExtraWideAspectRatioBone.transform;
				break;
			}
			return TransformUtil.GetAspectRatioDependentPosition(transform.position, transform2.position, transform3.position);
		}
		return state switch
		{
			State.CLOSED => m_info.m_ClosedBone.transform.position, 
			State.CLOSED_WITH_DRAWER => m_info.m_ClosedWithDrawerBone.transform.position, 
			_ => m_info.m_OpenedBone.transform.position, 
		};
	}

	public State GetState()
	{
		return m_state;
	}

	public bool ChangeState(State state)
	{
		if (m_state == state)
		{
			return false;
		}
		Vector3 cameraPosition = GetCameraPosition(state);
		m_parent.OnAnimStarted();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_applyAccelerometer = false;
			m_basePosition = base.transform.parent.InverseTransformPoint(cameraPosition);
			m_lookAtPoint = base.transform.parent.InverseTransformPoint(new Vector3(cameraPosition.x, 1.5f, cameraPosition.z));
			if (cameraPosition == base.gameObject.transform.position)
			{
				OnAnimFinished(state);
				return true;
			}
		}
		Hashtable args = null;
		switch (state)
		{
		case State.CLOSED:
			args = iTween.Hash("position", cameraPosition, "delay", m_info.m_ClosedDelaySec, "time", m_info.m_ClosedMoveSec, "easeType", m_info.m_ClosedMoveEaseType, "oncomplete", "OnAnimFinished", "oncompleteparams", state, "oncompletetarget", base.gameObject);
			break;
		case State.CLOSED_WITH_DRAWER:
			args = iTween.Hash("position", cameraPosition, "delay", m_info.m_ClosedWithDrawerDelaySec, "time", m_info.m_ClosedWithDrawerMoveSec, "easeType", m_info.m_ClosedWithDrawerMoveEaseType, "oncomplete", "OnAnimFinished", "oncompleteparams", state, "oncompletetarget", base.gameObject);
			break;
		case State.OPENED:
			args = iTween.Hash("position", cameraPosition, "delay", m_info.m_OpenedDelaySec, "time", m_info.m_OpenedMoveSec, "easeType", m_info.m_OpenedMoveEaseType, "oncomplete", "OnAnimFinished", "oncompleteparams", state, "oncompletetarget", base.gameObject);
			break;
		case State.SET_ROTATION_OPENED:
			args = iTween.Hash("position", cameraPosition, "delay", m_info.m_OpenedDelaySec, "time", 1.5f, "easeType", m_info.m_OpenedMoveEaseType, "oncomplete", "OnAnimFinished", "oncompleteparams", state, "oncompletetarget", base.gameObject);
			break;
		}
		CameraShakeMgr.Stop(GetComponent<Camera>());
		iTween.MoveTo(base.gameObject, args);
		return true;
	}

	public void EnableAccelerometer()
	{
	}

	public void Update()
	{
		if (!m_disableAccelerometer && !(base.transform.parent.gameObject.GetComponent<LoadingScreen>() != null) && (bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_applyAccelerometer)
			{
				m_gyroRotation.x = Input.gyro.rotationRateUnbiased.x;
				m_gyroRotation.y = 0f - Input.gyro.rotationRateUnbiased.y;
				m_currentAngle.x += m_gyroRotation.y * ROTATION_SCALE;
				m_currentAngle.y += m_gyroRotation.x * ROTATION_SCALE;
				m_currentAngle.x = Mathf.Clamp(m_currentAngle.x, 0f - MAX_GYRO_RANGE, MAX_GYRO_RANGE);
				m_currentAngle.y = Mathf.Clamp(m_currentAngle.y, 0f - MAX_GYRO_RANGE, MAX_GYRO_RANGE);
				base.gameObject.transform.localPosition = new Vector3(m_basePosition.x, m_basePosition.y, m_basePosition.z + m_currentAngle.y);
			}
			Vector3 worldUp = new Vector3(0f, 0f, 1f);
			Vector3 worldPosition = base.gameObject.transform.parent.TransformPoint(m_lookAtPoint);
			base.gameObject.transform.LookAt(worldPosition, worldUp);
			if (m_applyAccelerometer)
			{
				m_IgnoreFullscreenEffectsCamera.transform.position = base.gameObject.transform.parent.TransformPoint(m_basePosition);
				m_IgnoreFullscreenEffectsCamera.transform.LookAt(worldPosition, worldUp);
				m_TooltipCamera.transform.position = base.gameObject.transform.parent.TransformPoint(m_basePosition);
				m_TooltipCamera.transform.LookAt(worldPosition, worldUp);
			}
			else
			{
				TransformUtil.Identity(m_TooltipCamera);
				TransformUtil.Identity(m_IgnoreFullscreenEffectsCamera);
			}
		}
	}

	public void OnAnimFinished(State state)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_applyAccelerometer = m_state != State.OPENED;
			m_currentAngle = new Vector2(0f, 0f);
		}
		m_state = state;
		m_parent.OnAnimFinished();
	}

	public void UpdateState(State state)
	{
		m_state = state;
		base.transform.position = GetCameraPosition(state);
	}
}
