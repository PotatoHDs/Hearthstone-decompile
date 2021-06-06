using System;
using UnityEngine;

// Token: 0x02000813 RID: 2067
public class ParticleSystemScaler : MonoBehaviour
{
	// Token: 0x06006F9A RID: 28570 RVA: 0x0023FCE4 File Offset: 0x0023DEE4
	private void Awake()
	{
		this.m_unitMagnitude = Vector3.one.magnitude;
	}

	// Token: 0x06006F9B RID: 28571 RVA: 0x0023FD04 File Offset: 0x0023DF04
	private void Update()
	{
		if (this.ObjectToInherit != null)
		{
			this.ParticleSystemScale = this.ObjectToInherit.transform.lossyScale.magnitude / this.m_unitMagnitude;
		}
		foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
		{
			ParticleSystem.MainModule main = particleSystem.main;
			if (!this.m_initialValues.ContainsKey(particleSystem))
			{
				this.m_initialValues.Add(particleSystem, new ParticleSystemSizes());
				this.m_initialValues[particleSystem].startSpeed = main.startSpeed.constant;
				this.m_initialValues[particleSystem].startSize = main.startSize.constant;
				this.m_initialValues[particleSystem].gravityModifier = main.gravityModifier.constant;
			}
			main.startSize = this.m_initialValues[particleSystem].startSize * this.ParticleSystemScale;
			main.startSpeed = this.m_initialValues[particleSystem].startSpeed * this.ParticleSystemScale;
			main.gravityModifier = this.m_initialValues[particleSystem].gravityModifier * this.ParticleSystemScale;
		}
	}

	// Token: 0x06006F9C RID: 28572 RVA: 0x0023FE5C File Offset: 0x0023E05C
	private void ScaleParticleSystems(float scaleFactor)
	{
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystem.MainModule main = componentsInChildren[i].main;
			main.startSpeed = main.startSpeed.constant * scaleFactor;
			main.startSize = main.startSize.constant * scaleFactor;
			main.gravityModifier = main.gravityModifier.constant * scaleFactor;
		}
	}

	// Token: 0x0400597F RID: 22911
	public float ParticleSystemScale = 1f;

	// Token: 0x04005980 RID: 22912
	public GameObject ObjectToInherit;

	// Token: 0x04005981 RID: 22913
	private float m_unitMagnitude;

	// Token: 0x04005982 RID: 22914
	private Map<ParticleSystem, ParticleSystemSizes> m_initialValues = new Map<ParticleSystem, ParticleSystemSizes>();
}
