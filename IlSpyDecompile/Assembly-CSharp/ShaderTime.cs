using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class ShaderTime : IService, IHasUpdate
{
	private const float LOW_PRECISION = 15f;

	private const float MEDIUM_PRECISION = 255f;

	private const float HIGH_PRECISION = 65535f;

	private float m_maxTime = 65535f;

	private float m_time;

	private bool m_enabled = true;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(GraphicsManager) };
	}

	public void Shutdown()
	{
		Shader.SetGlobalFloat("_ShaderTime", 0f);
	}

	public static ShaderTime Get()
	{
		return HearthstoneServices.Get<ShaderTime>();
	}

	public void Update()
	{
		UpdateShaderAnimationTime();
		UpdateGyro();
	}

	public void DisableShaderAnimation()
	{
		m_enabled = false;
	}

	public void EnableShaderAnimation()
	{
		m_enabled = true;
	}

	private void UpdateShaderAnimationTime()
	{
		if (!m_enabled)
		{
			m_time = 1f;
		}
		else
		{
			m_time += Time.deltaTime / 20f;
			if (m_time > m_maxTime)
			{
				m_time -= m_maxTime;
				if (m_time <= 0f)
				{
					m_time = 0.0001f;
				}
			}
		}
		Shader.SetGlobalFloat("_ShaderTime", m_time);
	}

	private void UpdateGyro()
	{
		Vector4 value = Input.gyro.gravity;
		Shader.SetGlobalVector("_Gyroscope", value);
	}
}
