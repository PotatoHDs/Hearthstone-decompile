using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_27h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01.prefab:ae4d7d24b0d31cd4fa42a9152a036633");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01.prefab:51efdc6a588e31e45ae65109fbb3a04f");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Death_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Death_01.prefab:1eca16b5c40208e46818c432959ed733");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01.prefab:c7a226840a233054d9610e3f4830a73e");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01.prefab:b702f176cc486b3419048a416e01f919");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01.prefab:aef7c50073de7e14680a7a553e3eb809");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01.prefab:04094e304090c4347b42036c5958930b");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01.prefab:3f8b7755d548e5446a44cb3e2f08c74f");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02.prefab:aecd8b3b89fce5847ac7b7790420dfce");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03.prefab:06e3896d1910e0d4c859dbab4d1f5234");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01.prefab:c1db41f51500618498f81fa653ce764f");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02.prefab:2d8dab1eb474c45429e70b50780c604e");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01.prefab:1d1f7fc4f05687a409a71fb0c0717156");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01.prefab:a26ef345b077852448e25711e2cebf75");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02.prefab:20d6a03e0e4b64347a40babd869a6a22");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_01.prefab:932cb54619b385f43a26c946efc6b1e5");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_02.prefab:810832fb9e7259a4aaa85428cb01acb1");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_03.prefab:aa462d99f9421e1459102d9b1c18c6d9");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Intro_01.prefab:df7ab420a70f3dc4ea6c66d4b5c028ab");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01.prefab:2fc172ee9e2c8484c990f0c9287f388e");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Misc_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Misc_01.prefab:cf2f70db09005814ebd425414e58984a");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01.prefab:b4ae5f0cd3ef43447b8724ad3df8d155");

	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01.prefab:29eb9af2b7c78f743880581f0e5014f0");

	private static List<string> m_HeroPowerSmall = new List<string> { VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02, VO_DALA_BOSS_27h_Male_BloodElf_Misc_01 };

	private static List<string> m_HeroPowerLarge = new List<string> { VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02, VO_DALA_BOSS_27h_Male_BloodElf_Misc_01 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02, VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03, VO_DALA_BOSS_27h_Male_BloodElf_Misc_01 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_27h_Male_BloodElf_Idle_01, VO_DALA_BOSS_27h_Male_BloodElf_Idle_02, VO_DALA_BOSS_27h_Male_BloodElf_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01, VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01, VO_DALA_BOSS_27h_Male_BloodElf_Death_01, VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01, VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01, VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01, VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02, VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03,
			VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01, VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02, VO_DALA_BOSS_27h_Male_BloodElf_Idle_01, VO_DALA_BOSS_27h_Male_BloodElf_Idle_02, VO_DALA_BOSS_27h_Male_BloodElf_Idle_03, VO_DALA_BOSS_27h_Male_BloodElf_Intro_01, VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01,
			VO_DALA_BOSS_27h_Male_BloodElf_Misc_01, VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01, VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01
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
		m_introLine = VO_DALA_BOSS_27h_Male_BloodElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_27h_Male_BloodElf_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Tekahn" && cardId != "DALA_Eudora" && cardId != "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerSmall);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLarge);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01);
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
			case "GIL_663t":
			case "FP1_019t":
			case "EX1_158t":
			case "DAL_256t2":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "AT_033"))
		{
			if (cardId == "CS1_113")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01);
		}
	}
}
