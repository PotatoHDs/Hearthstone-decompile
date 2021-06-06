using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class BRM02_DarkIronArena : BRM_MissionEntity
{
	// Token: 0x06003412 RID: 13330 RVA: 0x0010B0DC File Offset: 0x001092DC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA02_1_RESPONSE_04.prefab:f5c965be447046543ad83c07cf2bcd0c");
		base.PreloadSound("VO_BRMA02_1_HERO_POWER_05.prefab:c00acb0a43035e24aa86b12856b7af44");
		base.PreloadSound("VO_BRMA02_1_TURN1_02.prefab:ef65f9070bf2aa6489a58c705d21b1d9");
		base.PreloadSound("VO_BRMA02_1_TURN1_PT2_03.prefab:b500c91480d36e045aece029d3646acc");
		base.PreloadSound("VO_BRMA02_1_ALAKIR_34.prefab:da389ff1794394e46bae7929a2db7452");
		base.PreloadSound("VO_BRMA02_1_ALEXSTRAZA_32.prefab:912653e742620bb4185dc691ba8380d7");
		base.PreloadSound("VO_BRMA02_1_BEAST_22.prefab:15d2fbd54c2845741af4946fd489ee0a");
		base.PreloadSound("VO_BRMA02_1_BOOM_28.prefab:b6075c33b877a14409eafb2f91ca9a6e");
		base.PreloadSound("VO_BRMA02_1_CAIRNE_20.prefab:b43fe34d1ec6a424abe41b02dc0e4196");
		base.PreloadSound("VO_BRMA02_1_CHO_07.prefab:ac3d8b3ee38366140b30299bee846f39");
		base.PreloadSound("VO_BRMA02_1_DEATHWING_35.prefab:c5f1030779827c643bfb810f919819fa");
		base.PreloadSound("VO_BRMA02_1_ETC_18.prefab:1533c18bd7e1b204ebed7b6fac30fa9a");
		base.PreloadSound("VO_BRMA02_1_FEUGEN_15.prefab:04b5933629beae54da3357341132b94a");
		base.PreloadSound("VO_BRMA02_1_FOEREAPER_29.prefab:356b9c85ddd8ad340a71d4c9c7c9d58c");
		base.PreloadSound("VO_BRMA02_1_GEDDON_13.prefab:c4a73265eab005b47bb7d51c74d7b41d");
		base.PreloadSound("VO_BRMA02_1_GELBIN_21.prefab:c9710a6ef19cdba46a7c51404bd73851");
		base.PreloadSound("VO_BRMA02_1_GRUUL_31.prefab:4fdd03655b03494488e8453de69a3c34");
		base.PreloadSound("VO_BRMA02_1_HOGGER_27.prefab:ec226470424912547a6971e727116826");
		base.PreloadSound("VO_BRMA02_1_LEVIATHAN_12.prefab:998e9da05f77b3945ac21f8a7c245738");
		base.PreloadSound("VO_BRMA02_1_LOATHEB_16.prefab:cb44878eabffc304db3080dd9571018b");
		base.PreloadSound("VO_BRMA02_1_MAEXXNA_24.prefab:4d37c3fd5de32014bb70b071c47e6491");
		base.PreloadSound("VO_BRMA02_1_MILLHOUSE_09.prefab:5d1b5f555fe70e344afce98d443d9730");
		base.PreloadSound("VO_BRMA02_1_MOGOR_25.prefab:13e09894e4d3edc4a95ccf97a4865fa3");
		base.PreloadSound("VO_BRMA02_1_MUKLA_10.prefab:1bcf6267b4e82b841a48d2a094f99618");
		base.PreloadSound("VO_BRMA02_1_NOZDORMU_36.prefab:890229ac317e19d45bd1fe7fb67af713");
		base.PreloadSound("VO_BRMA02_1_ONYXIA_33.prefab:a963754f9a1ffbe489bddca145214f19");
		base.PreloadSound("VO_BRMA02_1_PAGLE_08.prefab:4681ed30e0c08544c8f67ee5ee64e05c");
		base.PreloadSound("VO_BRMA02_1_SNEED_30.prefab:4e86ebca4343b2e49841d0afa38719cf");
		base.PreloadSound("VO_BRMA02_1_STALAGG_14.prefab:f86dfd95c5c043f40becb7def0ec2b5b");
		base.PreloadSound("VO_BRMA02_1_SYLVANAS_19.prefab:c81b1205ba2c977489aaa357f60879ba");
		base.PreloadSound("VO_BRMA02_1_THALNOS_06.prefab:dfdd1db2a3b8e31439ffb9609a5a686d");
		base.PreloadSound("VO_BRMA02_1_THAURISSAN_37.prefab:dc3a267f6d3fafd4196cbd9dd7c145e2");
		base.PreloadSound("VO_BRMA02_1_TINKMASTER_11.prefab:398799c5e8b1ef649af23857a270ac05");
		base.PreloadSound("VO_BRMA02_1_TOSHLEY_26.prefab:07c3b6a0fd3b6174dadba657f6248d8f");
		base.PreloadSound("VO_BRMA02_1_VOLJIN_17.prefab:319230fe533b551469286ecb9fe97dde");
		base.PreloadSound("VO_NEFARIAN_GRIMSTONE_DEAD1_30.prefab:cb4444f1726517043ad2997a282b7863");
		base.PreloadSound("VO_RAGNAROS_GRIMSTONE_DEAD2_66.prefab:5494ef65549f1b449b6132b3ef620f0f");
	}

	// Token: 0x06003413 RID: 13331 RVA: 0x0010B280 File Offset: 0x00109480
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BRMA02_1_RESPONSE_04.prefab:f5c965be447046543ad83c07cf2bcd0c",
						m_stringTag = "VO_BRMA02_1_RESPONSE_04"
					}
				}
			}
		};
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x0010B2DF File Offset: 0x001094DF
	protected override IEnumerator RespondToWillPlayCardWithTiming(string cardId)
	{
		if (this.m_linesPlayed.Contains(cardId))
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "BRMA02_2" || cardId == "BRMA02_2H")
		{
			if (this.m_enemySpeaking)
			{
				yield break;
			}
			GameState.Get().SetBusy(true);
			this.m_linesPlayed.Add(cardId);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_HERO_POWER_05.prefab:c00acb0a43035e24aa86b12856b7af44", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
			GameState.Get().SetBusy(false);
		}
		else
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
			if (num <= 1457382949U)
			{
				if (num <= 997149794U)
				{
					if (num <= 446764437U)
					{
						if (num != 2213014U)
						{
							if (num != 103025823U)
							{
								if (num == 446764437U)
								{
									if (cardId == "EX1_083")
									{
										this.m_linesPlayed.Add(cardId);
										yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_TINKMASTER_11.prefab:398799c5e8b1ef649af23857a270ac05", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
									}
								}
							}
							else if (cardId == "EX1_100")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_CHO_07.prefab:ac3d8b3ee38366140b30299bee846f39", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
						else if (cardId == "EX1_110")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_CAIRNE_20.prefab:b43fe34d1ec6a424abe41b02dc0e4196", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
					else if (num <= 913261699U)
					{
						if (num != 896484080U)
						{
							if (num == 913261699U)
							{
								if (cardId == "GVG_115")
								{
									this.m_linesPlayed.Add(cardId);
									yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_TOSHLEY_26.prefab:07c3b6a0fd3b6174dadba657f6248d8f", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
								}
							}
						}
						else if (cardId == "GVG_114")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_SNEED_30.prefab:4e86ebca4343b2e49841d0afa38719cf", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
					else if (num != 963594556U)
					{
						if (num == 997149794U)
						{
							if (cardId == "GVG_112")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_MOGOR_25.prefab:13e09894e4d3edc4a95ccf97a4865fa3", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "GVG_110")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_BOOM_28.prefab:b6075c33b877a14409eafb2f91ca9a6e", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num <= 1339792521U)
				{
					if (num <= 1256014575U)
					{
						if (num != 1013927413U)
						{
							if (num == 1256014575U)
							{
								if (cardId == "GVG_014")
								{
									this.m_linesPlayed.Add(cardId);
									yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_VOLJIN_17.prefab:319230fe533b551469286ecb9fe97dde", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
								}
							}
						}
						else if (cardId == "GVG_113")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_FOEREAPER_29.prefab:356b9c85ddd8ad340a71d4c9c7c9d58c", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
					else if (num != 1313671194U)
					{
						if (num == 1339792521U)
						{
							if (cardId == "EX1_577")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_BEAST_22.prefab:15d2fbd54c2845741af4946fd489ee0a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "FP1_030")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_LOATHEB_16.prefab:cb44878eabffc304db3080dd9571018b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num <= 1423827711U)
				{
					if (num != 1415861302U)
					{
						if (num == 1423827711U)
						{
							if (cardId == "EX1_562")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_ONYXIA_33.prefab:a963754f9a1ffbe489bddca145214f19", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "NEW1_010")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_ALAKIR_34.prefab:da389ff1794394e46bae7929a2db7452", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num != 1440605330U)
				{
					if (num == 1457382949U)
					{
						if (cardId == "EX1_560")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_NOZDORMU_36.prefab:890229ac317e19d45bd1fe7fb67af713", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
				}
				else if (cardId == "EX1_561")
				{
					this.m_linesPlayed.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_ALEXSTRAZA_32.prefab:912653e742620bb4185dc691ba8380d7", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
				}
			}
			else if (num <= 3259477640U)
			{
				if (num <= 2844727953U)
				{
					if (num <= 1975904393U)
					{
						if (num != 1608528615U)
						{
							if (num == 1975904393U)
							{
								if (cardId == "EX1_249")
								{
									this.m_linesPlayed.Add(cardId);
									yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_GEDDON_13.prefab:c4a73265eab005b47bb7d51c74d7b41d", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
								}
							}
						}
						else if (cardId == "EX1_557")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_PAGLE_08.prefab:4681ed30e0c08544c8f67ee5ee64e05c", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
					else if (num != 2811172715U)
					{
						if (num == 2844727953U)
						{
							if (cardId == "EX1_014")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_MUKLA_10.prefab:1bcf6267b4e82b841a48d2a094f99618", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "EX1_016")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_SYLVANAS_19.prefab:c81b1205ba2c977489aaa357f60879ba", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num <= 2931937053U)
				{
					if (num != 2878283191U)
					{
						if (num == 2931937053U)
						{
							if (cardId == "BRM_028")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_THAURISSAN_37.prefab:dc3a267f6d3fafd4196cbd9dd7c145e2", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "EX1_012")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_THALNOS_06.prefab:dfdd1db2a3b8e31439ffb9609a5a686d", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num != 3090915796U)
				{
					if (num == 3259477640U)
					{
						if (cardId == "FP1_010")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_MAEXXNA_24.prefab:4d37c3fd5de32014bb70b071c47e6491", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
				}
				else if (cardId == "PRO_001")
				{
					this.m_linesPlayed.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_ETC_18.prefab:1533c18bd7e1b204ebed7b6fac30fa9a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
				}
			}
			else if (num <= 3764330604U)
			{
				if (num <= 3343365735U)
				{
					if (num != 3326588116U)
					{
						if (num == 3343365735U)
						{
							if (cardId == "FP1_015")
							{
								this.m_linesPlayed.Add(cardId);
								yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_FEUGEN_15.prefab:04b5933629beae54da3357341132b94a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
							}
						}
					}
					else if (cardId == "FP1_014")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_STALAGG_14.prefab:f86dfd95c5c043f40becb7def0ec2b5b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
				else if (num != 3688519067U)
				{
					if (num == 3764330604U)
					{
						if (cardId == "NEW1_038")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_GRUUL_31.prefab:4fdd03655b03494488e8453de69a3c34", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
				}
				else if (cardId == "GVG_007")
				{
					this.m_linesPlayed.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_LEVIATHAN_12.prefab:998e9da05f77b3945ac21f8a7c245738", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
				}
			}
			else if (num <= 3848365794U)
			{
				if (num != 3797738747U)
				{
					if (num == 3848365794U)
					{
						if (cardId == "NEW1_029")
						{
							this.m_linesPlayed.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_MILLHOUSE_09.prefab:5d1b5f555fe70e344afce98d443d9730", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
						}
					}
				}
				else if (cardId == "NEW1_040")
				{
					this.m_linesPlayed.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_HOGGER_27.prefab:ec226470424912547a6971e727116826", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
				}
			}
			else if (num != 3898551556U)
			{
				if (num == 4263625072U)
				{
					if (cardId == "EX1_112")
					{
						this.m_linesPlayed.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_GELBIN_21.prefab:c9710a6ef19cdba46a7c51404bd73851", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
					}
				}
			}
			else if (cardId == "NEW1_030")
			{
				this.m_linesPlayed.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_DEATHWING_35.prefab:c5f1030779827c643bfb810f919819fa", Notification.SpeechBubbleDirection.TopRight, enemyActor, 0.7f, 1f, true, true, 0f));
			}
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003415 RID: 13333 RVA: 0x0010B2F5 File Offset: 0x001094F5
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_TURN1_02.prefab:ef65f9070bf2aa6489a58c705d21b1d9", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA02_1_TURN1_PT2_03.prefab:b500c91480d36e045aece029d3646acc", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003416 RID: 13334 RVA: 0x0010B30B File Offset: 0x0010950B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_GRIMSTONE_DEAD1_30"), "", true, 5f, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_NEFARIAN_GRIMSTONE_DEAD1_30.prefab:cb4444f1726517043ad2997a282b7863", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
			NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", NotificationManager.ALT_ADVENTURE_SCREEN_POS, GameStrings.Get("VO_RAGNAROS_GRIMSTONE_DEAD2_66"), "", true, 7f, null, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_RAGNAROS_GRIMSTONE_DEAD2_66.prefab:5494ef65549f1b449b6132b3ef620f0f", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
		}
		yield break;
	}

	// Token: 0x04001C85 RID: 7301
	private const float PLAY_CARD_DELAY = 0.7f;

	// Token: 0x04001C86 RID: 7302
	private HashSet<string> m_linesPlayed = new HashSet<string>();
}
