using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000054 RID: 84
	public class CreateDeck : IProtoBuf
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00015CE3 File Offset: 0x00013EE3
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00015CEB File Offset: 0x00013EEB
		public string Name { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00015CF4 File Offset: 0x00013EF4
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00015CFC File Offset: 0x00013EFC
		public int Hero { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00015D05 File Offset: 0x00013F05
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00015D0D File Offset: 0x00013F0D
		public int HeroPremium { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00015D16 File Offset: 0x00013F16
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00015D1E File Offset: 0x00013F1E
		public DeckType DeckType { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00015D27 File Offset: 0x00013F27
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00015D2F File Offset: 0x00013F2F
		public bool TaggedStandard
		{
			get
			{
				return this._TaggedStandard;
			}
			set
			{
				this._TaggedStandard = value;
				this.HasTaggedStandard = true;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00015D3F File Offset: 0x00013F3F
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00015D47 File Offset: 0x00013F47
		public long SortOrder { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00015D50 File Offset: 0x00013F50
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00015D58 File Offset: 0x00013F58
		public DeckSourceType SourceType
		{
			get
			{
				return this._SourceType;
			}
			set
			{
				this._SourceType = value;
				this.HasSourceType = true;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00015D68 File Offset: 0x00013F68
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00015D70 File Offset: 0x00013F70
		public string PastedDeckHash
		{
			get
			{
				return this._PastedDeckHash;
			}
			set
			{
				this._PastedDeckHash = value;
				this.HasPastedDeckHash = (value != null);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00015D83 File Offset: 0x00013F83
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00015D8B File Offset: 0x00013F8B
		public int BrawlLibraryItemId
		{
			get
			{
				return this._BrawlLibraryItemId;
			}
			set
			{
				this._BrawlLibraryItemId = value;
				this.HasBrawlLibraryItemId = true;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00015D9B File Offset: 0x00013F9B
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00015DA3 File Offset: 0x00013FA3
		public int RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00015DB3 File Offset: 0x00013FB3
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00015DBB File Offset: 0x00013FBB
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00015DCB File Offset: 0x00013FCB
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00015DD3 File Offset: 0x00013FD3
		public long FsgId
		{
			get
			{
				return this._FsgId;
			}
			set
			{
				this._FsgId = value;
				this.HasFsgId = true;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00015DE3 File Offset: 0x00013FE3
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00015DEB File Offset: 0x00013FEB
		public byte[] FsgSharedSecretKey
		{
			get
			{
				return this._FsgSharedSecretKey;
			}
			set
			{
				this._FsgSharedSecretKey = value;
				this.HasFsgSharedSecretKey = (value != null);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00015E00 File Offset: 0x00014000
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Name.GetHashCode();
			num ^= this.Hero.GetHashCode();
			num ^= this.HeroPremium.GetHashCode();
			num ^= this.DeckType.GetHashCode();
			if (this.HasTaggedStandard)
			{
				num ^= this.TaggedStandard.GetHashCode();
			}
			num ^= this.SortOrder.GetHashCode();
			if (this.HasSourceType)
			{
				num ^= this.SourceType.GetHashCode();
			}
			if (this.HasPastedDeckHash)
			{
				num ^= this.PastedDeckHash.GetHashCode();
			}
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasFsgId)
			{
				num ^= this.FsgId.GetHashCode();
			}
			if (this.HasFsgSharedSecretKey)
			{
				num ^= this.FsgSharedSecretKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00015F44 File Offset: 0x00014144
		public override bool Equals(object obj)
		{
			CreateDeck createDeck = obj as CreateDeck;
			return createDeck != null && this.Name.Equals(createDeck.Name) && this.Hero.Equals(createDeck.Hero) && this.HeroPremium.Equals(createDeck.HeroPremium) && this.DeckType.Equals(createDeck.DeckType) && this.HasTaggedStandard == createDeck.HasTaggedStandard && (!this.HasTaggedStandard || this.TaggedStandard.Equals(createDeck.TaggedStandard)) && this.SortOrder.Equals(createDeck.SortOrder) && this.HasSourceType == createDeck.HasSourceType && (!this.HasSourceType || this.SourceType.Equals(createDeck.SourceType)) && this.HasPastedDeckHash == createDeck.HasPastedDeckHash && (!this.HasPastedDeckHash || this.PastedDeckHash.Equals(createDeck.PastedDeckHash)) && this.HasBrawlLibraryItemId == createDeck.HasBrawlLibraryItemId && (!this.HasBrawlLibraryItemId || this.BrawlLibraryItemId.Equals(createDeck.BrawlLibraryItemId)) && this.HasRequestId == createDeck.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(createDeck.RequestId)) && this.HasFormatType == createDeck.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(createDeck.FormatType)) && this.HasFsgId == createDeck.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(createDeck.FsgId)) && this.HasFsgSharedSecretKey == createDeck.HasFsgSharedSecretKey && (!this.HasFsgSharedSecretKey || this.FsgSharedSecretKey.Equals(createDeck.FsgSharedSecretKey));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00016162 File Offset: 0x00014362
		public void Deserialize(Stream stream)
		{
			CreateDeck.Deserialize(stream, this);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001616C File Offset: 0x0001436C
		public static CreateDeck Deserialize(Stream stream, CreateDeck instance)
		{
			return CreateDeck.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00016178 File Offset: 0x00014378
		public static CreateDeck DeserializeLengthDelimited(Stream stream)
		{
			CreateDeck createDeck = new CreateDeck();
			CreateDeck.DeserializeLengthDelimited(stream, createDeck);
			return createDeck;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00016194 File Offset: 0x00014394
		public static CreateDeck DeserializeLengthDelimited(Stream stream, CreateDeck instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateDeck.Deserialize(stream, instance, num);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000161BC File Offset: 0x000143BC
		public static CreateDeck Deserialize(Stream stream, CreateDeck instance, long limit)
		{
			instance.SourceType = DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN;
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
							if (num == 10)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.HeroPremium = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.TaggedStandard = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 66)
					{
						if (num == 48)
						{
							instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.SourceType = (DeckSourceType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 66)
						{
							instance.PastedDeckHash = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 72)
						{
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 80)
						{
							instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
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
						if (field != 101U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
						}
					}
					else if (key.WireType == Wire.Varint)
					{
						instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000163B9 File Offset: 0x000145B9
		public void Serialize(Stream stream)
		{
			CreateDeck.Serialize(stream, this);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000163C4 File Offset: 0x000145C4
		public static void Serialize(Stream stream, CreateDeck instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Hero));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroPremium));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckType));
			if (instance.HasTaggedStandard)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.TaggedStandard);
			}
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			if (instance.HasSourceType)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SourceType));
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestId));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001655C File Offset: 0x0001475C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Hero));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroPremium));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckType));
			if (this.HasTaggedStandard)
			{
				num += 1U;
				num += 1U;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)this.SortOrder);
			if (this.HasSourceType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SourceType));
			}
			if (this.HasPastedDeckHash)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.PastedDeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasBrawlLibraryItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			if (this.HasRequestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestId));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasFsgId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			}
			if (this.HasFsgSharedSecretKey)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.FsgSharedSecretKey.Length) + (uint)this.FsgSharedSecretKey.Length;
			}
			return num + 5U;
		}

		// Token: 0x040001EF RID: 495
		public bool HasTaggedStandard;

		// Token: 0x040001F0 RID: 496
		private bool _TaggedStandard;

		// Token: 0x040001F2 RID: 498
		public bool HasSourceType;

		// Token: 0x040001F3 RID: 499
		private DeckSourceType _SourceType;

		// Token: 0x040001F4 RID: 500
		public bool HasPastedDeckHash;

		// Token: 0x040001F5 RID: 501
		private string _PastedDeckHash;

		// Token: 0x040001F6 RID: 502
		public bool HasBrawlLibraryItemId;

		// Token: 0x040001F7 RID: 503
		private int _BrawlLibraryItemId;

		// Token: 0x040001F8 RID: 504
		public bool HasRequestId;

		// Token: 0x040001F9 RID: 505
		private int _RequestId;

		// Token: 0x040001FA RID: 506
		public bool HasFormatType;

		// Token: 0x040001FB RID: 507
		private FormatType _FormatType;

		// Token: 0x040001FC RID: 508
		public bool HasFsgId;

		// Token: 0x040001FD RID: 509
		private long _FsgId;

		// Token: 0x040001FE RID: 510
		public bool HasFsgSharedSecretKey;

		// Token: 0x040001FF RID: 511
		private byte[] _FsgSharedSecretKey;

		// Token: 0x02000566 RID: 1382
		public enum PacketID
		{
			// Token: 0x04001E8A RID: 7818
			ID = 209,
			// Token: 0x04001E8B RID: 7819
			System = 0
		}
	}
}
