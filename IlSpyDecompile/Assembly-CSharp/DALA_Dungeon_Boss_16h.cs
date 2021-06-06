using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_16h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01.prefab:e0f023472381f8c4d8734d97f1f465e1");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01.prefab:01988e435a7b0a444b4339ae53b916c5");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01.prefab:8f69bb92b2ece5744b87b0485c2ab6c9");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Death_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Death_02.prefab:e0bca3927c63efb468e86c3e3faec774");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01.prefab:71344262b29cbd24a8a5fe86825341b8");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01.prefab:b9bdda4595d682c49a02d998664388f3");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_01.prefab:44f7e83f440796741b1c26adb962297c");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_02.prefab:c603b57656551d748a94cd5e838b4d91");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_03.prefab:0443ed55dcfad16429dd315074bae5d3");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_04.prefab:799eff561b0cd014a9f6f250dab1d5b6");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01.prefab:3f321ebe40544ef4f8607e252de4df0e");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_01.prefab:c0cb64a69ce3d0741a56661738e59c32");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_03.prefab:dd9d835453ad8e548b004902191958da");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_04.prefab:671c4ec59be451c41890e9756ca5e37f");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_06 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_06.prefab:575e3b92c81024f45a3d24be773d183a");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Intro_01.prefab:5539f00129dec4246a5d309dd53f3472");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01.prefab:889864329637ec34daf0317c9c528c07");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01.prefab:34ff3a32b8b9a1442ac76bd2b2ca77d6");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01.prefab:6491b9d206f7868498df3d4628a70d54");

	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02.prefab:df38533ffaaa22646a2d34854ff6edae");

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_16h_Female_Human_HeroPower_01, VO_DALA_BOSS_16h_Female_Human_HeroPower_02, VO_DALA_BOSS_16h_Female_Human_HeroPower_03, VO_DALA_BOSS_16h_Female_Human_HeroPower_04 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_16h_Female_Human_Idle_01, VO_DALA_BOSS_16h_Female_Human_Idle_03, VO_DALA_BOSS_16h_Female_Human_Idle_04, VO_DALA_BOSS_16h_Female_Human_Idle_06 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01, VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01, VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01, VO_DALA_BOSS_16h_Female_Human_Death_02, VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01, VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01, VO_DALA_BOSS_16h_Female_Human_HeroPower_01, VO_DALA_BOSS_16h_Female_Human_HeroPower_02, VO_DALA_BOSS_16h_Female_Human_HeroPower_03, VO_DALA_BOSS_16h_Female_Human_HeroPower_04,
			VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01, VO_DALA_BOSS_16h_Female_Human_Idle_01, VO_DALA_BOSS_16h_Female_Human_Idle_03, VO_DALA_BOSS_16h_Female_Human_Idle_04, VO_DALA_BOSS_16h_Female_Human_Idle_06, VO_DALA_BOSS_16h_Female_Human_Intro_01, VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01, VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01, VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01, VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_16h_Female_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_16h_Female_Human_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "BOT_576":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01);
				break;
			case "CFM_120":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01);
				break;
			case "CFM_065":
			case "CFM_094":
			case "CFM_603":
			case "CFM_608":
			case "CFM_662":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02);
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
			case "GIL_118":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01);
				break;
			case "TRL_564":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01);
				break;
			case "DAL_544":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01);
				break;
			}
		}
	}
}
