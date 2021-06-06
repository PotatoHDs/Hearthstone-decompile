using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000453 RID: 1107
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x000EB0A7 File Offset: 0x000E92A7
		// (set) Token: 0x06004B88 RID: 19336 RVA: 0x000EB0AF File Offset: 0x000E92AF
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

		// Token: 0x06004B89 RID: 19337 RVA: 0x000EB0C2 File Offset: 0x000E92C2
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x000EB0CB File Offset: 0x000E92CB
		// (set) Token: 0x06004B8B RID: 19339 RVA: 0x000EB0D3 File Offset: 0x000E92D3
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x000EB0E6 File Offset: 0x000E92E6
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x000EB0F0 File Offset: 0x000E92F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x000EB138 File Offset: 0x000E9338
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId)) && this.HasChannelId == unsubscribeRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(unsubscribeRequest.ChannelId));
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x000EB1A8 File Offset: 0x000E93A8
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x000EB1B2 File Offset: 0x000E93B2
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x000EB1BC File Offset: 0x000E93BC
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x000EB1C8 File Offset: 0x000E93C8
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x000EB1E4 File Offset: 0x000E93E4
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x000EB20C File Offset: 0x000E940C
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x06004B96 RID: 19350 RVA: 0x000EB2DE File Offset: 0x000E94DE
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x000EB2E8 File Offset: 0x000E94E8
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x000EB350 File Offset: 0x000E9550
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040018B1 RID: 6321
		public bool HasAgentId;

		// Token: 0x040018B2 RID: 6322
		private GameAccountHandle _AgentId;

		// Token: 0x040018B3 RID: 6323
		public bool HasChannelId;

		// Token: 0x040018B4 RID: 6324
		private ChannelId _ChannelId;
	}
}
