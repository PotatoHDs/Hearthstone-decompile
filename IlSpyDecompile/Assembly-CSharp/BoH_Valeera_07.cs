using System.Collections;
using System.Collections.Generic;

public class BoH_Valeera_07 : BoH_Valeera_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01.prefab:ca0a78c496b307f4d978e968148ee73a");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01.prefab:e6e6afd4839ebc64f9999af312fc5e07");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01.prefab:406c3e8392a4e4d40bf4a30d6dda954b");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02.prefab:36d4f03c022f4ec4693955416caa1c57");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01.prefab:ad52fa28b2a8451459405f99e7892151");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02.prefab:09af7309e6ef6b94dad841fdd7019598");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03.prefab:3cdc9e3aee956d34784c11cf1460891b");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03.prefab:a24855c53cc35ff489f54bc6478723b3");

	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01.prefab:cc804cecd9372574194cbaaac6652de5");

	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01.prefab:7629175f09311124190bb4163c0084e4");

	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02.prefab:85ecd6281705ded48b92e120122261ba");

	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02.prefab:91716e73c2c7a75428c8b1dae0c8b0ba");

	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01.prefab:b7d834ca9d05a1640a2d95f2f2fd967c");

	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04.prefab:7c3b14fb3dd0f284481252da995310c9");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02.prefab:fa404c845cad50945badfa8062f71af7");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02.prefab:ff6a3372d722839469b7d8acbdf8c03b");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01.prefab:0f6f9ae519e612644ae5d51dadbfe6bb");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01.prefab:cc8606b2f3a208b4aa9c493bdcc0fa69");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03.prefab:f684987f102468a44a31d7e5b657de34");

	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01.prefab:1365b33c2e210804f9e76f9e27a838c5");

	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03.prefab:60cc81f8fef1ce64a8cc762754149b43");

	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02.prefab:7a47b055f0d06ae4cb29e3f81989aff5");

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01, VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01,
			VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02, VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02, VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01, VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01,
			VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01;
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DRG;
		m_standardEmoteResponseLine = VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_Meryl"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_MerylDormant"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 106:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_Meryl"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 107:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_MerylDormant"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01);
			yield return MissionPlayVO(GetFriendlyActorByCardId("Story_06_Meryl"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01);
			yield return MissionPlayVO(GetFriendlyActorByCardId("Story_06_MerylDormant"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 108:
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_Meryl"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02);
			break;
		case 109:
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_MerylDormant"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02);
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_06_Meryl"), VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02);
			break;
		case 5:
			yield return PlayLineAlways(actor, VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02);
			break;
		}
	}
}
