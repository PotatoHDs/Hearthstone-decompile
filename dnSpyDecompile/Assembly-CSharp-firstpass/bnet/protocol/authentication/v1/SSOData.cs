using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FD RID: 1277
	public class SSOData : IProtoBuf
	{
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x001149C1 File Offset: 0x00112BC1
		// (set) Token: 0x06005AB8 RID: 23224 RVA: 0x001149C9 File Offset: 0x00112BC9
		public string AccountName
		{
			get
			{
				return this._AccountName;
			}
			set
			{
				this._AccountName = value;
				this.HasAccountName = (value != null);
			}
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x001149DC File Offset: 0x00112BDC
		public void SetAccountName(string val)
		{
			this.AccountName = val;
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06005ABA RID: 23226 RVA: 0x001149E5 File Offset: 0x00112BE5
		// (set) Token: 0x06005ABB RID: 23227 RVA: 0x001149ED File Offset: 0x00112BED
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

		// Token: 0x06005ABC RID: 23228 RVA: 0x001149FD File Offset: 0x00112BFD
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x00114A06 File Offset: 0x00112C06
		// (set) Token: 0x06005ABE RID: 23230 RVA: 0x00114A0E File Offset: 0x00112C0E
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x00114A1E File Offset: 0x00112C1E
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x00114A27 File Offset: 0x00112C27
		// (set) Token: 0x06005AC1 RID: 23233 RVA: 0x00114A2F File Offset: 0x00112C2F
		public ulong TimeCreated
		{
			get
			{
				return this._TimeCreated;
			}
			set
			{
				this._TimeCreated = value;
				this.HasTimeCreated = true;
			}
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x00114A3F File Offset: 0x00112C3F
		public void SetTimeCreated(ulong val)
		{
			this.TimeCreated = val;
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x00114A48 File Offset: 0x00112C48
		// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x00114A50 File Offset: 0x00112C50
		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				this._IpAddress = value;
				this.HasIpAddress = (value != null);
			}
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x00114A63 File Offset: 0x00112C63
		public void SetIpAddress(string val)
		{
			this.IpAddress = val;
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x00114A6C File Offset: 0x00112C6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountName)
			{
				num ^= this.AccountName.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasTimeCreated)
			{
				num ^= this.TimeCreated.GetHashCode();
			}
			if (this.HasIpAddress)
			{
				num ^= this.IpAddress.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x00114B00 File Offset: 0x00112D00
		public override bool Equals(object obj)
		{
			SSOData ssodata = obj as SSOData;
			return ssodata != null && this.HasAccountName == ssodata.HasAccountName && (!this.HasAccountName || this.AccountName.Equals(ssodata.AccountName)) && this.HasRegion == ssodata.HasRegion && (!this.HasRegion || this.Region.Equals(ssodata.Region)) && this.HasProgram == ssodata.HasProgram && (!this.HasProgram || this.Program.Equals(ssodata.Program)) && this.HasTimeCreated == ssodata.HasTimeCreated && (!this.HasTimeCreated || this.TimeCreated.Equals(ssodata.TimeCreated)) && this.HasIpAddress == ssodata.HasIpAddress && (!this.HasIpAddress || this.IpAddress.Equals(ssodata.IpAddress));
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x00114BFA File Offset: 0x00112DFA
		public static SSOData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SSOData>(bs, 0, -1);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x00114C04 File Offset: 0x00112E04
		public void Deserialize(Stream stream)
		{
			SSOData.Deserialize(stream, this);
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x00114C0E File Offset: 0x00112E0E
		public static SSOData Deserialize(Stream stream, SSOData instance)
		{
			return SSOData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x00114C1C File Offset: 0x00112E1C
		public static SSOData DeserializeLengthDelimited(Stream stream)
		{
			SSOData ssodata = new SSOData();
			SSOData.DeserializeLengthDelimited(stream, ssodata);
			return ssodata;
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x00114C38 File Offset: 0x00112E38
		public static SSOData DeserializeLengthDelimited(Stream stream, SSOData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SSOData.Deserialize(stream, instance, num);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x00114C60 File Offset: 0x00112E60
		public static SSOData Deserialize(Stream stream, SSOData instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else
				{
					if (num <= 21)
					{
						if (num == 10)
						{
							instance.AccountName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 21)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.TimeCreated = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.IpAddress = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 16U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.Varint)
					{
						instance.Region = ProtocolParser.ReadUInt32(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x00114D5B File Offset: 0x00112F5B
		public void Serialize(Stream stream)
		{
			SSOData.Serialize(stream, this);
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x00114D64 File Offset: 0x00112F64
		public static void Serialize(Stream stream, SSOData instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AccountName));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasTimeCreated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.TimeCreated);
			}
			if (instance.HasIpAddress)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IpAddress));
			}
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x00114E24 File Offset: 0x00113024
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AccountName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRegion)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasTimeCreated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.TimeCreated);
			}
			if (this.HasIpAddress)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.IpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04001C2D RID: 7213
		public bool HasAccountName;

		// Token: 0x04001C2E RID: 7214
		private string _AccountName;

		// Token: 0x04001C2F RID: 7215
		public bool HasRegion;

		// Token: 0x04001C30 RID: 7216
		private uint _Region;

		// Token: 0x04001C31 RID: 7217
		public bool HasProgram;

		// Token: 0x04001C32 RID: 7218
		private uint _Program;

		// Token: 0x04001C33 RID: 7219
		public bool HasTimeCreated;

		// Token: 0x04001C34 RID: 7220
		private ulong _TimeCreated;

		// Token: 0x04001C35 RID: 7221
		public bool HasIpAddress;

		// Token: 0x04001C36 RID: 7222
		private string _IpAddress;
	}
}
