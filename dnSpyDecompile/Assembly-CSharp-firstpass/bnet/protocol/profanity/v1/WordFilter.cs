using System;
using System.IO;
using System.Text;

namespace bnet.protocol.profanity.v1
{
	// Token: 0x02000331 RID: 817
	public class WordFilter : IProtoBuf
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000A8403 File Offset: 0x000A6603
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x000A840B File Offset: 0x000A660B
		public string Type { get; set; }

		// Token: 0x06003238 RID: 12856 RVA: 0x000A8414 File Offset: 0x000A6614
		public void SetType(string val)
		{
			this.Type = val;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000A841D File Offset: 0x000A661D
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000A8425 File Offset: 0x000A6625
		public string Regex { get; set; }

		// Token: 0x0600323B RID: 12859 RVA: 0x000A842E File Offset: 0x000A662E
		public void SetRegex(string val)
		{
			this.Regex = val;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000A8437 File Offset: 0x000A6637
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Type.GetHashCode() ^ this.Regex.GetHashCode();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000A845C File Offset: 0x000A665C
		public override bool Equals(object obj)
		{
			WordFilter wordFilter = obj as WordFilter;
			return wordFilter != null && this.Type.Equals(wordFilter.Type) && this.Regex.Equals(wordFilter.Regex);
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000A84A0 File Offset: 0x000A66A0
		public static WordFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilter>(bs, 0, -1);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000A84AA File Offset: 0x000A66AA
		public void Deserialize(Stream stream)
		{
			WordFilter.Deserialize(stream, this);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000A84B4 File Offset: 0x000A66B4
		public static WordFilter Deserialize(Stream stream, WordFilter instance)
		{
			return WordFilter.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000A84C0 File Offset: 0x000A66C0
		public static WordFilter DeserializeLengthDelimited(Stream stream)
		{
			WordFilter wordFilter = new WordFilter();
			WordFilter.DeserializeLengthDelimited(stream, wordFilter);
			return wordFilter;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000A84DC File Offset: 0x000A66DC
		public static WordFilter DeserializeLengthDelimited(Stream stream, WordFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WordFilter.Deserialize(stream, instance, num);
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000A8504 File Offset: 0x000A6704
		public static WordFilter Deserialize(Stream stream, WordFilter instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Regex = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Type = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000A859C File Offset: 0x000A679C
		public void Serialize(Stream stream)
		{
			WordFilter.Serialize(stream, this);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000A85A8 File Offset: 0x000A67A8
		public static void Serialize(Stream stream, WordFilter instance)
		{
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Regex == null)
			{
				throw new ArgumentNullException("Regex", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Regex));
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000A8624 File Offset: 0x000A6824
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Regex);
			return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
		}
	}
}
