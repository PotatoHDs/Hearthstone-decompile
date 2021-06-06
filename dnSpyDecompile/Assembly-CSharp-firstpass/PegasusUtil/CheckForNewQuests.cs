using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000100 RID: 256
	public class CheckForNewQuests : IProtoBuf
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003BD2B File Offset: 0x00039F2B
		public override bool Equals(object obj)
		{
			return obj is CheckForNewQuests;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003BD38 File Offset: 0x00039F38
		public void Deserialize(Stream stream)
		{
			CheckForNewQuests.Deserialize(stream, this);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003BD42 File Offset: 0x00039F42
		public static CheckForNewQuests Deserialize(Stream stream, CheckForNewQuests instance)
		{
			return CheckForNewQuests.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003BD50 File Offset: 0x00039F50
		public static CheckForNewQuests DeserializeLengthDelimited(Stream stream)
		{
			CheckForNewQuests checkForNewQuests = new CheckForNewQuests();
			CheckForNewQuests.DeserializeLengthDelimited(stream, checkForNewQuests);
			return checkForNewQuests;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003BD6C File Offset: 0x00039F6C
		public static CheckForNewQuests DeserializeLengthDelimited(Stream stream, CheckForNewQuests instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckForNewQuests.Deserialize(stream, instance, num);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003BD94 File Offset: 0x00039F94
		public static CheckForNewQuests Deserialize(Stream stream, CheckForNewQuests instance, long limit)
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

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003BE01 File Offset: 0x0003A001
		public void Serialize(Stream stream)
		{
			CheckForNewQuests.Serialize(stream, this);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, CheckForNewQuests instance)
		{
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000602 RID: 1538
		public enum PacketID
		{
			// Token: 0x04002043 RID: 8259
			ID = 605,
			// Token: 0x04002044 RID: 8260
			System = 0
		}
	}
}
