using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C5 RID: 709
	public class CreateChannelJoinTokenRequest : IProtoBuf
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x0009155C File Offset: 0x0008F75C
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x00091564 File Offset: 0x0008F764
		public string ChannelUri
		{
			get
			{
				return this._ChannelUri;
			}
			set
			{
				this._ChannelUri = value;
				this.HasChannelUri = (value != null);
			}
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x00091577 File Offset: 0x0008F777
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x00091580 File Offset: 0x0008F780
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x00091588 File Offset: 0x0008F788
		public AccountId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0009159B File Offset: 0x0008F79B
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000915A4 File Offset: 0x0008F7A4
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x000915AC File Offset: 0x0008F7AC
		public VoiceJoinType RequestedJoinType
		{
			get
			{
				return this._RequestedJoinType;
			}
			set
			{
				this._RequestedJoinType = value;
				this.HasRequestedJoinType = true;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000915BC File Offset: 0x0008F7BC
		public void SetRequestedJoinType(VoiceJoinType val)
		{
			this.RequestedJoinType = val;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000915C8 File Offset: 0x0008F7C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelUri)
			{
				num ^= this.ChannelUri.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasRequestedJoinType)
			{
				num ^= this.RequestedJoinType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00091630 File Offset: 0x0008F830
		public override bool Equals(object obj)
		{
			CreateChannelJoinTokenRequest createChannelJoinTokenRequest = obj as CreateChannelJoinTokenRequest;
			return createChannelJoinTokenRequest != null && this.HasChannelUri == createChannelJoinTokenRequest.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(createChannelJoinTokenRequest.ChannelUri)) && this.HasAccountId == createChannelJoinTokenRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(createChannelJoinTokenRequest.AccountId)) && this.HasRequestedJoinType == createChannelJoinTokenRequest.HasRequestedJoinType && (!this.HasRequestedJoinType || this.RequestedJoinType.Equals(createChannelJoinTokenRequest.RequestedJoinType));
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000916D9 File Offset: 0x0008F8D9
		public static CreateChannelJoinTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelJoinTokenRequest>(bs, 0, -1);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000916E3 File Offset: 0x0008F8E3
		public void Deserialize(Stream stream)
		{
			CreateChannelJoinTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000916ED File Offset: 0x0008F8ED
		public static CreateChannelJoinTokenRequest Deserialize(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			return CreateChannelJoinTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000916F8 File Offset: 0x0008F8F8
		public static CreateChannelJoinTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelJoinTokenRequest createChannelJoinTokenRequest = new CreateChannelJoinTokenRequest();
			CreateChannelJoinTokenRequest.DeserializeLengthDelimited(stream, createChannelJoinTokenRequest);
			return createChannelJoinTokenRequest;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00091714 File Offset: 0x0008F914
		public static CreateChannelJoinTokenRequest DeserializeLengthDelimited(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelJoinTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0009173C File Offset: 0x0008F93C
		public static CreateChannelJoinTokenRequest Deserialize(Stream stream, CreateChannelJoinTokenRequest instance, long limit)
		{
			instance.RequestedJoinType = VoiceJoinType.VOICE_JOIN_NORMAL;
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
							instance.RequestedJoinType = (VoiceJoinType)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
				}
				else
				{
					instance.ChannelUri = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00091812 File Offset: 0x0008FA12
		public void Serialize(Stream stream)
		{
			CreateChannelJoinTokenRequest.Serialize(stream, this);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0009181C File Offset: 0x0008FA1C
		public static void Serialize(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasRequestedJoinType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestedJoinType));
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x0009189C File Offset: 0x0008FA9C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelUri)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRequestedJoinType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestedJoinType));
			}
			return num;
		}

		// Token: 0x040011C6 RID: 4550
		public bool HasChannelUri;

		// Token: 0x040011C7 RID: 4551
		private string _ChannelUri;

		// Token: 0x040011C8 RID: 4552
		public bool HasAccountId;

		// Token: 0x040011C9 RID: 4553
		private AccountId _AccountId;

		// Token: 0x040011CA RID: 4554
		public bool HasRequestedJoinType;

		// Token: 0x040011CB RID: 4555
		private VoiceJoinType _RequestedJoinType;
	}
}
