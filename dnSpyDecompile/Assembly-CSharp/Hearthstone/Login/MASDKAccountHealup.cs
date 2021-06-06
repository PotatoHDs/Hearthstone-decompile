using System;
using bgs;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x0200113E RID: 4414
	public class MASDKAccountHealup : IAuthenticationDelegate
	{
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600C15C RID: 49500 RVA: 0x003ABE64 File Offset: 0x003AA064
		// (set) Token: 0x0600C15D RID: 49501 RVA: 0x003ABE6C File Offset: 0x003AA06C
		private ILogger Logger { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600C15E RID: 49502 RVA: 0x003ABE75 File Offset: 0x003AA075
		// (set) Token: 0x0600C15F RID: 49503 RVA: 0x003ABE7D File Offset: 0x003AA07D
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C160 RID: 49504 RVA: 0x003ABE86 File Offset: 0x003AA086
		public MASDKAccountHealup(ILogger logger, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C161 RID: 49505 RVA: 0x003ABE9C File Offset: 0x003AA09C
		public void StartHealup(IMobileAuth mobileAuth, Action<bool> finishedCallback)
		{
			if (this.m_isHealingUp)
			{
				return;
			}
			this.m_finishedCallback = finishedCallback;
			Account? authenticatedAccount = mobileAuth.GetAuthenticatedAccount();
			if (authenticatedAccount != null)
			{
				this.PresentHealupForAccount(mobileAuth, authenticatedAccount.Value);
				return;
			}
			ILogger logger = this.Logger;
			if (logger == null)
			{
				return;
			}
			logger.Log(Blizzard.T5.Core.LogLevel.Error, "No authenticated account! Cannot perform healup", Array.Empty<object>());
		}

		// Token: 0x0600C162 RID: 49506 RVA: 0x003ABEF3 File Offset: 0x003AA0F3
		private void PresentHealupForAccount(IMobileAuth mobileAuth, Account account)
		{
			this.SetStartingHealupStates();
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting healup of account {0} {1}", new object[]
				{
					account.displayName,
					account.regionId
				});
			}
			mobileAuth.PresentHealUpSoftAccount(account, this);
		}

		// Token: 0x0600C163 RID: 49507 RVA: 0x003ABF32 File Offset: 0x003AA132
		private void SetStartingHealupStates()
		{
			this.m_isHealingUp = true;
			this.BeginHandlingBnetErrors();
		}

		// Token: 0x0600C164 RID: 49508 RVA: 0x003ABF44 File Offset: 0x003AA144
		public void Authenticated(Account authenticatedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Healup success {0}", new object[]
				{
					authenticatedAccount.displayName
				});
			}
			Action<bool> finishedCallback = this.m_finishedCallback;
			if (finishedCallback != null)
			{
				finishedCallback(true);
			}
			this.ClearHealupStates();
			MASDKAccountHealup.UpdateOptionsForHealupSuccess();
			AdTrackingManager.Get().TrackHeadlessAccountHealedUp(authenticatedAccount.accountId);
			this.SendTelemetryResult(AccountHealUpResult.HealUpResult.SUCCESS, 0);
			MASDKAccountHealup.DisconnectAndRestartForLogin();
		}

		// Token: 0x0600C165 RID: 49509 RVA: 0x003ABFB1 File Offset: 0x003AA1B1
		private static void UpdateOptionsForHealupSuccess()
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, true);
			Options.Get().DeleteOption(Option.LAST_HEAL_UP_EVENT_DATE);
		}

		// Token: 0x0600C166 RID: 49510 RVA: 0x003ABFCC File Offset: 0x003AA1CC
		private static void DisconnectAndRestartForLogin()
		{
			BattleNet.RequestCloseAurora();
			HearthstoneApplication.Get().ResetAndForceLogin();
		}

		// Token: 0x0600C167 RID: 49511 RVA: 0x003ABFDD File Offset: 0x003AA1DD
		private void ClearHealupStates()
		{
			this.FinishHandlingBnetErrors();
			this.m_finishedCallback = null;
			this.m_isHealingUp = false;
		}

		// Token: 0x0600C168 RID: 49512 RVA: 0x003ABFF3 File Offset: 0x003AA1F3
		public void AuthenticationCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Healup canceled", Array.Empty<object>());
			}
			this.SendTelemetryResult(AccountHealUpResult.HealUpResult.CANCELED, 0);
			Action<bool> finishedCallback = this.m_finishedCallback;
			if (finishedCallback != null)
			{
				finishedCallback(false);
			}
			this.ClearHealupStates();
		}

		// Token: 0x0600C169 RID: 49513 RVA: 0x003AC034 File Offset: 0x003AA234
		public void AuthenticationError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Error, " error {0} {1} {2}", new object[]
				{
					error.errorCode,
					error.errorContext,
					error.errorMessage
				});
			}
			this.SendTelemetryResult(AccountHealUpResult.HealUpResult.FAILURE, error.errorCode);
			Action<bool> finishedCallback = this.m_finishedCallback;
			if (finishedCallback != null)
			{
				finishedCallback(false);
			}
			this.ClearHealupStates();
		}

		// Token: 0x0600C16A RID: 49514 RVA: 0x003AC0A3 File Offset: 0x003AA2A3
		private void BeginHandlingBnetErrors()
		{
			this.m_handledDisconnect = false;
			this.m_handledHealUpError = false;
			Network.Get().AddBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		}

		// Token: 0x0600C16B RID: 49515 RVA: 0x003AC0C9 File Offset: 0x003AA2C9
		private void FinishHandlingBnetErrors()
		{
			this.m_handledDisconnect = false;
			this.m_handledHealUpError = false;
			Network.Get().RemoveBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		}

		// Token: 0x0600C16C RID: 49516 RVA: 0x003AC0F0 File Offset: 0x003AA2F0
		private bool OnBnetError(BnetErrorInfo info, object userData)
		{
			if (this.m_handledDisconnect && this.m_handledHealUpError)
			{
				return false;
			}
			BattleNetErrors error = info.GetError();
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "OnBnetError: " + error, Array.Empty<object>());
			}
			if (error != BattleNetErrors.ERROR_SESSION_DATA_CHANGED)
			{
				if (error == BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED)
				{
					global::Log.TemporaryAccount.Print("Handled Disconnect", Array.Empty<object>());
					this.m_handledDisconnect = true;
					return true;
				}
				if (error != BattleNetErrors.ERROR_SESSION_ADMIN_KICK)
				{
					return false;
				}
			}
			global::Log.TemporaryAccount.Print("Handled Heal Up Error", Array.Empty<object>());
			this.m_handledHealUpError = true;
			return true;
		}

		// Token: 0x0600C16D RID: 49517 RVA: 0x003AC18F File Offset: 0x003AA38F
		private void SendTelemetryResult(AccountHealUpResult.HealUpResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendAccountHealUpResult(result, errorCode);
		}

		// Token: 0x04009C1C RID: 39964
		private bool m_handledDisconnect;

		// Token: 0x04009C1D RID: 39965
		private bool m_handledHealUpError;

		// Token: 0x04009C1E RID: 39966
		private bool m_isHealingUp;

		// Token: 0x04009C1F RID: 39967
		private Action<bool> m_finishedCallback;
	}
}
