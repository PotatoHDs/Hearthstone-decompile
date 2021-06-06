using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000140 RID: 320
	public class ProfileNoticeSellableDeckGranted : IProtoBuf
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00047AFF File Offset: 0x00045CFF
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x00047B07 File Offset: 0x00045D07
		public int SellableDeckId
		{
			get
			{
				return this._SellableDeckId;
			}
			set
			{
				this._SellableDeckId = value;
				this.HasSellableDeckId = true;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00047B17 File Offset: 0x00045D17
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x00047B1F File Offset: 0x00045D1F
		public bool WasDeckGranted
		{
			get
			{
				return this._WasDeckGranted;
			}
			set
			{
				this._WasDeckGranted = value;
				this.HasWasDeckGranted = true;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00047B2F File Offset: 0x00045D2F
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00047B37 File Offset: 0x00045D37
		public long PlayerDeckId
		{
			get
			{
				return this._PlayerDeckId;
			}
			set
			{
				this._PlayerDeckId = value;
				this.HasPlayerDeckId = true;
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00047B48 File Offset: 0x00045D48
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSellableDeckId)
			{
				num ^= this.SellableDeckId.GetHashCode();
			}
			if (this.HasWasDeckGranted)
			{
				num ^= this.WasDeckGranted.GetHashCode();
			}
			if (this.HasPlayerDeckId)
			{
				num ^= this.PlayerDeckId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00047BB0 File Offset: 0x00045DB0
		public override bool Equals(object obj)
		{
			ProfileNoticeSellableDeckGranted profileNoticeSellableDeckGranted = obj as ProfileNoticeSellableDeckGranted;
			return profileNoticeSellableDeckGranted != null && this.HasSellableDeckId == profileNoticeSellableDeckGranted.HasSellableDeckId && (!this.HasSellableDeckId || this.SellableDeckId.Equals(profileNoticeSellableDeckGranted.SellableDeckId)) && this.HasWasDeckGranted == profileNoticeSellableDeckGranted.HasWasDeckGranted && (!this.HasWasDeckGranted || this.WasDeckGranted.Equals(profileNoticeSellableDeckGranted.WasDeckGranted)) && this.HasPlayerDeckId == profileNoticeSellableDeckGranted.HasPlayerDeckId && (!this.HasPlayerDeckId || this.PlayerDeckId.Equals(profileNoticeSellableDeckGranted.PlayerDeckId));
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00047C54 File Offset: 0x00045E54
		public void Deserialize(Stream stream)
		{
			ProfileNoticeSellableDeckGranted.Deserialize(stream, this);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00047C5E File Offset: 0x00045E5E
		public static ProfileNoticeSellableDeckGranted Deserialize(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			return ProfileNoticeSellableDeckGranted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00047C6C File Offset: 0x00045E6C
		public static ProfileNoticeSellableDeckGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeSellableDeckGranted profileNoticeSellableDeckGranted = new ProfileNoticeSellableDeckGranted();
			ProfileNoticeSellableDeckGranted.DeserializeLengthDelimited(stream, profileNoticeSellableDeckGranted);
			return profileNoticeSellableDeckGranted;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00047C88 File Offset: 0x00045E88
		public static ProfileNoticeSellableDeckGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeSellableDeckGranted.Deserialize(stream, instance, num);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00047CB0 File Offset: 0x00045EB0
		public static ProfileNoticeSellableDeckGranted Deserialize(Stream stream, ProfileNoticeSellableDeckGranted instance, long limit)
		{
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
					if (num != 16)
					{
						if (num != 24)
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
							instance.PlayerDeckId = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.WasDeckGranted = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.SellableDeckId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00047D5E File Offset: 0x00045F5E
		public void Serialize(Stream stream)
		{
			ProfileNoticeSellableDeckGranted.Serialize(stream, this);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00047D68 File Offset: 0x00045F68
		public static void Serialize(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			if (instance.HasSellableDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SellableDeckId));
			}
			if (instance.HasWasDeckGranted)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.WasDeckGranted);
			}
			if (instance.HasPlayerDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerDeckId);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00047DCC File Offset: 0x00045FCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSellableDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SellableDeckId));
			}
			if (this.HasWasDeckGranted)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPlayerDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerDeckId);
			}
			return num;
		}

		// Token: 0x04000660 RID: 1632
		public bool HasSellableDeckId;

		// Token: 0x04000661 RID: 1633
		private int _SellableDeckId;

		// Token: 0x04000662 RID: 1634
		public bool HasWasDeckGranted;

		// Token: 0x04000663 RID: 1635
		private bool _WasDeckGranted;

		// Token: 0x04000664 RID: 1636
		public bool HasPlayerDeckId;

		// Token: 0x04000665 RID: 1637
		private long _PlayerDeckId;

		// Token: 0x02000636 RID: 1590
		public enum NoticeID
		{
			// Token: 0x040020CD RID: 8397
			ID = 28
		}
	}
}
