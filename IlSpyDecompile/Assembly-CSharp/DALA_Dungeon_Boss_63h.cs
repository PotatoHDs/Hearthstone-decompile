using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_63h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01.prefab:9784661d7e311d849bf1835ef8179ad7");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01.prefab:8dff18608f17a894b8cb74d0c039e9e4");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Death_01.prefab:1a4a883ab3a4fe04d95aabd6d04bf895");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01.prefab:bbd0d481210cce443bd5039f7d45678d");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01.prefab:9de198d6fd5a14f4caa17ca7a28e87ea");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_01.prefab:cdbef254005d58e4f90ddc35bfb49d51");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_02.prefab:056b67e688cc12e4ca622361ab47b20f");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_03.prefab:05bf56009309d504a977a4ad167583f1");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_04.prefab:488c9e1d211e6844ba412e8534832820");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_06.prefab:00d17bdb4f9ea7948af3055891fba90b");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_07.prefab:f8a993b2f7342074a8437ae9a20b53fa");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_08.prefab:440cdcfdab0e5a3449c10a29ad26361b");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01.prefab:8e1bdf0f90b1cce45a05f3cc8a997d14");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02.prefab:98b3e8862b840154d809e0ae7a33b855");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03.prefab:8cff32844530f6042ad345db5dc2ccec");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_01.prefab:0ef683245d33d974cacc58065fcb6e0b");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_02.prefab:14a8caa11d7b0c640a74fe8307987266");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_03.prefab:391b87c4109fbf74699041f93c5b1107");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Intro_01.prefab:656ea1eb30a094f448ef27cb5f474710");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01.prefab:e6dc51ef6513a0545a7fcb58e286d892");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01.prefab:17171b8a17cf96641ae127666f5a6d64");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01.prefab:19d54d88746302f4091eece73b5092b3");

	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01.prefab:4aafbb8a6461e3f41b1e55d5dfb993d4");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_63h_Male_Orc_Idle_01, VO_DALA_BOSS_63h_Male_Orc_Idle_02, VO_DALA_BOSS_63h_Male_Orc_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_63h_Male_Orc_HeroPower_01, VO_DALA_BOSS_63h_Male_Orc_HeroPower_02, VO_DALA_BOSS_63h_Male_Orc_HeroPower_03, VO_DALA_BOSS_63h_Male_Orc_HeroPower_04, VO_DALA_BOSS_63h_Male_Orc_HeroPower_06, VO_DALA_BOSS_63h_Male_Orc_HeroPower_07, VO_DALA_BOSS_63h_Male_Orc_HeroPower_08 };

	private static List<string> m_HeroPowerRare = new List<string> { VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01, VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02, VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01, VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01, VO_DALA_BOSS_63h_Male_Orc_Death_01, VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01, VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01, VO_DALA_BOSS_63h_Male_Orc_HeroPower_01, VO_DALA_BOSS_63h_Male_Orc_HeroPower_02, VO_DALA_BOSS_63h_Male_Orc_HeroPower_03, VO_DALA_BOSS_63h_Male_Orc_HeroPower_04, VO_DALA_BOSS_63h_Male_Orc_HeroPower_06,
			VO_DALA_BOSS_63h_Male_Orc_HeroPower_07, VO_DALA_BOSS_63h_Male_Orc_HeroPower_08, VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01, VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02, VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03, VO_DALA_BOSS_63h_Male_Orc_Idle_01, VO_DALA_BOSS_63h_Male_Orc_Idle_02, VO_DALA_BOSS_63h_Male_Orc_Idle_03, VO_DALA_BOSS_63h_Male_Orc_Intro_01, VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01,
			VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01, VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01, VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01
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
		m_introLine = VO_DALA_BOSS_63h_Male_Orc_Intro_01;
		m_deathLine = VO_DALA_BOSS_63h_Male_Orc_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01;
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
			if (cardId != "DALA_Eudora" && cardId != "DALA_Chu" && cardId != "DALA_Squeamlish")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerRare);
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
			case "CFM_669":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01);
				break;
			case "TRL_321":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01);
				break;
			case "EX1_607":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01);
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
			if (cardId == "EX1_407")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01);
			}
		}
	}
}
