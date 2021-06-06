using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002A3 RID: 675
	public class EmbedInfo : IProtoBuf
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x00089984 File Offset: 0x00087B84
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0008998C File Offset: 0x00087B8C
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
				this.HasTitle = (value != null);
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0008999F File Offset: 0x00087B9F
		public void SetTitle(string val)
		{
			this.Title = val;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000899A8 File Offset: 0x00087BA8
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x000899B0 File Offset: 0x00087BB0
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000899C3 File Offset: 0x00087BC3
		public void SetType(string val)
		{
			this.Type = val;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000899CC File Offset: 0x00087BCC
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x000899D4 File Offset: 0x00087BD4
		public string OriginalUrl
		{
			get
			{
				return this._OriginalUrl;
			}
			set
			{
				this._OriginalUrl = value;
				this.HasOriginalUrl = (value != null);
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000899E7 File Offset: 0x00087BE7
		public void SetOriginalUrl(string val)
		{
			this.OriginalUrl = val;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000899F0 File Offset: 0x00087BF0
		// (set) Token: 0x060026AE RID: 9902 RVA: 0x000899F8 File Offset: 0x00087BF8
		public EmbedImage Thumbnail
		{
			get
			{
				return this._Thumbnail;
			}
			set
			{
				this._Thumbnail = value;
				this.HasThumbnail = (value != null);
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00089A0B File Offset: 0x00087C0B
		public void SetThumbnail(EmbedImage val)
		{
			this.Thumbnail = val;
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x00089A14 File Offset: 0x00087C14
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x00089A1C File Offset: 0x00087C1C
		public Provider Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
				this.HasProvider = (value != null);
			}
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x00089A2F File Offset: 0x00087C2F
		public void SetProvider(Provider val)
		{
			this.Provider = val;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x00089A38 File Offset: 0x00087C38
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x00089A40 File Offset: 0x00087C40
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.HasDescription = (value != null);
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x00089A53 File Offset: 0x00087C53
		public void SetDescription(string val)
		{
			this.Description = val;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00089A5C File Offset: 0x00087C5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTitle)
			{
				num ^= this.Title.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasOriginalUrl)
			{
				num ^= this.OriginalUrl.GetHashCode();
			}
			if (this.HasThumbnail)
			{
				num ^= this.Thumbnail.GetHashCode();
			}
			if (this.HasProvider)
			{
				num ^= this.Provider.GetHashCode();
			}
			if (this.HasDescription)
			{
				num ^= this.Description.GetHashCode();
			}
			return num;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00089AFC File Offset: 0x00087CFC
		public override bool Equals(object obj)
		{
			EmbedInfo embedInfo = obj as EmbedInfo;
			return embedInfo != null && this.HasTitle == embedInfo.HasTitle && (!this.HasTitle || this.Title.Equals(embedInfo.Title)) && this.HasType == embedInfo.HasType && (!this.HasType || this.Type.Equals(embedInfo.Type)) && this.HasOriginalUrl == embedInfo.HasOriginalUrl && (!this.HasOriginalUrl || this.OriginalUrl.Equals(embedInfo.OriginalUrl)) && this.HasThumbnail == embedInfo.HasThumbnail && (!this.HasThumbnail || this.Thumbnail.Equals(embedInfo.Thumbnail)) && this.HasProvider == embedInfo.HasProvider && (!this.HasProvider || this.Provider.Equals(embedInfo.Provider)) && this.HasDescription == embedInfo.HasDescription && (!this.HasDescription || this.Description.Equals(embedInfo.Description));
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00089C18 File Offset: 0x00087E18
		public static EmbedInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EmbedInfo>(bs, 0, -1);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x00089C22 File Offset: 0x00087E22
		public void Deserialize(Stream stream)
		{
			EmbedInfo.Deserialize(stream, this);
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x00089C2C File Offset: 0x00087E2C
		public static EmbedInfo Deserialize(Stream stream, EmbedInfo instance)
		{
			return EmbedInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00089C38 File Offset: 0x00087E38
		public static EmbedInfo DeserializeLengthDelimited(Stream stream)
		{
			EmbedInfo embedInfo = new EmbedInfo();
			EmbedInfo.DeserializeLengthDelimited(stream, embedInfo);
			return embedInfo;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00089C54 File Offset: 0x00087E54
		public static EmbedInfo DeserializeLengthDelimited(Stream stream, EmbedInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EmbedInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00089C7C File Offset: 0x00087E7C
		public static EmbedInfo Deserialize(Stream stream, EmbedInfo instance, long limit)
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
				else
				{
					if (num <= 26)
					{
						if (num == 10)
						{
							instance.Title = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Type = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 26)
						{
							instance.OriginalUrl = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num != 42)
						{
							if (num == 50)
							{
								instance.Description = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Provider == null)
							{
								instance.Provider = Provider.DeserializeLengthDelimited(stream);
								continue;
							}
							Provider.DeserializeLengthDelimited(stream, instance.Provider);
							continue;
						}
					}
					else
					{
						if (instance.Thumbnail == null)
						{
							instance.Thumbnail = EmbedImage.DeserializeLengthDelimited(stream);
							continue;
						}
						EmbedImage.DeserializeLengthDelimited(stream, instance.Thumbnail);
						continue;
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

		// Token: 0x060026BF RID: 9919 RVA: 0x00089DB9 File Offset: 0x00087FB9
		public void Serialize(Stream stream)
		{
			EmbedInfo.Serialize(stream, this);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00089DC4 File Offset: 0x00087FC4
		public static void Serialize(Stream stream, EmbedInfo instance)
		{
			if (instance.HasTitle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Title));
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			}
			if (instance.HasOriginalUrl)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OriginalUrl));
			}
			if (instance.HasThumbnail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Thumbnail.GetSerializedSize());
				EmbedImage.Serialize(stream, instance.Thumbnail);
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Provider.GetSerializedSize());
				Provider.Serialize(stream, instance.Provider);
			}
			if (instance.HasDescription)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00089EC4 File Offset: 0x000880C4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTitle)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Title);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Type);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasOriginalUrl)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.OriginalUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasThumbnail)
			{
				num += 1U;
				uint serializedSize = this.Thumbnail.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProvider)
			{
				num += 1U;
				uint serializedSize2 = this.Provider.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDescription)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Description);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x040010FC RID: 4348
		public bool HasTitle;

		// Token: 0x040010FD RID: 4349
		private string _Title;

		// Token: 0x040010FE RID: 4350
		public bool HasType;

		// Token: 0x040010FF RID: 4351
		private string _Type;

		// Token: 0x04001100 RID: 4352
		public bool HasOriginalUrl;

		// Token: 0x04001101 RID: 4353
		private string _OriginalUrl;

		// Token: 0x04001102 RID: 4354
		public bool HasThumbnail;

		// Token: 0x04001103 RID: 4355
		private EmbedImage _Thumbnail;

		// Token: 0x04001104 RID: 4356
		public bool HasProvider;

		// Token: 0x04001105 RID: 4357
		private Provider _Provider;

		// Token: 0x04001106 RID: 4358
		public bool HasDescription;

		// Token: 0x04001107 RID: 4359
		private string _Description;
	}
}
