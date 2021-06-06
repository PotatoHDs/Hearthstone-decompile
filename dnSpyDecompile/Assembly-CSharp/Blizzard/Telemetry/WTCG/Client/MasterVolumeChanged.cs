using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DD RID: 4573
	public class MasterVolumeChanged : IProtoBuf
	{
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x0600CBE7 RID: 52199 RVA: 0x003CF103 File Offset: 0x003CD303
		// (set) Token: 0x0600CBE8 RID: 52200 RVA: 0x003CF10B File Offset: 0x003CD30B
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

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x0600CBE9 RID: 52201 RVA: 0x003CF11E File Offset: 0x003CD31E
		// (set) Token: 0x0600CBEA RID: 52202 RVA: 0x003CF126 File Offset: 0x003CD326
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

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x0600CBEB RID: 52203 RVA: 0x003CF139 File Offset: 0x003CD339
		// (set) Token: 0x0600CBEC RID: 52204 RVA: 0x003CF141 File Offset: 0x003CD341
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

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x0600CBED RID: 52205 RVA: 0x003CF151 File Offset: 0x003CD351
		// (set) Token: 0x0600CBEE RID: 52206 RVA: 0x003CF159 File Offset: 0x003CD359
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

		// Token: 0x0600CBEF RID: 52207 RVA: 0x003CF16C File Offset: 0x003CD36C
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

		// Token: 0x0600CBF0 RID: 52208 RVA: 0x003CF1E4 File Offset: 0x003CD3E4
		public override bool Equals(object obj)
		{
			MasterVolumeChanged masterVolumeChanged = obj as MasterVolumeChanged;
			return masterVolumeChanged != null && this.HasPlayer == masterVolumeChanged.HasPlayer && (!this.HasPlayer || this.Player.Equals(masterVolumeChanged.Player)) && this.HasDeviceInfo == masterVolumeChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(masterVolumeChanged.DeviceInfo)) && this.HasOldVolume == masterVolumeChanged.HasOldVolume && (!this.HasOldVolume || this.OldVolume.Equals(masterVolumeChanged.OldVolume)) && this.HasNewVolume == masterVolumeChanged.HasNewVolume && (!this.HasNewVolume || this.NewVolume.Equals(masterVolumeChanged.NewVolume));
		}

		// Token: 0x0600CBF1 RID: 52209 RVA: 0x003CF2B0 File Offset: 0x003CD4B0
		public void Deserialize(Stream stream)
		{
			MasterVolumeChanged.Deserialize(stream, this);
		}

		// Token: 0x0600CBF2 RID: 52210 RVA: 0x003CF2BA File Offset: 0x003CD4BA
		public static MasterVolumeChanged Deserialize(Stream stream, MasterVolumeChanged instance)
		{
			return MasterVolumeChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CBF3 RID: 52211 RVA: 0x003CF2C8 File Offset: 0x003CD4C8
		public static MasterVolumeChanged DeserializeLengthDelimited(Stream stream)
		{
			MasterVolumeChanged masterVolumeChanged = new MasterVolumeChanged();
			MasterVolumeChanged.DeserializeLengthDelimited(stream, masterVolumeChanged);
			return masterVolumeChanged;
		}

		// Token: 0x0600CBF4 RID: 52212 RVA: 0x003CF2E4 File Offset: 0x003CD4E4
		public static MasterVolumeChanged DeserializeLengthDelimited(Stream stream, MasterVolumeChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MasterVolumeChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CBF5 RID: 52213 RVA: 0x003CF30C File Offset: 0x003CD50C
		public static MasterVolumeChanged Deserialize(Stream stream, MasterVolumeChanged instance, long limit)
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

		// Token: 0x0600CBF6 RID: 52214 RVA: 0x003CF41E File Offset: 0x003CD61E
		public void Serialize(Stream stream)
		{
			MasterVolumeChanged.Serialize(stream, this);
		}

		// Token: 0x0600CBF7 RID: 52215 RVA: 0x003CF428 File Offset: 0x003CD628
		public static void Serialize(Stream stream, MasterVolumeChanged instance)
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

		// Token: 0x0600CBF8 RID: 52216 RVA: 0x003CF4D0 File Offset: 0x003CD6D0
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

		// Token: 0x0400A061 RID: 41057
		public bool HasPlayer;

		// Token: 0x0400A062 RID: 41058
		private Player _Player;

		// Token: 0x0400A063 RID: 41059
		public bool HasDeviceInfo;

		// Token: 0x0400A064 RID: 41060
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A065 RID: 41061
		public bool HasOldVolume;

		// Token: 0x0400A066 RID: 41062
		private float _OldVolume;

		// Token: 0x0400A067 RID: 41063
		public bool HasNewVolume;

		// Token: 0x0400A068 RID: 41064
		private float _NewVolume;
	}
}
