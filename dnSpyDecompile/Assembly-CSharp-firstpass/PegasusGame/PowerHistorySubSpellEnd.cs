using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C6 RID: 454
	public class PowerHistorySubSpellEnd : IProtoBuf
	{
		// Token: 0x06001CE5 RID: 7397 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00065E0B File Offset: 0x0006400B
		public override bool Equals(object obj)
		{
			return obj is PowerHistorySubSpellEnd;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00065E18 File Offset: 0x00064018
		public void Deserialize(Stream stream)
		{
			PowerHistorySubSpellEnd.Deserialize(stream, this);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00065E22 File Offset: 0x00064022
		public static PowerHistorySubSpellEnd Deserialize(Stream stream, PowerHistorySubSpellEnd instance)
		{
			return PowerHistorySubSpellEnd.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00065E30 File Offset: 0x00064030
		public static PowerHistorySubSpellEnd DeserializeLengthDelimited(Stream stream)
		{
			PowerHistorySubSpellEnd powerHistorySubSpellEnd = new PowerHistorySubSpellEnd();
			PowerHistorySubSpellEnd.DeserializeLengthDelimited(stream, powerHistorySubSpellEnd);
			return powerHistorySubSpellEnd;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00065E4C File Offset: 0x0006404C
		public static PowerHistorySubSpellEnd DeserializeLengthDelimited(Stream stream, PowerHistorySubSpellEnd instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistorySubSpellEnd.Deserialize(stream, instance, num);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00065E74 File Offset: 0x00064074
		public static PowerHistorySubSpellEnd Deserialize(Stream stream, PowerHistorySubSpellEnd instance, long limit)
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

		// Token: 0x06001CEC RID: 7404 RVA: 0x00065EE1 File Offset: 0x000640E1
		public void Serialize(Stream stream)
		{
			PowerHistorySubSpellEnd.Serialize(stream, this);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PowerHistorySubSpellEnd instance)
		{
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
