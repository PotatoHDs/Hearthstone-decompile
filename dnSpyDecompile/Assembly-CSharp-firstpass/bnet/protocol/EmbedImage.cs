using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002A1 RID: 673
	public class EmbedImage : IProtoBuf
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x0008943D File Offset: 0x0008763D
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x00089445 File Offset: 0x00087645
		public string Url
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
				this.HasUrl = (value != null);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00089458 File Offset: 0x00087658
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00089461 File Offset: 0x00087661
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x00089469 File Offset: 0x00087669
		public uint Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				this._Width = value;
				this.HasWidth = true;
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00089479 File Offset: 0x00087679
		public void SetWidth(uint val)
		{
			this.Width = val;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x00089482 File Offset: 0x00087682
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x0008948A File Offset: 0x0008768A
		public uint Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				this._Height = value;
				this.HasHeight = true;
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008949A File Offset: 0x0008769A
		public void SetHeight(uint val)
		{
			this.Height = val;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000894A4 File Offset: 0x000876A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			if (this.HasWidth)
			{
				num ^= this.Width.GetHashCode();
			}
			if (this.HasHeight)
			{
				num ^= this.Height.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x00089508 File Offset: 0x00087708
		public override bool Equals(object obj)
		{
			EmbedImage embedImage = obj as EmbedImage;
			return embedImage != null && this.HasUrl == embedImage.HasUrl && (!this.HasUrl || this.Url.Equals(embedImage.Url)) && this.HasWidth == embedImage.HasWidth && (!this.HasWidth || this.Width.Equals(embedImage.Width)) && this.HasHeight == embedImage.HasHeight && (!this.HasHeight || this.Height.Equals(embedImage.Height));
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000895A9 File Offset: 0x000877A9
		public static EmbedImage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EmbedImage>(bs, 0, -1);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000895B3 File Offset: 0x000877B3
		public void Deserialize(Stream stream)
		{
			EmbedImage.Deserialize(stream, this);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000895BD File Offset: 0x000877BD
		public static EmbedImage Deserialize(Stream stream, EmbedImage instance)
		{
			return EmbedImage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000895C8 File Offset: 0x000877C8
		public static EmbedImage DeserializeLengthDelimited(Stream stream)
		{
			EmbedImage embedImage = new EmbedImage();
			EmbedImage.DeserializeLengthDelimited(stream, embedImage);
			return embedImage;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000895E4 File Offset: 0x000877E4
		public static EmbedImage DeserializeLengthDelimited(Stream stream, EmbedImage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EmbedImage.Deserialize(stream, instance, num);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008960C File Offset: 0x0008780C
		public static EmbedImage Deserialize(Stream stream, EmbedImage instance, long limit)
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
					if (num != 16)
					{
						if (num != 24)
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
							instance.Height = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Width = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Url = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000896BA File Offset: 0x000878BA
		public void Serialize(Stream stream)
		{
			EmbedImage.Serialize(stream, this);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000896C4 File Offset: 0x000878C4
		public static void Serialize(Stream stream, EmbedImage instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
			if (instance.HasWidth)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Width);
			}
			if (instance.HasHeight)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Height);
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00089730 File Offset: 0x00087930
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUrl)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Url);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasWidth)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Width);
			}
			if (this.HasHeight)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Height);
			}
			return num;
		}

		// Token: 0x040010F4 RID: 4340
		public bool HasUrl;

		// Token: 0x040010F5 RID: 4341
		private string _Url;

		// Token: 0x040010F6 RID: 4342
		public bool HasWidth;

		// Token: 0x040010F7 RID: 4343
		private uint _Width;

		// Token: 0x040010F8 RID: 4344
		public bool HasHeight;

		// Token: 0x040010F9 RID: 4345
		private uint _Height;
	}
}
