using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200011D RID: 285
	public class SmartDeckCardData : IProtoBuf
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0004187A File Offset: 0x0003FA7A
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00041882 File Offset: 0x0003FA82
		public int Asset { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004188B File Offset: 0x0003FA8B
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00041893 File Offset: 0x0003FA93
		public int QtyNormal { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0004189C File Offset: 0x0003FA9C
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x000418A4 File Offset: 0x0003FAA4
		public int QtyGolden { get; set; }

		// Token: 0x060012B7 RID: 4791 RVA: 0x000418B0 File Offset: 0x0003FAB0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Asset.GetHashCode() ^ this.QtyNormal.GetHashCode() ^ this.QtyGolden.GetHashCode();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000418F8 File Offset: 0x0003FAF8
		public override bool Equals(object obj)
		{
			SmartDeckCardData smartDeckCardData = obj as SmartDeckCardData;
			return smartDeckCardData != null && this.Asset.Equals(smartDeckCardData.Asset) && this.QtyNormal.Equals(smartDeckCardData.QtyNormal) && this.QtyGolden.Equals(smartDeckCardData.QtyGolden);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004195A File Offset: 0x0003FB5A
		public void Deserialize(Stream stream)
		{
			SmartDeckCardData.Deserialize(stream, this);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00041964 File Offset: 0x0003FB64
		public static SmartDeckCardData Deserialize(Stream stream, SmartDeckCardData instance)
		{
			return SmartDeckCardData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00041970 File Offset: 0x0003FB70
		public static SmartDeckCardData DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckCardData smartDeckCardData = new SmartDeckCardData();
			SmartDeckCardData.DeserializeLengthDelimited(stream, smartDeckCardData);
			return smartDeckCardData;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004198C File Offset: 0x0003FB8C
		public static SmartDeckCardData DeserializeLengthDelimited(Stream stream, SmartDeckCardData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SmartDeckCardData.Deserialize(stream, instance, num);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000419B4 File Offset: 0x0003FBB4
		public static SmartDeckCardData Deserialize(Stream stream, SmartDeckCardData instance, long limit)
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
							instance.QtyGolden = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.QtyNormal = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Asset = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00041A64 File Offset: 0x0003FC64
		public void Serialize(Stream stream)
		{
			SmartDeckCardData.Serialize(stream, this);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00041A6D File Offset: 0x0003FC6D
		public static void Serialize(Stream stream, SmartDeckCardData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Asset));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QtyNormal));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QtyGolden));
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00041AAD File Offset: 0x0003FCAD
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Asset)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.QtyNormal)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.QtyGolden)) + 3U;
		}
	}
}
