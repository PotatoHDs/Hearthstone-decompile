using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class AddFriendFrame : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600064D RID: 1613 RVA: 0x00024B20 File Offset: 0x00022D20
	// (remove) Token: 0x0600064E RID: 1614 RVA: 0x00024B58 File Offset: 0x00022D58
	public event Action Closed;

	// Token: 0x0600064F RID: 1615 RVA: 0x00024B90 File Offset: 0x00022D90
	private void Awake()
	{
		this.InitItems();
		this.Layout();
		this.InitInput();
		this.InitInputTextField();
		DialogManager.Get().OnDialogShown += this.OnDialogShown;
		DialogManager.Get().OnDialogHidden += this.OnDialogHidden;
		this.m_RecentOpponent.button.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRecentOpponentButtonReleased));
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00024C00 File Offset: 0x00022E00
	private void Start()
	{
		this.m_InputTextField.SetInputFont(this.m_localizedInputFont);
		this.m_InputTextField.Activate();
		this.UpdateRecentOpponent();
		if (DialogManager.Get().ShowingDialog())
		{
			return;
		}
		this.m_InputTextField.Text = this.m_inputText;
		this.UpdateInstructions();
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00024C53 File Offset: 0x00022E53
	private void OnDestroy()
	{
		DialogManager.Get().OnDialogShown -= this.OnDialogShown;
		DialogManager.Get().OnDialogHidden -= this.OnDialogHidden;
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00024C84 File Offset: 0x00022E84
	private void InitInput()
	{
		FontDefinition fontDef = FontTable.Get().GetFontDef(this.m_InputFont);
		if (fontDef == null)
		{
			this.m_localizedInputFont = this.m_InputFont;
			return;
		}
		this.m_localizedInputFont = fontDef.m_Font;
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00024CC4 File Offset: 0x00022EC4
	public void UpdateLayout()
	{
		this.Layout();
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00024CCC File Offset: 0x00022ECC
	public void Close()
	{
		if (this.m_inputBlocker != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlocker.gameObject);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00024CF8 File Offset: 0x00022EF8
	public void SetPlayer(BnetPlayer player)
	{
		this.m_player = player;
		if (player == null)
		{
			this.m_usePlayer = false;
			this.m_playerDisplayName = null;
		}
		else
		{
			this.m_usePlayer = true;
			this.m_playerDisplayName = FriendUtils.GetUniqueName(this.m_player);
		}
		if (DialogManager.Get().ShowingDialog())
		{
			this.SaveAndHideText(this.m_playerDisplayName);
			return;
		}
		this.m_inputText = this.m_playerDisplayName;
		this.m_InputTextField.Text = this.m_inputText;
		this.UpdateInstructions();
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00024D74 File Offset: 0x00022F74
	public void UpdateRecentOpponent()
	{
		BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
		if (recentOpponent == null)
		{
			this.m_RecentOpponent.button.gameObject.SetActive(false);
			return;
		}
		this.m_RecentOpponent.button.gameObject.SetActive(true);
		this.m_RecentOpponent.nameText.Text = FriendUtils.GetUniqueNameWithColor(recentOpponent);
		this.AdjustHeaderTextPositionBasedOnBattletagLength();
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00024DD8 File Offset: 0x00022FD8
	private void OnRecentOpponentButtonReleased(UIEvent e)
	{
		if (!string.IsNullOrEmpty(this.m_RecentOpponent.nameText.Text))
		{
			BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
			this.SetPlayer(recentOpponent);
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00024E10 File Offset: 0x00023010
	private void InitItems()
	{
		this.m_HeaderText.Text = GameStrings.Get("GLOBAL_ADDFRIEND_HEADER");
		this.m_InstructionText.Text = GameStrings.Get("GLOBAL_ADDFRIEND_INSTRUCTION");
		this.m_initialLastPlayedTextPositionX = this.m_LastPlayedText.transform.localPosition.x;
		this.InitInputBlocker();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00024E68 File Offset: 0x00023068
	private void Layout()
	{
		base.transform.parent = BaseUI.Get().transform;
		base.transform.position = BaseUI.Get().GetAddFriendBone().position;
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 100f, base.transform.position.z);
			base.transform.position = position;
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00024F0F File Offset: 0x0002310F
	private void UpdateInstructions()
	{
		if (this.m_InstructionText != null)
		{
			this.m_InstructionText.gameObject.SetActive(string.IsNullOrEmpty(this.m_inputText) && string.IsNullOrEmpty(Input.compositionString));
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00024F4C File Offset: 0x0002314C
	private void AdjustHeaderTextPositionBasedOnBattletagLength()
	{
		float x = this.m_RecentOpponent.nameText.GetBounds().size.x;
		float x2 = this.m_RecentOpponent.nameText.GetTextBounds().size.x;
		float num = x - x2;
		if (base.transform.lossyScale.x != 0f)
		{
			num /= base.transform.lossyScale.x;
		}
		this.m_LastPlayedText.transform.localPosition = new Vector3(this.m_initialLastPlayedTextPositionX + num, this.m_LastPlayedText.transform.localPosition.y, this.m_LastPlayedText.transform.localPosition.z);
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00025008 File Offset: 0x00023208
	private void InitInputBlocker()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "AddFriendInputBlocker");
		gameObject.transform.parent = base.transform.parent;
		this.m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerReleased));
		TransformUtil.SetPosZ(this.m_inputBlocker, base.transform.position.z + 1f);
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0002508C File Offset: 0x0002328C
	private void OnInputBlockerReleased(UIEvent e)
	{
		this.OnClosed();
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00025094 File Offset: 0x00023294
	private void InitInputTextField()
	{
		this.m_InputTextField.Preprocess += this.OnInputPreprocess;
		this.m_InputTextField.Changed += this.OnInputChanged;
		this.m_InputTextField.Submitted += this.OnInputSubmitted;
		this.m_InputTextField.Canceled += this.OnInputCanceled;
		this.m_InstructionText.gameObject.SetActive(true);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002510E File Offset: 0x0002330E
	private void OnInputPreprocess(Event e)
	{
		if (Input.imeIsSelected)
		{
			this.UpdateInstructions();
		}
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0002511D File Offset: 0x0002331D
	private void OnInputChanged(string text)
	{
		this.m_inputText = text;
		this.UpdateInstructions();
		this.m_usePlayer = (string.Compare(this.m_playerDisplayName, text.Trim(), true) == 0);
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x00025148 File Offset: 0x00023348
	private void OnInputSubmitted(string input)
	{
		string name = this.m_usePlayer ? this.m_player.GetBattleTag().ToString() : input.Trim();
		if (!BnetFriendMgr.Get().SendInvite(name))
		{
			string message = GameStrings.Get("GLOBAL_ADDFRIEND_ERROR_MALFORMED");
			UIStatus.Get().AddError(message, -1f);
		}
		this.OnClosed();
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002508C File Offset: 0x0002328C
	private void OnInputCanceled()
	{
		this.OnClosed();
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x000251A4 File Offset: 0x000233A4
	private void OnClosed()
	{
		if (this.Closed != null)
		{
			this.Closed();
		}
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x000251B9 File Offset: 0x000233B9
	private void SaveAndHideText(string text)
	{
		this.m_inputText = text;
		this.m_InputTextField.Text = string.Empty;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x000251D2 File Offset: 0x000233D2
	private void ShowSavedText()
	{
		this.m_InputTextField.Text = this.m_inputText;
		this.UpdateInstructions();
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x000251EB File Offset: 0x000233EB
	private void OnDialogShown()
	{
		this.SaveAndHideText(this.m_inputText);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x000251F9 File Offset: 0x000233F9
	private void OnDialogHidden()
	{
		this.ShowSavedText();
	}

	// Token: 0x04000461 RID: 1121
	public AddFriendFrameBones m_Bones;

	// Token: 0x04000462 RID: 1122
	public UberText m_HeaderText;

	// Token: 0x04000463 RID: 1123
	public UberText m_InstructionText;

	// Token: 0x04000464 RID: 1124
	public TextField m_InputTextField;

	// Token: 0x04000465 RID: 1125
	public Font m_InputFont;

	// Token: 0x04000466 RID: 1126
	public RecentOpponent m_RecentOpponent;

	// Token: 0x04000467 RID: 1127
	public UberText m_LastPlayedText;

	// Token: 0x04000468 RID: 1128
	private PegUIElement m_inputBlocker;

	// Token: 0x04000469 RID: 1129
	private string m_inputText = string.Empty;

	// Token: 0x0400046A RID: 1130
	private BnetPlayer m_player;

	// Token: 0x0400046B RID: 1131
	private bool m_usePlayer;

	// Token: 0x0400046C RID: 1132
	private string m_playerDisplayName;

	// Token: 0x0400046D RID: 1133
	private Font m_localizedInputFont;

	// Token: 0x0400046E RID: 1134
	private float m_initialLastPlayedTextPositionX;
}
