using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119D RID: 4509
	public class AttributionScenarioResult : IProtoBuf
	{
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600C729 RID: 50985 RVA: 0x003BE0BE File Offset: 0x003BC2BE
		// (set) Token: 0x0600C72A RID: 50986 RVA: 0x003BE0C6 File Offset: 0x003BC2C6
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

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600C72B RID: 50987 RVA: 0x003BE0D9 File Offset: 0x003BC2D9
		// (set) Token: 0x0600C72C RID: 50988 RVA: 0x003BE0E1 File Offset: 0x003BC2E1
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

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600C72D RID: 50989 RVA: 0x003BE0F4 File Offset: 0x003BC2F4
		// (set) Token: 0x0600C72E RID: 50990 RVA: 0x003BE0FC File Offset: 0x003BC2FC
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

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600C72F RID: 50991 RVA: 0x003BE10C File Offset: 0x003BC30C
		// (set) Token: 0x0600C730 RID: 50992 RVA: 0x003BE114 File Offset: 0x003BC314
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

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600C731 RID: 50993 RVA: 0x003BE127 File Offset: 0x003BC327
		// (set) Token: 0x0600C732 RID: 50994 RVA: 0x003BE12F File Offset: 0x003BC32F
		public int ScenarioId
		{
			get
			{
				return this._ScenarioId;
			}
			set
			{
				this._ScenarioId = value;
				this.HasScenarioId = true;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600C733 RID: 50995 RVA: 0x003BE13F File Offset: 0x003BC33F
		// (set) Token: 0x0600C734 RID: 50996 RVA: 0x003BE147 File Offset: 0x003BC347
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

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600C735 RID: 50997 RVA: 0x003BE15A File Offset: 0x003BC35A
		// (set) Token: 0x0600C736 RID: 50998 RVA: 0x003BE162 File Offset: 0x003BC362
		public int BossId
		{
			get
			{
				return this._BossId;
			}
			set
			{
				this._BossId = value;
				this.HasBossId = true;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600C737 RID: 50999 RVA: 0x003BE172 File Offset: 0x003BC372
		// (set) Token: 0x0600C738 RID: 51000 RVA: 0x003BE17A File Offset: 0x003BC37A
		public IdentifierInfo Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				this._Identifier = value;
				this.HasIdentifier = (value != null);
			}
		}

		// Token: 0x0600C739 RID: 51001 RVA: 0x003BE190 File Offset: 0x003BC390
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
			if (this.HasScenarioId)
			{
				num ^= this.ScenarioId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasBossId)
			{
				num ^= this.BossId.GetHashCode();
			}
			if (this.HasIdentifier)
			{
				num ^= this.Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C73A RID: 51002 RVA: 0x003BE264 File Offset: 0x003BC464
		public override bool Equals(object obj)
		{
			AttributionScenarioResult attributionScenarioResult = obj as AttributionScenarioResult;
			return attributionScenarioResult != null && this.HasApplicationId == attributionScenarioResult.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionScenarioResult.ApplicationId)) && this.HasDeviceType == attributionScenarioResult.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionScenarioResult.DeviceType)) && this.HasFirstInstallDate == attributionScenarioResult.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionScenarioResult.FirstInstallDate)) && this.HasBundleId == attributionScenarioResult.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionScenarioResult.BundleId)) && this.HasScenarioId == attributionScenarioResult.HasScenarioId && (!this.HasScenarioId || this.ScenarioId.Equals(attributionScenarioResult.ScenarioId)) && this.HasResult == attributionScenarioResult.HasResult && (!this.HasResult || this.Result.Equals(attributionScenarioResult.Result)) && this.HasBossId == attributionScenarioResult.HasBossId && (!this.HasBossId || this.BossId.Equals(attributionScenarioResult.BossId)) && this.HasIdentifier == attributionScenarioResult.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionScenarioResult.Identifier));
		}

		// Token: 0x0600C73B RID: 51003 RVA: 0x003BE3DF File Offset: 0x003BC5DF
		public void Deserialize(Stream stream)
		{
			AttributionScenarioResult.Deserialize(stream, this);
		}

		// Token: 0x0600C73C RID: 51004 RVA: 0x003BE3E9 File Offset: 0x003BC5E9
		public static AttributionScenarioResult Deserialize(Stream stream, AttributionScenarioResult instance)
		{
			return AttributionScenarioResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C73D RID: 51005 RVA: 0x003BE3F4 File Offset: 0x003BC5F4
		public static AttributionScenarioResult DeserializeLengthDelimited(Stream stream)
		{
			AttributionScenarioResult attributionScenarioResult = new AttributionScenarioResult();
			AttributionScenarioResult.DeserializeLengthDelimited(stream, attributionScenarioResult);
			return attributionScenarioResult;
		}

		// Token: 0x0600C73E RID: 51006 RVA: 0x003BE410 File Offset: 0x003BC610
		public static AttributionScenarioResult DeserializeLengthDelimited(Stream stream, AttributionScenarioResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionScenarioResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C73F RID: 51007 RVA: 0x003BE438 File Offset: 0x003BC638
		public static AttributionScenarioResult Deserialize(Stream stream, AttributionScenarioResult instance, long limit)
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
					if (num != 18)
					{
						if (num != 24)
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
								if (field != 1000U)
								{
									ProtocolParser.SkipKey(stream, key);
								}
								else if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.Identifier == null)
									{
										instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
									}
									else
									{
										IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
									}
								}
								break;
							}
						}
						else
						{
							instance.BossId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Result = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C740 RID: 51008 RVA: 0x003BE5BD File Offset: 0x003BC7BD
		public void Serialize(Stream stream)
		{
			AttributionScenarioResult.Serialize(stream, this);
		}

		// Token: 0x0600C741 RID: 51009 RVA: 0x003BE5C8 File Offset: 0x003BC7C8
		public static void Serialize(Stream stream, AttributionScenarioResult instance)
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
			if (instance.HasScenarioId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			}
			if (instance.HasResult)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Result));
			}
			if (instance.HasBossId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BossId));
			}
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		// Token: 0x0600C742 RID: 51010 RVA: 0x003BE724 File Offset: 0x003BC924
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
			if (this.HasScenarioId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId));
			}
			if (this.HasResult)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Result);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasBossId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BossId));
			}
			if (this.HasIdentifier)
			{
				num += 2U;
				uint serializedSize = this.Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009E64 RID: 40548
		public bool HasApplicationId;

		// Token: 0x04009E65 RID: 40549
		private string _ApplicationId;

		// Token: 0x04009E66 RID: 40550
		public bool HasDeviceType;

		// Token: 0x04009E67 RID: 40551
		private string _DeviceType;

		// Token: 0x04009E68 RID: 40552
		public bool HasFirstInstallDate;

		// Token: 0x04009E69 RID: 40553
		private ulong _FirstInstallDate;

		// Token: 0x04009E6A RID: 40554
		public bool HasBundleId;

		// Token: 0x04009E6B RID: 40555
		private string _BundleId;

		// Token: 0x04009E6C RID: 40556
		public bool HasScenarioId;

		// Token: 0x04009E6D RID: 40557
		private int _ScenarioId;

		// Token: 0x04009E6E RID: 40558
		public bool HasResult;

		// Token: 0x04009E6F RID: 40559
		private string _Result;

		// Token: 0x04009E70 RID: 40560
		public bool HasBossId;

		// Token: 0x04009E71 RID: 40561
		private int _BossId;

		// Token: 0x04009E72 RID: 40562
		public bool HasIdentifier;

		// Token: 0x04009E73 RID: 40563
		private IdentifierInfo _Identifier;
	}
}
