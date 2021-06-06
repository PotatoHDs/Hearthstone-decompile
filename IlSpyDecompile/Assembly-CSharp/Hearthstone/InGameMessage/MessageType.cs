using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using bgs;
using Content.Delivery;
using Hearthstone.Core;
using MiniJSON;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	public class MessageType
	{
		public const int UPDATE_INTERVAL_NO_AUTOMATIC_UPDATE = -1;

		public const int UPDATE_INTERVAL_USE_HTTP_CACHE_AGE = 0;

		private string m_contentType;

		private string m_cachedJsonPath;

		private int m_nextUpdateInterval;

		private bool m_initialized;

		private bool m_deleted;

		private List<GameMessage> m_allMessages = new List<GameMessage>();

		private List<UpdateMessageHandler> m_updateMessageHandlers = new List<UpdateMessageHandler>();

		private ContentStackConnect m_connect = new ContentStackConnect();

		private byte[] m_latestHash;

		private byte[] m_currentHash;

		private string m_entryTitles;

		public bool IsReadyToUpdate
		{
			get
			{
				if (!m_initialized)
				{
					Init(force: false);
				}
				if (m_initialized && !m_deleted)
				{
					return m_connect.Ready;
				}
				return false;
			}
		}

		public bool IsAutomaticUpdate => m_nextUpdateInterval != -1;

		public int ValidSeconds => m_connect.ValidSeconds;

		public GameMessage[] Messages
		{
			get
			{
				if (m_allMessages.Count <= 0)
				{
					return null;
				}
				return m_allMessages.ToArray();
			}
		}

		private byte[] HashValues
		{
			get
			{
				if (m_currentHash == null && m_allMessages.Count > 0)
				{
					int num = m_allMessages[0].HashValue.Length;
					m_currentHash = new byte[num * m_allMessages.Count];
					int num2 = 0;
					foreach (GameMessage allMessage in m_allMessages)
					{
						Buffer.BlockCopy(allMessage.HashValue, 0, m_currentHash, num2, num);
						num2 += num;
					}
				}
				return m_currentHash;
			}
		}

		public MessageType(string contentType, int nextUpdateInterval)
		{
			m_contentType = contentType;
			m_cachedJsonPath = Path.Combine(FileUtils.PersistentDataPath, contentType + ".igm.json");
			m_nextUpdateInterval = nextUpdateInterval;
			Init(force: false);
		}

		public void Update(string query, object param)
		{
			Processor.RunCoroutine(m_connect.Query(UpdateAllMessagesFromJson, param, query, force: true));
		}

		public void Init(bool force)
		{
			if (force)
			{
				m_initialized = false;
				m_deleted = false;
			}
			if (BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_UNINITIALIZED)
			{
				Log.InGameMessage.PrintDebug("Failed to initialize '{0}': No region", m_contentType);
			}
			else if (!m_initialized)
			{
				m_connect.InitializeURL(m_contentType, Vars.Key("ContentStack.Env").GetStr("production"), Localization.GetBnetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, m_cachedJsonPath, (m_nextUpdateInterval != -1) ? m_nextUpdateInterval : 0);
				m_initialized = true;
			}
		}

		public bool AddHandler(UpdateMessageHandler handler)
		{
			if (m_updateMessageHandlers.Contains(handler))
			{
				return false;
			}
			m_updateMessageHandlers.Add(handler);
			return true;
		}

		public bool RemoveHandler(UpdateMessageHandler handler)
		{
			if (!m_updateMessageHandlers.Contains(handler))
			{
				return false;
			}
			m_updateMessageHandlers.Remove(handler);
			return true;
		}

		public void RunUpdateMessageHandlers()
		{
			if (m_allMessages.Count == 0 || m_updateMessageHandlers.Count == 0 || StructuralComparisons.StructuralEqualityComparer.Equals(m_latestHash, HashValues))
			{
				return;
			}
			foreach (UpdateMessageHandler updateMessageHandler in m_updateMessageHandlers)
			{
				updateMessageHandler(Messages);
			}
			m_latestHash = HashValues;
			foreach (GameMessage allMessage in m_allMessages)
			{
				TelemetryManager.Client().SendInGameMessageHandlerCalled(allMessage.ContentType, allMessage.Title, allMessage.UID);
			}
		}

		public void ResetWithNewUpdateInterval(int interval)
		{
			if (m_nextUpdateInterval != interval || m_deleted)
			{
				m_nextUpdateInterval = interval;
				Init(force: true);
			}
		}

		public void SetDeleted()
		{
			if (!m_deleted)
			{
				m_initialized = false;
				m_deleted = true;
				m_allMessages.Clear();
				m_latestHash = null;
				m_currentHash = null;
				m_entryTitles = string.Empty;
			}
		}

		private void UpdateAllMessagesFromJson(string jsonResponse, object param)
		{
			if (string.IsNullOrEmpty(jsonResponse))
			{
				return;
			}
			JsonNode response;
			try
			{
				response = Json.Deserialize(jsonResponse) as JsonNode;
			}
			catch (Exception ex)
			{
				response = null;
				Log.InGameMessage.PrintWarning("Aborting because of an invalid json response:\n{0}", jsonResponse);
				Debug.LogError(ex.StackTrace);
			}
			try
			{
				m_allMessages = InGameMessageUtils.GetAllMessagesFromJsonResponse(response, param as ViewCountController);
				m_currentHash = null;
				m_entryTitles = string.Empty;
				foreach (GameMessage allMessage in m_allMessages)
				{
					allMessage.ContentType = m_contentType;
				}
				RunUpdateMessageHandlers();
			}
			catch (Exception ex2)
			{
				Debug.LogErrorFormat("Failed to convert JSON response to message: {0}", ex2.Message);
			}
		}
	}
}
