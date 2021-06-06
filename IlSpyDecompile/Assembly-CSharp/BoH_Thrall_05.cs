using System.Collections;
using System.Collections.Generic;

public class BoH_Thrall_05 : BoH_Thrall_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01.prefab:c202f9e018853d34aa7cd3b170bbdbe8");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01.prefab:4f16ea7a791dd2840b821e43c4b42fff");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01.prefab:4a291c7eb5b14a746bbc1003174a6f21");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02.prefab:f99c12dbfd44d0a4298953bdad0858f0");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01.prefab:3f82a78d60a4f6a478151160c9f7c69d");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01.prefab:9bafc7ca0daed364a924acf2c3a285c8");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02.prefab:f14e857b02ace0a4c9e5e90079d98cf1");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03.prefab:e66307477c710aa40b5282cee8c08daa");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01.prefab:328671bb439191b4083ac5f1bd275d26");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02.prefab:3e5cc1ae68fb70f45b480e616e9704a4");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03.prefab:c2e738e549e1f964ab6e2d8167212a7c");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01.prefab:cb62db362889df3439c0f8763137926c");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01.prefab:1c2c7ae0fa332894ba11a77fcfa2d13c");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01.prefab:88af02a07f66f23489dc599a3f77c402");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01.prefab:db2b6786a83c30c4bab684c475433d2d");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01.prefab:97ba31a31434a1c48be3e4572b5ae710");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01.prefab:bc1fd3a3f7bc9e5498839203a0e16bcc");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01.prefab:5a4f9acbc7645df4f901722085f27e05");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01.prefab:2c88f209293c21243a61b4655f8a5176");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01.prefab:e5d6ea3ae3bd52649aaa5494bddde917");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03.prefab:6d4d1d899cdc9364f88de7f99f77fa7c");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:2f10ce18f13940249bec390be8571972");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Thrall_05()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
			VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01,
			VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03
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

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01);
		yield return MissionPlayVO(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGLOEBoss;
		m_standardEmoteResponseLine = VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01);
			break;
		case 8:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01);
			break;
		case 1:
			yield return PlayLineAlways(actor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01);
			break;
		}
	}
}
