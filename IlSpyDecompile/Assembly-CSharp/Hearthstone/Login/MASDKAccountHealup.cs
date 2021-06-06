using System;
using bgs;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class MASDKAccountHealup : IAuthenticationDelegate
	{
		private bool m_handledDisconnect;

		private bool m_handledHealUpError;

		private bool m_isHealingUp;

		private Action<bool> m_finishedCallback;

		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		public MASDKAccountHealup(ILogger logger, ITelemetryClient telemetryClient)
		{
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public void StartHealup(IMobileAuth mobileAuth, Action<bool> finishedCallback)
		{
			if (!m_isHealingUp)
			{
				m_finishedCallback = finishedCallback;
				Account? authenticatedAccount = mobileAuth.GetAuthenticatedAccount();
				if (!authenticatedAccount.HasValue)
				{
					Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "No authenticated account! Cannot perform healup");
				}
				else
				{
					PresentHealupForAccount(mobileAuth, authenticatedAccount.Value);
				}
			}
		}

		private void PresentHealupForAccount(IMobileAuth mobileAuth, Account account)
		{
			SetStartingHealupStates();
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting healup of account {0} {1}", account.displayName, account.regionId);
			mobileAuth.PresentHealUpSoftAccount(account, this);
		}

		private void SetStartingHealupStates()
		{
			m_isHealingUp = true;
			BeginHandlingBnetErrors();
		}

		public void Authenticated(Account authenticatedAccount)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Healup success {0}", authenticatedAccount.displayName);
			m_finishedCallback?.Invoke(obj: true);
			ClearHealupStates();
			UpdateOptionsForHealupSuccess();
			AdTrackingManager.Get().TrackHeadlessAccountHealedUp(authenticatedAccount.accountId);
			SendTelemetryResult(AccountHealUpResult.HealUpResult.SUCCESS);
			DisconnectAndRestartForLogin();
		}

		private static void UpdateOptionsForHealupSuccess()
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, val: true);
			Options.Get().DeleteOption(Option.LAST_HEAL_UP_EVENT_DATE);
		}

		private static void DisconnectAndRestartForLogin()
		{
			BattleNet.RequestCloseAurora();
			HearthstoneApplication.Get().ResetAndForceLogin();
		}

		private void ClearHealupStates()
		{
			FinishHandlingBnetErrors();
			m_finishedCallback = null;
			m_isHealingUp = false;
		}

		public void AuthenticationCancelled()
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Healup canceled");
			SendTelemetryResult(AccountHealUpResult.HealUpResult.CANCELED);
			m_finishedCallback?.Invoke(obj: false);
			ClearHealupStates();
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Error, " error {0} {1} {2}", error.errorCode, error.errorContext, error.errorMessage);
			SendTelemetryResult(AccountHealUpResult.HealUpResult.FAILURE, error.errorCode);
			m_finishedCallback?.Invoke(obj: false);
			ClearHealupStates();
		}

		private void BeginHandlingBnetErrors()
		{
			m_handledDisconnect = false;
			m_handledHealUpError = false;
			Network.Get().AddBnetErrorListener(OnBnetError);
		}

		private void FinishHandlingBnetErrors()
		{
			m_handledDisconnect = false;
			m_handledHealUpError = false;
			Network.Get().RemoveBnetErrorListener(OnBnetError);
		}

		private bool OnBnetError(BnetErrorInfo info, object userData)
		{
			if (m_handledDisconnect && m_handledHealUpError)
			{
				return false;
			}
			BattleNetErrors error = info.GetError();
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "OnBnetError: " + error);
			switch (error)
			{
			case BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED:
				Log.TemporaryAccount.Print("Handled Disconnect");
				m_handledDisconnect = true;
				return true;
			case BattleNetErrors.ERROR_SESSION_DATA_CHANGED:
			case BattleNetErrors.ERROR_SESSION_ADMIN_KICK:
				Log.TemporaryAccount.Print("Handled Heal Up Error");
				m_handledHealUpError = true;
				return true;
			default:
				return false;
			}
		}

		private void SendTelemetryResult(AccountHealUpResult.HealUpResult result, int errorCode = 0)
		{
			TelemetryClient?.SendAccountHealUpResult(result, errorCode);
		}
	}
}
