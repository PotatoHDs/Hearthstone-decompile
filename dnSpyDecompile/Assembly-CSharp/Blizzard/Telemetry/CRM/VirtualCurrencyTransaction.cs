using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x0200117B RID: 4475
	public class VirtualCurrencyTransaction : IProtoBuf
	{
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x0600C497 RID: 50327 RVA: 0x003B4B2C File Offset: 0x003B2D2C
		// (set) Token: 0x0600C498 RID: 50328 RVA: 0x003B4B34 File Offset: 0x003B2D34
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600C499 RID: 50329 RVA: 0x003B4B47 File Offset: 0x003B2D47
		// (set) Token: 0x0600C49A RID: 50330 RVA: 0x003B4B4F File Offset: 0x003B2D4F
		public string ItemId
		{
			get
			{
				return this._ItemId;
			}
			set
			{
				this._ItemId = value;
				this.HasItemId = (value != null);
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600C49B RID: 50331 RVA: 0x003B4B62 File Offset: 0x003B2D62
		// (set) Token: 0x0600C49C RID: 50332 RVA: 0x003B4B6A File Offset: 0x003B2D6A
		public string ItemCost
		{
			get
			{
				return this._ItemCost;
			}
			set
			{
				this._ItemCost = value;
				this.HasItemCost = (value != null);
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600C49D RID: 50333 RVA: 0x003B4B7D File Offset: 0x003B2D7D
		// (set) Token: 0x0600C49E RID: 50334 RVA: 0x003B4B85 File Offset: 0x003B2D85
		public string ItemQuantity
		{
			get
			{
				return this._ItemQuantity;
			}
			set
			{
				this._ItemQuantity = value;
				this.HasItemQuantity = (value != null);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600C49F RID: 50335 RVA: 0x003B4B98 File Offset: 0x003B2D98
		// (set) Token: 0x0600C4A0 RID: 50336 RVA: 0x003B4BA0 File Offset: 0x003B2DA0
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				this._Currency = value;
				this.HasCurrency = (value != null);
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600C4A1 RID: 50337 RVA: 0x003B4BB3 File Offset: 0x003B2DB3
		// (set) Token: 0x0600C4A2 RID: 50338 RVA: 0x003B4BBB File Offset: 0x003B2DBB
		public string Payload
		{
			get
			{
				return this._Payload;
			}
			set
			{
				this._Payload = value;
				this.HasPayload = (value != null);
			}
		}

		// Token: 0x0600C4A3 RID: 50339 RVA: 0x003B4BD0 File Offset: 0x003B2DD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasItemId)
			{
				num ^= this.ItemId.GetHashCode();
			}
			if (this.HasItemCost)
			{
				num ^= this.ItemCost.GetHashCode();
			}
			if (this.HasItemQuantity)
			{
				num ^= this.ItemQuantity.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			if (this.HasPayload)
			{
				num ^= this.Payload.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4A4 RID: 50340 RVA: 0x003B4C70 File Offset: 0x003B2E70
		public override bool Equals(object obj)
		{
			VirtualCurrencyTransaction virtualCurrencyTransaction = obj as VirtualCurrencyTransaction;
			return virtualCurrencyTransaction != null && this.HasApplicationId == virtualCurrencyTransaction.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(virtualCurrencyTransaction.ApplicationId)) && this.HasItemId == virtualCurrencyTransaction.HasItemId && (!this.HasItemId || this.ItemId.Equals(virtualCurrencyTransaction.ItemId)) && this.HasItemCost == virtualCurrencyTransaction.HasItemCost && (!this.HasItemCost || this.ItemCost.Equals(virtualCurrencyTransaction.ItemCost)) && this.HasItemQuantity == virtualCurrencyTransaction.HasItemQuantity && (!this.HasItemQuantity || this.ItemQuantity.Equals(virtualCurrencyTransaction.ItemQuantity)) && this.HasCurrency == virtualCurrencyTransaction.HasCurrency && (!this.HasCurrency || this.Currency.Equals(virtualCurrencyTransaction.Currency)) && this.HasPayload == virtualCurrencyTransaction.HasPayload && (!this.HasPayload || this.Payload.Equals(virtualCurrencyTransaction.Payload));
		}

		// Token: 0x0600C4A5 RID: 50341 RVA: 0x003B4D8C File Offset: 0x003B2F8C
		public void Deserialize(Stream stream)
		{
			VirtualCurrencyTransaction.Deserialize(stream, this);
		}

		// Token: 0x0600C4A6 RID: 50342 RVA: 0x003B4D96 File Offset: 0x003B2F96
		public static VirtualCurrencyTransaction Deserialize(Stream stream, VirtualCurrencyTransaction instance)
		{
			return VirtualCurrencyTransaction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C4A7 RID: 50343 RVA: 0x003B4DA4 File Offset: 0x003B2FA4
		public static VirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream)
		{
			VirtualCurrencyTransaction virtualCurrencyTransaction = new VirtualCurrencyTransaction();
			VirtualCurrencyTransaction.DeserializeLengthDelimited(stream, virtualCurrencyTransaction);
			return virtualCurrencyTransaction;
		}

		// Token: 0x0600C4A8 RID: 50344 RVA: 0x003B4DC0 File Offset: 0x003B2FC0
		public static VirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream, VirtualCurrencyTransaction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VirtualCurrencyTransaction.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C4A9 RID: 50345 RVA: 0x003B4DE8 File Offset: 0x003B2FE8
		public static VirtualCurrencyTransaction Deserialize(Stream stream, VirtualCurrencyTransaction instance, long limit)
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
				else if (num == 82)
				{
					instance.ApplicationId = ProtocolParser.ReadString(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field <= 30U)
					{
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						if (field != 20U)
						{
							if (field == 30U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.ItemCost = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.ItemId = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else if (field != 40U)
					{
						if (field != 50U)
						{
							if (field == 60U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.Payload = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.Currency = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemQuantity = ProtocolParser.ReadString(stream);
							continue;
						}
						continue;
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

		// Token: 0x0600C4AA RID: 50346 RVA: 0x003B4F2D File Offset: 0x003B312D
		public void Serialize(Stream stream)
		{
			VirtualCurrencyTransaction.Serialize(stream, this);
		}

		// Token: 0x0600C4AB RID: 50347 RVA: 0x003B4F38 File Offset: 0x003B3138
		public static void Serialize(Stream stream, VirtualCurrencyTransaction instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasItemId)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemId));
			}
			if (instance.HasItemCost)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemCost));
			}
			if (instance.HasItemQuantity)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemQuantity));
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Payload));
			}
		}

		// Token: 0x0600C4AC RID: 50348 RVA: 0x003B505C File Offset: 0x003B325C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasItemId)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ItemId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasItemCost)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ItemCost);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasItemQuantity)
			{
				num += 2U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ItemQuantity);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasCurrency)
			{
				num += 2U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasPayload)
			{
				num += 2U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.Payload);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			return num;
		}

		// Token: 0x04009D49 RID: 40265
		public bool HasApplicationId;

		// Token: 0x04009D4A RID: 40266
		private string _ApplicationId;

		// Token: 0x04009D4B RID: 40267
		public bool HasItemId;

		// Token: 0x04009D4C RID: 40268
		private string _ItemId;

		// Token: 0x04009D4D RID: 40269
		public bool HasItemCost;

		// Token: 0x04009D4E RID: 40270
		private string _ItemCost;

		// Token: 0x04009D4F RID: 40271
		public bool HasItemQuantity;

		// Token: 0x04009D50 RID: 40272
		private string _ItemQuantity;

		// Token: 0x04009D51 RID: 40273
		public bool HasCurrency;

		// Token: 0x04009D52 RID: 40274
		private string _Currency;

		// Token: 0x04009D53 RID: 40275
		public bool HasPayload;

		// Token: 0x04009D54 RID: 40276
		private string _Payload;
	}
}
