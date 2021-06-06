using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000300 RID: 768
	public class GetGoogleAuthTokenResponse : IProtoBuf
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x0009D77F File Offset: 0x0009B97F
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x0009D787 File Offset: 0x0009B987
		public string AccessToken
		{
			get
			{
				return this._AccessToken;
			}
			set
			{
				this._AccessToken = value;
				this.HasAccessToken = (value != null);
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0009D79A File Offset: 0x0009B99A
		public void SetAccessToken(string val)
		{
			this.AccessToken = val;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x0009D7A4 File Offset: 0x0009B9A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccessToken)
			{
				num ^= this.AccessToken.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0009D7D4 File Offset: 0x0009B9D4
		public override bool Equals(object obj)
		{
			GetGoogleAuthTokenResponse getGoogleAuthTokenResponse = obj as GetGoogleAuthTokenResponse;
			return getGoogleAuthTokenResponse != null && this.HasAccessToken == getGoogleAuthTokenResponse.HasAccessToken && (!this.HasAccessToken || this.AccessToken.Equals(getGoogleAuthTokenResponse.AccessToken));
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x0009D819 File Offset: 0x0009BA19
		public static GetGoogleAuthTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAuthTokenResponse>(bs, 0, -1);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x0009D823 File Offset: 0x0009BA23
		public void Deserialize(Stream stream)
		{
			GetGoogleAuthTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x0009D82D File Offset: 0x0009BA2D
		public static GetGoogleAuthTokenResponse Deserialize(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			return GetGoogleAuthTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0009D838 File Offset: 0x0009BA38
		public static GetGoogleAuthTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAuthTokenResponse getGoogleAuthTokenResponse = new GetGoogleAuthTokenResponse();
			GetGoogleAuthTokenResponse.DeserializeLengthDelimited(stream, getGoogleAuthTokenResponse);
			return getGoogleAuthTokenResponse;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x0009D854 File Offset: 0x0009BA54
		public static GetGoogleAuthTokenResponse DeserializeLengthDelimited(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGoogleAuthTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0009D87C File Offset: 0x0009BA7C
		public static GetGoogleAuthTokenResponse Deserialize(Stream stream, GetGoogleAuthTokenResponse instance, long limit)
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
					instance.AccessToken = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002E35 RID: 11829 RVA: 0x0009D8FC File Offset: 0x0009BAFC
		public void Serialize(Stream stream)
		{
			GetGoogleAuthTokenResponse.Serialize(stream, this);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x0009D905 File Offset: 0x0009BB05
		public static void Serialize(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			if (instance.HasAccessToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AccessToken));
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0009D930 File Offset: 0x0009BB30
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccessToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AccessToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040012B9 RID: 4793
		public bool HasAccessToken;

		// Token: 0x040012BA RID: 4794
		private string _AccessToken;
	}
}
