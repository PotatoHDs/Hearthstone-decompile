using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_06_Marrowgar : ICC_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_BoneSpikeLines = new List<string> { "VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_01.prefab:0d8f13b7cfeb71f429b93c866c5275d5", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_02.prefab:aba21ad07742e7f4fb6ca32b258409a1", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_03.prefab:2ecee2b9c6654b84b8f4f28cfe0d286f", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_04.prefab:0f1c5149f556fa4408ddcb17505bb9cf", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_05.prefab:05bc7fb82d9a83b43bc13bd653a7835d" };

	private List<string> m_BoneStormLines = new List<string> { "VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_01.prefab:cc62983b993331b449979a11c301c677", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_02.prefab:e105438b7ae41ab46af20bb556e0ec48", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_03.prefab:db321a5b14fcabf4aa54752084e6b09a", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_04.prefab:db1e405c5fd1c88438cdb06a36738bfb", "VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_05.prefab:44ed33cb18724514698929fc064c2836", "VO_ICC06_Marrowgar_Male_BoneWraith_BonestormFast_01.prefab:a0b973b1695dbed41acad3493971524e" };

	public override void PreloadAssets()
	{
		PreloadSound("VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586");
		PreloadSound("VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14");
		PreloadSound("VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01.prefab:e4ae3ca97af786f409807bfc4036a025");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_01.prefab:0d8f13b7cfeb71f429b93c866c5275d5");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_02.prefab:aba21ad07742e7f4fb6ca32b258409a1");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_03.prefab:2ecee2b9c6654b84b8f4f28cfe0d286f");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_04.prefab:0f1c5149f556fa4408ddcb17505bb9cf");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonespike_05.prefab:05bc7fb82d9a83b43bc13bd653a7835d");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_01.prefab:cc62983b993331b449979a11c301c677");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_02.prefab:e105438b7ae41ab46af20bb556e0ec48");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_03.prefab:db321a5b14fcabf4aa54752084e6b09a");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_04.prefab:db1e405c5fd1c88438cdb06a36738bfb");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Bonestorm_05.prefab:44ed33cb18724514698929fc064c2836");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonestormFast_01.prefab:a0b973b1695dbed41acad3493971524e");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BigHeal_01.prefab:37a633fcb36d4d74284341ae680fd836");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Death_01.prefab:230f3c0c20c575144b4b3c6d77ba668a");
		PreloadSound("VO_ICC06_LichKing_Male_Human_Win_01.prefab:feea7fac05deb3d4ebc10438d785fc96");
		PreloadSound("VO_ICC08_LichKing_Male_Human_Intro_02.prefab:0e52794eab68e6345b9ecc37992e08d9");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01.prefab:3ddb34d8a05cc1441b0ceeba1a3e415d");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonespikePlayerHit_01.prefab:55a5fa6d57681a64c8131262d7328c67");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_BonespikeStolen_01.prefab:0ffc094df922b3a428c0264ea0a9f861");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_LichKing_01.prefab:80c9284da1dc55d428eb606e3fa845e0");
		PreloadSound("VO_ICC06_LichKing_Male_Human_LichKing_02.prefab:83d929c80daaca44f941a7396a4bdf7e");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_DKTransform_01.prefab:3bd3dcc025737a14a87afc96b10543e1");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_AncientOne_01.prefab:fc5871d2551141546aa76bbe1fb4d6cc");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Alexstraza_01.prefab:87e77411c3930be4f9e74c493c1a490a");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_AuchenaiSoulpriest_01.prefab:083a70df0c2732148be19f1e370f3412");
		PreloadSound("VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01.prefab:3ddb34d8a05cc1441b0ceeba1a3e415d",
						m_stringTag = "VO_ICC06_Marrowgar_Male_BoneWraith_EmoteResponse_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.START },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01.prefab:e4ae3ca97af786f409807bfc4036a025",
						m_stringTag = "VO_ICC06_Marrowgar_Male_BoneWraith_Intro_01"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			if (m_BoneSpikeLines.Count != 0)
			{
				GameState.Get().SetBusy(busy: true);
				string text = m_BoneSpikeLines[Random.Range(0, m_BoneSpikeLines.Count)];
				m_BoneSpikeLines.Remove(text);
				yield return PlayLineOnlyOnce(enemyActor, text);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 102:
			if (m_BoneStormLines.Count != 0)
			{
				string line2 = m_BoneStormLines[Random.Range(0, m_BoneStormLines.Count - 1)];
				yield return PlayMissionFlavorLine(enemyActor, line2);
			}
			break;
		case 103:
			if (m_BoneStormLines.Count != 0)
			{
				string line = m_BoneStormLines[Random.Range(0, m_BoneStormLines.Count)];
				yield return PlayMissionFlavorLine(enemyActor, line);
			}
			break;
		case 104:
			m_playedLines.Add(item);
			yield return PlayMissionFlavorLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BonespikePlayerHit_01.prefab:55a5fa6d57681a64c8131262d7328c67");
			break;
		case 105:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BigHeal_01.prefab:37a633fcb36d4d74284341ae680fd836");
			GameState.Get().SetBusy(busy: false);
			break;
		case 106:
			yield return PlayBossLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Death_01.prefab:230f3c0c20c575144b4b3c6d77ba668a");
			break;
		case 109:
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(7f);
			yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_AncientOne_01.prefab:fc5871d2551141546aa76bbe1fb4d6cc");
			GameState.Get().SetBusy(busy: false);
			break;
		case 110:
			yield return new WaitForSeconds(3f);
			yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Alexstraza_01.prefab:87e77411c3930be4f9e74c493c1a490a");
			break;
		case 111:
			m_playedLines.Add(item);
			yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_AuchenaiSoulpriest_01.prefab:083a70df0c2732148be19f1e370f3412");
			break;
		case 113:
			m_playedLines.Add(item);
			yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_BonespikeStolen_01.prefab:0ffc094df922b3a428c0264ea0a9f861");
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ICC_314":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_LichKing_01.prefab:80c9284da1dc55d428eb606e3fa845e0");
				yield return PlayEasterEggLine(GetLichKingFriendlyMinion(), "VO_ICC06_LichKing_Male_Human_LichKing_02.prefab:83d929c80daaca44f941a7396a4bdf7e");
				break;
			case "ICC_854":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf");
				break;
			case "ICCA11_001":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_Skeleton_01.prefab:6fdfc15f57c55b54cacbcccfb416abdf");
				break;
			}
			yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC06_Marrowgar_Male_BoneWraith_DKTransform_01.prefab:3bd3dcc025737a14a87afc96b10543e1");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC06_LichKing_Male_Human_Win_01.prefab:feea7fac05deb3d4ebc10438d785fc96");
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC08_LichKing_Male_Human_Intro_02.prefab:0e52794eab68e6345b9ecc37992e08d9";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}
}
