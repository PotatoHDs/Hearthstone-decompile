using UnityEngine;

public class CameraShakeMgr : MonoBehaviour
{
	private Vector3 m_amount;

	private AnimationCurve m_intensityCurve;

	private float? m_holdAtSec;

	private bool m_started;

	private Vector3 m_initialPos;

	private float m_progressSec;

	private float m_durationSec;

	private void Update()
	{
		if (m_progressSec >= m_durationSec && !IsHolding())
		{
			DestroyShake();
		}
		else
		{
			UpdateShake();
		}
	}

	public static void Shake(Camera camera, Vector3 amount, AnimationCurve intensityCurve, float? holdAtTime = null)
	{
		if (!camera || !Options.Get().GetBool(Option.SCREEN_SHAKE_ENABLED))
		{
			return;
		}
		CameraShakeMgr cameraShakeMgr = camera.GetComponent<CameraShakeMgr>();
		if ((bool)cameraShakeMgr)
		{
			if (DoesCurveHaveZeroTime(intensityCurve))
			{
				cameraShakeMgr.DestroyShake();
				return;
			}
		}
		else
		{
			if (DoesCurveHaveZeroTime(intensityCurve))
			{
				return;
			}
			cameraShakeMgr = camera.gameObject.AddComponent<CameraShakeMgr>();
		}
		cameraShakeMgr.StartShake(amount, intensityCurve, holdAtTime);
	}

	public static void Shake(Camera camera, Vector3 amount, float time)
	{
		AnimationCurve intensityCurve = AnimationCurve.Linear(0f, 1f, time, 0f);
		Shake(camera, amount, intensityCurve);
	}

	public static void Stop(Camera camera, float time = 0f)
	{
		if (!camera)
		{
			return;
		}
		CameraShakeMgr component = camera.GetComponent<CameraShakeMgr>();
		if ((bool)component)
		{
			if (time <= 0f)
			{
				component.DestroyShake();
				return;
			}
			float valueStart = component.ComputeIntensity();
			AnimationCurve intensityCurve = AnimationCurve.Linear(0f, valueStart, time, 0f);
			component.StartShake(component.m_amount, intensityCurve);
		}
	}

	public static bool IsShaking(Camera camera)
	{
		if (!camera)
		{
			return false;
		}
		if (!camera.GetComponent<CameraShakeMgr>())
		{
			return false;
		}
		return true;
	}

	private static bool DoesCurveHaveZeroTime(AnimationCurve intensityCurve)
	{
		if (intensityCurve == null)
		{
			return true;
		}
		if (intensityCurve.length == 0)
		{
			return true;
		}
		Keyframe keyframe = intensityCurve.keys[intensityCurve.length - 1];
		if (keyframe.time <= 0f)
		{
			return true;
		}
		return false;
	}

	private void StartShake(Vector3 amount, AnimationCurve intensityCurve, float? holdAtSec = null)
	{
		m_amount = amount;
		m_intensityCurve = intensityCurve;
		m_holdAtSec = holdAtSec;
		if (!m_started)
		{
			m_started = true;
			m_initialPos = base.transform.position;
		}
		m_progressSec = 0f;
		Keyframe keyframe = intensityCurve.keys[intensityCurve.length - 1];
		m_durationSec = keyframe.time;
	}

	private void DestroyShake()
	{
		base.transform.position = m_initialPos;
		Object.Destroy(this);
	}

	private void UpdateShake()
	{
		float num = ComputeIntensity();
		Vector3 vector = default(Vector3);
		vector.x = Random.Range((0f - m_amount.x) * num, m_amount.x * num);
		vector.y = Random.Range((0f - m_amount.y) * num, m_amount.y * num);
		vector.z = Random.Range((0f - m_amount.z) * num, m_amount.z * num);
		base.transform.position = m_initialPos + vector;
		if (!IsHolding())
		{
			m_progressSec = Mathf.Min(m_progressSec + Time.deltaTime, m_durationSec);
		}
	}

	private float ComputeIntensity()
	{
		return m_intensityCurve.Evaluate(m_progressSec);
	}

	private bool IsHolding()
	{
		if (!m_holdAtSec.HasValue)
		{
			return false;
		}
		return m_progressSec >= m_holdAtSec;
	}
}
