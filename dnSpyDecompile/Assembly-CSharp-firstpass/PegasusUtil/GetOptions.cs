using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000051 RID: 81
	public class GetOptions : IProtoBuf
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000157B4 File Offset: 0x000139B4
		public override bool Equals(object obj)
		{
			return obj is GetOptions;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000157C1 File Offset: 0x000139C1
		public void Deserialize(Stream stream)
		{
			GetOptions.Deserialize(stream, this);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000157CB File Offset: 0x000139CB
		public static GetOptions Deserialize(Stream stream, GetOptions instance)
		{
			return GetOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000157D8 File Offset: 0x000139D8
		public static GetOptions DeserializeLengthDelimited(Stream stream)
		{
			GetOptions getOptions = new GetOptions();
			GetOptions.DeserializeLengthDelimited(stream, getOptions);
			return getOptions;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000157F4 File Offset: 0x000139F4
		public static GetOptions DeserializeLengthDelimited(Stream stream, GetOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001581C File Offset: 0x00013A1C
		public static GetOptions Deserialize(Stream stream, GetOptions instance, long limit)
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

		// Token: 0x06000534 RID: 1332 RVA: 0x00015889 File Offset: 0x00013A89
		public void Serialize(Stream stream)
		{
			GetOptions.Serialize(stream, this);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetOptions instance)
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000562 RID: 1378
		public enum PacketID
		{
			// Token: 0x04001E63 RID: 7779
			ID = 240,
			// Token: 0x04001E64 RID: 7780
			System = 0
		}
	}
}
