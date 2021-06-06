using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_05h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01.prefab:e933cf5fc48346d43b8b7173ee8330e4");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02.prefab:5e263a60c6b8b9e4cb9af33a0d785fef");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Death_01.prefab:17ec26e483d12d54e819ea3a462f7df8");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01.prefab:4bcb35aad3ffc744bbdf32332ae01018");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01.prefab:93730ba56c731f741a95a957acf59553");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01.prefab:054838c9417dfc046878fc064a8a7157");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02.prefab:32d12a2d9070dd84d9372fd11c9bab7f");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03.prefab:0510798abcee96845a0ba543afbb8005");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04.prefab:89b9a7236aa55cf4ea9b14401beb29d5");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_01.prefab:9fa4a74ff20dda247a9ad5b1392be813");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_02.prefab:24d6483a3608bd04e8a647638af21b63");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_03.prefab:4576b87a20ace514cb5c83250093e3f9");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Intro_01.prefab:fe467453afc2d7342bcb1ce9df93f975");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01.prefab:ce8f52baa057be24f834c9911002f2a6");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01.prefab:2940e5f61feaf4540846764efa5fc0fa");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01.prefab:237152cff25430f4898e8b6972aa2dd1");

	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03.prefab:6bc46825284fdb9459e135e616510045");

	private List<string> m_PlayerBigSpells = new List<string> { VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01, VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03 };

	private List<string> m_BossBigSpells = new List<string> { VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01, VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02 };

	private List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_05h_Male_Gnome_Idle_01, VO_DALA_BOSS_05h_Male_Gnome_Idle_02, VO_DALA_BOSS_05h_Male_Gnome_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01, VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02, VO_DALA_BOSS_05h_Male_Gnome_Death_01, VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01, VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04, VO_DALA_BOSS_05h_Male_Gnome_Idle_01,
			VO_DALA_BOSS_05h_Male_Gnome_Idle_02, VO_DALA_BOSS_05h_Male_Gnome_Idle_03, VO_DALA_BOSS_05h_Male_Gnome_Intro_01, VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01, VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01, VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01, VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03, VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_05h_Male_Gnome_Intro_01;
		m_deathLine = VO_DALA_BOSS_05h_Male_Gnome_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerBigSpells);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossBigSpells);
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
