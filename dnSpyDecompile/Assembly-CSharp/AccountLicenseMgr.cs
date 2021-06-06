using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000841 RID: 2113
public class AccountLicenseMgr : IService
{
	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x060070AA RID: 28842 RVA: 0x00245203 File Offset: 0x00243403
	public AccountLicenseMgr.LicenseUpdateState FixedLicensesState
	{
		get
		{
			return this.m_fixedLicensesUpdateState;
		}
	}

	// Token: 0x060070AB RID: 28843 RVA: 0x0024520B File Offset: 0x0024340B
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		serviceLocator.Get<Network>().RegisterNetHandler(UpdateAccountLicensesResponse.PacketID.ID, new Network.NetHandler(this.OnAccountLicensesUpdatedResponse), null);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		yield break;
	}

	// Token: 0x060070AC RID: 28844 RVA: 0x00245221 File Offset: 0x00243421
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache)
		};
	}

	// Token: 0x060070AD RID: 28845 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060070AE RID: 28846 RVA: 0x00245243 File Offset: 0x00243443
	private void WillReset()
	{
		if (this.m_seenLicenseNotices != null)
		{
			this.m_seenLicenseNotices.Clear();
		}
		this.m_consumableLicensesUpdateState = AccountLicenseMgr.LicenseUpdateState.UNKNOWN;
		this.m_fixedLicensesUpdateState = AccountLicenseMgr.LicenseUpdateState.UNKNOWN;
	}

	// Token: 0x060070AF RID: 28847 RVA: 0x00245266 File Offset: 0x00243466
	public static AccountLicenseMgr Get()
	{
		return HearthstoneServices.Get<AccountLicenseMgr>();
	}

	// Token: 0x060070B0 RID: 28848 RVA: 0x0024526D File Offset: 0x0024346D
	public void InitRequests()
	{
		Network.Get().RequestAccountLicensesUpdate();
	}

	// Token: 0x060070B1 RID: 28849 RVA: 0x0024527C File Offset: 0x0024347C
	public bool OwnsAccountLicense(long license)
	{
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		return netObject != null && netObject.AccountLicenses.ContainsKey(license) && this.OwnsAccountLicense(netObject.AccountLicenses[license]);
	}

	// Token: 0x060070B2 RID: 28850 RVA: 0x002452BB File Offset: 0x002434BB
	public bool OwnsAccountLicense(AccountLicenseInfo accountLicenseInfo)
	{
		return accountLicenseInfo != null && (accountLicenseInfo.Flags_ & 1UL) == 1UL;
	}

	// Token: 0x060070B3 RID: 28851 RVA: 0x002452D0 File Offset: 0x002434D0
	public List<AccountLicenseInfo> GetAllOwnedAccountLicenseInfo()
	{
		List<AccountLicenseInfo> list = new List<AccountLicenseInfo>();
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		if (netObject != null)
		{
			foreach (AccountLicenseInfo accountLicenseInfo in netObject.AccountLicenses.Values)
			{
				if (this.OwnsAccountLicense(accountLicenseInfo))
				{
					list.Add(accountLicenseInfo);
				}
			}
		}
		return list;
	}

	// Token: 0x060070B4 RID: 28852 RVA: 0x00245348 File Offset: 0x00243548
	public bool RegisterAccountLicensesChangedListener(AccountLicenseMgr.AccountLicensesChangedCallback callback)
	{
		return this.RegisterAccountLicensesChangedListener(callback, null);
	}

	// Token: 0x060070B5 RID: 28853 RVA: 0x00245354 File Offset: 0x00243554
	public bool RegisterAccountLicensesChangedListener(AccountLicenseMgr.AccountLicensesChangedCallback callback, object userData)
	{
		AccountLicenseMgr.AccountLicensesChangedListener accountLicensesChangedListener = new AccountLicenseMgr.AccountLicensesChangedListener();
		accountLicensesChangedListener.SetCallback(callback);
		accountLicensesChangedListener.SetUserData(userData);
		if (this.m_accountLicensesChangedListeners.Contains(accountLicensesChangedListener))
		{
			return false;
		}
		this.m_accountLicensesChangedListeners.Add(accountLicensesChangedListener);
		return true;
	}

	// Token: 0x060070B6 RID: 28854 RVA: 0x00245392 File Offset: 0x00243592
	public bool RemoveAccountLicensesChangedListener(AccountLicenseMgr.AccountLicensesChangedCallback callback)
	{
		return this.RemoveAccountLicensesChangedListener(callback, null);
	}

	// Token: 0x060070B7 RID: 28855 RVA: 0x0024539C File Offset: 0x0024359C
	public bool RemoveAccountLicensesChangedListener(AccountLicenseMgr.AccountLicensesChangedCallback callback, object userData)
	{
		AccountLicenseMgr.AccountLicensesChangedListener accountLicensesChangedListener = new AccountLicenseMgr.AccountLicensesChangedListener();
		accountLicensesChangedListener.SetCallback(callback);
		accountLicensesChangedListener.SetUserData(userData);
		return this.m_accountLicensesChangedListeners.Remove(accountLicensesChangedListener);
	}

	// Token: 0x060070B8 RID: 28856 RVA: 0x002453CC File Offset: 0x002435CC
	private void OnAccountLicensesUpdatedResponse()
	{
		UpdateAccountLicensesResponse updateAccountLicensesResponse = Network.Get().GetUpdateAccountLicensesResponse();
		this.m_consumableLicensesUpdateState = (updateAccountLicensesResponse.ConsumableLicenseSuccess ? AccountLicenseMgr.LicenseUpdateState.SUCCESS : AccountLicenseMgr.LicenseUpdateState.FAIL);
		this.m_fixedLicensesUpdateState = (updateAccountLicensesResponse.FixedLicenseSuccess ? AccountLicenseMgr.LicenseUpdateState.SUCCESS : AccountLicenseMgr.LicenseUpdateState.FAIL);
		Log.All.Print("OnAccountLicensesUpdatedResponse consumableLicensesUpdateState={0} fixedLicensesUpdateState={1}", new object[]
		{
			this.m_consumableLicensesUpdateState,
			this.m_fixedLicensesUpdateState
		});
		if (AccountLicenseMgr.LicenseUpdateState.SUCCESS == this.m_consumableLicensesUpdateState && AccountLicenseMgr.LicenseUpdateState.SUCCESS == this.m_fixedLicensesUpdateState)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_ERROR_ACCOUNT_LICENSES");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060070B9 RID: 28857 RVA: 0x0024548C File Offset: 0x0024368C
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		if (netObject == null)
		{
			Processor.RunCoroutine(this.OnNewNotices_WaitForNetCacheAccountLicenses(newNotices), null);
			return;
		}
		this.OnNewNotices_Internal(newNotices, netObject);
	}

	// Token: 0x060070BA RID: 28858 RVA: 0x002454BE File Offset: 0x002436BE
	private IEnumerator OnNewNotices_WaitForNetCacheAccountLicenses(List<NetCache.ProfileNotice> newNotices)
	{
		float startTime = Time.realtimeSinceStartup;
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		while (netObject == null && Time.realtimeSinceStartup - startTime < 30f)
		{
			yield return null;
			netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		}
		this.OnNewNotices_Internal(newNotices, netObject);
		yield break;
	}

	// Token: 0x060070BB RID: 28859 RVA: 0x002454D4 File Offset: 0x002436D4
	private void OnNewNotices_Internal(List<NetCache.ProfileNotice> newNotices, NetCache.NetCacheAccountLicenses netCacheAccountLicenses)
	{
		if (netCacheAccountLicenses == null)
		{
			Debug.LogWarning("AccountLicenses.OnNewNotices netCacheAccountLicenses is null -- going to ack all ACCOUNT_LICENSE notices assuming NetCache is not yet loaded");
		}
		HashSet<long> hashSet = new HashSet<long>();
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.ACCOUNT_LICENSE == profileNotice.Type)
			{
				NetCache.ProfileNoticeAcccountLicense profileNoticeAcccountLicense = profileNotice as NetCache.ProfileNoticeAcccountLicense;
				if (netCacheAccountLicenses != null)
				{
					if (!netCacheAccountLicenses.AccountLicenses.ContainsKey(profileNoticeAcccountLicense.License))
					{
						netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License] = new AccountLicenseInfo
						{
							License = profileNoticeAcccountLicense.License,
							Flags_ = 0UL,
							CasId = 0L
						};
					}
					if (profileNoticeAcccountLicense.CasID >= netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].CasId)
					{
						netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].CasId = profileNoticeAcccountLicense.CasID;
						NetCache.ProfileNotice.NoticeOrigin origin = profileNotice.Origin;
						if (origin == NetCache.ProfileNotice.NoticeOrigin.ACCOUNT_LICENSE_FLAGS)
						{
							netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].Flags_ = (ulong)profileNotice.OriginData;
						}
						else
						{
							Debug.LogWarning(string.Format("AccountLicenses.OnNewNotices unexpected notice origin {0} (data={1}) for license {2} casID {3}", new object[]
							{
								profileNotice.Origin,
								profileNotice.OriginData,
								profileNoticeAcccountLicense.License,
								profileNoticeAcccountLicense.CasID
							}));
						}
						long num = profileNoticeAcccountLicense.CasID - 1L;
						if (this.m_seenLicenseNotices != null)
						{
							this.m_seenLicenseNotices.TryGetValue(profileNoticeAcccountLicense.License, out num);
						}
						if (num < profileNoticeAcccountLicense.CasID)
						{
							hashSet.Add(profileNoticeAcccountLicense.License);
						}
						if (this.m_seenLicenseNotices == null)
						{
							this.m_seenLicenseNotices = new Map<long, long>();
						}
						this.m_seenLicenseNotices[profileNoticeAcccountLicense.License] = profileNoticeAcccountLicense.CasID;
					}
				}
				Network.Get().AckNotice(profileNotice.NoticeID);
			}
		}
		if (netCacheAccountLicenses == null)
		{
			return;
		}
		List<AccountLicenseInfo> list = new List<AccountLicenseInfo>();
		foreach (long key in hashSet)
		{
			if (netCacheAccountLicenses.AccountLicenses.ContainsKey(key))
			{
				list.Add(netCacheAccountLicenses.AccountLicenses[key]);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		AccountLicenseMgr.AccountLicensesChangedListener[] array = this.m_accountLicensesChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(list);
		}
	}

	// Token: 0x04005A74 RID: 23156
	private Map<long, long> m_seenLicenseNotices;

	// Token: 0x04005A75 RID: 23157
	private AccountLicenseMgr.LicenseUpdateState m_consumableLicensesUpdateState;

	// Token: 0x04005A76 RID: 23158
	private AccountLicenseMgr.LicenseUpdateState m_fixedLicensesUpdateState;

	// Token: 0x04005A77 RID: 23159
	private List<AccountLicenseMgr.AccountLicensesChangedListener> m_accountLicensesChangedListeners = new List<AccountLicenseMgr.AccountLicensesChangedListener>();

	// Token: 0x02002413 RID: 9235
	public enum LicenseUpdateState
	{
		// Token: 0x0400E93E RID: 59710
		UNKNOWN,
		// Token: 0x0400E93F RID: 59711
		SUCCESS,
		// Token: 0x0400E940 RID: 59712
		FAIL
	}

	// Token: 0x02002414 RID: 9236
	// (Invoke) Token: 0x06012E27 RID: 77351
	public delegate void AccountLicensesChangedCallback(List<AccountLicenseInfo> changedLicensesInfo, object userData);

	// Token: 0x02002415 RID: 9237
	private class AccountLicensesChangedListener : EventListener<AccountLicenseMgr.AccountLicensesChangedCallback>
	{
		// Token: 0x06012E2A RID: 77354 RVA: 0x0051EFDC File Offset: 0x0051D1DC
		public void Fire(List<AccountLicenseInfo> changedLicensesInfo)
		{
			this.m_callback(changedLicensesInfo, this.m_userData);
		}
	}
}
