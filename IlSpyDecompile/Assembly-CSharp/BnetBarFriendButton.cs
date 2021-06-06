using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public class BnetBarFriendButton : FriendListUIElement
{
	public UberText m_OnlineCountText;

	public Color m_AnyOnlineColor;

	public Color m_AllOfflineColor;

	public Color m_FSGColor;

	public GameObject m_PendingInvitesIcon;

	public GameObject m_FSGSocialBar;

	public GameObject m_FSGGlow;

	public GameObject m_Background;

	private static BnetBarFriendButton s_instance;

	private static bool m_hasClickedWhileFSGGlowing;

	private Material m_backgroundMaterial;

	private float m_originalLightingBlend;

	protected override void Awake()
	{
		s_instance = this;
		base.Awake();
		if (m_Background != null)
		{
			MeshRenderer component = m_Background.GetComponent<MeshRenderer>();
			if (component != null)
			{
				m_backgroundMaterial = component.GetMaterial();
				m_originalLightingBlend = m_backgroundMaterial.GetFloat("_LightingBlend");
			}
		}
		UpdateOnlineCount();
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		FiresideGatheringManager.OnPatronListUpdated += OnFSGPatronsUpdated;
		FiresideGatheringManager.Get().OnJoinFSG += OnJoinFSG;
		FiresideGatheringManager.Get().OnLeaveFSG += OnLeaveFSG;
		FiresideGatheringManager.Get().OnNearbyFSGs += OnNearbyFSGs;
		ShowPendingInvitesIcon(show: false);
	}

	protected override void OnDestroy()
	{
		if (BnetFriendMgr.Get() != null)
		{
			BnetFriendMgr.Get().RemoveChangeListener(OnFriendsChanged);
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		}
		FiresideGatheringManager.OnPatronListUpdated -= OnFSGPatronsUpdated;
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnJoinFSG -= OnJoinFSG;
			FiresideGatheringManager.Get().OnLeaveFSG -= OnLeaveFSG;
			FiresideGatheringManager.Get().OnNearbyFSGs -= OnNearbyFSGs;
		}
		s_instance = null;
		base.OnDestroy();
	}

	public static BnetBarFriendButton Get()
	{
		return s_instance;
	}

	public void HideTooltip()
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		UpdateOnlineCount();
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		UpdateOnlineCount();
	}

	private void OnJoinFSG(FSGConfig gathering)
	{
		m_Background.SetActive(value: false);
		m_FSGSocialBar.SetActive(value: true);
		UpdateOnlineCount();
	}

	private void OnLeaveFSG(FSGConfig gathering)
	{
		m_Background.SetActive(value: true);
		m_FSGSocialBar.SetActive(value: false);
		UpdateOnlineCount();
	}

	private void OnNearbyFSGs()
	{
		if (!m_hasClickedWhileFSGGlowing)
		{
			m_FSGGlow.SetActive(value: true);
		}
	}

	private void OnFSGPatronsUpdated(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		UpdateOnlineCount();
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		UpdateOnlineCount();
	}

	public void UpdateOnlineCount()
	{
		if (FiresideGatheringManager.Get().IsCheckedIn)
		{
			m_OnlineCountText.TextColor = m_FSGColor;
			if (FiresideGatheringManager.Get().CurrentFsgIsLargeScale)
			{
				m_OnlineCountText.Text = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_SOCIAL_BUTTON_LARGE_SCALE_LABEL");
			}
			else
			{
				m_OnlineCountText.Text = FiresideGatheringManager.Get().DisplayablePatronCount.ToString();
			}
			return;
		}
		int activeOnlineFriendCount = BnetFriendMgr.Get().GetActiveOnlineFriendCount();
		if (activeOnlineFriendCount == 0)
		{
			m_OnlineCountText.TextColor = m_AllOfflineColor;
		}
		else
		{
			m_OnlineCountText.TextColor = m_AnyOnlineColor;
		}
		m_OnlineCountText.Text = activeOnlineFriendCount.ToString();
	}

	public void ShowPendingInvitesIcon(bool show)
	{
		if (m_PendingInvitesIcon != null && m_PendingInvitesIcon.activeInHierarchy != show)
		{
			m_PendingInvitesIcon.SetActive(show);
			m_OnlineCountText.gameObject.SetActive(!show);
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		UpdateHighlight();
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
	}

	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (!enabled)
		{
			UpdateHighlight();
		}
		if (m_backgroundMaterial != null)
		{
			m_backgroundMaterial.SetFloat("_LightingBlend", enabled ? m_originalLightingBlend : 0.8f);
		}
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		if (!m_hasClickedWhileFSGGlowing && m_FSGGlow.activeInHierarchy)
		{
			m_FSGGlow.SetActive(value: false);
			m_hasClickedWhileFSGGlowing = true;
		}
	}
}
