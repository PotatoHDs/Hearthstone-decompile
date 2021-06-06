using System.Collections;
using UnityEngine;

public class BRM06_Majordomo : BRM_MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA06_1_RESPONSE_03.prefab:a908e5d8056a26b4dbdc0ea833f19a6e");
		PreloadSound("VO_BRMA06_3_RESPONSE_03.prefab:3abe0ccef6f202a45b4727361bc704df");
		PreloadSound("VO_BRMA06_1_DEATH_04.prefab:78c2973f7c025a641bb953654e358879");
		PreloadSound("VO_BRMA06_1_TURN1_02_ALT.prefab:e0ae95e6abc774f4b9bc68f07f7bbc29");
		PreloadSound("VO_BRMA06_1_SUMMON_RAG_05.prefab:e79eafab2edcfe2428e817359ec11c65");
		PreloadSound("VO_BRMA06_3_INTRO_01.prefab:ccee32264258cd14f9875a94ff81d0ea");
		PreloadSound("VO_BRMA06_3_TURN1_02.prefab:7d7272a7a2a62bf4f91488020ed8ab94");
		PreloadSound("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492");
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if ((uint)(emoteType - 1) <= 5u)
		{
			Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
			if (hero.GetCardId() == "BRMA06_1" || hero.GetCardId() == "BRMA06_1H")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA06_1_RESPONSE_03.prefab:a908e5d8056a26b4dbdc0ea833f19a6e", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (hero.GetCardId() == "BRMA06_3" || hero.GetCardId() == "BRMA06_3H")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA06_3_RESPONSE_03.prefab:3abe0ccef6f202a45b4727361bc704df", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 1)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef("BRMA06_3");
			Gameplay.Get().UpdateEnemySideNameBannerName(entityDef.GetName());
			NotificationManager.Get().CreateCharacterQuote("Majordomo_Quote.prefab:72286f87e5b724c21aa1d92d04426614", new Vector3(157.6f, NotificationManager.DEPTH, 84.5f), GameStrings.Get("VO_BRMA06_1_SUMMON_RAG_05"), "", allowRepeatDuringSession: true, 30f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA06_1_SUMMON_RAG_05.prefab:e79eafab2edcfe2428e817359ec11c65"));
			NotificationManager.Get().DestroyActiveQuote(0f);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA06_3_TURN1_02.prefab:7d7272a7a2a62bf4f91488020ed8ab94", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA06_1_TURN1_02_ALT.prefab:e0ae95e6abc774f4b9bc68f07f7bbc29", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_MAJORDOMO_DEAD_42"), "VO_NEFARIAN_MAJORDOMO_DEAD_42.prefab:ae35a7c021ec28c43944d401eb251dd5");
		}
	}

	public override IEnumerator PlayMissionIntroLineAndWait()
	{
		if (!NotificationManager.Get().HasSoundPlayedThisSession("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492"))
		{
			NotificationManager.Get().ForceAddSoundToPlayedList("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492");
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", new Vector3(97.7f, NotificationManager.DEPTH, 27.8f), GameStrings.Get("VO_NEFARIAN_MAJORDOMO_41"), "", allowRepeatDuringSession: false, 30f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492", "", Notification.SpeechBubbleDirection.None, null));
			NotificationManager.Get().DestroyActiveQuote(0f);
		}
	}
}
