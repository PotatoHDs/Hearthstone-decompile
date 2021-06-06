using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_56h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_BossBetrayal_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_BossBetrayal_01.prefab:0e9397ed2c94b934abae7d935e4d9cf2");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_BossBurgle_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_BossBurgle_01.prefab:3c8ec2ea8400ea64bb8ff7de30b7c5bf");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_BossTreasure_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_BossTreasure_01.prefab:aebe9b7a39a433f429e53bd599e3eab6");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_BossVanish_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_BossVanish_01.prefab:ae5e5bc60685ce144ac18929f0da2407");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Death_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Death_01.prefab:30a7ebb79a7d8d64c83fa741867ce128");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayer_01.prefab:5cdb1c8f5b0d4b04fac260fb59cce987");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayerRakanishu_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayerRakanishu_01.prefab:b454cfc8102481746ac2dcb0f4e1730b");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_EmoteResponse_01.prefab:5f5657d59ce9f54448ba89d7bd868bb6");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPower_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPower_01.prefab:c593252c3ca01f14391e3b3bf3406a65");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPower_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPower_02.prefab:8760388e515f643419355261e87b970a");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPower_03 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPower_03.prefab:fe125718c05e40243b05f925332d280a");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPower_04 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPower_04.prefab:315c28d1615e0f94a9af8b1028714373");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPower_05 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPower_05.prefab:13fd29aee7439004d8e7fff7657a14c2");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerDoomsayer_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerDoomsayer_01.prefab:d5c49934919fc7c4b8594a707e435138");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_01.prefab:d165550d640b87b488e7e18af1ef6e19");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_02.prefab:d78739bc9799f5f4ba151879737ab3c9");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerTreasure_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerTreasure_01.prefab:5ade7396f9f799c43bc9105d2fc87203");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_01.prefab:732f8bf3ab54f024a8058ddbf7c6c8da");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_03 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_03.prefab:07f7c2756d0cdd443832daaf99d95b7a");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Idle_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Idle_01.prefab:fd85650fc35f15d46b8fc8f7c71ae474");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Idle_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Idle_02.prefab:7bfcd0064fc7be140810aa9f89f4ea41");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Idle_03 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Idle_03.prefab:9a4aab048305b674b8c432e9e642ef7f");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_IntroFirst_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_IntroFirst_01.prefab:5e070b0bc0f8c304e84b3703c5fd42b7");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_IntroGeorge_01.prefab:345320c94f5985243af3b592222209bd");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_IntroRakanishu_01.prefab:d12e04c659f74214ba7c5c4d575de4ff");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_IntroSqueamlish_01.prefab:b505370a71809134b81aca8be94d97bb");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Misc_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Misc_01.prefab:7ed5364bf61d4aa4b8f6352aafdf26b3");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_Misc_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_Misc_02.prefab:44151ecd557e9fa418effaab6265e0c9");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_PlayerCandle_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_PlayerCandle_01.prefab:b4c5336f3e25a2649a79207e1d0fd3a8");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_PlayerDarkness_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_PlayerDarkness_01.prefab:87c8a0a5bd695fa429bd5bd7be723716");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_01.prefab:b3632dc64df0ba94380b156d06d56f49");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_02.prefab:62cb1aa3a53dc1a4496fd283c8a4c135");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_01 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_01.prefab:f8a8761002844274ebc91d489c5d397a");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_02 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_02.prefab:0d535958f9747c54dad5daf2b78760eb");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_03 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_03.prefab:b22b63d54c933a049b3a531995be42c5");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_04 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_04.prefab:9d48f35a3ffa7cc418af75b852b111a8");

	private static readonly AssetReference VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_05 = new AssetReference("VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_05.prefab:aa61a26efa204b14a869a98603b84348");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_56h_Female_Kobold_Idle_01, VO_DALA_BOSS_56h_Female_Kobold_Idle_02, VO_DALA_BOSS_56h_Female_Kobold_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_56h_Female_Kobold_HeroPower_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_02, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_03, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_04, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_05 };

	private static List<string> m_HeroPowerStrong = new List<string> { VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_02 };

	private static List<string> m_HeroPowerWeak = new List<string> { VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_03 };

	private static List<string> m_ReturnMinion = new List<string> { VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_01, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_02, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_03, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_04, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_56h_Female_Kobold_BossBetrayal_01, VO_DALA_BOSS_56h_Female_Kobold_BossBurgle_01, VO_DALA_BOSS_56h_Female_Kobold_BossTreasure_01, VO_DALA_BOSS_56h_Female_Kobold_BossVanish_01, VO_DALA_BOSS_56h_Female_Kobold_Death_01, VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayer_01, VO_DALA_BOSS_56h_Female_Kobold_DefeatPlayerRakanishu_01, VO_DALA_BOSS_56h_Female_Kobold_EmoteResponse_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_02,
			VO_DALA_BOSS_56h_Female_Kobold_HeroPower_03, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_04, VO_DALA_BOSS_56h_Female_Kobold_HeroPower_05, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerDoomsayer_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerStrong_02, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerTreasure_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_01, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerWeak_03, VO_DALA_BOSS_56h_Female_Kobold_Idle_01,
			VO_DALA_BOSS_56h_Female_Kobold_Idle_02, VO_DALA_BOSS_56h_Female_Kobold_Idle_03, VO_DALA_BOSS_56h_Female_Kobold_IntroFirst_01, VO_DALA_BOSS_56h_Female_Kobold_IntroGeorge_01, VO_DALA_BOSS_56h_Female_Kobold_IntroRakanishu_01, VO_DALA_BOSS_56h_Female_Kobold_IntroSqueamlish_01, VO_DALA_BOSS_56h_Female_Kobold_Misc_01, VO_DALA_BOSS_56h_Female_Kobold_Misc_02, VO_DALA_BOSS_56h_Female_Kobold_PlayerCandle_01, VO_DALA_BOSS_56h_Female_Kobold_PlayerDarkness_01,
			VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_01, VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_02, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_01, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_02, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_03, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_04, VO_DALA_BOSS_56h_Female_Kobold_ReturnMinion_05
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
		m_introLine = VO_DALA_BOSS_56h_Female_Kobold_IntroFirst_01;
		m_deathLine = VO_DALA_BOSS_56h_Female_Kobold_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_56h_Female_Kobold_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_56h_Female_Kobold_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_56h_Female_Kobold_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_BossTreasure_01);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerWeak);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerStrong);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerDoomsayer_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_HeroPowerTreasure_01);
			break;
		case 107:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_ReturnMinion);
			break;
		case 108:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_Misc_01);
			break;
		case 109:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_Misc_02);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "LOOT_541":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_01);
				break;
			case "DAL_417":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_56h_Female_Kobold_PlayerTogwaggle_02);
				break;
			case "LOOTA_843":
			case "DALA_714":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_56h_Female_Kobold_PlayerCandle_01);
				break;
			case "LOOT_526":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_56h_Female_Kobold_PlayerDarkness_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "EX1_126":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_BossBetrayal_01);
				break;
			case "AT_033":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_BossBurgle_01);
				break;
			case "NEW1_004":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_56h_Female_Kobold_BossVanish_01);
				break;
			}
		}
	}
}
