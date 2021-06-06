using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000098 RID: 152
	public class DeckList : IProtoBuf
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00025CF4 File Offset: 0x00023EF4
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00025CFC File Offset: 0x00023EFC
		public List<DeckInfo> Decks
		{
			get
			{
				return this._Decks;
			}
			set
			{
				this._Decks = value;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00025D08 File Offset: 0x00023F08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DeckInfo deckInfo in this.Decks)
			{
				num ^= deckInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00025D6C File Offset: 0x00023F6C
		public override bool Equals(object obj)
		{
			DeckList deckList = obj as DeckList;
			if (deckList == null)
			{
				return false;
			}
			if (this.Decks.Count != deckList.Decks.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Decks.Count; i++)
			{
				if (!this.Decks[i].Equals(deckList.Decks[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00025DD7 File Offset: 0x00023FD7
		public void Deserialize(Stream stream)
		{
			DeckList.Deserialize(stream, this);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00025DE1 File Offset: 0x00023FE1
		public static DeckList Deserialize(Stream stream, DeckList instance)
		{
			return DeckList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00025DEC File Offset: 0x00023FEC
		public static DeckList DeserializeLengthDelimited(Stream stream)
		{
			DeckList deckList = new DeckList();
			DeckList.DeserializeLengthDelimited(stream, deckList);
			return deckList;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00025E08 File Offset: 0x00024008
		public static DeckList DeserializeLengthDelimited(Stream stream, DeckList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckList.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00025E30 File Offset: 0x00024030
		public static DeckList Deserialize(Stream stream, DeckList instance, long limit)
		{
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckInfo>();
			}
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
					instance.Decks.Add(DeckInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000A47 RID: 2631 RVA: 0x00025EC8 File Offset: 0x000240C8
		public void Serialize(Stream stream)
		{
			DeckList.Serialize(stream, this);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00025ED4 File Offset: 0x000240D4
		public static void Serialize(Stream stream, DeckList instance)
		{
			if (instance.Decks.Count > 0)
			{
				foreach (DeckInfo deckInfo in instance.Decks)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, deckInfo.GetSerializedSize());
					DeckInfo.Serialize(stream, deckInfo);
				}
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00025F4C File Offset: 0x0002414C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Decks.Count > 0)
			{
				foreach (DeckInfo deckInfo in this.Decks)
				{
					num += 1U;
					uint serializedSize = deckInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000389 RID: 905
		private List<DeckInfo> _Decks = new List<DeckInfo>();

		// Token: 0x020005A9 RID: 1449
		public enum PacketID
		{
			// Token: 0x04001F57 RID: 8023
			ID = 202
		}
	}
}
