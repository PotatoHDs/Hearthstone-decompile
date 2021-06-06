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

public class AccountLicenseMgr : IService
{
	public enum LicenseUpdateState
	{
		UNKNOWN,
		SUCCESS,
		FAIL
	}

	public delegate void AccountLicensesChangedCallback(List<AccountLicenseInfo> changedLicensesInfo, object userData);

	private class AccountLicensesChangedListener : EventListener<AccountLicensesChangedCallback>
	{
		public void Fire(List<AccountLicenseInfo> changedLicensesInfo)
		{
			m_callback(changedLicensesInfo, m_userData);
		}
	}

	private Map<long, long> m_seenLicenseNotices;

	private LicenseUpdateState m_consumableLicensesUpdateState;

	private LicenseUpdateState m_fixedLicensesUpdateState;

	private List<AccountLicensesChangedListener> m_accountLicensesChangedListeners = new List<AccountLicensesChangedListener>();

	public LicenseUpdateState FixedLicensesState => m_fixedLicensesUpdateState;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		serviceLocator.Get<Network>().RegisterNetHandler(UpdateAccountLicensesResponse.PacketID.ID, OnAccountLicensesUpdatedResponse);
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(OnNewNotices);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(Network),
			typeof(NetCache)
		};
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		if (m_seenLicenseNotices != null)
		{
			m_seenLicenseNotices.Clear();
		}
		m_consumableLicensesUpdateState = LicenseUpdateState.UNKNOWN;
		m_fixedLicensesUpdateState = LicenseUpdateState.UNKNOWN;
	}

	public static AccountLicenseMgr Get()
	{
		return HearthstoneServices.Get<AccountLicenseMgr>();
	}

	public void InitRequests()
	{
		Network.Get().RequestAccountLicensesUpdate();
	}

	public bool OwnsAccountLicense(long license)
	{
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		if (netObject == null)
		{
			return false;
		}
		if (!netObject.AccountLicenses.ContainsKey(license))
		{
			return false;
		}
		return OwnsAccountLicense(netObject.AccountLicenses[license]);
	}

	public bool OwnsAccountLicense(AccountLicenseInfo accountLicenseInfo)
	{
		if (accountLicenseInfo == null)
		{
			return false;
		}
		return (accountLicenseInfo.Flags_ & 1) == 1;
	}

	public List<AccountLicenseInfo> GetAllOwnedAccountLicenseInfo()
	{
		List<AccountLicenseInfo> list = new List<AccountLicenseInfo>();
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		if (netObject != null)
		{
			foreach (AccountLicenseInfo value in netObject.AccountLicenses.Values)
			{
				if (OwnsAccountLicense(value))
				{
					list.Add(value);
				}
			}
			return list;
		}
		return list;
	}

	public bool RegisterAccountLicensesChangedListener(AccountLicensesChangedCallback callback)
	{
		return RegisterAccountLicensesChangedListener(callback, null);
	}

	public bool RegisterAccountLicensesChangedListener(AccountLicensesChangedCallback callback, object userData)
	{
		AccountLicensesChangedListener accountLicensesChangedListener = new AccountLicensesChangedListener();
		accountLicensesChangedListener.SetCallback(callback);
		accountLicensesChangedListener.SetUserData(userData);
		if (m_accountLicensesChangedListeners.Contains(accountLicensesChangedListener))
		{
			return false;
		}
		m_accountLicensesChangedListeners.Add(accountLicensesChangedListener);
		return true;
	}

	public bool RemoveAccountLicensesChangedListener(AccountLicensesChangedCallback callback)
	{
		return RemoveAccountLicensesChangedListener(callback, null);
	}

	public bool RemoveAccountLicensesChangedListener(AccountLicensesChangedCallback callback, object userData)
	{
		AccountLicensesChangedListener accountLicensesChangedListener = new AccountLicensesChangedListener();
		accountLicensesChangedListener.SetCallback(callback);
		accountLicensesChangedListener.SetUserData(userData);
		return m_accountLicensesChangedListeners.Remove(accountLicensesChangedListener);
	}

	private void OnAccountLicensesUpdatedResponse()
	{
		UpdateAccountLicensesResponse updateAccountLicensesResponse = Network.Get().GetUpdateAccountLicensesResponse();
		m_consumableLicensesUpdateState = (updateAccountLicensesResponse.ConsumableLicenseSuccess ? LicenseUpdateState.SUCCESS : LicenseUpdateState.FAIL);
		m_fixedLicensesUpdateState = (updateAccountLicensesResponse.FixedLicenseSuccess ? LicenseUpdateState.SUCCESS : LicenseUpdateState.FAIL);
		Log.All.Print("OnAccountLicensesUpdatedResponse consumableLicensesUpdateState={0} fixedLicensesUpdateState={1}", m_consumableLicensesUpdateState, m_fixedLicensesUpdateState);
		if (LicenseUpdateState.SUCCESS != m_consumableLicensesUpdateState || LicenseUpdateState.SUCCESS != m_fixedLicensesUpdateState)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
			popupInfo.m_text = GameStrings.Get("GLOBAL_ERROR_ACCOUNT_LICENSES");
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		if (netObject == null)
		{
			Processor.RunCoroutine(OnNewNotices_WaitForNetCacheAccountLicenses(newNotices));
		}
		else
		{
			OnNewNotices_Internal(newNotices, netObject);
		}
	}

	private IEnumerator OnNewNotices_WaitForNetCacheAccountLicenses(List<NetCache.ProfileNotice> newNotices)
	{
		float startTime = Time.realtimeSinceStartup;
		NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		while (netObject == null && Time.realtimeSinceStartup - startTime < 30f)
		{
			yield return null;
			netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
		}
		OnNewNotices_Internal(newNotices, netObject);
	}

	private void OnNewNotices_Internal(List<NetCache.ProfileNotice> newNotices, NetCache.NetCacheAccountLicenses netCacheAccountLicenses)
	{
		if (netCacheAccountLicenses == null)
		{
			Debug.LogWarning("AccountLicenses.OnNewNotices netCacheAccountLicenses is null -- going to ack all ACCOUNT_LICENSE notices assuming NetCache is not yet loaded");
		}
		HashSet<long> hashSet = new HashSet<long>();
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.ACCOUNT_LICENSE != newNotice.Type)
			{
				continue;
			}
			NetCache.ProfileNoticeAcccountLicense profileNoticeAcccountLicense = newNotice as NetCache.ProfileNoticeAcccountLicense;
			if (netCacheAccountLicenses != null)
			{
				if (!netCacheAccountLicenses.AccountLicenses.ContainsKey(profileNoticeAcccountLicense.License))
				{
					netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License] = new AccountLicenseInfo
					{
						License = profileNoticeAcccountLicense.License,
						Flags_ = 0uL,
						CasId = 0L
					};
				}
				if (profileNoticeAcccountLicense.CasID >= netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].CasId)
				{
					netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].CasId = profileNoticeAcccountLicense.CasID;
					NetCache.ProfileNotice.NoticeOrigin origin = newNotice.Origin;
					if (origin == NetCache.ProfileNotice.NoticeOrigin.ACCOUNT_LICENSE_FLAGS)
					{
						netCacheAccountLicenses.AccountLicenses[profileNoticeAcccountLicense.License].Flags_ = (ulong)newNotice.OriginData;
					}
					else
					{
						Debug.LogWarning($"AccountLicenses.OnNewNotices unexpected notice origin {newNotice.Origin} (data={newNotice.OriginData}) for license {profileNoticeAcccountLicense.License} casID {profileNoticeAcccountLicense.CasID}");
					}
					long value = profileNoticeAcccountLicense.CasID - 1;
					if (m_seenLicenseNotices != null)
					{
						m_seenLicenseNotices.TryGetValue(profileNoticeAcccountLicense.License, out value);
					}
					if (value < profileNoticeAcccountLicense.CasID)
					{
						hashSet.Add(profileNoticeAcccountLicense.License);
					}
					if (m_seenLicenseNotices == null)
					{
						m_seenLicenseNotices = new Map<long, long>();
					}
					m_seenLicenseNotices[profileNoticeAcccountLicense.License] = profileNoticeAcccountLicense.CasID;
				}
			}
			Network.Get().AckNotice(newNotice.NoticeID);
		}
		if (netCacheAccountLicenses == null)
		{
			return;
		}
		List<AccountLicenseInfo> list = new List<AccountLicenseInfo>();
		foreach (long item in hashSet)
		{
			if (netCacheAccountLicenses.AccountLicenses.ContainsKey(item))
			{
				list.Add(netCacheAccountLicenses.AccountLicenses[item]);
			}
		}
		if (list.Count != 0)
		{
			AccountLicensesChangedListener[] array = m_accountLicensesChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(list);
			}
		}
	}
}
