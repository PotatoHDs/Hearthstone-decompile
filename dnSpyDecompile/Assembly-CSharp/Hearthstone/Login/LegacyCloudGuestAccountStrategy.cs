using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001138 RID: 4408
	public class LegacyCloudGuestAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600C102 RID: 49410 RVA: 0x003AAFB6 File Offset: 0x003A91B6
		// (set) Token: 0x0600C103 RID: 49411 RVA: 0x003AAFBE File Offset: 0x003A91BE
		private ILegacyGuestAccountStorage AccountStorage { get; set; }

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600C104 RID: 49412 RVA: 0x003AAFC7 File Offset: 0x003A91C7
		// (set) Token: 0x0600C105 RID: 49413 RVA: 0x003AAFCF File Offset: 0x003A91CF
		private ISwitchAccountMenuController SwitchAccountController { get; set; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600C106 RID: 49414 RVA: 0x003AAFD8 File Offset: 0x003A91D8
		// (set) Token: 0x0600C107 RID: 49415 RVA: 0x003AAFE0 File Offset: 0x003A91E0
		private ILogger Logger { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600C108 RID: 49416 RVA: 0x003AAFE9 File Offset: 0x003A91E9
		// (set) Token: 0x0600C109 RID: 49417 RVA: 0x003AAFF1 File Offset: 0x003A91F1
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600C10A RID: 49418 RVA: 0x003AAFFA File Offset: 0x003A91FA
		// (set) Token: 0x0600C10B RID: 49419 RVA: 0x003AB002 File Offset: 0x003A9202
		private TokenPromise Promise { get; set; }

		// Token: 0x0600C10C RID: 49420 RVA: 0x003AB00B File Offset: 0x003A920B
		public LegacyCloudGuestAccountStrategy(ILegacyGuestAccountStorage accountStorgage, ISwitchAccountMenuController switchAccountMenuController, ILogger logger, ITelemetryClient telemetryClient)
		{
			this.AccountStorage = accountStorgage;
			this.SwitchAccountController = switchAccountMenuController;
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C10D RID: 49421 RVA: 0x003AB030 File Offset: 0x003A9230
		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			return !this.m_hasAttemptedImport;
		}

		// Token: 0x0600C10E RID: 49422 RVA: 0x003AB03C File Offset: 0x003A923C
		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			this.Promise = promise;
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Starting Legacy guest account import job", Array.Empty<object>());
			}
			Processor.QueueJob("ImportAndSelectGuestAccount", this.Job_ImportAndPresentOptions(parameters.MobileAuth), Array.Empty<IJobDependency>());
		}

		// Token: 0x0600C10F RID: 49423 RVA: 0x003AB089 File Offset: 0x003A9289
		private IEnumerator<IAsyncJobResult> Job_ImportAndPresentOptions(IMobileAuth mobileAuth)
		{
			this.m_hasAttemptedImport = true;
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Starting Import and Present Options", Array.Empty<object>());
			}
			yield return new LegacyCloudGuestAccountStrategy.WaitForCloudInitilization(this.Logger);
			IEnumerable<GuestAccountInfo> storedGuestAccounts = this.AccountStorage.GetStoredGuestAccounts();
			if (storedGuestAccounts != null && storedGuestAccounts.Count<GuestAccountInfo>() == 0)
			{
				ILogger logger2 = this.Logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Information, "No soft accounts found to import, failing the legacy guest account import", Array.Empty<object>());
				}
				this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
				yield break;
			}
			yield return new LegacyCloudGuestAccountStrategy.ImportCloudAccounts(this.AccountStorage, mobileAuth, this.Logger, this.TelemetryClient);
			string selectedGuestAccountId = this.AccountStorage.GetSelectedGuestAccountId();
			this.AccountStorage.ClearGuestAccounts();
			List<Account> softAccounts = mobileAuth.GetSoftAccounts();
			ILogger logger3 = this.Logger;
			if (logger3 != null)
			{
				logger3.Log(LogLevel.Information, "Got {0} soft accounts from mobile auth", new object[]
				{
					(softAccounts != null) ? new int?(softAccounts.Count) : null
				});
			}
			if (softAccounts.Count == 0)
			{
				ILogger logger4 = this.Logger;
				if (logger4 != null)
				{
					logger4.Log(LogLevel.Information, "No soft accounts found, failing the legacy guest account import", Array.Empty<object>());
				}
				this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
				yield break;
			}
			foreach (Account account in softAccounts)
			{
				if (account.bnetGuestId.Equals(selectedGuestAccountId))
				{
					ILogger logger5 = this.Logger;
					if (logger5 != null)
					{
						logger5.Log(LogLevel.Information, "Found a selected guest account, authenticating it", Array.Empty<object>());
					}
					this.AuthenticateAccount(mobileAuth, account);
					yield break;
				}
			}
			this.ShowAccountSelector(mobileAuth, softAccounts);
			yield break;
		}

		// Token: 0x0600C110 RID: 49424 RVA: 0x003AB0A0 File Offset: 0x003A92A0
		private void ShowAccountSelector(IMobileAuth mobileAuth, List<Account> authAccounts)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Showing switch account selection for {0} accounts", new object[]
				{
					authAccounts.Count
				});
			}
			this.SwitchAccountController.ShowSwitchAccount(authAccounts, delegate(Account? selectedAccount)
			{
				if (selectedAccount == null)
				{
					ILogger logger2 = this.Logger;
					if (logger2 != null)
					{
						logger2.Log(LogLevel.Information, "Login chosen for switch account. Failing so we can show login", Array.Empty<object>());
					}
					this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
					return;
				}
				this.AuthenticateAccount(mobileAuth, selectedAccount.Value);
			});
		}

		// Token: 0x0600C111 RID: 49425 RVA: 0x003AB104 File Offset: 0x003A9304
		private void AuthenticateAccount(IMobileAuth mobileAuth, Account selectedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Attempting to authenticate account {0}", new object[]
				{
					selectedAccount.displayName
				});
			}
			mobileAuth.AuthenticateAccount(selectedAccount, selectedAccount.regionId, this);
		}

		// Token: 0x0600C112 RID: 49426 RVA: 0x003AB13C File Offset: 0x003A933C
		public void Authenticated(Account authenticatedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Successfully authenticated account {0}", new object[]
				{
					authenticatedAccount.displayName
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
			if (MASDKRegionHelper.GetCurrentlyConnectedRegion().regionId != authenticatedAccount.regionId)
			{
				ILogger logger2 = this.Logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Information, "Chosen imported guest account region does not match current region, switching region and reseting", Array.Empty<object>());
				}
				MASDKRegionHelper.ChangePreferredRegionFromRegionId(authenticatedAccount.regionId);
				HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
				if (hearthstoneApplication == null)
				{
					return;
				}
				hearthstoneApplication.Reset();
			}
		}

		// Token: 0x0600C113 RID: 49427 RVA: 0x003AB1D6 File Offset: 0x003A93D6
		public void AuthenticationCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Authentication Cancelled", Array.Empty<object>());
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Canceled, null);
		}

		// Token: 0x0600C114 RID: 49428 RVA: 0x003AB20C File Offset: 0x003A940C
		public void AuthenticationError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Error, "Error authenticating account [{0}] {1}\nMessage: {2}", new object[]
				{
					error.errorCode,
					error.errorContext,
					error.errorMessage
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C115 RID: 49429 RVA: 0x003AB270 File Offset: 0x003A9470
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "LegacyCloudGuestAccount");
		}

		// Token: 0x04009C03 RID: 39939
		private bool m_hasAttemptedImport;

		// Token: 0x0200290C RID: 10508
		private class WaitForCloudInitilization : IJobDependency, IAsyncJobResult
		{
			// Token: 0x06013DBB RID: 81339 RVA: 0x0053DF9C File Offset: 0x0053C19C
			public WaitForCloudInitilization(ILogger logger)
			{
				LegacyCloudGuestAccountStrategy.WaitForCloudInitilization <>4__this = this;
				ILogger logger2 = logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Debug, "Starting Cloud Initialization", Array.Empty<object>());
				}
				CloudStorageManager.Get().StartInitialize(delegate
				{
					ILogger logger3 = logger;
					if (logger3 != null)
					{
						logger3.Log(LogLevel.Debug, "Cloud Initialized", Array.Empty<object>());
					}
					<>4__this.m_initilizationFinished = true;
				}, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_02"));
			}

			// Token: 0x06013DBC RID: 81340 RVA: 0x0053DFFF File Offset: 0x0053C1FF
			public bool IsReady()
			{
				return this.m_initilizationFinished;
			}

			// Token: 0x0400FB9E RID: 64414
			private bool m_initilizationFinished;
		}

		// Token: 0x0200290D RID: 10509
		private class ImportCloudAccounts : IJobDependency, IAsyncJobResult
		{
			// Token: 0x17002D56 RID: 11606
			// (get) Token: 0x06013DBD RID: 81341 RVA: 0x0053E007 File Offset: 0x0053C207
			// (set) Token: 0x06013DBE RID: 81342 RVA: 0x0053E00F File Offset: 0x0053C20F
			private IMobileAuth MobileAuth { get; set; }

			// Token: 0x17002D57 RID: 11607
			// (get) Token: 0x06013DBF RID: 81343 RVA: 0x0053E018 File Offset: 0x0053C218
			// (set) Token: 0x06013DC0 RID: 81344 RVA: 0x0053E020 File Offset: 0x0053C220
			private ILogger Logger { get; set; }

			// Token: 0x17002D58 RID: 11608
			// (get) Token: 0x06013DC1 RID: 81345 RVA: 0x0053E029 File Offset: 0x0053C229
			// (set) Token: 0x06013DC2 RID: 81346 RVA: 0x0053E031 File Offset: 0x0053C231
			private ITelemetryClient TelemetryClient { get; set; }

			// Token: 0x06013DC3 RID: 81347 RVA: 0x0053E03C File Offset: 0x0053C23C
			public ImportCloudAccounts(ILegacyGuestAccountStorage accountStorage, IMobileAuth mobileAuth, ILogger logger, ITelemetryClient telemetryClient)
			{
				this.Logger = logger;
				this.MobileAuth = mobileAuth;
				this.TelemetryClient = telemetryClient;
				IEnumerable<GuestAccountInfo> storedGuestAccounts = accountStorage.GetStoredGuestAccounts();
				this.m_accountsEnumerator = ((storedGuestAccounts != null) ? storedGuestAccounts.GetEnumerator() : null);
				ILogger logger2 = this.Logger;
				if (logger2 == null)
				{
					return;
				}
				logger2.Log(LogLevel.Information, "Got {0} stored accounts to import", new object[]
				{
					(storedGuestAccounts != null) ? new int?(storedGuestAccounts.Count<GuestAccountInfo>()) : null
				});
			}

			// Token: 0x06013DC4 RID: 81348 RVA: 0x0053E0C2 File Offset: 0x0053C2C2
			public bool IsReady()
			{
				if (!this.m_readyToProcess)
				{
					return false;
				}
				if (this.m_accountsEnumerator.MoveNext())
				{
					this.ProcessNextAccount();
					return false;
				}
				return true;
			}

			// Token: 0x06013DC5 RID: 81349 RVA: 0x0053E0E4 File Offset: 0x0053C2E4
			private void ProcessNextAccount()
			{
				GuestAccountInfo guestAccountInfo = this.m_accountsEnumerator.Current;
				this.m_readyToProcess = false;
				LegacyCloudGuestAccountStrategy.ImportCallbackWrapper importCallback = new LegacyCloudGuestAccountStrategy.ImportCallbackWrapper(delegate(bool success)
				{
					ILogger logger2 = this.Logger;
					if (logger2 != null)
					{
						logger2.Log(LogLevel.Debug, "Got callback import result {0}", new object[]
						{
							success
						});
					}
					this.m_readyToProcess = true;
				}, this.Logger, this.TelemetryClient);
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Debug, "Attempting import account of id {0} : {1}", new object[]
					{
						guestAccountInfo.GuestId,
						guestAccountInfo.RegionId
					});
				}
				this.MobileAuth.ImportGuestAccount(guestAccountInfo.GuestId, guestAccountInfo.RegionId, importCallback);
			}

			// Token: 0x0400FBA2 RID: 64418
			private IEnumerator<GuestAccountInfo> m_accountsEnumerator;

			// Token: 0x0400FBA3 RID: 64419
			private bool m_readyToProcess = true;
		}

		// Token: 0x0200290E RID: 10510
		private class ImportCallbackWrapper : IImportAccountCallback
		{
			// Token: 0x17002D59 RID: 11609
			// (get) Token: 0x06013DC7 RID: 81351 RVA: 0x0053E19C File Offset: 0x0053C39C
			// (set) Token: 0x06013DC8 RID: 81352 RVA: 0x0053E1A4 File Offset: 0x0053C3A4
			private ILogger Logger { get; set; }

			// Token: 0x17002D5A RID: 11610
			// (get) Token: 0x06013DC9 RID: 81353 RVA: 0x0053E1AD File Offset: 0x0053C3AD
			// (set) Token: 0x06013DCA RID: 81354 RVA: 0x0053E1B5 File Offset: 0x0053C3B5
			private ITelemetryClient TelemetryClient { get; set; }

			// Token: 0x17002D5B RID: 11611
			// (get) Token: 0x06013DCB RID: 81355 RVA: 0x0053E1BE File Offset: 0x0053C3BE
			// (set) Token: 0x06013DCC RID: 81356 RVA: 0x0053E1C6 File Offset: 0x0053C3C6
			private Action<bool> ResultCallback { get; set; }

			// Token: 0x06013DCD RID: 81357 RVA: 0x0053E1CF File Offset: 0x0053C3CF
			public ImportCallbackWrapper(Action<bool> callback, ILogger logger, ITelemetryClient telemetryClient)
			{
				this.Logger = logger;
				this.ResultCallback = callback;
				this.TelemetryClient = telemetryClient;
			}

			// Token: 0x06013DCE RID: 81358 RVA: 0x0053E1EC File Offset: 0x0053C3EC
			public void OnImportAccountError(BlzMobileAuthError error)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Error, "Import error [{0}] {1}\nMessage: {2}", new object[]
					{
						error.errorCode,
						error.errorContext,
						error.errorMessage
					});
				}
				this.SendTelemetryResult(MASDKImportResult.ImportResult.FAILURE, error.errorCode);
				Action<bool> resultCallback = this.ResultCallback;
				if (resultCallback == null)
				{
					return;
				}
				resultCallback(false);
			}

			// Token: 0x06013DCF RID: 81359 RVA: 0x0053E254 File Offset: 0x0053C454
			public void OnImportAccountSuccess(Account account)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Information, "Import success for account {0}", new object[]
					{
						account.displayName
					});
				}
				this.SendTelemetryResult(MASDKImportResult.ImportResult.SUCCESS, 0);
				Action<bool> resultCallback = this.ResultCallback;
				if (resultCallback == null)
				{
					return;
				}
				resultCallback(true);
			}

			// Token: 0x06013DD0 RID: 81360 RVA: 0x0053E2A0 File Offset: 0x0053C4A0
			private void SendTelemetryResult(MASDKImportResult.ImportResult result, int errorCode = 0)
			{
				ITelemetryClient telemetryClient = this.TelemetryClient;
				if (telemetryClient == null)
				{
					return;
				}
				telemetryClient.SendMASDKImportResult(result, MASDKImportResult.ImportType.GUEST_ACCOUNT_ID, errorCode);
			}
		}
	}
}
