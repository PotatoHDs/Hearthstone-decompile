using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.channel.v1;
using bnet.protocol.presence.v1;

namespace bgs
{
	public class PresenceAPI : BattleNetAPI
	{
		public class PresenceRefCountObject
		{
			public ulong objectId;

			public int refCount;
		}

		private class FieldKeyToPresenceMap : Map<FieldKey, Variant>
		{
		}

		private class EntityIdToFieldsMap : Map<bgs.types.EntityId, FieldKeyToPresenceMap>
		{
			public void SetCache(bgs.types.EntityId entity, FieldKey key, Variant value)
			{
				if (key != null)
				{
					if (!ContainsKey(entity))
					{
						base[entity] = new FieldKeyToPresenceMap();
					}
					base[entity][key] = value;
				}
			}

			public Variant GetCache(bgs.types.EntityId entity, FieldKey key)
			{
				if (key == null)
				{
					return null;
				}
				if (!ContainsKey(entity))
				{
					return null;
				}
				FieldKeyToPresenceMap fieldKeyToPresenceMap = base[entity];
				if (!fieldKeyToPresenceMap.ContainsKey(key))
				{
					return null;
				}
				return fieldKeyToPresenceMap[key];
			}
		}

		private class IndexToStringMap : Map<ulong, string>
		{
		}

		private class RichPresenceToStringsMap : Map<RichPresenceLocalizationKey, IndexToStringMap>
		{
		}

		public class PresenceChannelReferenceObject
		{
			public BasePresenceChannelData m_channelData;

			public PresenceChannelReferenceObject(bnet.protocol.EntityId entityId)
			{
				m_channelData = new BasePresenceChannelData(entityId, 0uL);
			}

			public PresenceChannelReferenceObject(BasePresenceChannelData channelData)
			{
				m_channelData = channelData;
			}
		}

		public class BasePresenceChannelData
		{
			public bnet.protocol.EntityId m_channelId;

			public ulong m_objectId;

			public ulong m_subscriberObjectId;

			public BasePresenceChannelData(bnet.protocol.EntityId entityId, ulong objectId)
			{
				m_channelId = entityId;
				m_objectId = objectId;
			}

			public void SetChannelId(bnet.protocol.EntityId channelId)
			{
				m_channelId = channelId;
			}

			public void SetObjectId(ulong objectId)
			{
				m_objectId = objectId;
			}

			public void SetSubscriberObjectId(ulong objectId)
			{
				m_subscriberObjectId = objectId;
			}
		}

		public class PresenceUnsubscribeContext
		{
			private ulong m_objectId;

			private BattleNetCSharp m_battleNet;

			public PresenceUnsubscribeContext(BattleNetCSharp battleNet, ulong objectId)
			{
				m_battleNet = battleNet;
				m_objectId = objectId;
			}

			public void PresenceUnsubscribeCallback(RPCContext context)
			{
				if (m_battleNet.Presence.CheckRPCCallback("PresenceUnsubscribeCallback", context))
				{
					m_battleNet.Presence.RemoveActiveChannel(m_objectId);
				}
			}
		}

		private const float CREDIT_LIMIT = -100f;

		private const float COST_PER_REQUEST = 1f;

		private const float PAYDOWN_RATE_PER_MS = 0.00333333341f;

		private float m_presenceSubscriptionBalance;

		private long m_lastPresenceSubscriptionSent;

		private Stopwatch m_stopWatch;

		private HashSet<bnet.protocol.EntityId> m_queuedSubscriptions = new HashSet<bnet.protocol.EntityId>();

		private Map<ulong, PresenceChannelReferenceObject> m_activeChannels = new Map<ulong, PresenceChannelReferenceObject>();

		private Map<bnet.protocol.EntityId, ulong> m_channelEntityObjectMap = new Map<bnet.protocol.EntityId, ulong>();

		private Map<bnet.protocol.EntityId, PresenceRefCountObject> m_presenceSubscriptions = new Map<bnet.protocol.EntityId, PresenceRefCountObject>();

		private List<PresenceUpdate> m_presenceUpdates = new List<PresenceUpdate>();

