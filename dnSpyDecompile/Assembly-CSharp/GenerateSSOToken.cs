using System;
using bgs;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
public class GenerateSSOToken : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
{
	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x06007D70 RID: 32112 RVA: 0x0028BAE0 File Offset: 0x00289CE0
	// (set) Token: 0x06007D71 RID: 32113 RVA: 0x0028BAE8 File Offset: 0x00289CE8
	public bool HasToken { get; private set; }

	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x06007D72 RID: 32114 RVA: 0x0028BAF1 File Offset: 0x00289CF1
	// (set) Token: 0x06007D73 RID: 32115 RVA: 0x0028BAF9 File Offset: 0x00289CF9
	public string Token { get; private set; }

	// Token: 0x06007D74 RID: 32116 RVA: 0x0028BB02 File Offset: 0x00289D02
	public GenerateSSOToken()
	{
		BattleNet.GenerateAppWebCredentials(new Action<bool, string>(this.OnTokenReceieved));
		this.m_startTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06007D75 RID: 32117 RVA: 0x0028BB26 File Offset: 0x00289D26
	public bool IsReady()
	{
		return this.m_hasResponse;
	}

	// Token: 0x06007D76 RID: 32118 RVA: 0x0028BB30 File Offset: 0x00289D30
	public bool HasFailed()
	{
		float num = Time.realtimeSinceStartup - this.m_startTime;
		return !this.m_hasResponse && num > 12f;
	}

	// Token: 0x06007D77 RID: 32119 RVA: 0x0028BB5C File Offset: 0x00289D5C
	private void OnTokenReceieved(bool hasToken, string token)
	{
		this.m_hasResponse = true;
		this.HasToken = hasToken;
		this.Token = token;
	}

	// Token: 0x040065B9 RID: 26041
	private bool m_hasResponse;

	// Token: 0x040065BA RID: 26042
	private float m_startTime;

	// Token: 0x040065BB RID: 26043
	private const float TIMEOUT_THRESHOLD_SECONDS = 12f;
}
