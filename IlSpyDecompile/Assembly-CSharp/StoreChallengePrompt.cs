using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hearthstone;
using Hearthstone.Http;
using MiniJSON;
using PegasusUtil;
using UnityEngine;

public class StoreChallengePrompt : UIBPopup
{
	public delegate void CancelListener(string challengeID);

	public delegate void CompleteListener(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo);

	public UIBButton m_submitButton;

	public UIBButton m_cancelButton;

	public UberText m_messageText;

	public UberText m_inputText;

	public GameObject m_infoButtonFrame;

	public UIBButton m_infoButton;

	private const int TASSADAR_CHALLENGE_TIMEOUT_SECONDS = 15;

	private string m_input = string.Empty;

	private string m_challengeID;

	private string m_challengeUrl;

	private JsonNode m_challengeJson;

	private JsonNode m_challengeInput;

	private string m_challengeType;

	public event CancelListener OnCancel;

	public event CompleteListener OnChallengeComplete;

	protected override void Awake()
	{
		base.Awake();
		m_inputText.RichText = false;
		m_submitButton.AddEventListener(UIEventType.RELEASE, OnSubmitPressed);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
		m_infoButton.AddEventListener(UIEventType.RELEASE, OnInfoPressed);
	}

	public IEnumerator Show(string challengeUrl)
	{
		m_challengeJson = null;
		m_challengeUrl = challengeUrl;
		if (IsShown())
		{
			yield break;
		}
		m_shown = true;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		IHttpRequest challenge = HttpRequestFactory.Get().CreateGetRequest(m_challengeUrl);
		challenge.SetRequestHeaders(dictionary);
		challenge.TimeoutSeconds = 15;
		yield return challenge.SendRequest();
		string text = null;
		if (challenge.IsNetworkError || challenge.IsHttpError)
		{
			text = challenge.ErrorString;
		}
		else if (string.IsNullOrEmpty(challenge.ResponseAsString))
		{
			text = "Empty Response";
		}
		else
		{
			if (HearthstoneApplication.IsInternal())
			{
				Log.BattleNet.PrintInfo("Challenge json received: {0}", challenge.ResponseAsString);
			}
			try
			{
				m_challengeJson = (JsonNode)Json.Deserialize(challenge.ResponseAsString);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				text = $"{ex.GetType().Name}: {ex.Message}";
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			Log.BattleNet.PrintError("Tassadar Challenge Retrieval Failed: " + text);
			Hide(animate: false);
			string header = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
			string message = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			CancelPurchase.CancelReason? reason = null;
			if (challenge.DidTimeout)
			{
				reason = CancelPurchase.CancelReason.CHALLENGE_TIMEOUT;
			}
			DisplayError(header, message, allowInputAgain: false, reason, text);
			yield break;
		}
		JsonNode jsonNode = (JsonNode)m_challengeJson["challenge"];
		m_challengeID = (string)m_challengeJson["challenge_id"];
		string text2 = (string)jsonNode["prompt"];
		m_challengeType = (string)jsonNode["type"];
		m_challengeInput = (JsonNode)((JsonList)jsonNode["inputs"])[0];
		JsonList jsonList = (jsonNode.ContainsKey("errors") ? (jsonNode["errors"] as JsonList) : null);
		if (jsonList != null && jsonList.Count > 0)
		{
			string text3 = string.Join("\n", jsonList.Select((object n) => (string)n).ToArray());
			DisplayError((string)m_challengeInput["label"], text3, allowInputAgain: false, CancelPurchase.CancelReason.CHALLENGE_OTHER_ERROR, text3);
			yield break;
		}
		bool active = false;
		if (m_challengeType == "cvv")
		{
			active = true;
		}
		m_messageText.Text = text2;
		if (string.IsNullOrEmpty(m_messageText.Text))
		{
			Log.BattleNet.PrintError("Challenge has no prompt text, json received: {0}", challenge.ResponseAsString);
		}
		m_infoButtonFrame.SetActive(active);
		m_input = string.Empty;
		UpdateInputText();
		DoShowAnimation(OnShown);
	}

	public string HideChallenge()
	{
		string challengeID = m_challengeID;
		Hide(animate: false);
		return challengeID;
	}

	private void OnShown()
	{
		if (IsShown())
		{
			ShowInput();
		}
	}

	protected override void Hide(bool animate)
	{
		if (IsShown())
		{
			m_shown = false;
			HideInput();
			DoHideAnimation(!animate, OnHidden);
		}
	}

	protected override void OnHidden()
	{
		m_challengeID = null;
	}

	private void Cancel()
	{
		string challengeID = m_challengeID;
		Hide(animate: true);
		if (this.OnCancel != null)
		{
			this.OnCancel(challengeID);
		}
	}

	private void OnSubmitPressed(UIEvent e)
	{
		StartCoroutine(SubmitChallenge());
	}

	private IEnumerator SubmitChallenge()
	{
		HideInput();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		dictionary["Content-Type"] = "application/json;charset=UTF-8";
		string text = ((m_challengeInput == null) ? null : ((string)m_challengeInput["input_id"]));
		if (text == null)
		{
			text = "";
		}
		string value = ((m_input == null) ? "" : m_input);
		string s = Json.Serialize(new JsonNode { 
		{
			"inputs",
			new JsonList
			{
				new JsonNode
				{
					{ "input_id", text },
					{ "value", value }
				}
			}
		} });
		IHttpRequest challengeResponse = HttpRequestFactory.Get().CreatePostRequest(m_challengeUrl, Encoding.UTF8.GetBytes(s));
		challengeResponse.SetRequestHeaders(dictionary);
		challengeResponse.TimeoutSeconds = 15;
		yield return challengeResponse.SendRequest();
		JsonNode jsonNode = null;
		string text2 = null;
		if (challengeResponse.IsNetworkError || challengeResponse.IsHttpError)
		{
			text2 = challengeResponse.ErrorString;
		}
		else if (string.IsNullOrEmpty(challengeResponse.ResponseAsString))
		{
			text2 = "Empty Response";
		}
		else
		{
			if (HearthstoneApplication.IsInternal())
			{
				Log.BattleNet.PrintInfo("Submit challenge response json received: {0}", challengeResponse.ResponseAsString);
			}
			try
			{
				jsonNode = (JsonNode)Json.Deserialize(challengeResponse.ResponseAsString);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				text2 = $"{ex.GetType().Name}: {ex.Message}";
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			Log.BattleNet.PrintError("Tassadar Challenge Submission Failed: " + text2);
			Hide(animate: false);
			string header = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
			string message = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			CancelPurchase.CancelReason? reason = null;
			if (challengeResponse.DidTimeout)
			{
				reason = CancelPurchase.CancelReason.CHALLENGE_TIMEOUT;
			}
			DisplayError(header, message, allowInputAgain: false, reason, text2);
			yield break;
		}
		bool num = (bool)jsonNode["done"];
		string challengeID = m_challengeID;
		if (!num)
		{
			JsonNode jsonNode2 = jsonNode["challenge"] as JsonNode;
			JsonList source = (jsonNode2.ContainsKey("errors") ? (jsonNode2["errors"] as JsonList) : new JsonList());
			string message2 = string.Join("\n", source.Select((object n) => (string)n).ToArray());
			DisplayError((string)m_challengeInput["label"], message2, allowInputAgain: true, null, null);
			yield break;
		}
		bool flag = true;
		text2 = (jsonNode.ContainsKey("error_code") ? (jsonNode["error_code"] as string) : null);
		if (!string.IsNullOrEmpty(text2))
		{
			flag = false;
		}
		if (flag)
		{
			Hide(animate: true);
			FireComplete(challengeID, flag, null, text2);
			yield break;
		}
		string header2 = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
		string message3 = GameStrings.Get("GLUE_STORE_FAIL_THROTTLED");
		CancelPurchase.CancelReason value2 = CancelPurchase.CancelReason.CHALLENGE_OTHER_ERROR;
		if (text2 == "DENIED")
		{
			value2 = CancelPurchase.CancelReason.CHALLENGE_DENIED;
			text2 = null;
		}
		DisplayError(header2, message3, allowInputAgain: false, value2, text2);
	}

	private void DisplayError(string header, string message, bool allowInputAgain, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		ClearInput();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_headerText = header;
		popupInfo.m_text = message;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		if (allowInputAgain)
		{
			popupInfo.m_responseCallback = delegate
			{
				ShowInput();
			};
		}
		else
		{
			string challengeID = HideChallenge();
			FireComplete(challengeID, isSuccess: false, reason, internalErrorInfo);
		}
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void FireComplete(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		if (this.OnChallengeComplete != null)
		{
			this.OnChallengeComplete(challengeID, isSuccess, reason, internalErrorInfo);
		}
	}

	private void OnCancelPressed(UIEvent e)
	{
		Cancel();
	}

	private void OnInfoPressed(UIEvent e)
	{
		Application.OpenURL(ExternalUrlService.Get().GetCVVLink());
	}

	private void ShowInput()
	{
		m_inputText.gameObject.SetActive(value: false);
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Bounds bounds = m_inputText.GetBounds();
		Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_password = true,
			m_rect = rect,
			m_updatedCallback = OnInputUpdated,
			m_completedCallback = OnInputComplete,
			m_canceledCallback = OnInputCanceled,
			m_font = m_inputText.TrueTypeFont,
			m_alignment = TextAnchor.MiddleCenter,
			m_maxCharacters = (int)((m_challengeInput != null) ? ((long)m_challengeInput["max_length"]) : 0)
		};
		UniversalInputManager.Get().UseTextInput(parms);
		m_submitButton.SetEnabled(enabled: true);
	}

	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject);
		m_inputText.gameObject.SetActive(value: true);
		m_submitButton.SetEnabled(enabled: false);
	}

	private void ClearInput()
	{
		UniversalInputManager.Get().SetInputText("");
	}

	private void OnInputUpdated(string input)
	{
		m_input = input;
		UpdateInputText();
	}

	private void OnInputComplete(string input)
	{
		m_input = input;
		UpdateInputText();
		StartCoroutine(SubmitChallenge());
	}

	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		m_input = string.Empty;
		UpdateInputText();
		Cancel();
	}

	private void UpdateInputText()
	{
		StringBuilder stringBuilder = new StringBuilder(m_input.Length);
		for (int i = 0; i < m_input.Length; i++)
		{
			stringBuilder.Append('*');
		}
		m_inputText.Text = stringBuilder.ToString();
	}
}
