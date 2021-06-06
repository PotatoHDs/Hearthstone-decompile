using System.Collections;

public class BRM17_ZombieNef : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	private bool m_inOnyxiaState;

	private Actor m_nefActor;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA17_1_DEATHWING_88.prefab:525f210af61d16b49b3b20fba2c2cd8c");
		PreloadSound("VO_BRMA17_1_HERO_POWER_87.prefab:e0f77b0064ea8164e92e8982694d89a7");
		PreloadSound("VO_BRMA17_1_CARD_86.prefab:d433af8d96634ae42877ecfd242f93bb");
		PreloadSound("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8");
		PreloadSound("VO_BRMA17_1_TURN1_79.prefab:d9c859c6074049d479898c0582940383");
		PreloadSound("VO_BRMA17_1_RESURRECT1_82.prefab:67b1bbccbff5d2249a0f00300daef60a");
		PreloadSound("VO_BRMA17_1_RESURRECT3_84.prefab:fc2708abf54774d43872254af96d4a6c");
		PreloadSound("VO_BRMA17_1_NEF_AIR1_89.prefab:51e99dbc580c406499d55cf131b94d1e");
		PreloadSound("VO_BRMA17_1_NEF_AIR2_90.prefab:fb528aa3456f4164a94f9ad0939bb055");
		PreloadSound("VO_BRMA17_1_NEF_AIR3_91.prefab:6f790b300a69c3247b83a3e60042ec52");
		PreloadSound("VO_BRMA17_1_NEF_AIR4_92.prefab:b2e088056ab3de043a5481de32fd5e8f");
		PreloadSound("VO_BRMA17_1_NEF_AIR5_93.prefab:315cfc6364a60c246a3bec36b3fda2ba");
		PreloadSound("VO_BRMA17_1_NEF_AIR6_94.prefab:218b8f33f696b194296f1a8c808e5659");
		PreloadSound("VO_BRMA17_1_NEF_AIR7_95.prefab:91e8dbfaaf49fd04e93af907bbb61fd4");
		PreloadSound("VO_BRMA17_1_NEF_AIR8_96.prefab:25016d16acfda5e458cf4b18470528a0");
		PreloadSound("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580");
		PreloadSound("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b");
		PreloadSound("OnyxiaBoss_Start_1.prefab:572ad57bf5b75434b8243fe0c9b5b262");
		PreloadSound("OnyxiaBoss_Death_1.prefab:3b229c4926824824598302037ef1483a");
		PreloadSound("OnyxiaBoss_EmoteResponse_1.prefab:69d9315cbeeddd34b889fe59faa4c480");
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if ((uint)(emoteType - 1) <= 5u)
		{
			switch (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId())
			{
			case "BRMA17_2":
			case "BRMA17_2H":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "BRMA17_3":
			case "BRMA17_3H":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("OnyxiaBoss_EmoteResponse_1.prefab:69d9315cbeeddd34b889fe59faa4c480", Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (entity.GetCardId())
		{
		case "BRMA17_4":
			if (!m_cardLinePlayed && !m_inOnyxiaState)
			{
				m_cardLinePlayed = true;
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_CARD_86.prefab:d433af8d96634ae42877ecfd242f93bb", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "BRMA17_5":
		case "BRMA17_5H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_HERO_POWER_87.prefab:e0f77b0064ea8164e92e8982694d89a7", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			m_nefActor = actor;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_TURN1_79.prefab:d9c859c6074049d479898c0582940383", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		default:
			yield break;
		case 1:
			m_inOnyxiaState = true;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_RESURRECT1_82.prefab:67b1bbccbff5d2249a0f00300daef60a", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_RESURRECT3_84.prefab:fc2708abf54774d43872254af96d4a6c", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			enemyActor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("OnyxiaBoss_Start_1.prefab:572ad57bf5b75434b8243fe0c9b5b262", Notification.SpeechBubbleDirection.TopRight, enemyActor2));
			GameState.Get().SetBusy(busy: false);
			yield break;
		case 3:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_DEATHWING_88.prefab:525f210af61d16b49b3b20fba2c2cd8c", Notification.SpeechBubbleDirection.TopRight, enemyActor2));
			yield break;
		case 4:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR1_89.prefab:51e99dbc580c406499d55cf131b94d1e", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 5:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR2_90.prefab:fb528aa3456f4164a94f9ad0939bb055", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 6:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR3_91.prefab:6f790b300a69c3247b83a3e60042ec52", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 7:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR4_92.prefab:b2e088056ab3de043a5481de32fd5e8f", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 8:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR5_93.prefab:315cfc6364a60c246a3bec36b3fda2ba", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 9:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR6_94.prefab:218b8f33f696b194296f1a8c808e5659", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 10:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR7_95.prefab:91e8dbfaaf49fd04e93af907bbb61fd4", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 11:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR8_96.prefab:25016d16acfda5e458cf4b18470528a0", Notification.SpeechBubbleDirection.TopRight, m_nefActor));
			yield break;
		case 13:
			m_inOnyxiaState = false;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580", Notification.SpeechBubbleDirection.TopRight, enemyActor2));
			GameState.Get().SetBusy(busy: false);
			yield break;
		case 14:
			break;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b", Notification.SpeechBubbleDirection.TopRight, enemyActor2));
	}
}
