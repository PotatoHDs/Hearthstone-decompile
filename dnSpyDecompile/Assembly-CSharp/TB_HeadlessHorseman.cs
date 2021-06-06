using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005AF RID: 1455
public class TB_HeadlessHorseman : MissionEntity
{
	// Token: 0x060050AE RID: 20654 RVA: 0x001A8074 File Offset: 0x001A6274
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_01);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_02);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_04);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_06);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_07);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_08);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_10);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_12);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_13);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_17);
		base.PreloadSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_19);
		base.PreloadSound(TB_HeadlessHorseman.VO_CS2_222_Attack_02);
	}

	// Token: 0x060050AF RID: 20655 RVA: 0x001A8144 File Offset: 0x001A6344
	private void GetHorsemanHead()
	{
		this.isHeadInPlay = false;
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		if (tag == 0)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			this.isHeadInPlay = (entity.GetZone() == TAG_ZONE.PLAY);
		}
		if (entity != null)
		{
			this.headCard = entity.GetCard();
		}
		if (this.headCard != null)
		{
			this.headActor = this.headCard.GetActor();
		}
	}

	// Token: 0x060050B0 RID: 20656 RVA: 0x001A81BC File Offset: 0x001A63BC
	private void GetHorseman()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		Entity entity = GameState.Get().GetEntity(tag);
		if (tag == 0)
		{
			return;
		}
		if (entity != null)
		{
			this.horsemanCard = entity.GetCard();
			this.isHorsemanInPlay = (entity.GetZone() == TAG_ZONE.PLAY);
		}
		if (this.horsemanCard != null)
		{
			this.horsemanActor = this.horsemanCard.GetActor();
		}
	}

	// Token: 0x060050B1 RID: 20657 RVA: 0x001A8228 File Offset: 0x001A6428
	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		this._announcerLinesPlayed++;
		int announcerLinesPlayed = this._announcerLinesPlayed;
		if (announcerLinesPlayed == 1)
		{
			return base.GetPreloadedSound(TB_HeadlessHorseman.VO_CS2_222_Attack_02);
		}
		if (announcerLinesPlayed != 2)
		{
			return base.GetAnnouncerLine(heroCard, type);
		}
		return base.GetPreloadedSound(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_19);
	}

	// Token: 0x060050B2 RID: 20658 RVA: 0x001A827E File Offset: 0x001A647E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 15)
		{
			this.GetHorsemanHead();
			this.GetHorseman();
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.hasSpoken && missionEvent != 99)
		{
			yield break;
		}
		if (missionEvent != 15)
		{
			this.GetHorsemanHead();
			this.GetHorseman();
		}
		switch (missionEvent)
		{
		case 10:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_04, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			this.hasSpoken = true;
			break;
		case 11:
			if (this.isHeadInPlay)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_08, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
			}
			else if (this.isHorsemanInPlay)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_08, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
			}
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(false);
			this.hasSpoken = true;
			break;
		case 12:
			this._fireballDialogDelay++;
			if (this._fireballDialogDelay > 1 && this._fireballDialogDelay <= 2)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_02, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(2.5f);
				GameState.Get().SetBusy(false);
				this.hasSpoken = true;
			}
			break;
		case 13:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_10, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(4f);
			GameState.Get().SetBusy(false);
			this.hasSpoken = true;
			break;
		case 14:
			if (!this._hasPlayed14)
			{
				this._hasPlayed14 = true;
				if (this.isHeadInPlay)
				{
					Debug.LogWarning(this.isHeadInPlay);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_13, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
				}
				else if (this.isHorsemanInPlay)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_13, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
				}
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(4f);
				GameState.Get().SetBusy(false);
				this.hasSpoken = true;
			}
			break;
		case 15:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_17, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
			this.hasSpoken = true;
			break;
		case 16:
			if (this.isHeadInPlay && this.isHorsemanInPlay && !this._hasPlayed16)
			{
				this._hasPlayed16 = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_01, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(5f);
				GameState.Get().SetBusy(false);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_10, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(false);
				this.hasSpoken = true;
			}
			break;
		case 17:
			if (!this._hasPlayed17)
			{
				this._hasPlayed17 = true;
				if (this.isHeadInPlay)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_12, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
				}
				else if (this.isHorsemanInPlay)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_12, Notification.SpeechBubbleDirection.TopRight, this.horsemanActor, 3f, 1f, true, false, 0f));
				}
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(false);
				this.hasSpoken = true;
			}
			break;
		case 18:
			if (!this._hasPlayed18)
			{
				this._hasPlayed18 = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_HeadlessHorseman.VO_HeadlessHorseman_Male_Human_HallowsEve_07, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(false);
				this.hasSpoken = true;
			}
			break;
		case 19:
			break;
		case 20:
			this.popUpPos = new Vector3(0f, 0f, 0f);
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get("TB_HEADLESS_HORSEMAN_POISON"), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 5f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			this.hasSpoken = true;
			break;
		default:
			if (missionEvent == 99)
			{
				this.hasSpoken = false;
			}
			break;
		}
		yield break;
	}

	// Token: 0x040046FF RID: 18175
	private Actor horsemanActor;

	// Token: 0x04004700 RID: 18176
	private Card horsemanCard;

	// Token: 0x04004701 RID: 18177
	private Actor headActor;

	// Token: 0x04004702 RID: 18178
	private Card headCard;

	// Token: 0x04004703 RID: 18179
	private bool isHeadInPlay;

	// Token: 0x04004704 RID: 18180
	private bool isHorsemanInPlay;

	// Token: 0x04004705 RID: 18181
	private bool hasSpoken;

	// Token: 0x04004706 RID: 18182
	private static readonly AssetReference VO_CS2_222_Attack_02 = new AssetReference("VO_CS2_222_Attack_02.prefab:c3191e3764f78654899b70a311936b93");

	// Token: 0x04004707 RID: 18183
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_01.prefab:95ede70d25607fd47923f829d4e5de42");

	// Token: 0x04004708 RID: 18184
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_02.prefab:35efdbdae6db14745bb99a6bf351634a");

	// Token: 0x04004709 RID: 18185
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_04 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_04.prefab:6324e43e11bdb2448ac1de3c8d07d048");

	// Token: 0x0400470A RID: 18186
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_06 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_06.prefab:36e1b30c8aceaf04992bc6cf0959d9c6");

	// Token: 0x0400470B RID: 18187
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_07 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_07.prefab:b88173866204fce4da7cbb3f7dddc915");

	// Token: 0x0400470C RID: 18188
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_08 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_08.prefab:dc0854d9ad190c84dbbea8f4b0f99dfe");

	// Token: 0x0400470D RID: 18189
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_10 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_10.prefab:92570580735ed754589290f4df5058bb");

	// Token: 0x0400470E RID: 18190
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_12 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_12.prefab:5b588aa7f8994a04eaf048ca87d73807");

	// Token: 0x0400470F RID: 18191
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_13 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_13.prefab:a015bfc61fca6a0489f276e3e2fbb0a3");

	// Token: 0x04004710 RID: 18192
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_17 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_17.prefab:84d6d0029d5f72542926291be8e40b39");

	// Token: 0x04004711 RID: 18193
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_19 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_19.prefab:74a92ec2af554f94fb8c6e205c561bde");

	// Token: 0x04004712 RID: 18194
	private Vector3 popUpPos;

	// Token: 0x04004713 RID: 18195
	private Notification StartPopup;

	// Token: 0x04004714 RID: 18196
	private int _announcerLinesPlayed;

	// Token: 0x04004715 RID: 18197
	private int _fireballDialogDelay;

	// Token: 0x04004716 RID: 18198
	private bool _hasPlayed10;

	// Token: 0x04004717 RID: 18199
	private bool _hasPlayed11;

	// Token: 0x04004718 RID: 18200
	private bool _hasPlayed12;

	// Token: 0x04004719 RID: 18201
	private bool _hasPlayed13;

	// Token: 0x0400471A RID: 18202
	private bool _hasPlayed14;

	// Token: 0x0400471B RID: 18203
	private bool _hasPlayed15;

	// Token: 0x0400471C RID: 18204
	private bool _hasPlayed16;

	// Token: 0x0400471D RID: 18205
	private bool _hasPlayed17;

	// Token: 0x0400471E RID: 18206
	private bool _hasPlayed18;

	// Token: 0x0400471F RID: 18207
	private bool _hasPlayed19;

	// Token: 0x04004720 RID: 18208
	private bool _hasPlayed20;

	// Token: 0x04004721 RID: 18209
	private bool _hasPlayed21;
}
