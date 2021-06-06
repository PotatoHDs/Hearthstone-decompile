using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001195 RID: 4501
	public class AttributionGameRoundEnd : IProtoBuf
	{
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x0600C66F RID: 50799 RVA: 0x003BAE98 File Offset: 0x003B9098
		// (set) Token: 0x0600C670 RID: 50800 RVA: 0x003BAEA0 File Offset: 0x003B90A0
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

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x0600C671 RID: 50801 RVA: 0x003BAEB3 File Offset: 0x003B90B3
		// (set) Token: 0x0600C672 RID: 50802 RVA: 0x003BAEBB File Offset: 0x003B90BB
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

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x0600C673 RID: 50803 RVA: 0x003BAECE File Offset: 0x003B90CE
		// (set) Token: 0x0600C674 RID: 50804 RVA: 0x003BAED6 File Offset: 0x003B90D6
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

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600C675 RID: 50805 RVA: 0x003BAEE6 File Offset: 0x003B90E6
		// (set) Token: 0x0600C676 RID: 50806 RVA: 0x003BAEEE File Offset: 0x003B90EE
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

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x0600C677 RID: 50807 RVA: 0x003BAF01 File Offset: 0x003B9101
		// (set) Token: 0x0600C678 RID: 50808 RVA: 0x003BAF09 File Offset: 0x003B9109
		public string GameMode
		{
			get
			{
				return this._GameMode;
			}
			set
			{
				this._GameMode = value;
				this.HasGameMode = (value != null);
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x0600C679 RID: 50809 RVA: 0x003BAF1C File Offset: 0x003B911C
		// (set) Token: 0x0600C67A RID: 50810 RVA: 0x003BAF24 File Offset: 0x003B9124
		public string Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = (value != null);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x0600C67B RID: 50811 RVA: 0x003BAF37 File Offset: 0x003B9137
		// (set) Token: 0x0600C67C RID: 50812 RVA: 0x003BAF3F File Offset: 0x003B913F
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

		// Token: 0x0600C67D RID: 50813 RVA: 0x003BAF50 File Offset: 0x003B9150
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
			if (this.HasGameMode)
			{
				num ^= this.GameMode.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C67E RID: 50814 RVA: 0x003BB010 File Offset: 0x003B9210
		public override bool Equals(object obj)
		{
			AttributionGameRoundEnd attributionGameRoundEnd = obj as AttributionGameRoundEnd;
			return attributionGameRoundEnd != null && this.HasApplicationId == attributionGameRoundEnd.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionGameRoundEnd.ApplicationId)) && this.HasDeviceType == attributionGameRoundEnd.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionGameRoundEnd.DeviceType)) && this.HasFirstInstallDate == attributionGameRoundEnd.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionGameRoundEnd.FirstInstallDate)) && this.HasBundleId == attributionGameRoundEnd.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionGameRoundEnd.BundleId)) && this.HasGameMode == attributionGameRoundEnd.HasGameMode && (!this.HasGameMode || this.GameMode.Equals(attributionGameRoundEnd.GameMode)) && this.HasResult == attributionGameRoundEnd.HasResult && (!this.HasResult || this.Result.Equals(attributionGameRoundEnd.Result)) && this.HasFormatType == attributionGameRoundEnd.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(attributionGameRoundEnd.FormatType));
		}

		// Token: 0x0600C67F RID: 50815 RVA: 0x003BB168 File Offset: 0x003B9368
		public void Deserialize(Stream stream)
		{
			AttributionGameRoundEnd.Deserialize(stream, this);
		}

		// Token: 0x0600C680 RID: 50816 RVA: 0x003BB172 File Offset: 0x003B9372
		public static AttributionGameRoundEnd Deserialize(Stream stream, AttributionGameRoundEnd instance)
		{
			return AttributionGameRoundEnd.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C681 RID: 50817 RVA: 0x003BB180 File Offset: 0x003B9380
		public static AttributionGameRoundEnd DeserializeLengthDelimited(Stream stream)
		{
			AttributionGameRoundEnd attributionGameRoundEnd = new AttributionGameRoundEnd();
			AttributionGameRoundEnd.DeserializeLengthDelimited(stream, attributionGameRoundEnd);
			return attributionGameRoundEnd;
		}

		// Token: 0x0600C682 RID: 50818 RVA: 0x003BB19C File Offset: 0x003B939C
		public static AttributionGameRoundEnd DeserializeLengthDelimited(Stream stream, AttributionGameRoundEnd instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionGameRoundEnd.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C683 RID: 50819 RVA: 0x003BB1C4 File Offset: 0x003B93C4
		public static AttributionGameRoundEnd Deserialize(Stream stream, AttributionGameRoundEnd instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 40)
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
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Result = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.GameMode = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C684 RID: 50820 RVA: 0x003BB30B File Offset: 0x003B950B
		public void Serialize(Stream stream)
		{
			AttributionGameRoundEnd.Serialize(stream, this);
		}

		// Token: 0x0600C685 RID: 50821 RVA: 0x003BB314 File Offset: 0x003B9514
		public static void Serialize(Stream stream, AttributionGameRoundEnd instance)
		{
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
			if (instance.HasGameMode)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameMode));
			}
			if (instance.HasResult)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Result));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x0600C686 RID: 50822 RVA: 0x003BB440 File Offset: 0x003B9640
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
			if (this.HasGameMode)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.GameMode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasResult)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Result);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num;
		}

		// Token: 0x04009E03 RID: 40451
		public bool HasApplicationId;

		// Token: 0x04009E04 RID: 40452
		private string _ApplicationId;

		// Token: 0x04009E05 RID: 40453
		public bool HasDeviceType;

		// Token: 0x04009E06 RID: 40454
		private string _DeviceType;

		// Token: 0x04009E07 RID: 40455
		public bool HasFirstInstallDate;

		// Token: 0x04009E08 RID: 40456
		private ulong _FirstInstallDate;

		// Token: 0x04009E09 RID: 40457
		public bool HasBundleId;

		// Token: 0x04009E0A RID: 40458
		private string _BundleId;

		// Token: 0x04009E0B RID: 40459
		public bool HasGameMode;

		// Token: 0x04009E0C RID: 40460
		private string _GameMode;

		// Token: 0x04009E0D RID: 40461
		public bool HasResult;

		// Token: 0x04009E0E RID: 40462
		private string _Result;

		// Token: 0x04009E0F RID: 40463
		public bool HasFormatType;

		// Token: 0x04009E10 RID: 40464
		private FormatType _FormatType;
	}
}
