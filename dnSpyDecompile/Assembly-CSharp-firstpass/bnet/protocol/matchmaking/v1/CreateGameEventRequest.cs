using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CC RID: 972
	public class CreateGameEventRequest : IProtoBuf
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x000CB51A File Offset: 0x000C971A
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x000CB522 File Offset: 0x000C9722
		public uint MatchmakerId
		{
			get
			{
				return this._MatchmakerId;
			}
			set
			{
				this._MatchmakerId = value;
				this.HasMatchmakerId = true;
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000CB532 File Offset: 0x000C9732
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000CB53B File Offset: 0x000C973B
		// (set) Token: 0x06003F9F RID: 16287 RVA: 0x000CB543 File Offset: 0x000C9743
		public MatchmakingEventInfo EventInfo
		{
			get
			{
				return this._EventInfo;
			}
			set
			{
				this._EventInfo = value;
				this.HasEventInfo = (value != null);
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000CB556 File Offset: 0x000C9756
		public void SetEventInfo(MatchmakingEventInfo val)
		{
			this.EventInfo = val;
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000CB55F File Offset: 0x000C975F
		// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x000CB567 File Offset: 0x000C9767
		public GameCreationProperties CreationProperties
		{
			get
			{
				return this._CreationProperties;
			}
			set
			{
				this._CreationProperties = value;
				this.HasCreationProperties = (value != null);
			}
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000CB57A File Offset: 0x000C977A
		public void SetCreationProperties(GameCreationProperties val)
		{
			this.CreationProperties = val;
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x000CB583 File Offset: 0x000C9783
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x000CB58B File Offset: 0x000C978B
		public uint GameInstanceId
		{
			get
			{
				return this._GameInstanceId;
			}
			set
			{
				this._GameInstanceId = value;
				this.HasGameInstanceId = true;
			}
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000CB59B File Offset: 0x000C979B
		public void SetGameInstanceId(uint val)
		{
			this.GameInstanceId = val;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000CB5A4 File Offset: 0x000C97A4
		// (set) Token: 0x06003FA8 RID: 16296 RVA: 0x000CB5AC File Offset: 0x000C97AC
		public ulong MatchmakerGuid
		{
			get
			{
				return this._MatchmakerGuid;
			}
			set
			{
				this._MatchmakerGuid = value;
				this.HasMatchmakerGuid = true;
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000CB5BC File Offset: 0x000C97BC
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x000CB5C5 File Offset: 0x000C97C5
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x000CB5CD File Offset: 0x000C97CD
		public bool SkipQueue
		{
			get
			{
				return this._SkipQueue;
			}
			set
			{
				this._SkipQueue = value;
				this.HasSkipQueue = true;
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000CB5DD File Offset: 0x000C97DD
		public void SetSkipQueue(bool val)
		{
			this.SkipQueue = val;
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x000CB5E6 File Offset: 0x000C97E6
		// (set) Token: 0x06003FAE RID: 16302 RVA: 0x000CB5EE File Offset: 0x000C97EE
		public bool SkipClientNotifications
		{
			get
			{
				return this._SkipClientNotifications;
			}
			set
			{
				this._SkipClientNotifications = value;
				this.HasSkipClientNotifications = true;
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000CB5FE File Offset: 0x000C97FE
		public void SetSkipClientNotifications(bool val)
		{
			this.SkipClientNotifications = val;
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000CB608 File Offset: 0x000C9808
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasEventInfo)
			{
				num ^= this.EventInfo.GetHashCode();
			}
			if (this.HasCreationProperties)
			{
				num ^= this.CreationProperties.GetHashCode();
			}
			if (this.HasGameInstanceId)
			{
				num ^= this.GameInstanceId.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			if (this.HasSkipQueue)
			{
				num ^= this.SkipQueue.GetHashCode();
			}
			if (this.HasSkipClientNotifications)
			{
				num ^= this.SkipClientNotifications.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x000CB6CC File Offset: 0x000C98CC
		public override bool Equals(object obj)
		{
			CreateGameEventRequest createGameEventRequest = obj as CreateGameEventRequest;
			return createGameEventRequest != null && this.HasMatchmakerId == createGameEventRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(createGameEventRequest.MatchmakerId)) && this.HasEventInfo == createGameEventRequest.HasEventInfo && (!this.HasEventInfo || this.EventInfo.Equals(createGameEventRequest.EventInfo)) && this.HasCreationProperties == createGameEventRequest.HasCreationProperties && (!this.HasCreationProperties || this.CreationProperties.Equals(createGameEventRequest.CreationProperties)) && this.HasGameInstanceId == createGameEventRequest.HasGameInstanceId && (!this.HasGameInstanceId || this.GameInstanceId.Equals(createGameEventRequest.GameInstanceId)) && this.HasMatchmakerGuid == createGameEventRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(createGameEventRequest.MatchmakerGuid)) && this.HasSkipQueue == createGameEventRequest.HasSkipQueue && (!this.HasSkipQueue || this.SkipQueue.Equals(createGameEventRequest.SkipQueue)) && this.HasSkipClientNotifications == createGameEventRequest.HasSkipClientNotifications && (!this.HasSkipClientNotifications || this.SkipClientNotifications.Equals(createGameEventRequest.SkipClientNotifications));
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x000CB822 File Offset: 0x000C9A22
		public static CreateGameEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameEventRequest>(bs, 0, -1);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000CB82C File Offset: 0x000C9A2C
		public void Deserialize(Stream stream)
		{
			CreateGameEventRequest.Deserialize(stream, this);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000CB836 File Offset: 0x000C9A36
		public static CreateGameEventRequest Deserialize(Stream stream, CreateGameEventRequest instance)
		{
			return CreateGameEventRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x000CB844 File Offset: 0x000C9A44
		public static CreateGameEventRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameEventRequest createGameEventRequest = new CreateGameEventRequest();
			CreateGameEventRequest.DeserializeLengthDelimited(stream, createGameEventRequest);
			return createGameEventRequest;
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000CB860 File Offset: 0x000C9A60
		public static CreateGameEventRequest DeserializeLengthDelimited(Stream stream, CreateGameEventRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameEventRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000CB888 File Offset: 0x000C9A88
		public static CreateGameEventRequest Deserialize(Stream stream, CreateGameEventRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 26)
					{
						if (num == 13)
						{
							instance.MatchmakerId = binaryReader.ReadUInt32();
							continue;
						}
						if (num != 18)
						{
							if (num == 26)
							{
								if (instance.CreationProperties == null)
								{
									instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
									continue;
								}
								GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
								continue;
							}
						}
						else
						{
							if (instance.EventInfo == null)
							{
								instance.EventInfo = MatchmakingEventInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							MatchmakingEventInfo.DeserializeLengthDelimited(stream, instance.EventInfo);
							continue;
						}
					}
					else if (num <= 41)
					{
						if (num == 37)
						{
							instance.GameInstanceId = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 41)
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.SkipQueue = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.SkipClientNotifications = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000CB9F8 File Offset: 0x000C9BF8
		public void Serialize(Stream stream)
		{
			CreateGameEventRequest.Serialize(stream, this);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x000CBA04 File Offset: 0x000C9C04
		public static void Serialize(Stream stream, CreateGameEventRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasEventInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasSkipQueue)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.SkipQueue);
			}
			if (instance.HasSkipClientNotifications)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.SkipClientNotifications);
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000CBB00 File Offset: 0x000C9D00
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasEventInfo)
			{
				num += 1U;
				uint serializedSize = this.EventInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCreationProperties)
			{
				num += 1U;
				uint serializedSize2 = this.CreationProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameInstanceId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasSkipQueue)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSkipClientNotifications)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001643 RID: 5699
		public bool HasMatchmakerId;

		// Token: 0x04001644 RID: 5700
		private uint _MatchmakerId;

		// Token: 0x04001645 RID: 5701
		public bool HasEventInfo;

		// Token: 0x04001646 RID: 5702
		private MatchmakingEventInfo _EventInfo;

		// Token: 0x04001647 RID: 5703
		public bool HasCreationProperties;

		// Token: 0x04001648 RID: 5704
		private GameCreationProperties _CreationProperties;

		// Token: 0x04001649 RID: 5705
		public bool HasGameInstanceId;

		// Token: 0x0400164A RID: 5706
		private uint _GameInstanceId;

		// Token: 0x0400164B RID: 5707
		public bool HasMatchmakerGuid;

		// Token: 0x0400164C RID: 5708
		private ulong _MatchmakerGuid;

		// Token: 0x0400164D RID: 5709
		public bool HasSkipQueue;

		// Token: 0x0400164E RID: 5710
		private bool _SkipQueue;

		// Token: 0x0400164F RID: 5711
		public bool HasSkipClientNotifications;

		// Token: 0x04001650 RID: 5712
		private bool _SkipClientNotifications;
	}
}
