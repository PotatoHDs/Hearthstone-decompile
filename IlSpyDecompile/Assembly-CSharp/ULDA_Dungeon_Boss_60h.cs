using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_60h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01.prefab:832d52d6c27ef3a41ae7c11ed17e4466");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01.prefab:b0180209467fc604f8f71c331f28579a");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01.prefab:28ca15992f5c5b84f9c80726ccc37e74");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Death_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Death_01.prefab:8b5e93c5f15d03044b5477876329df5b");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01.prefab:fdbb390966fd8264d906e22a14828473");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01.prefab:218d3d4ccfb266644ac630880e738ccd");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02.prefab:440880063ebdac3449f1df8df66b8871");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03.prefab:c0e0700250a91d249870f2ce5091b303");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04.prefab:db96b1d8e1f1ffe4791fa75534b29d66");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05.prefab:006b499fcea589947b6848dc60fefedf");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01.prefab:943ddd8352ce58246a5b8ce18154e56d");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02.prefab:2a3765d56abef1143b32b60e68ad6513");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03.prefab:6a0576ec316d5954998a27ce1707997f");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01.prefab:22e2fef3828561e46b83e176bacd2147");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01.prefab:275ae9f9c7ac3a845ac36ade2fd10446");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01.prefab:9696080ec36f628499cac7d24e1c5a66");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01.prefab:b6c2a25e18f840e439234c300e9a15c6");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01.prefab:3ddffbfd01e602c4d8325d8da126d37b");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01.prefab:0bf3da4fccdb2af4084a8621a7141878");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01.prefab:f5801d2dc607d5149b12c95ef0d7c2da");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01.prefab:852ff26535557cc4986fe220f77b13d7");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01.prefab:ae6ea80bb8f3a094fa76cc5896beb20d");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01.prefab:47f5a78f2981a7d43b3bddb129d23d1f");

	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01.prefab:37b4ca4b53526784994ef4c6f6170e0d");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01, VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02, VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03 };

	private List<string> m_PlayerTriggerCThun = new List<string> { VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01, VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01, VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01, VO_ULDA_BOSS_60h_Male_Ethereal_Death_01, VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01, VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04, VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05,
			VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01, VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02, VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03, VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01, VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01,
			VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		m_introLine = VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01;
		m_deathLine = VO_ULDA_BOSS_60h_Male_Ethereal_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01);
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
			case "ULD_143":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01);
				break;
			case "ULD_290":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01);
				break;
			case "OG_280":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTriggerCThun);
				break;
			case "NEW1_021":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01);
				break;
			case "CS2_234":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01);
				break;
			case "ULD_216":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01);
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
			switch (cardId)
			{
			case "OG_280":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01);
				break;
			case "OG_141":
			case "DAL_613":
			case "EX1_564":
			case "OG_207":
			case "OG_024":
			case "OG_174":
			case "DAL_744":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01);
				break;
			case "ULD_189":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01);
				break;
			}
		}
	}
}
