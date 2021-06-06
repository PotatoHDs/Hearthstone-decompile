using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_48h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01.prefab:1b3d9aa090490084cb5c69b5d6e7d911");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01.prefab:57241ec4c761c5c47aac98f4efd46c45");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01.prefab:dc0224756d9ed0a4ba83d16021db6ac1");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01.prefab:4b012baa630fc644e8d0dab6253c2a39");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01.prefab:47871a3b50561e2459b80724e45dd027");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01.prefab:1ff3d71246673cf4d9042798c40bcb0f");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01.prefab:8b2b23e5c9ecb4445925d949680861cd");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02.prefab:69ef1dfe547c9934788dd4a50e3b193b");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03.prefab:99cfa9ca66006244a9302dc6556d49a6");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_01.prefab:d2a4403d971a58b4780bf479c36cb186");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_02 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_02.prefab:a33b5cdcd6876d54c9109533f887c84d");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_03 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_03.prefab:52d9faf2ab95f4244ad180726bcf3622");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Intro_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Intro_01.prefab:7fbbd04bfd86a014db7feba3d49717ff");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01.prefab:b29c74c6cc52c9b43b2b128e16d69fc6");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01.prefab:1c940300f467aec4296e23d5ff09ae78");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01.prefab:0b21f1b9465901f48aa8bf0a1b1675f5");

	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01.prefab:450a9127f0e56be47a40dc22f06a648a");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01, VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02, VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_48h_Female_Maghar_Idle_01, VO_ULDA_BOSS_48h_Female_Maghar_Idle_02, VO_ULDA_BOSS_48h_Female_Maghar_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01, VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01, VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01, VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01, VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01, VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01, VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01, VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02, VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03, VO_ULDA_BOSS_48h_Female_Maghar_Idle_01,
			VO_ULDA_BOSS_48h_Female_Maghar_Idle_02, VO_ULDA_BOSS_48h_Female_Maghar_Idle_03, VO_ULDA_BOSS_48h_Female_Maghar_Intro_01, VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01
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
		m_introLine = VO_ULDA_BOSS_48h_Female_Maghar_Intro_01;
		m_deathLine = VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01);
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
			case "ULD_229":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01);
				break;
			case "EX1_407":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01);
				break;
			case "ULD_280":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01);
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
		if (!(cardId == "EX1_407"))
		{
			if (cardId == "CS2_124")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01);
		}
	}
}
