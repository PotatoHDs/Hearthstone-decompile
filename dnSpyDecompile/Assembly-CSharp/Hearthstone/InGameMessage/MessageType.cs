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
	// Token: 0x02001155 RID: 4437
	public class MessageType
	{
		// Token: 0x0600C275 RID: 49781 RVA: 0x003AE744 File Offset: 0x003AC944
		public MessageType(string contentType, int nextUpdateInterval)
		{
			this.m_contentType = contentType;
			this.m_cachedJsonPath = Path.Combine(FileUtils.PersistentDataPath, contentType + ".igm.json");
			this.m_nextUpdateInterval = nextUpdateInterval;
			this.Init(false);
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600C276 RID: 49782 RVA: 0x003AE7A8 File Offset: 0x003AC9A8
		public bool IsReadyToUpdate
		{
			get
			{
				if (!this.m_initialized)
				{
					this.Init(false);
				}
				return this.m_initialized && !this.m_deleted && this.m_connect.Ready;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600C277 RID: 49783 RVA: 0x003AE7D6 File Offset: 0x003AC9D6
		public bool IsAutomaticUpdate
		{
			get
			{
				return this.m_nextUpdateInterval != -1;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600C278 RID: 49784 RVA: 0x003AE7E4 File Offset: 0x003AC9E4
		public int ValidSeconds
		{
			get
			{
				return this.m_connect.ValidSeconds;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600C279 RID: 49785 RVA: 0x003AE7F1 File Offset: 0x003AC9F1
		public GameMessage[] Messages
		{
			get
			{
				if (this.m_allMessages.Count <= 0)
				{
					return null;
				}
				return this.m_allMessages.ToArray();
			}
		}

		// Token: 0x0600C27A RID: 49786 RVA: 0x003AE80E File Offset: 0x003ACA0E
		public void Update(string query, object param)
		{
			Processor.RunCoroutine(this.m_connect.Query(new ResponseProcessHandler(this.UpdateAllMessagesFromJson), param, query, true), null);
		}

		// Token: 0x0600C27B RID: 49787 RVA: 0x003AE834 File Offset: 0x003ACA34
		public void Init(bool force)
		{
			if (force)
			{
				this.m_initialized = false;
				this.m_deleted = false;
			}
			if (BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_UNINITIALIZED)
			{
				global::Log.InGameMessage.PrintDebug("Failed to initialize '{0}': No region", new object[]
				{
					this.m_contentType
				});
				return;
			}
			if (this.m_initialized)
			{
				return;
			}
			this.m_connect.InitializeURL(this.m_contentType, Vars.Key("ContentStack.Env").GetStr("production"), Localization.GetBnetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, this.m_cachedJsonPath, (this.m_nextUpdateInterval == -1) ? 0 : this.m_nextUpdateInterval);
			this.m_initialized = true;
		}

		// Token: 0x0600C27C RID: 49788 RVA: 0x003AE8D3 File Offset: 0x003ACAD3
		public bool AddHandler(UpdateMessageHandler handler)
		{
			if (this.m_updateMessageHandlers.Contains(handler))
			{
				return false;
			}
			this.m_updateMessageHandlers.Add(handler);
			return true;
		}

		// Token: 0x0600C27D RID: 49789 RVA: 0x003AE8F2 File Offset: 0x003ACAF2
		public bool RemoveHandler(UpdateMessageHandler handler)
		{
			if (!this.m_updateMessageHandlers.Contains(handler))
			{
				return false;
			}
			this.m_updateMessageHandlers.Remove(handler);
			return true;
		}

		// Token: 0x0600C27E RID: 49790 RVA: 0x003AE914 File Offset: 0x003ACB14
		public void RunUpdateMessageHandlers()
		{
			if (this.m_allMessages.Count == 0 || this.m_updateMessageHandlers.Count == 0)
			{
				return;
			}
			if (!StructuralComparisons.StructuralEqualityComparer.Equals(this.m_latestHash, this.HashValues))
			{
				foreach (UpdateMessageHandler updateMessageHandler in this.m_updateMessageHandlers)
				{
					updateMessageHandler(this.Messages);
				}
				this.m_latestHash = this.HashValues;
				foreach (GameMessage gameMessage in this.m_allMessages)
				{
					TelemetryManager.Client().SendInGameMessageHandlerCalled(gameMessage.ContentType, gameMessage.Title, gameMessage.UID);
				}
			}
		}

		// Token: 0x0600C27F RID: 49791 RVA: 0x003AEA04 File Offset: 0x003ACC04
		public void ResetWithNewUpdateInterval(int interval)
		{
			if (this.m_nextUpdateInterval != interval || this.m_deleted)
			{
				this.m_nextUpdateInterval = interval;
				this.Init(true);
			}
		}

		// Token: 0x0600C280 RID: 49792 RVA: 0x003AEA25 File Offset: 0x003ACC25
		public void SetDeleted()
		{
			if (!this.m_deleted)
			{
				this.m_initialized = false;
				this.m_deleted = true;
				this.m_allMessages.Clear();
				this.m_latestHash = null;
				this.m_currentHash = null;
				this.m_entryTitles = string.Empty;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600C281 RID: 49793 RVA: 0x003AEA64 File Offset: 0x003ACC64
		private byte[] HashValues
		{
			get
			{
				if (this.m_currentHash == null && this.m_allMessages.Count > 0)
				{
					int num = this.m_allMessages[0].HashValue.Length;
					this.m_currentHash = new byte[num * this.m_allMessages.Count];
					int num2 = 0;
					foreach (GameMessage gameMessage in this.m_allMessages)
					{
						Buffer.BlockCopy(gameMessage.HashValue, 0, this.m_currentHash, num2, num);
						num2 += num;
					}
				}
				return this.m_currentHash;
			}
		}

		// Token: 0x0600C282 RID: 49794 RVA: 0x003AEB14 File Offset: 0x003ACD14
		private void UpdateAllMessagesFromJson(string jsonResponse, object param)
		{
			if (string.IsNullOrEmpty(jsonResponse))
			{
				return;
			}
			JsonNode response;
			try
			{
				response = (Json.Deserialize(jsonResponse) as JsonNode);
			}
			catch (Exception ex)
			{
				response = null;
				global::Log.InGameMessage.PrintWarning("Aborting because of an invalid json response:\n{0}", new object[]
				{
					jsonResponse
				});
				Debug.LogError(ex.StackTrace);
			}
			try
			{
				this.m_allMessages = InGameMessageUtils.GetAllMessagesFromJsonResponse(response, param as ViewCountController);
				this.m_currentHash = null;
				this.m_entryTitles = string.Empty;
				foreach (GameMessage gameMessage in this.m_allMessages)
				{
					gameMessage.ContentType = this.m_contentType;
				}
				this.RunUpdateMessageHandlers();
			}
			catch (Exception ex2)
			{
				Debug.LogErrorFormat("Failed to convert JSON response to message: {0}", new object[]
				{
					ex2.Message
				});
			}
		}

		// Token: 0x04009CA2 RID: 40098
		public const int UPDATE_INTERVAL_NO_AUTOMATIC_UPDATE = -1;

		// Token: 0x04009CA3 RID: 40099
		public const int UPDATE_INTERVAL_USE_HTTP_CACHE_AGE = 0;

		// Token: 0x04009CA4 RID: 40100
		private string m_contentType;

		// Token: 0x04009CA5 RID: 40101
		private string m_cachedJsonPath;

		// Token: 0x04009CA6 RID: 40102
		private int m_nextUpdateInterval;

		// Token: 0x04009CA7 RID: 40103
		private bool m_initialized;

		// Token: 0x04009CA8 RID: 40104
		private bool m_deleted;

		// Token: 0x04009CA9 RID: 40105
		private List<GameMessage> m_allMessages = new List<GameMessage>();

		// Token: 0x04009CAA RID: 40106
		private List<UpdateMessageHandler> m_updateMessageHandlers = new List<UpdateMessageHandler>();

		// Token: 0x04009CAB RID: 40107
		private ContentStackConnect m_connect = new ContentStackConnect();

		// Token: 0x04009CAC RID: 40108
		private byte[] m_latestHash;

		// Token: 0x04009CAD RID: 40109
		private byte[] m_currentHash;

		// Token: 0x04009CAE RID: 40110
		private string m_entryTitles;
	}
}
