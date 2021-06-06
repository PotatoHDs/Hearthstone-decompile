using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DC RID: 1244
	public class GetLoginTokenResponse : IProtoBuf
	{
		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x0010D43F File Offset: 0x0010B63F
		// (set) Token: 0x060057CC RID: 22476 RVA: 0x0010D447 File Offset: 0x0010B647
		public VoiceCredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
				this.HasCredentials = (value != null);
			}
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x0010D45A File Offset: 0x0010B65A
		public void SetCredentials(VoiceCredentials val)
		{
			this.Credentials = val;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0010D464 File Offset: 0x0010B664
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCredentials)
			{
				num ^= this.Credentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0010D494 File Offset: 0x0010B694
		public override bool Equals(object obj)
		{
			GetLoginTokenResponse getLoginTokenResponse = obj as GetLoginTokenResponse;
			return getLoginTokenResponse != null && this.HasCredentials == getLoginTokenResponse.HasCredentials && (!this.HasCredentials || this.Credentials.Equals(getLoginTokenResponse.Credentials));
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x0010D4D9 File Offset: 0x0010B6D9
		public static GetLoginTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLoginTokenResponse>(bs, 0, -1);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x0010D4E3 File Offset: 0x0010B6E3
		public void Deserialize(Stream stream)
		{
			GetLoginTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0010D4ED File Offset: 0x0010B6ED
		public static GetLoginTokenResponse Deserialize(Stream stream, GetLoginTokenResponse instance)
		{
			return GetLoginTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0010D4F8 File Offset: 0x0010B6F8
		public static GetLoginTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetLoginTokenResponse getLoginTokenResponse = new GetLoginTokenResponse();
			GetLoginTokenResponse.DeserializeLengthDelimited(stream, getLoginTokenResponse);
			return getLoginTokenResponse;
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0010D514 File Offset: 0x0010B714
		public static GetLoginTokenResponse DeserializeLengthDelimited(Stream stream, GetLoginTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLoginTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x0010D53C File Offset: 0x0010B73C
		public static GetLoginTokenResponse Deserialize(Stream stream, GetLoginTokenResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Credentials == null)
					{
						instance.Credentials = VoiceCredentials.DeserializeLengthDelimited(stream);
					}
					else
					{
						VoiceCredentials.DeserializeLengthDelimited(stream, instance.Credentials);
					}
				}
				else
				{
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

		// Token: 0x060057D7 RID: 22487 RVA: 0x0010D5D6 File Offset: 0x0010B7D6
		public void Serialize(Stream stream)
		{
			GetLoginTokenResponse.Serialize(stream, this);
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0010D5DF File Offset: 0x0010B7DF
		public static void Serialize(Stream stream, GetLoginTokenResponse instance)
		{
			if (instance.HasCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Credentials.GetSerializedSize());
				VoiceCredentials.Serialize(stream, instance.Credentials);
			}
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x0010D610 File Offset: 0x0010B810
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCredentials)
			{
				num += 1U;
				uint serializedSize = this.Credentials.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001B8D RID: 7053
		public bool HasCredentials;

		// Token: 0x04001B8E RID: 7054
		private VoiceCredentials _Credentials;
	}
}
