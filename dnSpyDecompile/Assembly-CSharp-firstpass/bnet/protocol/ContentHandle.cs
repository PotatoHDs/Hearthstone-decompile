using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002A0 RID: 672
	public class ContentHandle : IProtoBuf
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000890AB File Offset: 0x000872AB
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x000890B3 File Offset: 0x000872B3
		public uint Region { get; set; }

		// Token: 0x06002667 RID: 9831 RVA: 0x000890BC File Offset: 0x000872BC
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000890C5 File Offset: 0x000872C5
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x000890CD File Offset: 0x000872CD
		public uint Usage { get; set; }

		// Token: 0x0600266A RID: 9834 RVA: 0x000890D6 File Offset: 0x000872D6
		public void SetUsage(uint val)
		{
			this.Usage = val;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000890DF File Offset: 0x000872DF
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x000890E7 File Offset: 0x000872E7
		public byte[] Hash { get; set; }

		// Token: 0x0600266D RID: 9837 RVA: 0x000890F0 File Offset: 0x000872F0
		public void SetHash(byte[] val)
		{
			this.Hash = val;
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000890F9 File Offset: 0x000872F9
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x00089101 File Offset: 0x00087301
		public string ProtoUrl
		{
			get
			{
				return this._ProtoUrl;
			}
			set
			{
				this._ProtoUrl = value;
				this.HasProtoUrl = (value != null);
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00089114 File Offset: 0x00087314
		public void SetProtoUrl(string val)
		{
			this.ProtoUrl = val;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00089120 File Offset: 0x00087320
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Region.GetHashCode();
			num ^= this.Usage.GetHashCode();
			num ^= this.Hash.GetHashCode();
			if (this.HasProtoUrl)
			{
				num ^= this.ProtoUrl.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00089180 File Offset: 0x00087380
		public override bool Equals(object obj)
		{
			ContentHandle contentHandle = obj as ContentHandle;
			return contentHandle != null && this.Region.Equals(contentHandle.Region) && this.Usage.Equals(contentHandle.Usage) && this.Hash.Equals(contentHandle.Hash) && this.HasProtoUrl == contentHandle.HasProtoUrl && (!this.HasProtoUrl || this.ProtoUrl.Equals(contentHandle.ProtoUrl));
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0008920A File Offset: 0x0008740A
		public static ContentHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandle>(bs, 0, -1);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00089214 File Offset: 0x00087414
		public void Deserialize(Stream stream)
		{
			ContentHandle.Deserialize(stream, this);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008921E File Offset: 0x0008741E
		public static ContentHandle Deserialize(Stream stream, ContentHandle instance)
		{
			return ContentHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0008922C File Offset: 0x0008742C
		public static ContentHandle DeserializeLengthDelimited(Stream stream)
		{
			ContentHandle contentHandle = new ContentHandle();
			ContentHandle.DeserializeLengthDelimited(stream, contentHandle);
			return contentHandle;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00089248 File Offset: 0x00087448
		public static ContentHandle DeserializeLengthDelimited(Stream stream, ContentHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ContentHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00089270 File Offset: 0x00087470
		public static ContentHandle Deserialize(Stream stream, ContentHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 21)
					{
						if (num == 13)
						{
							instance.Region = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 21)
						{
							instance.Usage = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Hash = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 34)
						{
							instance.ProtoUrl = ProtocolParser.ReadString(stream);
							continue;
						}
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

		// Token: 0x0600267A RID: 9850 RVA: 0x00089348 File Offset: 0x00087548
		public void Serialize(Stream stream)
		{
			ContentHandle.Serialize(stream, this);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00089354 File Offset: 0x00087554
		public static void Serialize(Stream stream, ContentHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Region);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Usage);
			if (instance.Hash == null)
			{
				throw new ArgumentNullException("Hash", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Hash);
			if (instance.HasProtoUrl)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProtoUrl));
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000893E0 File Offset: 0x000875E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 4U;
			num += 4U;
			num += ProtocolParser.SizeOfUInt32(this.Hash.Length) + (uint)this.Hash.Length;
			if (this.HasProtoUrl)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ProtoUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 3U;
		}

		// Token: 0x040010F2 RID: 4338
		public bool HasProtoUrl;

		// Token: 0x040010F3 RID: 4339
		private string _ProtoUrl;
	}
}
