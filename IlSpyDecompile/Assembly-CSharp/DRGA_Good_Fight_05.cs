using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_05 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01.prefab:6b1c03ee04f65fa45a52bc750928972a");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01.prefab:795fa85396a97d645915a178ddf4d9ab");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01.prefab:f6b748b0336dafd4a8d7262400abb69a");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01.prefab:2bfab2af5d2ab4444b01de94083c7359");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01.prefab:41ecc41ce2f5a6440baf49cb5ea2859f");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01.prefab:25c50c83185f8b342aac98d6633d2da2");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01.prefab:6fe20513ca1655d41bcd5d97fe8286c0");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01.prefab:13ed3a03a6b70c74a9764c2185860d87");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01.prefab:eaf74a16b14a52a41806c98b816a9040");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01.prefab:4a09dac33a99ace488a5ad2fff3c1e8b");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01.prefab:50f7d8f4491fbe44fb9857699ca22aaa");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01.prefab:d4c16850319e8be48b0f4b6bfffbd78b");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01.prefab:fc71e710d621b744d91ff2bca3eb1c7e");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01.prefab:e594c172775313a4d98d0cead7bf27a2");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01.prefab:1199239b067273645804d12696ab7150");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01.prefab:2ba5a96a6dfdc4e40bb2c07ce1ec6292");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01.prefab:42cfddb3cc8fd764a93e0d433a237e90");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01.prefab:eda9bdca870e15547b0dfdec81f464c6");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01.prefab:7220336bbc1278948a24377e9433b01b");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01.prefab:04ecf74a86f70654abe5752dcb6cd855");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01.prefab:d07d6b31df27f494698be08a04c27252");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01.prefab:9c7ca64e4201b7b4190301c5c5276d78");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01.prefab:6ae3a7e92c0fce744abf0513e6824062");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01.prefab:c42d78f52e2c59941b012a51ee69a19d");

	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01.prefab:39ad410ddd1a5b5458804cd3b7d3bd2f");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01.prefab:f95d0ebd33f847f49bf4cf60158c1bf9");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01.prefab:f4746670ef04a7e479c939403b16a164");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01.prefab:eb681105b9f05cf4199771701ab33bbe");

	private List<string> m_missionEventTrigger100Lines = new List<string> { VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01 };

	private List<string> m_missionEventTrigger101Lines = new List<string> { VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01 };

	private List<string> m_Player_Dragon_01_01 = new List<string> { VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01 };

	private static List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond = new List<string> { VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01 };

	private static List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond = new List<string> { VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01 };

	private List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy = new List<string>(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond);

	private List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy = new List<string>(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond);

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01,
			VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01,
			VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01, VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01, VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger100Lines);
			break;
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger101Lines);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01);
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01);
			}
			break;
		case 105:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_Player_Dragon_01_01);
			break;
		case 110:
			m_Galakrond = true;
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "YOD_013":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01);
				break;
			case "DRG_235":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01);
				break;
			case "TRL_302":
				yield return PlayLineOnlyOnce(actor2, VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01);
				break;
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "ICC_027")
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01);
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (!m_Galakrond)
			{
				string line = PopRandomLine(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy);
				if (m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy.Count == 0)
				{
					m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy = new List<string>(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond);
				}
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			}
			else
			{
				string line2 = PopRandomLine(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy);
				if (m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy.Count == 0)
				{
					m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy = new List<string>(m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond);
				}
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line2));
			}
		}
		else
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
		}
	}
}
