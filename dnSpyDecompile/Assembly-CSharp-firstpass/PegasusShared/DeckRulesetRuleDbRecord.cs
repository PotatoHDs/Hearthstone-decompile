using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000151 RID: 337
	public class DeckRulesetRuleDbRecord : IProtoBuf
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0004D255 File Offset: 0x0004B455
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0004D25D File Offset: 0x0004B45D
		public int Id { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0004D266 File Offset: 0x0004B466
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0004D26E File Offset: 0x0004B46E
		public int DeckRulesetId { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004D277 File Offset: 0x0004B477
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x0004D27F File Offset: 0x0004B47F
		public int AppliesToSubsetId
		{
			get
			{
				return this._AppliesToSubsetId;
			}
			set
			{
				this._AppliesToSubsetId = value;
				this.HasAppliesToSubsetId = true;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0004D28F File Offset: 0x0004B48F
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0004D297 File Offset: 0x0004B497
		public bool AppliesToIsNot
		{
			get
			{
				return this._AppliesToIsNot;
			}
			set
			{
				this._AppliesToIsNot = value;
				this.HasAppliesToIsNot = true;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0004D2A7 File Offset: 0x0004B4A7
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0004D2AF File Offset: 0x0004B4AF
		public string RuleType { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0004D2C0 File Offset: 0x0004B4C0
		public bool RuleIsNot { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0004D2C9 File Offset: 0x0004B4C9
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0004D2D1 File Offset: 0x0004B4D1
		public int MinValue
		{
			get
			{
				return this._MinValue;
			}
			set
			{
				this._MinValue = value;
				this.HasMinValue = true;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0004D2E1 File Offset: 0x0004B4E1
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x0004D2E9 File Offset: 0x0004B4E9
		public int MaxValue
		{
			get
			{
				return this._MaxValue;
			}
			set
			{
				this._MaxValue = value;
				this.HasMaxValue = true;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0004D2F9 File Offset: 0x0004B4F9
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x0004D301 File Offset: 0x0004B501
		public int Tag
		{
			get
			{
				return this._Tag;
			}
			set
			{
				this._Tag = value;
				this.HasTag = true;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0004D311 File Offset: 0x0004B511
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x0004D319 File Offset: 0x0004B519
		public int TagMinValue
		{
			get
			{
				return this._TagMinValue;
			}
			set
			{
				this._TagMinValue = value;
				this.HasTagMinValue = true;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0004D329 File Offset: 0x0004B529
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x0004D331 File Offset: 0x0004B531
		public int TagMaxValue
		{
			get
			{
				return this._TagMaxValue;
			}
			set
			{
				this._TagMaxValue = value;
				this.HasTagMaxValue = true;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0004D341 File Offset: 0x0004B541
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x0004D349 File Offset: 0x0004B549
		public string StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
				this.HasStringValue = (value != null);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0004D35C File Offset: 0x0004B55C
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x0004D364 File Offset: 0x0004B564
		public List<int> TargetSubsetIds
		{
			get
			{
				return this._TargetSubsetIds;
			}
			set
			{
				this._TargetSubsetIds = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0004D36D File Offset: 0x0004B56D
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x0004D375 File Offset: 0x0004B575
		public bool ShowInvalidCards { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0004D37E File Offset: 0x0004B57E
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x0004D386 File Offset: 0x0004B586
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

		// Token: 0x0600168D RID: 5773 RVA: 0x0004D390 File Offset: 0x0004B590
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.DeckRulesetId.GetHashCode();
			if (this.HasAppliesToSubsetId)
			{
				num ^= this.AppliesToSubsetId.GetHashCode();
			}
			if (this.HasAppliesToIsNot)
			{
				num ^= this.AppliesToIsNot.GetHashCode();
			}
			num ^= this.RuleType.GetHashCode();
			num ^= this.RuleIsNot.GetHashCode();
			if (this.HasMinValue)
			{
				num ^= this.MinValue.GetHashCode();
			}
			if (this.HasMaxValue)
			{
				num ^= this.MaxValue.GetHashCode();
			}
			if (this.HasTag)
			{
				num ^= this.Tag.GetHashCode();
			}
			if (this.HasTagMinValue)
			{
				num ^= this.TagMinValue.GetHashCode();
			}
			if (this.HasTagMaxValue)
			{
				num ^= this.TagMaxValue.GetHashCode();
			}
			if (this.HasStringValue)
			{
				num ^= this.StringValue.GetHashCode();
			}
			foreach (int num2 in this.TargetSubsetIds)
			{
				num ^= num2.GetHashCode();
			}
			num ^= this.ShowInvalidCards.GetHashCode();
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004D554 File Offset: 0x0004B754
		public override bool Equals(object obj)
		{
			DeckRulesetRuleDbRecord deckRulesetRuleDbRecord = obj as DeckRulesetRuleDbRecord;
			if (deckRulesetRuleDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(deckRulesetRuleDbRecord.Id))
			{
				return false;
			}
			if (!this.DeckRulesetId.Equals(deckRulesetRuleDbRecord.DeckRulesetId))
			{
				return false;
			}
			if (this.HasAppliesToSubsetId != deckRulesetRuleDbRecord.HasAppliesToSubsetId || (this.HasAppliesToSubsetId && !this.AppliesToSubsetId.Equals(deckRulesetRuleDbRecord.AppliesToSubsetId)))
			{
				return false;
			}
			if (this.HasAppliesToIsNot != deckRulesetRuleDbRecord.HasAppliesToIsNot || (this.HasAppliesToIsNot && !this.AppliesToIsNot.Equals(deckRulesetRuleDbRecord.AppliesToIsNot)))
			{
				return false;
			}
			if (!this.RuleType.Equals(deckRulesetRuleDbRecord.RuleType))
			{
				return false;
			}
			if (!this.RuleIsNot.Equals(deckRulesetRuleDbRecord.RuleIsNot))
			{
				return false;
			}
			if (this.HasMinValue != deckRulesetRuleDbRecord.HasMinValue || (this.HasMinValue && !this.MinValue.Equals(deckRulesetRuleDbRecord.MinValue)))
			{
				return false;
			}
			if (this.HasMaxValue != deckRulesetRuleDbRecord.HasMaxValue || (this.HasMaxValue && !this.MaxValue.Equals(deckRulesetRuleDbRecord.MaxValue)))
			{
				return false;
			}
			if (this.HasTag != deckRulesetRuleDbRecord.HasTag || (this.HasTag && !this.Tag.Equals(deckRulesetRuleDbRecord.Tag)))
			{
				return false;
			}
			if (this.HasTagMinValue != deckRulesetRuleDbRecord.HasTagMinValue || (this.HasTagMinValue && !this.TagMinValue.Equals(deckRulesetRuleDbRecord.TagMinValue)))
			{
				return false;
			}
			if (this.HasTagMaxValue != deckRulesetRuleDbRecord.HasTagMaxValue || (this.HasTagMaxValue && !this.TagMaxValue.Equals(deckRulesetRuleDbRecord.TagMaxValue)))
			{
				return false;
			}
			if (this.HasStringValue != deckRulesetRuleDbRecord.HasStringValue || (this.HasStringValue && !this.StringValue.Equals(deckRulesetRuleDbRecord.StringValue)))
			{
				return false;
			}
			if (this.TargetSubsetIds.Count != deckRulesetRuleDbRecord.TargetSubsetIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TargetSubsetIds.Count; i++)
			{
				if (!this.TargetSubsetIds[i].Equals(deckRulesetRuleDbRecord.TargetSubsetIds[i]))
				{
					return false;
				}
			}
			if (!this.ShowInvalidCards.Equals(deckRulesetRuleDbRecord.ShowInvalidCards))
			{
				return false;
			}
			if (this.Strings.Count != deckRulesetRuleDbRecord.Strings.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Strings.Count; j++)
			{
				if (!this.Strings[j].Equals(deckRulesetRuleDbRecord.Strings[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004D7FB File Offset: 0x0004B9FB
		public void Deserialize(Stream stream)
		{
			DeckRulesetRuleDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004D805 File Offset: 0x0004BA05
		public static DeckRulesetRuleDbRecord Deserialize(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			return DeckRulesetRuleDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004D810 File Offset: 0x0004BA10
		public static DeckRulesetRuleDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetRuleDbRecord deckRulesetRuleDbRecord = new DeckRulesetRuleDbRecord();
			DeckRulesetRuleDbRecord.DeserializeLengthDelimited(stream, deckRulesetRuleDbRecord);
			return deckRulesetRuleDbRecord;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004D82C File Offset: 0x0004BA2C
		public static DeckRulesetRuleDbRecord DeserializeLengthDelimited(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckRulesetRuleDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0004D854 File Offset: 0x0004BA54
		public static DeckRulesetRuleDbRecord Deserialize(Stream stream, DeckRulesetRuleDbRecord instance, long limit)
		{
			if (instance.TargetSubsetIds == null)
			{
				instance.TargetSubsetIds = new List<int>();
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.Id = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.AppliesToSubsetId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num == 32)
							{
								instance.AppliesToIsNot = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 42)
							{
								instance.RuleType = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.RuleIsNot = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 56)
							{
								instance.MinValue = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 80)
					{
						if (num == 64)
						{
							instance.MaxValue = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.Tag = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 80)
						{
							instance.TagMinValue = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 98)
					{
						if (num == 88)
						{
							instance.TagMaxValue = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 98)
						{
							instance.StringValue = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 104)
						{
							instance.TargetSubsetIds.Add((int)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 112)
						{
							instance.ShowInvalidCards = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004DABB File Offset: 0x0004BCBB
		public void Serialize(Stream stream)
		{
			DeckRulesetRuleDbRecord.Serialize(stream, this);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
		public static void Serialize(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckRulesetId));
			if (instance.HasAppliesToSubsetId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AppliesToSubsetId));
			}
			if (instance.HasAppliesToIsNot)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.AppliesToIsNot);
			}
			if (instance.RuleType == null)
			{
				throw new ArgumentNullException("RuleType", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RuleType));
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.RuleIsNot);
			if (instance.HasMinValue)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MinValue));
			}
			if (instance.HasMaxValue)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxValue));
			}
			if (instance.HasTag)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Tag));
			}
			if (instance.HasTagMinValue)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TagMinValue));
			}
			if (instance.HasTagMaxValue)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TagMaxValue));
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.TargetSubsetIds.Count > 0)
			{
				foreach (int num in instance.TargetSubsetIds)
				{
					stream.WriteByte(104);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			stream.WriteByte(112);
			ProtocolParser.WriteBool(stream, instance.ShowInvalidCards);
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004DD14 File Offset: 0x0004BF14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckRulesetId));
			if (this.HasAppliesToSubsetId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AppliesToSubsetId));
			}
			if (this.HasAppliesToIsNot)
			{
				num += 1U;
				num += 1U;
			}
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RuleType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += 1U;
			if (this.HasMinValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MinValue));
			}
			if (this.HasMaxValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxValue));
			}
			if (this.HasTag)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Tag));
			}
			if (this.HasTagMinValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TagMinValue));
			}
			if (this.HasTagMaxValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TagMaxValue));
			}
			if (this.HasStringValue)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.TargetSubsetIds.Count > 0)
			{
				foreach (int num2 in this.TargetSubsetIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += 1U;
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize = localizedString.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 5U;
			return num;
		}

		// Token: 0x040006EC RID: 1772
		public bool HasAppliesToSubsetId;

		// Token: 0x040006ED RID: 1773
		private int _AppliesToSubsetId;

		// Token: 0x040006EE RID: 1774
		public bool HasAppliesToIsNot;

		// Token: 0x040006EF RID: 1775
		private bool _AppliesToIsNot;

		// Token: 0x040006F2 RID: 1778
		public bool HasMinValue;

		// Token: 0x040006F3 RID: 1779
		private int _MinValue;

		// Token: 0x040006F4 RID: 1780
		public bool HasMaxValue;

		// Token: 0x040006F5 RID: 1781
		private int _MaxValue;

		// Token: 0x040006F6 RID: 1782
		public bool HasTag;

		// Token: 0x040006F7 RID: 1783
		private int _Tag;

		// Token: 0x040006F8 RID: 1784
		public bool HasTagMinValue;

		// Token: 0x040006F9 RID: 1785
		private int _TagMinValue;

		// Token: 0x040006FA RID: 1786
		public bool HasTagMaxValue;

		// Token: 0x040006FB RID: 1787
		private int _TagMaxValue;

		// Token: 0x040006FC RID: 1788
		public bool HasStringValue;

		// Token: 0x040006FD RID: 1789
		private string _StringValue;

		// Token: 0x040006FE RID: 1790
		private List<int> _TargetSubsetIds = new List<int>();

		// Token: 0x04000700 RID: 1792
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