		private EntityIdToFieldsMap m_presenceCache = new EntityIdToFieldsMap();

		private RichPresenceToStringsMap m_richPresenceStringTables = new RichPresenceToStringsMap();

		private HashSet<PresenceUpdate> m_pendingRichPresenceUpdates = new HashSet<PresenceUpdate>();

		private int m_numOutstandingRichPresenceStringFetches;

		private static ulong s_nextObjectId = 0uL;

		private ServiceDescriptor m_presenceService = new PresenceService();

		private ServiceDescriptor m_channelSubscriberService = new ChannelSubscriberService();

		private const string variablePrefix = "$0x";

		private static char[] hexChars = new char[22]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D',
			'E', 'F'
		};

		public ServiceDescriptor PresenceService => m_presenceService;

		public ServiceDescriptor ChannelSubscriberService => m_channelSubscriberService;

		public static ulong GetNextObjectId()
		{
			return ++s_nextObjectId;
		}

		public void AddActiveChannel(ulong objectId, PresenceChannelReferenceObject channelRefObject)
		{
			m_activeChannels.Add(objectId, channelRefObject);
			m_channelEntityObjectMap[channelRefObject.m_channelData.m_channelId] = objectId;
		}

		public void RemoveActiveChannel(ulong objectId)
		{
			PresenceChannelReferenceObject presenceChannelReferenceObject = GetPresenceChannelReferenceObject(objectId);
			if (presenceChannelReferenceObject != null)
			{
				m_channelEntityObjectMap.Remove(presenceChannelReferenceObject.m_channelData.m_channelId);
				m_activeChannels.Remove(objectId);
			}
		}

		public PresenceChannelReferenceObject GetPresenceChannelReferenceObject(ulong objectId)
		{
			if (m_activeChannels.TryGetValue(objectId, out var value))
			{
				return value;
			}
			return null;
		}

