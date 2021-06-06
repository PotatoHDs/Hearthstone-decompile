using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BF RID: 959
	public class UnregisterEventRequest : IProtoBuf
	{
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000C8E0F File Offset: 0x000C700F
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x000C8E17 File Offset: 0x000C7017
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

		// Token: 0x06003E97 RID: 16023 RVA: 0x000C8E27 File Offset: 0x000C7027
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x000C8E30 File Offset: 0x000C7030
		// (set) Token: 0x06003E99 RID: 16025 RVA: 0x000C8E38 File Offset: 0x000C7038
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

		// Token: 0x06003E9A RID: 16026 RVA: 0x000C8E48 File Offset: 0x000C7048
		public void SetGameInstanceId(uint val)
		{
			this.GameInstanceId = val;
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000C8E51 File Offset: 0x000C7051
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000C8E59 File Offset: 0x000C7059
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

		// Token: 0x06003E9D RID: 16029 RVA: 0x000C8E69 File Offset: 0x000C7069
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000C8E74 File Offset: 0x000C7074
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasGameInstanceId)
			{
				num ^= this.GameInstanceId.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000C8EDC File Offset: 0x000C70DC
		public override bool Equals(object obj)
		{
			UnregisterEventRequest unregisterEventRequest = obj as UnregisterEventRequest;
			return unregisterEventRequest != null && this.HasMatchmakerId == unregisterEventRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(unregisterEventRequest.MatchmakerId)) && this.HasGameInstanceId == unregisterEventRequest.HasGameInstanceId && (!this.HasGameInstanceId || this.GameInstanceId.Equals(unregisterEventRequest.GameInstanceId)) && this.HasMatchmakerGuid == unregisterEventRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(unregisterEventRequest.MatchmakerGuid));
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000C8F80 File Offset: 0x000C7180
		public static UnregisterEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterEventRequest>(bs, 0, -1);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000C8F8A File Offset: 0x000C718A
		public void Deserialize(Stream stream)
		{
			UnregisterEventRequest.Deserialize(stream, this);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x000C8F94 File Offset: 0x000C7194
		public static UnregisterEventRequest Deserialize(Stream stream, UnregisterEventRequest instance)
		{
			return UnregisterEventRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x000C8FA0 File Offset: 0x000C71A0
		public static UnregisterEventRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterEventRequest unregisterEventRequest = new UnregisterEventRequest();
			UnregisterEventRequest.DeserializeLengthDelimited(stream, unregisterEventRequest);
			return unregisterEventRequest;
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000C8FBC File Offset: 0x000C71BC
		public static UnregisterEventRequest DeserializeLengthDelimited(Stream stream, UnregisterEventRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterEventRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x000C8FE4 File Offset: 0x000C71E4
		public static UnregisterEventRequest Deserialize(Stream stream, UnregisterEventRequest instance, long limit)
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
				else if (num != 13)
				{
					if (num != 21)
					{
						if (num != 25)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
						}
					}
					else
					{
						instance.GameInstanceId = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.MatchmakerId = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x000C9099 File Offset: 0x000C7299
		public void Serialize(Stream stream)
		{
			UnregisterEventRequest.Serialize(stream, this);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x000C90A4 File Offset: 0x000C72A4
		public static void Serialize(Stream stream, UnregisterEventRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x000C910C File Offset: 0x000C730C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
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
			return num;
		}

		// Token: 0x04001610 RID: 5648
		public bool HasMatchmakerId;

		// Token: 0x04001611 RID: 5649
		private uint _MatchmakerId;

		// Token: 0x04001612 RID: 5650
		public bool HasGameInstanceId;

		// Token: 0x04001613 RID: 5651
		private uint _GameInstanceId;

		// Token: 0x04001614 RID: 5652
		public bool HasMatchmakerGuid;

		// Token: 0x04001615 RID: 5653
		private ulong _MatchmakerGuid;
	}
}
