using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class KAR06_Crone : KAR_MissionEntity
{
	// Token: 0x06003529 RID: 13609 RVA: 0x0010E8D0 File Offset: 0x0010CAD0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Crone_Female_Troll_CroneEmoteResponse_02.prefab:84a22c615324303489aa69ffb9423a7f");
		base.PreloadSound("VO_Crone_Female_Troll_CroneFlyingMonkeys_01.prefab:8f6b0cbfef4a3384286ec890a06d1d10");
		base.PreloadSound("VO_Crone_Female_Troll_CroneHeroPower_02.prefab:d368549c6d3ac224085adc9379623580");
		base.PreloadSound("VO_KARA_04_01_Female_Human_CroneLionTigerBear_02.prefab:3e90aeef2f8f4a243b34e38327499e93");
		base.PreloadSound("VO_KARA_04_01_Female_Human_CroneHuffer_01.prefab:08d4777be932bda4b9c516544e0f6dea");
		base.PreloadSound("VO_KARA_04_01_Female_Human_CroneTurn1_01.prefab:7b284f2e4c3942749a4841ee78d89d9f");
		base.PreloadSound("VO_KARA_04_01_Female_Human_CroneTurn3_01.prefab:48747ac249add864bb4c2e5a28b27205");
		base.PreloadSound("VO_Moroes_Male_Human_CroneTurn5_02.prefab:28a0e089d96edd241a04d24a6d686be5");
		base.PreloadSound("VO_Moroes_Male_Human_CroneWin_01.prefab:b3887f0f8b013314495204d89d64c121");
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x0010E940 File Offset: 0x0010CB40
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
						m_soundName = "VO_Crone_Female_Troll_CroneHeroPower_02.prefab:d368549c6d3ac224085adc9379623580",
						m_stringTag = "VO_Crone_Female_Troll_CroneHeroPower_02"
					}
				}
			}
		};
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x0010E9A0 File Offset: 0x0010CBA0
	private Actor GetDorothee()
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == "KARA_04_01")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x0010EA34 File Offset: 0x0010CC34
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		this.m_playedLines.Add(item);
		Actor dorothee = this.GetDorothee();
		if (dorothee == null)
		{
			yield break;
		}
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				yield return base.PlayEasterEggLine(dorothee, "VO_KARA_04_01_Female_Human_CroneHuffer_01.prefab:08d4777be932bda4b9c516544e0f6dea", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(dorothee, "VO_KARA_04_01_Female_Human_CroneLionTigerBear_02.prefab:3e90aeef2f8f4a243b34e38327499e93", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x0010EA4A File Offset: 0x0010CC4A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor dorothee = this.GetDorothee();
		if (dorothee == null && turn < 7)
		{
			yield break;
		}
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 10)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_CroneTurn5_02.prefab:28a0e089d96edd241a04d24a6d686be5", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				yield return base.PlayMissionFlavorLine(dorothee, "VO_KARA_04_01_Female_Human_CroneTurn3_01.prefab:48747ac249add864bb4c2e5a28b27205", 2.5f);
			}
		}
		else
		{
			yield return base.PlayOpeningLine(dorothee, "VO_KARA_04_01_Female_Human_CroneTurn1_01.prefab:7b284f2e4c3942749a4841ee78d89d9f", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x0010EA60 File Offset: 0x0010CC60
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "KARA_04_05") && !(cardId == "KARA_04_05h"))
		{
			if (cardId == "KARA_04_02hp")
			{
				yield return base.PlayCriticalLine(actor, "VO_Crone_Female_Troll_CroneEmoteResponse_02.prefab:84a22c615324303489aa69ffb9423a7f", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_Crone_Female_Troll_CroneFlyingMonkeys_01.prefab:8f6b0cbfef4a3384286ec890a06d1d10", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x0010EA76 File Offset: 0x0010CC76
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_CroneWin_01.prefab:b3887f0f8b013314495204d89d64c121", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD8 RID: 7384
	private HashSet<string> m_playedLines = new HashSet<string>();
}
