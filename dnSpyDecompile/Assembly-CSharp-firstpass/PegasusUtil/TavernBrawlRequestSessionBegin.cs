using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000E3 RID: 227
	public class TavernBrawlRequestSessionBegin : IProtoBuf
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000379AD File Offset: 0x00035BAD
		public override bool Equals(object obj)
		{
			return obj is TavernBrawlRequestSessionBegin;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000379BA File Offset: 0x00035BBA
		public void Deserialize(Stream stream)
		{
			TavernBrawlRequestSessionBegin.Deserialize(stream, this);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000379C4 File Offset: 0x00035BC4
		public static TavernBrawlRequestSessionBegin Deserialize(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
			return TavernBrawlRequestSessionBegin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x000379D0 File Offset: 0x00035BD0
		public static TavernBrawlRequestSessionBegin DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionBegin tavernBrawlRequestSessionBegin = new TavernBrawlRequestSessionBegin();
			TavernBrawlRequestSessionBegin.DeserializeLengthDelimited(stream, tavernBrawlRequestSessionBegin);
			return tavernBrawlRequestSessionBegin;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000379EC File Offset: 0x00035BEC
		public static TavernBrawlRequestSessionBegin DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlRequestSessionBegin.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00037A14 File Offset: 0x00035C14
		public static TavernBrawlRequestSessionBegin Deserialize(Stream stream, TavernBrawlRequestSessionBegin instance, long limit)
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

		// Token: 0x06000F66 RID: 3942 RVA: 0x00037A81 File Offset: 0x00035C81
		public void Serialize(Stream stream)
		{
			TavernBrawlRequestSessionBegin.Serialize(stream, this);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005E7 RID: 1511
		public enum PacketID
		{
			// Token: 0x04001FFB RID: 8187
			ID = 343,
			// Token: 0x04001FFC RID: 8188
			System = 0
		}
	}
}
