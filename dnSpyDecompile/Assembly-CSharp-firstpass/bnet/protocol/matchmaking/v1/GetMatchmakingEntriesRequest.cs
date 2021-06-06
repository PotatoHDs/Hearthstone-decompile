using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C9 RID: 969
	public class GetMatchmakingEntriesRequest : IProtoBuf
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000CA9C7 File Offset: 0x000C8BC7
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x000CA9CF File Offset: 0x000C8BCF
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

		// Token: 0x06003F58 RID: 16216 RVA: 0x000CA9DF File Offset: 0x000C8BDF
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x000CA9E8 File Offset: 0x000C8BE8
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x000CA9F0 File Offset: 0x000C8BF0
		public uint NumRequests
		{
			get
			{
				return this._NumRequests;
			}
			set
			{
				this._NumRequests = value;
				this.HasNumRequests = true;
			}
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000CAA00 File Offset: 0x000C8C00
		public void SetNumRequests(uint val)
		{
			this.NumRequests = val;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000CAA09 File Offset: 0x000C8C09
		// (set) Token: 0x06003F5D RID: 16221 RVA: 0x000CAA11 File Offset: 0x000C8C11
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

		// Token: 0x06003F5E RID: 16222 RVA: 0x000CAA21 File Offset: 0x000C8C21
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000CAA2C File Offset: 0x000C8C2C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasNumRequests)
			{
				num ^= this.NumRequests.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000CAA94 File Offset: 0x000C8C94
		public override bool Equals(object obj)
		{
			GetMatchmakingEntriesRequest getMatchmakingEntriesRequest = obj as GetMatchmakingEntriesRequest;
			return getMatchmakingEntriesRequest != null && this.HasMatchmakerId == getMatchmakingEntriesRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(getMatchmakingEntriesRequest.MatchmakerId)) && this.HasNumRequests == getMatchmakingEntriesRequest.HasNumRequests && (!this.HasNumRequests || this.NumRequests.Equals(getMatchmakingEntriesRequest.NumRequests)) && this.HasMatchmakerGuid == getMatchmakingEntriesRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(getMatchmakingEntriesRequest.MatchmakerGuid));
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000CAB38 File Offset: 0x000C8D38
		public static GetMatchmakingEntriesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetMatchmakingEntriesRequest>(bs, 0, -1);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000CAB42 File Offset: 0x000C8D42
		public void Deserialize(Stream stream)
		{
			GetMatchmakingEntriesRequest.Deserialize(stream, this);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000CAB4C File Offset: 0x000C8D4C
		public static GetMatchmakingEntriesRequest Deserialize(Stream stream, GetMatchmakingEntriesRequest instance)
		{
			return GetMatchmakingEntriesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000CAB58 File Offset: 0x000C8D58
		public static GetMatchmakingEntriesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetMatchmakingEntriesRequest getMatchmakingEntriesRequest = new GetMatchmakingEntriesRequest();
			GetMatchmakingEntriesRequest.DeserializeLengthDelimited(stream, getMatchmakingEntriesRequest);
			return getMatchmakingEntriesRequest;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000CAB74 File Offset: 0x000C8D74
		public static GetMatchmakingEntriesRequest DeserializeLengthDelimited(Stream stream, GetMatchmakingEntriesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetMatchmakingEntriesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000CAB9C File Offset: 0x000C8D9C
		public static GetMatchmakingEntriesRequest Deserialize(Stream stream, GetMatchmakingEntriesRequest instance, long limit)
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
					if (num != 16)
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
						instance.NumRequests = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06003F68 RID: 16232 RVA: 0x000CAC51 File Offset: 0x000C8E51
		public void Serialize(Stream stream)
		{
			GetMatchmakingEntriesRequest.Serialize(stream, this);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000CAC5C File Offset: 0x000C8E5C
		public static void Serialize(Stream stream, GetMatchmakingEntriesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasNumRequests)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.NumRequests);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000CACC4 File Offset: 0x000C8EC4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasNumRequests)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.NumRequests);
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x0400163A RID: 5690
		public bool HasMatchmakerId;

		// Token: 0x0400163B RID: 5691
		private uint _MatchmakerId;

		// Token: 0x0400163C RID: 5692
		public bool HasNumRequests;

		// Token: 0x0400163D RID: 5693
		private uint _NumRequests;

		// Token: 0x0400163E RID: 5694
		public bool HasMatchmakerGuid;

		// Token: 0x0400163F RID: 5695
		private ulong _MatchmakerGuid;
	}
}
