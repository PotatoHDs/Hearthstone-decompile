using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_LEAGUE_REVIVAL : MissionEntity
{
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01:9ce5e7d613230e941849b5f931223e63");

	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01:b840297c44497db49b6aba5c4d38b4be");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01:96b0cdfdbdede5d49b0837e7f5452166");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01:9f95317390d0fc04aa3078f809ae7c63");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01:10904e8fd88c571499588dddf5f3c6a0");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01 = new AssetReference("    VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01:1066a47a19d00ae4cb4505d2c8c8dc98");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01:60f8218c5dbe1eb49adb5d53e6557930");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01:9e3b3f84619e8ab4488be508fa177e2e");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01:a8a0c0192a5dc04468a5660b83e51c78");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01:a78d50286846bee4ea2057e628f2701f");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoIntro_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoIntro_01:51b93424ec254894085504fe66d623fa");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01:506636baf8d96e84c90d683c57c5bc3e");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01:280e2c387e4d3d148acc4f94b680d7e9");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction1_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction1_01:d3227a1465e0f3a4795e2fc410086bff");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction2_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction2_01:d194007e8d7efcc4d94a01d38f97dc0d");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01:6f7543597725bd74197ad1934731e672");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction3_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction3_01:f7de4b26cb2a29b45832807d0bab3d76");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01:4da47992306698742b05b47b9512af0e");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01:4a548c0acb584414eb7e6eb723a758be");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01:8a2ee849a5d4ae14d89319c767272589");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01:9d3063e2062e4f34ebbfa1eac098f4f6");

	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01:f6c24ba56ab31fe4e95717f127d2d293");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02:de2ec1d9cbf6ec14cb3c391616cfbd4d");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyVictory_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyVictory_01:6d6b2018d94d1dc4bbb49e3ef503baa0");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01:7829393354ab5e441840870b6822d3d0");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_BRB_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_BRB_01:b7c35e173c3688445a1ad3d8f917d285");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannVictory_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannVictory_01:16649f94f78e7ee4bbbffc12791af9c8");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01:1e63b3794a7d3f34591873d0f1faa411");

	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01:3331d0b821459ca4d89cf0d55f5b3518");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01:1197ae20d67fb444da9060c1614baee7");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01:0f616800d07769b478fd548c023eb3f3");

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[1] { "TB_LEAGUE_REVIVAL_01" }
		},
		{
			11,
			new string[1] { "TB_LEAGUE_REVIVAL_02" }
		},
		{
			12,
			new string[2] { "TB_LEAGUE_REVIVAL_03", "TB_LEAGUE_REVIVAL_04" }
		},
		{
			13,
			new string[1] { "TB_LEAGUE_REVIVAL_05" }
		},
		{
			14,
			new string[1] { "TB_LEAGUE_REVIVAL_06" }
		},
		{
			15,
			new string[1] { "TB_LEAGUE_REVIVAL_07" }
		}
	};

	private Player friendlySidePlayer;

	private Entity playerEntity;

	private float popUpScale = 1.25f;

	private Vector3 popUpPos;

	private Notification StartPopup;

	public override void PreloadAssets()
	{
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01);
		PreloadSound(VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01);
		PreloadSound(VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01);
		PreloadSound(VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01);
		PreloadSound(VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01);
		PreloadSound(VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01);
		PreloadSound(VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoIntro_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoReaction1_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoReaction2_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoReaction3_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01);
		PreloadSound(VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01);
		PreloadSound(VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02);
		PreloadSound(VO_ULDA_Finley_Male_Murloc_FinleyVictory_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01);
		PreloadSound(VO_ULDA_Finley_Male_Murloc_BRB_01);
		PreloadSound(VO_ULDA_Brann_Male_Dwarf_BrannVictory_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01);
		PreloadSound(VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01);
		PreloadSound(VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01);
		PreloadSound(VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01);
	}

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	private void SetPopupPosition()
	{
		if (friendlySidePlayer.IsCurrentPlayer())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.z = 66f;
		}
		else
		{
			popUpPos.z = 44f;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor heroActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, -40f);
		if (m_popUpInfo.ContainsKey(missionEvent))
		{
			if (missionEvent == 10)
			{
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
			}
			if (missionEvent == 11)
			{
				GameState.Get().SetBusy(busy: true);
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
				GameState.Get().SetBusy(busy: false);
			}
			if (missionEvent == 12)
			{
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
				yield return new WaitForSeconds(0.5f);
				popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][1]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
			}
			if (missionEvent == 13)
			{
				GameState.Get().SetBusy(busy: true);
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
				GameState.Get().SetBusy(busy: false);
			}
			if (missionEvent == 14)
			{
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
			}
			if (missionEvent == 15)
			{
				GameState.Get().SetBusy(busy: true);
				Notification popup7 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup7, 0f);
				GameState.Get().SetBusy(busy: false);
			}
		}
		if (missionEvent == 16)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 17)
		{
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(2.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			GameState.Get().SetBusy(busy: false);
		}
		if (missionEvent == 20)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(5.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 21)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 22)
		{
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Finley_Male_Murloc_FinleyVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(6.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Finley_Male_Murloc_BRB_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: false);
		}
		if (missionEvent == 30)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 31)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 32)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
		}
		if (missionEvent == 33)
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Brann_Male_Dwarf_BrannVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(5.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
		}
		if (missionEvent == 40)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoIntro_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 41)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
		if (missionEvent == 42)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoReaction2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
		}
		if (missionEvent == 43)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoReaction3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
		}
		if (missionEvent == 44)
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(3.5f);
			GameState.Get().SetBusy(busy: false);
		}
		if (missionEvent == 50)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(6.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor));
		}
		if (missionEvent == 51)
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: false);
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}
}
