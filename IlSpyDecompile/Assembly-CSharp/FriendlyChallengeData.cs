using System.Collections.Generic;
using bgs;
using PegasusShared;

public class FriendlyChallengeData
{
	public BnetEntityId m_partyId;

	public BnetGameAccountId m_challengerId;

	public bool m_challengerPending;

	public int m_scenarioId = 2;

	public int m_seasonId;

	public int m_brawlLibraryItemId;

	public BrawlType m_challengeBrawlType;

	public FormatType m_challengeFormatType;

	public BnetPlayer m_challenger;

	public long m_challengerDeckId;

	public long m_challengerHeroId;

	public bool m_challengerDeckOrHeroSelected;

	public byte[] m_challengerFsgSharedSecret;

	public BnetPlayer m_challengee;

	public long m_challengeeDeckId;

	public long m_challengeeHeroId;

	public byte[] m_challengeeFsgSharedSecret;

	public bool m_challengeeAccepted;

	public bool m_challengeeDeckOrHeroSelected;

	public bool m_challengerInGameState;

	public bool m_challengeeInGameState;

	public string m_challengerDeckShareState;

	public string m_challengeeDeckShareState;

	public List<CollectionDeck> m_sharedDecks;

	public bool m_updatePartyQuestInfoOnGameplaySceneUnload;

	public bool m_findGameErrorOccurred;

	public bool DidReceiveChallenge
	{
		get
		{
			if (m_challengerPending)
			{
				return true;
			}
			if (m_challenger != null)
			{
				return m_challengee == BnetPresenceMgr.Get().GetMyPlayer();
			}
			return false;
		}
	}

	public bool DidSendChallenge
	{
		get
		{
			if (m_challengee != null)
			{
				return m_challenger == BnetPresenceMgr.Get().GetMyPlayer();
			}
			return false;
		}
	}

	public bool IsPendingGotoGame
	{
		get
		{
			if (m_challengerInGameState && m_challengeeInGameState)
			{
				return false;
			}
			return true;
		}
	}
}
