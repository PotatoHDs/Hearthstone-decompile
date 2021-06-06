using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B5 RID: 1461
public class TB_LEAGUE_REVIVAL : MissionEntity
{
	// Token: 0x060050D6 RID: 20694 RVA: 0x001A8CE0 File Offset: 0x001A6EE0
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoIntro_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction2_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction3_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_FinleyVictory_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_BRB_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannVictory_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01);
		base.PreloadSound(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01);
	}

	// Token: 0x060050D7 RID: 20695 RVA: 0x001A8EDD File Offset: 0x001A70DD
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060050D8 RID: 20696 RVA: 0x001A8EF0 File Offset: 0x001A70F0
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x060050D9 RID: 20697 RVA: 0x001A8F65 File Offset: 0x001A7165
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor heroActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			if (missionEvent == 10)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			if (missionEvent == 11)
			{
				GameState.Get().SetBusy(true);
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
			if (missionEvent == 12)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				yield return new WaitForSeconds(0.5f);
				popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][1]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			if (missionEvent == 13)
			{
				GameState.Get().SetBusy(true);
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
			if (missionEvent == 14)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			if (missionEvent == 15)
			{
				GameState.Get().SetBusy(true);
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
		}
		if (missionEvent == 16)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 17)
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(2.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
		}
		if (missionEvent == 20)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(5.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 21)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 22)
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_FinleyVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(6.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Finley_Male_Murloc_BRB_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
		}
		if (missionEvent == 30)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 31)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 32)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 33)
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Brann_Male_Dwarf_BrannVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(5.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
		}
		if (missionEvent == 40)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoIntro_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 41)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 42)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 43)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoReaction3_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 44)
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3.5f);
			GameState.Get().SetBusy(false);
		}
		if (missionEvent == 50)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(6.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01, Notification.SpeechBubbleDirection.BottomLeft, heroActor, 3f, 1f, true, false, 0f));
		}
		if (missionEvent == 51)
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_LEAGUE_REVIVAL.VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060050DA RID: 20698 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060050DB RID: 20699 RVA: 0x001A8F7B File Offset: 0x001A717B
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04004778 RID: 18296
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseIntro_01:9ce5e7d613230e941849b5f931223e63");

	// Token: 0x04004779 RID: 18297
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_EliseIntroResponse1_01:b840297c44497db49b6aba5c4d38b4be");

	// Token: 0x0400477A RID: 18298
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_FinleyIntro_01:96b0cdfdbdede5d49b0837e7f5452166");

	// Token: 0x0400477B RID: 18299
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery1_01:9f95317390d0fc04aa3078f809ae7c63");

	// Token: 0x0400477C RID: 18300
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_PostFinleyDiscovery2_01:10904e8fd88c571499588dddf5f3c6a0");

	// Token: 0x0400477D RID: 18301
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01 = new AssetReference("    VO_ULDA_Finley_Male_Murloc_FinleyReaction1_01:1066a47a19d00ae4cb4505d2c8c8dc98");

	// Token: 0x0400477E RID: 18302
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannIntro_01:60f8218c5dbe1eb49adb5d53e6557930");

	// Token: 0x0400477F RID: 18303
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannIntroResponse1_01:9e3b3f84619e8ab4488be508fa177e2e");

	// Token: 0x04004780 RID: 18304
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannReaction3_01:a8a0c0192a5dc04468a5660b83e51c78");

	// Token: 0x04004781 RID: 18305
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannTurn1_01:a78d50286846bee4ea2057e628f2701f");

	// Token: 0x04004782 RID: 18306
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoIntro_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoIntro_01:51b93424ec254894085504fe66d623fa");

	// Token: 0x04004783 RID: 18307
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoIntroResponse1_01:506636baf8d96e84c90d683c57c5bc3e");

	// Token: 0x04004784 RID: 18308
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoIntroResponse3_01:280e2c387e4d3d148acc4f94b680d7e9");

	// Token: 0x04004785 RID: 18309
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction1_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction1_01:d3227a1465e0f3a4795e2fc410086bff");

	// Token: 0x04004786 RID: 18310
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction2_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction2_01:d194007e8d7efcc4d94a01d38f97dc0d");

	// Token: 0x04004787 RID: 18311
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoReaction2Response1_01:6f7543597725bd74197ad1934731e672");

	// Token: 0x04004788 RID: 18312
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoReaction3_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoReaction3_01:f7de4b26cb2a29b45832807d0bab3d76");

	// Token: 0x04004789 RID: 18313
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoReaction3Response1_01:4da47992306698742b05b47b9512af0e");

	// Token: 0x0400478A RID: 18314
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnIntro_01:4a548c0acb584414eb7e6eb723a758be");

	// Token: 0x0400478B RID: 18315
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_TekahnIntroResponse1_01:8a2ee849a5d4ae14d89319c767272589");

	// Token: 0x0400478C RID: 18316
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseVictory_01:9d3063e2062e4f34ebbfa1eac098f4f6");

	// Token: 0x0400478D RID: 18317
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_EliseVictoryResponse1_01:f6c24ba56ab31fe4e95717f127d2d293");

	// Token: 0x0400478E RID: 18318
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_EliseVictoryResponse2_02:de2ec1d9cbf6ec14cb3c391616cfbd4d");

	// Token: 0x0400478F RID: 18319
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyVictory_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyVictory_01:6d6b2018d94d1dc4bbb49e3ef503baa0");

	// Token: 0x04004790 RID: 18320
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_FinleyVictoryResponse_01:7829393354ab5e441840870b6822d3d0");

	// Token: 0x04004791 RID: 18321
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_BRB_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_BRB_01:b7c35e173c3688445a1ad3d8f917d285");

	// Token: 0x04004792 RID: 18322
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannVictory_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannVictory_01:16649f94f78e7ee4bbbffc12791af9c8");

	// Token: 0x04004793 RID: 18323
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_BrannVictoryResponse1_01:1e63b3794a7d3f34591873d0f1faa411");

	// Token: 0x04004794 RID: 18324
	private static readonly AssetReference VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01 = new AssetReference("VO_ULDA_Elise_Female_Night_Elf_RenoVictory_01:3331d0b821459ca4d89cf0d55f5b3518");

	// Token: 0x04004795 RID: 18325
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoVictoryResponse1_01:1197ae20d67fb444da9060c1614baee7");

	// Token: 0x04004796 RID: 18326
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_Plague_Lord_TekahnVictory_01:0f616800d07769b478fd548c023eb3f3");

	// Token: 0x04004797 RID: 18327
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_01"
			}
		},
		{
			11,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_02"
			}
		},
		{
			12,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_03",
				"TB_LEAGUE_REVIVAL_04"
			}
		},
		{
			13,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_05"
			}
		},
		{
			14,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_06"
			}
		},
		{
			15,
			new string[]
			{
				"TB_LEAGUE_REVIVAL_07"
			}
		}
	};

	// Token: 0x04004798 RID: 18328
	private Player friendlySidePlayer;

	// Token: 0x04004799 RID: 18329
	private Entity playerEntity;

	// Token: 0x0400479A RID: 18330
	private float popUpScale = 1.25f;

	// Token: 0x0400479B RID: 18331
	private Vector3 popUpPos;

	// Token: 0x0400479C RID: 18332
	private Notification StartPopup;
}
