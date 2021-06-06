using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A2 RID: 418
	public class GameRealTimeRaceCount : IProtoBuf
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0005CFB7 File Offset: 0x0005B1B7
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x0005CFBF File Offset: 0x0005B1BF
		public int Race { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0005CFC8 File Offset: 0x0005B1C8
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x0005CFD0 File Offset: 0x0005B1D0
		public int Count { get; set; }

		// Token: 0x06001A49 RID: 6729 RVA: 0x0005CFDC File Offset: 0x0005B1DC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Race.GetHashCode() ^ this.Count.GetHashCode();
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0005D014 File Offset: 0x0005B214
		public override bool Equals(object obj)
		{
			GameRealTimeRaceCount gameRealTimeRaceCount = obj as GameRealTimeRaceCount;
			return gameRealTimeRaceCount != null && this.Race.Equals(gameRealTimeRaceCount.Race) && this.Count.Equals(gameRealTimeRaceCount.Count);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0005D05E File Offset: 0x0005B25E
		public void Deserialize(Stream stream)
		{
			GameRealTimeRaceCount.Deserialize(stream, this);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0005D068 File Offset: 0x0005B268
		public static GameRealTimeRaceCount Deserialize(Stream stream, GameRealTimeRaceCount instance)
		{
			return GameRealTimeRaceCount.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0005D074 File Offset: 0x0005B274
		public static GameRealTimeRaceCount DeserializeLengthDelimited(Stream stream)
		{
			GameRealTimeRaceCount gameRealTimeRaceCount = new GameRealTimeRaceCount();
			GameRealTimeRaceCount.DeserializeLengthDelimited(stream, gameRealTimeRaceCount);
			return gameRealTimeRaceCount;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0005D090 File Offset: 0x0005B290
		public static GameRealTimeRaceCount DeserializeLengthDelimited(Stream stream, GameRealTimeRaceCount instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRealTimeRaceCount.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
		public static GameRealTimeRaceCount Deserialize(Stream stream, GameRealTimeRaceCount instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Race = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0005D151 File Offset: 0x0005B351
		public void Serialize(Stream stream)
		{
			GameRealTimeRaceCount.Serialize(stream, this);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0005D15A File Offset: 0x0005B35A
		public static void Serialize(Stream stream, GameRealTimeRaceCount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Race));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0005D185 File Offset: 0x0005B385
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Race)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Count)) + 2U;
		}
	}
}
