using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011EA RID: 4586
	public class ReconnectSuccess : IProtoBuf
	{
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600CCD4 RID: 52436 RVA: 0x003D2404 File Offset: 0x003D0604
		// (set) Token: 0x0600CCD5 RID: 52437 RVA: 0x003D240C File Offset: 0x003D060C
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

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x0600CCD6 RID: 52438 RVA: 0x003D241F File Offset: 0x003D061F
		// (set) Token: 0x0600CCD7 RID: 52439 RVA: 0x003D2427 File Offset: 0x003D0627
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

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600CCD8 RID: 52440 RVA: 0x003D243A File Offset: 0x003D063A
		// (set) Token: 0x0600CCD9 RID: 52441 RVA: 0x003D2442 File Offset: 0x003D0642
		public float DisconnectDuration
		{
			get
			{
				return this._DisconnectDuration;
			}
			set
			{
				this._DisconnectDuration = value;
				this.HasDisconnectDuration = true;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x0600CCDA RID: 52442 RVA: 0x003D2452 File Offset: 0x003D0652
		// (set) Token: 0x0600CCDB RID: 52443 RVA: 0x003D245A File Offset: 0x003D065A
		public float ReconnectDuration
		{
			get
			{
				return this._ReconnectDuration;
			}
			set
			{
				this._ReconnectDuration = value;
				this.HasReconnectDuration = true;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x0600CCDC RID: 52444 RVA: 0x003D246A File Offset: 0x003D066A
		// (set) Token: 0x0600CCDD RID: 52445 RVA: 0x003D2472 File Offset: 0x003D0672
		public string ReconnectType
		{
			get
			{
				return this._ReconnectType;
			}
			set
			{
				this._ReconnectType = value;
				this.HasReconnectType = (value != null);
			}
		}

		// Token: 0x0600CCDE RID: 52446 RVA: 0x003D2488 File Offset: 0x003D0688
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
			if (this.HasDisconnectDuration)
			{
				num ^= this.DisconnectDuration.GetHashCode();
			}
			if (this.HasReconnectDuration)
			{
				num ^= this.ReconnectDuration.GetHashCode();
			}
			if (this.HasReconnectType)
			{
				num ^= this.ReconnectType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CCDF RID: 52447 RVA: 0x003D2518 File Offset: 0x003D0718
		public override bool Equals(object obj)
		{
			ReconnectSuccess reconnectSuccess = obj as ReconnectSuccess;
			return reconnectSuccess != null && this.HasPlayer == reconnectSuccess.HasPlayer && (!this.HasPlayer || this.Player.Equals(reconnectSuccess.Player)) && this.HasDeviceInfo == reconnectSuccess.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(reconnectSuccess.DeviceInfo)) && this.HasDisconnectDuration == reconnectSuccess.HasDisconnectDuration && (!this.HasDisconnectDuration || this.DisconnectDuration.Equals(reconnectSuccess.DisconnectDuration)) && this.HasReconnectDuration == reconnectSuccess.HasReconnectDuration && (!this.HasReconnectDuration || this.ReconnectDuration.Equals(reconnectSuccess.ReconnectDuration)) && this.HasReconnectType == reconnectSuccess.HasReconnectType && (!this.HasReconnectType || this.ReconnectType.Equals(reconnectSuccess.ReconnectType));
		}

		// Token: 0x0600CCE0 RID: 52448 RVA: 0x003D260F File Offset: 0x003D080F
		public void Deserialize(Stream stream)
		{
			ReconnectSuccess.Deserialize(stream, this);
		}

		// Token: 0x0600CCE1 RID: 52449 RVA: 0x003D2619 File Offset: 0x003D0819
		public static ReconnectSuccess Deserialize(Stream stream, ReconnectSuccess instance)
		{
			return ReconnectSuccess.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CCE2 RID: 52450 RVA: 0x003D2624 File Offset: 0x003D0824
		public static ReconnectSuccess DeserializeLengthDelimited(Stream stream)
		{
			ReconnectSuccess reconnectSuccess = new ReconnectSuccess();
			ReconnectSuccess.DeserializeLengthDelimited(stream, reconnectSuccess);
			return reconnectSuccess;
		}

		// Token: 0x0600CCE3 RID: 52451 RVA: 0x003D2640 File Offset: 0x003D0840
		public static ReconnectSuccess DeserializeLengthDelimited(Stream stream, ReconnectSuccess instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReconnectSuccess.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CCE4 RID: 52452 RVA: 0x003D2668 File Offset: 0x003D0868
		public static ReconnectSuccess Deserialize(Stream stream, ReconnectSuccess instance, long limit)
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
						if (num == 29)
						{
							instance.DisconnectDuration = binaryReader.ReadSingle();
							continue;
						}
						if (num == 37)
						{
							instance.ReconnectDuration = binaryReader.ReadSingle();
							continue;
						}
						if (num == 42)
						{
							instance.ReconnectType = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CCE5 RID: 52453 RVA: 0x003D2793 File Offset: 0x003D0993
		public void Serialize(Stream stream)
		{
			ReconnectSuccess.Serialize(stream, this);
		}

		// Token: 0x0600CCE6 RID: 52454 RVA: 0x003D279C File Offset: 0x003D099C
		public static void Serialize(Stream stream, ReconnectSuccess instance)
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
			if (instance.HasDisconnectDuration)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.DisconnectDuration);
			}
			if (instance.HasReconnectDuration)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ReconnectDuration);
			}
			if (instance.HasReconnectType)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReconnectType));
			}
		}

		// Token: 0x0600CCE7 RID: 52455 RVA: 0x003D2868 File Offset: 0x003D0A68
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
			if (this.HasDisconnectDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasReconnectDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasReconnectType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ReconnectType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400A0BF RID: 41151
		public bool HasPlayer;

		// Token: 0x0400A0C0 RID: 41152
		private Player _Player;

		// Token: 0x0400A0C1 RID: 41153
		public bool HasDeviceInfo;

		// Token: 0x0400A0C2 RID: 41154
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A0C3 RID: 41155
		public bool HasDisconnectDuration;

		// Token: 0x0400A0C4 RID: 41156
		private float _DisconnectDuration;

		// Token: 0x0400A0C5 RID: 41157
		public bool HasReconnectDuration;

		// Token: 0x0400A0C6 RID: 41158
		private float _ReconnectDuration;

		// Token: 0x0400A0C7 RID: 41159
		public bool HasReconnectType;

		// Token: 0x0400A0C8 RID: 41160
		private string _ReconnectType;
	}
}
