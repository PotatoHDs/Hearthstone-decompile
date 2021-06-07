using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blizzard.MobileAuth
{
	public static class MobileAuth
	{
		internal interface IMobileAuthPlatform
		{
			bool IsAuthenticated { get; }

			void Configure(Configuration configuration);

			void PresentLogin(IAuthenticationDelegate authenticationDelegate);

			void Logout();

			Account? GetAuthenticatedAccount();

			SuggestedAccount? GetSuggestedAccount();

			void ContinueAsExplicit(string regionCode, SuggestedAccount suggestedAccount, IAuthenticationDelegate authenticationDelegate);

			void ContinueAsImplicit(SuggestedAccount suggestedAccount, IAuthenticationDelegate authenticationDelegate);

			void AuthenticateAccount(Account account, string regionID, IAuthenticationDelegate authenticationDelegate);

			void GenerateSingleUseSsoUrl(Account account, string targetWebAppUrl, IWebSsoListener listener);

			void GenerateSingleUseSsoToken(Account account, IWebSsoTokenCallback callback);

			void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate);

			Region[] BuiltInRegions();

			void ImportAccount(ImportAccount importAccount, IImportAccountCallback importAccountCallback);

			void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate);

			void GetRegionFromGeoIp(IRegionListener regionListener);

			void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener);

			List<Account> GetSoftAccounts();

			List<Account> GetAllAccounts();

			void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate);

			void PresentMergeAccount(Account account, IAuthenticationDelegate authenticationDelegate);

			void GetBattleTagInfo(Account account, IBattleTagInfoListener listener);

			void SetBattleTag(Account account, string newBattleTag, IBattleTagChangeListener listener);

			void RemoveAccountById(string accountId, IOnAccountRemovedListener listener);

			Account? GetAccountById(string accountId);
		}

		internal static readonly IMobileAuthPlatform Platform;

		internal static BlzMobileAuthGameObject GameObjectComponent;

		internal static CallbackManager callbackManager;

		public static bool IsAuthenticated => Platform.IsAuthenticated;

		internal static string GameObjectName => "BlzMobileAuthGameObject";

		internal static IBlzCallbackService CallbackService
		{
			get
			{
				if (GameObjectComponent != null)
				{
					return GameObjectComponent;
				}
				return null;
			}
		}

		internal static IBlzAuthCallbackHandler AuthCallbackHandler
		{
			get
			{
				if (GameObjectComponent != null)
				{
					return GameObjectComponent;
				}
				return null;
			}
		}

		public static void Configure(Configuration configuration)
		{
			Platform.Configure(configuration);
			InitializeBlzMobileAuthGameObject();
		}

		public static void PresentLogin(IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.PresentLogin(authenticationDelegate);
		}

		public static void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.PresentChallenge(url, authenticationDelegate);
		}

		public static Region[] BuiltInRegions()
		{
			return Platform.BuiltInRegions();
		}

		public static void Logout()
		{
			Platform.Logout();
		}

		public static Account? GetAuthenticatedAccount()
		{
			return Platform.GetAuthenticatedAccount();
		}

		public static void ImportAccount(ImportAccount importAccount, IImportAccountCallback importAccountCallback)
		{
			callbackManager.SetImportAccountCallback(importAccountCallback);
			Platform.ImportAccount(importAccount, importAccountCallback);
		}

		public static SuggestedAccount? GetSuggestedAccount()
		{
			return Platform.GetSuggestedAccount();
		}

		public static void ContinueAsExplicit(string regionCode, SuggestedAccount suggestedAccount, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.ContinueAsExplicit(regionCode, suggestedAccount, authenticationDelegate);
		}

		public static void ContinueAsImplicit(SuggestedAccount suggestedAccount, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.ContinueAsImplicit(suggestedAccount, authenticationDelegate);
		}

		public static void AuthenticateAccount(Account account, string regionID, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.AuthenticateAccount(account, regionID, authenticationDelegate);
		}

		public static void GenerateSingleUseSsoUrl(Account account, string targetWebAppUrl, IWebSsoListener listener)
		{
			callbackManager.SetWebSsoListener(listener);
			Platform.GenerateSingleUseSsoUrl(account, targetWebAppUrl, listener);
		}

		public static void GenerateSingleUseSsoToken(Account account, IWebSsoTokenCallback callback)
		{
			callbackManager.SetWebSsoTokenCallback(callback);
			Platform.GenerateSingleUseSsoToken(account, callback);
		}

		public static void GetRegionFromGeoIp(IRegionListener regionListener)
		{
			callbackManager.SetRegionListener(regionListener);
			Platform.GetRegionFromGeoIp(regionListener);
		}

		public static void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.AuthenticateSoftAccount(softAccountId, authenticationDelegate);
		}

		public static void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener)
		{
			callbackManager.SetGuestSoftAccountIdListener(listener);
			Platform.GenerateGuestSoftAccountId(region, listener);
		}

		public static List<Account> GetSoftAccounts()
		{
			return Platform.GetSoftAccounts();
		}

		public static List<Account> GetAllAccounts()
		{
			return Platform.GetAllAccounts();
		}

		public static void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.PresentHealUpSoftAccount(account, authenticationDelegate);
		}

		public static void PresentMergeAccount(Account account, IAuthenticationDelegate authenticationDelegate)
		{
			callbackManager.SetAuthDelegate(authenticationDelegate);
			Platform.PresentMergeAccount(account, authenticationDelegate);
		}

		public static void GetBattleTagInfo(Account account, IBattleTagInfoListener listener)
		{
			callbackManager.SetBattleTagInfoListener(listener);
			Platform.GetBattleTagInfo(account, listener);
		}

		public static void SetBattleTag(Account account, string newBattleTag, IBattleTagChangeListener listener)
		{
			callbackManager.SetBattleTagChangeListener(listener);
			Platform.SetBattleTag(account, newBattleTag, listener);
		}

		public static void RemoveAccountById(string accountId, IOnAccountRemovedListener listener)
		{
			callbackManager.SetOnAccountRemovedListener(listener);
			Platform.RemoveAccountById(accountId, listener);
		}

		public static Account? GetAccountById(string accountId)
		{
			return Platform.GetAccountById(accountId);
		}

		private static void InitializeBlzMobileAuthGameObject()
		{
			GameObjectComponent = new GameObject(GameObjectName).AddComponent<BlzMobileAuthGameObject>();
		}

		static MobileAuth()
		{
			callbackManager = new CallbackManager();
			if (!Application.isEditor)
			{
				Platform = null;
				throw new ApplicationException("Platform not supported");
			}
		}
	}
}
