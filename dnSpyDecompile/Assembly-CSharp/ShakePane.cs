using System;
using System.Collections;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000A92 RID: 2706
public class ShakePane : MonoBehaviour
{
	// Token: 0x060090A6 RID: 37030 RVA: 0x002EEAE8 File Offset: 0x002ECCE8
	protected void Awake()
	{
		if (this.m_shakeyObject != null)
		{
			this.m_shakeyObjectOriginalLocalRotation = this.m_shakeyObject.transform.localEulerAngles;
			this.m_shakeyObjectOriginalLocalPosition = this.m_shakeyObject.transform.localPosition;
		}
	}

	// Token: 0x060090A7 RID: 37031 RVA: 0x002EEB24 File Offset: 0x002ECD24
	public void Shake(float xRotationAmount, float shakeTime, float delay = 0f, float translateAmount = 0f)
	{
		if (this.m_shakeyObject == null)
		{
			return;
		}
		if (base.gameObject.activeInHierarchy)
		{
			this.m_shakeyStoreAnimCoroutine = base.StartCoroutine(this.AnimateShakeyObjectCoroutine(xRotationAmount, translateAmount, shakeTime, delay));
		}
	}

	// Token: 0x060090A8 RID: 37032 RVA: 0x002EEB59 File Offset: 0x002ECD59
	public void Reset()
	{
		if (this.m_shakeyStoreAnimCoroutine != null)
		{
			base.StopCoroutine(this.m_shakeyStoreAnimCoroutine);
		}
	}

	// Token: 0x060090A9 RID: 37033 RVA: 0x002EEB6F File Offset: 0x002ECD6F
	private void OnStopShaking(object obj)
	{
		this.m_stillShaking = false;
	}

	// Token: 0x060090AA RID: 37034 RVA: 0x002EEB78 File Offset: 0x002ECD78
	private IEnumerator AnimateShakeyObjectCoroutine(float xRotationAmount, float translationAmount, float shakeTime, float delay)
	{
		xRotationAmount = Mathf.Clamp(xRotationAmount, -this.m_maxRotation, this.m_maxRotation);
		float absRotation = Mathf.Abs(xRotationAmount);
		if (absRotation - this.m_lastShakeAmount < this.m_multipleShakeTolerance && this.m_stillShaking)
		{
			yield break;
		}
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		this.m_lastShakeAmount = absRotation;
		this.m_stillShaking = true;
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.OnStopShaking), null);
		Processor.ScheduleCallback(shakeTime * 0.25f, false, new Processor.ScheduledCallback(this.OnStopShaking), null);
		iTween.Stop(this.m_shakeyObject);
		this.m_shakeyObject.transform.localEulerAngles = this.m_shakeyObjectOriginalLocalRotation;
		iTween.PunchRotation(this.m_shakeyObject, iTween.Hash(new object[]
		{
			"x",
			xRotationAmount,
			"time",
			shakeTime,
			"delay",
			0.001f,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_shakeyObject.transform.localEulerAngles = this.m_shakeyObjectOriginalLocalRotation;
				this.m_lastShakeAmount = 0f;
			})
		}));
		if (translationAmount != 0f)
		{
			this.m_shakeyObject.transform.localPosition = this.m_shakeyObjectOriginalLocalPosition;
			iTween.PunchPosition(this.m_shakeyObject, iTween.Hash(new object[]
			{
				"y",
				translationAmount,
				"time",
				shakeTime,
				"delay",
				0.001f,
				"oncomplete",
				new Action<object>(delegate(object o)
				{
					this.m_shakeyObject.transform.localPosition = this.m_shakeyObjectOriginalLocalPosition;
				})
			}));
		}
		yield break;
	}

	// Token: 0x0400796C RID: 31084
	[SerializeField]
	private GameObject m_shakeyObject;

	// Token: 0x0400796D RID: 31085
	[SerializeField]
	private float m_multipleShakeTolerance = 1.2f;

	// Token: 0x0400796E RID: 31086
	[SerializeField]
	private float m_maxRotation = 30f;

	// Token: 0x0400796F RID: 31087
	private Coroutine m_shakeyStoreAnimCoroutine;

	// Token: 0x04007970 RID: 31088
	private Vector3 m_shakeyObjectOriginalLocalRotation = Vector3.zero;

	// Token: 0x04007971 RID: 31089
	private Vector3 m_shakeyObjectOriginalLocalPosition = Vector3.zero;

	// Token: 0x04007972 RID: 31090
	private float m_lastShakeAmount;

	// Token: 0x04007973 RID: 31091
	private bool m_stillShaking;
}
