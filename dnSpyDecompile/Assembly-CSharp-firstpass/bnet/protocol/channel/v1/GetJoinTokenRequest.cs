using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DD RID: 1245
	public class GetJoinTokenRequest : IProtoBuf
	{
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x0010D643 File Offset: 0x0010B843
		// (set) Token: 0x060057DC RID: 22492 RVA: 0x0010D64B File Offset: 0x0010B84B
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

		// Token: 0x060057DD RID: 22493 RVA: 0x0010D65E File Offset: 0x0010B85E
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060057DE RID: 22494 RVA: 0x0010D667 File Offset: 0x0010B867
		// (set) Token: 0x060057DF RID: 22495 RVA: 0x0010D66F File Offset: 0x0010B86F
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x0010D682 File Offset: 0x0010B882
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x0010D68C File Offset: 0x0010B88C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0010D6D4 File Offset: 0x0010B8D4
		public override bool Equals(object obj)
		{
			GetJoinTokenRequest getJoinTokenRequest = obj as GetJoinTokenRequest;
			return getJoinTokenRequest != null && this.HasChannelId == getJoinTokenRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(getJoinTokenRequest.ChannelId)) && this.HasMemberId == getJoinTokenRequest.HasMemberId && (!this.HasMemberId || this.MemberId.Equals(getJoinTokenRequest.MemberId));
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0010D744 File Offset: 0x0010B944
		public static GetJoinTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinTokenRequest>(bs, 0, -1);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x0010D74E File Offset: 0x0010B94E
		public void Deserialize(Stream stream)
		{
			GetJoinTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0010D758 File Offset: 0x0010B958
		public static GetJoinTokenRequest Deserialize(Stream stream, GetJoinTokenRequest instance)
		{
			return GetJoinTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0010D764 File Offset: 0x0010B964
		public static GetJoinTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetJoinTokenRequest getJoinTokenRequest = new GetJoinTokenRequest();
			GetJoinTokenRequest.DeserializeLengthDelimited(stream, getJoinTokenRequest);
			return getJoinTokenRequest;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x0010D780 File Offset: 0x0010B980
		public static GetJoinTokenRequest DeserializeLengthDelimited(Stream stream, GetJoinTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetJoinTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x0010D7A8 File Offset: 0x0010B9A8
		public static GetJoinTokenRequest Deserialize(Stream stream, GetJoinTokenRequest instance, long limit)
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
				else if (num != 18)
				{
					if (num != 34)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
					}
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x0010D87A File Offset: 0x0010BA7A
		public void Serialize(Stream stream)
		{
			GetJoinTokenRequest.Serialize(stream, this);
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x0010D884 File Offset: 0x0010BA84
		public static void Serialize(Stream stream, GetJoinTokenRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x0010D8EC File Offset: 0x0010BAEC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize2 = this.MemberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001B8F RID: 7055
		public bool HasChannelId;

		// Token: 0x04001B90 RID: 7056
		private ChannelId _ChannelId;

		// Token: 0x04001B91 RID: 7057
		public bool HasMemberId;

		// Token: 0x04001B92 RID: 7058
		private GameAccountHandle _MemberId;
	}
}
