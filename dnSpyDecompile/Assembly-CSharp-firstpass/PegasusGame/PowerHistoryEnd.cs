using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C4 RID: 452
	public class PowerHistoryEnd : IProtoBuf
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006590E File Offset: 0x00063B0E
		public override bool Equals(object obj)
		{
			return obj is PowerHistoryEnd;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0006591B File Offset: 0x00063B1B
		public void Deserialize(Stream stream)
		{
			PowerHistoryEnd.Deserialize(stream, this);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00065925 File Offset: 0x00063B25
		public static PowerHistoryEnd Deserialize(Stream stream, PowerHistoryEnd instance)
		{
			return PowerHistoryEnd.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00065930 File Offset: 0x00063B30
		public static PowerHistoryEnd DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryEnd powerHistoryEnd = new PowerHistoryEnd();
			PowerHistoryEnd.DeserializeLengthDelimited(stream, powerHistoryEnd);
			return powerHistoryEnd;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0006594C File Offset: 0x00063B4C
		public static PowerHistoryEnd DeserializeLengthDelimited(Stream stream, PowerHistoryEnd instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryEnd.Deserialize(stream, instance, num);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00065974 File Offset: 0x00063B74
		public static PowerHistoryEnd Deserialize(Stream stream, PowerHistoryEnd instance, long limit)
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

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000659E1 File Offset: 0x00063BE1
		public void Serialize(Stream stream)
		{
			PowerHistoryEnd.Serialize(stream, this);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PowerHistoryEnd instance)
		{
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
