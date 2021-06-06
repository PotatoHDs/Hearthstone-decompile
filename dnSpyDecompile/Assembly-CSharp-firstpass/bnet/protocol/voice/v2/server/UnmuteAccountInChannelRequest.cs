using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002CA RID: 714
	public class UnmuteAccountInChannelRequest : IProtoBuf
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000923C7 File Offset: 0x000905C7
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000923CF File Offset: 0x000905CF
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

		// Token: 0x060029C8 RID: 10696 RVA: 0x000923E2 File Offset: 0x000905E2
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000923EB File Offset: 0x000905EB
		// (set) Token: 0x060029CA RID: 10698 RVA: 0x000923F3 File Offset: 0x000905F3
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

		// Token: 0x060029CB RID: 10699 RVA: 0x00092406 File Offset: 0x00090606
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00092410 File Offset: 0x00090610
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
			return num;
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00092458 File Offset: 0x00090658
		public override bool Equals(object obj)
		{
			UnmuteAccountInChannelRequest unmuteAccountInChannelRequest = obj as UnmuteAccountInChannelRequest;
			return unmuteAccountInChannelRequest != null && this.HasChannelUri == unmuteAccountInChannelRequest.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(unmuteAccountInChannelRequest.ChannelUri)) && this.HasAccountId == unmuteAccountInChannelRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(unmuteAccountInChannelRequest.AccountId));
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000924C8 File Offset: 0x000906C8
		public static UnmuteAccountInChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnmuteAccountInChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000924D2 File Offset: 0x000906D2
		public void Deserialize(Stream stream)
		{
			UnmuteAccountInChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000924DC File Offset: 0x000906DC
		public static UnmuteAccountInChannelRequest Deserialize(Stream stream, UnmuteAccountInChannelRequest instance)
		{
			return UnmuteAccountInChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000924E8 File Offset: 0x000906E8
		public static UnmuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			UnmuteAccountInChannelRequest unmuteAccountInChannelRequest = new UnmuteAccountInChannelRequest();
			UnmuteAccountInChannelRequest.DeserializeLengthDelimited(stream, unmuteAccountInChannelRequest);
			return unmuteAccountInChannelRequest;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00092504 File Offset: 0x00090704
		public static UnmuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream, UnmuteAccountInChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnmuteAccountInChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0009252C File Offset: 0x0009072C
		public static UnmuteAccountInChannelRequest Deserialize(Stream stream, UnmuteAccountInChannelRequest instance, long limit)
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

		// Token: 0x060029D5 RID: 10709 RVA: 0x000925DE File Offset: 0x000907DE
		public void Serialize(Stream stream)
		{
			UnmuteAccountInChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000925E8 File Offset: 0x000907E8
		public static void Serialize(Stream stream, UnmuteAccountInChannelRequest instance)
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
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00092648 File Offset: 0x00090848
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
			return num;
		}

		// Token: 0x040011D7 RID: 4567
		public bool HasChannelUri;

		// Token: 0x040011D8 RID: 4568
		private string _ChannelUri;

		// Token: 0x040011D9 RID: 4569
		public bool HasAccountId;

		// Token: 0x040011DA RID: 4570
		private AccountId _AccountId;
	}
}
