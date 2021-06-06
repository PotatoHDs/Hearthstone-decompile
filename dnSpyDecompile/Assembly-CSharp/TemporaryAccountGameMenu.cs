using System;
using UnityEngine;

// Token: 0x0200073D RID: 1853
public class TemporaryAccountGameMenu : UIBPopup, GameMenuInterface
{
	// Token: 0x060068DC RID: 26844 RVA: 0x00222AF0 File Offset: 0x00220CF0
	protected override void Awake()
	{
		base.Awake();
		this.m_destroyOnSceneLoad = false;
		this.m_gameMenuBase = new GameMenuBase();
		this.m_gameMenuBase.m_showCallback = new GameMenuBase.ShowCallback(this.Show);
		this.m_gameMenuBase.m_hideCallback = new GameMenuBase.HideCallback(this.Hide);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_showPosition = (this.m_gameMenuBase.UseKoreanRating() ? TemporaryAccountGameMenu.SHOW_POS_PHONE_KR : TemporaryAccountGameMenu.SHOW_POS_PHONE);
			this.m_showScale = (this.m_gameMenuBase.UseKoreanRating() ? TemporaryAccountGameMenu.SHOW_SCALE_PHONE_KR : TemporaryAccountGameMenu.SHOW_SCALE_PHONE);
		}
		this.m_signUpButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSignUpPressed));
		this.m_optionsButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOptionsPressed));
	}

	// Token: 0x060068DD RID: 26845 RVA: 0x00222BC1 File Offset: 0x00220DC1
	private void OnDestroy()
	{
		this.m_gameMenuBase.DestroyOptionsMenu();
	}

	// Token: 0x060068DE RID: 26846 RVA: 0x00210DD9 File Offset: 0x0020EFD9
	public bool GameMenuIsShown()
	{
		return this.IsShown();
	}

	// Token: 0x060068DF RID: 26847 RVA: 0x00222BCE File Offset: 0x00220DCE
	public void GameMenuShow()
	{
		this.Show();
	}

	// Token: 0x060068E0 RID: 26848 RVA: 0x00210DE1 File Offset: 0x0020EFE1
	public void GameMenuHide()
	{
		this.Hide();
	}

	// Token: 0x060068E1 RID: 26849 RVA: 0x00222BD6 File Offset: 0x00220DD6
	public void GameMenuShowOptionsMenu()
	{
		this.ShowOptionsMenu();
	}

	// Token: 0x060068E2 RID: 26850 RVA: 0x00036786 File Offset: 0x00034986
	public GameObject GameMenuGetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060068E3 RID: 26851 RVA: 0x00222BE0 File Offset: 0x00220DE0
	public override void Show()
	{
		if (base.IsShown())
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.Show(true);
		if (this.m_gameMenuBase.UseKoreanRating())
		{
			this.m_koreanRatings.SetActive(true);
		}
		base.gameObject.SetActive(true);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "TemporaryAccountGameMenuInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		TransformUtil.SetPosY(this.m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		BnetBar.Get().m_menuButton.SetSelected(true);
	}

	// Token: 0x060068E4 RID: 26852 RVA: 0x00222CE8 File Offset: 0x00220EE8
	public override void Hide()
	{
		this.Hide(false);
		BnetBar.Get().m_menuButton.SetSelected(false);
	}

	// Token: 0x060068E5 RID: 26853 RVA: 0x00222D04 File Offset: 0x00220F04
	protected override void Hide(bool animate)
	{
		if (!base.IsShown())
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(false);
		}
		base.Hide(animate);
	}

	// Token: 0x060068E6 RID: 26854 RVA: 0x00222D72 File Offset: 0x00220F72
	public void ShowOptionsMenu()
	{
		if (this.m_gameMenuBase != null)
		{
			this.m_gameMenuBase.ShowOptionsMenu();
		}
	}

	// Token: 0x060068E7 RID: 26855 RVA: 0x00222D87 File Offset: 0x00220F87
	private void OnSignUpPressed(UIEvent e)
	{
		this.Hide();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.GAME_MENU, null);
	}

	// Token: 0x060068E8 RID: 26856 RVA: 0x00222BD6 File Offset: 0x00220DD6
	private void OnOptionsPressed(UIEvent e)
	{
		this.ShowOptionsMenu();
	}

	// Token: 0x060068E9 RID: 26857 RVA: 0x00222D9B File Offset: 0x00220F9B
	private bool OnNavigateBack()
	{
		this.Hide();
		return true;
	}

	// Token: 0x060068EA RID: 26858 RVA: 0x00210DE1 File Offset: 0x0020EFE1
	private void OnInputBlockerRelease(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x040055E5 RID: 21989
	public UIBButton m_signUpButton;

	// Token: 0x040055E6 RID: 21990
	public UIBButton m_optionsButton;

	// Token: 0x040055E7 RID: 21991
	public GameObject m_koreanRatings;

	// Token: 0x040055E8 RID: 21992
	private static readonly Vector3 SHOW_POS_PHONE = new Vector3(0f, 0f, 0f);

	// Token: 0x040055E9 RID: 21993
	private static readonly Vector3 SHOW_SCALE_PHONE = new Vector3(75f, 75f, 75f);

	// Token: 0x040055EA RID: 21994
	private static readonly Vector3 SHOW_POS_PHONE_KR = new Vector3(0f, 0f, 15f);

	// Token: 0x040055EB RID: 21995
	private static readonly Vector3 SHOW_SCALE_PHONE_KR = new Vector3(65f, 65f, 65f);

	// Token: 0x040055EC RID: 21996
	private GameMenuBase m_gameMenuBase;

	// Token: 0x040055ED RID: 21997
	private PegUIElement m_inputBlockerPegUIElement;
}
