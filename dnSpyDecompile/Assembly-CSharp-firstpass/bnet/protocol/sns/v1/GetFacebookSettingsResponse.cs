using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FC RID: 764
	public class GetFacebookSettingsResponse : IProtoBuf
	{
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x0009CC87 File Offset: 0x0009AE87
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x0009CC8F File Offset: 0x0009AE8F
		public string AppId
		{
			get
			{
				return this._AppId;
			}
			set
			{
				this._AppId = value;
				this.HasAppId = (value != null);
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0009CCA2 File Offset: 0x0009AEA2
		public void SetAppId(string val)
		{
			this.AppId = val;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0009CCAB File Offset: 0x0009AEAB
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x0009CCB3 File Offset: 0x0009AEB3
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

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0009CCC6 File Offset: 0x0009AEC6
		public void SetRedirectUri(string val)
		{
			this.RedirectUri = val;
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x0009CCCF File Offset: 0x0009AECF
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x0009CCD7 File Offset: 0x0009AED7
		public string ApiVersion
		{
			get
			{
				return this._ApiVersion;
			}
			set
			{
				this._ApiVersion = value;
				this.HasApiVersion = (value != null);
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0009CCEA File Offset: 0x0009AEEA
		public void SetApiVersion(string val)
		{
			this.ApiVersion = val;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0009CCF4 File Offset: 0x0009AEF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAppId)
			{
				num ^= this.AppId.GetHashCode();
			}
			if (this.HasRedirectUri)
			{
				num ^= this.RedirectUri.GetHashCode();
			}
			if (this.HasApiVersion)
			{
				num ^= this.ApiVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0009CD50 File Offset: 0x0009AF50
		public override bool Equals(object obj)
		{
			GetFacebookSettingsResponse getFacebookSettingsResponse = obj as GetFacebookSettingsResponse;
			return getFacebookSettingsResponse != null && this.HasAppId == getFacebookSettingsResponse.HasAppId && (!this.HasAppId || this.AppId.Equals(getFacebookSettingsResponse.AppId)) && this.HasRedirectUri == getFacebookSettingsResponse.HasRedirectUri && (!this.HasRedirectUri || this.RedirectUri.Equals(getFacebookSettingsResponse.RedirectUri)) && this.HasApiVersion == getFacebookSettingsResponse.HasApiVersion && (!this.HasApiVersion || this.ApiVersion.Equals(getFacebookSettingsResponse.ApiVersion));
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0009CDEB File Offset: 0x0009AFEB
		public static GetFacebookSettingsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookSettingsResponse>(bs, 0, -1);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0009CDF5 File Offset: 0x0009AFF5
		public void Deserialize(Stream stream)
		{
			GetFacebookSettingsResponse.Deserialize(stream, this);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0009CDFF File Offset: 0x0009AFFF
		public static GetFacebookSettingsResponse Deserialize(Stream stream, GetFacebookSettingsResponse instance)
		{
			return GetFacebookSettingsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0009CE0C File Offset: 0x0009B00C
		public static GetFacebookSettingsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookSettingsResponse getFacebookSettingsResponse = new GetFacebookSettingsResponse();
			GetFacebookSettingsResponse.DeserializeLengthDelimited(stream, getFacebookSettingsResponse);
			return getFacebookSettingsResponse;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0009CE28 File Offset: 0x0009B028
		public static GetFacebookSettingsResponse DeserializeLengthDelimited(Stream stream, GetFacebookSettingsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookSettingsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0009CE50 File Offset: 0x0009B050
		public static GetFacebookSettingsResponse Deserialize(Stream stream, GetFacebookSettingsResponse instance, long limit)
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
							instance.ApiVersion = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.RedirectUri = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.AppId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0009CEFE File Offset: 0x0009B0FE
		public void Serialize(Stream stream)
		{
			GetFacebookSettingsResponse.Serialize(stream, this);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0009CF08 File Offset: 0x0009B108
		public static void Serialize(Stream stream, GetFacebookSettingsResponse instance)
		{
			if (instance.HasAppId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppId));
			}
			if (instance.HasRedirectUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RedirectUri));
			}
			if (instance.HasApiVersion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApiVersion));
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x0009CF88 File Offset: 0x0009B188
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAppId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AppId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRedirectUri)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.RedirectUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasApiVersion)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ApiVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x040012A9 RID: 4777
		public bool HasAppId;

		// Token: 0x040012AA RID: 4778
		private string _AppId;

		// Token: 0x040012AB RID: 4779
		public bool HasRedirectUri;

		// Token: 0x040012AC RID: 4780
		private string _RedirectUri;

		// Token: 0x040012AD RID: 4781
		public bool HasApiVersion;

		// Token: 0x040012AE RID: 4782
		private string _ApiVersion;
	}
}
