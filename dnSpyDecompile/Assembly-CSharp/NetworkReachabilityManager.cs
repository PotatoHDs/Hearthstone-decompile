using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x020008ED RID: 2285
public class NetworkReachabilityManager : IService, IHasUpdate
{
	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x06007EA2 RID: 32418 RVA: 0x00290D04 File Offset: 0x0028EF04
	public static bool OnCellular
	{
		get
		{
			return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Options.Get().GetBool(Option.SIMULATE_CELLULAR);
		}
	}

	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x00290D1C File Offset: 0x0028EF1C
	public static bool InternetAvailable
	{
		get
		{
			return Application.internetReachability > NetworkReachability.NotReachable;
		}
	}

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x06007EA4 RID: 32420 RVA: 0x00290D28 File Offset: 0x0028EF28
	public bool InternetAvailable_Cached
	{
		get
		{
			NetworkReachability networkReachability = NetworkReachability.NotReachable;
			if (!this.m_internetReachabilityForceDisabled)
			{
				networkReachability = this.m_cachedInternetReachability;
			}
			return networkReachability > NetworkReachability.NotReachable;
		}
	}

	// Token: 0x06007EA5 RID: 32421 RVA: 0x00290D4A File Offset: 0x0028EF4A
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_cachedInternetReachability = Application.internetReachability;
		yield break;
	}

	// Token: 0x06007EA6 RID: 32422 RVA: 0x00290D59 File Offset: 0x0028EF59
	public Type[] GetDependencies()
	{
		return new Type[0];
	}

	// Token: 0x06007EA7 RID: 32423 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007EA8 RID: 32424 RVA: 0x00290D61 File Offset: 0x0028EF61
	public void SetForceUnreachable(bool value)
	{
		this.m_internetReachabilityForceDisabled = value;
	}

	// Token: 0x06007EA9 RID: 32425 RVA: 0x00290D6A File Offset: 0x0028EF6A
	public bool GetForceUnreachable()
	{
		return this.m_internetReachabilityForceDisabled;
	}

	// Token: 0x06007EAA RID: 32426 RVA: 0x00290D72 File Offset: 0x0028EF72
	void IHasUpdate.Update()
	{
		this.PollInternetReachability();
	}

	// Token: 0x06007EAB RID: 32427 RVA: 0x00290D7A File Offset: 0x0028EF7A
	private void PollInternetReachability()
	{
		this.m_internetReachabilityPollTimer += Time.unscaledDeltaTime;
		if (this.m_internetReachabilityPollTimer >= 1f)
		{
			this.m_internetReachabilityPollTimer = 0f;
			this.m_cachedInternetReachability = Application.internetReachability;
		}
	}

	// Token: 0x04006641 RID: 26177
	private const float INTERNET_REACHABILITY_POLL_RATE_SECONDS = 1f;

	// Token: 0x04006642 RID: 26178
	private NetworkReachability m_cachedInternetReachability;

	// Token: 0x04006643 RID: 26179
	private float m_internetReachabilityPollTimer;

	// Token: 0x04006644 RID: 26180
	private bool m_internetReachabilityForceDisabled;
}
