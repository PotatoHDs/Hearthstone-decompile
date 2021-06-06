using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000085 RID: 133
	public class AcknowledgeBanner : IProtoBuf
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001D034 File Offset: 0x0001B234
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001D03C File Offset: 0x0001B23C
		public int Banner { get; set; }

		// Token: 0x0600082F RID: 2095 RVA: 0x0001D048 File Offset: 0x0001B248
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Banner.GetHashCode();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001D070 File Offset: 0x0001B270
		public override bool Equals(object obj)
		{
			AcknowledgeBanner acknowledgeBanner = obj as AcknowledgeBanner;
			return acknowledgeBanner != null && this.Banner.Equals(acknowledgeBanner.Banner);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001D0A2 File Offset: 0x0001B2A2
		public void Deserialize(Stream stream)
		{
			AcknowledgeBanner.Deserialize(stream, this);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		public static AcknowledgeBanner Deserialize(Stream stream, AcknowledgeBanner instance)
		{
			return AcknowledgeBanner.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		public static AcknowledgeBanner DeserializeLengthDelimited(Stream stream)
		{
			AcknowledgeBanner acknowledgeBanner = new AcknowledgeBanner();
			AcknowledgeBanner.DeserializeLengthDelimited(stream, acknowledgeBanner);
			return acknowledgeBanner;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public static AcknowledgeBanner DeserializeLengthDelimited(Stream stream, AcknowledgeBanner instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcknowledgeBanner.Deserialize(stream, instance, num);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		public static AcknowledgeBanner Deserialize(Stream stream, AcknowledgeBanner instance, long limit)
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
				else if (num == 8)
				{
					instance.Banner = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000836 RID: 2102 RVA: 0x0001D17C File Offset: 0x0001B37C
		public void Serialize(Stream stream)
		{
			AcknowledgeBanner.Serialize(stream, this);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001D185 File Offset: 0x0001B385
		public static void Serialize(Stream stream, AcknowledgeBanner instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Banner));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001D19B File Offset: 0x0001B39B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Banner)) + 1U;
		}

		// Token: 0x02000598 RID: 1432
		public enum PacketID
		{
			// Token: 0x04001F23 RID: 7971
			ID = 309,
			// Token: 0x04001F24 RID: 7972
			System = 0
		}
	}
}
