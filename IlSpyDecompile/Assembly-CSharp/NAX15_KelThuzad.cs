using System.Collections;
using UnityEngine;

public class NAX15_KelThuzad : NAX_MissionEntity
{
	private bool m_frostHeroPowerLinePlayed;

	private bool m_bigglesLinePlayed;

	private bool m_hurryLinePlayed;

	private int m_numTimesMindControlPlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304");
		PreloadSound("VO_NAX15_01_PHASE2_10.prefab:ad7357857f8cb904fad0280a4ed2d988");
		PreloadSound("VO_NAX15_01_HP_07.prefab:a56d0bb88cc42014fa9c5d53903faf15");
		PreloadSound("VO_NAX15_01_HP2_05.prefab:ee4e46ad9b4206146ab439ccfad4e59e");
		PreloadSound("VO_NAX15_01_HP3_06.prefab:41d3bd9b7963d5f41a0b3614df6074aa");
		PreloadSound("VO_NAX15_01_PHASE2_ALT_11.prefab:2f066d9f9a49df94cafa065e79d7ebdf");
		PreloadSound("VO_NAX15_01_EMOTE_HELLO_26.prefab:9ed2ae3873b199146819291cfaa396e5");
		PreloadSound("VO_NAX15_01_EMOTE_WP_25.prefab:57f0f617dc85a1441a6fe68fe570347c");
		PreloadSound("VO_NAX15_01_EMOTE_OOPS_29.prefab:0d497df4f2aced741bbba13ac2912d58");
		PreloadSound("VO_NAX15_01_EMOTE_SORRY_28.prefab:c7086f87dd8a03e489d1f19339942794");
		PreloadSound("VO_NAX15_01_EMOTE_THANKS_27.prefab:72955d7b668a26d4581ac52bf0ed03d0");
		PreloadSound("VO_NAX15_01_EMOTE_THREATEN_30.prefab:983b1fb96a8525041945d5b41475599f");
		PreloadSound("VO_KT_HEIGAN2_55.prefab:f465a1b0b2312764f92f4d86160c9dac");
		PreloadSound("VO_NAX15_01_RESPOND_GARROSH_15.prefab:48cc88124901a3447b86a466a761f3a9");
		PreloadSound("VO_NAX15_01_RESPOND_THRALL_17.prefab:ccc75bb0ed1ff104bbd9a85820ff5afe");
		PreloadSound("VO_NAX15_01_RESPOND_VALEERA_18.prefab:c9de6754f5d117a4d8fbdb6c7b7871e9");
		PreloadSound("VO_NAX15_01_RESPOND_UTHER_14.prefab:1079fbad87857364a95f558df2e47102");
		PreloadSound("VO_NAX15_01_RESPOND_REXXAR_19.prefab:5b09a5dd8bedd5d4b854e38878b48e80");
		PreloadSound("VO_NAX15_01_RESPOND_MALFURION_ALT_21.prefab:609de8d4162f0894da2c05b14473b6e7");
		PreloadSound("VO_NAX15_01_RESPOND_GULDAN_22.prefab:b28117d69d646014bb3a8ec39d5cc388");
		PreloadSound("VO_NAX15_01_RESPOND_JAINA_23.prefab:0434b865495ab2f45a36cef7be6b4ebc");
		PreloadSound("VO_NAX15_01_RESPOND_ANDUIN_24.prefab:10a05fc478fe371419a859b464b13b3e");
		PreloadSound("VO_NAX15_01_BIGGLES_32.prefab:1c0b11f45e9af1547ac5db34be687f9e");
		PreloadSound("VO_NAX15_01_HURRY_31.prefab:552de9d45281f0a47a4a9cb9645c98f6");
	}

	public override void OnPlayThinkEmote()
	{
		if (!m_hurryLinePlayed && !m_enemySpeaking)
		{
			Player currentPlayer = GameState.Get().GetCurrentPlayer();
			if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
			{
				m_hurryLinePlayed = true;
				Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
					.GetActor();
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_HURRY_31.prefab:552de9d45281f0a47a4a9cb9645c98f6", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			int KTgloat = Options.Get().GetInt(Option.KELTHUZADTAUNTS);
			yield return new WaitForSeconds(5f);
			switch (KTgloat)
			{
			case 0:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT1_33", "VO_NAX15_01_GLOAT1_33.prefab:6afb33fab639f1f43a7f33f17ef4d7d4");
				break;
			case 1:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT2_34", "VO_NAX15_01_GLOAT2_34.prefab:ee23015fccf6cce44a21420f7ca0c8e6");
				break;
			case 2:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT3_35", "VO_NAX15_01_GLOAT3_35.prefab:c7a207b5224015747a321ac520a02b9c");
				break;
			case 3:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT4_36", "VO_NAX15_01_GLOAT4_36.prefab:8c432d06dd4a9254a9b415621fe22539");
				break;
			case 4:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT5_37", "VO_NAX15_01_GLOAT5_37.prefab:e6821e5c9b4225441912e23add8b17f4");
				break;
			}
			if (KTgloat >= 4)
			{
				Options.Get().SetInt(Option.KELTHUZADTAUNTS, 0);
			}
			else
			{
				Options.Get().SetInt(Option.KELTHUZADTAUNTS, KTgloat + 1);
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 2:
			m_enemySpeaking = true;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_PHASE2_10.prefab:ad7357857f8cb904fad0280a4ed2d988", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 3:
			if (!m_frostHeroPowerLinePlayed)
			{
				m_frostHeroPowerLinePlayed = true;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_HP_07.prefab:a56d0bb88cc42014fa9c5d53903faf15", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			break;
		case 4:
			if (m_numTimesMindControlPlayed == 0)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_HP2_05.prefab:ee4e46ad9b4206146ab439ccfad4e59e", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			else if (m_numTimesMindControlPlayed == 1)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_HP3_06.prefab:41d3bd9b7963d5f41a0b3614df6074aa", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			m_numTimesMindControlPlayed++;
			break;
		case 5:
			if (!m_bigglesLinePlayed)
			{
				m_bigglesLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_BIGGLES_32.prefab:1c0b11f45e9af1547ac5db34be687f9e", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			break;
		}
	}

	public override void HandleRealTimeMissionEvent(int missionEvent)
	{
		if (missionEvent == 1)
		{
			AssetLoader.Get().InstantiatePrefab("KelThuzad_StealTurn.prefab:7630c436593404790a4a948dc219f537", OnStealTurnSpellLoaded);
		}
	}

	private void OnStealTurnSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			if (TurnTimer.Get() != null)
			{
				TurnTimer.Get().OnEndTurnRequested();
			}
			EndTurnButton.Get().OnEndTurnRequested();
			return;
		}
		go.transform.position = EndTurnButton.Get().transform.position;
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			if (TurnTimer.Get() != null)
			{
				TurnTimer.Get().OnEndTurnRequested();
			}
			EndTurnButton.Get().OnEndTurnRequested();
		}
		else
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			component.ActivateState(SpellStateType.ACTION);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_PHASE2_ALT_11.prefab:2f066d9f9a49df94cafa065e79d7ebdf", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (emoteType)
		{
		case EmoteType.GREETINGS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_HELLO_26.prefab:9ed2ae3873b199146819291cfaa396e5", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.WELL_PLAYED:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_WP_25.prefab:57f0f617dc85a1441a6fe68fe570347c", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.OOPS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_OOPS_29.prefab:0d497df4f2aced741bbba13ac2912d58", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.SORRY:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_SORRY_28.prefab:c7086f87dd8a03e489d1f19339942794", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.THANKS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_THANKS_27.prefab:72955d7b668a26d4581ac52bf0ed03d0", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.THREATEN:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_THREATEN_30.prefab:983b1fb96a8525041945d5b41475599f", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.WOW:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_KT_HEIGAN2_55.prefab:f465a1b0b2312764f92f4d86160c9dac", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.START:
			switch (GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId())
			{
			case "HERO_01":
			case "HERO_01b":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_GARROSH_15.prefab:48cc88124901a3447b86a466a761f3a9", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_02":
			case "HERO_02d":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_THRALL_17.prefab:ccc75bb0ed1ff104bbd9a85820ff5afe", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_03":
			case "HERO_03b":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_VALEERA_18.prefab:c9de6754f5d117a4d8fbdb6c7b7871e9", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_04":
			case "HERO_04d":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_UTHER_14.prefab:1079fbad87857364a95f558df2e47102", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_05":
			case "HERO_05b":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_REXXAR_19.prefab:5b09a5dd8bedd5d4b854e38878b48e80", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_06":
			case "HERO_06c":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_MALFURION_ALT_21.prefab:609de8d4162f0894da2c05b14473b6e7", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_07":
			case "HERO_07c":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_GULDAN_22.prefab:b28117d69d646014bb3a8ec39d5cc388", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_08":
			case "HERO_08c":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_JAINA_23.prefab:0434b865495ab2f45a36cef7be6b4ebc", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_09":
			case "HERO_09c":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_ANDUIN_24.prefab:10a05fc478fe371419a859b464b13b3e", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
			break;
		}
	}
}
