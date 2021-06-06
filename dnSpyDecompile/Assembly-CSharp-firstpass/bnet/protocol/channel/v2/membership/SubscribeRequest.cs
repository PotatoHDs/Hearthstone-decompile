using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A3 RID: 1187
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x000FFD3A File Offset: 0x000FDF3A
		// (set) Token: 0x060052C5 RID: 21189 RVA: 0x000FFD42 File Offset: 0x000FDF42
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

		// Token: 0x060052C6 RID: 21190 RVA: 0x000FFD55 File Offset: 0x000FDF55
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x000FFD60 File Offset: 0x000FDF60
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x000FFD90 File Offset: 0x000FDF90
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId));
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x000FFDD5 File Offset: 0x000FDFD5
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x000FFDDF File Offset: 0x000FDFDF
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x000FFDE9 File Offset: 0x000FDFE9
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x000FFDF4 File Offset: 0x000FDFF4
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x000FFE10 File Offset: 0x000FE010
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x000FFE38 File Offset: 0x000FE038
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.AgentId == null)
					{
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
				}
				else
				{
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

		// Token: 0x060052D0 RID: 21200 RVA: 0x000FFED2 File Offset: 0x000FE0D2
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x000FFEDB File Offset: 0x000FE0DB
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x000FFF0C File Offset: 0x000FE10C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001A78 RID: 6776
		public bool HasAgentId;

		// Token: 0x04001A79 RID: 6777
		private GameAccountHandle _AgentId;
	}
}
