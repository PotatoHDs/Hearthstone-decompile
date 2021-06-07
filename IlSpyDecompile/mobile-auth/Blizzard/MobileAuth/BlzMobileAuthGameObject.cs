using System.Collections;
using UnityEngine;

namespace Blizzard.MobileAuth
{
	internal class BlzMobileAuthGameObject : MonoBehaviour, IBlzAuthCallbackHandler, IBlzCallbackService
	{
		public void Awake()
		{
			Object.DontDestroyOnLoad(this);
		}

		public void OnBlzAccountReceived(string message)
		{
			Debug.Log("BlzMobileAuthGameObject OnBlzAccountReceived: " + message);
			Account blzAccount = JsonUtility.FromJson<Account>(message);
			MobileAuth.callbackManager.OnBlzAccountResult(blzAccount);
		}

		public void OnCancelled(string message)
		{
			Debug.Log("BlzMobileAuthGameObject OnCancelled: " + message);
			MobileAuth.callbackManager.OnCancelled();
		}

		public void OnAuthError(string message)
		{
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			Debug.Log("BlzMobileAuthGameObject OnError: " + error);
			MobileAuth.callbackManager.OnAuthError(error);
		}

		public void OnBlzWebSsoUrlReady(string message)
		{
			Debug.Log("OnBlzWebSssoUrlReady " + message);
			MobileAuth.callbackManager.OnBlzWebSsoUrlReady(message);
		}

		public void OnBlzWebSsoUrlError(string message)
		{
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			Debug.Log("OnBlzWebSsoUrlError " + message);
			MobileAuth.callbackManager.OnSsoUrlError(error);
		}

		public void OnBlzWebSsoTokenRetrieved(string message)
		{
			Debug.Log("OnBlzWebSsoTokenRetrieved " + message);
			MobileAuth.callbackManager.OnWebSsoTokenReady(message);
		}

		public void OnBlzWebSsoTokenError(string message)
		{
			Debug.Log("OnBlzWebSsoTokenError " + message);
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnWebSsoTokenError(error);
		}

		private void OnBlzRegionRetrieved(string message)
		{
			Debug.Log("OnRegionRetrieved " + message);
			Region region = new Region(message);
			MobileAuth.callbackManager.OnRegionRetrieved(region);
		}

		private void OnBlzRegionError(string message)
		{
			Debug.Log("OnRegionError " + message);
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnRegionError(error);
		}

		private void OnBlzGuestSoftAccountIdRetrieved(string message)
		{
			Debug.Log("OnGuestSoftAccountIdRetrieved " + message);
			GuestSoftAccountId guestSoftAccountId = JsonUtility.FromJson<GuestSoftAccountId>(message);
			MobileAuth.callbackManager.OnGuestSoftAccountIdRetrieved(guestSoftAccountId);
		}

		private void OnBlzGuestSoftAccountIdError(string message)
		{
			Debug.Log("OnBlzGuestSoftAccountIdError " + message);
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnGuestSoftAccountIdError(error);
		}

		private void OnBlzBattleTagInfoRetrieved(string message)
		{
			Debug.Log("OnBlzBattleTagInfoRetrieved " + message);
			BattleTagInfo battleTagInfo = JsonUtility.FromJson<BattleTagInfo>(message);
			MobileAuth.callbackManager.OnBattleTagInfoRetrieved(battleTagInfo);
		}

		private void OnBlzBattleTagInfoError(string message)
		{
			Debug.Log("OnBlzBattleTagInfoError " + message);
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnBattleTagInfoError(error);
		}

		private void OnBlzBattleTagChange(string message)
		{
			Debug.Log("OnBlzBattleTagChange " + message);
			BattleTagChangeValue battleTagChangeValue = JsonUtility.FromJson<BattleTagChangeValue>(message);
			MobileAuth.callbackManager.OnBattleTagChange(battleTagChangeValue);
		}

		private void OnBlzBattleTagChangeError(string message)
		{
			Debug.Log("OnBlzBattleTagChangeError " + message);
			BlzMobileAuthError value = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnBattleTagChangeError(value, null);
		}

		private void OnBlzBattleTagChangeErrorContainer(string message)
		{
			Debug.Log("OnBlzBattleTagChangeErrorContainer " + message);
			BattleTagChangeErrorContainer value = JsonUtility.FromJson<BattleTagChangeErrorContainer>(message);
			MobileAuth.callbackManager.OnBattleTagChangeError(null, value);
		}

		void IBlzCallbackService.StartCoroutine(IEnumerator operation)
		{
			StartCoroutine(operation);
		}

		private void OnBlzImportAccountSuccess(string message)
		{
			Debug.Log("OnBlzImportAccountSuccess " + message);
			Account account = JsonUtility.FromJson<Account>(message);
			MobileAuth.callbackManager.OnImportAccountSuccess(account);
		}

		private void OnBlzImportAccountError(string message)
		{
			Debug.Log("OnBlzImportAccountError " + message);
			BlzMobileAuthError error = JsonUtility.FromJson<BlzMobileAuthError>(message);
			MobileAuth.callbackManager.OnImportAccountError(error);
		}
	}
}
