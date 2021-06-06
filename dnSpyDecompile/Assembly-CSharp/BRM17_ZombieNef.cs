using System;
using System.Collections;

// Token: 0x02000386 RID: 902
public class BRM17_ZombieNef : BRM_MissionEntity
{
	// Token: 0x06003473 RID: 13427 RVA: 0x0010C300 File Offset: 0x0010A500
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA17_1_DEATHWING_88.prefab:525f210af61d16b49b3b20fba2c2cd8c");
		base.PreloadSound("VO_BRMA17_1_HERO_POWER_87.prefab:e0f77b0064ea8164e92e8982694d89a7");
		base.PreloadSound("VO_BRMA17_1_CARD_86.prefab:d433af8d96634ae42877ecfd242f93bb");
		base.PreloadSound("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8");
		base.PreloadSound("VO_BRMA17_1_TURN1_79.prefab:d9c859c6074049d479898c0582940383");
		base.PreloadSound("VO_BRMA17_1_RESURRECT1_82.prefab:67b1bbccbff5d2249a0f00300daef60a");
		base.PreloadSound("VO_BRMA17_1_RESURRECT3_84.prefab:fc2708abf54774d43872254af96d4a6c");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR1_89.prefab:51e99dbc580c406499d55cf131b94d1e");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR2_90.prefab:fb528aa3456f4164a94f9ad0939bb055");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR3_91.prefab:6f790b300a69c3247b83a3e60042ec52");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR4_92.prefab:b2e088056ab3de043a5481de32fd5e8f");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR5_93.prefab:315cfc6364a60c246a3bec36b3fda2ba");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR6_94.prefab:218b8f33f696b194296f1a8c808e5659");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR7_95.prefab:91e8dbfaaf49fd04e93af907bbb61fd4");
		base.PreloadSound("VO_BRMA17_1_NEF_AIR8_96.prefab:25016d16acfda5e458cf4b18470528a0");
		base.PreloadSound("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580");
		base.PreloadSound("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b");
		base.PreloadSound("OnyxiaBoss_Start_1.prefab:572ad57bf5b75434b8243fe0c9b5b262");
		base.PreloadSound("OnyxiaBoss_Death_1.prefab:3b229c4926824824598302037ef1483a");
		base.PreloadSound("OnyxiaBoss_EmoteResponse_1.prefab:69d9315cbeeddd34b889fe59faa4c480");
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x0010C3EC File Offset: 0x0010A5EC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType - EmoteType.GREETINGS <= 5)
		{
			string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
			if (cardId == "BRMA17_2" || cardId == "BRMA17_2H")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "BRMA17_3" || cardId == "BRMA17_3H")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("OnyxiaBoss_EmoteResponse_1.prefab:69d9315cbeeddd34b889fe59faa4c480", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
	}

	// Token: 0x06003475 RID: 13429 RVA: 0x0010C4B3 File Offset: 0x0010A6B3
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "BRMA17_4"))
		{
			if (cardId == "BRMA17_5" || cardId == "BRMA17_5H")
			{
				if (this.m_heroPowerLinePlayed)
				{
					yield break;
				}
				this.m_heroPowerLinePlayed = true;
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_HERO_POWER_87.prefab:e0f77b0064ea8164e92e8982694d89a7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_cardLinePlayed)
			{
				yield break;
			}
			if (this.m_inOnyxiaState)
			{
				yield break;
			}
			this.m_cardLinePlayed = true;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_CARD_86.prefab:d433af8d96634ae42877ecfd242f93bb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003476 RID: 13430 RVA: 0x0010C4C9 File Offset: 0x0010A6C9
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			this.m_nefActor = actor;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_TURN1_79.prefab:d9c859c6074049d479898c0582940383", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x0010C4DF File Offset: 0x0010A6DF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 1:
			this.m_inOnyxiaState = true;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_RESURRECT1_82.prefab:67b1bbccbff5d2249a0f00300daef60a", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_RESURRECT3_84.prefab:fc2708abf54774d43872254af96d4a6c", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("OnyxiaBoss_Start_1.prefab:572ad57bf5b75434b8243fe0c9b5b262", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 3:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_DEATHWING_88.prefab:525f210af61d16b49b3b20fba2c2cd8c", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		case 4:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR1_89.prefab:51e99dbc580c406499d55cf131b94d1e", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 5:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR2_90.prefab:fb528aa3456f4164a94f9ad0939bb055", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 6:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR3_91.prefab:6f790b300a69c3247b83a3e60042ec52", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 7:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR4_92.prefab:b2e088056ab3de043a5481de32fd5e8f", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 8:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR5_93.prefab:315cfc6364a60c246a3bec36b3fda2ba", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 9:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR6_94.prefab:218b8f33f696b194296f1a8c808e5659", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 10:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR7_95.prefab:91e8dbfaaf49fd04e93af907bbb61fd4", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 11:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_NEF_AIR8_96.prefab:25016d16acfda5e458cf4b18470528a0", Notification.SpeechBubbleDirection.TopRight, this.m_nefActor, 3f, 1f, true, false, 0f));
			break;
		case 13:
			this.m_inOnyxiaState = false;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 14:
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		}
		yield break;
	}

	// Token: 0x04001CAA RID: 7338
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001CAB RID: 7339
	private bool m_cardLinePlayed;

	// Token: 0x04001CAC RID: 7340
	private bool m_inOnyxiaState;

	// Token: 0x04001CAD RID: 7341
	private Actor m_nefActor;
}
