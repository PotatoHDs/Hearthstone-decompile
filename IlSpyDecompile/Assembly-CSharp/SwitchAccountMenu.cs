using System.Collections.Generic;
using Blizzard.MobileAuth;
using UnityEngine;

public class SwitchAccountMenu : ButtonListMenu
{
	public delegate void OnSwitchAccountLogInPressed(object account);

	private const int TEMPORARY_ACCOUNT_SHOWN_LIMIT = 4;

	public Transform m_menuBone;

	private List<UIBButton> m_temporaryAccountButtons;

	private OnSwitchAccountLogInPressed m_onSwitchAccountLoginInPressedHandler;

	protected override void Awake()
	{
		m_menuParent = m_menuBone;
		m_showAnimation = false;
		base.Awake();
		m_menu.m_headerText.Text = GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_SWITCH_ACCOUNT_HEADER");
		m_temporaryAccountButtons = new List<UIBButton>();
	}

	protected override void OnDestroy()
	{
	}

	public void Show(OnSwitchAccountLogInPressed onSwitchAccountLogInPressedHandler)
	{
		m_onSwitchAccountLoginInPressedHandler = onSwitchAccountLogInPressedHandler;
		base.Show();
	}

	public void AddTemporaryAccountButtons(IEnumerable<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> sortedTemporaryAccounts, string selectedTemporaryAccountId)
	{
		m_temporaryAccountButtons.Clear();
		UIBButton item = CreateMenuButton("Log In", "GLOBAL_LOGIN", OnLogInButtonPressed);
		m_temporaryAccountButtons.Add(item);
		m_temporaryAccountButtons.Add(null);
		int num = 0;
		foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount sortedTemporaryAccount in sortedTemporaryAccounts)
		{
			if (num >= 4)
			{
				break;
			}
			if ((selectedTemporaryAccountId == null || !string.Equals(selectedTemporaryAccountId, sortedTemporaryAccount.m_temporaryAccountId)) && !sortedTemporaryAccount.m_isHealedUp)
			{
				item = CreateMenuButton("TemporaryAccountButton" + num, sortedTemporaryAccount.m_battleTag, OnTemporaryAccountButtonPressed);
				item.SetData(sortedTemporaryAccount);
				m_temporaryAccountButtons.Add(item);
				num++;
			}
		}
	}

	public void AddAccountButtons(IEnumerable<Account> sortedAccounts)
	{
		m_temporaryAccountButtons.Clear();
		UIBButton item = CreateMenuButton("Log In", "GLOBAL_LOGIN", OnLogInButtonPressed);
		m_temporaryAccountButtons.Add(item);
		m_temporaryAccountButtons.Add(null);
		int num = 0;
		foreach (Account sortedAccount in sortedAccounts)
		{
			if (num >= 4)
			{
				break;
			}
			item = CreateMenuButton("TemporaryAccountButton" + num, sortedAccount.displayName, OnTemporaryAccountButtonPressed);
			item.SetData(sortedAccount);
			m_temporaryAccountButtons.Add(item);
			num++;
		}
	}

	protected override List<UIBButton> GetButtons()
	{
		return m_temporaryAccountButtons;
	}

	private void OnLogInButtonPressed(UIEvent e)
	{
		if (m_onSwitchAccountLoginInPressedHandler != null)
		{
			m_onSwitchAccountLoginInPressedHandler(null);
			m_onSwitchAccountLoginInPressedHandler = null;
		}
		Hide();
	}

	private void OnTemporaryAccountButtonPressed(UIEvent e)
	{
		object data = e.GetElement().GetData();
		Hide();
		if (m_onSwitchAccountLoginInPressedHandler != null)
		{
			m_onSwitchAccountLoginInPressedHandler(data);
			m_onSwitchAccountLoginInPressedHandler = null;
		}
	}
}
