using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B2A RID: 2858
public class RegionMenu : ButtonListMenu
{
	// Token: 0x06009790 RID: 38800 RVA: 0x003102F8 File Offset: 0x0030E4F8
	protected override void Awake()
	{
		Debug.Log("region menu awake!");
		this.m_menuDefPrefab = this.m_menuDefPrefabOverride;
		this.m_menuParent = this.m_menuBone;
		this.m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		this.m_menu.m_headerText.Text = GameStrings.Get("GLUE_PICK_A_REGION");
		base.gameObject.SetActive(false);
	}

	// Token: 0x06009791 RID: 38801 RVA: 0x0031035B File Offset: 0x0030E55B
	public void SetButtons(List<UIBButton> buttons)
	{
		this.m_buttons = buttons;
	}

	// Token: 0x06009792 RID: 38802 RVA: 0x00310364 File Offset: 0x0030E564
	public override void Show()
	{
		base.Show();
		SplashScreen splashScreen = SplashScreen.Get();
		if (splashScreen == null)
		{
			return;
		}
		splashScreen.HideWebAuth();
	}

	// Token: 0x06009793 RID: 38803 RVA: 0x0031037B File Offset: 0x0030E57B
	public override void Hide()
	{
		base.Hide();
		SplashScreen splashScreen = SplashScreen.Get();
		if (splashScreen != null)
		{
			splashScreen.UnHideWebAuth();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06009794 RID: 38804 RVA: 0x0031039E File Offset: 0x0030E59E
	protected override List<UIBButton> GetButtons()
	{
		return this.m_buttons;
	}

	// Token: 0x04007EEC RID: 32492
	public Transform m_menuBone;

	// Token: 0x04007EED RID: 32493
	private List<UIBButton> m_buttons;

	// Token: 0x04007EEE RID: 32494
	protected string m_menuDefPrefabOverride = "ButtonListMenuDef_RegionMenu:a74fe28bd9261474dbc2b9493e2e14f6";
}
