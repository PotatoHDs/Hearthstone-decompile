using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_02h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01.prefab:9aefc1756b18c3243bcd1fc9b7ddba95");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01.prefab:440f45fe4088d9644a2a7bdf3957eb1e");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01.prefab:55bfc25fbbd181246b758b80647f1fbb");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01.prefab:1f805fefe136f9749b33bcd30d3d5a18");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01.prefab:9e7eff7b8d4c8ff4baa68bad4c6f7d0a");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01.prefab:3345597ffd2600c4abef8ec08b66c8b4");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01.prefab:8bdfee6ba1794e646a801a7547f795e9");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02.prefab:ea6491933eac5e6489a327d066a9ffe3");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03.prefab:af3da69402a52e94ca677423ac3f0dd8");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04.prefab:8421ff98e4c1ac746ac4789c26d320a1");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05.prefab:bbe26dbaa8c01b949915c809920b4b86");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01.prefab:216e7b15265cb5047b1fac95ad0df5cc");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02.prefab:69155876b0008274a801a5326256b2e4");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03.prefab:f513c26c122f736499228e5f41e4b610");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01.prefab:47b82e642ae7cc242aa9b5388a956488");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01.prefab:8ce518a633961124da0d79bc08561d6e");

	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01.prefab:3b8d27c284dc924468518b254e182440");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04,
			VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03, VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01, VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01
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
		m_introLine = VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Brann")
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
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ULD_198"))
		{
			if (cardId == "ULD_265")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01);
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
			case "ULD_208":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01);
				break;
			case "ULD_723":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01);
				break;
			case "ULD_185":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01);
				break;
			}
		}
	}
}
