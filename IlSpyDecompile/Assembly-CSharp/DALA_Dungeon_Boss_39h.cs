using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_39h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Death_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Death_01.prefab:e6054b803c717e24c9208f51763cff93");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01.prefab:5a1bed9db02a44640b9fb292539ee3b6");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01.prefab:5e821931754f8e14d90accf92242ef2c");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01.prefab:afc618ecc329f624bac4643e1e50b00c");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02.prefab:71b540dbd96248649bdba9536cb7981e");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03.prefab:60b7d0c6d14c3784d8499ae21846a66b");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04.prefab:cbf2c89b47d1e654ba9b189351f6a4fa");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05.prefab:c5c668583ca30a6418a3a94c44242408");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06.prefab:b80aeb9cf33e46149a37d4d21d73a74f");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_01.prefab:4957cfb564c733947bcf4e45022a34a2");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_02.prefab:365686a1ce5a60642b536ff814f70e69");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_03.prefab:0fd376c26c189df478cedbc3fb1f2881");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Intro_01.prefab:a3659800ac23e2c428b2e1b44dd4c4b8");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01.prefab:9e49459701d5a284eb0da6665fceb7dd");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01.prefab:b0e5fac69736c3945986027ce6272a33");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01.prefab:016abe45f62b7014ebc93068c91292a5");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01.prefab:496837dce3c5e5d43b1110afcb3951d4");

	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01.prefab:47c09de8023ee3e4b8dc91a461afce57");

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_39h_Male_Draenei_Idle_01, VO_DALA_BOSS_39h_Male_Draenei_Idle_02, VO_DALA_BOSS_39h_Male_Draenei_Idle_03 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_39h_Male_Draenei_Death_01, VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01, VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05, VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06, VO_DALA_BOSS_39h_Male_Draenei_Idle_01,
			VO_DALA_BOSS_39h_Male_Draenei_Idle_02, VO_DALA_BOSS_39h_Male_Draenei_Idle_03, VO_DALA_BOSS_39h_Male_Draenei_Intro_01, VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01, VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01, VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01, VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01, VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_39h_Male_Draenei_Intro_01;
		m_deathLine = VO_DALA_BOSS_39h_Male_Draenei_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "ICC_828":
			case "ICC_831":
			case "ICC_833":
			case "ICC_832":
			case "ICC_830":
			case "ICC_481":
			case "ICC_834":
			case "ICC_827":
			case "ICC_829":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01);
				break;
			case "CS1_112":
			case "CS1_130":
			case "CS2_089":
			case "CS2_093":
			case "EX1_365":
			case "EX1_624":
			case "GIL_134":
			case "AT_011":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
