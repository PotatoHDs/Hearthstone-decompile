using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FA RID: 762
	public class GetFacebookAuthCodeResponse : IProtoBuf
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x0009C46F File Offset: 0x0009A66F
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x0009C477 File Offset: 0x0009A677
		public string FbCode
		{
			get
			{
				return this._FbCode;
			}
			set
			{
				this._FbCode = value;
				this.HasFbCode = (value != null);
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x0009C48A File Offset: 0x0009A68A
		public void SetFbCode(string val)
		{
			this.FbCode = val;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x0009C493 File Offset: 0x0009A693
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x0009C49B File Offset: 0x0009A69B
		public string RedirectUri
		{
			get
			{
				return this._RedirectUri;
			}
			set
			{
				this._RedirectUri = value;
				this.HasRedirectUri = (value != null);
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x0009C4AE File Offset: 0x0009A6AE
		public void SetRedirectUri(string val)
		{
			this.RedirectUri = val;
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x0009C4B7 File Offset: 0x0009A6B7
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x0009C4BF File Offset: 0x0009A6BF
		public string FbId
		{
			get
			{
				return this._FbId;
			}
			set
			{
				this._FbId = value;
				this.HasFbId = (value != null);
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x0009C4D2 File Offset: 0x0009A6D2
		public void SetFbId(string val)
		{
			this.FbId = val;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0009C4DC File Offset: 0x0009A6DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFbCode)
			{
				num ^= this.FbCode.GetHashCode();
			}
			if (this.HasRedirectUri)
			{
				num ^= this.RedirectUri.GetHashCode();
			}
			if (this.HasFbId)
			{
				num ^= this.FbId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x0009C538 File Offset: 0x0009A738
		public override bool Equals(object obj)
		{
			GetFacebookAuthCodeResponse getFacebookAuthCodeResponse = obj as GetFacebookAuthCodeResponse;
			return getFacebookAuthCodeResponse != null && this.HasFbCode == getFacebookAuthCodeResponse.HasFbCode && (!this.HasFbCode || this.FbCode.Equals(getFacebookAuthCodeResponse.FbCode)) && this.HasRedirectUri == getFacebookAuthCodeResponse.HasRedirectUri && (!this.HasRedirectUri || this.RedirectUri.Equals(getFacebookAuthCodeResponse.RedirectUri)) && this.HasFbId == getFacebookAuthCodeResponse.HasFbId && (!this.HasFbId || this.FbId.Equals(getFacebookAuthCodeResponse.FbId));
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0009C5D3 File Offset: 0x0009A7D3
		public static GetFacebookAuthCodeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAuthCodeResponse>(bs, 0, -1);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0009C5DD File Offset: 0x0009A7DD
		public void Deserialize(Stream stream)
		{
			GetFacebookAuthCodeResponse.Deserialize(stream, this);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0009C5E7 File Offset: 0x0009A7E7
		public static GetFacebookAuthCodeResponse Deserialize(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			return GetFacebookAuthCodeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0009C5F4 File Offset: 0x0009A7F4
		public static GetFacebookAuthCodeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAuthCodeResponse getFacebookAuthCodeResponse = new GetFacebookAuthCodeResponse();
			GetFacebookAuthCodeResponse.DeserializeLengthDelimited(stream, getFacebookAuthCodeResponse);
			return getFacebookAuthCodeResponse;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0009C610 File Offset: 0x0009A810
		public static GetFacebookAuthCodeResponse DeserializeLengthDelimited(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookAuthCodeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0009C638 File Offset: 0x0009A838
		public static GetFacebookAuthCodeResponse Deserialize(Stream stream, GetFacebookAuthCodeResponse instance, long limit)
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
						if (num != 26)
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
							instance.FbId = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.RedirectUri = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.FbCode = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0009C6E6 File Offset: 0x0009A8E6
		public void Serialize(Stream stream)
		{
			GetFacebookAuthCodeResponse.Serialize(stream, this);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0009C6F0 File Offset: 0x0009A8F0
		public static void Serialize(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			if (instance.HasFbCode)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbCode));
			}
			if (instance.HasRedirectUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RedirectUri));
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0009C770 File Offset: 0x0009A970
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFbCode)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FbCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRedirectUri)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.RedirectUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFbId)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x0400129B RID: 4763
		public bool HasFbCode;

		// Token: 0x0400129C RID: 4764
		private string _FbCode;

		// Token: 0x0400129D RID: 4765
		public bool HasRedirectUri;

		// Token: 0x0400129E RID: 4766
		private string _RedirectUri;

		// Token: 0x0400129F RID: 4767
		public bool HasFbId;

		// Token: 0x040012A0 RID: 4768
		private string _FbId;
	}
}
