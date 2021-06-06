using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x02000502 RID: 1282
	public class GenerateTrustedWebCredentialsRequest : IProtoBuf
	{
		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06005B3D RID: 23357 RVA: 0x00116174 File Offset: 0x00114374
		// (set) Token: 0x06005B3E RID: 23358 RVA: 0x0011617C File Offset: 0x0011437C
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

		// Token: 0x06005B3F RID: 23359 RVA: 0x0011618F File Offset: 0x0011438F
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x00116198 File Offset: 0x00114398
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x001161A0 File Offset: 0x001143A0
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

		// Token: 0x06005B42 RID: 23362 RVA: 0x001161B0 File Offset: 0x001143B0
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x001161B9 File Offset: 0x001143B9
		// (set) Token: 0x06005B44 RID: 23364 RVA: 0x001161C1 File Offset: 0x001143C1
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

		// Token: 0x06005B45 RID: 23365 RVA: 0x001161D4 File Offset: 0x001143D4
		public void SetPlatformId(string val)
		{
			this.PlatformId = val;
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x001161DD File Offset: 0x001143DD
		// (set) Token: 0x06005B47 RID: 23367 RVA: 0x001161E5 File Offset: 0x001143E5
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

		// Token: 0x06005B48 RID: 23368 RVA: 0x001161F8 File Offset: 0x001143F8
		public void SetClientIp(string val)
		{
			this.ClientIp = val;
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x00116201 File Offset: 0x00114401
		// (set) Token: 0x06005B4A RID: 23370 RVA: 0x00116209 File Offset: 0x00114409
		public byte[] SessionKey
		{
			get
			{
				return this._SessionKey;
			}
			set
			{
				this._SessionKey = value;
				this.HasSessionKey = (value != null);
			}
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x0011621C File Offset: 0x0011441C
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x00116228 File Offset: 0x00114428
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
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x001162B4 File Offset: 0x001144B4
		public override bool Equals(object obj)
		{
			GenerateTrustedWebCredentialsRequest generateTrustedWebCredentialsRequest = obj as GenerateTrustedWebCredentialsRequest;
			return generateTrustedWebCredentialsRequest != null && this.HasAccountId == generateTrustedWebCredentialsRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(generateTrustedWebCredentialsRequest.AccountId)) && this.HasProgram == generateTrustedWebCredentialsRequest.HasProgram && (!this.HasProgram || this.Program.Equals(generateTrustedWebCredentialsRequest.Program)) && this.HasPlatformId == generateTrustedWebCredentialsRequest.HasPlatformId && (!this.HasPlatformId || this.PlatformId.Equals(generateTrustedWebCredentialsRequest.PlatformId)) && this.HasClientIp == generateTrustedWebCredentialsRequest.HasClientIp && (!this.HasClientIp || this.ClientIp.Equals(generateTrustedWebCredentialsRequest.ClientIp)) && this.HasSessionKey == generateTrustedWebCredentialsRequest.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(generateTrustedWebCredentialsRequest.SessionKey));
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x001163A8 File Offset: 0x001145A8
		public static GenerateTrustedWebCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTrustedWebCredentialsRequest>(bs, 0, -1);
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x001163B2 File Offset: 0x001145B2
		public void Deserialize(Stream stream)
		{
			GenerateTrustedWebCredentialsRequest.Deserialize(stream, this);
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x001163BC File Offset: 0x001145BC
		public static GenerateTrustedWebCredentialsRequest Deserialize(Stream stream, GenerateTrustedWebCredentialsRequest instance)
		{
			return GenerateTrustedWebCredentialsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x001163C8 File Offset: 0x001145C8
		public static GenerateTrustedWebCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateTrustedWebCredentialsRequest generateTrustedWebCredentialsRequest = new GenerateTrustedWebCredentialsRequest();
			GenerateTrustedWebCredentialsRequest.DeserializeLengthDelimited(stream, generateTrustedWebCredentialsRequest);
			return generateTrustedWebCredentialsRequest;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x001163E4 File Offset: 0x001145E4
		public static GenerateTrustedWebCredentialsRequest DeserializeLengthDelimited(Stream stream, GenerateTrustedWebCredentialsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateTrustedWebCredentialsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x0011640C File Offset: 0x0011460C
		public static GenerateTrustedWebCredentialsRequest Deserialize(Stream stream, GenerateTrustedWebCredentialsRequest instance, long limit)
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
						if (num != 10)
						{
							if (num == 21)
							{
								instance.Program = binaryReader.ReadUInt32();
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
						if (num == 26)
						{
							instance.PlatformId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.ClientIp = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.SessionKey = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005B55 RID: 23381 RVA: 0x00116517 File Offset: 0x00114717
		public void Serialize(Stream stream)
		{
			GenerateTrustedWebCredentialsRequest.Serialize(stream, this);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00116520 File Offset: 0x00114720
		public static void Serialize(Stream stream, GenerateTrustedWebCredentialsRequest instance)
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
			if (instance.HasSessionKey)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x001165E8 File Offset: 0x001147E8
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
			if (this.HasSessionKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionKey.Length) + (uint)this.SessionKey.Length;
			}
			return num;
		}

		// Token: 0x04001C5B RID: 7259
		public bool HasAccountId;

		// Token: 0x04001C5C RID: 7260
		private AccountId _AccountId;

		// Token: 0x04001C5D RID: 7261
		public bool HasProgram;

		// Token: 0x04001C5E RID: 7262
		private uint _Program;

		// Token: 0x04001C5F RID: 7263
		public bool HasPlatformId;

		// Token: 0x04001C60 RID: 7264
		private string _PlatformId;

		// Token: 0x04001C61 RID: 7265
		public bool HasClientIp;

		// Token: 0x04001C62 RID: 7266
		private string _ClientIp;

		// Token: 0x04001C63 RID: 7267
		public bool HasSessionKey;

		// Token: 0x04001C64 RID: 7268
		private byte[] _SessionKey;
	}
}
