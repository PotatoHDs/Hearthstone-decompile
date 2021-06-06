using System;
using bgs;

// Token: 0x020000A0 RID: 160
public class PlayerChatInfo
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x00039E2D File Offset: 0x0003802D
	public BnetPlayer GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00039E35 File Offset: 0x00038035
	public void SetPlayer(BnetPlayer player)
	{
		this.m_player = player;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00039E3E File Offset: 0x0003803E
	public float GetLastFocusTime()
	{
		return this.m_lastFocusTime;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00039E46 File Offset: 0x00038046
	public void SetLastFocusTime(float time)
	{
		this.m_lastFocusTime = time;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00039E4F File Offset: 0x0003804F
	public BnetWhisper GetLastSeenWhisper()
	{
		return this.m_lastSeenWhisper;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00039E57 File Offset: 0x00038057
	public void SetLastSeenWhisper(BnetWhisper whisper)
	{
		this.m_lastSeenWhisper = whisper;
	}

	// Token: 0x0400068B RID: 1675
	private BnetPlayer m_player;

	// Token: 0x0400068C RID: 1676
	private float m_lastFocusTime;

	// Token: 0x0400068D RID: 1677
	private BnetWhisper m_lastSeenWhisper;
}
