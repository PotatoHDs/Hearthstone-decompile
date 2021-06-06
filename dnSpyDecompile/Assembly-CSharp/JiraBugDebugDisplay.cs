using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Blizzard.T5.AssetManager;
using Hearthstone;
using Hearthstone.Core;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200029E RID: 670
public class JiraBugDebugDisplay : MonoBehaviour
{
	// Token: 0x060021C7 RID: 8647 RVA: 0x000A66B0 File Offset: 0x000A48B0
	public static JiraBugDebugDisplay Get()
	{
		if (JiraBugDebugDisplay.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			JiraBugDebugDisplay.s_instance = gameObject.AddComponent<JiraBugDebugDisplay>();
			gameObject.name = "JIRABugDebugDisplay (Dynamically created)";
			AssetLoader.Get().LoadAsset<Texture>(JiraBugDebugDisplay.s_backgroundTexture, new AssetHandleCallback<Texture>(JiraBugDebugDisplay.s_instance.OnTextureLoad), null, AssetLoadingOptions.None);
			JiraBugDebugDisplay.s_instance.m_debugTextStyle = new GUIStyle("box");
			JiraBugDebugDisplay.s_instance.m_debugTextStyle.fontSize = 16;
			JiraBugDebugDisplay.s_instance.m_debugTextStyle.normal.textColor = Color.white;
			JiraBugDebugDisplay.s_instance.m_debugTextStyle.alignment = TextAnchor.MiddleLeft;
		}
		return JiraBugDebugDisplay.s_instance;
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x000A6761 File Offset: 0x000A4961
	private void OnTextureLoad(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
	{
		AssetHandle.Take<Texture>(ref this.m_loadedBackgroundTexture, loadedTexture);
		JiraBugDebugDisplay.s_instance.m_debugTextStyle.normal.background = (Texture2D)this.m_loadedBackgroundTexture.Asset;
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x000A6794 File Offset: 0x000A4994
	private void OnGUI()
	{
		if (JiraBugDebugDisplay.s_instance == null)
		{
			return;
		}
		if (Event.current.type == EventType.KeyUp)
		{
			bool flag = false;
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				flag = Event.current.control;
			}
			else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			{
				flag = Event.current.alt;
			}
			if (Event.current.keyCode == KeyCode.F2 && flag && Time.fixedUnscaledTime > this.m_lastHotkeyInvokeTime + 1f)
			{
				this.m_lastHotkeyInvokeTime = Time.fixedUnscaledTime;
				this.LoadBugsInBrowser();
			}
		}
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x000A682E File Offset: 0x000A4A2E
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedBackgroundTexture);
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x000A683B File Offset: 0x000A4A3B
	private void LoadBugsInBrowser()
	{
		Application.OpenURL(this.GetSearchURL(this.m_currentCard, false));
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x000A684F File Offset: 0x000A4A4F
	private bool GetBugsForCard(string cardid, out string bugs)
	{
		this.m_bugcache.TryGetValue(cardid, out bugs);
		if (string.IsNullOrWhiteSpace(bugs))
		{
			bugs = "No Issues Found";
			return false;
		}
		return !(bugs == "Loading...");
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x000A6884 File Offset: 0x000A4A84
	private string GetSearchURL(string search, bool useApiEndpoint = true)
	{
		string str;
		if (useApiEndpoint)
		{
			str = "https://jira.blizzard.com/rest/api/2/search/?jql=";
		}
		else
		{
			str = "https://jira.blizzard.com/issues/?jql=";
		}
		string s = string.Format("summary ~ \"{0}\" and status != closed and issuetype=Bug", search);
		return str + UnityWebRequest.EscapeURL(s);
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x000A68BC File Offset: 0x000A4ABC
	private IEnumerator SearchJira(string search)
	{
		if (!this.m_bugcache.ContainsKey(search))
		{
			this.m_remoteRequestCount++;
			this.m_bugcache.TryAdd(search, "Loading...");
			string searchURL = this.GetSearchURL(search, true);
			UnityWebRequest request = new UnityWebRequest(searchURL, "GET");
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Authorization", "");
			request.useHttpContinue = false;
			yield return request.SendWebRequest();
			this.m_bugcache.TryUpdate(search, this.ParseJiraSearchResults(request), "Loading...");
			request = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x000A68D4 File Offset: 0x000A4AD4
	private string ParseJiraSearchResults(UnityWebRequest request)
	{
		StringBuilder stringBuilder = new StringBuilder();
		JsonNode jsonNode = Json.Deserialize(request.downloadHandler.text) as JsonNode;
		if (jsonNode != null && jsonNode.Count > 0)
		{
			if (!jsonNode.ContainsKey("total"))
			{
				return string.Empty;
			}
			long num = (long)jsonNode["total"];
			if (num == 0L)
			{
				return string.Empty;
			}
			JsonList jsonList = jsonNode["issues"] as JsonList;
			int num2 = 0;
			while ((long)num2 < num)
			{
				JsonNode jsonNode2 = jsonList[num2] as JsonNode;
				string text = jsonNode2["key"] as string;
				JsonNode jsonNode3 = jsonNode2["fields"] as JsonNode;
				stringBuilder.Append(text.PadRight(11));
				stringBuilder.Append(" - ");
				stringBuilder.AppendLine(jsonNode3["summary"] as string);
				num2++;
			}
			stringBuilder.Length--;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x000A69D6 File Offset: 0x000A4BD6
	public bool EnableDebugDisplay(string func, string[] args, string rawArgs)
	{
		this.m_isEnabled = true;
		return true;
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x000A69E0 File Offset: 0x000A4BE0
	public bool DisableDebugDisplay(string func, string[] args, string rawArgs)
	{
		this.m_isEnabled = false;
		this.m_bugcache.Clear();
		return true;
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x000A69F8 File Offset: 0x000A4BF8
	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (!this.m_isEnabled)
		{
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		gameState.GetEntityMap();
		Card mousedOverCard = InputManager.Get().GetMousedOverCard();
		Entity entity = null;
		if (mousedOverCard != null && mousedOverCard.GetEntity() != null)
		{
			entity = mousedOverCard.GetEntity();
		}
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag == TAG_ZONE.HAND || zone.m_ServerTag == TAG_ZONE.PLAY || zone.m_ServerTag == TAG_ZONE.SECRET)
			{
				foreach (Card card in zone.GetCards())
				{
					Entity entity2 = card.GetEntity();
					if (entity == null || entity == entity2)
					{
						Vector3 vector = card.transform.position;
						if (zone.m_ServerTag == TAG_ZONE.HAND)
						{
							Vector3 vector2 = card.transform.forward;
							if (card.GetControllerSide() == Player.Side.OPPOSING)
							{
								vector2 *= -1.5f;
								if (card.GetController().IsRevealed())
								{
									vector2 = -vector2;
								}
							}
							vector += vector2;
						}
						if (entity != null)
						{
							string cardId = card.GetEntity().GetCardId();
							if (string.IsNullOrEmpty(cardId))
							{
								return;
							}
							Processor.RunCoroutine(this.SearchJira(cardId), null);
							this.SetCurrentCard(cardId);
							this.DrawDebugTextForHighlightedCard(entity2, DebugTextManager.WorldPosToScreenPos(vector), false, false);
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x000A6BA4 File Offset: 0x000A4DA4
	private void SetCurrentCard(string cardid)
	{
		JiraBugDebugDisplay.s_instance.m_currentCard = cardid;
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x000A6BB4 File Offset: 0x000A4DB4
	private void DrawDebugTextForHighlightedCard(Entity ent, Vector3 pos, bool screenSpace = false, bool forceShowZeroTags = false)
	{
		string text;
		if (this.GetBugsForCard(ent.GetCardId(), out text))
		{
			text = "Press ALT+F2 to view in JIRA\n" + text;
		}
		DebugTextManager.Get().DrawDebugText(text, pos, 0f, screenSpace, "", this.m_debugTextStyle);
	}

	// Token: 0x040012A9 RID: 4777
	private const string s_loadingText = "Loading...";

	// Token: 0x040012AA RID: 4778
	private const string s_jiraUrl = "https://jira.blizzard.com/";

	// Token: 0x040012AB RID: 4779
	private const string s_searchQuery = "summary ~ \"{0}\" and status != closed and issuetype=Bug";

	// Token: 0x040012AC RID: 4780
	private const string s_jiraAuth = "";

	// Token: 0x040012AD RID: 4781
	private static JiraBugDebugDisplay s_instance = null;

	// Token: 0x040012AE RID: 4782
	private static readonly AssetReference s_backgroundTexture = new AssetReference("tilable_background_grey_vertical.tif:2069edef921936f4db7eaeb542bcf5f1");

	// Token: 0x040012AF RID: 4783
	private ConcurrentDictionary<string, string> m_bugcache = new ConcurrentDictionary<string, string>();

	// Token: 0x040012B0 RID: 4784
	private int m_remoteRequestCount;

	// Token: 0x040012B1 RID: 4785
	private string m_currentCard = "";

	// Token: 0x040012B2 RID: 4786
	private float m_lastHotkeyInvokeTime;

	// Token: 0x040012B3 RID: 4787
	private bool m_isEnabled;

	// Token: 0x040012B4 RID: 4788
	private GUIStyle m_debugTextStyle;

	// Token: 0x040012B5 RID: 4789
	private AssetHandle<Texture> m_loadedBackgroundTexture;
}
