using System;
using UnityEngine;

// Token: 0x02000A5E RID: 2654
public class PlayAnimator : MonoBehaviour
{
	// Token: 0x06008EBA RID: 36538 RVA: 0x002E15DA File Offset: 0x002DF7DA
	public void PlayAnimator1()
	{
		if (this.m_Target1 == null)
		{
			return;
		}
		this.m_Target1.GetComponent<Animator>().enabled = true;
		this.m_Target1.GetComponent<Animator>().Play(this.m_Target1State, -1, 0f);
	}

	// Token: 0x06008EBB RID: 36539 RVA: 0x002E1618 File Offset: 0x002DF818
	public void PlayAnimator2()
	{
		if (this.m_Target1 == null)
		{
			return;
		}
		this.m_Target2.GetComponent<Animator>().enabled = true;
		this.m_Target2.GetComponent<Animator>().Play(this.m_Target2State, -1, 0f);
	}

	// Token: 0x06008EBC RID: 36540 RVA: 0x002E1656 File Offset: 0x002DF856
	public void PlayAnimator3()
	{
		if (this.m_Target1 == null)
		{
			return;
		}
		this.m_Target3.GetComponent<Animator>().enabled = true;
		this.m_Target3.GetComponent<Animator>().Play(this.m_Target3State, -1, 0f);
	}

	// Token: 0x0400770F RID: 30479
	public GameObject m_Target1;

	// Token: 0x04007710 RID: 30480
	public string m_Target1State;

	// Token: 0x04007711 RID: 30481
	public GameObject m_Target2;

	// Token: 0x04007712 RID: 30482
	public string m_Target2State;

	// Token: 0x04007713 RID: 30483
	public GameObject m_Target3;

	// Token: 0x04007714 RID: 30484
	public string m_Target3State;
}
