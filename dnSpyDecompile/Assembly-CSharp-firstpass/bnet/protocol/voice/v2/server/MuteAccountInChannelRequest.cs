using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C9 RID: 713
	public class MuteAccountInChannelRequest : IProtoBuf
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000920EB File Offset: 0x000902EB
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x000920F3 File Offset: 0x000902F3
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

		// Token: 0x060029B5 RID: 10677 RVA: 0x00092106 File Offset: 0x00090306
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x0009210F File Offset: 0x0009030F
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x00092117 File Offset: 0x00090317
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

		// Token: 0x060029B8 RID: 10680 RVA: 0x0009212A File Offset: 0x0009032A
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00092134 File Offset: 0x00090334
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

		// Token: 0x060029BA RID: 10682 RVA: 0x0009217C File Offset: 0x0009037C
		public override bool Equals(object obj)
		{
			MuteAccountInChannelRequest muteAccountInChannelRequest = obj as MuteAccountInChannelRequest;
			return muteAccountInChannelRequest != null && this.HasChannelUri == muteAccountInChannelRequest.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(muteAccountInChannelRequest.ChannelUri)) && this.HasAccountId == muteAccountInChannelRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(muteAccountInChannelRequest.AccountId));
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000921EC File Offset: 0x000903EC
		public static MuteAccountInChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MuteAccountInChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000921F6 File Offset: 0x000903F6
		public void Deserialize(Stream stream)
		{
			MuteAccountInChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00092200 File Offset: 0x00090400
		public static MuteAccountInChannelRequest Deserialize(Stream stream, MuteAccountInChannelRequest instance)
		{
			return MuteAccountInChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0009220C File Offset: 0x0009040C
		public static MuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			MuteAccountInChannelRequest muteAccountInChannelRequest = new MuteAccountInChannelRequest();
			MuteAccountInChannelRequest.DeserializeLengthDelimited(stream, muteAccountInChannelRequest);
			return muteAccountInChannelRequest;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00092228 File Offset: 0x00090428
		public static MuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream, MuteAccountInChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MuteAccountInChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00092250 File Offset: 0x00090450
		public static MuteAccountInChannelRequest Deserialize(Stream stream, MuteAccountInChannelRequest instance, long limit)
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

		// Token: 0x060029C2 RID: 10690 RVA: 0x00092302 File Offset: 0x00090502
		public void Serialize(Stream stream)
		{
			MuteAccountInChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x0009230C File Offset: 0x0009050C
		public static void Serialize(Stream stream, MuteAccountInChannelRequest instance)
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

		// Token: 0x060029C4 RID: 10692 RVA: 0x0009236C File Offset: 0x0009056C
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

		// Token: 0x040011D3 RID: 4563
		public bool HasChannelUri;

		// Token: 0x040011D4 RID: 4564
		private string _ChannelUri;

		// Token: 0x040011D5 RID: 4565
		public bool HasAccountId;

		// Token: 0x040011D6 RID: 4566
		private AccountId _AccountId;
	}
}
