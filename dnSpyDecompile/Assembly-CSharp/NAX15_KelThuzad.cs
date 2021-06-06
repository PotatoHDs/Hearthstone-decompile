using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000591 RID: 1425
public class NAX15_KelThuzad : NAX_MissionEntity
{
	// Token: 0x06004F28 RID: 20264 RVA: 0x0019FC58 File Offset: 0x0019DE58
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304");
		base.PreloadSound("VO_NAX15_01_PHASE2_10.prefab:ad7357857f8cb904fad0280a4ed2d988");
		base.PreloadSound("VO_NAX15_01_HP_07.prefab:a56d0bb88cc42014fa9c5d53903faf15");
		base.PreloadSound("VO_NAX15_01_HP2_05.prefab:ee4e46ad9b4206146ab439ccfad4e59e");
		base.PreloadSound("VO_NAX15_01_HP3_06.prefab:41d3bd9b7963d5f41a0b3614df6074aa");
		base.PreloadSound("VO_NAX15_01_PHASE2_ALT_11.prefab:2f066d9f9a49df94cafa065e79d7ebdf");
		base.PreloadSound("VO_NAX15_01_EMOTE_HELLO_26.prefab:9ed2ae3873b199146819291cfaa396e5");
		base.PreloadSound("VO_NAX15_01_EMOTE_WP_25.prefab:57f0f617dc85a1441a6fe68fe570347c");
		base.PreloadSound("VO_NAX15_01_EMOTE_OOPS_29.prefab:0d497df4f2aced741bbba13ac2912d58");
		base.PreloadSound("VO_NAX15_01_EMOTE_SORRY_28.prefab:c7086f87dd8a03e489d1f19339942794");
		base.PreloadSound("VO_NAX15_01_EMOTE_THANKS_27.prefab:72955d7b668a26d4581ac52bf0ed03d0");
		base.PreloadSound("VO_NAX15_01_EMOTE_THREATEN_30.prefab:983b1fb96a8525041945d5b41475599f");
		base.PreloadSound("VO_KT_HEIGAN2_55.prefab:f465a1b0b2312764f92f4d86160c9dac");
		base.PreloadSound("VO_NAX15_01_RESPOND_GARROSH_15.prefab:48cc88124901a3447b86a466a761f3a9");
		base.PreloadSound("VO_NAX15_01_RESPOND_THRALL_17.prefab:ccc75bb0ed1ff104bbd9a85820ff5afe");
		base.PreloadSound("VO_NAX15_01_RESPOND_VALEERA_18.prefab:c9de6754f5d117a4d8fbdb6c7b7871e9");
		base.PreloadSound("VO_NAX15_01_RESPOND_UTHER_14.prefab:1079fbad87857364a95f558df2e47102");
		base.PreloadSound("VO_NAX15_01_RESPOND_REXXAR_19.prefab:5b09a5dd8bedd5d4b854e38878b48e80");
		base.PreloadSound("VO_NAX15_01_RESPOND_MALFURION_ALT_21.prefab:609de8d4162f0894da2c05b14473b6e7");
		base.PreloadSound("VO_NAX15_01_RESPOND_GULDAN_22.prefab:b28117d69d646014bb3a8ec39d5cc388");
		base.PreloadSound("VO_NAX15_01_RESPOND_JAINA_23.prefab:0434b865495ab2f45a36cef7be6b4ebc");
		base.PreloadSound("VO_NAX15_01_RESPOND_ANDUIN_24.prefab:10a05fc478fe371419a859b464b13b3e");
		base.PreloadSound("VO_NAX15_01_BIGGLES_32.prefab:1c0b11f45e9af1547ac5db34be687f9e");
		base.PreloadSound("VO_NAX15_01_HURRY_31.prefab:552de9d45281f0a47a4a9cb9645c98f6");
	}

	// Token: 0x06004F29 RID: 20265 RVA: 0x0019FD70 File Offset: 0x0019DF70
	public override void OnPlayThinkEmote()
	{
		if (this.m_hurryLinePlayed)
		{
			return;
		}
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		this.m_hurryLinePlayed = true;
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_HURRY_31.prefab:552de9d45281f0a47a4a9cb9645c98f6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004F2A RID: 20266 RVA: 0x0019FDF6 File Offset: 0x0019DFF6
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			int KTgloat = Options.Get().GetInt(Option.KELTHUZADTAUNTS);
			yield return new WaitForSeconds(5f);
			switch (KTgloat)
			{
			case 0:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT1_33", "VO_NAX15_01_GLOAT1_33.prefab:6afb33fab639f1f43a7f33f17ef4d7d4", true);
				break;
			case 1:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT2_34", "VO_NAX15_01_GLOAT2_34.prefab:ee23015fccf6cce44a21420f7ca0c8e6", true);
				break;
			case 2:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT3_35", "VO_NAX15_01_GLOAT3_35.prefab:c7a207b5224015747a321ac520a02b9c", true);
				break;
			case 3:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT4_36", "VO_NAX15_01_GLOAT4_36.prefab:8c432d06dd4a9254a9b415621fe22539", true);
				break;
			case 4:
				NotificationManager.Get().CreateKTQuote("VO_NAX15_01_GLOAT5_37", "VO_NAX15_01_GLOAT5_37.prefab:e6821e5c9b4225441912e23add8b17f4", true);
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
		yield break;
	}

	// Token: 0x06004F2B RID: 20267 RVA: 0x0019FE05 File Offset: 0x0019E005
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		case 2:
			this.m_enemySpeaking = true;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_PHASE2_10.prefab:ad7357857f8cb904fad0280a4ed2d988", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_SUMMON_ADDS_12.prefab:94bc769aa09d4234fb3ec6e6012b2304", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		case 3:
			if (!this.m_frostHeroPowerLinePlayed)
			{
				this.m_frostHeroPowerLinePlayed = true;
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_HP_07.prefab:a56d0bb88cc42014fa9c5d53903faf15", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			break;
		case 4:
			if (this.m_numTimesMindControlPlayed == 0)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_HP2_05.prefab:ee4e46ad9b4206146ab439ccfad4e59e", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			else if (this.m_numTimesMindControlPlayed == 1)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_HP3_06.prefab:41d3bd9b7963d5f41a0b3614df6074aa", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			this.m_numTimesMindControlPlayed++;
			break;
		case 5:
			if (!this.m_bigglesLinePlayed)
			{
				this.m_bigglesLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_BIGGLES_32.prefab:1c0b11f45e9af1547ac5db34be687f9e", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004F2C RID: 20268 RVA: 0x0019FE1B File Offset: 0x0019E01B
	public override void HandleRealTimeMissionEvent(int missionEvent)
	{
		if (missionEvent == 1)
		{
			AssetLoader.Get().InstantiatePrefab("KelThuzad_StealTurn.prefab:7630c436593404790a4a948dc219f537", new PrefabCallback<GameObject>(this.OnStealTurnSpellLoaded), null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06004F2D RID: 20269 RVA: 0x0019FE44 File Offset: 0x0019E044
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
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		component.ActivateState(SpellStateType.ACTION);
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_PHASE2_ALT_11.prefab:2f066d9f9a49df94cafa065e79d7ebdf", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004F2E RID: 20270 RVA: 0x0019FF10 File Offset: 0x0019E110
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (emoteType)
		{
		case EmoteType.GREETINGS:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_HELLO_26.prefab:9ed2ae3873b199146819291cfaa396e5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.WELL_PLAYED:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_WP_25.prefab:57f0f617dc85a1441a6fe68fe570347c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.OOPS:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_OOPS_29.prefab:0d497df4f2aced741bbba13ac2912d58", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.THREATEN:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_THREATEN_30.prefab:983b1fb96a8525041945d5b41475599f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.THANKS:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_THANKS_27.prefab:72955d7b668a26d4581ac52bf0ed03d0", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.SORRY:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_EMOTE_SORRY_28.prefab:c7086f87dd8a03e489d1f19339942794", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		case EmoteType.CONCEDE:
			break;
		case EmoteType.START:
		{
			string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
			if (num <= 2128193475U)
			{
				if (num > 1772983703U)
				{
					if (num <= 1973770678U)
					{
						if (num != 1889588393U)
						{
							if (num != 1973770678U)
							{
								return;
							}
							if (!(cardId == "HERO_04d"))
							{
								return;
							}
							goto IL_432;
						}
						else if (!(cardId == "HERO_06c"))
						{
							return;
						}
					}
					else if (num != 2041719797U)
					{
						if (num != 2111415856U)
						{
							if (num != 2128193475U)
							{
								return;
							}
							if (!(cardId == "HERO_07"))
							{
								return;
							}
							goto IL_4B0;
						}
						else if (!(cardId == "HERO_06"))
						{
							return;
						}
					}
					else
					{
						if (!(cardId == "HERO_01b"))
						{
							return;
						}
						goto IL_3B4;
					}
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_MALFURION_ALT_21.prefab:609de8d4162f0894da2c05b14473b6e7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				}
				if (num <= 1638365393U)
				{
					if (num != 1352851680U)
					{
						if (num != 1638365393U)
						{
							return;
						}
						if (!(cardId == "HERO_05b"))
						{
							return;
						}
						goto IL_45C;
					}
					else if (!(cardId == "HERO_07c"))
					{
						return;
					}
				}
				else if (num != 1757780202U)
				{
					if (num != 1772983703U)
					{
						return;
					}
					if (!(cardId == "HERO_03b"))
					{
						return;
					}
					goto IL_408;
				}
				else
				{
					if (!(cardId == "HERO_09c"))
					{
						return;
					}
					goto IL_504;
				}
				IL_4B0:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_GULDAN_22.prefab:b28117d69d646014bb3a8ec39d5cc388", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (num <= 2175396296U)
			{
				if (num <= 2160295963U)
				{
					if (num != 2144971094U)
					{
						if (num != 2160295963U)
						{
							return;
						}
						if (!(cardId == "HERO_08c"))
						{
							return;
						}
						goto IL_4DA;
					}
					else
					{
						if (!(cardId == "HERO_04"))
						{
							return;
						}
						goto IL_432;
					}
				}
				else if (num != 2161748713U)
				{
					if (num != 2175396296U)
					{
						return;
					}
					if (!(cardId == "HERO_02d"))
					{
						return;
					}
				}
				else
				{
					if (!(cardId == "HERO_05"))
					{
						return;
					}
					goto IL_45C;
				}
			}
			else if (num <= 2195303951U)
			{
				if (num != 2178526332U)
				{
					if (num != 2195303951U)
					{
						return;
					}
					if (!(cardId == "HERO_03"))
					{
						return;
					}
					goto IL_408;
				}
				else if (!(cardId == "HERO_02"))
				{
					return;
				}
			}
			else if (num != 2228859189U)
			{
				if (num != 2346302522U)
				{
					if (num != 2363080141U)
					{
						return;
					}
					if (!(cardId == "HERO_09"))
					{
						return;
					}
					goto IL_504;
				}
				else
				{
					if (!(cardId == "HERO_08"))
					{
						return;
					}
					goto IL_4DA;
				}
			}
			else
			{
				if (!(cardId == "HERO_01"))
				{
					return;
				}
				goto IL_3B4;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_THRALL_17.prefab:ccc75bb0ed1ff104bbd9a85820ff5afe", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_4DA:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_JAINA_23.prefab:0434b865495ab2f45a36cef7be6b4ebc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_3B4:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_GARROSH_15.prefab:48cc88124901a3447b86a466a761f3a9", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_408:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_VALEERA_18.prefab:c9de6754f5d117a4d8fbdb6c7b7871e9", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_432:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_UTHER_14.prefab:1079fbad87857364a95f558df2e47102", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_45C:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_REXXAR_19.prefab:5b09a5dd8bedd5d4b854e38878b48e80", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
			IL_504:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX15_01_RESPOND_ANDUIN_24.prefab:10a05fc478fe371419a859b464b13b3e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			break;
		}
		default:
			if (emoteType != EmoteType.WOW)
			{
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_KT_HEIGAN2_55.prefab:f465a1b0b2312764f92f4d86160c9dac", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0400454E RID: 17742
	private bool m_frostHeroPowerLinePlayed;

	// Token: 0x0400454F RID: 17743
	private bool m_bigglesLinePlayed;

	// Token: 0x04004550 RID: 17744
	private bool m_hurryLinePlayed;

	// Token: 0x04004551 RID: 17745
	private int m_numTimesMindControlPlayed;
}
