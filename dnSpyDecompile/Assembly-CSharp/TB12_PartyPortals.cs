using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005D0 RID: 1488
public class TB12_PartyPortals : MissionEntity
{
	// Token: 0x06005182 RID: 20866 RVA: 0x001AC9C8 File Offset: 0x001AABC8
	private void GetMediva()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			this.m_mediva = entity.GetCard();
		}
	}

	// Token: 0x06005183 RID: 20867 RVA: 0x001ACA04 File Offset: 0x001AAC04
	public override void PreloadAssets()
	{
		this.last_time_emoted = new bool[100];
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_AranHeroPower_01.prefab:17f84cec05407ad4ba00444532d3dc57");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_ChessEmoteOops_03.prefab:d683b2a98fbbb144d8b77a7fb489ebcd");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_CroneTurn1_03.prefab:ce73297395e1c1345add800864adbc76");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_CuratorBeasts_09.prefab:88b08556c97b42f4abce91e7f74327c1");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_CuratorBeasts_04.prefab:fbc24fd7b3a032d47b4eee419b07c81d");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_IllhoofWounded_03.prefab:0b74579f7557eb74ca82a03a8ebd836a");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_JulianneDeadlyPoison_03.prefab:50ad4576b0fb3ac449a3a278545da800");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_NetherspiteTurn1_02.prefab:9a41a80e5a8f96e4493ddcb6d5d447c6");
		base.PreloadSound("VO_Mediva_01_Female_Blood Elf_PrologueTurn5_02.prefab:d4d9c8af68125694daba05120621c64f");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_AranHeroPower_03.prefab:da922c0672c31da4f81909c2657b5234");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_AranSpell_01.prefab:273579995bc33f74494b7cc7200aafd6");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_AranSpell_10.prefab:b51e4a9e59e8ba849873075607a612d5");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_ChessEmoteOops_04.prefab:8e650f4ad55c8be4f98d7fa413e88aab");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_CroneTurn1_05.prefab:7704001245786da42aedc4ad2f4c4f3f");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_CuratorBeasts_05.prefab:1dc288019d7cc8a4dad344fed01e209a");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_IllhoofWounded_02.prefab:fe2ad172371198c489a2cc828b07e883");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_NetherspiteTurn1_04.prefab:da946132f0f4a7f4d8b6840c31662f0b");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_CuratorBeasts_08.prefab:49cc12aaa1d21734995dbe25480e9b9f");
		base.PreloadSound("VO_Mediva_02_Female_Draenei_PrologueTurn5_02.prefab:af6e8c753bd1d6e409e5e7740a36d99f");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranHeroPower_04.prefab:9bca13673c70fbe4e856fa6cadb44d32");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranSpell_06.prefab:7ee6ed9064bae8b4cae9b616322befa2");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranSpell_01.prefab:391ba56a5ee5e0e4b9098147ac14d7bc");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_ChessEmoteOops_01.prefab:4133c951e3b700242b5747520ab4cf2c");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_CuratorBeasts_01.prefab:bd55c69fcafba594b984a9d7792e43e9");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_IllhoofWounded_01.prefab:6eac5d210aeee3048957194c8f610ceb");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_IllhoofWounded_04.prefab:e774579b014394548914674ee10897e6");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_JulianneDeadlyPoison_02.prefab:3facdbce34af76a438defff4a56c6ce2");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_PrologueTurn5_03.prefab:ee16361b04c4daa419e0006a5b5949e8");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_PrologueTurn5_02.prefab:71fd5f307f54d174e939f2aada7af9bf");
	}

	// Token: 0x06005184 RID: 20868 RVA: 0x001ACB5D File Offset: 0x001AAD5D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.GetMediva();
		if (this.m_mediva == null)
		{
			yield break;
		}
		Actor medivaActor = this.m_mediva.GetActor();
		switch (missionEvent)
		{
		case 1:
			if (!this.last_time_emoted[1])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_AranHeroPower_01.prefab:17f84cec05407ad4ba00444532d3dc57", "VO_TB_MEDIVA1_001", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[1] = true;
			}
			break;
		case 2:
			if (!this.last_time_emoted[2])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_ChessEmoteOops_03.prefab:d683b2a98fbbb144d8b77a7fb489ebcd", "VO_TB_MEDIVA1_002", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[2] = true;
			}
			break;
		case 3:
			if (!this.last_time_emoted[3])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_CroneTurn1_03.prefab:ce73297395e1c1345add800864adbc76", "VO_TB_MEDIVA1_003", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[3] = true;
			}
			break;
		case 4:
			if (!this.last_time_emoted[4])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_CuratorBeasts_09.prefab:88b08556c97b42f4abce91e7f74327c1", "VO_TB_MEDIVA1_004", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[4] = true;
			}
			break;
		case 5:
			if (!this.last_time_emoted[5])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_CuratorBeasts_04.prefab:fbc24fd7b3a032d47b4eee419b07c81d", "VO_TB_MEDIVA1_005", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[5] = true;
			}
			break;
		case 6:
			if (!this.last_time_emoted[6])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_IllhoofWounded_03.prefab:0b74579f7557eb74ca82a03a8ebd836a", "VO_TB_MEDIVA1_006", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[6] = true;
			}
			break;
		case 7:
			if (!this.last_time_emoted[7])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_JulianneDeadlyPoison_03.prefab:50ad4576b0fb3ac449a3a278545da800", "VO_TB_MEDIVA1_007", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[7] = true;
			}
			break;
		case 8:
			if (!this.last_time_emoted[8])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_NetherspiteTurn1_02.prefab:9a41a80e5a8f96e4493ddcb6d5d447c6", "VO_TB_MEDIVA1_008", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[8] = true;
			}
			break;
		case 9:
			if (!this.last_time_emoted[9])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_01_Female_Blood Elf_PrologueTurn5_02.prefab:d4d9c8af68125694daba05120621c64f", "VO_TB_MEDIVA1_009", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[9] = true;
			}
			break;
		case 21:
			if (!this.last_time_emoted[21])
			{
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(1.5f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_AranHeroPower_04.prefab:9bca13673c70fbe4e856fa6cadb44d32", "VO_TB_MEDIVA2_001", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[21] = true;
			}
			break;
		case 22:
			if (!this.last_time_emoted[22])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_AranSpell_01.prefab:273579995bc33f74494b7cc7200aafd6", "VO_TB_MEDIVA2_002", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[22] = true;
			}
			break;
		case 23:
			if (!this.last_time_emoted[23])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_AranSpell_10.prefab:b51e4a9e59e8ba849873075607a612d5", "VO_TB_MEDIVA2_003", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[23] = true;
			}
			break;
		case 24:
			if (!this.last_time_emoted[24])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_ChessEmoteOops_04.prefab:8e650f4ad55c8be4f98d7fa413e88aab", "VO_TB_MEDIVA2_004", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[24] = true;
			}
			break;
		case 25:
			if (!this.last_time_emoted[25])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_CroneTurn1_05.prefab:7704001245786da42aedc4ad2f4c4f3f", "VO_TB_MEDIVA2_005", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[25] = true;
			}
			break;
		case 26:
			if (!this.last_time_emoted[26])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_CuratorBeasts_05.prefab:1dc288019d7cc8a4dad344fed01e209a", "VO_TB_MEDIVA2_006", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[26] = true;
			}
			break;
		case 27:
			if (!this.last_time_emoted[27])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_IllhoofWounded_02.prefab:fe2ad172371198c489a2cc828b07e883", "VO_TB_MEDIVA2_007", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[27] = true;
			}
			break;
		case 28:
			if (!this.last_time_emoted[28])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_NetherspiteTurn1_04.prefab:da946132f0f4a7f4d8b6840c31662f0b", "VO_TB_MEDIVA2_008", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[28] = true;
			}
			break;
		case 29:
			if (!this.last_time_emoted[29])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_PrologueTurn5_02.prefab:af6e8c753bd1d6e409e5e7740a36d99f", "VO_TB_MEDIVA2_010", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[29] = true;
			}
			break;
		case 40:
			if (!this.last_time_emoted[40])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_02_Female_Draenei_CuratorBeasts_08.prefab:49cc12aaa1d21734995dbe25480e9b9f", "VO_TB_MEDIVA2_009", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[40] = true;
			}
			break;
		case 41:
			if (!this.last_time_emoted[41])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_AranHeroPower_04.prefab:9bca13673c70fbe4e856fa6cadb44d32", "VO_TB_MEDIVA3_001", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[41] = true;
			}
			break;
		case 42:
			if (!this.last_time_emoted[42])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_AranSpell_06.prefab:7ee6ed9064bae8b4cae9b616322befa2", "VO_TB_MEDIVA3_002", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[42] = true;
			}
			break;
		case 43:
			if (!this.last_time_emoted[43])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_AranSpell_01.prefab:391ba56a5ee5e0e4b9098147ac14d7bc", "VO_TB_MEDIVA3_003", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[43] = true;
			}
			break;
		case 44:
			if (!this.last_time_emoted[44])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_ChessEmoteOops_01.prefab:4133c951e3b700242b5747520ab4cf2c", "VO_TB_MEDIVA3_004", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[44] = true;
			}
			break;
		case 45:
			if (!this.last_time_emoted[45])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_CuratorBeasts_01.prefab:bd55c69fcafba594b984a9d7792e43e9", "VO_TB_MEDIVA3_005", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[45] = true;
			}
			break;
		case 46:
			if (!this.last_time_emoted[46])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_IllhoofWounded_01.prefab:6eac5d210aeee3048957194c8f610ceb", "VO_TB_MEDIVA3_006", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[46] = true;
			}
			break;
		case 47:
			if (!this.last_time_emoted[47])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_IllhoofWounded_04.prefab:e774579b014394548914674ee10897e6", "VO_TB_MEDIVA3_007", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[47] = true;
			}
			break;
		case 48:
			if (!this.last_time_emoted[48])
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_JulianneDeadlyPoison_02.prefab:3facdbce34af76a438defff4a56c6ce2", "VO_TB_MEDIVA3_008", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[48] = true;
			}
			break;
		case 49:
			if (!this.last_time_emoted[49])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_PrologueTurn5_03.prefab:ee16361b04c4daa419e0006a5b5949e8", "VO_TB_MEDIVA3_009", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[49] = true;
			}
			break;
		case 50:
			if (!this.last_time_emoted[50])
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_Mediva_03_Male_Night Elf_PrologueTurn5_02.prefab:71fd5f307f54d174e939f2aada7af9bf", "VO_TB_MEDIVA3_010", Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
				GameState.Get().SetBusy(false);
				this.last_time_emoted[50] = true;
			}
			break;
		}
		yield break;
	}

	// Token: 0x040048FD RID: 18685
	private Card m_bossCard;

	// Token: 0x040048FE RID: 18686
	private Card m_mediva;

	// Token: 0x040048FF RID: 18687
	private int times_emoted;

	// Token: 0x04004900 RID: 18688
	private bool[] last_time_emoted;
}
