using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class BRM06_Majordomo : BRM_MissionEntity
{
	// Token: 0x0600342B RID: 13355 RVA: 0x0010B604 File Offset: 0x00109804
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA06_1_RESPONSE_03.prefab:a908e5d8056a26b4dbdc0ea833f19a6e");
		base.PreloadSound("VO_BRMA06_3_RESPONSE_03.prefab:3abe0ccef6f202a45b4727361bc704df");
		base.PreloadSound("VO_BRMA06_1_DEATH_04.prefab:78c2973f7c025a641bb953654e358879");
		base.PreloadSound("VO_BRMA06_1_TURN1_02_ALT.prefab:e0ae95e6abc774f4b9bc68f07f7bbc29");
		base.PreloadSound("VO_BRMA06_1_SUMMON_RAG_05.prefab:e79eafab2edcfe2428e817359ec11c65");
		base.PreloadSound("VO_BRMA06_3_INTRO_01.prefab:ccee32264258cd14f9875a94ff81d0ea");
		base.PreloadSound("VO_BRMA06_3_TURN1_02.prefab:7d7272a7a2a62bf4f91488020ed8ab94");
		base.PreloadSound("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492");
	}

	// Token: 0x0600342C RID: 13356 RVA: 0x0010B66C File Offset: 0x0010986C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType - EmoteType.GREETINGS <= 5)
		{
			Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
			if (hero.GetCardId() == "BRMA06_1" || hero.GetCardId() == "BRMA06_1H")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA06_1_RESPONSE_03.prefab:a908e5d8056a26b4dbdc0ea833f19a6e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (hero.GetCardId() == "BRMA06_3" || hero.GetCardId() == "BRMA06_3H")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA06_3_RESPONSE_03.prefab:3abe0ccef6f202a45b4727361bc704df", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
	}

	// Token: 0x0600342D RID: 13357 RVA: 0x0010B742 File Offset: 0x00109942
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 1)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef("BRMA06_3");
			Gameplay.Get().UpdateEnemySideNameBannerName(entityDef.GetName());
			NotificationManager.Get().CreateCharacterQuote("Majordomo_Quote.prefab:72286f87e5b724c21aa1d92d04426614", new Vector3(157.6f, NotificationManager.DEPTH, 84.5f), GameStrings.Get("VO_BRMA06_1_SUMMON_RAG_05"), "", true, 30f, null, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA06_1_SUMMON_RAG_05.prefab:e79eafab2edcfe2428e817359ec11c65", 1f, true, false));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
			actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA06_3_TURN1_02.prefab:7d7272a7a2a62bf4f91488020ed8ab94", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600342E RID: 13358 RVA: 0x0010B758 File Offset: 0x00109958
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA06_1_TURN1_02_ALT.prefab:e0ae95e6abc774f4b9bc68f07f7bbc29", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600342F RID: 13359 RVA: 0x0010B76E File Offset: 0x0010996E
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_MAJORDOMO_DEAD_42"), "VO_NEFARIAN_MAJORDOMO_DEAD_42.prefab:ae35a7c021ec28c43944d401eb251dd5", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x06003430 RID: 13360 RVA: 0x0010B77D File Offset: 0x0010997D
	public override IEnumerator PlayMissionIntroLineAndWait()
	{
		if (NotificationManager.Get().HasSoundPlayedThisSession("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492"))
		{
			yield break;
		}
		NotificationManager.Get().ForceAddSoundToPlayedList("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492");
		NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", new Vector3(97.7f, NotificationManager.DEPTH, 27.8f), GameStrings.Get("VO_NEFARIAN_MAJORDOMO_41"), "", false, 30f, null, CanvasAnchor.BOTTOM_LEFT, false);
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_NEFARIAN_MAJORDOMO_41.prefab:6ac77ff3b0e8b55419b8b72559f1d492", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
		NotificationManager.Get().DestroyActiveQuote(0f, false);
		yield break;
	}
}
