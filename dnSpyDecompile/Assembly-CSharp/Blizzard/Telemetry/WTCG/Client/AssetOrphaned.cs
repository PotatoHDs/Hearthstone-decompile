using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001191 RID: 4497
	public class AssetOrphaned : IProtoBuf
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x0600C61B RID: 50715 RVA: 0x003B9A19 File Offset: 0x003B7C19
		// (set) Token: 0x0600C61C RID: 50716 RVA: 0x003B9A21 File Offset: 0x003B7C21
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

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x0600C61D RID: 50717 RVA: 0x003B9A34 File Offset: 0x003B7C34
		// (set) Token: 0x0600C61E RID: 50718 RVA: 0x003B9A3C File Offset: 0x003B7C3C
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

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600C61F RID: 50719 RVA: 0x003B9A4F File Offset: 0x003B7C4F
		// (set) Token: 0x0600C620 RID: 50720 RVA: 0x003B9A57 File Offset: 0x003B7C57
		public string HandleOwner
		{
			get
			{
				return this._HandleOwner;
			}
			set
			{
				this._HandleOwner = value;
				this.HasHandleOwner = (value != null);
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x0600C621 RID: 50721 RVA: 0x003B9A6A File Offset: 0x003B7C6A
		// (set) Token: 0x0600C622 RID: 50722 RVA: 0x003B9A72 File Offset: 0x003B7C72
		public string HandleType
		{
			get
			{
				return this._HandleType;
			}
			set
			{
				this._HandleType = value;
				this.HasHandleType = (value != null);
			}
		}

		// Token: 0x0600C623 RID: 50723 RVA: 0x003B9A88 File Offset: 0x003B7C88
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasFilePath)
			{
				num ^= this.FilePath.GetHashCode();
			}
			if (this.HasHandleOwner)
			{
				num ^= this.HandleOwner.GetHashCode();
			}
			if (this.HasHandleType)
			{
				num ^= this.HandleType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C624 RID: 50724 RVA: 0x003B9AFC File Offset: 0x003B7CFC
		public override bool Equals(object obj)
		{
			AssetOrphaned assetOrphaned = obj as AssetOrphaned;
			return assetOrphaned != null && this.HasDeviceInfo == assetOrphaned.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(assetOrphaned.DeviceInfo)) && this.HasFilePath == assetOrphaned.HasFilePath && (!this.HasFilePath || this.FilePath.Equals(assetOrphaned.FilePath)) && this.HasHandleOwner == assetOrphaned.HasHandleOwner && (!this.HasHandleOwner || this.HandleOwner.Equals(assetOrphaned.HandleOwner)) && this.HasHandleType == assetOrphaned.HasHandleType && (!this.HasHandleType || this.HandleType.Equals(assetOrphaned.HandleType));
		}

		// Token: 0x0600C625 RID: 50725 RVA: 0x003B9BC2 File Offset: 0x003B7DC2
		public void Deserialize(Stream stream)
		{
			AssetOrphaned.Deserialize(stream, this);
		}

		// Token: 0x0600C626 RID: 50726 RVA: 0x003B9BCC File Offset: 0x003B7DCC
		public static AssetOrphaned Deserialize(Stream stream, AssetOrphaned instance)
		{
			return AssetOrphaned.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C627 RID: 50727 RVA: 0x003B9BD8 File Offset: 0x003B7DD8
		public static AssetOrphaned DeserializeLengthDelimited(Stream stream)
		{
			AssetOrphaned assetOrphaned = new AssetOrphaned();
			AssetOrphaned.DeserializeLengthDelimited(stream, assetOrphaned);
			return assetOrphaned;
		}

		// Token: 0x0600C628 RID: 50728 RVA: 0x003B9BF4 File Offset: 0x003B7DF4
		public static AssetOrphaned DeserializeLengthDelimited(Stream stream, AssetOrphaned instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetOrphaned.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C629 RID: 50729 RVA: 0x003B9C1C File Offset: 0x003B7E1C
		public static AssetOrphaned Deserialize(Stream stream, AssetOrphaned instance, long limit)
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
								instance.FilePath = ProtocolParser.ReadString(stream);
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
							instance.HandleOwner = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.HandleType = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C62A RID: 50730 RVA: 0x003B9D07 File Offset: 0x003B7F07
		public void Serialize(Stream stream)
		{
			AssetOrphaned.Serialize(stream, this);
		}

		// Token: 0x0600C62B RID: 50731 RVA: 0x003B9D10 File Offset: 0x003B7F10
		public static void Serialize(Stream stream, AssetOrphaned instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasFilePath)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilePath));
			}
			if (instance.HasHandleOwner)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HandleOwner));
			}
			if (instance.HasHandleType)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HandleType));
			}
		}

		// Token: 0x0600C62C RID: 50732 RVA: 0x003B9DBC File Offset: 0x003B7FBC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFilePath)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FilePath);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasHandleOwner)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.HandleOwner);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasHandleType)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.HandleType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009DDB RID: 40411
		public bool HasDeviceInfo;

		// Token: 0x04009DDC RID: 40412
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009DDD RID: 40413
		public bool HasFilePath;

		// Token: 0x04009DDE RID: 40414
		private string _FilePath;

		// Token: 0x04009DDF RID: 40415
		public bool HasHandleOwner;

		// Token: 0x04009DE0 RID: 40416
		private string _HandleOwner;

		// Token: 0x04009DE1 RID: 40417
		public bool HasHandleType;

		// Token: 0x04009DE2 RID: 40418
		private string _HandleType;
	}
}
