using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using UnityEngine;

namespace Hearthstone.Login
{
	// Token: 0x0200114A RID: 4426
	public class SwitchAccountMenuController : ISwitchAccountMenuController
	{
		// Token: 0x0600C1EF RID: 49647 RVA: 0x003AD9AC File Offset: 0x003ABBAC
		public void ShowSwitchAccount(IEnumerable<Account> accounts, Action<Account?> callback)
		{
			GameObject accountPrefab = AssetLoader.Get().InstantiatePrefab("SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf", AssetLoadingOptions.None);
			SwitchAccountMenu component = accountPrefab.GetComponent<SwitchAccountMenu>();
			component.AddAccountButtons(accounts);
			component.Show(delegate(object accountObject)
			{
				if (accountObject == null || !(accountObject is Account))
				{
					UnityEngine.Object.Destroy(accountPrefab);
					callback(null);
					return;
				}
				Account value = (Account)accountObject;
				callback(new Account?(value));
				UnityEngine.Object.Destroy(accountPrefab);
			});
		}

		// Token: 0x04009C50 RID: 40016
		private const string SWITCH_ACCOUNT_PREFAB = "SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf";
	}
}
