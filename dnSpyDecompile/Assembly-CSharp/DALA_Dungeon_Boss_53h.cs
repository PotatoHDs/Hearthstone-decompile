using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000462 RID: 1122
public class DALA_Dungeon_Boss_53h : DALA_Dungeon
{
	// Token: 0x06003CE2 RID: 15586 RVA: 0x0013DE48 File Offset: 0x0013C048
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_Death_01,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_Intro_01,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04,
			DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CE3 RID: 15587 RVA: 0x0013DFDC File Offset: 0x0013C1DC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01;
	}

	// Token: 0x06003CE4 RID: 15588 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x0013E014 File Offset: 0x0013C214
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George" && cardId != "DALA_Barkeye" && cardId != "DALA_Rakanishu" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x0013E0E6 File Offset: 0x0013C2E6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_53h.m_BossHeroPower);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_53h.m_PlayerMurloc);
		}
		yield break;
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x0013E0FC File Offset: 0x0013C2FC
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "AT_076")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x0013E112 File Offset: 0x0013C312
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
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x040026DD RID: 9949
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_Death_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_Death_01.prefab:e3bf95c5ab6eec54d808f1250ab22e1e");

	// Token: 0x040026DE RID: 9950
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02.prefab:4dc3e58ff8eaa3543bb49056fbc71529");

	// Token: 0x040026DF RID: 9951
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01.prefab:71e7c09ba24fca74db76a97b7a6e1667");

	// Token: 0x040026E0 RID: 9952
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01.prefab:1039d921dad26eb41951d60dd05c96b6");

	// Token: 0x040026E1 RID: 9953
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02.prefab:099a69e4686ebe348a585e20be363c58");

	// Token: 0x040026E2 RID: 9954
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03.prefab:4d6b1895e7904e54c8ad30d435328e67");

	// Token: 0x040026E3 RID: 9955
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04.prefab:1cb1390b529f3ec46bec334b065851c8");

	// Token: 0x040026E4 RID: 9956
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05.prefab:def450c772088e24387d584759cb21d8");

	// Token: 0x040026E5 RID: 9957
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06.prefab:e50af7eac2fb44648acbc231c30eb8ed");

	// Token: 0x040026E6 RID: 9958
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07.prefab:3b7e42fcbca56df43a5d23ea684b51b9");

	// Token: 0x040026E7 RID: 9959
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08.prefab:5fff01046a5fbda40bf2edf2b921918a");

	// Token: 0x040026E8 RID: 9960
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09.prefab:f0137bd3e6e1ebe4a8aa54cace26c1a6");

	// Token: 0x040026E9 RID: 9961
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11.prefab:1092e920fa881654aa59ddd397f0698f");

	// Token: 0x040026EA RID: 9962
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_Intro_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_Intro_01.prefab:23e54644913977444af93befdfc3ec27");

	// Token: 0x040026EB RID: 9963
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01.prefab:13ed21ba65aa81841a3def332d19940c");

	// Token: 0x040026EC RID: 9964
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02.prefab:9df0a564df5893a48a648367cf634804");

	// Token: 0x040026ED RID: 9965
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03.prefab:90a45315c0871e1458be5af01ea73121");

	// Token: 0x040026EE RID: 9966
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04.prefab:ed859e85cf96b0847b95f469a1f22eca");

	// Token: 0x040026EF RID: 9967
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01.prefab:4b788431304575149a556655330c896a");

	// Token: 0x040026F0 RID: 9968
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040026F1 RID: 9969
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11
	};

	// Token: 0x040026F2 RID: 9970
	private static List<string> m_PlayerMurloc = new List<string>
	{
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03,
		DALA_Dungeon_Boss_53h.VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04
	};
}
