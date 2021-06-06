using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class NetworkReachabilityManager : IService, IHasUpdate
{
	private const float INTERNET_REACHABILITY_POLL_RATE_SECONDS = 1f;

	private NetworkReachability m_cachedInternetReachability;

	private float m_internetReachabilityPollTimer;

	private bool m_internetReachabilityForceDisabled;

	public static bool OnCellular
	{
		get
		{
			if (Application.internetReachability != NetworkReachability.ReachableViaCarrierDataNetwork)
			{
				return Options.Get().GetBool(Option.SIMULATE_CELLULAR);
			}
			return true;
		}
	}

	public static bool InternetAvailable => Application.internetReachability != NetworkReachability.NotReachable;

	public bool InternetAvailable_Cached
	{
		get
		{
			NetworkReachability networkReachability = NetworkReachability.NotReachable;
			if (!m_internetReachabilityForceDisabled)
			{
				networkReachability = m_cachedInternetReachability;
			}
			return networkReachability != NetworkReachability.NotReachable;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_cachedInternetReachability = Application.internetReachability;
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[0];
	}

	public void Shutdown()
	{
	}

	public void SetForceUnreachable(bool value)
	{
		m_internetReachabilityForceDisabled = value;
	}

	public bool GetForceUnreachable()
	{
		return m_internetReachabilityForceDisabled;
	}

	void IHasUpdate.Update()
	{
		PollInternetReachability();
	}

	private void PollInternetReachability()
	{
		m_internetReachabilityPollTimer += Time.unscaledDeltaTime;
		if (m_internetReachabilityPollTimer >= 1f)
		{
			m_internetReachabilityPollTimer = 0f;
			m_cachedInternetReachability = Application.internetReachability;
		}
	}
}
