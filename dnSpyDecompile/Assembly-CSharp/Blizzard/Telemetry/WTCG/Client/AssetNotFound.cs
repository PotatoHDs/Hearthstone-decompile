using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001190 RID: 4496
	public class AssetNotFound : IProtoBuf
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x0600C606 RID: 50694 RVA: 0x003B9500 File Offset: 0x003B7700
		// (set) Token: 0x0600C607 RID: 50695 RVA: 0x003B9508 File Offset: 0x003B7708
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600C608 RID: 50696 RVA: 0x003B951B File Offset: 0x003B771B
		// (set) Token: 0x0600C609 RID: 50697 RVA: 0x003B9523 File Offset: 0x003B7723
		public string AssetType
		{
			get
			{
				return this._AssetType;
			}
			set
			{
				this._AssetType = value;
				this.HasAssetType = (value != null);
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600C60A RID: 50698 RVA: 0x003B9536 File Offset: 0x003B7736
		// (set) Token: 0x0600C60B RID: 50699 RVA: 0x003B953E File Offset: 0x003B773E
		public string AssetGuid
		{
			get
			{
				return this._AssetGuid;
			}
			set
			{
				this._AssetGuid = value;
				this.HasAssetGuid = (value != null);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x0600C60C RID: 50700 RVA: 0x003B9551 File Offset: 0x003B7751
		// (set) Token: 0x0600C60D RID: 50701 RVA: 0x003B9559 File Offset: 0x003B7759
		public string FilePath
		{
			get
			{
				return this._FilePath;
			}
			set
			{
				this._FilePath = value;
				this.HasFilePath = (value != null);
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x0600C60E RID: 50702 RVA: 0x003B956C File Offset: 0x003B776C
		// (set) Token: 0x0600C60F RID: 50703 RVA: 0x003B9574 File Offset: 0x003B7774
		public string LegacyName
		{
			get
			{
				return this._LegacyName;
			}
			set
			{
				this._LegacyName = value;
				this.HasLegacyName = (value != null);
			}
		}

		// Token: 0x0600C610 RID: 50704 RVA: 0x003B9588 File Offset: 0x003B7788
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasAssetType)
			{
				num ^= this.AssetType.GetHashCode();
			}
			if (this.HasAssetGuid)
			{
				num ^= this.AssetGuid.GetHashCode();
			}
			if (this.HasFilePath)
			{
				num ^= this.FilePath.GetHashCode();
			}
			if (this.HasLegacyName)
			{
				num ^= this.LegacyName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C611 RID: 50705 RVA: 0x003B9610 File Offset: 0x003B7810
		public override bool Equals(object obj)
		{
			AssetNotFound assetNotFound = obj as AssetNotFound;
			return assetNotFound != null && this.HasDeviceInfo == assetNotFound.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(assetNotFound.DeviceInfo)) && this.HasAssetType == assetNotFound.HasAssetType && (!this.HasAssetType || this.AssetType.Equals(assetNotFound.AssetType)) && this.HasAssetGuid == assetNotFound.HasAssetGuid && (!this.HasAssetGuid || this.AssetGuid.Equals(assetNotFound.AssetGuid)) && this.HasFilePath == assetNotFound.HasFilePath && (!this.HasFilePath || this.FilePath.Equals(assetNotFound.FilePath)) && this.HasLegacyName == assetNotFound.HasLegacyName && (!this.HasLegacyName || this.LegacyName.Equals(assetNotFound.LegacyName));
		}

		// Token: 0x0600C612 RID: 50706 RVA: 0x003B9701 File Offset: 0x003B7901
		public void Deserialize(Stream stream)
		{
			AssetNotFound.Deserialize(stream, this);
		}

		// Token: 0x0600C613 RID: 50707 RVA: 0x003B970B File Offset: 0x003B790B
		public static AssetNotFound Deserialize(Stream stream, AssetNotFound instance)
		{
			return AssetNotFound.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C614 RID: 50708 RVA: 0x003B9718 File Offset: 0x003B7918
		public static AssetNotFound DeserializeLengthDelimited(Stream stream)
		{
			AssetNotFound assetNotFound = new AssetNotFound();
			AssetNotFound.DeserializeLengthDelimited(stream, assetNotFound);
			return assetNotFound;
		}

		// Token: 0x0600C615 RID: 50709 RVA: 0x003B9734 File Offset: 0x003B7934
		public static AssetNotFound DeserializeLengthDelimited(Stream stream, AssetNotFound instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetNotFound.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C616 RID: 50710 RVA: 0x003B975C File Offset: 0x003B795C
		public static AssetNotFound Deserialize(Stream stream, AssetNotFound instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.AssetType = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.DeviceInfo == null)
							{
								instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.AssetGuid = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.FilePath = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.LegacyName = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C617 RID: 50711 RVA: 0x003B9860 File Offset: 0x003B7A60
		public void Serialize(Stream stream)
		{
			AssetNotFound.Serialize(stream, this);
		}

		// Token: 0x0600C618 RID: 50712 RVA: 0x003B986C File Offset: 0x003B7A6C
		public static void Serialize(Stream stream, AssetNotFound instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasAssetType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetType));
			}
			if (instance.HasAssetGuid)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetGuid));
			}
			if (instance.HasFilePath)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilePath));
			}
			if (instance.HasLegacyName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LegacyName));
			}
		}

		// Token: 0x0600C619 RID: 50713 RVA: 0x003B9940 File Offset: 0x003B7B40
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAssetType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AssetType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAssetGuid)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AssetGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFilePath)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.FilePath);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasLegacyName)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.LegacyName);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x04009DD1 RID: 40401
		public bool HasDeviceInfo;

		// Token: 0x04009DD2 RID: 40402
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009DD3 RID: 40403
		public bool HasAssetType;

		// Token: 0x04009DD4 RID: 40404
		private string _AssetType;

		// Token: 0x04009DD5 RID: 40405
		public bool HasAssetGuid;

		// Token: 0x04009DD6 RID: 40406
		private string _AssetGuid;

		// Token: 0x04009DD7 RID: 40407
		public bool HasFilePath;

		// Token: 0x04009DD8 RID: 40408
		private string _FilePath;

		// Token: 0x04009DD9 RID: 40409
		public bool HasLegacyName;

		// Token: 0x04009DDA RID: 40410
		private string _LegacyName;
	}
}
