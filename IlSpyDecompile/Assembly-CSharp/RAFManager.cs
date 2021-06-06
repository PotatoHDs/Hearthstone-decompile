using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusUtil;
using UnityEngine;

public class RAFManager : IService
{
	public class RecruitData
	{
		public PegasusUtil.RecruitData m_recruit;

		public string m_recruitBattleTag;
	}

	private bool m_isRAFLoading;

	private RAFFrame m_RAFFrame;

	private string m_rafDisplayURL;

	private string m_rafFullURL;

	private bool m_hasRAFData;

	private uint m_totalRecruitCount;

	private List<RecruitData> m_topRecruits;

	public const int MAX_RECRUITS_SHOWN = 5;

	public const int MAX_PROGRESS_LEVEL = 20;

	public const int REWARDED_HERO_ID = 17;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(ProcessRecruitAFriendResponse.PacketID.ID, OnProcessRecruitResponse);
		network.RegisterNetHandler(RecruitAFriendURLResponse.PacketID.ID, OnURLResponse);
		network.RegisterNetHandler(RecruitAFriendDataResponse.PacketID.ID, OnDataResponse);
		HearthstoneApplication.Get().WillReset += WillReset;
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(Network),
			typeof(SoundManager)
		};
	}

	public void Shutdown()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= WillReset;
		}
		Network service = null;
		if (HearthstoneServices.TryGet<Network>(out service))
		{
			service.RemoveNetHandler(ProcessRecruitAFriendResponse.PacketID.ID, OnProcessRecruitResponse);
			service.RemoveNetHandler(RecruitAFriendURLResponse.PacketID.ID, OnURLResponse);
			service.RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, OnDataResponse);
		}
	}

	public static RAFManager Get()
	{
		return HearthstoneServices.Get<RAFManager>();
	}

	public void WillReset()
	{
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
		m_RAFFrame = null;
		m_rafDisplayURL = null;
		m_rafFullURL = null;
		m_hasRAFData = false;
		m_totalRecruitCount = 0u;
		m_topRecruits = null;
	}

	public void InitializeRequests()
	{
		Network.Get().RequestProcessRecruitAFriend();
	}

	public void ShowRAFFrame()
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (!m_hasRAFData)
		{
			Log.RAF.Print("Network.RequestRecruitAFriendData");
			Network.Get().RequestRecruitAFriendData();
		}
		Processor.CancelCoroutine(ShowRAFFrameWhenReady());
		Processor.RunCoroutine(ShowRAFFrameWhenReady());
	}

	public RAFFrame GetRAFFrame()
	{
		return m_RAFFrame;
	}

	public void ShowRAFHeroFrame()
	{
		if (m_RAFFrame != null)
		{
			m_RAFFrame.ShowHeroFrame();
		}
	}

	public void ShowRAFProgressFrame()
	{
		if (m_RAFFrame != null)
		{
			m_RAFFrame.ShowProgressFrame();
		}
	}

	public void SetRAFProgress(int progress)
	{
		if (m_RAFFrame != null)
		{
			m_RAFFrame.SetProgress(progress);
		}
	}

	public string GetRecruitDisplayURL()
	{
		if (m_rafDisplayURL != null)
		{
			return m_rafDisplayURL;
		}
		Log.RAF.Print("Network.RequestRecruitAFriendURL");
		Network.Get().RequestRecruitAFriendUrl();
		return null;
	}

	public string GetRecruitFullURL()
	{
		if (m_rafFullURL != null)
		{
			return m_rafFullURL;
		}
		return null;
	}

	public void GotoRAFWebsite()
	{
		Processor.CancelCoroutine(SendToRAFWebsiteThenHide());
		Processor.RunCoroutine(SendToRAFWebsiteThenHide());
	}

	public uint GetTotalRecruitCount()
	{
		return m_totalRecruitCount;
	}

	private IEnumerator ShowRAFFrameWhenReady()
	{
		if (m_RAFFrame == null && !m_isRAFLoading)
		{
			m_isRAFLoading = true;
			AssetLoader.Get().InstantiatePrefab("RAF_main.prefab:5fa2642eb52ae469dbe27e96a7570e08", OnRAFLoaded);
		}
		while (m_RAFFrame == null)
		{
			yield return null;
		}
		while (!m_hasRAFData)
		{
			yield return null;
		}
		m_RAFFrame.Show();
		ChatMgr.Get().CloseFriendsList();
	}

	private void OnRAFLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_isRAFLoading = false;
		if (go == null)
		{
			Log.RAF.PrintError("RAFManager.OnRAFLoaded() - FAILED to load RAFManager GameObject");
			return;
		}
		m_RAFFrame = go.GetComponent<RAFFrame>();
		if (m_RAFFrame == null)
		{
			Log.RAF.PrintError(string.Concat("RAFManager.OnRAFLoaded() - ERROR RAFManager GameObject has no ", typeof(RAFFrame), " component"));
		}
		else if (m_hasRAFData)
		{
			if (m_totalRecruitCount != 0)
			{
				m_RAFFrame.SetProgressData(m_totalRecruitCount, m_topRecruits);
				m_RAFFrame.ShowProgressFrame();
			}
			else
			{
				m_RAFFrame.ShowHeroFrame();
			}
		}
	}

	private void OnProcessRecruitResponse()
	{
	}

	private void OnURLResponse()
	{
		RecruitAFriendURLResponse recruitAFriendUrlResponse = Network.Get().GetRecruitAFriendUrlResponse();
		if (recruitAFriendUrlResponse == null || recruitAFriendUrlResponse.RafServiceStatus == RAFServiceStatus.RAFServiceStatus_NotAvailable || string.IsNullOrEmpty(recruitAFriendUrlResponse.RafUrl))
		{
			string text = "RAFManager.OnURLResponse() - Response not valid!";
			if (recruitAFriendUrlResponse != null)
			{
				text += ((string.Concat(" ", recruitAFriendUrlResponse.RafServiceStatus, ", ", recruitAFriendUrlResponse.RafUrl) == null) ? "null" : recruitAFriendUrlResponse.RafUrl);
			}
			Log.RAF.PrintError(text);
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_RAF_ERROR_HEADER"),
				m_showAlertIcon = true,
				m_text = GameStrings.Get("GLUE_RAF_ERROR_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = null
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			m_rafDisplayURL = recruitAFriendUrlResponse.RafUrl;
			Log.RAF.Print("Recruit URL = " + m_rafDisplayURL);
			if (m_RAFFrame != null)
			{
				m_rafFullURL = recruitAFriendUrlResponse.RafUrlFull;
				m_RAFFrame.ShowLinkFrame(m_rafDisplayURL, m_rafFullURL);
			}
		}
	}

	private void OnDataResponse()
	{
		RecruitAFriendDataResponse recruitAFriendDataResponse = Network.Get().GetRecruitAFriendDataResponse();
		if (recruitAFriendDataResponse == null)
		{
			Log.RAF.PrintError("RAFManager.OnDataResponse() - Recruit Data is NULL!");
			return;
		}
		m_hasRAFData = true;
		m_totalRecruitCount = recruitAFriendDataResponse.TotalRecruitCount;
		m_topRecruits = new List<RecruitData>();
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += OnPresenceChanged;
		for (int i = 0; i < recruitAFriendDataResponse.TopRecruits.Count; i++)
		{
			RecruitData recruitData = new RecruitData();
			m_topRecruits.Add(recruitData);
			recruitData.m_recruit = recruitAFriendDataResponse.TopRecruits[i];
			if (recruitData.m_recruit.GameAccountId == null)
			{
				Log.RAF.PrintWarning("RAFManager.OnDataResponse() - GameAccountId is NULL for recruit!");
				continue;
			}
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.SetHi(recruitData.m_recruit.GameAccountId.Hi);
			bnetGameAccountId.SetLo(recruitData.m_recruit.GameAccountId.Lo);
			EntityId entityId = default(EntityId);
			entityId.hi = bnetGameAccountId.GetHi();
			entityId.lo = bnetGameAccountId.GetLo();
			List<PresenceFieldKey> list = new List<PresenceFieldKey>();
			PresenceFieldKey item = new PresenceFieldKey
			{
				programId = BnetProgramId.BNET.GetValue(),
				groupId = 2u,
				fieldId = 7u,
				uniqueId = 0uL
			};
			list.Add(item);
			item.programId = BnetProgramId.BNET.GetValue();
			item.groupId = 2u;
			item.fieldId = 3u;
			item.uniqueId = 0uL;
			list.Add(item);
			item.programId = BnetProgramId.BNET.GetValue();
			item.groupId = 2u;
			item.fieldId = 5u;
			item.uniqueId = 0uL;
			list.Add(item);
			PresenceFieldKey[] fieldList = list.ToArray();
			BattleNet.RequestPresenceFields(isGameAccountEntityId: true, entityId, fieldList);
		}
		if (m_RAFFrame != null)
		{
			if (m_totalRecruitCount != 0)
			{
				m_RAFFrame.SetProgressData(m_totalRecruitCount, m_topRecruits);
				m_RAFFrame.ShowProgressFrame();
			}
			else
			{
				m_RAFFrame.ShowHeroFrame();
			}
		}
	}

	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		if (m_topRecruits == null)
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		for (int i = 0; i < updates.Length; i++)
		{
			PresenceUpdate presenceUpdate = updates[i];
			if (presenceUpdate.programId != BnetProgramId.BNET || presenceUpdate.groupId != 2 || presenceUpdate.fieldId != 5)
			{
				continue;
			}
			BnetPlayer player = BnetUtils.GetPlayer(BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId));
			if (player == null || player == myPlayer || player.GetBattleTag() == null)
			{
				continue;
			}
			EntityId entityId = presenceUpdate.entityId;
			foreach (RecruitData topRecruit in m_topRecruits)
			{
				if (topRecruit.m_recruit.GameAccountId.Lo == entityId.lo && topRecruit.m_recruit.GameAccountId.Hi == entityId.hi)
				{
					topRecruit.m_recruitBattleTag = player.GetBattleTag().GetString();
					Log.RAF.Print("Found Battle Tag for Game Account ID: " + topRecruit.m_recruitBattleTag);
					if (m_RAFFrame != null)
					{
						m_RAFFrame.UpdateBattleTag(topRecruit.m_recruit.GameAccountId, topRecruit.m_recruitBattleTag);
					}
					break;
				}
			}
		}
	}

	private IEnumerator SendToRAFWebsiteThenHide()
	{
		m_RAFFrame.m_infoFrame.m_okayButton.SetEnabled(enabled: false);
		string recruitAFriendLink = ExternalUrlService.Get().GetRecruitAFriendLink();
		if (!string.IsNullOrEmpty(recruitAFriendLink))
		{
			Application.OpenURL(recruitAFriendLink);
		}
		m_RAFFrame.m_infoFrame.Hide();
		yield break;
	}
}
