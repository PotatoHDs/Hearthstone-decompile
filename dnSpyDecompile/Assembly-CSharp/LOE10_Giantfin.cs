using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class LOE10_Giantfin : LOE_MissionEntity
{
	// Token: 0x060034CB RID: 13515 RVA: 0x0010D2E0 File Offset: 0x0010B4E0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing3);
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x0010D434 File Offset: 0x0010B634
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOEA10_1_MIDDLEFIN.prefab:db87360596b82634f9350b9fb516a52c");
		base.PreloadSound("VO_LOE10_NYAH_FINLEY.prefab:7d68f7ae697cde142ae6875d4758b0b0");
		base.PreloadSound("VO_LOE_10_NYAH.prefab:e9c4965c3cf4d274886180e4facf749f");
		base.PreloadSound("VO_LOE_10_RESPONSE.prefab:d59cf1de856198e4f9443ae4bdb2d04a");
		base.PreloadSound("VO_LOE_10_START_2.prefab:dec3b2452f06e4542b21afabb06cbdbf");
		base.PreloadSound("VO_LOE_10_TURN1.prefab:5f1e5b8506cd049419e78ee08f8143e4");
		base.PreloadSound("VO_LOE_10_WIN.prefab:31018ff004f803045bcb5e3af8447198");
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x0010D48E File Offset: 0x0010B68E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 2)
		{
			if (missionEvent == 3)
			{
				if (this.m_cardLinePlayed1)
				{
					yield break;
				}
				this.m_cardLinePlayed1 = true;
			}
		}
		else
		{
			if (this.m_cardLinePlayed2)
			{
				yield break;
			}
			this.m_cardLinePlayed2 = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA10_1_MIDDLEFIN.prefab:db87360596b82634f9350b9fb516a52c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034CE RID: 13518 RVA: 0x0010D4A4 File Offset: 0x0010B6A4
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "LOEA10_5")
		{
			if (this.m_nyahLinePlayed)
			{
				yield break;
			}
			this.m_nyahLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_10_NYAH.prefab:e9c4965c3cf4d274886180e4facf749f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(4f);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE10_NYAH_FINLEY.prefab:7d68f7ae697cde142ae6875d4758b0b0", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034CF RID: 13519 RVA: 0x0010D4BC File Offset: 0x0010B6BC
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_LOE_10_RESPONSE.prefab:d59cf1de856198e4f9443ae4bdb2d04a",
						m_stringTag = "VO_LOE_10_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x0010D51B File Offset: 0x0010B71B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_turnToPlayFoundLine == 5)
		{
			this.m_turnToPlayFoundLine = 7;
		}
		if (turn == this.m_turnToPlayFoundLine)
		{
			this.m_turnToPlayFoundLine = -1;
			yield break;
		}
		if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_10_TURN1.prefab:5f1e5b8506cd049419e78ee08f8143e4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_10_START_2.prefab:dec3b2452f06e4542b21afabb06cbdbf", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034D1 RID: 13521 RVA: 0x0010D531 File Offset: 0x0010B731
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOE_10_WIN.prefab:31018ff004f803045bcb5e3af8447198", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CC9 RID: 7369
	private bool m_cardLinePlayed1;

	// Token: 0x04001CCA RID: 7370
	private bool m_cardLinePlayed2;

	// Token: 0x04001CCB RID: 7371
	private bool m_nyahLinePlayed;

	// Token: 0x04001CCC RID: 7372
	private int m_turnToPlayFoundLine = -1;
}
