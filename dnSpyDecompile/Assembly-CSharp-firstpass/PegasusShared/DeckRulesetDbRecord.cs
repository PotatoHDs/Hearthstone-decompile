using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000152 RID: 338
	public class DeckRulesetDbRecord : IProtoBuf
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0004DF22 File Offset: 0x0004C122
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x0004DF2A File Offset: 0x0004C12A
		public int Id { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0004DF33 File Offset: 0x0004C133
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0004DF3B File Offset: 0x0004C13B
		public List<DeckRulesetRuleDbRecord> Rules
		{
			get
			{
				return this._Rules;
			}
			set
			{
				this._Rules = value;
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0004DF44 File Offset: 0x0004C144
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (DeckRulesetRuleDbRecord deckRulesetRuleDbRecord in this.Rules)
			{
				num ^= deckRulesetRuleDbRecord.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0004DFB8 File Offset: 0x0004C1B8
		public override bool Equals(object obj)
		{
			DeckRulesetDbRecord deckRulesetDbRecord = obj as DeckRulesetDbRecord;
			if (deckRulesetDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(deckRulesetDbRecord.Id))
			{
				return false;
			}
			if (this.Rules.Count != deckRulesetDbRecord.Rules.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Rules.Count; i++)
			{
				if (!this.Rules[i].Equals(deckRulesetDbRecord.Rules[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004E03B File Offset: 0x0004C23B
		public void Deserialize(Stream stream)
		{
			DeckRulesetDbRecord.Deserialize(stream, this);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0004E045 File Offset: 0x0004C245
		public static DeckRulesetDbRecord Deserialize(Stream stream, DeckRulesetDbRecord instance)
		{
			return DeckRulesetDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0004E050 File Offset: 0x0004C250
		public static DeckRulesetDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetDbRecord deckRulesetDbRecord = new DeckRulesetDbRecord();
			DeckRulesetDbRecord.DeserializeLengthDelimited(stream, deckRulesetDbRecord);
			return deckRulesetDbRecord;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0004E06C File Offset: 0x0004C26C
		public static DeckRulesetDbRecord DeserializeLengthDelimited(Stream stream, DeckRulesetDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckRulesetDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0004E094 File Offset: 0x0004C294
		public static DeckRulesetDbRecord Deserialize(Stream stream, DeckRulesetDbRecord instance, long limit)
		{
			if (instance.Rules == null)
			{
				instance.Rules = new List<DeckRulesetRuleDbRecord>();
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
						instance.Rules.Add(DeckRulesetRuleDbRecord.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0004E144 File Offset: 0x0004C344
		public void Serialize(Stream stream)
		{
			DeckRulesetDbRecord.Serialize(stream, this);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004E150 File Offset: 0x0004C350
		public static void Serialize(Stream stream, DeckRulesetDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.Rules.Count > 0)
			{
				foreach (DeckRulesetRuleDbRecord deckRulesetRuleDbRecord in instance.Rules)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, deckRulesetRuleDbRecord.GetSerializedSize());
					DeckRulesetRuleDbRecord.Serialize(stream, deckRulesetRuleDbRecord);
				}
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0004E1DC File Offset: 0x0004C3DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.Rules.Count > 0)
			{
				foreach (DeckRulesetRuleDbRecord deckRulesetRuleDbRecord in this.Rules)
				{
					num += 1U;
					uint serializedSize = deckRulesetRuleDbRecord.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000702 RID: 1794
		private List<DeckRulesetRuleDbRecord> _Rules = new List<DeckRulesetRuleDbRecord>();
	}
}
