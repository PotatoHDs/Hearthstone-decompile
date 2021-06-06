using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000020 RID: 32
	public class PatronCheckedInToFSG : IProtoBuf
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00007199 File Offset: 0x00005399
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000071A1 File Offset: 0x000053A1
		public FSGPatron Patron { get; set; }

		// Token: 0x06000175 RID: 373 RVA: 0x000071AA File Offset: 0x000053AA
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Patron.GetHashCode();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000071C4 File Offset: 0x000053C4
		public override bool Equals(object obj)
		{
			PatronCheckedInToFSG patronCheckedInToFSG = obj as PatronCheckedInToFSG;
			return patronCheckedInToFSG != null && this.Patron.Equals(patronCheckedInToFSG.Patron);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000071F3 File Offset: 0x000053F3
		public void Deserialize(Stream stream)
		{
			PatronCheckedInToFSG.Deserialize(stream, this);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000071FD File Offset: 0x000053FD
		public static PatronCheckedInToFSG Deserialize(Stream stream, PatronCheckedInToFSG instance)
		{
			return PatronCheckedInToFSG.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007208 File Offset: 0x00005408
		public static PatronCheckedInToFSG DeserializeLengthDelimited(Stream stream)
		{
			PatronCheckedInToFSG patronCheckedInToFSG = new PatronCheckedInToFSG();
			PatronCheckedInToFSG.DeserializeLengthDelimited(stream, patronCheckedInToFSG);
			return patronCheckedInToFSG;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007224 File Offset: 0x00005424
		public static PatronCheckedInToFSG DeserializeLengthDelimited(Stream stream, PatronCheckedInToFSG instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PatronCheckedInToFSG.Deserialize(stream, instance, num);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000724C File Offset: 0x0000544C
		public static PatronCheckedInToFSG Deserialize(Stream stream, PatronCheckedInToFSG instance, long limit)
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

		// Token: 0x0600017C RID: 380 RVA: 0x000072E6 File Offset: 0x000054E6
		public void Serialize(Stream stream)
		{
			PatronCheckedInToFSG.Serialize(stream, this);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000072EF File Offset: 0x000054EF
		public static void Serialize(Stream stream, PatronCheckedInToFSG instance)
		{
			if (instance.Patron == null)
			{
				throw new ArgumentNullException("Patron", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Patron.GetSerializedSize());
			FSGPatron.Serialize(stream, instance.Patron);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007330 File Offset: 0x00005530
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Patron.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x02000556 RID: 1366
		public enum PacketID
		{
			// Token: 0x04001E1F RID: 7711
			ID = 509,
			// Token: 0x04001E20 RID: 7712
			System = 3
		}
	}
}
