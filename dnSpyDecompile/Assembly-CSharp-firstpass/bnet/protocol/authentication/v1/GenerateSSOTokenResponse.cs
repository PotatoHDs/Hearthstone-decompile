using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004EF RID: 1263
	public class GenerateSSOTokenResponse : IProtoBuf
	{
		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060059A9 RID: 22953 RVA: 0x0011268C File Offset: 0x0011088C
		// (set) Token: 0x060059AA RID: 22954 RVA: 0x00112694 File Offset: 0x00110894
		public byte[] SsoId
		{
			get
			{
				return this._SsoId;
			}
			set
			{
				this._SsoId = value;
				this.HasSsoId = (value != null);
			}
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x001126A7 File Offset: 0x001108A7
		public void SetSsoId(byte[] val)
		{
			this.SsoId = val;
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x001126B0 File Offset: 0x001108B0
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x001126B8 File Offset: 0x001108B8
		public byte[] SsoSecret
		{
			get
			{
				return this._SsoSecret;
			}
			set
			{
				this._SsoSecret = value;
				this.HasSsoSecret = (value != null);
			}
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x001126CB File Offset: 0x001108CB
		public void SetSsoSecret(byte[] val)
		{
			this.SsoSecret = val;
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x001126D4 File Offset: 0x001108D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSsoId)
			{
				num ^= this.SsoId.GetHashCode();
			}
			if (this.HasSsoSecret)
			{
				num ^= this.SsoSecret.GetHashCode();
			}
			return num;
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0011271C File Offset: 0x0011091C
		public override bool Equals(object obj)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = obj as GenerateSSOTokenResponse;
			return generateSSOTokenResponse != null && this.HasSsoId == generateSSOTokenResponse.HasSsoId && (!this.HasSsoId || this.SsoId.Equals(generateSSOTokenResponse.SsoId)) && this.HasSsoSecret == generateSSOTokenResponse.HasSsoSecret && (!this.HasSsoSecret || this.SsoSecret.Equals(generateSSOTokenResponse.SsoSecret));
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060059B1 RID: 22961 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0011278C File Offset: 0x0011098C
		public static GenerateSSOTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateSSOTokenResponse>(bs, 0, -1);
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x00112796 File Offset: 0x00110996
		public void Deserialize(Stream stream)
		{
			GenerateSSOTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x001127A0 File Offset: 0x001109A0
		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			return GenerateSSOTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x001127AC File Offset: 0x001109AC
		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = new GenerateSSOTokenResponse();
			GenerateSSOTokenResponse.DeserializeLengthDelimited(stream, generateSSOTokenResponse);
			return generateSSOTokenResponse;
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x001127C8 File Offset: 0x001109C8
		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream, GenerateSSOTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateSSOTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x001127F0 File Offset: 0x001109F0
		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance, long limit)
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
					else
					{
						instance.SsoSecret = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.SsoId = ProtocolParser.ReadBytes(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x00112888 File Offset: 0x00110A88
		public void Serialize(Stream stream)
		{
			GenerateSSOTokenResponse.Serialize(stream, this);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x00112891 File Offset: 0x00110A91
		public static void Serialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			if (instance.HasSsoId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SsoId);
			}
			if (instance.HasSsoSecret)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.SsoSecret);
			}
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x001128CC File Offset: 0x00110ACC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSsoId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SsoId.Length) + (uint)this.SsoId.Length;
			}
			if (this.HasSsoSecret)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SsoSecret.Length) + (uint)this.SsoSecret.Length;
			}
			return num;
		}

		// Token: 0x04001C02 RID: 7170
		public bool HasSsoId;

		// Token: 0x04001C03 RID: 7171
		private byte[] _SsoId;

		// Token: 0x04001C04 RID: 7172
		public bool HasSsoSecret;

		// Token: 0x04001C05 RID: 7173
		private byte[] _SsoSecret;
	}
}
