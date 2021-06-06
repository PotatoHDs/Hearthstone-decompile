using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B9 RID: 4537
	public class DeviceMuteChanged : IProtoBuf
	{
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600C92B RID: 51499 RVA: 0x003C5211 File Offset: 0x003C3411
		// (set) Token: 0x0600C92C RID: 51500 RVA: 0x003C5219 File Offset: 0x003C3419
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

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x0600C92D RID: 51501 RVA: 0x003C522C File Offset: 0x003C342C
		// (set) Token: 0x0600C92E RID: 51502 RVA: 0x003C5234 File Offset: 0x003C3434
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

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x0600C92F RID: 51503 RVA: 0x003C5247 File Offset: 0x003C3447
		// (set) Token: 0x0600C930 RID: 51504 RVA: 0x003C524F File Offset: 0x003C344F
		public bool Muted
		{
			get
			{
				return this._Muted;
			}
			set
			{
				this._Muted = value;
				this.HasMuted = true;
			}
		}

		// Token: 0x0600C931 RID: 51505 RVA: 0x003C5260 File Offset: 0x003C3460
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
			if (this.HasMuted)
			{
				num ^= this.Muted.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C932 RID: 51506 RVA: 0x003C52C0 File Offset: 0x003C34C0
		public override bool Equals(object obj)
		{
			DeviceMuteChanged deviceMuteChanged = obj as DeviceMuteChanged;
			return deviceMuteChanged != null && this.HasPlayer == deviceMuteChanged.HasPlayer && (!this.HasPlayer || this.Player.Equals(deviceMuteChanged.Player)) && this.HasDeviceInfo == deviceMuteChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deviceMuteChanged.DeviceInfo)) && this.HasMuted == deviceMuteChanged.HasMuted && (!this.HasMuted || this.Muted.Equals(deviceMuteChanged.Muted));
		}

		// Token: 0x0600C933 RID: 51507 RVA: 0x003C535E File Offset: 0x003C355E
		public void Deserialize(Stream stream)
		{
			DeviceMuteChanged.Deserialize(stream, this);
		}

		// Token: 0x0600C934 RID: 51508 RVA: 0x003C5368 File Offset: 0x003C3568
		public static DeviceMuteChanged Deserialize(Stream stream, DeviceMuteChanged instance)
		{
			return DeviceMuteChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C935 RID: 51509 RVA: 0x003C5374 File Offset: 0x003C3574
		public static DeviceMuteChanged DeserializeLengthDelimited(Stream stream)
		{
			DeviceMuteChanged deviceMuteChanged = new DeviceMuteChanged();
			DeviceMuteChanged.DeserializeLengthDelimited(stream, deviceMuteChanged);
			return deviceMuteChanged;
		}

		// Token: 0x0600C936 RID: 51510 RVA: 0x003C5390 File Offset: 0x003C3590
		public static DeviceMuteChanged DeserializeLengthDelimited(Stream stream, DeviceMuteChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeviceMuteChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C937 RID: 51511 RVA: 0x003C53B8 File Offset: 0x003C35B8
		public static DeviceMuteChanged Deserialize(Stream stream, DeviceMuteChanged instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 32)
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
							instance.Muted = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C938 RID: 51512 RVA: 0x003C54A0 File Offset: 0x003C36A0
		public void Serialize(Stream stream)
		{
			DeviceMuteChanged.Serialize(stream, this);
		}

		// Token: 0x0600C939 RID: 51513 RVA: 0x003C54AC File Offset: 0x003C36AC
		public static void Serialize(Stream stream, DeviceMuteChanged instance)
		{
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
			if (instance.HasMuted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Muted);
			}
		}

		// Token: 0x0600C93A RID: 51514 RVA: 0x003C5530 File Offset: 0x003C3730
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
			if (this.HasMuted)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04009F31 RID: 40753
		public bool HasPlayer;

		// Token: 0x04009F32 RID: 40754
		private Player _Player;

		// Token: 0x04009F33 RID: 40755
		public bool HasDeviceInfo;

		// Token: 0x04009F34 RID: 40756
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F35 RID: 40757
		public bool HasMuted;

		// Token: 0x04009F36 RID: 40758
		private bool _Muted;
	}
}
