using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x02000501 RID: 1281
	public class AuthenticateTokenResponse : IProtoBuf
	{
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x00115EA3 File Offset: 0x001140A3
		// (set) Token: 0x06005B2B RID: 23339 RVA: 0x00115EAB File Offset: 0x001140AB
		public byte[] AuthenticationToken
		{
			get
			{
				return this._AuthenticationToken;
			}
			set
			{
				this._AuthenticationToken = value;
				this.HasAuthenticationToken = (value != null);
			}
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x00115EBE File Offset: 0x001140BE
		public void SetAuthenticationToken(byte[] val)
		{
			this.AuthenticationToken = val;
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x00115EC7 File Offset: 0x001140C7
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x00115ECF File Offset: 0x001140CF
		public SSOData SsoData
		{
			get
			{
				return this._SsoData;
			}
			set
			{
				this._SsoData = value;
				this.HasSsoData = (value != null);
			}
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x00115EE2 File Offset: 0x001140E2
		public void SetSsoData(SSOData val)
		{
			this.SsoData = val;
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x00115EEC File Offset: 0x001140EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAuthenticationToken)
			{
				num ^= this.AuthenticationToken.GetHashCode();
			}
			if (this.HasSsoData)
			{
				num ^= this.SsoData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x00115F34 File Offset: 0x00114134
		public override bool Equals(object obj)
		{
			AuthenticateTokenResponse authenticateTokenResponse = obj as AuthenticateTokenResponse;
			return authenticateTokenResponse != null && this.HasAuthenticationToken == authenticateTokenResponse.HasAuthenticationToken && (!this.HasAuthenticationToken || this.AuthenticationToken.Equals(authenticateTokenResponse.AuthenticationToken)) && this.HasSsoData == authenticateTokenResponse.HasSsoData && (!this.HasSsoData || this.SsoData.Equals(authenticateTokenResponse.SsoData));
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x00115FA4 File Offset: 0x001141A4
		public static AuthenticateTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthenticateTokenResponse>(bs, 0, -1);
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x00115FAE File Offset: 0x001141AE
		public void Deserialize(Stream stream)
		{
			AuthenticateTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x00115FB8 File Offset: 0x001141B8
		public static AuthenticateTokenResponse Deserialize(Stream stream, AuthenticateTokenResponse instance)
		{
			return AuthenticateTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x00115FC4 File Offset: 0x001141C4
		public static AuthenticateTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			AuthenticateTokenResponse authenticateTokenResponse = new AuthenticateTokenResponse();
			AuthenticateTokenResponse.DeserializeLengthDelimited(stream, authenticateTokenResponse);
			return authenticateTokenResponse;
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x00115FE0 File Offset: 0x001141E0
		public static AuthenticateTokenResponse DeserializeLengthDelimited(Stream stream, AuthenticateTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AuthenticateTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x00116008 File Offset: 0x00114208
		public static AuthenticateTokenResponse Deserialize(Stream stream, AuthenticateTokenResponse instance, long limit)
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
					else if (instance.SsoData == null)
					{
						instance.SsoData = SSOData.DeserializeLengthDelimited(stream);
					}
					else
					{
						SSOData.DeserializeLengthDelimited(stream, instance.SsoData);
					}
				}
				else
				{
					instance.AuthenticationToken = ProtocolParser.ReadBytes(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x001160BA File Offset: 0x001142BA
		public void Serialize(Stream stream)
		{
			AuthenticateTokenResponse.Serialize(stream, this);
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x001160C4 File Offset: 0x001142C4
		public static void Serialize(Stream stream, AuthenticateTokenResponse instance)
		{
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
			}
			if (instance.HasSsoData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SsoData.GetSerializedSize());
				SSOData.Serialize(stream, instance.SsoData);
			}
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0011611C File Offset: 0x0011431C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAuthenticationToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AuthenticationToken.Length) + (uint)this.AuthenticationToken.Length;
			}
			if (this.HasSsoData)
			{
				num += 1U;
				uint serializedSize = this.SsoData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001C57 RID: 7255
		public bool HasAuthenticationToken;

		// Token: 0x04001C58 RID: 7256
		private byte[] _AuthenticationToken;

		// Token: 0x04001C59 RID: 7257
		public bool HasSsoData;

		// Token: 0x04001C5A RID: 7258
		private SSOData _SsoData;
	}
}
