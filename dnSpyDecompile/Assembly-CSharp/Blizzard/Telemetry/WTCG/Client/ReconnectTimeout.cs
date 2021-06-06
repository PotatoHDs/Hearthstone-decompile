using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011EB RID: 4587
	public class ReconnectTimeout : IProtoBuf
	{
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x0600CCE9 RID: 52457 RVA: 0x003D2906 File Offset: 0x003D0B06
		// (set) Token: 0x0600CCEA RID: 52458 RVA: 0x003D290E File Offset: 0x003D0B0E
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

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x0600CCEB RID: 52459 RVA: 0x003D2921 File Offset: 0x003D0B21
		// (set) Token: 0x0600CCEC RID: 52460 RVA: 0x003D2929 File Offset: 0x003D0B29
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x0600CCED RID: 52461 RVA: 0x003D293C File Offset: 0x003D0B3C
		// (set) Token: 0x0600CCEE RID: 52462 RVA: 0x003D2944 File Offset: 0x003D0B44
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

		// Token: 0x0600CCEF RID: 52463 RVA: 0x003D2958 File Offset: 0x003D0B58
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
			if (this.HasReconnectType)
			{
				num ^= this.ReconnectType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CCF0 RID: 52464 RVA: 0x003D29B4 File Offset: 0x003D0BB4
		public override bool Equals(object obj)
		{
			ReconnectTimeout reconnectTimeout = obj as ReconnectTimeout;
			return reconnectTimeout != null && this.HasPlayer == reconnectTimeout.HasPlayer && (!this.HasPlayer || this.Player.Equals(reconnectTimeout.Player)) && this.HasDeviceInfo == reconnectTimeout.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(reconnectTimeout.DeviceInfo)) && this.HasReconnectType == reconnectTimeout.HasReconnectType && (!this.HasReconnectType || this.ReconnectType.Equals(reconnectTimeout.ReconnectType));
		}

		// Token: 0x0600CCF1 RID: 52465 RVA: 0x003D2A4F File Offset: 0x003D0C4F
		public void Deserialize(Stream stream)
		{
			ReconnectTimeout.Deserialize(stream, this);
		}

		// Token: 0x0600CCF2 RID: 52466 RVA: 0x003D2A59 File Offset: 0x003D0C59
		public static ReconnectTimeout Deserialize(Stream stream, ReconnectTimeout instance)
		{
			return ReconnectTimeout.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CCF3 RID: 52467 RVA: 0x003D2A64 File Offset: 0x003D0C64
		public static ReconnectTimeout DeserializeLengthDelimited(Stream stream)
		{
			ReconnectTimeout reconnectTimeout = new ReconnectTimeout();
			ReconnectTimeout.DeserializeLengthDelimited(stream, reconnectTimeout);
			return reconnectTimeout;
		}

		// Token: 0x0600CCF4 RID: 52468 RVA: 0x003D2A80 File Offset: 0x003D0C80
		public static ReconnectTimeout DeserializeLengthDelimited(Stream stream, ReconnectTimeout instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReconnectTimeout.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CCF5 RID: 52469 RVA: 0x003D2AA8 File Offset: 0x003D0CA8
		public static ReconnectTimeout Deserialize(Stream stream, ReconnectTimeout instance, long limit)
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
						if (num != 26)
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
							instance.ReconnectType = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CCF6 RID: 52470 RVA: 0x003D2B90 File Offset: 0x003D0D90
		public void Serialize(Stream stream)
		{
			ReconnectTimeout.Serialize(stream, this);
		}

		// Token: 0x0600CCF7 RID: 52471 RVA: 0x003D2B9C File Offset: 0x003D0D9C
		public static void Serialize(Stream stream, ReconnectTimeout instance)
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
			if (instance.HasReconnectType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReconnectType));
			}
		}

		// Token: 0x0600CCF8 RID: 52472 RVA: 0x003D2C2C File Offset: 0x003D0E2C
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
			if (this.HasReconnectType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ReconnectType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400A0C9 RID: 41161
		public bool HasPlayer;

		// Token: 0x0400A0CA RID: 41162
		private Player _Player;

		// Token: 0x0400A0CB RID: 41163
		public bool HasDeviceInfo;

		// Token: 0x0400A0CC RID: 41164
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A0CD RID: 41165
		public bool HasReconnectType;

		// Token: 0x0400A0CE RID: 41166
		private string _ReconnectType;
	}
}
