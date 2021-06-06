using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_65h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01.prefab:c079a30c3920f9b46ba88bc4e0eaa7ed");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01.prefab:7d2e2be11d4f00d47a16f7ab1508db45");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01.prefab:f946894bfb47d1e4196e94c7c9418d34");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01.prefab:c79c62a4416368846b0302ee2bcce7b4");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01.prefab:15c95fa5e23c219419888025f800b9b8");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01.prefab:4373ea7232369e14582be39fff1b343c");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01.prefab:6b585040d8ebccd42b88c4d8fa743c3a");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02.prefab:27d7ab56ae04df143990810b8b2ae39b");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03.prefab:bd6da2634009c21419eff71b2b0c0a74");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04.prefab:345c121d1b494f941a4c5fcf03f341b6");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05.prefab:50d90bb20d0840c41a3b17ff131abff6");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01.prefab:7cd7e9d0c1f7e1348a605e1c5674c41e");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Idle_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Idle_01.prefab:48bf2a053e38b2848840a4de30786d70");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Idle_02 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Idle_02.prefab:01f2cea5275006c44adecb6d4b48ce80");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Intro_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Intro_01.prefab:65dc909e58a43964d99c99a729d36fb3");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01.prefab:22d9f1e8364eb354b9fb4c431eeb3401");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01.prefab:56ae90ddf360c2d4b8a9e614b97278d8");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01.prefab:3bde0e046b335e741b32e4b3905a6a2b");

	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01.prefab:682004f7068986c42ae87839d9571b82");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_65h_Male_Ogre_Idle_01, VO_ULDA_BOSS_65h_Male_Ogre_Idle_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01, VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01, VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01, VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01, VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01, VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03, VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04,
			VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05, VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01, VO_ULDA_BOSS_65h_Male_Ogre_Idle_01, VO_ULDA_BOSS_65h_Male_Ogre_Idle_02, VO_ULDA_BOSS_65h_Male_Ogre_Intro_01, VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01
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
		m_introLine = VO_ULDA_BOSS_65h_Male_Ogre_Intro_01;
		m_deathLine = VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Reno")
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "ULD_214":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01);
				break;
			case "ULD_133":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01);
				break;
			case "ULD_163":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01);
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
			case "ULD_328":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01);
				break;
			case "ULD_229":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01);
				break;
			case "DAL_714":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01);
				break;
			}
		}
	}
}
