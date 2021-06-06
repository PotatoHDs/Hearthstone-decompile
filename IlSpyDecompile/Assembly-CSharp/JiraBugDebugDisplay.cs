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

public class JiraBugDebugDisplay : MonoBehaviour
{
	private const string s_loadingText = "Loading...";

	private const string s_jiraUrl = "https://jira.blizzard.com/";

	private const string s_searchQuery = "summary ~ \"{0}\" and status != closed and issuetype=Bug";

	private const string s_jiraAuth = "";

	private static JiraBugDebugDisplay s_instance = null;

	private static readonly AssetReference s_backgroundTexture = new AssetReference("tilable_background_grey_vertical.tif:2069edef921936f4db7eaeb542bcf5f1");

	private ConcurrentDictionary<string, string> m_bugcache = new ConcurrentDictionary<string, string>();

	private int m_remoteRequestCount;

	private string m_currentCard = "";

	private float m_lastHotkeyInvokeTime;

	private bool m_isEnabled;

	private GUIStyle m_debugTextStyle;

	private AssetHandle<Texture> m_loadedBackgroundTexture;

	public static JiraBugDebugDisplay Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<JiraBugDebugDisplay>();
			obj.name = "JIRABugDebugDisplay (Dynamically created)";
			AssetLoader.Get().LoadAsset<Texture>(s_backgroundTexture, s_instance.OnTextureLoad);
			s_instance.m_debugTextStyle = new GUIStyle("box");
			s_instance.m_debugTextStyle.fontSize = 16;
			s_instance.m_debugTextStyle.normal.textColor = Color.white;
			s_instance.m_debugTextStyle.alignment = TextAnchor.MiddleLeft;
		}
		return s_instance;
	}

	private void OnTextureLoad(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
	{
		AssetHandle.Take(ref m_loadedBackgroundTexture, loadedTexture);
		s_instance.m_debugTextStyle.normal.background = (Texture2D)m_loadedBackgroundTexture.Asset;
	}

	private void OnGUI()
	{
		if (!(s_instance == null) && Event.current.type == EventType.KeyUp)
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
			if (Event.current.keyCode == KeyCode.F2 && flag && Time.fixedUnscaledTime > m_lastHotkeyInvokeTime + 1f)
			{
				m_lastHotkeyInvokeTime = Time.fixedUnscaledTime;
				LoadBugsInBrowser();
			}
		}
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_loadedBackgroundTexture);
	}

	private void LoadBugsInBrowser()
	{
		Application.OpenURL(GetSearchURL(m_currentCard, useApiEndpoint: false));
	}

	private bool GetBugsForCard(string cardid, out string bugs)
	{
		m_bugcache.TryGetValue(cardid, out bugs);
		if (string.IsNullOrWhiteSpace(bugs))
		{
			bugs = "No Issues Found";
			return false;
		}
		if (bugs == "Loading...")
		{
			return false;
		}
		return true;
	}

	private string GetSearchURL(string search, bool useApiEndpoint = true)
	{
		string text = ((!useApiEndpoint) ? "https://jira.blizzard.com/issues/?jql=" : "https://jira.blizzard.com/rest/api/2/search/?jql=");
		string s = $"summary ~ \"{search}\" and status != closed and issuetype=Bug";
		return text + UnityWebRequest.EscapeURL(s);
	}

	private IEnumerator SearchJira(string search)
	{
		if (!m_bugcache.ContainsKey(search))
		{
			m_remoteRequestCount++;
			m_bugcache.TryAdd(search, "Loading...");
			string searchURL = GetSearchURL(search);
			UnityWebRequest request = new UnityWebRequest(searchURL, "GET");
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Authorization", "");
			request.useHttpContinue = false;
			yield return request.SendWebRequest();
			m_bugcache.TryUpdate(search, ParseJiraSearchResults(request), "Loading...");
		}
		yield return null;
	}

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
			for (int i = 0; i < num; i++)
			{
				JsonNode obj = jsonList[i] as JsonNode;
				string text = obj["key"] as string;
				JsonNode jsonNode2 = obj["fields"] as JsonNode;
				stringBuilder.Append(text.PadRight(11));
				stringBuilder.Append(" - ");
				stringBuilder.AppendLine(jsonNode2["summary"] as string);
			}
			stringBuilder.Length--;
		}
		return stringBuilder.ToString();
	}

	public bool EnableDebugDisplay(string func, string[] args, string rawArgs)
	{
		m_isEnabled = true;
		return true;
	}

	public bool DisableDebugDisplay(string func, string[] args, string rawArgs)
	{
		m_isEnabled = false;
		m_bugcache.Clear();
		return true;
	}

	private void Update()
	{
		if (HearthstoneApplication.IsPublic() || !m_isEnabled)
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
			if (zone.m_ServerTag != TAG_ZONE.HAND && zone.m_ServerTag != TAG_ZONE.PLAY && zone.m_ServerTag != TAG_ZONE.SECRET)
			{
				continue;
			}
			foreach (Card card in zone.GetCards())
			{
				Entity entity2 = card.GetEntity();
				if (entity != null && entity != entity2)
				{
					continue;
				}
				Vector3 position = card.transform.position;
				if (zone.m_ServerTag == TAG_ZONE.HAND)
				{
					Vector3 vector = card.transform.forward;
					if (card.GetControllerSide() == Player.Side.OPPOSING)
					{
						vector *= -1.5f;
						if (card.GetController().IsRevealed())
						{
							vector = -vector;
						}
					}
					position += vector;
				}
				if (entity != null)
				{
					string cardId = card.GetEntity().GetCardId();
					if (!string.IsNullOrEmpty(cardId))
					{
						Processor.RunCoroutine(SearchJira(cardId));
						SetCurrentCard(cardId);
						DrawDebugTextForHighlightedCard(entity2, DebugTextManager.WorldPosToScreenPos(position));
					}
					return;
				}
			}
		}
	}

	private void SetCurrentCard(string cardid)
	{
		s_instance.m_currentCard = cardid;
	}

	private void DrawDebugTextForHighlightedCard(Entity ent, Vector3 pos, bool screenSpace = false, bool forceShowZeroTags = false)
	{
		if (GetBugsForCard(ent.GetCardId(), out var bugs))
		{
			bugs = "Press ALT+F2 to view in JIRA\n" + bugs;
		}
		DebugTextManager.Get().DrawDebugText(bugs, pos, 0f, screenSpace, "", m_debugTextStyle);
	}
}
