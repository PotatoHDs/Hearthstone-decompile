using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000157 RID: 343
	public class DeckTemplateDbRecord : IProtoBuf
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0004F282 File Offset: 0x0004D482
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x0004F28A File Offset: 0x0004D48A
		public int Id { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0004F293 File Offset: 0x0004D493
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0004F29B File Offset: 0x0004D49B
		public int ClassId { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0004F2AC File Offset: 0x0004D4AC
		public string Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				this._Event = value;
				this.HasEvent = (value != null);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0004F2BF File Offset: 0x0004D4BF
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0004F2C7 File Offset: 0x0004D4C7
		public int SortOrder { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0004F2D0 File Offset: 0x0004D4D0
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0004F2D8 File Offset: 0x0004D4D8
		public int DeckId { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0004F2E1 File Offset: 0x0004D4E1
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0004F2E9 File Offset: 0x0004D4E9
		public string DisplayTexture
		{
			get
			{
				return this._DisplayTexture;
			}
			set
			{
				this._DisplayTexture = value;
				this.HasDisplayTexture = (value != null);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0004F2FC File Offset: 0x0004D4FC
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x0004F304 File Offset: 0x0004D504
		public DeckDbRecord DeckRecord
		{
			get
			{
				return this._DeckRecord;
			}
			set
			{
				this._DeckRecord = value;
				this.HasDeckRecord = (value != null);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0004F317 File Offset: 0x0004D517
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x0004F31F File Offset: 0x0004D51F
		public bool IsFreeReward
		{
			get
			{
				return this._IsFreeReward;
			}
			set
			{
				this._IsFreeReward = value;
				this.HasIsFreeReward = true;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0004F32F File Offset: 0x0004D52F
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x0004F337 File Offset: 0x0004D537
		public bool IsStarterDeck
		{
			get
			{
				return this._IsStarterDeck;
			}
			set
			{
				this._IsStarterDeck = value;
				this.HasIsStarterDeck = true;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0004F347 File Offset: 0x0004D547
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x0004F34F File Offset: 0x0004D54F
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0004F35F File Offset: 0x0004D55F
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0004F367 File Offset: 0x0004D567
		public int DisplayCardId
		{
			get
			{
				return this._DisplayCardId;
			}
			set
			{
				this._DisplayCardId = value;
				this.HasDisplayCardId = true;
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0004F378 File Offset: 0x0004D578
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.ClassId.GetHashCode();
			if (this.HasEvent)
			{
				num ^= this.Event.GetHashCode();
			}
			num ^= this.SortOrder.GetHashCode();
			num ^= this.DeckId.GetHashCode();
			if (this.HasDisplayTexture)
			{
				num ^= this.DisplayTexture.GetHashCode();
			}
			if (this.HasDeckRecord)
			{
				num ^= this.DeckRecord.GetHashCode();
			}
			if (this.HasIsFreeReward)
			{
				num ^= this.IsFreeReward.GetHashCode();
			}
			if (this.HasIsStarterDeck)
			{
				num ^= this.IsStarterDeck.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasDisplayCardId)
			{
				num ^= this.DisplayCardId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0004F484 File Offset: 0x0004D684
		public override bool Equals(object obj)
		{
			DeckTemplateDbRecord deckTemplateDbRecord = obj as DeckTemplateDbRecord;
			return deckTemplateDbRecord != null && this.Id.Equals(deckTemplateDbRecord.Id) && this.ClassId.Equals(deckTemplateDbRecord.ClassId) && this.HasEvent == deckTemplateDbRecord.HasEvent && (!this.HasEvent || this.Event.Equals(deckTemplateDbRecord.Event)) && this.SortOrder.Equals(deckTemplateDbRecord.SortOrder) && this.DeckId.Equals(deckTemplateDbRecord.DeckId) && this.HasDisplayTexture == deckTemplateDbRecord.HasDisplayTexture && (!this.HasDisplayTexture || this.DisplayTexture.Equals(deckTemplateDbRecord.DisplayTexture)) && this.HasDeckRecord == deckTemplateDbRecord.HasDeckRecord && (!this.HasDeckRecord || this.DeckRecord.Equals(deckTemplateDbRecord.DeckRecord)) && this.HasIsFreeReward == deckTemplateDbRecord.HasIsFreeReward && (!this.HasIsFreeReward || this.IsFreeReward.Equals(deckTemplateDbRecord.IsFreeReward)) && this.HasIsStarterDeck == deckTemplateDbRecord.HasIsStarterDeck && (!this.HasIsStarterDeck || this.IsStarterDeck.Equals(deckTemplateDbRecord.IsStarterDeck)) && this.HasFormatType == deckTemplateDbRecord.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(deckTemplateDbRecord.FormatType)) && this.HasDisplayCardId == deckTemplateDbRecord.HasDisplayCardId && (!this.HasDisplayCardId || this.DisplayCardId.Equals(deckTemplateDbRecord.DisplayCardId));
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0004F642 File Offset: 0x0004D842
		public void Deserialize(Stream stream)
		{
			DeckTemplateDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0004F64C File Offset: 0x0004D84C
		public static DeckTemplateDbRecord Deserialize(Stream stream, DeckTemplateDbRecord instance)
		{
			return DeckTemplateDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0004F658 File Offset: 0x0004D858
		public static DeckTemplateDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckTemplateDbRecord deckTemplateDbRecord = new DeckTemplateDbRecord();
			DeckTemplateDbRecord.DeserializeLengthDelimited(stream, deckTemplateDbRecord);
			return deckTemplateDbRecord;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0004F674 File Offset: 0x0004D874
		public static DeckTemplateDbRecord DeserializeLengthDelimited(Stream stream, DeckTemplateDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckTemplateDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004F69C File Offset: 0x0004D89C
		public static DeckTemplateDbRecord Deserialize(Stream stream, DeckTemplateDbRecord instance, long limit)
		{
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.Id = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Event = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 32)
							{
								instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.DeckId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 64)
					{
						if (num == 50)
						{
							instance.DisplayTexture = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 58)
						{
							if (num == 64)
							{
								instance.IsFreeReward = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.DeckRecord == null)
							{
								instance.DeckRecord = DeckDbRecord.DeserializeLengthDelimited(stream);
								continue;
							}
							DeckDbRecord.DeserializeLengthDelimited(stream, instance.DeckRecord);
							continue;
						}
					}
					else
					{
						if (num == 72)
						{
							instance.IsStarterDeck = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.DisplayCardId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600170A RID: 5898 RVA: 0x0004F865 File Offset: 0x0004DA65
		public void Serialize(Stream stream)
		{
			DeckTemplateDbRecord.Serialize(stream, this);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004F870 File Offset: 0x0004DA70
		public static void Serialize(Stream stream, DeckTemplateDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
			if (instance.HasEvent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Event));
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SortOrder));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckId));
			if (instance.HasDisplayTexture)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayTexture));
			}
			if (instance.HasDeckRecord)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.DeckRecord.GetSerializedSize());
				DeckDbRecord.Serialize(stream, instance.DeckRecord);
			}
			if (instance.HasIsFreeReward)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsFreeReward);
			}
			if (instance.HasIsStarterDeck)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsStarterDeck);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasDisplayCardId)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DisplayCardId));
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0004F9BC File Offset: 0x0004DBBC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId));
			if (this.HasEvent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Event);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SortOrder));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckId));
			if (this.HasDisplayTexture)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DisplayTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasDeckRecord)
			{
				num += 1U;
				uint serializedSize = this.DeckRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasIsFreeReward)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsStarterDeck)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasDisplayCardId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DisplayCardId));
			}
			return num + 4U;
		}

		// Token: 0x04000714 RID: 1812
		public bool HasEvent;

		// Token: 0x04000715 RID: 1813
		private string _Event;

		// Token: 0x04000718 RID: 1816
		public bool HasDisplayTexture;

		// Token: 0x04000719 RID: 1817
		private string _DisplayTexture;

		// Token: 0x0400071A RID: 1818
		public bool HasDeckRecord;

		// Token: 0x0400071B RID: 1819
		private DeckDbRecord _DeckRecord;

		// Token: 0x0400071C RID: 1820
		public bool HasIsFreeReward;

		// Token: 0x0400071D RID: 1821
		private bool _IsFreeReward;

		// Token: 0x0400071E RID: 1822
		public bool HasIsStarterDeck;

		// Token: 0x0400071F RID: 1823
		private bool _IsStarterDeck;

		// Token: 0x04000720 RID: 1824
		public bool HasFormatType;

		// Token: 0x04000721 RID: 1825
		private FormatType _FormatType;

		// Token: 0x04000722 RID: 1826
		public bool HasDisplayCardId;

		// Token: 0x04000723 RID: 1827
		private int _DisplayCardId;
	}
}
