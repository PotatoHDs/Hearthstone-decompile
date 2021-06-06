using bgs;
using Blizzard.T5.Jobs;
using UnityEngine;

public class GenerateSSOToken : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
{
	private bool m_hasResponse;

	private float m_startTime;

	private const float TIMEOUT_THRESHOLD_SECONDS = 12f;

	public bool HasToken { get; private set; }

	public string Token { get; private set; }

	public GenerateSSOToken()
	{
		BattleNet.GenerateAppWebCredentials(OnTokenReceieved);
		m_startTime = Time.realtimeSinceStartup;
	}

	public bool IsReady()
	{
		return m_hasResponse;
	}

	public bool HasFailed()
	{
		float num = Time.realtimeSinceStartup - m_startTime;
		if (!m_hasResponse)
		{
			return num > 12f;
		}
		return false;
	}

	private void OnTokenReceieved(bool hasToken, string token)
	{
		m_hasResponse = true;
		HasToken = hasToken;
		Token = token;
	}
}
