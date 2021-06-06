using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000156 RID: 342
	public class DeckDbRecord : IProtoBuf
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0004EC31 File Offset: 0x0004CE31
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0004EC39 File Offset: 0x0004CE39
		public int Id { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0004EC42 File Offset: 0x0004CE42
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x0004EC4A File Offset: 0x0004CE4A
		public string NoteName { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0004EC53 File Offset: 0x0004CE53
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0004EC5B File Offset: 0x0004CE5B
		public int TopCardId
		{
			get
			{
				return this._TopCardId;
			}
			set
			{
				this._TopCardId = value;
				this.HasTopCardId = true;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0004EC6B File Offset: 0x0004CE6B
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x0004EC73 File Offset: 0x0004CE73
		public List<DeckCardDbRecord> DeckCard
		{
			get
			{
				return this._DeckCard;
			}
			set
			{
				this._DeckCard = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0004EC7C File Offset: 0x0004CE7C
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0004EC84 File Offset: 0x0004CE84
		public List<LocalizedString> Strings
		{
			get
			{
				return this._Strings;
			}
			set
			{
				this._Strings = value;
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0004EC90 File Offset: 0x0004CE90
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.NoteName.GetHashCode();
			if (this.HasTopCardId)
			{
				num ^= this.TopCardId.GetHashCode();
			}
			foreach (DeckCardDbRecord deckCardDbRecord in this.DeckCard)
			{
				num ^= deckCardDbRecord.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0004ED74 File Offset: 0x0004CF74
		public override bool Equals(object obj)
		{
			DeckDbRecord deckDbRecord = obj as DeckDbRecord;
			if (deckDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(deckDbRecord.Id))
			{
				return false;
			}
			if (!this.NoteName.Equals(deckDbRecord.NoteName))
			{
				return false;
			}
			if (this.HasTopCardId != deckDbRecord.HasTopCardId || (this.HasTopCardId && !this.TopCardId.Equals(deckDbRecord.TopCardId)))
			{
				return false;
			}
			if (this.DeckCard.Count != deckDbRecord.DeckCard.Count)
			{
				return false;
			}
			for (int i = 0; i < this.DeckCard.Count; i++)
			{
				if (!this.DeckCard[i].Equals(deckDbRecord.DeckCard[i]))
				{
					return false;
				}
			}
			if (this.Strings.Count != deckDbRecord.Strings.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Strings.Count; j++)
			{
				if (!this.Strings[j].Equals(deckDbRecord.Strings[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0004EE8B File Offset: 0x0004D08B
		public void Deserialize(Stream stream)
		{
			DeckDbRecord.Deserialize(stream, this);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004EE95 File Offset: 0x0004D095
		public static DeckDbRecord Deserialize(Stream stream, DeckDbRecord instance)
		{
			return DeckDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0004EEA0 File Offset: 0x0004D0A0
		public static DeckDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckDbRecord deckDbRecord = new DeckDbRecord();
			DeckDbRecord.DeserializeLengthDelimited(stream, deckDbRecord);
			return deckDbRecord;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0004EEBC File Offset: 0x0004D0BC
		public static DeckDbRecord DeserializeLengthDelimited(Stream stream, DeckDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		public static DeckDbRecord Deserialize(Stream stream, DeckDbRecord instance, long limit)
		{
			if (instance.DeckCard == null)
			{
				instance.DeckCard = new List<DeckCardDbRecord>();
			}
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.Id = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.NoteName = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.TopCardId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.DeckCard.Add(DeckCardDbRecord.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
							continue;
						}
					}
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

		// Token: 0x060016E9 RID: 5865 RVA: 0x0004EFFC File Offset: 0x0004D1FC
		public void Serialize(Stream stream)
		{
			DeckDbRecord.Serialize(stream, this);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0004F008 File Offset: 0x0004D208
		public static void Serialize(Stream stream, DeckDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.NoteName == null)
			{
				throw new ArgumentNullException("NoteName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NoteName));
			if (instance.HasTopCardId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TopCardId));
			}
			if (instance.DeckCard.Count > 0)
			{
				foreach (DeckCardDbRecord deckCardDbRecord in instance.DeckCard)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, deckCardDbRecord.GetSerializedSize());
					DeckCardDbRecord.Serialize(stream, deckCardDbRecord);
				}
			}
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0004F148 File Offset: 0x0004D348
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.NoteName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasTopCardId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TopCardId));
			}
			if (this.DeckCard.Count > 0)
			{
				foreach (DeckCardDbRecord deckCardDbRecord in this.DeckCard)
				{
					num += 1U;
					uint serializedSize = deckCardDbRecord.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 1U;
					uint serializedSize2 = localizedString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x0400070E RID: 1806
		public bool HasTopCardId;

		// Token: 0x0400070F RID: 1807
		private int _TopCardId;

		// Token: 0x04000710 RID: 1808
		private List<DeckCardDbRecord> _DeckCard = new List<DeckCardDbRecord>();

		// Token: 0x04000711 RID: 1809
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
