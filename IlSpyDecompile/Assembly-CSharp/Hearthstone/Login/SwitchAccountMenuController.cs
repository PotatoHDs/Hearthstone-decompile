using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using UnityEngine;

namespace Hearthstone.Login
{
	public class SwitchAccountMenuController : ISwitchAccountMenuController
	{
		private const string SWITCH_ACCOUNT_PREFAB = "SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf";

		public void ShowSwitchAccount(IEnumerable<Account> accounts, Action<Account?> callback)
		{
			GameObject accountPrefab = AssetLoader.Get().InstantiatePrefab("SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf");
			SwitchAccountMenu component = accountPrefab.GetComponent<SwitchAccountMenu>();
			component.AddAccountButtons(accounts);
			component.Show(delegate(object accountObject)
			{
				if (accountObject == null || !(accountObject is Account))
				{
					UnityEngine.Object.Destroy(accountPrefab);
					callback(null);
				}
				else
				{
					Account value = (Account)accountObject;
					callback(value);
					UnityEngine.Object.Destroy(accountPrefab);
				}
			});
		}
	}
}
