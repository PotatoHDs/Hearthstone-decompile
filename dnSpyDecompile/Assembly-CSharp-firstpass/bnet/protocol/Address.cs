using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002AF RID: 687
	public class Address : IProtoBuf
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x0008D9C2 File Offset: 0x0008BBC2
		// (set) Token: 0x060027F7 RID: 10231 RVA: 0x0008D9CA File Offset: 0x0008BBCA
		public string Address_ { get; set; }

		// Token: 0x060027F8 RID: 10232 RVA: 0x0008D9D3 File Offset: 0x0008BBD3
		public void SetAddress_(string val)
		{
			this.Address_ = val;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x0008D9DC File Offset: 0x0008BBDC
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x0008D9E4 File Offset: 0x0008BBE4
		public uint Port
		{
			get
			{
				return this._Port;
			}
			set
			{
				this._Port = value;
				this.HasPort = true;
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0008D9F4 File Offset: 0x0008BBF4
		public void SetPort(uint val)
		{
			this.Port = val;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0008DA00 File Offset: 0x0008BC00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Address_.GetHashCode();
			if (this.HasPort)
			{
				num ^= this.Port.GetHashCode();
			}
			return num;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0008DA44 File Offset: 0x0008BC44
		public override bool Equals(object obj)
		{
			Address address = obj as Address;
			return address != null && this.Address_.Equals(address.Address_) && this.HasPort == address.HasPort && (!this.HasPort || this.Port.Equals(address.Port));
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0008DAA1 File Offset: 0x0008BCA1
		public static Address ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Address>(bs, 0, -1);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0008DAAB File Offset: 0x0008BCAB
		public void Deserialize(Stream stream)
		{
			Address.Deserialize(stream, this);
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0008DAB5 File Offset: 0x0008BCB5
		public static Address Deserialize(Stream stream, Address instance)
		{
			return Address.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0008DAC0 File Offset: 0x0008BCC0
		public static Address DeserializeLengthDelimited(Stream stream)
		{
			Address address = new Address();
			Address.DeserializeLengthDelimited(stream, address);
			return address;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0008DADC File Offset: 0x0008BCDC
		public static Address DeserializeLengthDelimited(Stream stream, Address instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Address.Deserialize(stream, instance, num);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0008DB04 File Offset: 0x0008BD04
		public static Address Deserialize(Stream stream, Address instance, long limit)
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
					if (num != 16)
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
						instance.Port = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Address_ = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0008DB9C File Offset: 0x0008BD9C
		public void Serialize(Stream stream)
		{
			Address.Serialize(stream, this);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0008DBA8 File Offset: 0x0008BDA8
		public static void Serialize(Stream stream, Address instance)
		{
			if (instance.Address_ == null)
			{
				throw new ArgumentNullException("Address_", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address_));
			if (instance.HasPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Port);
			}
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0008DC08 File Offset: 0x0008BE08
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Address_);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasPort)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Port);
			}
			return num + 1U;
		}

		// Token: 0x04001152 RID: 4434
		public bool HasPort;

		// Token: 0x04001153 RID: 4435
		private uint _Port;
	}
}
