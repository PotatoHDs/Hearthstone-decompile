using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using Content.Delivery;
using Hearthstone.Core;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001153 RID: 4435
	public class InGameMessageScheduler : IService
	{
		// Token: 0x0600C258 RID: 49752 RVA: 0x003AE283 File Offset: 0x003AC483
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			this.CleanupOldMessages();
			this.CreateUpdateMessagesJob();
			HearthstoneApplication.Get().Resetting += this.CreateUpdateMessagesJob;
			yield break;
		}

		// Token: 0x0600C259 RID: 49753 RVA: 0x003AE292 File Offset: 0x003AC492
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(SceneMgr),
				typeof(NetCache)
			};
		}

		// Token: 0x0600C25A RID: 49754 RVA: 0x003AE2B4 File Offset: 0x003AC4B4
		public void Shutdown()
		{
			this.IsTerminated = true;
		}

		// Token: 0x0600C25B RID: 49755 RVA: 0x003AE2BD File Offset: 0x003AC4BD
		public static InGameMessageScheduler Get()
		{
			return HearthstoneServices.Get<InGameMessageScheduler>();
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600C25C RID: 49756 RVA: 0x003AE2C4 File Offset: 0x003AC4C4
		// (set) Token: 0x0600C25D RID: 49757 RVA: 0x003AE2CC File Offset: 0x003AC4CC
		public bool IsTerminated { get; set; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600C25E RID: 49758 RVA: 0x003AE2D5 File Offset: 0x003AC4D5
		// (set) Token: 0x0600C25F RID: 49759 RVA: 0x003AE2DD File Offset: 0x003AC4DD
		public bool HasNewRegisteredType { get; set; }

		// Token: 0x0600C260 RID: 49760 RVA: 0x003AE2E8 File Offset: 0x003AC4E8
		public void OnLoginCompleted()
		{
			foreach (KeyValuePair<string, MessageType> keyValuePair in this.m_messageTypes)
			{
				keyValuePair.Value.Init(true);
			}
		}

		// Token: 0x0600C261 RID: 49761 RVA: 0x003AE344 File Offset: 0x003AC544
		public void RegisterMessage(string contentType, int nextUpdateInterval, UpdateMessageHandler handler)
		{
			global::Log.InGameMessage.PrintInfo("Registered message: {0}, refresh time: {1}s", new object[]
			{
				contentType,
				nextUpdateInterval
			});
			MessageType messageType;
			if (!this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				messageType = new MessageType(contentType, nextUpdateInterval);
				this.m_messageTypes[contentType] = messageType;
			}
			else
			{
				messageType.ResetWithNewUpdateInterval(nextUpdateInterval);
				if (this.m_deletedMessageTypes.Contains(contentType))
				{
					this.m_deletedMessageTypes.Remove(contentType);
				}
			}
			if (handler != null)
			{
				messageType.AddHandler(handler);
			}
			this.HasNewRegisteredType = true;
		}

		// Token: 0x0600C262 RID: 49762 RVA: 0x003AE3D0 File Offset: 0x003AC5D0
		public void UnregisterMessage(string contentType)
		{
			global::Log.InGameMessage.PrintInfo("Unregistered message: {0}", new object[]
			{
				contentType
			});
			MessageType messageType;
			if (this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				messageType.SetDeleted();
				if (!this.m_deletedMessageTypes.Contains(contentType))
				{
					this.m_deletedMessageTypes.Add(contentType);
				}
			}
		}

		// Token: 0x0600C263 RID: 49763 RVA: 0x003AE428 File Offset: 0x003AC628
		public bool AddHandler(string contentType, UpdateMessageHandler handler)
		{
			MessageType messageType;
			if (!this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				global::Log.InGameMessage.PrintError("Failed to add a handler for '{0}'", new object[]
				{
					contentType
				});
				return false;
			}
			return messageType.AddHandler(handler);
		}

		// Token: 0x0600C264 RID: 49764 RVA: 0x003AE468 File Offset: 0x003AC668
		public bool RemoveHandler(string contentType, UpdateMessageHandler handler)
		{
			MessageType messageType;
			if (!this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				global::Log.InGameMessage.PrintError("Failed to remove a handler for '{0}'", new object[]
				{
					contentType
				});
				return false;
			}
			return messageType.RemoveHandler(handler);
		}

		// Token: 0x0600C265 RID: 49765 RVA: 0x003AE4A7 File Offset: 0x003AC6A7
		public void Refresh(string contentType)
		{
			Processor.RunCoroutine(this.TryToUpdate(contentType), null);
		}

		// Token: 0x0600C266 RID: 49766 RVA: 0x003AE4B7 File Offset: 0x003AC6B7
		private IEnumerator TryToUpdate(string contentType)
		{
			while (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
			{
				yield return null;
			}
			if (!ContentConnect.ContentstackEnabled)
			{
				global::Log.InGameMessage.PrintInfo("Refresh is ignored because system is turned off.", Array.Empty<object>());
				yield break;
			}
			MessageType messageType;
			if (!this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				global::Log.InGameMessage.PrintError("Failed to refresh: {0}", new object[]
				{
					contentType
				});
				yield break;
			}
			if (messageType.IsReadyToUpdate)
			{
				messageType.Update(this.QueryString, this.m_viewCountController);
			}
			else
			{
				messageType.RunUpdateMessageHandlers();
			}
			yield break;
		}

		// Token: 0x0600C267 RID: 49767 RVA: 0x003AE4D0 File Offset: 0x003AC6D0
		public GameMessage[] GetMessage(string contentType)
		{
			MessageType messageType;
			if (!this.m_messageTypes.TryGetValue(contentType, out messageType))
			{
				global::Log.InGameMessage.PrintError("Failed to get a message: {0}", new object[]
				{
					contentType
				});
				return null;
			}
			return messageType.Messages;
		}

		// Token: 0x0600C268 RID: 49768 RVA: 0x003AE50E File Offset: 0x003AC70E
		public void IncreaseViewCount(GameMessage message)
		{
			if (message == null)
			{
				return;
			}
			if (message.MaxViewCount > 0)
			{
				this.m_viewCountController.IncreaseViewCount(message.UID);
			}
		}

		// Token: 0x0600C269 RID: 49769 RVA: 0x003AE52E File Offset: 0x003AC72E
		public void SendTelemetryMessageAction(GameMessage message, InGameMessageAction.ActionType action)
		{
			TelemetryManager.Client().SendInGameMessageAction(message.ContentType, message.Title, action, this.m_viewCountController.GetViewCount(message.UID), message.UID);
		}

		// Token: 0x0600C26A RID: 49770 RVA: 0x003AE55E File Offset: 0x003AC75E
		public void ResetViewCount()
		{
			this.m_viewCountController.ClearViewCounts();
		}

		// Token: 0x0600C26B RID: 49771 RVA: 0x003AE56C File Offset: 0x003AC76C
		public void ForceRefreshAllContents()
		{
			foreach (KeyValuePair<string, MessageType> keyValuePair in this.m_messageTypes)
			{
				keyValuePair.Value.SetDeleted();
				keyValuePair.Value.Init(true);
				keyValuePair.Value.Update(this.QueryString, this.m_viewCountController);
			}
		}

		// Token: 0x0600C26C RID: 49772 RVA: 0x003AE5EC File Offset: 0x003AC7EC
		private void CreateUpdateMessagesJob()
		{
			Processor.QueueJobIfNotExist("InGameMessage.UpdateMessages", this.Job_UpdateMessages(), new IJobDependency[]
			{
				new InGameMessageScheduler.WaitForBnetRegionAndNetCacheFeatures()
			});
		}

		// Token: 0x0600C26D RID: 49773 RVA: 0x003AE610 File Offset: 0x003AC810
		private void CleanupOldMessages()
		{
			foreach (string text in Directory.GetFiles(FileUtils.PersistentDataPath, "*.igm.json"))
			{
				new FileInfo(text);
				if (DateTime.UtcNow.Subtract(new FileInfo(text).LastAccessTimeUtc).TotalHours > 672.0)
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600C26E RID: 49774 RVA: 0x003AE678 File Offset: 0x003AC878
		// (set) Token: 0x0600C26F RID: 49775 RVA: 0x003AE698 File Offset: 0x003AC898
		private string QueryString
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_query))
				{
					this.m_query = InGameMessageUtils.MakeQueryString();
				}
				return this.m_query;
			}
			set
			{
				this.m_query = value;
			}
		}

		// Token: 0x0600C270 RID: 49776 RVA: 0x003AE6A1 File Offset: 0x003AC8A1
		private IEnumerator<IAsyncJobResult> Job_UpdateMessages()
		{
			while (!this.IsTerminated)
			{
				this.QueryString = InGameMessageUtils.MakeQueryString();
				this.HasNewRegisteredType = false;
				int num = 60;
				if (ContentConnect.ContentstackEnabled)
				{
					foreach (KeyValuePair<string, MessageType> keyValuePair in this.m_messageTypes)
					{
						if (this.IsTerminated)
						{
							yield break;
						}
						if (keyValuePair.Value.IsReadyToUpdate && keyValuePair.Value.IsAutomaticUpdate)
						{
							keyValuePair.Value.Update(this.QueryString, this.m_viewCountController);
						}
						else
						{
							int validSeconds = keyValuePair.Value.ValidSeconds;
							if (validSeconds > 0 && validSeconds < num)
							{
								num = validSeconds;
							}
						}
					}
					if (this.m_deletedMessageTypes.Count > 0)
					{
						this.m_deletedMessageTypes.ForEach(delegate(string key)
						{
							this.m_messageTypes.Remove(key);
						});
						this.m_deletedMessageTypes.Clear();
					}
				}
				else
				{
					global::Log.InGameMessage.PrintDebug("The system is turned off by server configuration.", Array.Empty<object>());
				}
				if (this.IsTerminated)
				{
					yield break;
				}
				global::Log.InGameMessage.PrintDebug("Wait for {0}s before next update", new object[]
				{
					num
				});
				yield return new WaitForDurationForNextUpdate((float)num);
			}
			yield break;
		}

		// Token: 0x04009C97 RID: 40087
		public const string IGM_JSON_EXTENSION = ".igm.json";

		// Token: 0x04009C98 RID: 40088
		private const string IGM_UPDATE_JOB_NAME = "InGameMessage.UpdateMessages";

		// Token: 0x04009C99 RID: 40089
		private const double IGM_VALID_MESSAGE_HOURS = 672.0;

		// Token: 0x04009C9A RID: 40090
		private const int IGM_MAX_WAIT_TIME = 60;

		// Token: 0x04009C9D RID: 40093
		private Dictionary<string, MessageType> m_messageTypes = new Dictionary<string, MessageType>();

		// Token: 0x04009C9E RID: 40094
		private List<string> m_deletedMessageTypes = new List<string>();

		// Token: 0x04009C9F RID: 40095
		private ViewCountController m_viewCountController = new ViewCountController();

		// Token: 0x04009CA0 RID: 40096
		private string m_query;

		// Token: 0x0200291D RID: 10525
		private class WaitForBnetRegionAndNetCacheFeatures : IJobDependency, IAsyncJobResult
		{
			// Token: 0x06013E00 RID: 81408 RVA: 0x0053EB40 File Offset: 0x0053CD40
			public bool IsReady()
			{
				return NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() != null && BattleNet.GetCurrentRegion() != constants.BnetRegion.REGION_UNINITIALIZED;
			}
		}
	}
}
