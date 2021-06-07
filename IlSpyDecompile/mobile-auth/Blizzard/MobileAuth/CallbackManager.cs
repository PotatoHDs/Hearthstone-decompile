namespace Blizzard.MobileAuth
{
	internal sealed class CallbackManager
	{
		private IAuthenticationDelegate authDelegate;

		private IWebSsoListener webSsoListener;

		private IWebSsoTokenCallback webSsoTokenCallback;

		private IRegionListener regionListener;

		private IGuestSoftAccountIdListener guestSoftAccountIdListener;

		private IBattleTagInfoListener battleTagInfoListener;

		private IBattleTagChangeListener battleTagChangeListener;

		private IOnAccountRemovedListener onAccountRemovedListener;

		private IImportAccountCallback importAccountCallback;

		public void SetAuthDelegate(IAuthenticationDelegate authDelegate)
		{
			this.authDelegate = authDelegate;
		}

		public void SetWebSsoListener(IWebSsoListener listener)
		{
			webSsoListener = listener;
		}

		public void SetWebSsoTokenCallback(IWebSsoTokenCallback callback)
		{
			webSsoTokenCallback = callback;
		}

		public void SetRegionListener(IRegionListener listener)
		{
			regionListener = listener;
		}

		public void SetGuestSoftAccountIdListener(IGuestSoftAccountIdListener listener)
		{
			guestSoftAccountIdListener = listener;
		}

		public void SetBattleTagInfoListener(IBattleTagInfoListener listener)
		{
			battleTagInfoListener = listener;
		}

		public void SetBattleTagChangeListener(IBattleTagChangeListener listener)
		{
			battleTagChangeListener = listener;
		}

		public void SetOnAccountRemovedListener(IOnAccountRemovedListener listener)
		{
			onAccountRemovedListener = listener;
		}

		public void SetImportAccountCallback(IImportAccountCallback callback)
		{
			importAccountCallback = callback;
		}

		public void OnBlzAccountResult(Account blzAccount)
		{
			if (authDelegate != null)
			{
				authDelegate.Authenticated(blzAccount);
			}
			authDelegate = null;
		}

		internal void OnCancelled()
		{
			if (authDelegate != null)
			{
				authDelegate.AuthenticationCancelled();
			}
			authDelegate = null;
		}

		internal void OnAuthError(BlzMobileAuthError error)
		{
			if (authDelegate != null)
			{
				authDelegate.AuthenticationError(error);
			}
			authDelegate = null;
		}

		internal void OnBlzWebSsoUrlReady(string result)
		{
			webSsoListener?.OnWebSsoUrlReady(result);
			webSsoListener = null;
		}

		internal void OnSsoUrlError(BlzMobileAuthError error)
		{
			webSsoListener?.OnSsoUrlError(error);
			webSsoListener = null;
		}

		internal void OnWebSsoTokenReady(string result)
		{
			webSsoTokenCallback?.OnWebSsoToken(result);
			webSsoTokenCallback = null;
		}

		internal void OnWebSsoTokenError(BlzMobileAuthError error)
		{
			webSsoTokenCallback?.OnWebSsoTokenError(error);
			webSsoTokenCallback = null;
		}

		internal void OnRegionRetrieved(Region region)
		{
			regionListener?.OnRegionRetrieved(region);
			regionListener = null;
		}

		internal void OnRegionError(BlzMobileAuthError error)
		{
			regionListener?.OnRegionError(error);
			regionListener = null;
		}

		internal void OnGuestSoftAccountIdRetrieved(GuestSoftAccountId guestSoftAccountId)
		{
			guestSoftAccountIdListener?.OnGuestSoftAccountIdRetrieved(guestSoftAccountId);
			guestSoftAccountIdListener = null;
		}

		internal void OnGuestSoftAccountIdError(BlzMobileAuthError error)
		{
			guestSoftAccountIdListener?.OnGuestSoftAccountIdError(error);
			guestSoftAccountIdListener = null;
		}

		internal void OnBattleTagInfoRetrieved(BattleTagInfo battleTagInfo)
		{
			battleTagInfoListener?.OnBattleTagInfoRetrieved(battleTagInfo);
			battleTagInfoListener = null;
		}

		internal void OnBattleTagInfoError(BlzMobileAuthError error)
		{
			battleTagInfoListener?.OnBatleTagInfoError(error);
			battleTagInfoListener = null;
		}

		internal void OnBattleTagChange(BattleTagChangeValue battleTagChangeValue)
		{
			battleTagChangeListener?.OnBattleTagChange(battleTagChangeValue);
			battleTagChangeListener = null;
		}

		internal void OnBattleTagChangeError(BlzMobileAuthError? error, BattleTagChangeErrorContainer? battleTagChangeErrorContainer)
		{
			battleTagChangeListener?.OnBattleTagChangeError(error, battleTagChangeErrorContainer);
			battleTagChangeListener = null;
		}

		internal void OnAccountRemovedResult(bool result)
		{
			onAccountRemovedListener?.OnAccountRemovedResult(result);
			onAccountRemovedListener = null;
		}

		internal void OnImportAccountSuccess(Account account)
		{
			importAccountCallback?.OnImportAccountSuccess(account);
			importAccountCallback = null;
		}

		internal void OnImportAccountError(BlzMobileAuthError error)
		{
			importAccountCallback?.OnImportAccountError(error);
			importAccountCallback = null;
		}
	}
}
