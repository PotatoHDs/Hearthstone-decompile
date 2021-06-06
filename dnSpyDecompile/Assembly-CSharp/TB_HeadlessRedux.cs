using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B0 RID: 1456
public class TB_HeadlessRedux : MissionEntity
{
	// Token: 0x060050B5 RID: 20661 RVA: 0x001A8355 File Offset: 0x001A6555
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060050B6 RID: 20662 RVA: 0x001A8367 File Offset: 0x001A6567
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_HeadlessRedux.VO_HeadlessHorseman_Male_Human_HallowsEve_13.ToString());
		base.PreloadSound(TB_HeadlessRedux.VO_CS2_222_Attack_02.ToString());
	}

	// Token: 0x060050B7 RID: 20663 RVA: 0x001A838C File Offset: 0x001A658C
	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		this._announcerLinesPlayed++;
		int announcerLinesPlayed = this._announcerLinesPlayed;
		if (announcerLinesPlayed == 1)
		{
			return base.GetPreloadedSound(TB_HeadlessRedux.VO_CS2_222_Attack_02.ToString());
		}
		if (announcerLinesPlayed != 2)
		{
			return base.GetAnnouncerLine(heroCard, type);
		}
		return base.GetPreloadedSound(TB_HeadlessRedux.VO_HeadlessHorseman_Male_Human_HallowsEve_13.ToString());
	}

	// Token: 0x060050B8 RID: 20664 RVA: 0x001A83E4 File Offset: 0x001A65E4
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

	// Token: 0x060050B9 RID: 20665 RVA: 0x001A8459 File Offset: 0x001A6659
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.isPlayerHorseman = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			if (this.isPlayerHorseman == 1)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][1]), false, NotificationManager.PopupTextType.FANCY);
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				yield return new WaitForSeconds(1f);
				popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[11][1]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
			else
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				yield return new WaitForSeconds(1f);
				popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[11][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
		}
		yield break;
	}

	// Token: 0x060050BA RID: 20666 RVA: 0x001A846F File Offset: 0x001A666F
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04004722 RID: 18210
	private static readonly AssetReference VO_CS2_222_Attack_02 = new AssetReference("VO_CS2_222_Attack_02.prefab:c3191e3764f78654899b70a311936b93");

	// Token: 0x04004723 RID: 18211
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_13 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_13.prefab:a015bfc61fca6a0489f276e3e2fbb0a3");

	// Token: 0x04004724 RID: 18212
	private float popUpScale = 1f;

	// Token: 0x04004725 RID: 18213
	private Vector3 popUpPos;

	// Token: 0x04004726 RID: 18214
	private Notification StartPopup;

	// Token: 0x04004727 RID: 18215
	private int _announcerLinesPlayed;

	// Token: 0x04004728 RID: 18216
	private bool _hasPlayed11;

	// Token: 0x04004729 RID: 18217
	private bool _hasPlayed12;

	// Token: 0x0400472A RID: 18218
	private bool _hasPlayed13;

	// Token: 0x0400472B RID: 18219
	private bool _hasPlayed14;

	// Token: 0x0400472C RID: 18220
	private bool _hasPlayed15;

	// Token: 0x0400472D RID: 18221
	private bool _hasPlayed16;

	// Token: 0x0400472E RID: 18222
	private bool _hasPlayed17;

	// Token: 0x0400472F RID: 18223
	private bool _hasPlayed18;

	// Token: 0x04004730 RID: 18224
	private bool _hasPlayed19;

	// Token: 0x04004731 RID: 18225
	private bool _hasPlayed20;

	// Token: 0x04004732 RID: 18226
	private bool _hasPlayed21;

	// Token: 0x04004733 RID: 18227
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_HEADLESSREDUX_01",
				"TB_HEADLESSREDUX_03"
			}
		},
		{
			11,
			new string[]
			{
				"TB_HEADLESSREDUX_02",
				"TB_HEADLESSREDUX_04"
			}
		}
	};

	// Token: 0x04004734 RID: 18228
	private Player friendlySidePlayer;

	// Token: 0x04004735 RID: 18229
	private Entity playerEntity;

	// Token: 0x04004736 RID: 18230
	private int isPlayerHorseman;
}
