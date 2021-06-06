using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_61h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01.prefab:5bc6075829115144bbb96ab57e30f76e");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01.prefab:1c06497909716e542b140e741a337ae2");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01.prefab:5e0399e358d6c6d41a01683ea59d1b72");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Death_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Death_01.prefab:fe169976ca10faa429dbbca88f32c74c");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01.prefab:4107a059264ebe14080c27d33ccf7698");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01.prefab:5f7e079bd698eae40b6f54e4aa12049b");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01.prefab:7248c20cbfddbc643b53b2b69bb64936");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03.prefab:94af211a6c463f14e9598b703bfa2930");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04.prefab:3941fdfc471deab478c247119c1a243e");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01.prefab:b91d4aa412f1a404b87385f0a930e6bb");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_01.prefab:0ba028108f7067042a49fa48ce394793");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_02 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_02.prefab:8740a575c7a413c4c8b114e6819e36cf");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_03 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_03.prefab:e1104dc2e694c0f4ba7b921322f1943e");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Intro_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Intro_01.prefab:ba61452640398c040ac0291e228dc5fe");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01.prefab:b8da20929ac465b4680fb17ac71c29b7");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01.prefab:e1ced7aaba56be44abf3eaa034774d49");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01.prefab:224a62861ff08ad4ea789dc65194d6aa");

	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01.prefab:83c124071c0e8a943ba105757e000d6f");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01, VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03, VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_61h_Female_Dragon_Idle_01, VO_ULDA_BOSS_61h_Female_Dragon_Idle_02, VO_ULDA_BOSS_61h_Female_Dragon_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01, VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01, VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01, VO_ULDA_BOSS_61h_Female_Dragon_Death_01, VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01, VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01, VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01, VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03, VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04, VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01,
			VO_ULDA_BOSS_61h_Female_Dragon_Idle_01, VO_ULDA_BOSS_61h_Female_Dragon_Idle_02, VO_ULDA_BOSS_61h_Female_Dragon_Idle_03, VO_ULDA_BOSS_61h_Female_Dragon_Intro_01, VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01, VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01, VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01, VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_61h_Female_Dragon_Intro_01;
		m_deathLine = VO_ULDA_BOSS_61h_Female_Dragon_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01;
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
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
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
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01);
			break;
		case 101:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01);
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
			switch (cardId)
			{
			case "EX1_251":
			case "CFM_707":
			case "EX1_238":
			case "EX1_259":
			case "OG_206":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01);
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
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "TRL_569"))
		{
			if (cardId == "TRL_362")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01);
		}
	}
}
