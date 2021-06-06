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
	// Token: 0x0200020A RID: 522
	public class PresenceAPI : BattleNetAPI
	{
		// Token: 0x06002057 RID: 8279 RVA: 0x00073B15 File Offset: 0x00071D15
		public static ulong GetNextObjectId()
		{
			return PresenceAPI.s_nextObjectId += 1UL;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00073B25 File Offset: 0x00071D25
		public void AddActiveChannel(ulong objectId, PresenceAPI.PresenceChannelReferenceObject channelRefObject)
		{
			this.m_activeChannels.Add(objectId, channelRefObject);
			this.m_channelEntityObjectMap[channelRefObject.m_channelData.m_channelId] = objectId;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00073B4C File Offset: 0x00071D4C
		public void RemoveActiveChannel(ulong objectId)
		{
			PresenceAPI.PresenceChannelReferenceObject presenceChannelReferenceObject = this.GetPresenceChannelReferenceObject(objectId);
			if (presenceChannelReferenceObject != null)
			{
				this.m_channelEntityObjectMap.Remove(presenceChannelReferenceObject.m_channelData.m_channelId);
				this.m_activeChannels.Remove(objectId);
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00073B88 File Offset: 0x00071D88
		public PresenceAPI.PresenceChannelReferenceObject GetPresenceChannelReferenceObject(ulong objectId)
		{
			PresenceAPI.PresenceChannelReferenceObject result;
			if (this.m_activeChannels.TryGetValue(objectId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00073BA8 File Offset: 0x00071DA8
		public PresenceAPI(BattleNetCSharp battlenet) : base(battlenet, "Presence")
		{
			this.m_stopWatch = new Stopwatch();
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00073C3A File Offset: 0x00071E3A
		public ServiceDescriptor PresenceService
		{
			get
			{
				return this.m_presenceService;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00073C42 File Offset: 0x00071E42
		public ServiceDescriptor ChannelSubscriberService
		{
			get
			{
				return this.m_channelSubscriberService;
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00073C4C File Offset: 0x00071E4C
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 1U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyAdd));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 2U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyJoin));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 3U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyRemove));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 4U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyLeave));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 5U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifySendMessage));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 6U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyUpdateChannelState));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 7U, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyUpdateMemberState));
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00073D55 File Offset: 0x00071F55
		public override void Initialize()
		{
			base.Initialize();
			this.m_stopWatch.Start();
			this.m_lastPresenceSubscriptionSent = 0L;
			this.m_presenceSubscriptionBalance = 0f;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00073D7C File Offset: 0x00071F7C
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_activeChannels.Clear();
			this.m_channelEntityObjectMap.Clear();
			this.m_presenceSubscriptions.Clear();
			this.m_presenceUpdates.Clear();
			this.m_queuedSubscriptions.Clear();
			this.m_stopWatch.Stop();
			this.m_lastPresenceSubscriptionSent = 0L;
			this.m_presenceSubscriptionBalance = 0f;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00073DE4 File Offset: 0x00071FE4
		public override void Process()
		{
			base.Process();
			this.HandleSubscriptionRequests();
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00073DF2 File Offset: 0x00071FF2
		public int PresenceSize()
		{
			return this.m_presenceUpdates.Count;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00073DFF File Offset: 0x00071FFF
		public void ClearPresence()
		{
			this.m_presenceUpdates.Clear();
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00073E0C File Offset: 0x0007200C
		public void GetPresence([Out] PresenceUpdate[] updates)
		{
			this.m_presenceUpdates.CopyTo(updates);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00073E1A File Offset: 0x0007201A
		public void SetPresenceBool(uint field, bool val)
		{
			this.SetPresenceBool(field, val, BnetProgramId.HEARTHSTONE, 2U, this.m_battleNet.GameAccountId);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00073E35 File Offset: 0x00072035
		public void SetAccountLevelPresenceBool(uint field, bool val)
		{
			this.SetPresenceBool(field, val, BnetProgramId.BNET, 1U, this.m_battleNet.AccountId);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00073E50 File Offset: 0x00072050
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
			this.PublishField(updateRequest);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00073ECC File Offset: 0x000720CC
		public void SetPresenceInt(uint field, long val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2U);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetIntValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00073F64 File Offset: 0x00072164
		public void SetPresenceString(uint field, string val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2U);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetStringValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00073FFC File Offset: 0x000721FC
		public void SetPresenceBlob(uint field, byte[] val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2U);
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
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000740A0 File Offset: 0x000722A0
		public void SetPresenceEntityId(uint field, bgs.types.EntityId val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			fieldKey.SetGroup(2U);
			fieldKey.SetField(field);
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetHigh(val.hi);
			entityId.SetLow(val.lo);
			Variant variant = new Variant();
			variant.SetEntityIdValue(entityId);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0007415C File Offset: 0x0007235C
		public void PublishRichPresence([In] RichPresenceUpdate[] updates)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			foreach (RichPresenceUpdate richPresenceUpdate in updates)
			{
				FieldOperation fieldOperation = new FieldOperation();
				Field field = new Field();
				FieldKey fieldKey = new FieldKey();
				fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
				fieldKey.SetGroup(2U);
				fieldKey.SetField(8U);
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
				updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
				updateRequest.AddFieldOperation(fieldOperation);
			}
			this.PublishField(updateRequest);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00074260 File Offset: 0x00072460
		private void HandleSubscriptionRequests()
		{
			if (this.m_queuedSubscriptions.Count > 0)
			{
				long elapsedMilliseconds = this.m_stopWatch.ElapsedMilliseconds;
				this.m_presenceSubscriptionBalance = Math.Min(0f, this.m_presenceSubscriptionBalance + (float)(elapsedMilliseconds - this.m_lastPresenceSubscriptionSent) * 0.0033333334f);
				this.m_lastPresenceSubscriptionSent = elapsedMilliseconds;
				bool inStartupPeriod = this.m_rpcConnection.GetInStartupPeriod();
				List<bnet.protocol.EntityId> list = new List<bnet.protocol.EntityId>();
				foreach (bnet.protocol.EntityId entityId in this.m_queuedSubscriptions.ToList<bnet.protocol.EntityId>())
				{
					if (this.m_presenceSubscriptionBalance - 1f < -100f)
					{
						break;
					}
					PresenceAPI.PresenceRefCountObject presenceRefCountObject = this.m_presenceSubscriptions[entityId];
					bnet.protocol.presence.v1.SubscribeRequest subscribeRequest = new bnet.protocol.presence.v1.SubscribeRequest();
					subscribeRequest.SetObjectId(PresenceAPI.GetNextObjectId());
					subscribeRequest.SetEntityId(entityId);
					presenceRefCountObject.objectId = subscribeRequest.ObjectId;
					this.AddActiveChannel(subscribeRequest.ObjectId, new PresenceAPI.PresenceChannelReferenceObject(entityId));
					this.m_rpcConnection.QueueRequest(this.m_presenceService, 1U, subscribeRequest, new RPCContextDelegate(this.PresenceSubscribeCallback), 0U);
					if (!inStartupPeriod)
					{
						this.m_presenceSubscriptionBalance -= 1f;
					}
					list.Add(entityId);
				}
				foreach (bnet.protocol.EntityId item in list)
				{
					this.m_queuedSubscriptions.Remove(item);
				}
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000743FC File Offset: 0x000725FC
		public void PresenceSubscribe(bnet.protocol.EntityId entityId)
		{
			if (this.m_presenceSubscriptions.ContainsKey(entityId))
			{
				this.m_presenceSubscriptions[entityId].refCount++;
				return;
			}
			PresenceAPI.PresenceRefCountObject presenceRefCountObject = new PresenceAPI.PresenceRefCountObject();
			presenceRefCountObject.objectId = 0UL;
			presenceRefCountObject.refCount = 1;
			this.m_presenceSubscriptions.Add(entityId, presenceRefCountObject);
			this.m_queuedSubscriptions.Add(entityId);
			this.HandleSubscriptionRequests();
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00074468 File Offset: 0x00072668
		public void PresenceUnsubscribe(bnet.protocol.EntityId entityId)
		{
			if (this.m_presenceSubscriptions.ContainsKey(entityId))
			{
				this.m_presenceSubscriptions[entityId].refCount--;
				if (this.m_presenceSubscriptions[entityId].refCount <= 0)
				{
					if (this.m_queuedSubscriptions.Contains(entityId))
					{
						this.m_queuedSubscriptions.Remove(entityId);
						return;
					}
					PresenceAPI.PresenceUnsubscribeContext @object = new PresenceAPI.PresenceUnsubscribeContext(this.m_battleNet, this.m_presenceSubscriptions[entityId].objectId);
					UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
					unsubscribeRequest.SetEntityId(entityId);
					this.m_rpcConnection.QueueRequest(this.m_presenceService, 2U, unsubscribeRequest, new RPCContextDelegate(@object.PresenceUnsubscribeCallback), 0U);
					this.m_presenceSubscriptions.Remove(entityId);
				}
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00074527 File Offset: 0x00072727
		public bool IsSubscribedToEntity(bnet.protocol.EntityId entityId)
		{
			return this.m_presenceSubscriptions.ContainsKey(entityId) && this.m_presenceSubscriptions[entityId].refCount != 0;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0007454D File Offset: 0x0007274D
		public void PublishField(UpdateRequest updateRequest)
		{
			if (this.m_rpcConnection == null)
			{
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_presenceService, 3U, updateRequest, new RPCContextDelegate(this.PresenceUpdateCallback), 0U);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00074579 File Offset: 0x00072779
		private void PresenceSubscribeCallback(RPCContext context)
		{
			base.CheckRPCCallback("PresenceSubscribeCallback", context);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00074588 File Offset: 0x00072788
		private void PresenceUpdateCallback(RPCContext context)
		{
			base.CheckRPCCallback("PresenceUpdateCallback", context);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00074598 File Offset: 0x00072798
		public void HandlePresenceUpdates(bnet.protocol.presence.v1.ChannelState channelState, PresenceAPI.PresenceChannelReferenceObject channelRef)
		{
			bgs.types.EntityId entityId;
			entityId.hi = channelRef.m_channelData.m_channelId.High;
			entityId.lo = channelRef.m_channelData.m_channelId.Low;
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(1U);
			fieldKey.SetField(3U);
			FieldKey fieldKey2 = fieldKey;
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (FieldOperation fieldOperation in channelState.FieldOperationList)
			{
				if (fieldOperation.Operation == FieldOperation.Types.OperationType.CLEAR)
				{
					this.m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, null);
				}
				else
				{
					this.m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, fieldOperation.Field.Value);
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
						this.m_battleNet.Friends.RemoveFriendsActiveGameAccount(entityId2, fieldOperation.Field.Key.UniqueId);
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
						this.m_battleNet.Friends.AddFriendsActiveGameAccount(entityId3, fieldOperation.Field.Value.EntityIdValue, fieldOperation.Field.Key.UniqueId);
					}
				}
				else if (fieldOperation.Field.Value.HasBlobValue)
				{
					presenceUpdate.blobVal = fieldOperation.Field.Value.BlobValue;
				}
				else
				{
					if (fieldOperation.Field.Value.HasMessageValue && fieldOperation.Field.Key.Field == 8U)
					{
						this.FetchRichPresenceResource(fieldOperation.Field.Value);
						this.HandleRichPresenceUpdate(presenceUpdate, fieldOperation.Field.Key);
						continue;
					}
					continue;
				}
				list.Add(presenceUpdate);
			}
			list.Reverse();
			this.m_presenceUpdates.AddRange(list);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00074A44 File Offset: 0x00072C44
		private void HandleRichPresenceUpdate(PresenceUpdate rpUpdate, FieldKey fieldKey)
		{
			FieldKey fieldKey2 = new FieldKey();
			fieldKey2.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey2.SetGroup(2U);
			fieldKey2.SetField(8U);
			fieldKey2.SetUniqueId(0UL);
			if (!fieldKey2.Equals(fieldKey))
			{
				return;
			}
			this.m_pendingRichPresenceUpdates.Add(rpUpdate);
			this.TryToResolveRichPresence();
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00074A98 File Offset: 0x00072C98
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
			if (this.m_richPresenceStringTables.ContainsKey(richPresenceLocalizationKey))
			{
				return false;
			}
			FourCC programId = new FourCC(richPresenceLocalizationKey.Program);
			FourCC streamId = new FourCC(richPresenceLocalizationKey.Stream);
			FourCC locale = new FourCC(BattleNet.Client().GetLocaleName());
			this.IncrementOutstandingRichPresenceStringFetches();
			this.m_battleNet.Resources.LookupResource(programId, streamId, locale, new ResourcesAPI.ResourceLookupCallback(this.ResouceLookupCallback), richPresenceLocalizationKey);
			return true;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00074B34 File Offset: 0x00072D34
		private void ResouceLookupCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("BN resource look up failed unable to proceed");
				this.DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Lookup done Region={0} Usage={1} SHA256={2}", new object[]
			{
				contentHandle.Region,
				contentHandle.Usage,
				contentHandle.Sha256Digest
			});
			this.m_battleNet.LocalStorage.GetFile(contentHandle, new LocalStorageAPI.DownloadCompletedCallback(this.DownloadCompletedCallback), userContext);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00074BAC File Offset: 0x00072DAC
		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				base.ApiLog.LogWarning("Downloading of rich presence data from depot failed!");
				this.DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Downloading of rich presence data completed");
			try
			{
				PresenceAPI.IndexToStringMap indexToStringMap = new PresenceAPI.IndexToStringMap();
				using (StringReader stringReader = new StringReader(Encoding.UTF8.GetString(data).Trim(new char[]
				{
					'﻿',
					'​'
				})))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader))
					{
						while (xmlReader.Read())
						{
							if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "e")
							{
								string attribute = xmlReader.GetAttribute("id");
								string value = xmlReader.ReadElementContentAsString();
								ulong key = Convert.ToUInt64(attribute, 10);
								indexToStringMap[key] = value;
							}
						}
					}
				}
				PresenceAPI.RichPresenceToStringsMap richPresenceStringTables = this.m_richPresenceStringTables;
				lock (richPresenceStringTables)
				{
					RichPresenceLocalizationKey key2 = (RichPresenceLocalizationKey)userContext;
					this.m_richPresenceStringTables[key2] = indexToStringMap;
				}
			}
			catch (Exception ex)
			{
				base.ApiLog.LogWarning("Failed to parse received data into rich presence strings. Ex  = {0}", new object[]
				{
					ex.ToString()
				});
			}
			this.DecrementOutstandingRichPresenceStringFetches();
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00074D18 File Offset: 0x00072F18
		private void IncrementOutstandingRichPresenceStringFetches()
		{
			this.m_numOutstandingRichPresenceStringFetches++;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00074D28 File Offset: 0x00072F28
		private void DecrementOutstandingRichPresenceStringFetches()
		{
			if (this.m_numOutstandingRichPresenceStringFetches <= 0)
			{
				base.ApiLog.LogWarning("Number of outstanding rich presence string fetches tracked incorrectly - decremented to negative");
				return;
			}
			this.m_numOutstandingRichPresenceStringFetches--;
			this.TryToResolveRichPresence();
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00074D58 File Offset: 0x00072F58
		private void TryToResolveRichPresence()
		{
			if (this.m_numOutstandingRichPresenceStringFetches == 0)
			{
				this.ResolveRichPresence();
				if (this.m_pendingRichPresenceUpdates.Count != 0)
				{
					base.ApiLog.LogWarning("Failed to resolve rich presence strings");
					this.m_pendingRichPresenceUpdates.Clear();
				}
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00074D90 File Offset: 0x00072F90
		private void ResolveRichPresence()
		{
			if (this.m_pendingRichPresenceUpdates.Count == 0)
			{
				return;
			}
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (PresenceUpdate presenceUpdate in this.m_pendingRichPresenceUpdates)
			{
				string stringVal;
				if (this.ResolveRichPresenceStrings(out stringVal, presenceUpdate.entityId, 0UL, 0))
				{
					list.Add(presenceUpdate);
					PresenceUpdate item = presenceUpdate;
					item.fieldId = 1000U;
					item.stringVal = stringVal;
					this.m_presenceUpdates.Add(item);
				}
			}
			foreach (PresenceUpdate item2 in list)
			{
				this.m_pendingRichPresenceUpdates.Remove(item2);
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00074E74 File Offset: 0x00073074
		private bool ResolveRichPresenceStrings(out string richPresenceString, bgs.types.EntityId entityId, ulong index, int recurseDepth)
		{
			richPresenceString = "";
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(2U);
			fieldKey.SetField(8U);
			fieldKey.SetUniqueId(index);
			Variant cache = this.m_presenceCache.GetCache(entityId, fieldKey);
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
			if (!this.m_richPresenceStringTables.ContainsKey(richPresenceLocalizationKey))
			{
				return false;
			}
			PresenceAPI.IndexToStringMap indexToStringMap = this.m_richPresenceStringTables[richPresenceLocalizationKey];
			if (!indexToStringMap.ContainsKey((ulong)richPresenceLocalizationKey.LocalizationId))
			{
				base.ApiLog.LogWarning("Rich presence string table data is missing");
				return false;
			}
			richPresenceString = indexToStringMap[(ulong)richPresenceLocalizationKey.LocalizationId];
			if (recurseDepth < 1 && !this.SubstituteVariables(out richPresenceString, richPresenceString, entityId, recurseDepth + 1))
			{
				base.ApiLog.LogWarning("Failed to substitute rich presence variables in: {0}", new object[]
				{
					richPresenceString
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00074F80 File Offset: 0x00073180
		private bool SubstituteVariables(out string substitutedString, string originalStr, bgs.types.EntityId entityId, int recurseDepth)
		{
			substitutedString = originalStr;
			int i = 0;
			while (i < substitutedString.Length)
			{
				i = substitutedString.IndexOf("$0x", i);
				if (i != -1)
				{
					int num = i + "$0x".Length;
					int num2 = this.LastIndexOfOccurenceFromIndex(substitutedString, PresenceAPI.hexChars, num);
					int length = num2 + 1 - num;
					int length2 = num2 + 1 - i;
					string text = substitutedString.Substring(num, length);
					ulong num3 = 0UL;
					try
					{
						num3 = Convert.ToUInt64(text, 16);
					}
					catch (Exception)
					{
						base.ApiLog.LogWarning("Failed to convert {0} to ulong when substiting rich presence variables", new object[]
						{
							text
						});
						return false;
					}
					string newValue;
					if (!this.ResolveRichPresenceStrings(out newValue, entityId, num3, recurseDepth))
					{
						base.ApiLog.LogWarning("Failed resolve rich presence string for \"{0}\" when substiting variables in \"{1}\"", new object[]
						{
							num3,
							originalStr
						});
						return false;
					}
					string oldValue = substitutedString.Substring(i, length2);
					substitutedString = substitutedString.Replace(oldValue, newValue);
					continue;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00075080 File Offset: 0x00073280
		private int LastIndexOfOccurenceFromIndex(string str, char[] testChars, int startIndex)
		{
			int result = -1;
			char[] array = str.ToCharArray();
			for (int i = startIndex; i < array.Length; i++)
			{
				char c = array[i];
				bool flag = false;
				foreach (char c2 in PresenceAPI.hexChars)
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

		// Token: 0x06002080 RID: 8320 RVA: 0x000750E0 File Offset: 0x000732E0
		public void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			QueryRequest queryRequest = new QueryRequest();
			bnet.protocol.EntityId entityId2 = new bnet.protocol.EntityId();
			entityId2.SetHigh(entityId.hi);
			entityId2.SetLow(entityId.lo);
			queryRequest.SetEntityId(entityId2);
			foreach (PresenceFieldKey presenceFieldKey in fieldList)
			{
				FieldKey fieldKey = new FieldKey();
				fieldKey.SetProgram(presenceFieldKey.programId);
				fieldKey.SetGroup(presenceFieldKey.groupId);
				fieldKey.SetField(presenceFieldKey.fieldId);
				fieldKey.SetUniqueId(presenceFieldKey.uniqueId);
				queryRequest.AddKey(fieldKey);
			}
			this.m_rpcConnection.QueueRequest(this.m_presenceService, 4U, queryRequest, delegate(RPCContext context)
			{
				this.RequestPresenceFieldsCallback(new bgs.types.EntityId(entityId), context);
			}, 0U);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000751BC File Offset: 0x000733BC
		private void RequestPresenceFieldsCallback(bgs.types.EntityId entityId, RPCContext context)
		{
			if (base.CheckRPCCallback("RequestPresenceFieldsCallback", context))
			{
				foreach (Field field in QueryResponse.ParseFrom(context.Payload).FieldList)
				{
					this.m_presenceCache.SetCache(entityId, field.Key, field.Value);
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
							if (field.Key.Field == 8U)
							{
								this.FetchRichPresenceResource(field.Value);
								this.HandleRichPresenceUpdate(presenceUpdate, field.Key);
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
					this.m_presenceUpdates.Add(presenceUpdate);
				}
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0007545C File Offset: 0x0007365C
		private void HandleChannelSubscriber_NotifyAdd(RPCContext context)
		{
			JoinNotification joinNotification = JoinNotification.ParseFrom(context.Payload);
			PresenceAPI.PresenceChannelReferenceObject presenceChannelReferenceObject = this.GetPresenceChannelReferenceObject(context.Header.ObjectId);
			if (presenceChannelReferenceObject == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyAdd had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			if (joinNotification.ChannelState.HasPresence)
			{
				bnet.protocol.presence.v1.ChannelState presence = joinNotification.ChannelState.Presence;
				this.m_battleNet.Presence.HandlePresenceUpdates(presence, presenceChannelReferenceObject);
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000754DC File Offset: 0x000736DC
		private void HandleChannelSubscriber_NotifyJoin(RPCContext context)
		{
			MemberAddedNotification arg = MemberAddedNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyJoin: " + arg);
			if (this.GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyJoin had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00075544 File Offset: 0x00073744
		private void HandleChannelSubscriber_NotifyRemove(RPCContext context)
		{
			LeaveNotification arg = LeaveNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyRemove: " + arg);
			if (this.GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyRemove had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			this.RemoveActiveChannel(context.Header.ObjectId);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000755C0 File Offset: 0x000737C0
		private void HandleChannelSubscriber_NotifyLeave(RPCContext context)
		{
			MemberRemovedNotification arg = MemberRemovedNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyLeave: " + arg);
			if (this.GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyLeave had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00075628 File Offset: 0x00073828
		private void HandleChannelSubscriber_NotifySendMessage(RPCContext context)
		{
			SendMessageNotification arg = SendMessageNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifySendMessage: " + arg);
			if (this.GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifySendMessage had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00075690 File Offset: 0x00073890
		private void HandleChannelSubscriber_NotifyUpdateChannelState(RPCContext context)
		{
			UpdateChannelStateNotification updateChannelStateNotification = UpdateChannelStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateChannelState: " + updateChannelStateNotification);
			PresenceAPI.PresenceChannelReferenceObject presenceChannelReferenceObject = this.GetPresenceChannelReferenceObject(context.Header.ObjectId);
			if (presenceChannelReferenceObject == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyUpdateChannelState had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			if (updateChannelStateNotification.StateChange.HasPresence)
			{
				bnet.protocol.presence.v1.ChannelState presence = updateChannelStateNotification.StateChange.Presence;
				this.m_battleNet.Presence.HandlePresenceUpdates(presence, presenceChannelReferenceObject);
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00075728 File Offset: 0x00073928
		private void HandleChannelSubscriber_NotifyUpdateMemberState(RPCContext context)
		{
			UpdateMemberStateNotification arg = UpdateMemberStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateMemberState: " + arg);
			if (this.GetPresenceChannelReferenceObject(context.Header.ObjectId) == null)
			{
				base.ApiLog.LogInfo("HandleChannelSubscriber_NotifyUpdateMemberState had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
		}

		// Token: 0x04000B9C RID: 2972
		private const float CREDIT_LIMIT = -100f;

		// Token: 0x04000B9D RID: 2973
		private const float COST_PER_REQUEST = 1f;

		// Token: 0x04000B9E RID: 2974
		private const float PAYDOWN_RATE_PER_MS = 0.0033333334f;

		// Token: 0x04000B9F RID: 2975
		private float m_presenceSubscriptionBalance;

		// Token: 0x04000BA0 RID: 2976
		private long m_lastPresenceSubscriptionSent;

		// Token: 0x04000BA1 RID: 2977
		private Stopwatch m_stopWatch;

		// Token: 0x04000BA2 RID: 2978
		private HashSet<bnet.protocol.EntityId> m_queuedSubscriptions = new HashSet<bnet.protocol.EntityId>();

		// Token: 0x04000BA3 RID: 2979
		private Map<ulong, PresenceAPI.PresenceChannelReferenceObject> m_activeChannels = new Map<ulong, PresenceAPI.PresenceChannelReferenceObject>();

		// Token: 0x04000BA4 RID: 2980
		private Map<bnet.protocol.EntityId, ulong> m_channelEntityObjectMap = new Map<bnet.protocol.EntityId, ulong>();

		// Token: 0x04000BA5 RID: 2981
		private Map<bnet.protocol.EntityId, PresenceAPI.PresenceRefCountObject> m_presenceSubscriptions = new Map<bnet.protocol.EntityId, PresenceAPI.PresenceRefCountObject>();

		// Token: 0x04000BA6 RID: 2982
		private List<PresenceUpdate> m_presenceUpdates = new List<PresenceUpdate>();

		// Token: 0x04000BA7 RID: 2983
		private PresenceAPI.EntityIdToFieldsMap m_presenceCache = new PresenceAPI.EntityIdToFieldsMap();

		// Token: 0x04000BA8 RID: 2984
		private PresenceAPI.RichPresenceToStringsMap m_richPresenceStringTables = new PresenceAPI.RichPresenceToStringsMap();

		// Token: 0x04000BA9 RID: 2985
		private HashSet<PresenceUpdate> m_pendingRichPresenceUpdates = new HashSet<PresenceUpdate>();

		// Token: 0x04000BAA RID: 2986
		private int m_numOutstandingRichPresenceStringFetches;

		// Token: 0x04000BAB RID: 2987
		private static ulong s_nextObjectId = 0UL;

		// Token: 0x04000BAC RID: 2988
		private ServiceDescriptor m_presenceService = new PresenceService();

		// Token: 0x04000BAD RID: 2989
		private ServiceDescriptor m_channelSubscriberService = new ChannelSubscriberService();

		// Token: 0x04000BAE RID: 2990
		private const string variablePrefix = "$0x";

		// Token: 0x04000BAF RID: 2991
		private static char[] hexChars = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x020006AD RID: 1709
		public class PresenceRefCountObject
		{
			// Token: 0x040021F6 RID: 8694
			public ulong objectId;

			// Token: 0x040021F7 RID: 8695
			public int refCount;
		}

		// Token: 0x020006AE RID: 1710
		private class FieldKeyToPresenceMap : Map<FieldKey, Variant>
		{
		}

		// Token: 0x020006AF RID: 1711
		private class EntityIdToFieldsMap : Map<bgs.types.EntityId, PresenceAPI.FieldKeyToPresenceMap>
		{
			// Token: 0x06006241 RID: 25153 RVA: 0x0012867D File Offset: 0x0012687D
			public void SetCache(bgs.types.EntityId entity, FieldKey key, Variant value)
			{
				if (key == null)
				{
					return;
				}
				if (!base.ContainsKey(entity))
				{
					base[entity] = new PresenceAPI.FieldKeyToPresenceMap();
				}
				base[entity][key] = value;
			}

			// Token: 0x06006242 RID: 25154 RVA: 0x001286A8 File Offset: 0x001268A8
			public Variant GetCache(bgs.types.EntityId entity, FieldKey key)
			{
				if (key == null)
				{
					return null;
				}
				if (!base.ContainsKey(entity))
				{
					return null;
				}
				PresenceAPI.FieldKeyToPresenceMap fieldKeyToPresenceMap = base[entity];
				if (!fieldKeyToPresenceMap.ContainsKey(key))
				{
					return null;
				}
				return fieldKeyToPresenceMap[key];
			}
		}

		// Token: 0x020006B0 RID: 1712
		private class IndexToStringMap : Map<ulong, string>
		{
		}

		// Token: 0x020006B1 RID: 1713
		private class RichPresenceToStringsMap : Map<RichPresenceLocalizationKey, PresenceAPI.IndexToStringMap>
		{
		}

		// Token: 0x020006B2 RID: 1714
		public class PresenceChannelReferenceObject
		{
			// Token: 0x06006246 RID: 25158 RVA: 0x001286F7 File Offset: 0x001268F7
			public PresenceChannelReferenceObject(bnet.protocol.EntityId entityId)
			{
				this.m_channelData = new PresenceAPI.BasePresenceChannelData(entityId, 0UL);
			}

			// Token: 0x06006247 RID: 25159 RVA: 0x0012870D File Offset: 0x0012690D
			public PresenceChannelReferenceObject(PresenceAPI.BasePresenceChannelData channelData)
			{
				this.m_channelData = channelData;
			}

			// Token: 0x040021F8 RID: 8696
			public PresenceAPI.BasePresenceChannelData m_channelData;
		}

		// Token: 0x020006B3 RID: 1715
		public class BasePresenceChannelData
		{
			// Token: 0x06006248 RID: 25160 RVA: 0x0012871C File Offset: 0x0012691C
			public BasePresenceChannelData(bnet.protocol.EntityId entityId, ulong objectId)
			{
				this.m_channelId = entityId;
				this.m_objectId = objectId;
			}

			// Token: 0x06006249 RID: 25161 RVA: 0x00128732 File Offset: 0x00126932
			public void SetChannelId(bnet.protocol.EntityId channelId)
			{
				this.m_channelId = channelId;
			}

			// Token: 0x0600624A RID: 25162 RVA: 0x0012873B File Offset: 0x0012693B
			public void SetObjectId(ulong objectId)
			{
				this.m_objectId = objectId;
			}

			// Token: 0x0600624B RID: 25163 RVA: 0x00128744 File Offset: 0x00126944
			public void SetSubscriberObjectId(ulong objectId)
			{
				this.m_subscriberObjectId = objectId;
			}

			// Token: 0x040021F9 RID: 8697
			public bnet.protocol.EntityId m_channelId;

			// Token: 0x040021FA RID: 8698
			public ulong m_objectId;

			// Token: 0x040021FB RID: 8699
			public ulong m_subscriberObjectId;
		}

		// Token: 0x020006B4 RID: 1716
		public class PresenceUnsubscribeContext
		{
			// Token: 0x0600624C RID: 25164 RVA: 0x0012874D File Offset: 0x0012694D
			public PresenceUnsubscribeContext(BattleNetCSharp battleNet, ulong objectId)
			{
				this.m_battleNet = battleNet;
				this.m_objectId = objectId;
			}

			// Token: 0x0600624D RID: 25165 RVA: 0x00128763 File Offset: 0x00126963
			public void PresenceUnsubscribeCallback(RPCContext context)
			{
				if (this.m_battleNet.Presence.CheckRPCCallback("PresenceUnsubscribeCallback", context))
				{
					this.m_battleNet.Presence.RemoveActiveChannel(this.m_objectId);
				}
			}

			// Token: 0x040021FC RID: 8700
			private ulong m_objectId;

			// Token: 0x040021FD RID: 8701
			private BattleNetCSharp m_battleNet;
		}
	}
}
