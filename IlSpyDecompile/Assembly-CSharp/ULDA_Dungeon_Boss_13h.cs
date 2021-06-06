using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_13h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01.prefab:fc14d47dc8353244d93b225bd0389c68");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01.prefab:29f876551907be54a9db5b338b6c7bd2");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01.prefab:ab431ecfefb581b4b89797fde7a7661c");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Death_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Death_01.prefab:bcad9230fd3554c408c6d5bdbbc8d2e9");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01.prefab:06ad541e8ea9f3048bf5edf41bc654c9");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01.prefab:9a15c02ed8506bd42974df75bde1e269");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01.prefab:b64af718a490c34448d870288a2c89d0");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02.prefab:0abadc0695b27f74ea260c27ba85a77a");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04.prefab:c3aac27f4d0a7124db98697baaf3a284");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05.prefab:8badb20ec4bdc344488e0cb2f15621ab");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01.prefab:1958c5c47397efc4bacecdb7a69cb021");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02.prefab:868cda1fa725f9a429554cdfdf937c6e");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03.prefab:ad7ff5e31b93cc34bb27cdf541147cba");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01.prefab:d637f53e47dd1ca4f8252cf959248287");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01.prefab:b0e46475ae10b1547ba3bfb3d2756cbf");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01.prefab:f55b0076b4967ea419ad8781253b73ca");

	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01.prefab:97f0e105405f8a04ea032721288798f1");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01, VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02, VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01, VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01, VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01, VO_ULDA_BOSS_13h_Male_Gnoll_Death_01, VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01, VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04, VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05,
			VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01, VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02, VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03, VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01, VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01, VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01, VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01
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
		m_introLine = VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01;
		m_deathLine = VO_ULDA_BOSS_13h_Male_Gnoll_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "ULDA_Reno")
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
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01);
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
			case "ULD_154":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01);
				break;
			case "DAL_371":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01);
				break;
			case "GIL_826":
			case "UNG_086":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01);
				break;
			case "EX1_554":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01);
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
		}
	}
}
