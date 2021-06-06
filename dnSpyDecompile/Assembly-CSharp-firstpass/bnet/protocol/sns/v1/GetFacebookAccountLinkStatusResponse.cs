using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FE RID: 766
	public class GetFacebookAccountLinkStatusResponse : IProtoBuf
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x0009D213 File Offset: 0x0009B413
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x0009D21B File Offset: 0x0009B41B
		public bool AccountLinked
		{
			get
			{
				return this._AccountLinked;
			}
			set
			{
				this._AccountLinked = value;
				this.HasAccountLinked = true;
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0009D22B File Offset: 0x0009B42B
		public void SetAccountLinked(bool val)
		{
			this.AccountLinked = val;
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x0009D234 File Offset: 0x0009B434
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x0009D23C File Offset: 0x0009B43C
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

		// Token: 0x06002E08 RID: 11784 RVA: 0x0009D24F File Offset: 0x0009B44F
		public void SetFbId(string val)
		{
			this.FbId = val;
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x0009D258 File Offset: 0x0009B458
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x0009D260 File Offset: 0x0009B460
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
				this.HasDisplayName = (value != null);
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0009D273 File Offset: 0x0009B473
		public void SetDisplayName(string val)
		{
			this.DisplayName = val;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0009D27C File Offset: 0x0009B47C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountLinked)
			{
				num ^= this.AccountLinked.GetHashCode();
			}
			if (this.HasFbId)
			{
				num ^= this.FbId.GetHashCode();
			}
			if (this.HasDisplayName)
			{
				num ^= this.DisplayName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0009D2DC File Offset: 0x0009B4DC
		public override bool Equals(object obj)
		{
			GetFacebookAccountLinkStatusResponse getFacebookAccountLinkStatusResponse = obj as GetFacebookAccountLinkStatusResponse;
			return getFacebookAccountLinkStatusResponse != null && this.HasAccountLinked == getFacebookAccountLinkStatusResponse.HasAccountLinked && (!this.HasAccountLinked || this.AccountLinked.Equals(getFacebookAccountLinkStatusResponse.AccountLinked)) && this.HasFbId == getFacebookAccountLinkStatusResponse.HasFbId && (!this.HasFbId || this.FbId.Equals(getFacebookAccountLinkStatusResponse.FbId)) && this.HasDisplayName == getFacebookAccountLinkStatusResponse.HasDisplayName && (!this.HasDisplayName || this.DisplayName.Equals(getFacebookAccountLinkStatusResponse.DisplayName));
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0009D37A File Offset: 0x0009B57A
		public static GetFacebookAccountLinkStatusResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAccountLinkStatusResponse>(bs, 0, -1);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0009D384 File Offset: 0x0009B584
		public void Deserialize(Stream stream)
		{
			GetFacebookAccountLinkStatusResponse.Deserialize(stream, this);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0009D38E File Offset: 0x0009B58E
		public static GetFacebookAccountLinkStatusResponse Deserialize(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			return GetFacebookAccountLinkStatusResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0009D39C File Offset: 0x0009B59C
		public static GetFacebookAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAccountLinkStatusResponse getFacebookAccountLinkStatusResponse = new GetFacebookAccountLinkStatusResponse();
			GetFacebookAccountLinkStatusResponse.DeserializeLengthDelimited(stream, getFacebookAccountLinkStatusResponse);
			return getFacebookAccountLinkStatusResponse;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0009D3B8 File Offset: 0x0009B5B8
		public static GetFacebookAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookAccountLinkStatusResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
		public static GetFacebookAccountLinkStatusResponse Deserialize(Stream stream, GetFacebookAccountLinkStatusResponse instance, long limit)
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
				else if (num != 8)
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
							instance.DisplayName = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.FbId = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.AccountLinked = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0009D48D File Offset: 0x0009B68D
		public void Serialize(Stream stream)
		{
			GetFacebookAccountLinkStatusResponse.Serialize(stream, this);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0009D498 File Offset: 0x0009B698
		public static void Serialize(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			if (instance.HasAccountLinked)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AccountLinked);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayName));
			}
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x0009D50C File Offset: 0x0009B70C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountLinked)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFbId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDisplayName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DisplayName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x040012B1 RID: 4785
		public bool HasAccountLinked;

		// Token: 0x040012B2 RID: 4786
		private bool _AccountLinked;

		// Token: 0x040012B3 RID: 4787
		public bool HasFbId;

		// Token: 0x040012B4 RID: 4788
		private string _FbId;

		// Token: 0x040012B5 RID: 4789
		public bool HasDisplayName;

		// Token: 0x040012B6 RID: 4790
		private string _DisplayName;
	}
}
