using System.Collections.Generic;
using UnityEngine;

public class RegionMenu : ButtonListMenu
{
	public Transform m_menuBone;

	private List<UIBButton> m_buttons;

	protected string m_menuDefPrefabOverride = "ButtonListMenuDef_RegionMenu:a74fe28bd9261474dbc2b9493e2e14f6";

	protected override void Awake()
	{
		Debug.Log("region menu awake!");
		m_menuDefPrefab = m_menuDefPrefabOverride;
		m_menuParent = m_menuBone;
		m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		m_menu.m_headerText.Text = GameStrings.Get("GLUE_PICK_A_REGION");
		base.gameObject.SetActive(value: false);
	}

	public void SetButtons(List<UIBButton> buttons)
	{
		m_buttons = buttons;
	}

	public override void Show()
	{
		base.Show();
		SplashScreen.Get()?.HideWebAuth();
	}

	public override void Hide()
	{
		base.Hide();
		SplashScreen.Get()?.UnHideWebAuth();
		Object.Destroy(base.gameObject);
	}

	protected override List<UIBButton> GetButtons()
	{
		return m_buttons;
	}
}
