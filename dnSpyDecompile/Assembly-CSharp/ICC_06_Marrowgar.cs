using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class ICC_06_Marrowgar : ICC_MissionEntity
{
	// Token: 0x0600359A RID: 13722 RVA: 0x00110674 File Offset: 0x0010E874
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586");
		base.PreloadSound("VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14");
		base.PreloadSound("VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01.prefab:e4ae3ca97af786f409807bfc4036a025");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_01.prefab:0d8f13b7cfeb71f429b93c866c5275d5");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_02.prefab:aba21ad07742e7f4fb6ca32b258409a1");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_03.prefab:2ecee2b9c6654b84b8f4f28cfe0d286f");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_04.prefab:0f1c5149f556fa4408ddcb17505bb9cf");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_05.prefab:05bc7fb82d9a83b43bc13bd653a7835d");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_01.prefab:cc62983b993331b449979a11c301c677");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_02.prefab:e105438b7ae41ab46af20bb556e0ec48");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_03.prefab:db321a5b14fcabf4aa54752084e6b09a");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_04.prefab:db1e405c5fd1c88438cdb06a36738bfb");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_05.prefab:44ed33cb18724514698929fc064c2836");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonestormFast_01.prefab:a0b973b1695dbed41acad3493971524e");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BigHeal_01.prefab:37a633fcb36d4d74284341ae680fd836");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Death_01.prefab:230f3c0c20c575144b4b3c6d77ba668a");
		base.PreloadSound("VO_ICC06_LichKing_Male_Human_Win_01.prefab:feea7fac05deb3d4ebc10438d785fc96");
		base.PreloadSound("VO_ICC08_LichKing_Male_Human_Intro_02.prefab:0e52794eab68e6345b9ecc37992e08d9");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01.prefab:3ddb34d8a05cc1441b0ceeba1a3e415d");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonespikePlayerHit_01.prefab:55a5fa6d57681a64c8131262d7328c67");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonespikeStolen_01.prefab:0ffc094df922b3a428c0264ea0a9f861");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_LichKing_01.prefab:80c9284da1dc55d428eb606e3fa845e0");
		base.PreloadSound("VO_ICC06_LichKing_Male_Human_LichKing_02.prefab:83d929c80daaca44f941a7396a4bdf7e");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_DKTransform_01.prefab:3bd3dcc025737a14a87afc96b10543e1");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_AncientOne_01.prefab:fc5871d2551141546aa76bbe1fb4d6cc");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Alexstraza_01.prefab:87e77411c3930be4f9e74c493c1a490a");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_AuchenaiSoulpriest_01.prefab:083a70df0c2732148be19f1e370f3412");
		base.PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf");
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x001107C0 File Offset: 0x0010E9C0
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
						m_soundName = "VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01.prefab:3ddb34d8a05cc1441b0ceeba1a3e415d",
						m_stringTag = "VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.START
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01.prefab:e4ae3ca97af786f409807bfc4036a025",
						m_stringTag = "VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01"
					}
				}
			}
		};
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x00110868 File Offset: 0x0010EA68
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			if (this.m_BoneSpikeLines.Count != 0)
			{
				GameState.Get().SetBusy(true);
				string text = this.m_BoneSpikeLines[UnityEngine.Random.Range(0, this.m_BoneSpikeLines.Count)];
				this.m_BoneSpikeLines.Remove(text);
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 102:
			if (this.m_BoneStormLines.Count != 0)
			{
				string line = this.m_BoneStormLines[UnityEngine.Random.Range(0, this.m_BoneStormLines.Count - 1)];
				yield return base.PlayMissionFlavorLine(enemyActor, line, 2.5f);
			}
			break;
		case 103:
			if (this.m_BoneStormLines.Count != 0)
			{
				string line2 = this.m_BoneStormLines[UnityEngine.Random.Range(0, this.m_BoneStormLines.Count)];
				yield return base.PlayMissionFlavorLine(enemyActor, line2, 2.5f);
			}
			break;
		case 104:
			this.m_playedLines.Add(item);
			yield return base.PlayMissionFlavorLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BonespikePlayerHit_01.prefab:55a5fa6d57681a64c8131262d7328c67", 2.5f);
			break;
		case 105:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BigHeal_01.prefab:37a633fcb36d4d74284341ae680fd836", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 106:
			yield return base.PlayBossLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Death_01.prefab:230f3c0c20c575144b4b3c6d77ba668a", 2.5f);
			break;
		case 109:
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(7f);
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_AncientOne_01.prefab:fc5871d2551141546aa76bbe1fb4d6cc", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 110:
			yield return new WaitForSeconds(3f);
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Alexstraza_01.prefab:87e77411c3930be4f9e74c493c1a490a", 2.5f);
			break;
		case 111:
			this.m_playedLines.Add(item);
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_AuchenaiSoulpriest_01.prefab:083a70df0c2732148be19f1e370f3412", 2.5f);
			break;
		case 113:
			this.m_playedLines.Add(item);
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BonespikeStolen_01.prefab:0ffc094df922b3a428c0264ea0a9f861", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x0011087E File Offset: 0x0010EA7E
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ICC_314"))
		{
			if (!(cardId == "ICC_854"))
			{
				if (cardId == "ICCA11_001")
				{
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_LichKing_01.prefab:80c9284da1dc55d428eb606e3fa845e0", 2.5f);
			yield return base.PlayEasterEggLine(base.GetLichKingFriendlyMinion(), "VO_ICC06_LichKing_Male_Human_LichKing_02.prefab:83d929c80daaca44f941a7396a4bdf7e", 2.5f);
		}
		yield return base.IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_DKTransform_01.prefab:3bd3dcc025737a14a87afc96b10543e1");
		yield break;
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x00110894 File Offset: 0x0010EA94
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC06_LichKing_Male_Human_Win_01.prefab:feea7fac05deb3d4ebc10438d785fc96", 2.5f);
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC08_LichKing_Male_Human_Intro_02.prefab:0e52794eab68e6345b9ecc37992e08d9";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x04001CF5 RID: 7413
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001CF6 RID: 7414
	private List<string> m_BoneSpikeLines = new List<string>
	{
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_01.prefab:0d8f13b7cfeb71f429b93c866c5275d5",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_02.prefab:aba21ad07742e7f4fb6ca32b258409a1",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_03.prefab:2ecee2b9c6654b84b8f4f28cfe0d286f",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_04.prefab:0f1c5149f556fa4408ddcb17505bb9cf",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_05.prefab:05bc7fb82d9a83b43bc13bd653a7835d"
	};

	// Token: 0x04001CF7 RID: 7415
	private List<string> m_BoneStormLines = new List<string>
	{
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_01.prefab:cc62983b993331b449979a11c301c677",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_02.prefab:e105438b7ae41ab46af20bb556e0ec48",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_03.prefab:db321a5b14fcabf4aa54752084e6b09a",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_04.prefab:db1e405c5fd1c88438cdb06a36738bfb",
		"VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_05.prefab:44ed33cb18724514698929fc064c2836",
		"VO_ICC06_Marrowgar_Male_BoneWraith_BonestormFast_01.prefab:a0b973b1695dbed41acad3493971524e"
	};
}
