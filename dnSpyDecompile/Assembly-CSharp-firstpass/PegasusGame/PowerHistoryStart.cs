using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001C3 RID: 451
	public class PowerHistoryStart : IProtoBuf
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0006506E File Offset: 0x0006326E
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x00065076 File Offset: 0x00063276
		public HistoryBlock.Type Type { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0006507F File Offset: 0x0006327F
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x00065087 File Offset: 0x00063287
		public int SubOption { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00065090 File Offset: 0x00063290
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x00065098 File Offset: 0x00063298
		public int Source { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000650A1 File Offset: 0x000632A1
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x000650A9 File Offset: 0x000632A9
		public int Target { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000650B2 File Offset: 0x000632B2
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x000650BA File Offset: 0x000632BA
		public string EffectCardId
		{
			get
			{
				return this._EffectCardId;
			}
			set
			{
				this._EffectCardId = value;
				this.HasEffectCardId = (value != null);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x000650CD File Offset: 0x000632CD
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x000650D5 File Offset: 0x000632D5
		public int EffectIndex
		{
			get
			{
				return this._EffectIndex;
			}
			set
			{
				this._EffectIndex = value;
				this.HasEffectIndex = true;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x000650E5 File Offset: 0x000632E5
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x000650ED File Offset: 0x000632ED
		public int TriggerKeyword
		{
			get
			{
				return this._TriggerKeyword;
			}
			set
			{
				this._TriggerKeyword = value;
				this.HasTriggerKeyword = true;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x000650FD File Offset: 0x000632FD
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x00065105 File Offset: 0x00063305
		public bool ShowInHistory
		{
			get
			{
				return this._ShowInHistory;
			}
			set
			{
				this._ShowInHistory = value;
				this.HasShowInHistory = true;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00065115 File Offset: 0x00063315
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0006511D File Offset: 0x0006331D
		public bool IsDeferrable
		{
			get
			{
				return this._IsDeferrable;
			}
			set
			{
				this._IsDeferrable = value;
				this.HasIsDeferrable = true;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0006512D File Offset: 0x0006332D
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x00065135 File Offset: 0x00063335
		public bool IsBatchable
		{
			get
			{
				return this._IsBatchable;
			}
			set
			{
				this._IsBatchable = value;
				this.HasIsBatchable = true;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00065145 File Offset: 0x00063345
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0006514D File Offset: 0x0006334D
		public bool IsDeferBlocker
		{
			get
			{
				return this._IsDeferBlocker;
			}
			set
			{
				this._IsDeferBlocker = value;
				this.HasIsDeferBlocker = true;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0006515D File Offset: 0x0006335D
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00065165 File Offset: 0x00063365
		public bool ForceShowBigCard
		{
			get
			{
				return this._ForceShowBigCard;
			}
			set
			{
				this._ForceShowBigCard = value;
				this.HasForceShowBigCard = true;
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00065178 File Offset: 0x00063378
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			num ^= this.SubOption.GetHashCode();
			num ^= this.Source.GetHashCode();
			num ^= this.Target.GetHashCode();
			if (this.HasEffectCardId)
			{
				num ^= this.EffectCardId.GetHashCode();
			}
			if (this.HasEffectIndex)
			{
				num ^= this.EffectIndex.GetHashCode();
			}
			if (this.HasTriggerKeyword)
			{
				num ^= this.TriggerKeyword.GetHashCode();
			}
			if (this.HasShowInHistory)
			{
				num ^= this.ShowInHistory.GetHashCode();
			}
			if (this.HasIsDeferrable)
			{
				num ^= this.IsDeferrable.GetHashCode();
			}
			if (this.HasIsBatchable)
			{
				num ^= this.IsBatchable.GetHashCode();
			}
			if (this.HasIsDeferBlocker)
			{
				num ^= this.IsDeferBlocker.GetHashCode();
			}
			if (this.HasForceShowBigCard)
			{
				num ^= this.ForceShowBigCard.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000652A4 File Offset: 0x000634A4
		public override bool Equals(object obj)
		{
			PowerHistoryStart powerHistoryStart = obj as PowerHistoryStart;
			return powerHistoryStart != null && this.Type.Equals(powerHistoryStart.Type) && this.SubOption.Equals(powerHistoryStart.SubOption) && this.Source.Equals(powerHistoryStart.Source) && this.Target.Equals(powerHistoryStart.Target) && this.HasEffectCardId == powerHistoryStart.HasEffectCardId && (!this.HasEffectCardId || this.EffectCardId.Equals(powerHistoryStart.EffectCardId)) && this.HasEffectIndex == powerHistoryStart.HasEffectIndex && (!this.HasEffectIndex || this.EffectIndex.Equals(powerHistoryStart.EffectIndex)) && this.HasTriggerKeyword == powerHistoryStart.HasTriggerKeyword && (!this.HasTriggerKeyword || this.TriggerKeyword.Equals(powerHistoryStart.TriggerKeyword)) && this.HasShowInHistory == powerHistoryStart.HasShowInHistory && (!this.HasShowInHistory || this.ShowInHistory.Equals(powerHistoryStart.ShowInHistory)) && this.HasIsDeferrable == powerHistoryStart.HasIsDeferrable && (!this.HasIsDeferrable || this.IsDeferrable.Equals(powerHistoryStart.IsDeferrable)) && this.HasIsBatchable == powerHistoryStart.HasIsBatchable && (!this.HasIsBatchable || this.IsBatchable.Equals(powerHistoryStart.IsBatchable)) && this.HasIsDeferBlocker == powerHistoryStart.HasIsDeferBlocker && (!this.HasIsDeferBlocker || this.IsDeferBlocker.Equals(powerHistoryStart.IsDeferBlocker)) && this.HasForceShowBigCard == powerHistoryStart.HasForceShowBigCard && (!this.HasForceShowBigCard || this.ForceShowBigCard.Equals(powerHistoryStart.ForceShowBigCard));
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00065496 File Offset: 0x00063696
		public void Deserialize(Stream stream)
		{
			PowerHistoryStart.Deserialize(stream, this);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000654A0 File Offset: 0x000636A0
		public static PowerHistoryStart Deserialize(Stream stream, PowerHistoryStart instance)
		{
			return PowerHistoryStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000654AC File Offset: 0x000636AC
		public static PowerHistoryStart DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryStart powerHistoryStart = new PowerHistoryStart();
			PowerHistoryStart.DeserializeLengthDelimited(stream, powerHistoryStart);
			return powerHistoryStart;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000654C8 File Offset: 0x000636C8
		public static PowerHistoryStart DeserializeLengthDelimited(Stream stream, PowerHistoryStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryStart.Deserialize(stream, instance, num);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000654F0 File Offset: 0x000636F0
		public static PowerHistoryStart Deserialize(Stream stream, PowerHistoryStart instance, long limit)
		{
			instance.EffectIndex = 0;
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
					if (num <= 48)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.Type = (HistoryBlock.Type)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.SubOption = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.Source = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 32)
							{
								instance.Target = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 42)
							{
								instance.EffectCardId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 48)
							{
								instance.EffectIndex = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 72)
					{
						if (num == 56)
						{
							instance.TriggerKeyword = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.ShowInHistory = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 72)
						{
							instance.IsDeferrable = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 80)
						{
							instance.IsBatchable = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 88)
						{
							instance.IsDeferBlocker = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 96)
						{
							instance.ForceShowBigCard = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000656BB File Offset: 0x000638BB
		public void Serialize(Stream stream)
		{
			PowerHistoryStart.Serialize(stream, this);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000656C4 File Offset: 0x000638C4
		public static void Serialize(Stream stream, PowerHistoryStart instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubOption));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Target));
			if (instance.HasEffectCardId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EffectCardId));
			}
			if (instance.HasEffectIndex)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EffectIndex));
			}
			if (instance.HasTriggerKeyword)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TriggerKeyword));
			}
			if (instance.HasShowInHistory)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.ShowInHistory);
			}
			if (instance.HasIsDeferrable)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsDeferrable);
			}
			if (instance.HasIsBatchable)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.IsBatchable);
			}
			if (instance.HasIsDeferBlocker)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.IsDeferBlocker);
			}
			if (instance.HasForceShowBigCard)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.ForceShowBigCard);
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00065810 File Offset: 0x00063A10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubOption));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Target));
			if (this.HasEffectCardId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EffectCardId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasEffectIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EffectIndex));
			}
			if (this.HasTriggerKeyword)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TriggerKeyword));
			}
			if (this.HasShowInHistory)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsDeferrable)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsBatchable)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsDeferBlocker)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasForceShowBigCard)
			{
				num += 1U;
				num += 1U;
			}
			return num + 4U;
		}

		// Token: 0x04000A6C RID: 2668
		public bool HasEffectCardId;

		// Token: 0x04000A6D RID: 2669
		private string _EffectCardId;

		// Token: 0x04000A6E RID: 2670
		public bool HasEffectIndex;

		// Token: 0x04000A6F RID: 2671
		private int _EffectIndex;

		// Token: 0x04000A70 RID: 2672
		public bool HasTriggerKeyword;

		// Token: 0x04000A71 RID: 2673
		private int _TriggerKeyword;

		// Token: 0x04000A72 RID: 2674
		public bool HasShowInHistory;

		// Token: 0x04000A73 RID: 2675
		private bool _ShowInHistory;

		// Token: 0x04000A74 RID: 2676
		public bool HasIsDeferrable;

		// Token: 0x04000A75 RID: 2677
		private bool _IsDeferrable;

		// Token: 0x04000A76 RID: 2678
		public bool HasIsBatchable;

		// Token: 0x04000A77 RID: 2679
		private bool _IsBatchable;

		// Token: 0x04000A78 RID: 2680
		public bool HasIsDeferBlocker;

		// Token: 0x04000A79 RID: 2681
		private bool _IsDeferBlocker;

		// Token: 0x04000A7A RID: 2682
		public bool HasForceShowBigCard;

		// Token: 0x04000A7B RID: 2683
		private bool _ForceShowBigCard;
	}
}
