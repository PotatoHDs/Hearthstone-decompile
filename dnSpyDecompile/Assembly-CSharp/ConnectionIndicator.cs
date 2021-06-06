using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class ConnectionIndicator : MonoBehaviour
{
	// Token: 0x0600077F RID: 1919 RVA: 0x0002AAE8 File Offset: 0x00028CE8
	private void Awake()
	{
		ConnectionIndicator.s_instance = this;
		this.m_active = false;
		this.m_indicator.SetActive(false);
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0002AB03 File Offset: 0x00028D03
	private void OnDestroy()
	{
		ConnectionIndicator.s_instance = null;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0002AB0B File Offset: 0x00028D0B
	public static ConnectionIndicator Get()
	{
		return ConnectionIndicator.s_instance;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0002AB12 File Offset: 0x00028D12
	private void SetIndicator(bool val)
	{
		if (val == this.m_active)
		{
			return;
		}
		this.m_active = val;
		this.m_indicator.SetActive(val);
		BnetBar.Get().UpdateLayout();
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0002AB3B File Offset: 0x00028D3B
	public bool IsVisible()
	{
		return this.m_active;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0002AB43 File Offset: 0x00028D43
	private void Update()
	{
		this.SetIndicator(Network.Get().TimeSinceLastPong() > 3.0);
	}

	// Token: 0x04000519 RID: 1305
	public GameObject m_indicator;

	// Token: 0x0400051A RID: 1306
	private static ConnectionIndicator s_instance;

	// Token: 0x0400051B RID: 1307
	private bool m_active;

	// Token: 0x0400051C RID: 1308
	private const float LATENCY_TOLERANCE = 3f;
}
