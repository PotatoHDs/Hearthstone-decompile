using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class LOE09_LordSlitherspear : LOE_MissionEntity
{
	// Token: 0x060034C4 RID: 13508 RVA: 0x0010D2E0 File Offset: 0x0010B4E0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing3);
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x0010D2F4 File Offset: 0x0010B4F4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOEA09_1_RESPONSE.prefab:69af0af0192a1024eb47158a3af3f40a");
		base.PreloadSound("VO_LOEA09_UNSTABLE.prefab:2ba176281f83bb7418d2c01df2e900bc");
		base.PreloadSound("VO_LOEA09_HERO_POWER.prefab:e752937402315d043b487ed44f4a461a");
		base.PreloadSound("FX_MinionSummon_Cast.prefab:d0a0997a72042914f8779e138bb2755e");
		base.PreloadSound("VO_LOEA09_QUOTE1.prefab:ade9f08ae927cc94082169c6b71c13b9");
		base.PreloadSound("VO_LOEA09_FINLEY_DEATH.prefab:47571c42e9fc0014a94352f1eb0a3a1f");
		base.PreloadSound("VO_LOEA09_HERO_POWER1.prefab:4f4322070683ced41a83c03c0527ee0e");
		base.PreloadSound("VO_LOEA09_HERO_POWER2.prefab:bab01103d3d6d894b81e103efe850c11");
		base.PreloadSound("VO_LOEA09_HERO_POWER3.prefab:7ae0457d447b24e4c8c17ab4120d40cc");
		base.PreloadSound("VO_LOEA09_HERO_POWER4.prefab:1d497fab014f3c64c81ef834d8ee009b");
		base.PreloadSound("VO_LOEA09_HERO_POWER5.prefab:ef08dadc1d255714ab3105ab1b5cd2a8");
		base.PreloadSound("VO_LOEA09_HERO_POWER6.prefab:6ca24415fc389714885735792c3ef46c");
		base.PreloadSound("VO_LOEA09_WIN.prefab:add3634f635104d49987f553e3f83a8c");
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x0010D390 File Offset: 0x0010B590
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
						m_soundName = "VO_LOEA09_1_RESPONSE.prefab:69af0af0192a1024eb47158a3af3f40a",
						m_stringTag = "VO_LOEA09_1_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x0010D3EF File Offset: 0x0010B5EF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent != 1)
		{
			if (missionEvent == 3)
			{
				if (this.m_finley_death_line)
				{
					yield break;
				}
				this.m_finley_death_line = true;
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_FINLEY_DEATH.prefab:47571c42e9fc0014a94352f1eb0a3a1f", 3f, 1f, false, false));
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			this.m_finley_saved = true;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_QUOTE1.prefab:ade9f08ae927cc94082169c6b71c13b9", 3f, 1f, false, false));
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_UNSTABLE.prefab:2ba176281f83bb7418d2c01df2e900bc", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x0010D405 File Offset: 0x0010B605
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (this.m_finley_saved)
		{
			yield break;
		}
		if (this.m_cauldronCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				this.m_cauldronCard = entity.GetCard();
			}
		}
		if (this.m_cauldronCard == null && turn > 1)
		{
			yield break;
		}
		if (this.m_cauldronCard != null && this.m_cauldronCard.GetEntity().GetZone() != TAG_ZONE.PLAY)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			switch (turn)
			{
			case 4:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER2.prefab:bab01103d3d6d894b81e103efe850c11", Notification.SpeechBubbleDirection.TopRight, this.m_cauldronCard.GetActor(), 3f, 1f, true, false, 0f));
				break;
			case 6:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER3.prefab:7ae0457d447b24e4c8c17ab4120d40cc", Notification.SpeechBubbleDirection.TopRight, this.m_cauldronCard.GetActor(), 3f, 1f, true, false, 0f));
				break;
			case 8:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER4.prefab:1d497fab014f3c64c81ef834d8ee009b", Notification.SpeechBubbleDirection.TopRight, this.m_cauldronCard.GetActor(), 3f, 1f, true, false, 0f));
				break;
			case 10:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER5.prefab:ef08dadc1d255714ab3105ab1b5cd2a8", Notification.SpeechBubbleDirection.TopRight, this.m_cauldronCard.GetActor(), 3f, 1f, true, false, 0f));
				break;
			case 12:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER6.prefab:6ca24415fc389714885735792c3ef46c", Notification.SpeechBubbleDirection.TopRight, this.m_cauldronCard.GetActor(), 3f, 1f, true, false, 0f));
				break;
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER.prefab:e752937402315d043b487ed44f4a461a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			if (!(this.m_cauldronCard == null))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_HERO_POWER1.prefab:4f4322070683ced41a83c03c0527ee0e", 3f, 1f, false, false));
			}
		}
		yield break;
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x0010D41B File Offset: 0x0010B61B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOEA09_WIN.prefab:add3634f635104d49987f553e3f83a8c", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CC6 RID: 7366
	private bool m_finley_death_line;

	// Token: 0x04001CC7 RID: 7367
	private bool m_finley_saved;

	// Token: 0x04001CC8 RID: 7368
	private Card m_cauldronCard;
}
