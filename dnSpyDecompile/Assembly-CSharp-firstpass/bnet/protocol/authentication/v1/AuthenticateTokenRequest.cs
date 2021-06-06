using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x02000500 RID: 1280
	public class AuthenticateTokenRequest : IProtoBuf
	{
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x0011573E File Offset: 0x0011393E
		// (set) Token: 0x06005B06 RID: 23302 RVA: 0x00115746 File Offset: 0x00113946
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

		// Token: 0x06005B07 RID: 23303 RVA: 0x00115759 File Offset: 0x00113959
		public void SetAuthenticationToken(byte[] val)
		{
			this.AuthenticationToken = val;
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x00115762 File Offset: 0x00113962
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x0011576A File Offset: 0x0011396A
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

		// Token: 0x06005B0A RID: 23306 RVA: 0x0011577A File Offset: 0x0011397A
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06005B0B RID: 23307 RVA: 0x00115783 File Offset: 0x00113983
		// (set) Token: 0x06005B0C RID: 23308 RVA: 0x0011578B File Offset: 0x0011398B
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

		// Token: 0x06005B0D RID: 23309 RVA: 0x0011579E File Offset: 0x0011399E
		public void SetPlatformId(string val)
		{
			this.PlatformId = val;
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x001157A7 File Offset: 0x001139A7
		// (set) Token: 0x06005B0F RID: 23311 RVA: 0x001157AF File Offset: 0x001139AF
		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x001157C2 File Offset: 0x001139C2
		public void SetLocale(string val)
		{
			this.Locale = val;
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06005B11 RID: 23313 RVA: 0x001157CB File Offset: 0x001139CB
		// (set) Token: 0x06005B12 RID: 23314 RVA: 0x001157D3 File Offset: 0x001139D3
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

		// Token: 0x06005B13 RID: 23315 RVA: 0x001157E6 File Offset: 0x001139E6
		public void SetClientIp(string val)
		{
			this.ClientIp = val;
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x001157EF File Offset: 0x001139EF
		// (set) Token: 0x06005B15 RID: 23317 RVA: 0x001157F7 File Offset: 0x001139F7
		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this._UserAgent = value;
				this.HasUserAgent = (value != null);
			}
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x0011580A File Offset: 0x00113A0A
		public void SetUserAgent(string val)
		{
			this.UserAgent = val;
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x00115813 File Offset: 0x00113A13
		// (set) Token: 0x06005B18 RID: 23320 RVA: 0x0011581B File Offset: 0x00113A1B
		public ulong Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = true;
			}
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x0011582B File Offset: 0x00113A2B
		public void SetVersion(ulong val)
		{
			this.Version = val;
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x00115834 File Offset: 0x00113A34
		// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0011583C File Offset: 0x00113A3C
		public byte[] AuthenticationTokenId
		{
			get
			{
				return this._AuthenticationTokenId;
			}
			set
			{
				this._AuthenticationTokenId = value;
				this.HasAuthenticationTokenId = (value != null);
			}
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x0011584F File Offset: 0x00113A4F
		public void SetAuthenticationTokenId(byte[] val)
		{
			this.AuthenticationTokenId = val;
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x00115858 File Offset: 0x00113A58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAuthenticationToken)
			{
				num ^= this.AuthenticationToken.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasPlatformId)
			{
				num ^= this.PlatformId.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasClientIp)
			{
				num ^= this.ClientIp.GetHashCode();
			}
			if (this.HasUserAgent)
			{
				num ^= this.UserAgent.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			if (this.HasAuthenticationTokenId)
			{
				num ^= this.AuthenticationTokenId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x00115928 File Offset: 0x00113B28
		public override bool Equals(object obj)
		{
			AuthenticateTokenRequest authenticateTokenRequest = obj as AuthenticateTokenRequest;
			return authenticateTokenRequest != null && this.HasAuthenticationToken == authenticateTokenRequest.HasAuthenticationToken && (!this.HasAuthenticationToken || this.AuthenticationToken.Equals(authenticateTokenRequest.AuthenticationToken)) && this.HasProgram == authenticateTokenRequest.HasProgram && (!this.HasProgram || this.Program.Equals(authenticateTokenRequest.Program)) && this.HasPlatformId == authenticateTokenRequest.HasPlatformId && (!this.HasPlatformId || this.PlatformId.Equals(authenticateTokenRequest.PlatformId)) && this.HasLocale == authenticateTokenRequest.HasLocale && (!this.HasLocale || this.Locale.Equals(authenticateTokenRequest.Locale)) && this.HasClientIp == authenticateTokenRequest.HasClientIp && (!this.HasClientIp || this.ClientIp.Equals(authenticateTokenRequest.ClientIp)) && this.HasUserAgent == authenticateTokenRequest.HasUserAgent && (!this.HasUserAgent || this.UserAgent.Equals(authenticateTokenRequest.UserAgent)) && this.HasVersion == authenticateTokenRequest.HasVersion && (!this.HasVersion || this.Version.Equals(authenticateTokenRequest.Version)) && this.HasAuthenticationTokenId == authenticateTokenRequest.HasAuthenticationTokenId && (!this.HasAuthenticationTokenId || this.AuthenticationTokenId.Equals(authenticateTokenRequest.AuthenticationTokenId));
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x00115AA0 File Offset: 0x00113CA0
		public static AuthenticateTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthenticateTokenRequest>(bs, 0, -1);
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x00115AAA File Offset: 0x00113CAA
		public void Deserialize(Stream stream)
		{
			AuthenticateTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x00115AB4 File Offset: 0x00113CB4
		public static AuthenticateTokenRequest Deserialize(Stream stream, AuthenticateTokenRequest instance)
		{
			return AuthenticateTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x00115AC0 File Offset: 0x00113CC0
		public static AuthenticateTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			AuthenticateTokenRequest authenticateTokenRequest = new AuthenticateTokenRequest();
			AuthenticateTokenRequest.DeserializeLengthDelimited(stream, authenticateTokenRequest);
			return authenticateTokenRequest;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x00115ADC File Offset: 0x00113CDC
		public static AuthenticateTokenRequest DeserializeLengthDelimited(Stream stream, AuthenticateTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AuthenticateTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x00115B04 File Offset: 0x00113D04
		public static AuthenticateTokenRequest Deserialize(Stream stream, AuthenticateTokenRequest instance, long limit)
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
					if (num <= 34)
					{
						if (num <= 21)
						{
							if (num == 10)
							{
								instance.AuthenticationToken = ProtocolParser.ReadBytes(stream);
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
							if (num == 26)
							{
								instance.PlatformId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 34)
							{
								instance.Locale = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 50)
					{
						if (num == 42)
						{
							instance.ClientIp = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.UserAgent = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.Version = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 66)
						{
							instance.AuthenticationTokenId = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005B26 RID: 23334 RVA: 0x00115C54 File Offset: 0x00113E54
		public void Serialize(Stream stream)
		{
			AuthenticateTokenRequest.Serialize(stream, this);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x00115C60 File Offset: 0x00113E60
		public static void Serialize(Stream stream, AuthenticateTokenRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
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
			if (instance.HasLocale)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasClientIp)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientIp));
			}
			if (instance.HasUserAgent)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserAgent));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.Version);
			}
			if (instance.HasAuthenticationTokenId)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationTokenId);
			}
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00115D7C File Offset: 0x00113F7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAuthenticationToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AuthenticationToken.Length) + (uint)this.AuthenticationToken.Length;
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
			if (this.HasLocale)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasClientIp)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ClientIp);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasUserAgent)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Version);
			}
			if (this.HasAuthenticationTokenId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AuthenticationTokenId.Length) + (uint)this.AuthenticationTokenId.Length;
			}
			return num;
		}

		// Token: 0x04001C47 RID: 7239
		public bool HasAuthenticationToken;

		// Token: 0x04001C48 RID: 7240
		private byte[] _AuthenticationToken;

		// Token: 0x04001C49 RID: 7241
		public bool HasProgram;

		// Token: 0x04001C4A RID: 7242
		private uint _Program;

		// Token: 0x04001C4B RID: 7243
		public bool HasPlatformId;

		// Token: 0x04001C4C RID: 7244
		private string _PlatformId;

		// Token: 0x04001C4D RID: 7245
		public bool HasLocale;

		// Token: 0x04001C4E RID: 7246
		private string _Locale;

		// Token: 0x04001C4F RID: 7247
		public bool HasClientIp;

		// Token: 0x04001C50 RID: 7248
		private string _ClientIp;

		// Token: 0x04001C51 RID: 7249
		public bool HasUserAgent;

		// Token: 0x04001C52 RID: 7250
		private string _UserAgent;

		// Token: 0x04001C53 RID: 7251
		public bool HasVersion;

		// Token: 0x04001C54 RID: 7252
		private ulong _Version;

		// Token: 0x04001C55 RID: 7253
		public bool HasAuthenticationTokenId;

		// Token: 0x04001C56 RID: 7254
		private byte[] _AuthenticationTokenId;
	}
}
