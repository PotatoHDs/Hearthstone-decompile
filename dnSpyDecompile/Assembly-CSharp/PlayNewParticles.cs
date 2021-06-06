using System;
using UnityEngine;

// Token: 0x02000A60 RID: 2656
public class PlayNewParticles : MonoBehaviour
{
	// Token: 0x06008EC1 RID: 36545 RVA: 0x002E16E6 File Offset: 0x002DF8E6
	public void PlayNewParticles3()
	{
		if (this.m_Target == null)
		{
			return;
		}
		this.m_Target.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x06008EC2 RID: 36546 RVA: 0x002E1707 File Offset: 0x002DF907
	public void StopNewParticles3()
	{
		if (this.m_Target == null)
		{
			return;
		}
		this.m_Target.GetComponent<ParticleSystem>().Stop();
	}

	// Token: 0x06008EC3 RID: 36547 RVA: 0x002E1728 File Offset: 0x002DF928
	public void PlayNewParticles3andChilds()
	{
		if (this.m_Target2 == null)
		{
			return;
		}
		this.m_Target2.GetComponent<ParticleSystem>().Play(true);
	}

	// Token: 0x06008EC4 RID: 36548 RVA: 0x002E174A File Offset: 0x002DF94A
	public void StopNewParticles3andChilds()
	{
		if (this.m_Target2 == null)
		{
			return;
		}
		this.m_Target2.GetComponent<ParticleSystem>().Stop(true);
	}

	// Token: 0x06008EC5 RID: 36549 RVA: 0x002E176C File Offset: 0x002DF96C
	public void PlayNewParticles3andChilds2()
	{
		if (this.m_Target3 == null)
		{
			return;
		}
		this.m_Target3.GetComponent<ParticleSystem>().Play(true);
	}

	// Token: 0x06008EC6 RID: 36550 RVA: 0x002E178E File Offset: 0x002DF98E
	public void StopNewParticles3andChilds2()
	{
		if (this.m_Target3 == null)
		{
			return;
		}
		this.m_Target3.GetComponent<ParticleSystem>().Stop(true);
	}

	// Token: 0x06008EC7 RID: 36551 RVA: 0x002E17B0 File Offset: 0x002DF9B0
	public void PlayNewParticles3andChilds3()
	{
		if (this.m_Target4 == null)
		{
			return;
		}
		this.m_Target4.GetComponent<ParticleSystem>().Play(true);
	}

	// Token: 0x06008EC8 RID: 36552 RVA: 0x002E17D2 File Offset: 0x002DF9D2
	public void StopNewParticles3andChilds3()
	{
		if (this.m_Target4 == null)
		{
			return;
		}
		this.m_Target4.GetComponent<ParticleSystem>().Stop(true);
	}

	// Token: 0x0400771B RID: 30491
	public GameObject m_Target;

	// Token: 0x0400771C RID: 30492
	public GameObject m_Target2;

	// Token: 0x0400771D RID: 30493
	public GameObject m_Target3;

	// Token: 0x0400771E RID: 30494
	public GameObject m_Target4;
}
