using System;
using UnityEngine;

// Token: 0x02000A15 RID: 2581
public class CameraShakeMgr : MonoBehaviour
{
	// Token: 0x06008B53 RID: 35667 RVA: 0x002C8EF7 File Offset: 0x002C70F7
	private void Update()
	{
		if (this.m_progressSec >= this.m_durationSec && !this.IsHolding())
		{
			this.DestroyShake();
			return;
		}
		this.UpdateShake();
	}

	// Token: 0x06008B54 RID: 35668 RVA: 0x002C8F1C File Offset: 0x002C711C
	public static void Shake(Camera camera, Vector3 amount, AnimationCurve intensityCurve, float? holdAtTime = null)
	{
		if (!camera)
		{
			return;
		}
		if (!Options.Get().GetBool(Option.SCREEN_SHAKE_ENABLED))
		{
			return;
		}
		CameraShakeMgr cameraShakeMgr = camera.GetComponent<CameraShakeMgr>();
		if (cameraShakeMgr)
		{
			if (CameraShakeMgr.DoesCurveHaveZeroTime(intensityCurve))
			{
				cameraShakeMgr.DestroyShake();
				return;
			}
		}
		else
		{
			if (CameraShakeMgr.DoesCurveHaveZeroTime(intensityCurve))
			{
				return;
			}
			cameraShakeMgr = camera.gameObject.AddComponent<CameraShakeMgr>();
		}
		cameraShakeMgr.StartShake(amount, intensityCurve, holdAtTime);
	}

	// Token: 0x06008B55 RID: 35669 RVA: 0x002C8F80 File Offset: 0x002C7180
	public static void Shake(Camera camera, Vector3 amount, float time)
	{
		AnimationCurve intensityCurve = AnimationCurve.Linear(0f, 1f, time, 0f);
		CameraShakeMgr.Shake(camera, amount, intensityCurve, null);
	}

	// Token: 0x06008B56 RID: 35670 RVA: 0x002C8FB4 File Offset: 0x002C71B4
	public static void Stop(Camera camera, float time = 0f)
	{
		if (!camera)
		{
			return;
		}
		CameraShakeMgr component = camera.GetComponent<CameraShakeMgr>();
		if (!component)
		{
			return;
		}
		if (time <= 0f)
		{
			component.DestroyShake();
			return;
		}
		float valueStart = component.ComputeIntensity();
		AnimationCurve intensityCurve = AnimationCurve.Linear(0f, valueStart, time, 0f);
		component.StartShake(component.m_amount, intensityCurve, null);
	}

	// Token: 0x06008B57 RID: 35671 RVA: 0x002C9018 File Offset: 0x002C7218
	public static bool IsShaking(Camera camera)
	{
		return camera && camera.GetComponent<CameraShakeMgr>();
	}

	// Token: 0x06008B58 RID: 35672 RVA: 0x002C9034 File Offset: 0x002C7234
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
		return keyframe.time <= 0f;
	}

	// Token: 0x06008B59 RID: 35673 RVA: 0x002C9078 File Offset: 0x002C7278
	private void StartShake(Vector3 amount, AnimationCurve intensityCurve, float? holdAtSec = null)
	{
		this.m_amount = amount;
		this.m_intensityCurve = intensityCurve;
		this.m_holdAtSec = holdAtSec;
		if (!this.m_started)
		{
			this.m_started = true;
			this.m_initialPos = base.transform.position;
		}
		this.m_progressSec = 0f;
		Keyframe keyframe = intensityCurve.keys[intensityCurve.length - 1];
		this.m_durationSec = keyframe.time;
	}

	// Token: 0x06008B5A RID: 35674 RVA: 0x002C90E6 File Offset: 0x002C72E6
	private void DestroyShake()
	{
		base.transform.position = this.m_initialPos;
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x06008B5B RID: 35675 RVA: 0x002C9100 File Offset: 0x002C7300
	private void UpdateShake()
	{
		float num = this.ComputeIntensity();
		Vector3 b = default(Vector3);
		b.x = UnityEngine.Random.Range(-this.m_amount.x * num, this.m_amount.x * num);
		b.y = UnityEngine.Random.Range(-this.m_amount.y * num, this.m_amount.y * num);
		b.z = UnityEngine.Random.Range(-this.m_amount.z * num, this.m_amount.z * num);
		base.transform.position = this.m_initialPos + b;
		if (this.IsHolding())
		{
			return;
		}
		this.m_progressSec = Mathf.Min(this.m_progressSec + Time.deltaTime, this.m_durationSec);
	}

	// Token: 0x06008B5C RID: 35676 RVA: 0x002C91CE File Offset: 0x002C73CE
	private float ComputeIntensity()
	{
		return this.m_intensityCurve.Evaluate(this.m_progressSec);
	}

	// Token: 0x06008B5D RID: 35677 RVA: 0x002C91E4 File Offset: 0x002C73E4
	private bool IsHolding()
	{
		if (this.m_holdAtSec == null)
		{
			return false;
		}
		float progressSec = this.m_progressSec;
		float? holdAtSec = this.m_holdAtSec;
		return progressSec >= holdAtSec.GetValueOrDefault() & holdAtSec != null;
	}

	// Token: 0x040073D6 RID: 29654
	private Vector3 m_amount;

	// Token: 0x040073D7 RID: 29655
	private AnimationCurve m_intensityCurve;

	// Token: 0x040073D8 RID: 29656
	private float? m_holdAtSec;

	// Token: 0x040073D9 RID: 29657
	private bool m_started;

	// Token: 0x040073DA RID: 29658
	private Vector3 m_initialPos;

	// Token: 0x040073DB RID: 29659
	private float m_progressSec;

	// Token: 0x040073DC RID: 29660
	private float m_durationSec;
}
