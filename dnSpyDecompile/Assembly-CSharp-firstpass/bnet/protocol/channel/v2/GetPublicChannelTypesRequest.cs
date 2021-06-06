using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044E RID: 1102
	public class GetPublicChannelTypesRequest : IProtoBuf
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x000EA11F File Offset: 0x000E831F
		// (set) Token: 0x06004B25 RID: 19237 RVA: 0x000EA127 File Offset: 0x000E8327
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x000EA13A File Offset: 0x000E833A
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x000EA143 File Offset: 0x000E8343
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x000EA14B File Offset: 0x000E834B
		public GetPublicChannelTypesOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x000EA15E File Offset: 0x000E835E
		public void SetOptions(GetPublicChannelTypesOptions val)
		{
			this.Options = val;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x000EA167 File Offset: 0x000E8367
		// (set) Token: 0x06004B2B RID: 19243 RVA: 0x000EA16F File Offset: 0x000E836F
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x000EA17F File Offset: 0x000E837F
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x000EA188 File Offset: 0x000E8388
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x000EA1E8 File Offset: 0x000E83E8
		public override bool Equals(object obj)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = obj as GetPublicChannelTypesRequest;
			return getPublicChannelTypesRequest != null && this.HasAgentId == getPublicChannelTypesRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getPublicChannelTypesRequest.AgentId)) && this.HasOptions == getPublicChannelTypesRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getPublicChannelTypesRequest.Options)) && this.HasContinuation == getPublicChannelTypesRequest.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getPublicChannelTypesRequest.Continuation));
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x000EA286 File Offset: 0x000E8486
		public static GetPublicChannelTypesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesRequest>(bs, 0, -1);
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x000EA290 File Offset: 0x000E8490
		public void Deserialize(Stream stream)
		{
			GetPublicChannelTypesRequest.Deserialize(stream, this);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x000EA29A File Offset: 0x000E849A
		public static GetPublicChannelTypesRequest Deserialize(Stream stream, GetPublicChannelTypesRequest instance)
		{
			return GetPublicChannelTypesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x000EA2A8 File Offset: 0x000E84A8
		public static GetPublicChannelTypesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = new GetPublicChannelTypesRequest();
			GetPublicChannelTypesRequest.DeserializeLengthDelimited(stream, getPublicChannelTypesRequest);
			return getPublicChannelTypesRequest;
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x000EA2C4 File Offset: 0x000E84C4
		public static GetPublicChannelTypesRequest DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPublicChannelTypesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x000EA2EC File Offset: 0x000E84EC
		public static GetPublicChannelTypesRequest Deserialize(Stream stream, GetPublicChannelTypesRequest instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
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
							instance.Continuation = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.Options == null)
					{
						instance.Options = GetPublicChannelTypesOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GetPublicChannelTypesOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x000EA3D4 File Offset: 0x000E85D4
		public void Serialize(Stream stream)
		{
			GetPublicChannelTypesRequest.Serialize(stream, this);
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x000EA3E0 File Offset: 0x000E85E0
		public static void Serialize(Stream stream, GetPublicChannelTypesRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetPublicChannelTypesOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x000EA464 File Offset: 0x000E8664
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x0400189E RID: 6302
		public bool HasAgentId;

		// Token: 0x0400189F RID: 6303
		private GameAccountHandle _AgentId;

		// Token: 0x040018A0 RID: 6304
		public bool HasOptions;

		// Token: 0x040018A1 RID: 6305
		private GetPublicChannelTypesOptions _Options;

		// Token: 0x040018A2 RID: 6306
		public bool HasContinuation;

		// Token: 0x040018A3 RID: 6307
		private ulong _Continuation;
	}
}
