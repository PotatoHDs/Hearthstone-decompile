using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000094 RID: 148
	public class DraftChosen : IProtoBuf
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000250E9 File Offset: 0x000232E9
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x000250F1 File Offset: 0x000232F1
		public CardDef Chosen { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000250FA File Offset: 0x000232FA
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00025102 File Offset: 0x00023302
		public List<CardDef> NextChoiceList
		{
			get
			{
				return this._NextChoiceList;
			}
			set
			{
				this._NextChoiceList = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002510B File Offset: 0x0002330B
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00025113 File Offset: 0x00023313
		public DraftSlotType SlotType { get; set; }

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002511C File Offset: 0x0002331C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Chosen.GetHashCode();
			foreach (CardDef cardDef in this.NextChoiceList)
			{
				num ^= cardDef.GetHashCode();
			}
			num ^= this.SlotType.GetHashCode();
			return num;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000251A4 File Offset: 0x000233A4
		public override bool Equals(object obj)
		{
			DraftChosen draftChosen = obj as DraftChosen;
			if (draftChosen == null)
			{
				return false;
			}
			if (!this.Chosen.Equals(draftChosen.Chosen))
			{
				return false;
			}
			if (this.NextChoiceList.Count != draftChosen.NextChoiceList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.NextChoiceList.Count; i++)
			{
				if (!this.NextChoiceList[i].Equals(draftChosen.NextChoiceList[i]))
				{
					return false;
				}
			}
			return this.SlotType.Equals(draftChosen.SlotType);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00025247 File Offset: 0x00023447
		public void Deserialize(Stream stream)
		{
			DraftChosen.Deserialize(stream, this);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00025251 File Offset: 0x00023451
		public static DraftChosen Deserialize(Stream stream, DraftChosen instance)
		{
			return DraftChosen.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002525C File Offset: 0x0002345C
		public static DraftChosen DeserializeLengthDelimited(Stream stream)
		{
			DraftChosen draftChosen = new DraftChosen();
			DraftChosen.DeserializeLengthDelimited(stream, draftChosen);
			return draftChosen;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00025278 File Offset: 0x00023478
		public static DraftChosen DeserializeLengthDelimited(Stream stream, DraftChosen instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftChosen.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000252A0 File Offset: 0x000234A0
		public static DraftChosen Deserialize(Stream stream, DraftChosen instance, long limit)
		{
			if (instance.NextChoiceList == null)
			{
				instance.NextChoiceList = new List<CardDef>();
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
				else if (num != 26)
				{
					if (num != 34)
					{
						if (num != 40)
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
							instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.NextChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.Chosen == null)
				{
					instance.Chosen = CardDef.DeserializeLengthDelimited(stream);
				}
				else
				{
					CardDef.DeserializeLengthDelimited(stream, instance.Chosen);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00025387 File Offset: 0x00023587
		public void Serialize(Stream stream)
		{
			DraftChosen.Serialize(stream, this);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00025390 File Offset: 0x00023590
		public static void Serialize(Stream stream, DraftChosen instance)
		{
			if (instance.Chosen == null)
			{
				throw new ArgumentNullException("Chosen", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Chosen.GetSerializedSize());
			CardDef.Serialize(stream, instance.Chosen);
			if (instance.NextChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in instance.NextChoiceList)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
					CardDef.Serialize(stream, cardDef);
				}
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SlotType));
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00025458 File Offset: 0x00023658
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Chosen.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.NextChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in this.NextChoiceList)
				{
					num += 1U;
					uint serializedSize2 = cardDef.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SlotType));
			num += 2U;
			return num;
		}

		// Token: 0x0400037F RID: 895
		private List<CardDef> _NextChoiceList = new List<CardDef>();

		// Token: 0x020005A6 RID: 1446
		public enum PacketID
		{
			// Token: 0x04001F51 RID: 8017
			ID = 249
		}
	}
}
