using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using UnityEngine;

// Token: 0x0200073C RID: 1852
public class SwitchAccountMenu : ButtonListMenu
{
	// Token: 0x060068D3 RID: 26835 RVA: 0x0022287C File Offset: 0x00220A7C
	protected override void Awake()
	{
		this.m_menuParent = this.m_menuBone;
		this.m_showAnimation = false;
		base.Awake();
		this.m_menu.m_headerText.Text = GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_SWITCH_ACCOUNT_HEADER");
		this.m_temporaryAccountButtons = new List<UIBButton>();
	}

	// Token: 0x060068D4 RID: 26836 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void OnDestroy()
	{
	}

	// Token: 0x060068D5 RID: 26837 RVA: 0x002228BC File Offset: 0x00220ABC
	public void Show(SwitchAccountMenu.OnSwitchAccountLogInPressed onSwitchAccountLogInPressedHandler)
	{
		this.m_onSwitchAccountLoginInPressedHandler = onSwitchAccountLogInPressedHandler;
		base.Show();
	}

	// Token: 0x060068D6 RID: 26838 RVA: 0x002228CC File Offset: 0x00220ACC
	public void AddTemporaryAccountButtons(IEnumerable<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> sortedTemporaryAccounts, string selectedTemporaryAccountId)
	{
		this.m_temporaryAccountButtons.Clear();
		UIBButton uibbutton = base.CreateMenuButton("Log In", "GLOBAL_LOGIN", new UIEvent.Handler(this.OnLogInButtonPressed));
		this.m_temporaryAccountButtons.Add(uibbutton);
		this.m_temporaryAccountButtons.Add(null);
		int num = 0;
		foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount in sortedTemporaryAccounts)
		{
			if (num >= 4)
			{
				break;
			}
			if ((selectedTemporaryAccountId == null || !string.Equals(selectedTemporaryAccountId, temporaryAccount.m_temporaryAccountId)) && !temporaryAccount.m_isHealedUp)
			{
				uibbutton = base.CreateMenuButton("TemporaryAccountButton" + num.ToString(), temporaryAccount.m_battleTag, new UIEvent.Handler(this.OnTemporaryAccountButtonPressed));
				uibbutton.SetData(temporaryAccount);
				this.m_temporaryAccountButtons.Add(uibbutton);
				num++;
			}
		}
	}

	// Token: 0x060068D7 RID: 26839 RVA: 0x002229B0 File Offset: 0x00220BB0
	public void AddAccountButtons(IEnumerable<Account> sortedAccounts)
	{
		this.m_temporaryAccountButtons.Clear();
		UIBButton uibbutton = base.CreateMenuButton("Log In", "GLOBAL_LOGIN", new UIEvent.Handler(this.OnLogInButtonPressed));
		this.m_temporaryAccountButtons.Add(uibbutton);
		this.m_temporaryAccountButtons.Add(null);
		int num = 0;
		foreach (Account account in sortedAccounts)
		{
			if (num >= 4)
			{
				break;
			}
			uibbutton = base.CreateMenuButton("TemporaryAccountButton" + num.ToString(), account.displayName, new UIEvent.Handler(this.OnTemporaryAccountButtonPressed));
			uibbutton.SetData(account);
			this.m_temporaryAccountButtons.Add(uibbutton);
			num++;
		}
	}

	// Token: 0x060068D8 RID: 26840 RVA: 0x00222A80 File Offset: 0x00220C80
	protected override List<UIBButton> GetButtons()
	{
		return this.m_temporaryAccountButtons;
	}

	// Token: 0x060068D9 RID: 26841 RVA: 0x00222A88 File Offset: 0x00220C88
	private void OnLogInButtonPressed(UIEvent e)
	{
		if (this.m_onSwitchAccountLoginInPressedHandler != null)
		{
			this.m_onSwitchAccountLoginInPressedHandler(null);
			this.m_onSwitchAccountLoginInPressedHandler = null;
		}
		this.Hide();
	}

	// Token: 0x060068DA RID: 26842 RVA: 0x00222AAC File Offset: 0x00220CAC
	private void OnTemporaryAccountButtonPressed(UIEvent e)
	{
		object data = e.GetElement().GetData();
		this.Hide();
		if (this.m_onSwitchAccountLoginInPressedHandler != null)
		{
			this.m_onSwitchAccountLoginInPressedHandler(data);
			this.m_onSwitchAccountLoginInPressedHandler = null;
		}
	}

	// Token: 0x040055E1 RID: 21985
	private const int TEMPORARY_ACCOUNT_SHOWN_LIMIT = 4;

	// Token: 0x040055E2 RID: 21986
	public Transform m_menuBone;

	// Token: 0x040055E3 RID: 21987
	private List<UIBButton> m_temporaryAccountButtons;

	// Token: 0x040055E4 RID: 21988
	private SwitchAccountMenu.OnSwitchAccountLogInPressed m_onSwitchAccountLoginInPressedHandler;

	// Token: 0x02002311 RID: 8977
	// (Invoke) Token: 0x060129B2 RID: 76210
	public delegate void OnSwitchAccountLogInPressed(object account);
}
