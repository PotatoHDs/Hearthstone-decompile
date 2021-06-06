using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class BnetBarFriendButton : FriendListUIElement
{
	// Token: 0x060006A9 RID: 1705 RVA: 0x00026FD0 File Offset: 0x000251D0
	protected override void Awake()
	{
		BnetBarFriendButton.s_instance = this;
		base.Awake();
		if (this.m_Background != null)
		{
			MeshRenderer component = this.m_Background.GetComponent<MeshRenderer>();
			if (component != null)
			{
				this.m_backgroundMaterial = component.GetMaterial();
				this.m_originalLightingBlend = this.m_backgroundMaterial.GetFloat("_LightingBlend");
			}
		}
		this.UpdateOnlineCount();
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		FiresideGatheringManager.OnPatronListUpdated += this.OnFSGPatronsUpdated;
		FiresideGatheringManager.Get().OnJoinFSG += this.OnJoinFSG;
		FiresideGatheringManager.Get().OnLeaveFSG += this.OnLeaveFSG;
		FiresideGatheringManager.Get().OnNearbyFSGs += this.OnNearbyFSGs;
		this.ShowPendingInvitesIcon(false);
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x000270D4 File Offset: 0x000252D4
	protected override void OnDestroy()
	{
		if (BnetFriendMgr.Get() != null)
		{
			BnetFriendMgr.Get().RemoveChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		}
		FiresideGatheringManager.OnPatronListUpdated -= this.OnFSGPatronsUpdated;
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnJoinFSG -= this.OnJoinFSG;
			FiresideGatheringManager.Get().OnLeaveFSG -= this.OnLeaveFSG;
			FiresideGatheringManager.Get().OnNearbyFSGs -= this.OnNearbyFSGs;
		}
		BnetBarFriendButton.s_instance = null;
		base.OnDestroy();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x000271A1 File Offset: 0x000253A1
	public static BnetBarFriendButton Get()
	{
		return BnetBarFriendButton.s_instance;
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x000271A8 File Offset: 0x000253A8
	public void HideTooltip()
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x000271CB File Offset: 0x000253CB
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		this.UpdateOnlineCount();
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000271CB File Offset: 0x000253CB
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		this.UpdateOnlineCount();
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x000271D3 File Offset: 0x000253D3
	private void OnJoinFSG(FSGConfig gathering)
	{
		this.m_Background.SetActive(false);
		this.m_FSGSocialBar.SetActive(true);
		this.UpdateOnlineCount();
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x000271F3 File Offset: 0x000253F3
	private void OnLeaveFSG(FSGConfig gathering)
	{
		this.m_Background.SetActive(true);
		this.m_FSGSocialBar.SetActive(false);
		this.UpdateOnlineCount();
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00027213 File Offset: 0x00025413
	private void OnNearbyFSGs()
	{
		if (BnetBarFriendButton.m_hasClickedWhileFSGGlowing)
		{
			return;
		}
		this.m_FSGGlow.SetActive(true);
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x000271CB File Offset: 0x000253CB
	private void OnFSGPatronsUpdated(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		this.UpdateOnlineCount();
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x000271CB File Offset: 0x000253CB
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.UpdateOnlineCount();
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0002722C File Offset: 0x0002542C
	public void UpdateOnlineCount()
	{
		if (!FiresideGatheringManager.Get().IsCheckedIn)
		{
			int activeOnlineFriendCount = BnetFriendMgr.Get().GetActiveOnlineFriendCount();
			if (activeOnlineFriendCount == 0)
			{
				this.m_OnlineCountText.TextColor = this.m_AllOfflineColor;
			}
			else
			{
				this.m_OnlineCountText.TextColor = this.m_AnyOnlineColor;
			}
			this.m_OnlineCountText.Text = activeOnlineFriendCount.ToString();
			return;
		}
		this.m_OnlineCountText.TextColor = this.m_FSGColor;
		if (FiresideGatheringManager.Get().CurrentFsgIsLargeScale)
		{
			this.m_OnlineCountText.Text = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_SOCIAL_BUTTON_LARGE_SCALE_LABEL");
			return;
		}
		this.m_OnlineCountText.Text = FiresideGatheringManager.Get().DisplayablePatronCount.ToString();
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x000272DA File Offset: 0x000254DA
	public void ShowPendingInvitesIcon(bool show)
	{
		if (this.m_PendingInvitesIcon != null && this.m_PendingInvitesIcon.activeInHierarchy != show)
		{
			this.m_PendingInvitesIcon.SetActive(show);
			this.m_OnlineCountText.gameObject.SetActive(!show);
		}
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00027318 File Offset: 0x00025518
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		base.UpdateHighlight();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00027334 File Offset: 0x00025534
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00027340 File Offset: 0x00025540
	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (!enabled)
		{
			base.UpdateHighlight();
		}
		if (this.m_backgroundMaterial != null)
		{
			this.m_backgroundMaterial.SetFloat("_LightingBlend", enabled ? this.m_originalLightingBlend : 0.8f);
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0002738C File Offset: 0x0002558C
	protected override void OnRelease()
	{
		base.OnRelease();
		if (!BnetBarFriendButton.m_hasClickedWhileFSGGlowing && this.m_FSGGlow.activeInHierarchy)
		{
			this.m_FSGGlow.SetActive(false);
			BnetBarFriendButton.m_hasClickedWhileFSGGlowing = true;
		}
	}

	// Token: 0x0400049C RID: 1180
	public UberText m_OnlineCountText;

	// Token: 0x0400049D RID: 1181
	public Color m_AnyOnlineColor;

	// Token: 0x0400049E RID: 1182
	public Color m_AllOfflineColor;

	// Token: 0x0400049F RID: 1183
	public Color m_FSGColor;

	// Token: 0x040004A0 RID: 1184
	public GameObject m_PendingInvitesIcon;

	// Token: 0x040004A1 RID: 1185
	public GameObject m_FSGSocialBar;

	// Token: 0x040004A2 RID: 1186
	public GameObject m_FSGGlow;

	// Token: 0x040004A3 RID: 1187
	public GameObject m_Background;

	// Token: 0x040004A4 RID: 1188
	private static BnetBarFriendButton s_instance;

	// Token: 0x040004A5 RID: 1189
	private static bool m_hasClickedWhileFSGGlowing;

	// Token: 0x040004A6 RID: 1190
	private Material m_backgroundMaterial;

	// Token: 0x040004A7 RID: 1191
	private float m_originalLightingBlend;
}
