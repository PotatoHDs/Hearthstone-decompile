using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200008D RID: 141
	public class DraftBeginning : IProtoBuf
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x000225FA File Offset: 0x000207FA
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00022602 File Offset: 0x00020802
		public long DeckId { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0002260B File Offset: 0x0002080B
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x00022613 File Offset: 0x00020813
		public List<CardDef> ChoiceList
		{
			get
			{
				return this._ChoiceList;
			}
			set
			{
				this._ChoiceList = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0002261C File Offset: 0x0002081C
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00022624 File Offset: 0x00020824
		public int DeprecatedWins
		{
			get
			{
				return this._DeprecatedWins;
			}
			set
			{
				this._DeprecatedWins = value;
				this.HasDeprecatedWins = true;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00022634 File Offset: 0x00020834
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0002263C File Offset: 0x0002083C
		public int MaxSlot { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00022645 File Offset: 0x00020845
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0002264D File Offset: 0x0002084D
		public ArenaSession CurrentSession
		{
			get
			{
				return this._CurrentSession;
			}
			set
			{
				this._CurrentSession = value;
				this.HasCurrentSession = (value != null);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00022660 File Offset: 0x00020860
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00022668 File Offset: 0x00020868
		public DraftSlotType SlotType { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00022671 File Offset: 0x00020871
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00022679 File Offset: 0x00020879
		public List<DraftSlotType> UniqueSlotTypes
		{
			get
			{
				return this._UniqueSlotTypes;
			}
			set
			{
				this._UniqueSlotTypes = value;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00022684 File Offset: 0x00020884
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.DeckId.GetHashCode();
			foreach (CardDef cardDef in this.ChoiceList)
			{
				num ^= cardDef.GetHashCode();
			}
			if (this.HasDeprecatedWins)
			{
				num ^= this.DeprecatedWins.GetHashCode();
			}
			num ^= this.MaxSlot.GetHashCode();
			if (this.HasCurrentSession)
			{
				num ^= this.CurrentSession.GetHashCode();
			}
			num ^= this.SlotType.GetHashCode();
			foreach (DraftSlotType draftSlotType in this.UniqueSlotTypes)
			{
				num ^= draftSlotType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000227A0 File Offset: 0x000209A0
		public override bool Equals(object obj)
		{
			DraftBeginning draftBeginning = obj as DraftBeginning;
			if (draftBeginning == null)
			{
				return false;
			}
			if (!this.DeckId.Equals(draftBeginning.DeckId))
			{
				return false;
			}
			if (this.ChoiceList.Count != draftBeginning.ChoiceList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ChoiceList.Count; i++)
			{
				if (!this.ChoiceList[i].Equals(draftBeginning.ChoiceList[i]))
				{
					return false;
				}
			}
			if (this.HasDeprecatedWins != draftBeginning.HasDeprecatedWins || (this.HasDeprecatedWins && !this.DeprecatedWins.Equals(draftBeginning.DeprecatedWins)))
			{
				return false;
			}
			if (!this.MaxSlot.Equals(draftBeginning.MaxSlot))
			{
				return false;
			}
			if (this.HasCurrentSession != draftBeginning.HasCurrentSession || (this.HasCurrentSession && !this.CurrentSession.Equals(draftBeginning.CurrentSession)))
			{
				return false;
			}
			if (!this.SlotType.Equals(draftBeginning.SlotType))
			{
				return false;
			}
			if (this.UniqueSlotTypes.Count != draftBeginning.UniqueSlotTypes.Count)
			{
				return false;
			}
			for (int j = 0; j < this.UniqueSlotTypes.Count; j++)
			{
				if (!this.UniqueSlotTypes[j].Equals(draftBeginning.UniqueSlotTypes[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002291E File Offset: 0x00020B1E
		public void Deserialize(Stream stream)
		{
			DraftBeginning.Deserialize(stream, this);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00022928 File Offset: 0x00020B28
		public static DraftBeginning Deserialize(Stream stream, DraftBeginning instance)
		{
			return DraftBeginning.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00022934 File Offset: 0x00020B34
		public static DraftBeginning DeserializeLengthDelimited(Stream stream)
		{
			DraftBeginning draftBeginning = new DraftBeginning();
			DraftBeginning.DeserializeLengthDelimited(stream, draftBeginning);
			return draftBeginning;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00022950 File Offset: 0x00020B50
		public static DraftBeginning DeserializeLengthDelimited(Stream stream, DraftBeginning instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftBeginning.Deserialize(stream, instance, num);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00022978 File Offset: 0x00020B78
		public static DraftBeginning Deserialize(Stream stream, DraftBeginning instance, long limit)
		{
			if (instance.ChoiceList == null)
			{
				instance.ChoiceList = new List<CardDef>();
			}
			if (instance.UniqueSlotTypes == null)
			{
				instance.UniqueSlotTypes = new List<DraftSlotType>();
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
					if (num <= 32)
					{
						if (num == 8)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 26)
						{
							instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.DeprecatedWins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 50)
					{
						if (num == 40)
						{
							instance.MaxSlot = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							if (instance.CurrentSession == null)
							{
								instance.CurrentSession = ArenaSession.DeserializeLengthDelimited(stream);
								continue;
							}
							ArenaSession.DeserializeLengthDelimited(stream, instance.CurrentSession);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.UniqueSlotTypes.Add((DraftSlotType)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x06000974 RID: 2420 RVA: 0x00022AF4 File Offset: 0x00020CF4
		public void Serialize(Stream stream)
		{
			DraftBeginning.Serialize(stream, this);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00022B00 File Offset: 0x00020D00
		public static void Serialize(Stream stream, DraftBeginning instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in instance.ChoiceList)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
					CardDef.Serialize(stream, cardDef);
				}
			}
			if (instance.HasDeprecatedWins)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedWins));
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxSlot));
			if (instance.HasCurrentSession)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSession.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.CurrentSession);
			}
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SlotType));
			if (instance.UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType draftSlotType in instance.UniqueSlotTypes)
				{
					stream.WriteByte(64);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)draftSlotType));
				}
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00022C58 File Offset: 0x00020E58
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			if (this.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in this.ChoiceList)
				{
					num += 1U;
					uint serializedSize = cardDef.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDeprecatedWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedWins));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxSlot));
			if (this.HasCurrentSession)
			{
				num += 1U;
				uint serializedSize2 = this.CurrentSession.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SlotType));
			if (this.UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType draftSlotType in this.UniqueSlotTypes)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)draftSlotType));
				}
			}
			num += 3U;
			return num;
		}

		// Token: 0x0400033F RID: 831
		private List<CardDef> _ChoiceList = new List<CardDef>();

		// Token: 0x04000340 RID: 832
		public bool HasDeprecatedWins;

		// Token: 0x04000341 RID: 833
		private int _DeprecatedWins;

		// Token: 0x04000343 RID: 835
		public bool HasCurrentSession;

		// Token: 0x04000344 RID: 836
		private ArenaSession _CurrentSession;

		// Token: 0x04000346 RID: 838
		private List<DraftSlotType> _UniqueSlotTypes = new List<DraftSlotType>();

		// Token: 0x020005A1 RID: 1441
		public enum PacketID
		{
			// Token: 0x04001F47 RID: 8007
			ID = 246
		}
	}
}
