using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050B RID: 1291
	public class IsIgrAddressRequest : IProtoBuf
	{
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06005BEA RID: 23530 RVA: 0x00117AF7 File Offset: 0x00115CF7
		// (set) Token: 0x06005BEB RID: 23531 RVA: 0x00117AFF File Offset: 0x00115CFF
		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00117B12 File Offset: 0x00115D12
		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06005BED RID: 23533 RVA: 0x00117B1B File Offset: 0x00115D1B
		// (set) Token: 0x06005BEE RID: 23534 RVA: 0x00117B23 File Offset: 0x00115D23
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x00117B33 File Offset: 0x00115D33
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x00117B3C File Offset: 0x00115D3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x00117B88 File Offset: 0x00115D88
		public override bool Equals(object obj)
		{
			IsIgrAddressRequest isIgrAddressRequest = obj as IsIgrAddressRequest;
			return isIgrAddressRequest != null && this.HasClientAddress == isIgrAddressRequest.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(isIgrAddressRequest.ClientAddress)) && this.HasRegion == isIgrAddressRequest.HasRegion && (!this.HasRegion || this.Region.Equals(isIgrAddressRequest.Region));
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06005BF2 RID: 23538 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x00117BFB File Offset: 0x00115DFB
		public static IsIgrAddressRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsIgrAddressRequest>(bs, 0, -1);
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00117C05 File Offset: 0x00115E05
		public void Deserialize(Stream stream)
		{
			IsIgrAddressRequest.Deserialize(stream, this);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x00117C0F File Offset: 0x00115E0F
		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance)
		{
			return IsIgrAddressRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x00117C1C File Offset: 0x00115E1C
		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream)
		{
			IsIgrAddressRequest isIgrAddressRequest = new IsIgrAddressRequest();
			IsIgrAddressRequest.DeserializeLengthDelimited(stream, isIgrAddressRequest);
			return isIgrAddressRequest;
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x00117C38 File Offset: 0x00115E38
		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream, IsIgrAddressRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IsIgrAddressRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00117C60 File Offset: 0x00115E60
		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance, long limit)
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
						instance.Region = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.ClientAddress = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x00117CF8 File Offset: 0x00115EF8
		public void Serialize(Stream stream)
		{
			IsIgrAddressRequest.Serialize(stream, this);
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x00117D04 File Offset: 0x00115F04
		public static void Serialize(Stream stream, IsIgrAddressRequest instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x00117D54 File Offset: 0x00115F54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			return num;
		}

		// Token: 0x04001C79 RID: 7289
		public bool HasClientAddress;

		// Token: 0x04001C7A RID: 7290
		private string _ClientAddress;

		// Token: 0x04001C7B RID: 7291
		public bool HasRegion;

		// Token: 0x04001C7C RID: 7292
		private uint _Region;
	}
}
