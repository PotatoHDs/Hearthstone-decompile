using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000021 RID: 33
	public class PatronCheckedOutOfFSG : IProtoBuf
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007355 File Offset: 0x00005555
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000735D File Offset: 0x0000555D
		public FSGPatron Patron { get; set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00007366 File Offset: 0x00005566
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Patron.GetHashCode();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007380 File Offset: 0x00005580
		public override bool Equals(object obj)
		{
			PatronCheckedOutOfFSG patronCheckedOutOfFSG = obj as PatronCheckedOutOfFSG;
			return patronCheckedOutOfFSG != null && this.Patron.Equals(patronCheckedOutOfFSG.Patron);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000073AF File Offset: 0x000055AF
		public void Deserialize(Stream stream)
		{
			PatronCheckedOutOfFSG.Deserialize(stream, this);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000073B9 File Offset: 0x000055B9
		public static PatronCheckedOutOfFSG Deserialize(Stream stream, PatronCheckedOutOfFSG instance)
		{
			return PatronCheckedOutOfFSG.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000073C4 File Offset: 0x000055C4
		public static PatronCheckedOutOfFSG DeserializeLengthDelimited(Stream stream)
		{
			PatronCheckedOutOfFSG patronCheckedOutOfFSG = new PatronCheckedOutOfFSG();
			PatronCheckedOutOfFSG.DeserializeLengthDelimited(stream, patronCheckedOutOfFSG);
			return patronCheckedOutOfFSG;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000073E0 File Offset: 0x000055E0
		public static PatronCheckedOutOfFSG DeserializeLengthDelimited(Stream stream, PatronCheckedOutOfFSG instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PatronCheckedOutOfFSG.Deserialize(stream, instance, num);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007408 File Offset: 0x00005608
		public static PatronCheckedOutOfFSG Deserialize(Stream stream, PatronCheckedOutOfFSG instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Patron == null)
					{
						instance.Patron = FSGPatron.DeserializeLengthDelimited(stream);
					}
					else
					{
						FSGPatron.DeserializeLengthDelimited(stream, instance.Patron);
					}
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

		// Token: 0x06000189 RID: 393 RVA: 0x000074A2 File Offset: 0x000056A2
		public void Serialize(Stream stream)
		{
			PatronCheckedOutOfFSG.Serialize(stream, this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000074AB File Offset: 0x000056AB
		public static void Serialize(Stream stream, PatronCheckedOutOfFSG instance)
		{
			if (instance.Patron == null)
			{
				throw new ArgumentNullException("Patron", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Patron.GetSerializedSize());
			FSGPatron.Serialize(stream, instance.Patron);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000074EC File Offset: 0x000056EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Patron.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x02000557 RID: 1367
		public enum PacketID
		{
			// Token: 0x04001E22 RID: 7714
			ID = 510,
			// Token: 0x04001E23 RID: 7715
			System = 3
		}
	}
}
