using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000303 RID: 771
	public class GetGoogleAccountLinkStatusResponse : IProtoBuf
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x0009DD53 File Offset: 0x0009BF53
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x0009DD5B File Offset: 0x0009BF5B
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

		// Token: 0x06002E5B RID: 11867 RVA: 0x0009DD6B File Offset: 0x0009BF6B
		public void SetAccountLinked(bool val)
		{
			this.AccountLinked = val;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x0009DD74 File Offset: 0x0009BF74
		// (set) Token: 0x06002E5D RID: 11869 RVA: 0x0009DD7C File Offset: 0x0009BF7C
		public string GoogleId
		{
			get
			{
				return this._GoogleId;
			}
			set
			{
				this._GoogleId = value;
				this.HasGoogleId = (value != null);
			}
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0009DD8F File Offset: 0x0009BF8F
		public void SetGoogleId(string val)
		{
			this.GoogleId = val;
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x0009DD98 File Offset: 0x0009BF98
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0009DDA0 File Offset: 0x0009BFA0
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

		// Token: 0x06002E61 RID: 11873 RVA: 0x0009DDB3 File Offset: 0x0009BFB3
		public void SetDisplayName(string val)
		{
			this.DisplayName = val;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x0009DDBC File Offset: 0x0009BFBC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountLinked)
			{
				num ^= this.AccountLinked.GetHashCode();
			}
			if (this.HasGoogleId)
			{
				num ^= this.GoogleId.GetHashCode();
			}
			if (this.HasDisplayName)
			{
				num ^= this.DisplayName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x0009DE1C File Offset: 0x0009C01C
		public override bool Equals(object obj)
		{
			GetGoogleAccountLinkStatusResponse getGoogleAccountLinkStatusResponse = obj as GetGoogleAccountLinkStatusResponse;
			return getGoogleAccountLinkStatusResponse != null && this.HasAccountLinked == getGoogleAccountLinkStatusResponse.HasAccountLinked && (!this.HasAccountLinked || this.AccountLinked.Equals(getGoogleAccountLinkStatusResponse.AccountLinked)) && this.HasGoogleId == getGoogleAccountLinkStatusResponse.HasGoogleId && (!this.HasGoogleId || this.GoogleId.Equals(getGoogleAccountLinkStatusResponse.GoogleId)) && this.HasDisplayName == getGoogleAccountLinkStatusResponse.HasDisplayName && (!this.HasDisplayName || this.DisplayName.Equals(getGoogleAccountLinkStatusResponse.DisplayName));
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0009DEBA File Offset: 0x0009C0BA
		public static GetGoogleAccountLinkStatusResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAccountLinkStatusResponse>(bs, 0, -1);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0009DEC4 File Offset: 0x0009C0C4
		public void Deserialize(Stream stream)
		{
			GetGoogleAccountLinkStatusResponse.Deserialize(stream, this);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0009DECE File Offset: 0x0009C0CE
		public static GetGoogleAccountLinkStatusResponse Deserialize(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			return GetGoogleAccountLinkStatusResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x0009DEDC File Offset: 0x0009C0DC
		public static GetGoogleAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAccountLinkStatusResponse getGoogleAccountLinkStatusResponse = new GetGoogleAccountLinkStatusResponse();
			GetGoogleAccountLinkStatusResponse.DeserializeLengthDelimited(stream, getGoogleAccountLinkStatusResponse);
			return getGoogleAccountLinkStatusResponse;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0009DEF8 File Offset: 0x0009C0F8
		public static GetGoogleAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGoogleAccountLinkStatusResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0009DF20 File Offset: 0x0009C120
		public static GetGoogleAccountLinkStatusResponse Deserialize(Stream stream, GetGoogleAccountLinkStatusResponse instance, long limit)
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
						instance.GoogleId = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002E6B RID: 11883 RVA: 0x0009DFCD File Offset: 0x0009C1CD
		public void Serialize(Stream stream)
		{
			GetGoogleAccountLinkStatusResponse.Serialize(stream, this);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0009DFD8 File Offset: 0x0009C1D8
		public static void Serialize(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			if (instance.HasAccountLinked)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AccountLinked);
			}
			if (instance.HasGoogleId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GoogleId));
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayName));
			}
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0009E04C File Offset: 0x0009C24C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountLinked)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGoogleId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.GoogleId);
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

		// Token: 0x040012BF RID: 4799
		public bool HasAccountLinked;

		// Token: 0x040012C0 RID: 4800
		private bool _AccountLinked;

		// Token: 0x040012C1 RID: 4801
		public bool HasGoogleId;

		// Token: 0x040012C2 RID: 4802
		private string _GoogleId;

		// Token: 0x040012C3 RID: 4803
		public bool HasDisplayName;

		// Token: 0x040012C4 RID: 4804
		private string _DisplayName;
	}
}
