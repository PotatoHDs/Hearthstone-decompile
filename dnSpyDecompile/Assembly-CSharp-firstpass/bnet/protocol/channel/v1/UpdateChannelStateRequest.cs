using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C8 RID: 1224
	public class UpdateChannelStateRequest : IProtoBuf
	{
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060055B5 RID: 21941 RVA: 0x00106F84 File Offset: 0x00105184
		// (set) Token: 0x060055B6 RID: 21942 RVA: 0x00106F8C File Offset: 0x0010518C
		public EntityId AgentId
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

		// Token: 0x060055B7 RID: 21943 RVA: 0x00106F9F File Offset: 0x0010519F
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00106FA8 File Offset: 0x001051A8
		// (set) Token: 0x060055B9 RID: 21945 RVA: 0x00106FB0 File Offset: 0x001051B0
		public ChannelState StateChange { get; set; }

		// Token: 0x060055BA RID: 21946 RVA: 0x00106FB9 File Offset: 0x001051B9
		public void SetStateChange(ChannelState val)
		{
			this.StateChange = val;
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00106FC4 File Offset: 0x001051C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.StateChange.GetHashCode();
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00107004 File Offset: 0x00105204
		public override bool Equals(object obj)
		{
			UpdateChannelStateRequest updateChannelStateRequest = obj as UpdateChannelStateRequest;
			return updateChannelStateRequest != null && this.HasAgentId == updateChannelStateRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(updateChannelStateRequest.AgentId)) && this.StateChange.Equals(updateChannelStateRequest.StateChange);
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060055BD RID: 21949 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x0010705E File Offset: 0x0010525E
		public static UpdateChannelStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateRequest>(bs, 0, -1);
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x00107068 File Offset: 0x00105268
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateRequest.Deserialize(stream, this);
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00107072 File Offset: 0x00105272
		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance)
		{
			return UpdateChannelStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00107080 File Offset: 0x00105280
		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateRequest updateChannelStateRequest = new UpdateChannelStateRequest();
			UpdateChannelStateRequest.DeserializeLengthDelimited(stream, updateChannelStateRequest);
			return updateChannelStateRequest;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0010709C File Offset: 0x0010529C
		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream, UpdateChannelStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001070C4 File Offset: 0x001052C4
		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance, long limit)
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
					else if (instance.StateChange == null)
					{
						instance.StateChange = ChannelState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelState.DeserializeLengthDelimited(stream, instance.StateChange);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00107196 File Offset: 0x00105396
		public void Serialize(Stream stream)
		{
			UpdateChannelStateRequest.Serialize(stream, this);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x001071A0 File Offset: 0x001053A0
		public static void Serialize(Stream stream, UpdateChannelStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange == null)
			{
				throw new ArgumentNullException("StateChange", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.StateChange.GetSerializedSize());
			ChannelState.Serialize(stream, instance.StateChange);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00107218 File Offset: 0x00105418
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.StateChange.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1U;
		}

		// Token: 0x04001B07 RID: 6919
		public bool HasAgentId;

		// Token: 0x04001B08 RID: 6920
		private EntityId _AgentId;
	}
}
