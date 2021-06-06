using System;
using System.IO;
using System.Text;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000312 RID: 786
	public class GetSignedSessionStateResponse : IProtoBuf
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000A189F File Offset: 0x0009FA9F
		// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x000A18A7 File Offset: 0x0009FAA7
		public string Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = (value != null);
			}
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000A18BA File Offset: 0x0009FABA
		public void SetToken(string val)
		{
			this.Token = val;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000A18C4 File Offset: 0x0009FAC4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000A18F4 File Offset: 0x0009FAF4
		public override bool Equals(object obj)
		{
			GetSignedSessionStateResponse getSignedSessionStateResponse = obj as GetSignedSessionStateResponse;
			return getSignedSessionStateResponse != null && this.HasToken == getSignedSessionStateResponse.HasToken && (!this.HasToken || this.Token.Equals(getSignedSessionStateResponse.Token));
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000A1939 File Offset: 0x0009FB39
		public static GetSignedSessionStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSignedSessionStateResponse>(bs, 0, -1);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000A1943 File Offset: 0x0009FB43
		public void Deserialize(Stream stream)
		{
			GetSignedSessionStateResponse.Deserialize(stream, this);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000A194D File Offset: 0x0009FB4D
		public static GetSignedSessionStateResponse Deserialize(Stream stream, GetSignedSessionStateResponse instance)
		{
			return GetSignedSessionStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000A1958 File Offset: 0x0009FB58
		public static GetSignedSessionStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSignedSessionStateResponse getSignedSessionStateResponse = new GetSignedSessionStateResponse();
			GetSignedSessionStateResponse.DeserializeLengthDelimited(stream, getSignedSessionStateResponse);
			return getSignedSessionStateResponse;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000A1974 File Offset: 0x0009FB74
		public static GetSignedSessionStateResponse DeserializeLengthDelimited(Stream stream, GetSignedSessionStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSignedSessionStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000A199C File Offset: 0x0009FB9C
		public static GetSignedSessionStateResponse Deserialize(Stream stream, GetSignedSessionStateResponse instance, long limit)
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
					instance.Token = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000A1A1C File Offset: 0x0009FC1C
		public void Serialize(Stream stream)
		{
			GetSignedSessionStateResponse.Serialize(stream, this);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000A1A25 File Offset: 0x0009FC25
		public static void Serialize(Stream stream, GetSignedSessionStateResponse instance)
		{
			if (instance.HasToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Token));
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000A1A50 File Offset: 0x0009FC50
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Token);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001317 RID: 4887
		public bool HasToken;

		// Token: 0x04001318 RID: 4888
		private string _Token;
	}
}
