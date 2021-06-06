using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200005B RID: 91
	public class SetProgress : IProtoBuf
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00017282 File Offset: 0x00015482
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0001728A File Offset: 0x0001548A
		public long Value { get; set; }

		// Token: 0x060005CF RID: 1487 RVA: 0x00017294 File Offset: 0x00015494
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000172BC File Offset: 0x000154BC
		public override bool Equals(object obj)
		{
			SetProgress setProgress = obj as SetProgress;
			return setProgress != null && this.Value.Equals(setProgress.Value);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000172EE File Offset: 0x000154EE
		public void Deserialize(Stream stream)
		{
			SetProgress.Deserialize(stream, this);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000172F8 File Offset: 0x000154F8
		public static SetProgress Deserialize(Stream stream, SetProgress instance)
		{
			return SetProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00017304 File Offset: 0x00015504
		public static SetProgress DeserializeLengthDelimited(Stream stream)
		{
			SetProgress setProgress = new SetProgress();
			SetProgress.DeserializeLengthDelimited(stream, setProgress);
			return setProgress;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00017320 File Offset: 0x00015520
		public static SetProgress DeserializeLengthDelimited(Stream stream, SetProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00017348 File Offset: 0x00015548
		public static SetProgress Deserialize(Stream stream, SetProgress instance, long limit)
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
					instance.Value = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060005D6 RID: 1494 RVA: 0x000173C7 File Offset: 0x000155C7
		public void Serialize(Stream stream)
		{
			SetProgress.Serialize(stream, this);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000173D0 File Offset: 0x000155D0
		public static void Serialize(Stream stream, SetProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Value);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000173E5 File Offset: 0x000155E5
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Value) + 1U;
		}

		// Token: 0x0200056D RID: 1389
		public enum PacketID
		{
			// Token: 0x04001E9D RID: 7837
			ID = 230,
			// Token: 0x04001E9E RID: 7838
			System = 0
		}
	}
}
