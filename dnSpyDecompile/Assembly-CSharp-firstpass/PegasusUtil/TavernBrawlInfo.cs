using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000BE RID: 190
	public class TavernBrawlInfo : IProtoBuf
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00030B03 File Offset: 0x0002ED03
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00030B0B File Offset: 0x0002ED0B
		public TavernBrawlSeasonSpec CurrentTavernBrawl
		{
			get
			{
				return this._CurrentTavernBrawl;
			}
			set
			{
				this._CurrentTavernBrawl = value;
				this.HasCurrentTavernBrawl = (value != null);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00030B1E File Offset: 0x0002ED1E
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00030B26 File Offset: 0x0002ED26
		public ulong NextStartSecondsFromNow
		{
			get
			{
				return this._NextStartSecondsFromNow;
			}
			set
			{
				this._NextStartSecondsFromNow = value;
				this.HasNextStartSecondsFromNow = true;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00030B36 File Offset: 0x0002ED36
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00030B3E File Offset: 0x0002ED3E
		public BrawlType BrawlType
		{
			get
			{
				return this._BrawlType;
			}
			set
			{
				this._BrawlType = value;
				this.HasBrawlType = true;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00030B4E File Offset: 0x0002ED4E
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00030B56 File Offset: 0x0002ED56
		public TavernBrawlPlayerRecord MyRecord
		{
			get
			{
				return this._MyRecord;
			}
			set
			{
				this._MyRecord = value;
				this.HasMyRecord = (value != null);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00030B69 File Offset: 0x0002ED69
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00030B71 File Offset: 0x0002ED71
		public string DeprecatedStoreInstructionPrefab
		{
			get
			{
				return this._DeprecatedStoreInstructionPrefab;
			}
			set
			{
				this._DeprecatedStoreInstructionPrefab = value;
				this.HasDeprecatedStoreInstructionPrefab = (value != null);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00030B84 File Offset: 0x0002ED84
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00030B8C File Offset: 0x0002ED8C
		public string DeprecatedStoreInstructionPrefabPhone
		{
			get
			{
				return this._DeprecatedStoreInstructionPrefabPhone;
			}
			set
			{
				this._DeprecatedStoreInstructionPrefabPhone = value;
				this.HasDeprecatedStoreInstructionPrefabPhone = (value != null);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00030BA0 File Offset: 0x0002EDA0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCurrentTavernBrawl)
			{
				num ^= this.CurrentTavernBrawl.GetHashCode();
			}
			if (this.HasNextStartSecondsFromNow)
			{
				num ^= this.NextStartSecondsFromNow.GetHashCode();
			}
			if (this.HasBrawlType)
			{
				num ^= this.BrawlType.GetHashCode();
			}
			if (this.HasMyRecord)
			{
				num ^= this.MyRecord.GetHashCode();
			}
			if (this.HasDeprecatedStoreInstructionPrefab)
			{
				num ^= this.DeprecatedStoreInstructionPrefab.GetHashCode();
			}
			if (this.HasDeprecatedStoreInstructionPrefabPhone)
			{
				num ^= this.DeprecatedStoreInstructionPrefabPhone.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public override bool Equals(object obj)
		{
			TavernBrawlInfo tavernBrawlInfo = obj as TavernBrawlInfo;
			return tavernBrawlInfo != null && this.HasCurrentTavernBrawl == tavernBrawlInfo.HasCurrentTavernBrawl && (!this.HasCurrentTavernBrawl || this.CurrentTavernBrawl.Equals(tavernBrawlInfo.CurrentTavernBrawl)) && this.HasNextStartSecondsFromNow == tavernBrawlInfo.HasNextStartSecondsFromNow && (!this.HasNextStartSecondsFromNow || this.NextStartSecondsFromNow.Equals(tavernBrawlInfo.NextStartSecondsFromNow)) && this.HasBrawlType == tavernBrawlInfo.HasBrawlType && (!this.HasBrawlType || this.BrawlType.Equals(tavernBrawlInfo.BrawlType)) && this.HasMyRecord == tavernBrawlInfo.HasMyRecord && (!this.HasMyRecord || this.MyRecord.Equals(tavernBrawlInfo.MyRecord)) && this.HasDeprecatedStoreInstructionPrefab == tavernBrawlInfo.HasDeprecatedStoreInstructionPrefab && (!this.HasDeprecatedStoreInstructionPrefab || this.DeprecatedStoreInstructionPrefab.Equals(tavernBrawlInfo.DeprecatedStoreInstructionPrefab)) && this.HasDeprecatedStoreInstructionPrefabPhone == tavernBrawlInfo.HasDeprecatedStoreInstructionPrefabPhone && (!this.HasDeprecatedStoreInstructionPrefabPhone || this.DeprecatedStoreInstructionPrefabPhone.Equals(tavernBrawlInfo.DeprecatedStoreInstructionPrefabPhone));
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00030D79 File Offset: 0x0002EF79
		public void Deserialize(Stream stream)
		{
			TavernBrawlInfo.Deserialize(stream, this);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00030D83 File Offset: 0x0002EF83
		public static TavernBrawlInfo Deserialize(Stream stream, TavernBrawlInfo instance)
		{
			return TavernBrawlInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00030D90 File Offset: 0x0002EF90
		public static TavernBrawlInfo DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlInfo tavernBrawlInfo = new TavernBrawlInfo();
			TavernBrawlInfo.DeserializeLengthDelimited(stream, tavernBrawlInfo);
			return tavernBrawlInfo;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00030DAC File Offset: 0x0002EFAC
		public static TavernBrawlInfo DeserializeLengthDelimited(Stream stream, TavernBrawlInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		public static TavernBrawlInfo Deserialize(Stream stream, TavernBrawlInfo instance, long limit)
		{
			instance.BrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.NextStartSecondsFromNow = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.CurrentTavernBrawl == null)
							{
								instance.CurrentTavernBrawl = TavernBrawlSeasonSpec.DeserializeLengthDelimited(stream);
								continue;
							}
							TavernBrawlSeasonSpec.DeserializeLengthDelimited(stream, instance.CurrentTavernBrawl);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 42)
						{
							instance.DeprecatedStoreInstructionPrefab = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.DeprecatedStoreInstructionPrefabPhone = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (instance.MyRecord == null)
						{
							instance.MyRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
							continue;
						}
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.MyRecord);
						continue;
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

		// Token: 0x06000D2B RID: 3371 RVA: 0x00030F1F File Offset: 0x0002F11F
		public void Serialize(Stream stream)
		{
			TavernBrawlInfo.Serialize(stream, this);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00030F28 File Offset: 0x0002F128
		public static void Serialize(Stream stream, TavernBrawlInfo instance)
		{
			if (instance.HasCurrentTavernBrawl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.CurrentTavernBrawl.GetSerializedSize());
				TavernBrawlSeasonSpec.Serialize(stream, instance.CurrentTavernBrawl);
			}
			if (instance.HasNextStartSecondsFromNow)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.NextStartSecondsFromNow);
			}
			if (instance.HasBrawlType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlType));
			}
			if (instance.HasMyRecord)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.MyRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.MyRecord);
			}
			if (instance.HasDeprecatedStoreInstructionPrefab)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefab));
			}
			if (instance.HasDeprecatedStoreInstructionPrefabPhone)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefabPhone));
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00031014 File Offset: 0x0002F214
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCurrentTavernBrawl)
			{
				num += 1U;
				uint serializedSize = this.CurrentTavernBrawl.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasNextStartSecondsFromNow)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.NextStartSecondsFromNow);
			}
			if (this.HasBrawlType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlType));
			}
			if (this.HasMyRecord)
			{
				num += 1U;
				uint serializedSize2 = this.MyRecord.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDeprecatedStoreInstructionPrefab)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeprecatedStoreInstructionPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeprecatedStoreInstructionPrefabPhone)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeprecatedStoreInstructionPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04000483 RID: 1155
		public bool HasCurrentTavernBrawl;

		// Token: 0x04000484 RID: 1156
		private TavernBrawlSeasonSpec _CurrentTavernBrawl;

		// Token: 0x04000485 RID: 1157
		public bool HasNextStartSecondsFromNow;

		// Token: 0x04000486 RID: 1158
		private ulong _NextStartSecondsFromNow;

		// Token: 0x04000487 RID: 1159
		public bool HasBrawlType;

		// Token: 0x04000488 RID: 1160
		private BrawlType _BrawlType;

		// Token: 0x04000489 RID: 1161
		public bool HasMyRecord;

		// Token: 0x0400048A RID: 1162
		private TavernBrawlPlayerRecord _MyRecord;

		// Token: 0x0400048B RID: 1163
		public bool HasDeprecatedStoreInstructionPrefab;

		// Token: 0x0400048C RID: 1164
		private string _DeprecatedStoreInstructionPrefab;

		// Token: 0x0400048D RID: 1165
		public bool HasDeprecatedStoreInstructionPrefabPhone;

		// Token: 0x0400048E RID: 1166
		private string _DeprecatedStoreInstructionPrefabPhone;

		// Token: 0x020005CD RID: 1485
		public enum PacketID
		{
			// Token: 0x04001FB3 RID: 8115
			ID = 316
		}
	}
}
