using System;

namespace Hearthstone.Login
{
	// Token: 0x0200112B RID: 4395
	public static class CreateSkipHelper
	{
		// Token: 0x0600C0C0 RID: 49344 RVA: 0x003AAD00 File Offset: 0x003A8F00
		public static bool IsCreateSkipScreenSupported()
		{
			ILoginService loginService;
			return HearthstoneServices.TryGet<ILoginService>(out loginService) && loginService.SupportsAccountHealup();
		}

		// Token: 0x0600C0C1 RID: 49345 RVA: 0x003AAD1E File Offset: 0x003A8F1E
		public static bool ShouldShowCreateSkip()
		{
			return TemporaryAccountManager.IsTemporaryAccount() && CreateSkipHelper.IsCreateSkipScreenSupported() && Options.Get().GetBool(Option.SHOW_CREATE_SKIP_ACCT, false);
		}

		// Token: 0x0600C0C2 RID: 49346 RVA: 0x003AAD3D File Offset: 0x003A8F3D
		public static void RequestShowCreateSkip()
		{
			if (CreateSkipHelper.IsCreateSkipScreenSupported())
			{
				Options.Get().SetBool(Option.SHOW_CREATE_SKIP_ACCT, true);
			}
		}

		// Token: 0x0600C0C3 RID: 49347 RVA: 0x003AAD53 File Offset: 0x003A8F53
		public static void ClearShowCreateSkip()
		{
			Options.Get().SetBool(Option.SHOW_CREATE_SKIP_ACCT, false);
		}

		// Token: 0x0600C0C4 RID: 49348 RVA: 0x003AAD64 File Offset: 0x003A8F64
		public static bool ShowCreateSkipDialog(Action onCanceled)
		{
			Log.Login.PrintInfo("Requesting showing create/Skip", Array.Empty<object>());
			TemporaryAccountSignUpPopUp.PopupTextParameters popupArgs = new TemporaryAccountSignUpPopUp.PopupTextParameters
			{
				Header = "GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_02",
				Body = "GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_07",
				CancelButton = "GLUE_TEMPORARY_ACCOUNT_SKIP"
			};
			bool flag = TemporaryAccountManager.Get().ShowHealUpDialog(popupArgs, TemporaryAccountManager.HealUpReason.UNKNOWN, true, delegate()
			{
				Action onCanceled2 = onCanceled;
				if (onCanceled2 == null)
				{
					return;
				}
				onCanceled2();
			});
			if (flag)
			{
				CreateSkipHelper.ClearShowCreateSkip();
			}
			return flag;
		}
	}
}
