using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_52h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01.prefab:cfa446a643d60a64ea09b62b59456d34");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01.prefab:37f600da9b03cac40b2fc5fded125e4a");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02.prefab:816f980a808332c488cc5b0d9817b385");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Death_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Death_02.prefab:6a6881dd6ac050940a6e0f0632b77934");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01.prefab:1fb99876dc000524e81c8f1ce8a5567f");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01.prefab:e43b11a56a2f88f44bb46ff323af670d");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_01.prefab:e933330d1634bab45aa1a4876f2d7489");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_02.prefab:2830b09c31945764e9e565a3eb30a08d");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_03.prefab:f7c6859c8c9989c44a2aa0b8cc346ad8");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_04.prefab:e594193c4461de84fbfc611586ea54e1");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_06 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_06.prefab:5a83fe616fe20f4488b80b1cdc86aa64");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_01.prefab:a4fdbd70f32e84b4893689942008fca6");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_02.prefab:3e80a289b9ef37b4eb353f51f591cd51");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_03.prefab:f280da6703835b34190f7c55d17f6366");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Intro_01.prefab:5879922272189e94792461b07a327cdd");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_IntroChu_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_IntroChu_01.prefab:49870e98d3bf026408f1f03630c4fe3c");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01.prefab:f18b673fd046d5a4192696af12901f01");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01.prefab:35262de6dd338884eac1cc2ece516582");

	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01.prefab:75175cb91086157479d32c468547a002");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_52h_Female_Human_Idle_01, VO_DALA_BOSS_52h_Female_Human_Idle_02, VO_DALA_BOSS_52h_Female_Human_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01, VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01, VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02, VO_DALA_BOSS_52h_Female_Human_Death_02, VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01, VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01, VO_DALA_BOSS_52h_Female_Human_HeroPower_01, VO_DALA_BOSS_52h_Female_Human_HeroPower_02, VO_DALA_BOSS_52h_Female_Human_HeroPower_03, VO_DALA_BOSS_52h_Female_Human_HeroPower_04,
			VO_DALA_BOSS_52h_Female_Human_HeroPower_06, VO_DALA_BOSS_52h_Female_Human_Idle_01, VO_DALA_BOSS_52h_Female_Human_Idle_02, VO_DALA_BOSS_52h_Female_Human_Idle_03, VO_DALA_BOSS_52h_Female_Human_Intro_01, VO_DALA_BOSS_52h_Female_Human_IntroChu_01, VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01, VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01, VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_52h_Female_Human_HeroPower_01, VO_DALA_BOSS_52h_Female_Human_HeroPower_02, VO_DALA_BOSS_52h_Female_Human_HeroPower_03, VO_DALA_BOSS_52h_Female_Human_HeroPower_04, VO_DALA_BOSS_52h_Female_Human_HeroPower_06, VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_52h_Female_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_52h_Female_Human_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01;
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
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_52h_Female_Human_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish")
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
			case "DALA_722":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01);
				break;
			case "GVG_085":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01);
				break;
			case "DALA_726":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01);
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
		if (!(cardId == "DAL_095"))
		{
			if (cardId == "DAL_096")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01);
		}
	}
}
