using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000056 RID: 86
	public class RenameDeck : IProtoBuf
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000168A1 File Offset: 0x00014AA1
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000168A9 File Offset: 0x00014AA9
		public long Deck { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000168B2 File Offset: 0x00014AB2
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000168BA File Offset: 0x00014ABA
		public string Name { get; set; }

		// Token: 0x0600058A RID: 1418 RVA: 0x000168C4 File Offset: 0x00014AC4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000168F8 File Offset: 0x00014AF8
		public override bool Equals(object obj)
		{
			RenameDeck renameDeck = obj as RenameDeck;
			return renameDeck != null && this.Deck.Equals(renameDeck.Deck) && this.Name.Equals(renameDeck.Name);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001693F File Offset: 0x00014B3F
		public void Deserialize(Stream stream)
		{
			RenameDeck.Deserialize(stream, this);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016949 File Offset: 0x00014B49
		public static RenameDeck Deserialize(Stream stream, RenameDeck instance)
		{
			return RenameDeck.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016954 File Offset: 0x00014B54
		public static RenameDeck DeserializeLengthDelimited(Stream stream)
		{
			RenameDeck renameDeck = new RenameDeck();
			RenameDeck.DeserializeLengthDelimited(stream, renameDeck);
			return renameDeck;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00016970 File Offset: 0x00014B70
		public static RenameDeck DeserializeLengthDelimited(Stream stream, RenameDeck instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RenameDeck.Deserialize(stream, instance, num);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00016998 File Offset: 0x00014B98
		public static RenameDeck Deserialize(Stream stream, RenameDeck instance, long limit)
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
					if (num != 18)
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
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00016A2F File Offset: 0x00014C2F
		public void Serialize(Stream stream)
		{
			RenameDeck.Serialize(stream, this);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00016A38 File Offset: 0x00014C38
		public static void Serialize(Stream stream, RenameDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00016A90 File Offset: 0x00014C90
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 2U;
		}

		// Token: 0x02000568 RID: 1384
		public enum PacketID
		{
			// Token: 0x04001E90 RID: 7824
			ID = 211,
			// Token: 0x04001E91 RID: 7825
			System = 0
		}
	}
}
