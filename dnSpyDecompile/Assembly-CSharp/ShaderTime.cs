using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x02000A8F RID: 2703
public class ShaderTime : IService, IHasUpdate
{
	// Token: 0x06009097 RID: 37015 RVA: 0x002EE941 File Offset: 0x002ECB41
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06009098 RID: 37016 RVA: 0x001E71F7 File Offset: 0x001E53F7
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GraphicsManager)
		};
	}

	// Token: 0x06009099 RID: 37017 RVA: 0x002EE949 File Offset: 0x002ECB49
	public void Shutdown()
	{
		Shader.SetGlobalFloat("_ShaderTime", 0f);
	}

	// Token: 0x0600909A RID: 37018 RVA: 0x002EE95A File Offset: 0x002ECB5A
	public static ShaderTime Get()
	{
		return HearthstoneServices.Get<ShaderTime>();
	}

	// Token: 0x0600909B RID: 37019 RVA: 0x002EE961 File Offset: 0x002ECB61
	public void Update()
	{
		this.UpdateShaderAnimationTime();
		this.UpdateGyro();
	}

	// Token: 0x0600909C RID: 37020 RVA: 0x002EE96F File Offset: 0x002ECB6F
	public void DisableShaderAnimation()
	{
		this.m_enabled = false;
	}

	// Token: 0x0600909D RID: 37021 RVA: 0x002EE978 File Offset: 0x002ECB78
	public void EnableShaderAnimation()
	{
		this.m_enabled = true;
	}

	// Token: 0x0600909E RID: 37022 RVA: 0x002EE984 File Offset: 0x002ECB84
	private void UpdateShaderAnimationTime()
	{
		if (!this.m_enabled)
		{
			this.m_time = 1f;
		}
		else
		{
			this.m_time += Time.deltaTime / 20f;
			if (this.m_time > this.m_maxTime)
			{
				this.m_time -= this.m_maxTime;
				if (this.m_time <= 0f)
				{
					this.m_time = 0.0001f;
				}
			}
		}
		Shader.SetGlobalFloat("_ShaderTime", this.m_time);
	}

	// Token: 0x0600909F RID: 37023 RVA: 0x002EEA08 File Offset: 0x002ECC08
	private void UpdateGyro()
	{
		Vector4 value = Input.gyro.gravity;
		Shader.SetGlobalVector("_Gyroscope", value);
	}

	// Token: 0x04007964 RID: 31076
	private const float LOW_PRECISION = 15f;

	// Token: 0x04007965 RID: 31077
	private const float MEDIUM_PRECISION = 255f;

	// Token: 0x04007966 RID: 31078
	private const float HIGH_PRECISION = 65535f;

	// Token: 0x04007967 RID: 31079
	private float m_maxTime = 65535f;

	// Token: 0x04007968 RID: 31080
	private float m_time;

	// Token: 0x04007969 RID: 31081
	private bool m_enabled = true;
}
