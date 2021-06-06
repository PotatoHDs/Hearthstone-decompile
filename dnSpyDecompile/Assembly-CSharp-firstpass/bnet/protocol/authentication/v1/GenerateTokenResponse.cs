using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FF RID: 1279
	public class GenerateTokenResponse : IProtoBuf
	{
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x001154A3 File Offset: 0x001136A3
		// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x001154AB File Offset: 0x001136AB
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

		// Token: 0x06005AF4 RID: 23284 RVA: 0x001154BE File Offset: 0x001136BE
		public void SetAuthenticationToken(byte[] val)
		{
			this.AuthenticationToken = val;
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x001154C7 File Offset: 0x001136C7
		// (set) Token: 0x06005AF6 RID: 23286 RVA: 0x001154CF File Offset: 0x001136CF
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

		// Token: 0x06005AF7 RID: 23287 RVA: 0x001154E2 File Offset: 0x001136E2
		public void SetAuthenticationTokenId(byte[] val)
		{
			this.AuthenticationTokenId = val;
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x001154EC File Offset: 0x001136EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAuthenticationToken)
			{
				num ^= this.AuthenticationToken.GetHashCode();
			}
			if (this.HasAuthenticationTokenId)
			{
				num ^= this.AuthenticationTokenId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x00115534 File Offset: 0x00113734
		public override bool Equals(object obj)
		{
			GenerateTokenResponse generateTokenResponse = obj as GenerateTokenResponse;
			return generateTokenResponse != null && this.HasAuthenticationToken == generateTokenResponse.HasAuthenticationToken && (!this.HasAuthenticationToken || this.AuthenticationToken.Equals(generateTokenResponse.AuthenticationToken)) && this.HasAuthenticationTokenId == generateTokenResponse.HasAuthenticationTokenId && (!this.HasAuthenticationTokenId || this.AuthenticationTokenId.Equals(generateTokenResponse.AuthenticationTokenId));
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x001155A4 File Offset: 0x001137A4
		public static GenerateTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTokenResponse>(bs, 0, -1);
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x001155AE File Offset: 0x001137AE
		public void Deserialize(Stream stream)
		{
			GenerateTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x001155B8 File Offset: 0x001137B8
		public static GenerateTokenResponse Deserialize(Stream stream, GenerateTokenResponse instance)
		{
			return GenerateTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x001155C4 File Offset: 0x001137C4
		public static GenerateTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateTokenResponse generateTokenResponse = new GenerateTokenResponse();
			GenerateTokenResponse.DeserializeLengthDelimited(stream, generateTokenResponse);
			return generateTokenResponse;
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x001155E0 File Offset: 0x001137E0
		public static GenerateTokenResponse DeserializeLengthDelimited(Stream stream, GenerateTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x00115608 File Offset: 0x00113808
		public static GenerateTokenResponse Deserialize(Stream stream, GenerateTokenResponse instance, long limit)
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
						instance.AuthenticationTokenId = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005B01 RID: 23297 RVA: 0x001156A0 File Offset: 0x001138A0
		public void Serialize(Stream stream)
		{
			GenerateTokenResponse.Serialize(stream, this);
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x001156A9 File Offset: 0x001138A9
		public static void Serialize(Stream stream, GenerateTokenResponse instance)
		{
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
			}
			if (instance.HasAuthenticationTokenId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationTokenId);
			}
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x001156E4 File Offset: 0x001138E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAuthenticationToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AuthenticationToken.Length) + (uint)this.AuthenticationToken.Length;
			}
			if (this.HasAuthenticationTokenId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AuthenticationTokenId.Length) + (uint)this.AuthenticationTokenId.Length;
			}
			return num;
		}

		// Token: 0x04001C43 RID: 7235
		public bool HasAuthenticationToken;

		// Token: 0x04001C44 RID: 7236
		private byte[] _AuthenticationToken;

		// Token: 0x04001C45 RID: 7237
		public bool HasAuthenticationTokenId;

		// Token: 0x04001C46 RID: 7238
		private byte[] _AuthenticationTokenId;
	}
}
