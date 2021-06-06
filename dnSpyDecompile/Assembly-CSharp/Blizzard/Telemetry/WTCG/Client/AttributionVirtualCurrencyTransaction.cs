using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119E RID: 4510
	public class AttributionVirtualCurrencyTransaction : IProtoBuf
	{
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600C744 RID: 51012 RVA: 0x003BE84D File Offset: 0x003BCA4D
		// (set) Token: 0x0600C745 RID: 51013 RVA: 0x003BE855 File Offset: 0x003BCA55
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

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600C746 RID: 51014 RVA: 0x003BE868 File Offset: 0x003BCA68
		// (set) Token: 0x0600C747 RID: 51015 RVA: 0x003BE870 File Offset: 0x003BCA70
		public string DeviceType
		{
			get
			{
				return this._DeviceType;
			}
			set
			{
				this._DeviceType = value;
				this.HasDeviceType = (value != null);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600C748 RID: 51016 RVA: 0x003BE883 File Offset: 0x003BCA83
		// (set) Token: 0x0600C749 RID: 51017 RVA: 0x003BE88B File Offset: 0x003BCA8B
		public ulong FirstInstallDate
		{
			get
			{
				return this._FirstInstallDate;
			}
			set
			{
				this._FirstInstallDate = value;
				this.HasFirstInstallDate = true;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600C74A RID: 51018 RVA: 0x003BE89B File Offset: 0x003BCA9B
		// (set) Token: 0x0600C74B RID: 51019 RVA: 0x003BE8A3 File Offset: 0x003BCAA3
		public string BundleId
		{
			get
			{
				return this._BundleId;
			}
			set
			{
				this._BundleId = value;
				this.HasBundleId = (value != null);
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600C74C RID: 51020 RVA: 0x003BE8B6 File Offset: 0x003BCAB6
		// (set) Token: 0x0600C74D RID: 51021 RVA: 0x003BE8BE File Offset: 0x003BCABE
		public float Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
				this.HasAmount = true;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600C74E RID: 51022 RVA: 0x003BE8CE File Offset: 0x003BCACE
		// (set) Token: 0x0600C74F RID: 51023 RVA: 0x003BE8D6 File Offset: 0x003BCAD6
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

		// Token: 0x0600C750 RID: 51024 RVA: 0x003BE8EC File Offset: 0x003BCAEC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasDeviceType)
			{
				num ^= this.DeviceType.GetHashCode();
			}
			if (this.HasFirstInstallDate)
			{
				num ^= this.FirstInstallDate.GetHashCode();
			}
			if (this.HasBundleId)
			{
				num ^= this.BundleId.GetHashCode();
			}
			if (this.HasAmount)
			{
				num ^= this.Amount.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C751 RID: 51025 RVA: 0x003BE990 File Offset: 0x003BCB90
		public override bool Equals(object obj)
		{
			AttributionVirtualCurrencyTransaction attributionVirtualCurrencyTransaction = obj as AttributionVirtualCurrencyTransaction;
			return attributionVirtualCurrencyTransaction != null && this.HasApplicationId == attributionVirtualCurrencyTransaction.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionVirtualCurrencyTransaction.ApplicationId)) && this.HasDeviceType == attributionVirtualCurrencyTransaction.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionVirtualCurrencyTransaction.DeviceType)) && this.HasFirstInstallDate == attributionVirtualCurrencyTransaction.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionVirtualCurrencyTransaction.FirstInstallDate)) && this.HasBundleId == attributionVirtualCurrencyTransaction.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionVirtualCurrencyTransaction.BundleId)) && this.HasAmount == attributionVirtualCurrencyTransaction.HasAmount && (!this.HasAmount || this.Amount.Equals(attributionVirtualCurrencyTransaction.Amount)) && this.HasCurrency == attributionVirtualCurrencyTransaction.HasCurrency && (!this.HasCurrency || this.Currency.Equals(attributionVirtualCurrencyTransaction.Currency));
		}

		// Token: 0x0600C752 RID: 51026 RVA: 0x003BEAB2 File Offset: 0x003BCCB2
		public void Deserialize(Stream stream)
		{
			AttributionVirtualCurrencyTransaction.Deserialize(stream, this);
		}

		// Token: 0x0600C753 RID: 51027 RVA: 0x003BEABC File Offset: 0x003BCCBC
		public static AttributionVirtualCurrencyTransaction Deserialize(Stream stream, AttributionVirtualCurrencyTransaction instance)
		{
			return AttributionVirtualCurrencyTransaction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C754 RID: 51028 RVA: 0x003BEAC8 File Offset: 0x003BCCC8
		public static AttributionVirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream)
		{
			AttributionVirtualCurrencyTransaction attributionVirtualCurrencyTransaction = new AttributionVirtualCurrencyTransaction();
			AttributionVirtualCurrencyTransaction.DeserializeLengthDelimited(stream, attributionVirtualCurrencyTransaction);
			return attributionVirtualCurrencyTransaction;
		}

		// Token: 0x0600C755 RID: 51029 RVA: 0x003BEAE4 File Offset: 0x003BCCE4
		public static AttributionVirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream, AttributionVirtualCurrencyTransaction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionVirtualCurrencyTransaction.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C756 RID: 51030 RVA: 0x003BEB0C File Offset: 0x003BCD0C
		public static AttributionVirtualCurrencyTransaction Deserialize(Stream stream, AttributionVirtualCurrencyTransaction instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 13)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						switch (field)
						{
						case 100U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.ApplicationId = ProtocolParser.ReadString(stream);
							}
							break;
						case 101U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.DeviceType = ProtocolParser.ReadString(stream);
							}
							break;
						case 102U:
							if (key.WireType == Wire.Varint)
							{
								instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
							}
							break;
						case 103U:
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.BundleId = ProtocolParser.ReadString(stream);
							}
							break;
						default:
							ProtocolParser.SkipKey(stream, key);
							break;
						}
					}
					else
					{
						instance.Currency = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Amount = binaryReader.ReadSingle();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C757 RID: 51031 RVA: 0x003BEC3F File Offset: 0x003BCE3F
		public void Serialize(Stream stream)
		{
			AttributionVirtualCurrencyTransaction.Serialize(stream, this);
		}

		// Token: 0x0600C758 RID: 51032 RVA: 0x003BEC48 File Offset: 0x003BCE48
		public static void Serialize(Stream stream, AttributionVirtualCurrencyTransaction instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Amount);
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
		}

		// Token: 0x0600C759 RID: 51033 RVA: 0x003BED54 File Offset: 0x003BCF54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceType)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFirstInstallDate)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.FirstInstallDate);
			}
			if (this.HasBundleId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasAmount)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x04009E74 RID: 40564
		public bool HasApplicationId;

		// Token: 0x04009E75 RID: 40565
		private string _ApplicationId;

		// Token: 0x04009E76 RID: 40566
		public bool HasDeviceType;

		// Token: 0x04009E77 RID: 40567
		private string _DeviceType;

		// Token: 0x04009E78 RID: 40568
		public bool HasFirstInstallDate;

		// Token: 0x04009E79 RID: 40569
		private ulong _FirstInstallDate;

		// Token: 0x04009E7A RID: 40570
		public bool HasBundleId;

		// Token: 0x04009E7B RID: 40571
		private string _BundleId;

		// Token: 0x04009E7C RID: 40572
		public bool HasAmount;

		// Token: 0x04009E7D RID: 40573
		private float _Amount;

		// Token: 0x04009E7E RID: 40574
		public bool HasCurrency;

		// Token: 0x04009E7F RID: 40575
		private string _Currency;
	}
}
