using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000048 RID: 72
	public class GoldCostBooster : IProtoBuf
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00013164 File Offset: 0x00011364
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001316C File Offset: 0x0001136C
		public long Cost { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00013175 File Offset: 0x00011375
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001317D File Offset: 0x0001137D
		public int PackType { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00013186 File Offset: 0x00011386
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0001318E File Offset: 0x0001138E
		public string BuyWithGoldEventName
		{
			get
			{
				return this._BuyWithGoldEventName;
			}
			set
			{
				this._BuyWithGoldEventName = value;
				this.HasBuyWithGoldEventName = (value != null);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000131A4 File Offset: 0x000113A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Cost.GetHashCode();
			num ^= this.PackType.GetHashCode();
			if (this.HasBuyWithGoldEventName)
			{
				num ^= this.BuyWithGoldEventName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000131F8 File Offset: 0x000113F8
		public override bool Equals(object obj)
		{
			GoldCostBooster goldCostBooster = obj as GoldCostBooster;
			return goldCostBooster != null && this.Cost.Equals(goldCostBooster.Cost) && this.PackType.Equals(goldCostBooster.PackType) && this.HasBuyWithGoldEventName == goldCostBooster.HasBuyWithGoldEventName && (!this.HasBuyWithGoldEventName || this.BuyWithGoldEventName.Equals(goldCostBooster.BuyWithGoldEventName));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001326D File Offset: 0x0001146D
		public void Deserialize(Stream stream)
		{
			GoldCostBooster.Deserialize(stream, this);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013277 File Offset: 0x00011477
		public static GoldCostBooster Deserialize(Stream stream, GoldCostBooster instance)
		{
			return GoldCostBooster.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00013284 File Offset: 0x00011484
		public static GoldCostBooster DeserializeLengthDelimited(Stream stream)
		{
			GoldCostBooster goldCostBooster = new GoldCostBooster();
			GoldCostBooster.DeserializeLengthDelimited(stream, goldCostBooster);
			return goldCostBooster;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000132A0 File Offset: 0x000114A0
		public static GoldCostBooster DeserializeLengthDelimited(Stream stream, GoldCostBooster instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GoldCostBooster.Deserialize(stream, instance, num);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000132C8 File Offset: 0x000114C8
		public static GoldCostBooster Deserialize(Stream stream, GoldCostBooster instance, long limit)
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
						if (num != 26)
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
							instance.BuyWithGoldEventName = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.PackType = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Cost = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013376 File Offset: 0x00011576
		public void Serialize(Stream stream)
		{
			GoldCostBooster.Serialize(stream, this);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013380 File Offset: 0x00011580
		public static void Serialize(Stream stream, GoldCostBooster instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Cost);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PackType));
			if (instance.HasBuyWithGoldEventName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BuyWithGoldEventName));
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000133DC File Offset: 0x000115DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Cost);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PackType));
			if (this.HasBuyWithGoldEventName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BuyWithGoldEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 2U;
		}

		// Token: 0x040001AF RID: 431
		public bool HasBuyWithGoldEventName;

		// Token: 0x040001B0 RID: 432
		private string _BuyWithGoldEventName;
	}
}
