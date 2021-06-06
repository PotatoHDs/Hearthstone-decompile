using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_05 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01.prefab:997fcc78b0cb3fe4fbec72709bcee854");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02.prefab:b70268c69b6fcfe44a884a2332281f05");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01.prefab:2f1d78ced905b924a8b7e095df3072f6");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01.prefab:728c0896ee6c3ae43991f81dd9cd21fa");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01.prefab:6fb03e885e5140a47a705acd75232b89");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01.prefab:6d2ecdfadf3bce347878adecd0b587d0");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01.prefab:87e8d2cb6f399ee46a2b5bf1d9500eb0");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02.prefab:a1a24e27312657741b588fe92ac39f86");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03.prefab:30be6009789d5af41996148c83158ad3");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01.prefab:d3baf9d334cc64c4291fdc03ca7082c3");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02.prefab:ddc3334a445dc6d49bb758de4924bad4");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03.prefab:132abd92e75104748b9f0e53df4e4eb7");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01.prefab:94bd06ace9e7b2446829a7a780a7f9f0");

	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01.prefab:6679e11bb7f9d704dbdbd23961d754ef");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:fa0da701f910f7c4b8405a9da4992d55");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01.prefab:b240c16eba247d248b5d5e1a3e295422");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01.prefab:3b7fab7a81b8ee941931847b27517c20");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01.prefab:1432f5c6ded7ab64aa0b200dd057e7f2");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01.prefab:0228474dc52500d4bb83de6d1295a2c0");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02.prefab:8bc801ed88bd6bf40bfe597daae5f6a0");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01.prefab:bc321e0aa483b2b4bb0cdcddb89f3ff4");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01.prefab:1a6e39f4d9cd0b849904b3f0e974bb46");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02.prefab:a320a54cbadebf44db61d3a51ebbde8b");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01.prefab:8fe4e45a1df13dc4f94043292557f3b2");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01.prefab:4da7e895d72989e459030bacd0263096");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01.prefab:09f65539eb4190a4eb5517a7df375319");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01.prefab:a281acc43a6ef4d4a9eb83996b93fc28");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01.prefab:8f4adabd70a55eb4aaf239e3d5616081");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01.prefab:5f8dcfef5a39fa945bef952cc027b04d");

	private static readonly AssetReference VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01 = new AssetReference("VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01.prefab:82d107a16b46519499058165c6d0f7f6");

	public static readonly AssetReference DaelinBrassRing = new AssetReference("Daelin_BrassRing_Quote.prefab:8553800b28758a44da69e1cd9bdacf07");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	private List<string> m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPowerLines = new List<string> { VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03 };

	private List<string> m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5IdleLines = new List<string> { VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_05()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01,
			VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02,
			VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01, VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01);
		yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
		{
			GameState.Get().SetBusy(busy: true);
			Actor enemyActorByCardId = GetEnemyActorByCardId("Story_02_WoundedFootman");
			if (enemyActorByCardId != null)
			{
				yield return PlayLineAlways(enemyActorByCardId, VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01);
			}
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02);
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 201:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01);
			break;
		case 202:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01);
			break;
		case 203:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01);
			break;
		case 204:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01);
			yield return PlayLineAlways(DaelinBrassRing, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01);
			yield return PlayLineAlways(DaelinBrassRing, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 17:
			yield return PlayLineAlways(actor, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01);
			break;
		case 19:
			yield return PlayLineAlways(actor, VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}
}