		public PresenceAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Presence")
		{
			m_stopWatch = new Stopwatch();
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 1u, HandleChannelSubscriber_NotifyAdd);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 2u, HandleChannelSubscriber_NotifyJoin);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 3u, HandleChannelSubscriber_NotifyRemove);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 4u, HandleChannelSubscriber_NotifyLeave);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 5u, HandleChannelSubscriber_NotifySendMessage);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 6u, HandleChannelSubscriber_NotifyUpdateChannelState);
			m_rpcConnection.RegisterServiceMethodListener(m_channelSubscriberService.Id, 7u, HandleChannelSubscriber_NotifyUpdateMemberState);
		}

		public override void Initialize()
		{
			base.Initialize();
			m_stopWatch.Start();
			m_lastPresenceSubscriptionSent = 0L;
			m_presenceSubscriptionBalance = 0f;
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			m_activeChannels.Clear();
			m_channelEntityObjectMap.Clear();
			m_presenceSubscriptions.Clear();
			m_presenceUpdates.Clear();
			m_queuedSubscriptions.Clear();
			m_stopWatch.Stop();
			m_lastPresenceSubscriptionSent = 0L;
			m_presenceSubscriptionBalance = 0f;
		}

		public override void Process()
		{
			base.Process();
			HandleSubscriptionRequests();
		}

		public int PresenceSize()
		{
			return m_presenceUpdates.Count;
		}

		public void ClearPresence()
		{
			m_presenceUpdates.Clear();
		}

		public void GetPresence([Out] PresenceUpdate[] updates)
		{
			m_presenceUpdates.CopyTo(updates);
		}

		public void SetPresenceBool(uint field, bool val)
		{
			SetPresenceBool(field, val, BnetProgramId.HEARTHSTONE, 2u, m_battleNet.GameAccountId);
		}

		public void SetAccountLevelPresenceBool(uint field, bool val)
		{
			SetPresenceBool(field, val, BnetProgramId.BNET, 1u, m_battleNet.AccountId);
		}

		private void SetPresenceBool(uint field, bool val, BnetProgramId programId, uint groupId, bnet.protocol.EntityId entityId)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(programId.GetValue());
			fieldKey.SetGroup(groupId);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetBoolValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(entityId);
			updateRequest.AddFieldOperation(fieldOperation);
			PublishField(updateRequest);
		}

		public void SetPresenceInt(uint field, long val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetIntValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			PublishField(updateRequest);
		}

		public void SetPresenceString(uint field, string val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetStringValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			PublishField(updateRequest);
		}

		public void SetPresenceBlob(uint field, byte[] val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			if (val == null)
			{
				val = new byte[0];
			}
			variant.SetBlobValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			PublishField(updateRequest);
		}

		public void SetPresenceEntityId(uint field, bgs.types.EntityId val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetHigh(val.hi);
			entityId.SetLow(val.lo);
			Variant variant = new Variant();
			variant.SetEntityIdValue(entityId);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			PublishField(updateRequest);
		}

		public void PublishRichPresence([In] RichPresenceUpdate[] updates)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = m_battleNet.GameAccountId;
			for (int i = 0; i < updates.Length; i++)
			{
				RichPresenceUpdate richPresenceUpdate = updates[i];
				FieldOperation fieldOperation = new FieldOperation();
				Field field = new Field();
				FieldKey fieldKey = new FieldKey();
				fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
				fieldKey.SetGroup(2u);
				fieldKey.SetField(8u);
				fieldKey.SetUniqueId(richPresenceUpdate.presenceFieldIndex);
				RichPresenceLocalizationKey richPresenceLocalizationKey = new RichPresenceLocalizationKey();
				richPresenceLocalizationKey.SetLocalizationId(richPresenceUpdate.index);
				richPresenceLocalizationKey.SetProgram(richPresenceUpdate.programId);
				richPresenceLocalizationKey.SetStream(richPresenceUpdate.streamId);
				Variant variant = new Variant();
				variant.SetMessageValue(ProtobufUtil.ToByteArray(richPresenceLocalizationKey));
				field.SetKey(fieldKey);
				field.SetValue(variant);
				fieldOperation.SetField(field);
				updateRequest.SetEntityId(m_battleNet.GameAccountId);
				updateRequest.AddFieldOperation(fieldOperation);
			}
			PublishField(updateRequest);
		}

		private void HandleSubscriptionRequests()
		{
			if (m_queuedSubscriptions.Count <= 0)
			{
				return;
			}
			long elapsedMilliseconds = m_stopWatch.ElapsedMilliseconds;
			m_presenceSubscriptionBalance = Math.Min(0f, m_presenceSubscriptionBalance + (float)(elapsedMilliseconds - m_lastPresenceSubscriptionSent) * 0.00333333341f);
			m_lastPresenceSubscriptionSent = elapsedMilliseconds;
			bool inStartupPeriod = m_rpcConnection.GetInStartupPeriod();
			List<bnet.protocol.EntityId> list = new List<bnet.protocol.EntityId>();
			foreach (bnet.protocol.EntityId item in m_queuedSubscriptions.ToList())
			{
				if (m_presenceSubscriptionBalance - 1f < -100f)
				{
					break;
				}
				PresenceRefCountObject presenceRefCountObject = m_presenceSubscriptions[item];
				bnet.protocol.presence.v1.SubscribeRequest subscribeRequest = new bnet.protocol.presence.v1.SubscribeRequest();
				subscribeRequest.SetObjectId(GetNextObjectId());
				subscribeRequest.SetEntityId(item);
				presenceRefCountObject.objectId = subscribeRequest.ObjectId;
				AddActiveChannel(subscribeRequest.ObjectId, new PresenceChannelReferenceObject(item));
				m_rpcConnection.QueueRequest(m_presenceService, 1u, subscribeRequest, PresenceSubscribeCallback);
				if (!inStartupPeriod)
				{
					m_presenceSubscriptionBalance -= 1f;
				}
				list.Add(item);
			}
			foreach (bnet.protocol.EntityId item2 in list)
			{
				m_queuedSubscriptions.Remove(item2);
			}
		}

		public void PresenceSubscribe(bnet.protocol.EntityId entityId)
		{
			if (m_presenceSubscriptions.ContainsKey(entityId))
			{
				m_presenceSubscriptions[entityId].refCount++;
				return;
			}
			PresenceRefCountObject presenceRefCountObject = new PresenceRefCountObject();
			presenceRefCountObject.objectId = 0uL;
			presenceRefCountObject.refCount = 1;
			m_presenceSubscriptions.Add(entityId, presenceRefCountObject);
			m_queuedSubscriptions.Add(entityId);
			HandleSubscriptionRequests();
		}

		public void PresenceUnsubscribe(bnet.protocol.EntityId entityId)
		{
			if (!m_presenceSubscriptions.ContainsKey(entityId))
			{
				return;
			}
			m_presenceSubscriptions[entityId].refCount--;
			if (m_presenceSubscriptions[entityId].refCount <= 0)
			{
				if (m_queuedSubscriptions.Contains(entityId))
				{
					m_queuedSubscriptions.Remove(entityId);
					return;
				}
				PresenceUnsubscribeContext @object = new PresenceUnsubscribeContext(m_battleNet, m_presenceSubscriptions[entityId].objectId);
				UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
				unsubscribeRequest.SetEntityId(entityId);
				m_rpcConnection.QueueRequest(m_presenceService, 2u, unsubscribeRequest, @object.PresenceUnsubscribeCallback);
				m_presenceSubscriptions.Remove(entityId);
			}
		}

		public bool IsSubscribedToEntity(bnet.protocol.EntityId entityId)
		{
			if (m_presenceSubscriptions.ContainsKey(entityId))
			{
				return m_presenceSubscriptions[entityId].refCount != 0;
			}
			return false;
		}

		public void PublishField(UpdateRequest updateRequest)
		{
			if (m_rpcConnection != null)
			{
				m_rpcConnection.QueueRequest(m_presenceService, 3u, updateRequest, PresenceUpdateCallback);
			}
		}

		private void PresenceSubscribeCallback(RPCContext context)
		{
			CheckRPCCallback("PresenceSubscribeCallback", context);
		}

		private void PresenceUpdateCallback(RPCContext context)
		{
			CheckRPCCallback("PresenceUpdateCallback", context);
		}

		public void HandlePresenceUpdates(bnet.protocol.presence.v1.ChannelState channelState, PresenceChannelReferenceObject channelRef)
		{
			bgs.types.EntityId entityId = default(bgs.types.EntityId);
			entityId.hi = channelRef.m_channelData.m_channelId.High;
			entityId.lo = channelRef.m_channelData.m_channelId.Low;
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(1u);
			fieldKey.SetField(3u);
			FieldKey fieldKey2 = fieldKey;
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (FieldOperation fieldOperation in channelState.FieldOperationList)
			{
				if (fieldOperation.Operation == FieldOperation.Types.OperationType.CLEAR)
				{
					m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, null);
				}
				else
				{
					m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, fieldOperation.Field.Value);
				}
				PresenceUpdate presenceUpdate = default(PresenceUpdate);
				presenceUpdate.entityId = entityId;
				presenceUpdate.programId = fieldOperation.Field.Key.Program;
				presenceUpdate.groupId = fieldOperation.Field.Key.Group;
				presenceUpdate.fieldId = fieldOperation.Field.Key.Field;
				presenceUpdate.index = fieldOperation.Field.Key.UniqueId;
				presenceUpdate.boolVal = false;
				presenceUpdate.intVal = 0L;
				presenceUpdate.stringVal = "";
				presenceUpdate.valCleared = false;
				presenceUpdate.blobVal = new byte[0];
				if (fieldOperation.Operation == FieldOperation.Types.OperationType.CLEAR)
				{
					presenceUpdate.valCleared = true;
					bool flag = fieldKey2.Program == fieldOperation.Field.Key.Program;
					bool flag2 = fieldKey2.Group == fieldOperation.Field.Key.Group;
					bool flag3 = fieldKey2.Field == fieldOperation.Field.Key.Field;
					if (flag && flag2 && flag3)
					{
						BnetEntityId entityId2 = BnetEntityId.CreateFromEntityId(presenceUpdate.entityId);
						m_battleNet.Friends.RemoveFriendsActiveGameAccount(entityId2, fieldOperation.Field.Key.UniqueId);
					}
				}
				else if (fieldOperation.Field.Value.HasBoolValue)
				{
					presenceUpdate.boolVal = fieldOperation.Field.Value.BoolValue;
				}
				else if (fieldOperation.Field.Value.HasIntValue)
				{
					presenceUpdate.intVal = fieldOperation.Field.Value.IntValue;
				}
				else if (fieldOperation.Field.Value.HasStringValue)
				{
					presenceUpdate.stringVal = fieldOperation.Field.Value.StringValue;
				}
				else if (fieldOperation.Field.Value.HasFourccValue)
				{
					presenceUpdate.stringVal = new BnetProgramId(fieldOperation.Field.Value.FourccValue).ToString();
				}
				else if (fieldOperation.Field.Value.HasEntityIdValue)
				{
					presenceUpdate.entityIdVal.hi = fieldOperation.Field.Value.EntityIdValue.High;
					presenceUpdate.entityIdVal.lo = fieldOperation.Field.Value.EntityIdValue.Low;
					bool flag4 = fieldKey2.Program == fieldOperation.Field.Key.Program;
					bool flag5 = fieldKey2.Group == fieldOperation.Field.Key.Group;
					bool flag6 = fieldKey2.Field == fieldOperation.Field.Key.Field;
					if (flag4 && flag5 && flag6)
					{
						BnetEntityId entityId3 = BnetEntityId.CreateFromEntityId(presenceUpdate.entityId);
						m_battleNet.Friends.AddFriendsActiveGameAccount(entityId3, fieldOperation.Field.Value.EntityIdValue, fieldOperation.Field.Key.UniqueId);
					}
				}
				else
				{
					if (!fieldOperation.Field.Value.HasBlobValue)
					{
						if (fieldOperation.Field.Value.HasMessageValue && fieldOperation.Field.Key.Field == 8)
						{
							FetchRichPresenceResource(fieldOperation.Field.Value);
							HandleRichPresenceUpdate(presenceUpdate, fieldOperation.Field.Key);
						}
						continue;
					}
					presenceUpdate.blobVal = fieldOperation.Field.Value.BlobValue;
				}
				list.Add(presenceUpdate);
			}
			list.Reverse();
			m_presenceUpdates.AddRange(list);
		}

		private void HandleRichPresenceUpdate(PresenceUpdate rpUpdate, FieldKey fieldKey)
		{
			FieldKey fieldKey2 = new FieldKey();
			fieldKey2.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey2.SetGroup(2u);
			fieldKey2.SetField(8u);
			fieldKey2.SetUniqueId(0uL);
			if (fieldKey2.Equals(fieldKey))
			{
				m_pendingRichPresenceUpdates.Add(rpUpdate);
				TryToResolveRichPresence();
			}
		}

		private bool FetchRichPresenceResource(Variant presenceValue)
		{
			if (presenceValue == null)
			{
				return false;
			}
			RichPresenceLocalizationKey richPresenceLocalizationKey = RichPresenceLocalizationKey.ParseFrom(presenceValue.MessageValue);
			if (richPresenceLocalizationKey == null || !richPresenceLocalizationKey.IsInitialized)
			{
				base.ApiLog.LogError("Rich presence field from battle.net does not contain valid RichPresence message");
				return false;
			}
			if (m_richPresenceStringTables.ContainsKey(richPresenceLocalizationKey))
			{
				return false;
			}
			FourCC programId = new FourCC(richPresenceLocalizationKey.Program);
			FourCC streamId = new FourCC(richPresenceLocalizationKey.Stream);
			FourCC locale = new FourCC(BattleNet.Client().GetLocaleName());
			IncrementOutstandingRichPresenceStringFetches();
			m_battleNet.Resources.LookupResource(programId, streamId, locale, ResouceLookupCallback, richPresenceLocalizationKey);
			return true;
		}

		private void ResouceLookupCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("BN resource look up failed unable to proceed");
				DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Lookup done Region={0} Usage={1} SHA256={2}", contentHandle.Region, contentHandle.Usage, contentHandle.Sha256Digest);
			m_battleNet.LocalStorage.GetFile(contentHandle, DownloadCompletedCallback, userContext);
		}

		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				base.ApiLog.LogWarning("Downloading of rich presence data from depot failed!");
				DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Downloading of rich presence data completed");
			try
			{
				IndexToStringMap indexToStringMap = new IndexToStringMap();
				using (StringReader input = new StringReader(Encoding.UTF8.GetString(data).Trim('\ufeff', '\u200b')))
				{
					using XmlReader xmlReader = XmlReader.Create(input);
					while (xmlReader.Read())
					{
						if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "e")
						{
							string value = xmlReader.GetAttribute("id");
							string value2 = xmlReader.ReadElementContentAsString();
							ulong key = Convert.ToUInt64(value, 10);
							indexToStringMap[key] = value2;
						}
					}
				}
				lock (m_richPresenceStringTables)
				{
					RichPresenceLocalizationKey key2 = (RichPresenceLocalizationKey)userContext;
					m_richPresenceStringTables[key2] = indexToStringMap;
				}
			}
			catch (Exception ex)
			{
				base.ApiLog.LogWarning("Failed to parse received data into rich presence strings. Ex  = {0}", ex.ToString());
			}
			DecrementOutstandingRichPresenceStringFetches();
		}

		private void IncrementOutstandingRichPresenceStringFetches()
		{
			m_numOutstandingRichPresenceStringFetches++;
		}

		private void DecrementOutstandingRichPresenceStringFetches()
		{
			if (m_numOutstandingRichPresenceStringFetches <= 0)
			{
				base.ApiLog.LogWarning("Number of outstanding rich presence string fetches tracked incorrectly - decremented to negative");
				return;
			}
			m_numOutstandingRichPresenceStringFetches--;
			TryToResolveRichPresence();
		}

		private void TryToResolveRichPresence()
		{
			if (m_numOutstandingRichPresenceStringFetches == 0)
			{
				ResolveRichPresence();
				if (m_pendingRichPresenceUpdates.Count != 0)
				{
					base.ApiLog.LogWarning("Failed to resolve rich presence strings");
					m_pendingRichPresenceUpdates.Clear();
				}
			}
		}

		private void ResolveRichPresence()
		{
			if (m_pendingRichPresenceUpdates.Count == 0)
			{
				return;
			}
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (PresenceUpdate pendingRichPresenceUpdate in m_pendingRichPresenceUpdates)
			{
				if (ResolveRichPresenceStrings(out var richPresenceString, pendingRichPresenceUpdate.entityId, 0uL, 0))
				{
					list.Add(pendingRichPresenceUpdate);
					PresenceUpdate item = pendingRichPresenceUpdate;
					item.fieldId = 1000u;
					item.stringVal = richPresenceString;
					m_presenceUpdates.Add(item);
				}
			}
			foreach (PresenceUpdate item2 in list)
			{
				m_pendingRichPresenceUpdates.Remove(item2);
			}
		}

		private bool ResolveRichPresenceStrings(out string richPresenceString, bgs.types.EntityId entityId, ulong index, int recurseDepth)
		{
			richPresenceString = "";
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(8u);
			fieldKey.SetUniqueId(index);
			Variant cache = m_presenceCache.GetCache(entityId, fieldKey);
			if (cache == null)
			{
				base.ApiLog.LogError("Expected field missing from presence cache when resolving rich presence string");
				return false;
			}
			RichPresenceLocalizationKey richPresenceLocalizationKey = RichPresenceLocalizationKey.ParseFrom(cache.MessageValue);
			if (richPresenceLocalizationKey == null || !richPresenceLocalizationKey.IsInitialized)
			{
				base.ApiLog.LogError("Rich presence field did not contain valid RichPresence message when resolving");
				return false;
			}
			if (!m_richPresenceStringTables.ContainsKey(richPresenceLocalizationKey))
			{
				return false;
			}
			IndexToStringMap indexToStringMap = m_richPresenceStringTables[richPresenceLocalizationKey];
			if (!indexToStringMap.ContainsKey(richPresenceLocalizationKey.LocalizationId))
			{
				base.ApiLog.LogWarning("Rich presence string table data is missing");
				return false;
			}
			richPresenceString = indexToStringMap[richPresenceLocalizationKey.LocalizationId];
			if (recurseDepth < 1 && !SubstituteVariables(out richPresenceString, richPresenceString, entityId, recurseDepth + 1))
			{
				base.ApiLog.LogWarning("Failed to substitute rich presence variables in: {0}", richPresenceString);
				return false;
			}
			return true;
		}

		private bool SubstituteVariables(out string substitutedString, string originalStr, bgs.types.EntityId entityId, int recurseDepth)
		{
			substitutedString = originalStr;
			int num = 0;
			while (num < substitutedString.Length)
			{
				num = substitutedString.IndexOf("$0x", num);
				if (num == -1)
				{
					break;
				}
				int num2 = num + "$0x".Length;
				int num3 = LastIndexOfOccurenceFromIndex(substitutedString, hexChars, num2);
				int length = num3 + 1 - num2;
				int length2 = num3 + 1 - num;
				string text = substitutedString.Substring(num2, length);
				ulong num4 = 0uL;
				try
				{
					num4 = Convert.ToUInt64(text, 16);
				}
				catch (Exception)
				{
					base.ApiLog.LogWarning("Failed to convert {0} to ulong when substiting rich presence variables", text);
					return false;
				}
				if (!ResolveRichPresenceStrings(out var richPresenceString, entityId, num4, recurseDepth))
				{
					base.ApiLog.LogWarning("Failed resolve rich presence string for \"{0}\" when substiting variables in \"{1}\"", num4, originalStr);
					return false;
				}
				string oldValue = substitutedString.Substring(num, length2);
				substitutedString = substitutedString.Replace(oldValue, richPresenceString);
			}
			return true;
		}

		private int LastIndexOfOccurenceFromIndex(string str, char[] testChars, int startIndex)
		{
			int result = -1;
			char[] array = str.ToCharArray();
			for (int i = startIndex; i < array.Length; i++)
			{
				char c = array[i];
				bool flag = false;
				char[] array2 = hexChars;
				foreach (char c2 in array2)
				{
					if (c == c2)
					{
						result = i;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			return result;
		}

		public void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			QueryRequest queryRequest = new QueryRequest();
			bnet.protocol.EntityId entityId2 = new bnet.protocol.EntityId();
			entityId2.SetHigh(entityId.hi);
			entityId2.SetLow(entityId.lo);
			queryRequest.SetEntityId(entityId2);
			for (int i = 0; i < fieldList.Length; i++)
			{
				PresenceFieldKey presenceFieldKey = fieldList[i];
				FieldKey fieldKey = new FieldKey();
				fieldKey.SetProgram(presenceFieldKey.programId);
				fieldKey.SetGroup(presenceFieldKey.groupId);
				fieldKey.SetField(presenceFieldKey.fieldId);
				fieldKey.SetUniqueId(presenceFieldKey.uniqueId);
				queryRequest.AddKey(fieldKey);
			}
			m_rpcConnection.QueueRequest(m_presenceService, 4u, queryRequest, delegate(RPCContext context)
			{
				RequestPresenceFieldsCallback(new bgs.types.EntityId(entityId), context);
			});
		}

		private void RequestPresenceFieldsCallback(bgs.types.EntityId entityId, RPCContext context)
		{
			if (!CheckRPCCallback("RequestPresenceFieldsCallback", context))
			{
				return;
			}
			foreach (Field field in QueryResponse.ParseFrom(context.Payload).FieldList)
			{
				m_presenceCache.SetCache(entityId, field.Key, field.Value);
				PresenceUpdate presenceUpdate = default(PresenceUpdate);
				presenceUpdate.entityId = entityId;
				presenceUpdate.programId = field.Key.Program;
				presenceUpdate.groupId = field.Key.Group;
				presenceUpdate.fieldId = field.Key.Field;
				presenceUpdate.index = field.Key.UniqueId;
				presenceUpdate.boolVal = false;
				presenceUpdate.intVal = 0L;
				presenceUpdate.stringVal = "";
				presenceUpdate.valCleared = false;
				presenceUpdate.blobVal = new byte[0];
				if (field.Value.HasBoolValue)
				{
					presenceUpdate.boolVal = field.Value.BoolValue;
				}
				else if (field.Value.HasIntValue)
				{
					presenceUpdate.intVal = field.Value.IntValue;
				}
				else if (!field.Value.HasFloatValue)
				{
					if (field.Value.HasStringValue)
					{
						presenceUpdate.stringVal = field.Value.StringValue;
					}
					else if (field.Value.HasBlobValue)
					{
						presenceUpdate.blobVal = field.Value.BlobValue;
					}
					else if (field.Value.HasMessageValue)
					{
						if (field.Key.Field == 8)
						{
							FetchRichPresenceResource(field.Value);
							HandleRichPresenceUpdate(presenceUpdate, field.Key);
						}
						else
						{
							presenceUpdate.blobVal = field.Value.MessageValue;
						}
					}
					else if (field.Value.HasFourccValue)
					{
						presenceUpdate.stringVal = new BnetProgramId(field.Value.FourccValue).ToString();
					}
					else if (!field.Value.HasUintValue)
					{
						if (field.Value.HasEntityIdValue)
						{
							presenceUpdate.entityIdVal.hi = field.Value.EntityIdValue.High;
							presenceUpdate.entityIdVal.lo = field.Value.EntityIdValue.Low;
						}
						else
						{
							presenceUpdate.valCleared = true;
						}
					}
				}
				m_presenceUpdates.Add(presenceUpdate);
			}
		}

		private void HandleChannelSubscriber_NotifyAdd(RPCContext context)
		{
			JoinNotification joinNotification = JoinNotification.ParseFrom(context.Payload);
			PresenceChannelReferenceObject presenceChannelReferenceObject = GetPresenceChannelReferenceObject(context.Header.ObjectId);
			if (presenceChannelReferenceObject == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyAdd had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
			else if (joinNotification.ChannelState.HasPresence)
			{
				bnet.protocol.presence.v1.ChannelState presence = joinNotification.ChannelState.Presence;
				m_battleNet.Presence.HandlePresenceUpdates(presence, presenceChannelReferenceObject);
			}
		}

		private void HandleChannelSubscriber_NotifyJoin(RPCContext context)
		{
			MemberAddedNotification memberAddedNotification = MemberAddedNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyJoin: " + memberAddedNotification);
			if (GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyJoin had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
		}

		private void HandleChannelSubscriber_NotifyRemove(RPCContext context)
		{
			LeaveNotification leaveNotification = LeaveNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyRemove: " + leaveNotification);
			if (GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyRemove had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
			else
			{
				RemoveActiveChannel(context.Header.ObjectId);
			}
		}

		private void HandleChannelSubscriber_NotifyLeave(RPCContext context)
		{
			MemberRemovedNotification memberRemovedNotification = MemberRemovedNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyLeave: " + memberRemovedNotification);
			if (GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyLeave had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
		}

		private void HandleChannelSubscriber_NotifySendMessage(RPCContext context)
		{
			SendMessageNotification sendMessageNotification = SendMessageNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifySendMessage: " + sendMessageNotification);
			if (GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifySendMessage had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
		}

		private void HandleChannelSubscriber_NotifyUpdateChannelState(RPCContext context)
		{
			UpdateChannelStateNotification updateChannelStateNotification = UpdateChannelStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateChannelState: " + updateChannelStateNotification);
			PresenceChannelReferenceObject presenceChannelReferenceObject = GetPresenceChannelReferenceObject(context.Header.ObjectId);
			if (presenceChannelReferenceObject == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyUpdateChannelState had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
			else if (updateChannelStateNotification.StateChange.HasPresence)
			{
				bnet.protocol.presence.v1.ChannelState presence = updateChannelStateNotification.StateChange.Presence;
				m_battleNet.Presence.HandlePresenceUpdates(presence, presenceChannelReferenceObject);
			}
		}

		private void HandleChannelSubscriber_NotifyUpdateMemberState(RPCContext context)
		{
			UpdateMemberStateNotification updateMemberStateNotification = UpdateMemberStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateMemberState: " + updateMemberStateNotification);
			if (GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyUpdateMemberState had unexpected traffic for objectId : " + context.Header.ObjectId);
			}
		}
	}
}
