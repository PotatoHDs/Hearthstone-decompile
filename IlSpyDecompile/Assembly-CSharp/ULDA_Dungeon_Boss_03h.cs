using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_03h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01.prefab:a51a01532d42ad146b2aa33565be4b34");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01.prefab:d010cf5ae2f4dfc4898b4ed186806fa7");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Death_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Death_01.prefab:a6327c291f470f949bbf8cb5104218be");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01.prefab:213adc426d7f1474a9deef70ff873cd9");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:82f4117350cc42447b084876b0992418");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_01.prefab:8a85ecb0a49cf0d42a5c7f78f095334d");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_02.prefab:c0fc67769fe151048b3d0e594ae978df");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_05.prefab:e467b975f9ed44347945d9ec4cd5320c");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_01.prefab:eee83ede7cb5c234d920db60baa9ce35");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_02 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_02.prefab:c785f9f3685821447af9360560687b3b");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_03 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_03.prefab:da256f8fe6d5dad4ba4597194bf33d57");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Intro_01.prefab:d2328bc0879cd1f408381a0aba8822ab");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01.prefab:00517485c0759c445b311efde42de66e");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01.prefab:e980a990444f5f44798d5d123f42846c");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01.prefab:081efa092c272794883e1217241e80da");

	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01.prefab:187596b96e402164fbe57fd39100233d");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_03h_Female_Human_HeroPower_01, VO_ULDA_BOSS_03h_Female_Human_HeroPower_02, VO_ULDA_BOSS_03h_Female_Human_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_03h_Female_Human_Idle_01, VO_ULDA_BOSS_03h_Female_Human_Idle_02, VO_ULDA_BOSS_03h_Female_Human_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01, VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01, VO_ULDA_BOSS_03h_Female_Human_Death_01, VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01, VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01, VO_ULDA_BOSS_03h_Female_Human_HeroPower_01, VO_ULDA_BOSS_03h_Female_Human_HeroPower_02, VO_ULDA_BOSS_03h_Female_Human_HeroPower_05, VO_ULDA_BOSS_03h_Female_Human_Idle_01, VO_ULDA_BOSS_03h_Female_Human_Idle_02,
			VO_ULDA_BOSS_03h_Female_Human_Idle_03, VO_ULDA_BOSS_03h_Female_Human_Intro_01, VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01, VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01, VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01, VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_03h_Female_Human_Intro_01;
		m_deathLine = VO_ULDA_BOSS_03h_Female_Human_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "ULDA_Finley")
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
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
		if (!(cardId == "ULD_195"))
		{
			if (cardId == "ULD_163")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01);
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
		if (!(cardId == "ULD_160"))
		{
			if (cardId == "ULD_170")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01);
		}
	}
}
