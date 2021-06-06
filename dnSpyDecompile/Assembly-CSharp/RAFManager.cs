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

// Token: 0x0200063F RID: 1599
public class RAFManager : IService
{
	// Token: 0x06005A18 RID: 23064 RVA: 0x001D69C5 File Offset: 0x001D4BC5
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(ProcessRecruitAFriendResponse.PacketID.ID, new Network.NetHandler(this.OnProcessRecruitResponse), null);
		network.RegisterNetHandler(RecruitAFriendURLResponse.PacketID.ID, new Network.NetHandler(this.OnURLResponse), null);
		network.RegisterNetHandler(RecruitAFriendDataResponse.PacketID.ID, new Network.NetHandler(this.OnDataResponse), null);
		HearthstoneApplication.Get().WillReset += this.WillReset;
		yield break;
	}

	// Token: 0x06005A19 RID: 23065 RVA: 0x001D69DB File Offset: 0x001D4BDB
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(SoundManager)
		};
	}

	// Token: 0x06005A1A RID: 23066 RVA: 0x001D6A00 File Offset: 0x001D4C00
	public void Shutdown()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.WillReset;
		}
		Network network = null;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.RemoveNetHandler(ProcessRecruitAFriendResponse.PacketID.ID, new Network.NetHandler(this.OnProcessRecruitResponse));
			network.RemoveNetHandler(RecruitAFriendURLResponse.PacketID.ID, new Network.NetHandler(this.OnURLResponse));
			network.RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, new Network.NetHandler(this.OnDataResponse));
		}
	}

	// Token: 0x06005A1B RID: 23067 RVA: 0x001D6A90 File Offset: 0x001D4C90
	public static RAFManager Get()
	{
		return HearthstoneServices.Get<RAFManager>();
	}

	// Token: 0x06005A1C RID: 23068 RVA: 0x001D6A98 File Offset: 0x001D4C98
	public void WillReset()
	{
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
		this.m_RAFFrame = null;
		this.m_rafDisplayURL = null;
		this.m_rafFullURL = null;
		this.m_hasRAFData = false;
		this.m_totalRecruitCount = 0U;
		this.m_topRecruits = null;
	}

	// Token: 0x06005A1D RID: 23069 RVA: 0x001D6AE5 File Offset: 0x001D4CE5
	public void InitializeRequests()
	{
		Network.Get().RequestProcessRecruitAFriend();
	}

	// Token: 0x06005A1E RID: 23070 RVA: 0x001D6AF4 File Offset: 0x001D4CF4
	public void ShowRAFFrame()
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (!this.m_hasRAFData)
		{
			global::Log.RAF.Print("Network.RequestRecruitAFriendData", Array.Empty<object>());
			Network.Get().RequestRecruitAFriendData();
		}
		Processor.CancelCoroutine(this.ShowRAFFrameWhenReady());
		Processor.RunCoroutine(this.ShowRAFFrameWhenReady(), null);
	}

	// Token: 0x06005A1F RID: 23071 RVA: 0x001D6B53 File Offset: 0x001D4D53
	public RAFFrame GetRAFFrame()
	{
		return this.m_RAFFrame;
	}

	// Token: 0x06005A20 RID: 23072 RVA: 0x001D6B5B File Offset: 0x001D4D5B
	public void ShowRAFHeroFrame()
	{
		if (this.m_RAFFrame != null)
		{
			this.m_RAFFrame.ShowHeroFrame();
		}
	}

	// Token: 0x06005A21 RID: 23073 RVA: 0x001D6B76 File Offset: 0x001D4D76
	public void ShowRAFProgressFrame()
	{
		if (this.m_RAFFrame != null)
		{
			this.m_RAFFrame.ShowProgressFrame();
		}
	}

	// Token: 0x06005A22 RID: 23074 RVA: 0x001D6B91 File Offset: 0x001D4D91
	public void SetRAFProgress(int progress)
	{
		if (this.m_RAFFrame != null)
		{
			this.m_RAFFrame.SetProgress(progress);
		}
	}

	// Token: 0x06005A23 RID: 23075 RVA: 0x001D6BAD File Offset: 0x001D4DAD
	public string GetRecruitDisplayURL()
	{
		if (this.m_rafDisplayURL != null)
		{
			return this.m_rafDisplayURL;
		}
		global::Log.RAF.Print("Network.RequestRecruitAFriendURL", Array.Empty<object>());
		Network.Get().RequestRecruitAFriendUrl();
		return null;
	}

	// Token: 0x06005A24 RID: 23076 RVA: 0x001D6BDD File Offset: 0x001D4DDD
	public string GetRecruitFullURL()
	{
		if (this.m_rafFullURL != null)
		{
			return this.m_rafFullURL;
		}
		return null;
	}

	// Token: 0x06005A25 RID: 23077 RVA: 0x001D6BEF File Offset: 0x001D4DEF
	public void GotoRAFWebsite()
	{
		Processor.CancelCoroutine(this.SendToRAFWebsiteThenHide());
		Processor.RunCoroutine(this.SendToRAFWebsiteThenHide(), null);
	}

	// Token: 0x06005A26 RID: 23078 RVA: 0x001D6C09 File Offset: 0x001D4E09
	public uint GetTotalRecruitCount()
	{
		return this.m_totalRecruitCount;
	}

	// Token: 0x06005A27 RID: 23079 RVA: 0x001D6C11 File Offset: 0x001D4E11
	private IEnumerator ShowRAFFrameWhenReady()
	{
		if (this.m_RAFFrame == null && !this.m_isRAFLoading)
		{
			this.m_isRAFLoading = true;
			AssetLoader.Get().InstantiatePrefab("RAF_main.prefab:5fa2642eb52ae469dbe27e96a7570e08", new PrefabCallback<GameObject>(this.OnRAFLoaded), null, AssetLoadingOptions.None);
		}
		while (this.m_RAFFrame == null)
		{
			yield return null;
		}
		while (!this.m_hasRAFData)
		{
			yield return null;
		}
		this.m_RAFFrame.Show();
		ChatMgr.Get().CloseFriendsList();
		yield break;
	}

	// Token: 0x06005A28 RID: 23080 RVA: 0x001D6C20 File Offset: 0x001D4E20
	private void OnRAFLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_isRAFLoading = false;
		if (go == null)
		{
			global::Log.RAF.PrintError("RAFManager.OnRAFLoaded() - FAILED to load RAFManager GameObject", Array.Empty<object>());
			return;
		}
		this.m_RAFFrame = go.GetComponent<RAFFrame>();
		if (this.m_RAFFrame == null)
		{
			global::Log.RAF.PrintError("RAFManager.OnRAFLoaded() - ERROR RAFManager GameObject has no " + typeof(RAFFrame) + " component", Array.Empty<object>());
			return;
		}
		if (this.m_hasRAFData)
		{
			if (this.m_totalRecruitCount > 0U)
			{
				this.m_RAFFrame.SetProgressData(this.m_totalRecruitCount, this.m_topRecruits);
				this.m_RAFFrame.ShowProgressFrame();
				return;
			}
			this.m_RAFFrame.ShowHeroFrame();
		}
	}

	// Token: 0x06005A29 RID: 23081 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnProcessRecruitResponse()
	{
	}

	// Token: 0x06005A2A RID: 23082 RVA: 0x001D6CD4 File Offset: 0x001D4ED4
	private void OnURLResponse()
	{
		RecruitAFriendURLResponse recruitAFriendUrlResponse = Network.Get().GetRecruitAFriendUrlResponse();
		if (recruitAFriendUrlResponse == null || recruitAFriendUrlResponse.RafServiceStatus == RAFServiceStatus.RAFServiceStatus_NotAvailable || string.IsNullOrEmpty(recruitAFriendUrlResponse.RafUrl))
		{
			string text = "RAFManager.OnURLResponse() - Response not valid!";
			if (recruitAFriendUrlResponse != null)
			{
				text += ((string.Concat(new object[]
				{
					" ",
					recruitAFriendUrlResponse.RafServiceStatus,
					", ",
					recruitAFriendUrlResponse.RafUrl
				}) == null) ? "null" : recruitAFriendUrlResponse.RafUrl);
			}
			global::Log.RAF.PrintError(text, Array.Empty<object>());
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_RAF_ERROR_HEADER"),
				m_showAlertIcon = true,
				m_text = GameStrings.Get("GLUE_RAF_ERROR_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = null
			};
			DialogManager.Get().ShowPopup(info);
			return;
		}
		this.m_rafDisplayURL = recruitAFriendUrlResponse.RafUrl;
		global::Log.RAF.Print("Recruit URL = " + this.m_rafDisplayURL, Array.Empty<object>());
		if (this.m_RAFFrame != null)
		{
			this.m_rafFullURL = recruitAFriendUrlResponse.RafUrlFull;
			this.m_RAFFrame.ShowLinkFrame(this.m_rafDisplayURL, this.m_rafFullURL);
		}
	}

	// Token: 0x06005A2B RID: 23083 RVA: 0x001D6E0C File Offset: 0x001D500C
	private void OnDataResponse()
	{
		RecruitAFriendDataResponse recruitAFriendDataResponse = Network.Get().GetRecruitAFriendDataResponse();
		if (recruitAFriendDataResponse == null)
		{
			global::Log.RAF.PrintError("RAFManager.OnDataResponse() - Recruit Data is NULL!", Array.Empty<object>());
			return;
		}
		this.m_hasRAFData = true;
		this.m_totalRecruitCount = recruitAFriendDataResponse.TotalRecruitCount;
		this.m_topRecruits = new List<RAFManager.RecruitData>();
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += this.OnPresenceChanged;
		for (int i = 0; i < recruitAFriendDataResponse.TopRecruits.Count; i++)
		{
			RAFManager.RecruitData recruitData = new RAFManager.RecruitData();
			this.m_topRecruits.Add(recruitData);
			recruitData.m_recruit = recruitAFriendDataResponse.TopRecruits[i];
			if (recruitData.m_recruit.GameAccountId == null)
			{
				global::Log.RAF.PrintWarning("RAFManager.OnDataResponse() - GameAccountId is NULL for recruit!", Array.Empty<object>());
			}
			else
			{
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
					groupId = 2U,
					fieldId = 7U,
					uniqueId = 0UL
				};
				list.Add(item);
				item.programId = BnetProgramId.BNET.GetValue();
				item.groupId = 2U;
				item.fieldId = 3U;
				item.uniqueId = 0UL;
				list.Add(item);
				item.programId = BnetProgramId.BNET.GetValue();
				item.groupId = 2U;
				item.fieldId = 5U;
				item.uniqueId = 0UL;
				list.Add(item);
				PresenceFieldKey[] fieldList = list.ToArray();
				BattleNet.RequestPresenceFields(true, entityId, fieldList);
			}
		}
		if (this.m_RAFFrame != null)
		{
			if (this.m_totalRecruitCount > 0U)
			{
				this.m_RAFFrame.SetProgressData(this.m_totalRecruitCount, this.m_topRecruits);
				this.m_RAFFrame.ShowProgressFrame();
				return;
			}
			this.m_RAFFrame.ShowHeroFrame();
		}
	}

	// Token: 0x06005A2C RID: 23084 RVA: 0x001D703C File Offset: 0x001D523C
	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		if (this.m_topRecruits == null)
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		foreach (PresenceUpdate presenceUpdate in updates)
		{
			if (!(presenceUpdate.programId != BnetProgramId.BNET) && presenceUpdate.groupId == 2U && presenceUpdate.fieldId == 5U)
			{
				BnetPlayer player = BnetUtils.GetPlayer(BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId));
				if (player != null && player != myPlayer && !(player.GetBattleTag() == null))
				{
					EntityId entityId = presenceUpdate.entityId;
					foreach (RAFManager.RecruitData recruitData in this.m_topRecruits)
					{
						if (recruitData.m_recruit.GameAccountId.Lo == entityId.lo && recruitData.m_recruit.GameAccountId.Hi == entityId.hi)
						{
							recruitData.m_recruitBattleTag = player.GetBattleTag().GetString();
							global::Log.RAF.Print("Found Battle Tag for Game Account ID: " + recruitData.m_recruitBattleTag, Array.Empty<object>());
							if (this.m_RAFFrame != null)
							{
								this.m_RAFFrame.UpdateBattleTag(recruitData.m_recruit.GameAccountId, recruitData.m_recruitBattleTag);
								break;
							}
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06005A2D RID: 23085 RVA: 0x001D71C0 File Offset: 0x001D53C0
	private IEnumerator SendToRAFWebsiteThenHide()
	{
		this.m_RAFFrame.m_infoFrame.m_okayButton.SetEnabled(false, false);
		string recruitAFriendLink = ExternalUrlService.Get().GetRecruitAFriendLink();
		if (!string.IsNullOrEmpty(recruitAFriendLink))
		{
			Application.OpenURL(recruitAFriendLink);
		}
		this.m_RAFFrame.m_infoFrame.Hide();
		yield break;
	}

	// Token: 0x04004D1E RID: 19742
	private bool m_isRAFLoading;

	// Token: 0x04004D1F RID: 19743
	private RAFFrame m_RAFFrame;

	// Token: 0x04004D20 RID: 19744
	private string m_rafDisplayURL;

	// Token: 0x04004D21 RID: 19745
	private string m_rafFullURL;

	// Token: 0x04004D22 RID: 19746
	private bool m_hasRAFData;

	// Token: 0x04004D23 RID: 19747
	private uint m_totalRecruitCount;

	// Token: 0x04004D24 RID: 19748
	private List<RAFManager.RecruitData> m_topRecruits;

	// Token: 0x04004D25 RID: 19749
	public const int MAX_RECRUITS_SHOWN = 5;

	// Token: 0x04004D26 RID: 19750
	public const int MAX_PROGRESS_LEVEL = 20;

	// Token: 0x04004D27 RID: 19751
	public const int REWARDED_HERO_ID = 17;

	// Token: 0x02002146 RID: 8518
	public class RecruitData
	{
		// Token: 0x0400DFC4 RID: 57284
		public PegasusUtil.RecruitData m_recruit;

		// Token: 0x0400DFC5 RID: 57285
		public string m_recruitBattleTag;
	}
}
