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
	public class InGameMessageScheduler : IService
	{
		private class WaitForBnetRegionAndNetCacheFeatures : IJobDependency, IAsyncJobResult
		{
			public bool IsReady()
			{
				if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() != null)
				{
					return BattleNet.GetCurrentRegion() != constants.BnetRegion.REGION_UNINITIALIZED;
				}
				return false;
			}
		}

		public const string IGM_JSON_EXTENSION = ".igm.json";

		private const string IGM_UPDATE_JOB_NAME = "InGameMessage.UpdateMessages";

		private const double IGM_VALID_MESSAGE_HOURS = 672.0;

		private const int IGM_MAX_WAIT_TIME = 60;

		private Dictionary<string, MessageType> m_messageTypes = new Dictionary<string, MessageType>();

		private List<string> m_deletedMessageTypes = new List<string>();

		private ViewCountController m_viewCountController = new ViewCountController();

		private string m_query;

		public bool IsTerminated { get; set; }

		public bool HasNewRegisteredType { get; set; }

		private string QueryString
		{
			get
			{
				if (string.IsNullOrEmpty(m_query))
				{
					m_query = InGameMessageUtils.MakeQueryString();
				}
				return m_query;
			}
			set
			{
				m_query = value;
			}
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			CleanupOldMessages();
			CreateUpdateMessagesJob();
			HearthstoneApplication.Get().Resetting += CreateUpdateMessagesJob;
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[2]
			{
				typeof(SceneMgr),
				typeof(NetCache)
			};
		}

		public void Shutdown()
		{
			IsTerminated = true;
		}

		public static InGameMessageScheduler Get()
		{
			return HearthstoneServices.Get<InGameMessageScheduler>();
		}

		public void OnLoginCompleted()
		{
			foreach (KeyValuePair<string, MessageType> messageType in m_messageTypes)
			{
				messageType.Value.Init(force: true);
			}
		}

		public void RegisterMessage(string contentType, int nextUpdateInterval, UpdateMessageHandler handler)
		{
			Log.InGameMessage.PrintInfo("Registered message: {0}, refresh time: {1}s", contentType, nextUpdateInterval);
			if (!m_messageTypes.TryGetValue(contentType, out var value))
			{
				value = new MessageType(contentType, nextUpdateInterval);
				m_messageTypes[contentType] = value;
			}
			else
			{
				value.ResetWithNewUpdateInterval(nextUpdateInterval);
				if (m_deletedMessageTypes.Contains(contentType))
				{
					m_deletedMessageTypes.Remove(contentType);
				}
			}
			if (handler != null)
			{
				value.AddHandler(handler);
			}
			HasNewRegisteredType = true;
		}

		public void UnregisterMessage(string contentType)
		{
			Log.InGameMessage.PrintInfo("Unregistered message: {0}", contentType);
			if (m_messageTypes.TryGetValue(contentType, out var value))
			{
				value.SetDeleted();
				if (!m_deletedMessageTypes.Contains(contentType))
				{
					m_deletedMessageTypes.Add(contentType);
				}
			}
		}

		public bool AddHandler(string contentType, UpdateMessageHandler handler)
		{
			if (!m_messageTypes.TryGetValue(contentType, out var value))
			{
				Log.InGameMessage.PrintError("Failed to add a handler for '{0}'", contentType);
				return false;
			}
			return value.AddHandler(handler);
		}

		public bool RemoveHandler(string contentType, UpdateMessageHandler handler)
		{
			if (!m_messageTypes.TryGetValue(contentType, out var value))
			{
				Log.InGameMessage.PrintError("Failed to remove a handler for '{0}'", contentType);
				return false;
			}
			return value.RemoveHandler(handler);
		}

		public void Refresh(string contentType)
		{
			Processor.RunCoroutine(TryToUpdate(contentType));
		}

		private IEnumerator TryToUpdate(string contentType)
		{
			while (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
			{
				yield return null;
			}
			MessageType value;
			if (!ContentConnect.ContentstackEnabled)
			{
				Log.InGameMessage.PrintInfo("Refresh is ignored because system is turned off.");
			}
			else if (!m_messageTypes.TryGetValue(contentType, out value))
			{
				Log.InGameMessage.PrintError("Failed to refresh: {0}", contentType);
			}
			else if (value.IsReadyToUpdate)
			{
				value.Update(QueryString, m_viewCountController);
			}
			else
			{
				value.RunUpdateMessageHandlers();
			}
		}

		public GameMessage[] GetMessage(string contentType)
		{
			if (!m_messageTypes.TryGetValue(contentType, out var value))
			{
				Log.InGameMessage.PrintError("Failed to get a message: {0}", contentType);
				return null;
			}
			return value.Messages;
		}

		public void IncreaseViewCount(GameMessage message)
		{
			if (message != null && message.MaxViewCount > 0)
			{
				m_viewCountController.IncreaseViewCount(message.UID);
			}
		}

		public void SendTelemetryMessageAction(GameMessage message, InGameMessageAction.ActionType action)
		{
			TelemetryManager.Client().SendInGameMessageAction(message.ContentType, message.Title, action, m_viewCountController.GetViewCount(message.UID), message.UID);
		}

		public void ResetViewCount()
		{
			m_viewCountController.ClearViewCounts();
		}

		public void ForceRefreshAllContents()
		{
			foreach (KeyValuePair<string, MessageType> messageType in m_messageTypes)
			{
				messageType.Value.SetDeleted();
				messageType.Value.Init(force: true);
				messageType.Value.Update(QueryString, m_viewCountController);
			}
		}

		private void CreateUpdateMessagesJob()
		{
			Processor.QueueJobIfNotExist("InGameMessage.UpdateMessages", Job_UpdateMessages(), new WaitForBnetRegionAndNetCacheFeatures());
		}

		private void CleanupOldMessages()
		{
			string[] files = Directory.GetFiles(FileUtils.PersistentDataPath, "*.igm.json");
			foreach (string text in files)
			{
				new FileInfo(text);
				if (DateTime.UtcNow.Subtract(new FileInfo(text).LastAccessTimeUtc).TotalHours > 672.0)
				{
					File.Delete(text);
				}
			}
		}

		private IEnumerator<IAsyncJobResult> Job_UpdateMessages()
		{
			while (!IsTerminated)
			{
				QueryString = InGameMessageUtils.MakeQueryString();
				HasNewRegisteredType = false;
				int num = 60;
				if (ContentConnect.ContentstackEnabled)
				{
					foreach (KeyValuePair<string, MessageType> messageType in m_messageTypes)
					{
						if (IsTerminated)
						{
							yield break;
						}
						if (messageType.Value.IsReadyToUpdate && messageType.Value.IsAutomaticUpdate)
						{
							messageType.Value.Update(QueryString, m_viewCountController);
							continue;
						}
						int validSeconds = messageType.Value.ValidSeconds;
						if (validSeconds > 0 && validSeconds < num)
						{
							num = validSeconds;
						}
					}
					if (m_deletedMessageTypes.Count > 0)
					{
						m_deletedMessageTypes.ForEach(delegate(string key)
						{
							m_messageTypes.Remove(key);
						});
						m_deletedMessageTypes.Clear();
					}
				}
				else
				{
					Log.InGameMessage.PrintDebug("The system is turned off by server configuration.");
				}
				if (IsTerminated)
				{
					break;
				}
				Log.InGameMessage.PrintDebug("Wait for {0}s before next update", num);
				yield return new WaitForDurationForNextUpdate(num);
			}
		}
	}
}
