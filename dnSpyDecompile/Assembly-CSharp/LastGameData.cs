using System;
using PegasusShared;

// Token: 0x020008C4 RID: 2244
public class LastGameData
{
	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x06007B75 RID: 31605 RVA: 0x00280C32 File Offset: 0x0027EE32
	// (set) Token: 0x06007B76 RID: 31606 RVA: 0x00280C3A File Offset: 0x0027EE3A
	public TAG_PLAYSTATE GameResult { get; set; }

	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x06007B77 RID: 31607 RVA: 0x00280C43 File Offset: 0x0027EE43
	// (set) Token: 0x06007B78 RID: 31608 RVA: 0x00280C4B File Offset: 0x0027EE4B
	public int WhizbangDeckID { get; set; }

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x06007B79 RID: 31609 RVA: 0x00280C54 File Offset: 0x0027EE54
	// (set) Token: 0x06007B7A RID: 31610 RVA: 0x00280C5C File Offset: 0x0027EE5C
	public GameConnectionInfo GameConnectionInfo { get; set; }

	// Token: 0x17000708 RID: 1800
	// (get) Token: 0x06007B7B RID: 31611 RVA: 0x00280C65 File Offset: 0x0027EE65
	// (set) Token: 0x06007B7C RID: 31612 RVA: 0x00280C6D File Offset: 0x0027EE6D
	public int BattlegroundsLeaderboardPlace { get; set; }

	// Token: 0x06007B7D RID: 31613 RVA: 0x00280C76 File Offset: 0x0027EE76
	public LastGameData()
	{
		this.Clear();
	}

	// Token: 0x06007B7E RID: 31614 RVA: 0x00280C84 File Offset: 0x0027EE84
	public void Clear()
	{
		this.GameResult = TAG_PLAYSTATE.INVALID;
		this.WhizbangDeckID = 0;
		this.GameConnectionInfo = null;
	}

	// Token: 0x06007B7F RID: 31615 RVA: 0x00280C9B File Offset: 0x0027EE9B
	public bool HasWhizbangDeckID()
	{
		return this.WhizbangDeckID > 0;
	}
}
