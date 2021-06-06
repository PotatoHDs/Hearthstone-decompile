using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FE RID: 1278
	public class GenerateTokenRequest : IProtoBuf
	{
		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x00114EC8 File Offset: 0x001130C8
		// (set) Token: 0x06005AD4 RID: 23252 RVA: 0x00114ED0 File Offset: 0x001130D0
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

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00114EE3 File Offset: 0x001130E3
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x00114EEC File Offset: 0x001130EC
		// (set) Token: 0x06005AD7 RID: 23255 RVA: 0x00114EF4 File Offset: 0x001130F4
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

		// Token: 0x06005AD8 RID: 23256 RVA: 0x00114F04 File Offset: 0x00113104
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06005AD9 RID: 23257 RVA: 0x00114F0D File Offset: 0x0011310D
		// (set) Token: 0x06005ADA RID: 23258 RVA: 0x00114F15 File Offset: 0x00113115
		public string PlatformId
		{
			get
			{
				return this._PlatformId;
			}
			set
			{
				this._PlatformId = value;
				this.HasPlatformId = (value != null);
			}
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x00114F28 File Offset: 0x00113128
		public void SetPlatformId(string val)
		{
			this.PlatformId = val;
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x00114F31 File Offset: 0x00113131
		// (set) Token: 0x06005ADD RID: 23261 RVA: 0x00114F39 File Offset: 0x00113139
		public string ClientIp
		{
			get
			{
				return this._ClientIp;
			}
			set
			{
				this._ClientIp = value;
				this.HasClientIp = (value != null);
			}
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x00114F4C File Offset: 0x0011314C
		public void SetClientIp(string val)
		{
			this.ClientIp = val;
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06005ADF RID: 23263 RVA: 0x00114F55 File Offset: 0x00113155
		// (set) Token: 0x06005AE0 RID: 23264 RVA: 0x00114F5D File Offset: 0x0011315D
		public bool SingleUse
		{
			get
			{
				return this._SingleUse;
			}
			set
			{
				this._SingleUse = value;
				this.HasSingleUse = true;
			}
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x00114F6D File Offset: 0x0011316D
		public void SetSingleUse(bool val)
		{
			this.SingleUse = val;
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x00114F76 File Offset: 0x00113176
		// (set) Token: 0x06005AE3 RID: 23267 RVA: 0x00114F7E File Offset: 0x0011317E
		public bool GenerateTokenId
		{
			get
			{
				return this._GenerateTokenId;
			}
			set
			{
				this._GenerateTokenId = value;
				this.HasGenerateTokenId = true;
			}
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x00114F8E File Offset: 0x0011318E
		public void SetGenerateTokenId(bool val)
		{
			this.GenerateTokenId = val;
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x00114F98 File Offset: 0x00113198
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasPlatformId)
			{
				num ^= this.PlatformId.GetHashCode();
			}
			if (this.HasClientIp)
			{
				num ^= this.ClientIp.GetHashCode();
			}
			if (this.HasSingleUse)
			{
				num ^= this.SingleUse.GetHashCode();
			}
			if (this.HasGenerateTokenId)
			{
				num ^= this.GenerateTokenId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x00115040 File Offset: 0x00113240
		public override bool Equals(object obj)
		{
			GenerateTokenRequest generateTokenRequest = obj as GenerateTokenRequest;
			return generateTokenRequest != null && this.HasAccountId == generateTokenRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(generateTokenRequest.AccountId)) && this.HasProgram == generateTokenRequest.HasProgram && (!this.HasProgram || this.Program.Equals(generateTokenRequest.Program)) && this.HasPlatformId == generateTokenRequest.HasPlatformId && (!this.HasPlatformId || this.PlatformId.Equals(generateTokenRequest.PlatformId)) && this.HasClientIp == generateTokenRequest.HasClientIp && (!this.HasClientIp || this.ClientIp.Equals(generateTokenRequest.ClientIp)) && this.HasSingleUse == generateTokenRequest.HasSingleUse && (!this.HasSingleUse || this.SingleUse.Equals(generateTokenRequest.SingleUse)) && this.HasGenerateTokenId == generateTokenRequest.HasGenerateTokenId && (!this.HasGenerateTokenId || this.GenerateTokenId.Equals(generateTokenRequest.GenerateTokenId));
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00115165 File Offset: 0x00113365
		public static GenerateTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTokenRequest>(bs, 0, -1);
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x0011516F File Offset: 0x0011336F
		public void Deserialize(Stream stream)
		{
			GenerateTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x00115179 File Offset: 0x00113379
		public static GenerateTokenRequest Deserialize(Stream stream, GenerateTokenRequest instance)
		{
			return GenerateTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x00115184 File Offset: 0x00113384
		public static GenerateTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest();
			GenerateTokenRequest.DeserializeLengthDelimited(stream, generateTokenRequest);
			return generateTokenRequest;
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x001151A0 File Offset: 0x001133A0
		public static GenerateTokenRequest DeserializeLengthDelimited(Stream stream, GenerateTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x001151C8 File Offset: 0x001133C8
		public static GenerateTokenRequest Deserialize(Stream stream, GenerateTokenRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.SingleUse = true;
			instance.GenerateTokenId = false;
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 21)
							{
								instance.Program = binaryReader.ReadUInt32();
								continue;
							}
							if (num == 26)
							{
								instance.PlatformId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.ClientIp = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 48)
						{
							instance.SingleUse = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.GenerateTokenId = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
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

		// Token: 0x06005AEE RID: 23278 RVA: 0x00115300 File Offset: 0x00113500
		public void Serialize(Stream stream)
		{
			GenerateTokenRequest.Serialize(stream, this);
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x0011530C File Offset: 0x0011350C
		public static void Serialize(Stream stream, GenerateTokenRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasPlatformId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PlatformId));
			}
			if (instance.HasClientIp)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientIp));
			}
			if (instance.HasSingleUse)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.SingleUse);
			}
			if (instance.HasGenerateTokenId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.GenerateTokenId);
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x001153F0 File Offset: 0x001135F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasPlatformId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.PlatformId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasClientIp)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientIp);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasSingleUse)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGenerateTokenId)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001C37 RID: 7223
		public bool HasAccountId;

		// Token: 0x04001C38 RID: 7224
		private AccountId _AccountId;

		// Token: 0x04001C39 RID: 7225
		public bool HasProgram;

		// Token: 0x04001C3A RID: 7226
		private uint _Program;

		// Token: 0x04001C3B RID: 7227
		public bool HasPlatformId;

		// Token: 0x04001C3C RID: 7228
		private string _PlatformId;

		// Token: 0x04001C3D RID: 7229
		public bool HasClientIp;

		// Token: 0x04001C3E RID: 7230
		private string _ClientIp;

		// Token: 0x04001C3F RID: 7231
		public bool HasSingleUse;

		// Token: 0x04001C40 RID: 7232
		private bool _SingleUse;

		// Token: 0x04001C41 RID: 7233
		public bool HasGenerateTokenId;

		// Token: 0x04001C42 RID: 7234
		private bool _GenerateTokenId;
	}
}
