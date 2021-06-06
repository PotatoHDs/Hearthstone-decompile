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

// Token: 0x02000721 RID: 1825
public class StoreChallengePrompt : UIBPopup
{
	// Token: 0x14000063 RID: 99
	// (add) Token: 0x060065B7 RID: 26039 RVA: 0x0021186C File Offset: 0x0020FA6C
	// (remove) Token: 0x060065B8 RID: 26040 RVA: 0x002118A4 File Offset: 0x0020FAA4
	public event StoreChallengePrompt.CancelListener OnCancel;

	// Token: 0x14000064 RID: 100
	// (add) Token: 0x060065B9 RID: 26041 RVA: 0x002118DC File Offset: 0x0020FADC
	// (remove) Token: 0x060065BA RID: 26042 RVA: 0x00211914 File Offset: 0x0020FB14
	public event StoreChallengePrompt.CompleteListener OnChallengeComplete;

	// Token: 0x060065BB RID: 26043 RVA: 0x0021194C File Offset: 0x0020FB4C
	protected override void Awake()
	{
		base.Awake();
		this.m_inputText.RichText = false;
		this.m_submitButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSubmitPressed));
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
		this.m_infoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoPressed));
	}

	// Token: 0x060065BC RID: 26044 RVA: 0x002119B6 File Offset: 0x0020FBB6
	public IEnumerator Show(string challengeUrl)
	{
		this.m_challengeJson = null;
		this.m_challengeUrl = challengeUrl;
		if (this.IsShown())
		{
			yield break;
		}
		this.m_shown = true;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		IHttpRequest challenge = HttpRequestFactory.Get().CreateGetRequest(this.m_challengeUrl);
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
				Log.BattleNet.PrintInfo("Challenge json received: {0}", new object[]
				{
					challenge.ResponseAsString
				});
			}
			try
			{
				this.m_challengeJson = (JsonNode)Json.Deserialize(challenge.ResponseAsString);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				text = string.Format("{0}: {1}", ex.GetType().Name, ex.Message);
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			Log.BattleNet.PrintError("Tassadar Challenge Retrieval Failed: " + text, Array.Empty<object>());
			this.Hide(false);
			string header = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
			string message = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			CancelPurchase.CancelReason? reason = null;
			if (challenge.DidTimeout)
			{
				reason = new CancelPurchase.CancelReason?(CancelPurchase.CancelReason.CHALLENGE_TIMEOUT);
			}
			this.DisplayError(header, message, false, reason, text);
			yield break;
		}
		JsonNode jsonNode = (JsonNode)this.m_challengeJson["challenge"];
		this.m_challengeID = (string)this.m_challengeJson["challenge_id"];
		string text2 = (string)jsonNode["prompt"];
		this.m_challengeType = (string)jsonNode["type"];
		this.m_challengeInput = (JsonNode)((JsonList)jsonNode["inputs"])[0];
		JsonList jsonList = jsonNode.ContainsKey("errors") ? (jsonNode["errors"] as JsonList) : null;
		if (jsonList != null && jsonList.Count > 0)
		{
			string text3 = string.Join("\n", (from n in jsonList
			select (string)n).ToArray<string>());
			this.DisplayError((string)this.m_challengeInput["label"], text3, false, new CancelPurchase.CancelReason?(CancelPurchase.CancelReason.CHALLENGE_OTHER_ERROR), text3);
			yield break;
		}
		bool active = false;
		if (this.m_challengeType == "cvv")
		{
			active = true;
		}
		this.m_messageText.Text = text2;
		if (string.IsNullOrEmpty(this.m_messageText.Text))
		{
			Log.BattleNet.PrintError("Challenge has no prompt text, json received: {0}", new object[]
			{
				challenge.ResponseAsString
			});
		}
		this.m_infoButtonFrame.SetActive(active);
		this.m_input = string.Empty;
		this.UpdateInputText();
		base.DoShowAnimation(new UIBPopup.OnAnimationComplete(this.OnShown));
		yield break;
	}

	// Token: 0x060065BD RID: 26045 RVA: 0x002119CC File Offset: 0x0020FBCC
	public string HideChallenge()
	{
		string challengeID = this.m_challengeID;
		this.Hide(false);
		return challengeID;
	}

	// Token: 0x060065BE RID: 26046 RVA: 0x002119DB File Offset: 0x0020FBDB
	private void OnShown()
	{
		if (!this.IsShown())
		{
			return;
		}
		this.ShowInput();
	}

	// Token: 0x060065BF RID: 26047 RVA: 0x002119EC File Offset: 0x0020FBEC
	protected override void Hide(bool animate)
	{
		if (!this.IsShown())
		{
			return;
		}
		this.m_shown = false;
		this.HideInput();
		base.DoHideAnimation(!animate, new UIBPopup.OnAnimationComplete(this.OnHidden));
	}

	// Token: 0x060065C0 RID: 26048 RVA: 0x00211A1B File Offset: 0x0020FC1B
	protected override void OnHidden()
	{
		this.m_challengeID = null;
	}

	// Token: 0x060065C1 RID: 26049 RVA: 0x00211A24 File Offset: 0x0020FC24
	private void Cancel()
	{
		string challengeID = this.m_challengeID;
		this.Hide(true);
		if (this.OnCancel != null)
		{
			this.OnCancel(challengeID);
		}
	}

	// Token: 0x060065C2 RID: 26050 RVA: 0x00211A53 File Offset: 0x0020FC53
	private void OnSubmitPressed(UIEvent e)
	{
		base.StartCoroutine(this.SubmitChallenge());
	}

	// Token: 0x060065C3 RID: 26051 RVA: 0x00211A62 File Offset: 0x0020FC62
	private IEnumerator SubmitChallenge()
	{
		this.HideInput();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		dictionary["Content-Type"] = "application/json;charset=UTF-8";
		string text = (this.m_challengeInput == null) ? null : ((string)this.m_challengeInput["input_id"]);
		if (text == null)
		{
			text = "";
		}
		string value = (this.m_input == null) ? "" : this.m_input;
		string s = Json.Serialize(new JsonNode
		{
			{
				"inputs",
				new JsonList
				{
					new JsonNode
					{
						{
							"input_id",
							text
						},
						{
							"value",
							value
						}
					}
				}
			}
		});
		IHttpRequest challengeResponse = HttpRequestFactory.Get().CreatePostRequest(this.m_challengeUrl, Encoding.UTF8.GetBytes(s));
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
				Log.BattleNet.PrintInfo("Submit challenge response json received: {0}", new object[]
				{
					challengeResponse.ResponseAsString
				});
			}
			try
			{
				jsonNode = (JsonNode)Json.Deserialize(challengeResponse.ResponseAsString);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				text2 = string.Format("{0}: {1}", ex.GetType().Name, ex.Message);
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			Log.BattleNet.PrintError("Tassadar Challenge Submission Failed: " + text2, Array.Empty<object>());
			this.Hide(false);
			string header = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
			string message = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			CancelPurchase.CancelReason? reason = null;
			if (challengeResponse.DidTimeout)
			{
				reason = new CancelPurchase.CancelReason?(CancelPurchase.CancelReason.CHALLENGE_TIMEOUT);
			}
			this.DisplayError(header, message, false, reason, text2);
			yield break;
		}
		bool flag = (bool)jsonNode["done"];
		string challengeID = this.m_challengeID;
		if (!flag)
		{
			JsonNode jsonNode2 = jsonNode["challenge"] as JsonNode;
			JsonList source = jsonNode2.ContainsKey("errors") ? (jsonNode2["errors"] as JsonList) : new JsonList();
			string message2 = string.Join("\n", (from n in source
			select (string)n).ToArray<string>());
			this.DisplayError((string)this.m_challengeInput["label"], message2, true, null, null);
		}
		else
		{
			bool flag2 = true;
			text2 = (jsonNode.ContainsKey("error_code") ? (jsonNode["error_code"] as string) : null);
			if (!string.IsNullOrEmpty(text2))
			{
				flag2 = false;
			}
			if (flag2)
			{
				this.Hide(true);
				this.FireComplete(challengeID, flag2, null, text2);
			}
			else
			{
				string header2 = GameStrings.Get("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE");
				string message3 = GameStrings.Get("GLUE_STORE_FAIL_THROTTLED");
				CancelPurchase.CancelReason value2 = CancelPurchase.CancelReason.CHALLENGE_OTHER_ERROR;
				if (text2 == "DENIED")
				{
					value2 = CancelPurchase.CancelReason.CHALLENGE_DENIED;
					text2 = null;
				}
				this.DisplayError(header2, message3, false, new CancelPurchase.CancelReason?(value2), text2);
			}
		}
		yield break;
	}

	// Token: 0x060065C4 RID: 26052 RVA: 0x00211A74 File Offset: 0x0020FC74
	private void DisplayError(string header, string message, bool allowInputAgain, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		this.ClearInput();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_headerText = header;
		popupInfo.m_text = message;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		if (allowInputAgain)
		{
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				this.ShowInput();
			};
		}
		else
		{
			string challengeID = this.HideChallenge();
			this.FireComplete(challengeID, false, reason, internalErrorInfo);
		}
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060065C5 RID: 26053 RVA: 0x00211AE5 File Offset: 0x0020FCE5
	private void FireComplete(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		if (this.OnChallengeComplete != null)
		{
			this.OnChallengeComplete(challengeID, isSuccess, reason, internalErrorInfo);
		}
	}

	// Token: 0x060065C6 RID: 26054 RVA: 0x00211AFF File Offset: 0x0020FCFF
	private void OnCancelPressed(UIEvent e)
	{
		this.Cancel();
	}

	// Token: 0x060065C7 RID: 26055 RVA: 0x00211B07 File Offset: 0x0020FD07
	private void OnInfoPressed(UIEvent e)
	{
		Application.OpenURL(ExternalUrlService.Get().GetCVVLink());
	}

	// Token: 0x060065C8 RID: 26056 RVA: 0x00211B18 File Offset: 0x0020FD18
	private void ShowInput()
	{
		this.m_inputText.gameObject.SetActive(false);
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Bounds bounds = this.m_inputText.GetBounds();
		Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_password = true,
			m_rect = rect,
			m_updatedCallback = new UniversalInputManager.TextInputUpdatedCallback(this.OnInputUpdated),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(this.OnInputComplete),
			m_canceledCallback = new UniversalInputManager.TextInputCanceledCallback(this.OnInputCanceled),
			m_font = this.m_inputText.TrueTypeFont,
			m_alignment = new TextAnchor?(TextAnchor.MiddleCenter),
			m_maxCharacters = ((this.m_challengeInput != null) ? ((int)((long)this.m_challengeInput["max_length"])) : 0)
		};
		UniversalInputManager.Get().UseTextInput(parms, false);
		this.m_submitButton.SetEnabled(true, false);
	}

	// Token: 0x060065C9 RID: 26057 RVA: 0x00211C19 File Offset: 0x0020FE19
	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		this.m_inputText.gameObject.SetActive(true);
		this.m_submitButton.SetEnabled(false, false);
	}

	// Token: 0x060065CA RID: 26058 RVA: 0x00211C4A File Offset: 0x0020FE4A
	private void ClearInput()
	{
		UniversalInputManager.Get().SetInputText("", false);
	}

	// Token: 0x060065CB RID: 26059 RVA: 0x00211C5C File Offset: 0x0020FE5C
	private void OnInputUpdated(string input)
	{
		this.m_input = input;
		this.UpdateInputText();
	}

	// Token: 0x060065CC RID: 26060 RVA: 0x00211C6B File Offset: 0x0020FE6B
	private void OnInputComplete(string input)
	{
		this.m_input = input;
		this.UpdateInputText();
		base.StartCoroutine(this.SubmitChallenge());
	}

	// Token: 0x060065CD RID: 26061 RVA: 0x00211C87 File Offset: 0x0020FE87
	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		this.m_input = string.Empty;
		this.UpdateInputText();
		this.Cancel();
	}

	// Token: 0x060065CE RID: 26062 RVA: 0x00211CA0 File Offset: 0x0020FEA0
	private void UpdateInputText()
	{
		StringBuilder stringBuilder = new StringBuilder(this.m_input.Length);
		for (int i = 0; i < this.m_input.Length; i++)
		{
			stringBuilder.Append('*');
		}
		this.m_inputText.Text = stringBuilder.ToString();
	}

	// Token: 0x0400545D RID: 21597
	public UIBButton m_submitButton;

	// Token: 0x0400545E RID: 21598
	public UIBButton m_cancelButton;

	// Token: 0x0400545F RID: 21599
	public UberText m_messageText;

	// Token: 0x04005460 RID: 21600
	public UberText m_inputText;

	// Token: 0x04005461 RID: 21601
	public GameObject m_infoButtonFrame;

	// Token: 0x04005462 RID: 21602
	public UIBButton m_infoButton;

	// Token: 0x04005463 RID: 21603
	private const int TASSADAR_CHALLENGE_TIMEOUT_SECONDS = 15;

	// Token: 0x04005464 RID: 21604
	private string m_input = string.Empty;

	// Token: 0x04005465 RID: 21605
	private string m_challengeID;

	// Token: 0x04005466 RID: 21606
	private string m_challengeUrl;

	// Token: 0x04005467 RID: 21607
	private JsonNode m_challengeJson;

	// Token: 0x04005468 RID: 21608
	private JsonNode m_challengeInput;

	// Token: 0x04005469 RID: 21609
	private string m_challengeType;

	// Token: 0x020022B3 RID: 8883
	// (Invoke) Token: 0x0601283A RID: 75834
	public delegate void CancelListener(string challengeID);

	// Token: 0x020022B4 RID: 8884
	// (Invoke) Token: 0x0601283E RID: 75838
	public delegate void CompleteListener(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo);
}
