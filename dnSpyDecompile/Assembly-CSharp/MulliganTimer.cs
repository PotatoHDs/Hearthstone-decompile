using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class MulliganTimer : MonoBehaviour
{
	// Token: 0x06002E4D RID: 11853 RVA: 0x000EC1E7 File Offset: 0x000EA3E7
	private void Start()
	{
		if (MulliganManager.Get() == null)
		{
			return;
		}
		base.transform.position = this.GetNewPosition();
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x000EC208 File Offset: 0x000EA408
	private void Update()
	{
		if (!this.m_remainingTimeSet)
		{
			return;
		}
		Vector3 newPosition = this.GetNewPosition();
		if (newPosition != base.transform.position)
		{
			base.transform.position = newPosition;
		}
		float num = this.ComputeCountdownRemainingSec();
		int num2 = Mathf.RoundToInt(num);
		if (num2 < 0)
		{
			num2 = 0;
		}
		this.m_timeText.Text = string.Format(":{0:D2}", num2);
		if (num > 0f)
		{
			return;
		}
		if (MulliganManager.Get())
		{
			MulliganManager.Get().AutomaticContinueMulligan();
			return;
		}
		this.SelfDestruct();
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x000EC298 File Offset: 0x000EA498
	private Vector3 GetNewPosition()
	{
		if (MulliganManager.Get() == null)
		{
			return new Vector3(100f, 0f, 0f);
		}
		Vector3 mulliganTimerPosition = MulliganManager.Get().GetMulliganTimerPosition();
		if (UniversalInputManager.UsePhoneUI)
		{
			mulliganTimerPosition = new Vector3(mulliganTimerPosition.x + 1.8f, mulliganTimerPosition.y, mulliganTimerPosition.z);
		}
		else
		{
			mulliganTimerPosition = new Vector3(mulliganTimerPosition.x, mulliganTimerPosition.y, mulliganTimerPosition.z - 1f);
		}
		return mulliganTimerPosition;
	}

	// Token: 0x06002E50 RID: 11856 RVA: 0x000EC320 File Offset: 0x000EA520
	private float ComputeCountdownRemainingSec()
	{
		float num = this.m_endTimeStamp - Time.realtimeSinceStartup;
		if (num < 0f)
		{
			return 0f;
		}
		return num;
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x000EC349 File Offset: 0x000EA549
	public void SetEndTime(float endTimeStamp)
	{
		this.m_endTimeStamp = endTimeStamp;
		this.m_remainingTimeSet = true;
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	public void SelfDestruct()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040019BD RID: 6589
	public UberText m_timeText;

	// Token: 0x040019BE RID: 6590
	private bool m_remainingTimeSet;

	// Token: 0x040019BF RID: 6591
	private float m_endTimeStamp;
}
