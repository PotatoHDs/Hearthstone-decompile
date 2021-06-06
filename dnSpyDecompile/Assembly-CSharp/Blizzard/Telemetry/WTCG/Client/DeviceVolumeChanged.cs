using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BA RID: 4538
	public class DeviceVolumeChanged : IProtoBuf
	{
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x0600C93C RID: 51516 RVA: 0x003C5596 File Offset: 0x003C3796
		// (set) Token: 0x0600C93D RID: 51517 RVA: 0x003C559E File Offset: 0x003C379E
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x0600C93E RID: 51518 RVA: 0x003C55B1 File Offset: 0x003C37B1
		// (set) Token: 0x0600C93F RID: 51519 RVA: 0x003C55B9 File Offset: 0x003C37B9
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

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x0600C940 RID: 51520 RVA: 0x003C55CC File Offset: 0x003C37CC
		// (set) Token: 0x0600C941 RID: 51521 RVA: 0x003C55D4 File Offset: 0x003C37D4
		public float OldVolume
		{
			get
			{
				return this._OldVolume;
			}
			set
			{
				this._OldVolume = value;
				this.HasOldVolume = true;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x0600C942 RID: 51522 RVA: 0x003C55E4 File Offset: 0x003C37E4
		// (set) Token: 0x0600C943 RID: 51523 RVA: 0x003C55EC File Offset: 0x003C37EC
		public float NewVolume
		{
			get
			{
				return this._NewVolume;
			}
			set
			{
				this._NewVolume = value;
				this.HasNewVolume = true;
			}
		}

		// Token: 0x0600C944 RID: 51524 RVA: 0x003C55FC File Offset: 0x003C37FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasOldVolume)
			{
				num ^= this.OldVolume.GetHashCode();
			}
			if (this.HasNewVolume)
			{
				num ^= this.NewVolume.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C945 RID: 51525 RVA: 0x003C5674 File Offset: 0x003C3874
		public override bool Equals(object obj)
		{
			DeviceVolumeChanged deviceVolumeChanged = obj as DeviceVolumeChanged;
			return deviceVolumeChanged != null && this.HasPlayer == deviceVolumeChanged.HasPlayer && (!this.HasPlayer || this.Player.Equals(deviceVolumeChanged.Player)) && this.HasDeviceInfo == deviceVolumeChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deviceVolumeChanged.DeviceInfo)) && this.HasOldVolume == deviceVolumeChanged.HasOldVolume && (!this.HasOldVolume || this.OldVolume.Equals(deviceVolumeChanged.OldVolume)) && this.HasNewVolume == deviceVolumeChanged.HasNewVolume && (!this.HasNewVolume || this.NewVolume.Equals(deviceVolumeChanged.NewVolume));
		}

		// Token: 0x0600C946 RID: 51526 RVA: 0x003C5740 File Offset: 0x003C3940
		public void Deserialize(Stream stream)
		{
			DeviceVolumeChanged.Deserialize(stream, this);
		}

		// Token: 0x0600C947 RID: 51527 RVA: 0x003C574A File Offset: 0x003C394A
		public static DeviceVolumeChanged Deserialize(Stream stream, DeviceVolumeChanged instance)
		{
			return DeviceVolumeChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C948 RID: 51528 RVA: 0x003C5758 File Offset: 0x003C3958
		public static DeviceVolumeChanged DeserializeLengthDelimited(Stream stream)
		{
			DeviceVolumeChanged deviceVolumeChanged = new DeviceVolumeChanged();
			DeviceVolumeChanged.DeserializeLengthDelimited(stream, deviceVolumeChanged);
			return deviceVolumeChanged;
		}

		// Token: 0x0600C949 RID: 51529 RVA: 0x003C5774 File Offset: 0x003C3974
		public static DeviceVolumeChanged DeserializeLengthDelimited(Stream stream, DeviceVolumeChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeviceVolumeChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C94A RID: 51530 RVA: 0x003C579C File Offset: 0x003C399C
		public static DeviceVolumeChanged Deserialize(Stream stream, DeviceVolumeChanged instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
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
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else
					{
						if (num == 37)
						{
							instance.OldVolume = binaryReader.ReadSingle();
							continue;
						}
						if (num == 45)
						{
							instance.NewVolume = binaryReader.ReadSingle();
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

		// Token: 0x0600C94B RID: 51531 RVA: 0x003C58AE File Offset: 0x003C3AAE
		public void Serialize(Stream stream)
		{
			DeviceVolumeChanged.Serialize(stream, this);
		}

		// Token: 0x0600C94C RID: 51532 RVA: 0x003C58B8 File Offset: 0x003C3AB8
		public static void Serialize(Stream stream, DeviceVolumeChanged instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasOldVolume)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.OldVolume);
			}
			if (instance.HasNewVolume)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.NewVolume);
			}
		}

		// Token: 0x0600C94D RID: 51533 RVA: 0x003C5960 File Offset: 0x003C3B60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasOldVolume)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasNewVolume)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009F37 RID: 40759
		public bool HasPlayer;

		// Token: 0x04009F38 RID: 40760
		private Player _Player;

		// Token: 0x04009F39 RID: 40761
		public bool HasDeviceInfo;

		// Token: 0x04009F3A RID: 40762
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F3B RID: 40763
		public bool HasOldVolume;

		// Token: 0x04009F3C RID: 40764
		private float _OldVolume;

		// Token: 0x04009F3D RID: 40765
		public bool HasNewVolume;

		// Token: 0x04009F3E RID: 40766
		private float _NewVolume;
	}
}
