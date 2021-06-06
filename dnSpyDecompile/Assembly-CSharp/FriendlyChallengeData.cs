using System;
using System.Collections.Generic;
using bgs;
using PegasusShared;

// Token: 0x02000088 RID: 136
public class FriendlyChallengeData
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060007AD RID: 1965 RVA: 0x0002B7BB File Offset: 0x000299BB
	public bool DidReceiveChallenge
	{
		get
		{
			return this.m_challengerPending || (this.m_challenger != null && this.m_challengee == BnetPresenceMgr.Get().GetMyPlayer());
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060007AE RID: 1966 RVA: 0x0002B7E3 File Offset: 0x000299E3
	public bool DidSendChallenge
	{
		get
		{
			return this.m_challengee != null && this.m_challenger == BnetPresenceMgr.Get().GetMyPlayer();
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002B801 File Offset: 0x00029A01
	public bool IsPendingGotoGame
	{
		get
		{
			return !this.m_challengerInGameState || !this.m_challengeeInGameState;
		}
	}

	// Token: 0x04000549 RID: 1353
	public BnetEntityId m_partyId;

	// Token: 0x0400054A RID: 1354
	public BnetGameAccountId m_challengerId;

	// Token: 0x0400054B RID: 1355
	public bool m_challengerPending;

	// Token: 0x0400054C RID: 1356
	public int m_scenarioId = 2;

	// Token: 0x0400054D RID: 1357
	public int m_seasonId;

	// Token: 0x0400054E RID: 1358
	public int m_brawlLibraryItemId;

	// Token: 0x0400054F RID: 1359
	public BrawlType m_challengeBrawlType;

	// Token: 0x04000550 RID: 1360
	public FormatType m_challengeFormatType;

	// Token: 0x04000551 RID: 1361
	public BnetPlayer m_challenger;

	// Token: 0x04000552 RID: 1362
	public long m_challengerDeckId;

	// Token: 0x04000553 RID: 1363
	public long m_challengerHeroId;

	// Token: 0x04000554 RID: 1364
	public bool m_challengerDeckOrHeroSelected;

	// Token: 0x04000555 RID: 1365
	public byte[] m_challengerFsgSharedSecret;

	// Token: 0x04000556 RID: 1366
	public BnetPlayer m_challengee;

	// Token: 0x04000557 RID: 1367
	public long m_challengeeDeckId;

	// Token: 0x04000558 RID: 1368
	public long m_challengeeHeroId;

	// Token: 0x04000559 RID: 1369
	public byte[] m_challengeeFsgSharedSecret;

	// Token: 0x0400055A RID: 1370
	public bool m_challengeeAccepted;

	// Token: 0x0400055B RID: 1371
	public bool m_challengeeDeckOrHeroSelected;

	// Token: 0x0400055C RID: 1372
	public bool m_challengerInGameState;

	// Token: 0x0400055D RID: 1373
	public bool m_challengeeInGameState;

	// Token: 0x0400055E RID: 1374
	public string m_challengerDeckShareState;

	// Token: 0x0400055F RID: 1375
	public string m_challengeeDeckShareState;

	// Token: 0x04000560 RID: 1376
	public List<CollectionDeck> m_sharedDecks;

	// Token: 0x04000561 RID: 1377
	public bool m_updatePartyQuestInfoOnGameplaySceneUnload;

	// Token: 0x04000562 RID: 1378
	public bool m_findGameErrorOccurred;
}
