using System;
using UnityEngine;

public class AddFriendFrame : MonoBehaviour
{
	public AddFriendFrameBones m_Bones;

	public UberText m_HeaderText;

	public UberText m_InstructionText;

	public TextField m_InputTextField;

	public Font m_InputFont;

	public RecentOpponent m_RecentOpponent;

	public UberText m_LastPlayedText;

	private PegUIElement m_inputBlocker;

	private string m_inputText = string.Empty;

	private BnetPlayer m_player;

	private bool m_usePlayer;

	private string m_playerDisplayName;

	private Font m_localizedInputFont;

	private float m_initialLastPlayedTextPositionX;

	public event Action Closed;

	private void Awake()
	{
		InitItems();
		Layout();
		InitInput();
		InitInputTextField();
		DialogManager.Get().OnDialogShown += OnDialogShown;
		DialogManager.Get().OnDialogHidden += OnDialogHidden;
		m_RecentOpponent.button.AddEventListener(UIEventType.RELEASE, OnRecentOpponentButtonReleased);
	}

	private void Start()
	{
		m_InputTextField.SetInputFont(m_localizedInputFont);
		m_InputTextField.Activate();
		UpdateRecentOpponent();
		if (!DialogManager.Get().ShowingDialog())
		{
			m_InputTextField.Text = m_inputText;
			UpdateInstructions();
		}
	}

	private void OnDestroy()
	{
		DialogManager.Get().OnDialogShown -= OnDialogShown;
		DialogManager.Get().OnDialogHidden -= OnDialogHidden;
	}

	private void InitInput()
	{
		FontDefinition fontDef = FontTable.Get().GetFontDef(m_InputFont);
		if (fontDef == null)
		{
			m_localizedInputFont = m_InputFont;
		}
		else
		{
			m_localizedInputFont = fontDef.m_Font;
		}
	}

	public void UpdateLayout()
	{
		Layout();
	}

	public void Close()
	{
		if (m_inputBlocker != null)
		{
			UnityEngine.Object.Destroy(m_inputBlocker.gameObject);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void SetPlayer(BnetPlayer player)
	{
		m_player = player;
		if (player == null)
		{
			m_usePlayer = false;
			m_playerDisplayName = null;
		}
		else
		{
			m_usePlayer = true;
			m_playerDisplayName = FriendUtils.GetUniqueName(m_player);
		}
		if (DialogManager.Get().ShowingDialog())
		{
			SaveAndHideText(m_playerDisplayName);
			return;
		}
		m_inputText = m_playerDisplayName;
		m_InputTextField.Text = m_inputText;
		UpdateInstructions();
	}

	public void UpdateRecentOpponent()
	{
		BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
		if (recentOpponent == null)
		{
			m_RecentOpponent.button.gameObject.SetActive(value: false);
			return;
		}
		m_RecentOpponent.button.gameObject.SetActive(value: true);
		m_RecentOpponent.nameText.Text = FriendUtils.GetUniqueNameWithColor(recentOpponent);
		AdjustHeaderTextPositionBasedOnBattletagLength();
	}

	private void OnRecentOpponentButtonReleased(UIEvent e)
	{
		if (!string.IsNullOrEmpty(m_RecentOpponent.nameText.Text))
		{
			BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
			SetPlayer(recentOpponent);
		}
	}

	private void InitItems()
	{
		m_HeaderText.Text = GameStrings.Get("GLOBAL_ADDFRIEND_HEADER");
		m_InstructionText.Text = GameStrings.Get("GLOBAL_ADDFRIEND_INSTRUCTION");
		m_initialLastPlayedTextPositionX = m_LastPlayedText.transform.localPosition.x;
		InitInputBlocker();
	}

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

	private void UpdateInstructions()
	{
		if (m_InstructionText != null)
		{
			m_InstructionText.gameObject.SetActive(string.IsNullOrEmpty(m_inputText) && string.IsNullOrEmpty(Input.compositionString));
		}
	}

	private void AdjustHeaderTextPositionBasedOnBattletagLength()
	{
		float x = m_RecentOpponent.nameText.GetBounds().size.x;
		float x2 = m_RecentOpponent.nameText.GetTextBounds().size.x;
		float num = x - x2;
		if (base.transform.lossyScale.x != 0f)
		{
			num /= base.transform.lossyScale.x;
		}
		m_LastPlayedText.transform.localPosition = new Vector3(m_initialLastPlayedTextPositionX + num, m_LastPlayedText.transform.localPosition.y, m_LastPlayedText.transform.localPosition.z);
	}

	private void InitInputBlocker()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "AddFriendInputBlocker");
		gameObject.transform.parent = base.transform.parent;
		m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		m_inputBlocker.AddEventListener(UIEventType.RELEASE, OnInputBlockerReleased);
		TransformUtil.SetPosZ(m_inputBlocker, base.transform.position.z + 1f);
	}

	private void OnInputBlockerReleased(UIEvent e)
	{
		OnClosed();
	}

	private void InitInputTextField()
	{
		m_InputTextField.Preprocess += OnInputPreprocess;
		m_InputTextField.Changed += OnInputChanged;
		m_InputTextField.Submitted += OnInputSubmitted;
		m_InputTextField.Canceled += OnInputCanceled;
		m_InstructionText.gameObject.SetActive(value: true);
	}

	private void OnInputPreprocess(Event e)
	{
		if (Input.imeIsSelected)
		{
			UpdateInstructions();
		}
	}

	private void OnInputChanged(string text)
	{
		m_inputText = text;
		UpdateInstructions();
		m_usePlayer = string.Compare(m_playerDisplayName, text.Trim(), ignoreCase: true) == 0;
	}

	private void OnInputSubmitted(string input)
	{
		string text = (m_usePlayer ? m_player.GetBattleTag().ToString() : input.Trim());
		if (!BnetFriendMgr.Get().SendInvite(text))
		{
			string message = GameStrings.Get("GLOBAL_ADDFRIEND_ERROR_MALFORMED");
			UIStatus.Get().AddError(message);
		}
		OnClosed();
	}

	private void OnInputCanceled()
	{
		OnClosed();
	}

	private void OnClosed()
	{
		if (this.Closed != null)
		{
			this.Closed();
		}
	}

	private void SaveAndHideText(string text)
	{
		m_inputText = text;
		m_InputTextField.Text = string.Empty;
	}

	private void ShowSavedText()
	{
		m_InputTextField.Text = m_inputText;
		UpdateInstructions();
	}

	private void OnDialogShown()
	{
		SaveAndHideText(m_inputText);
	}

	private void OnDialogHidden()
	{
		ShowSavedText();
	}
}
