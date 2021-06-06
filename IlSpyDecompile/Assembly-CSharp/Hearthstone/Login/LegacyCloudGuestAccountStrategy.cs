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
	public class LegacyCloudGuestAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		private class WaitForCloudInitilization : IJobDependency, IAsyncJobResult
		{
			private bool m_initilizationFinished;

			public WaitForCloudInitilization(ILogger logger)
			{
				WaitForCloudInitilization waitForCloudInitilization = this;
				logger?.Log(LogLevel.Debug, "Starting Cloud Initialization");
				CloudStorageManager.Get().StartInitialize(delegate
				{
					logger?.Log(LogLevel.Debug, "Cloud Initialized");
					waitForCloudInitilization.m_initilizationFinished = true;
				}, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_02"));
			}

			public bool IsReady()
			{
				return m_initilizationFinished;
			}
		}

		private class ImportCloudAccounts : IJobDependency, IAsyncJobResult
		{
			private IEnumerator<GuestAccountInfo> m_accountsEnumerator;

			private bool m_readyToProcess = true;

			private IMobileAuth MobileAuth { get; set; }

			private ILogger Logger { get; set; }

			private ITelemetryClient TelemetryClient { get; set; }

			public ImportCloudAccounts(ILegacyGuestAccountStorage accountStorage, IMobileAuth mobileAuth, ILogger logger, ITelemetryClient telemetryClient)
			{
				Logger = logger;
				MobileAuth = mobileAuth;
				TelemetryClient = telemetryClient;
				IEnumerable<GuestAccountInfo> storedGuestAccounts = accountStorage.GetStoredGuestAccounts();
				m_accountsEnumerator = storedGuestAccounts?.GetEnumerator();
				Logger?.Log(LogLevel.Information, "Got {0} stored accounts to import", storedGuestAccounts?.Count());
			}

			public bool IsReady()
			{
				if (!m_readyToProcess)
				{
					return false;
				}
				if (m_accountsEnumerator.MoveNext())
				{
					ProcessNextAccount();
					return false;
				}
				return true;
			}

			private void ProcessNextAccount()
			{
				GuestAccountInfo current = m_accountsEnumerator.Current;
				m_readyToProcess = false;
				ImportCallbackWrapper importCallback = new ImportCallbackWrapper(delegate(bool success)
				{
					Logger?.Log(LogLevel.Debug, "Got callback import result {0}", success);
					m_readyToProcess = true;
				}, Logger, TelemetryClient);
				Logger?.Log(LogLevel.Debug, "Attempting import account of id {0} : {1}", current.GuestId, current.RegionId);
				MobileAuth.ImportGuestAccount(current.GuestId, current.RegionId, importCallback);
			}
		}

		private class ImportCallbackWrapper : IImportAccountCallback
		{
			private ILogger Logger { get; set; }

			private ITelemetryClient TelemetryClient { get; set; }

			private Action<bool> ResultCallback { get; set; }

			public ImportCallbackWrapper(Action<bool> callback, ILogger logger, ITelemetryClient telemetryClient)
			{
				Logger = logger;
				ResultCallback = callback;
				TelemetryClient = telemetryClient;
			}

			public void OnImportAccountError(BlzMobileAuthError error)
			{
				Logger?.Log(LogLevel.Error, "Import error [{0}] {1}\nMessage: {2}", error.errorCode, error.errorContext, error.errorMessage);
				SendTelemetryResult(MASDKImportResult.ImportResult.FAILURE, error.errorCode);
				ResultCallback?.Invoke(obj: false);
			}

			public void OnImportAccountSuccess(Account account)
			{
				Logger?.Log(LogLevel.Information, "Import success for account {0}", account.displayName);
				SendTelemetryResult(MASDKImportResult.ImportResult.SUCCESS);
				ResultCallback?.Invoke(obj: true);
			}

			private void SendTelemetryResult(MASDKImportResult.ImportResult result, int errorCode = 0)
			{
				TelemetryClient?.SendMASDKImportResult(result, MASDKImportResult.ImportType.GUEST_ACCOUNT_ID, errorCode);
			}
		}

		private bool m_hasAttemptedImport;

		private ILegacyGuestAccountStorage AccountStorage { get; set; }

		private ISwitchAccountMenuController SwitchAccountController { get; set; }

		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		private TokenPromise Promise { get; set; }

		public LegacyCloudGuestAccountStrategy(ILegacyGuestAccountStorage accountStorgage, ISwitchAccountMenuController switchAccountMenuController, ILogger logger, ITelemetryClient telemetryClient)
		{
			AccountStorage = accountStorgage;
			SwitchAccountController = switchAccountMenuController;
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			return !m_hasAttemptedImport;
		}

		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			Promise = promise;
			Logger?.Log(LogLevel.Debug, "Starting Legacy guest account import job");
			Processor.QueueJob("ImportAndSelectGuestAccount", Job_ImportAndPresentOptions(parameters.MobileAuth));
		}

		private IEnumerator<IAsyncJobResult> Job_ImportAndPresentOptions(IMobileAuth mobileAuth)
		{
			m_hasAttemptedImport = true;
			Logger?.Log(LogLevel.Information, "Starting Import and Present Options");
			yield return new WaitForCloudInitilization(Logger);
			IEnumerable<GuestAccountInfo> storedGuestAccounts = AccountStorage.GetStoredGuestAccounts();
			if (storedGuestAccounts != null && storedGuestAccounts.Count() == 0)
			{
				Logger?.Log(LogLevel.Information, "No soft accounts found to import, failing the legacy guest account import");
				Promise.SetResult(TokenPromise.ResultType.Failure);
				yield break;
			}
			yield return new ImportCloudAccounts(AccountStorage, mobileAuth, Logger, TelemetryClient);
			string selectedGuestAccountId = AccountStorage.GetSelectedGuestAccountId();
			AccountStorage.ClearGuestAccounts();
			List<Account> softAccounts = mobileAuth.GetSoftAccounts();
			Logger?.Log(LogLevel.Information, "Got {0} soft accounts from mobile auth", softAccounts?.Count);
			if (softAccounts.Count == 0)
			{
				Logger?.Log(LogLevel.Information, "No soft accounts found, failing the legacy guest account import");
				Promise.SetResult(TokenPromise.ResultType.Failure);
				yield break;
			}
			foreach (Account item in softAccounts)
			{
				if (item.bnetGuestId.Equals(selectedGuestAccountId))
				{
					Logger?.Log(LogLevel.Information, "Found a selected guest account, authenticating it");
					AuthenticateAccount(mobileAuth, item);
					yield break;
				}
			}
			ShowAccountSelector(mobileAuth, softAccounts);
		}

		private void ShowAccountSelector(IMobileAuth mobileAuth, List<Account> authAccounts)
		{
			Logger?.Log(LogLevel.Information, "Showing switch account selection for {0} accounts", authAccounts.Count);
			SwitchAccountController.ShowSwitchAccount(authAccounts, delegate(Account? selectedAccount)
			{
				if (!selectedAccount.HasValue)
				{
					Logger?.Log(LogLevel.Information, "Login chosen for switch account. Failing so we can show login");
					Promise.SetResult(TokenPromise.ResultType.Failure);
				}
				else
				{
					AuthenticateAccount(mobileAuth, selectedAccount.Value);
				}
			});
		}

		private void AuthenticateAccount(IMobileAuth mobileAuth, Account selectedAccount)
		{
			Logger?.Log(LogLevel.Information, "Attempting to authenticate account {0}", selectedAccount.displayName);
			mobileAuth.AuthenticateAccount(selectedAccount, selectedAccount.regionId, this);
		}

		public void Authenticated(Account authenticatedAccount)
		{
			Logger?.Log(LogLevel.Information, "Successfully authenticated account {0}", authenticatedAccount.displayName);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
			Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
			if (MASDKRegionHelper.GetCurrentlyConnectedRegion().regionId != authenticatedAccount.regionId)
			{
				Logger?.Log(LogLevel.Information, "Chosen imported guest account region does not match current region, switching region and reseting");
				MASDKRegionHelper.ChangePreferredRegionFromRegionId(authenticatedAccount.regionId);
				HearthstoneApplication.Get()?.Reset();
			}
		}

		public void AuthenticationCancelled()
		{
			Logger?.Log(LogLevel.Information, "Authentication Cancelled");
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED);
			Promise.SetResult(TokenPromise.ResultType.Canceled);
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			Logger?.Log(LogLevel.Error, "Error authenticating account [{0}] {1}\nMessage: {2}", error.errorCode, error.errorContext, error.errorMessage);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "LegacyCloudGuestAccount");
		}
	}
}
