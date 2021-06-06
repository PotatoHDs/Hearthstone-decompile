using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E4 RID: 4580
	public class PresenceChanged : IProtoBuf
	{
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x0600CC62 RID: 52322 RVA: 0x003D0ACC File Offset: 0x003CECCC
		// (set) Token: 0x0600CC63 RID: 52323 RVA: 0x003D0AD4 File Offset: 0x003CECD4
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

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600CC64 RID: 52324 RVA: 0x003D0AE7 File Offset: 0x003CECE7
		// (set) Token: 0x0600CC65 RID: 52325 RVA: 0x003D0AEF File Offset: 0x003CECEF
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

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x0600CC66 RID: 52326 RVA: 0x003D0B02 File Offset: 0x003CED02
		// (set) Token: 0x0600CC67 RID: 52327 RVA: 0x003D0B0A File Offset: 0x003CED0A
		public PresenceStatus NewPresenceStatus
		{
			get
			{
				return this._NewPresenceStatus;
			}
			set
			{
				this._NewPresenceStatus = value;
				this.HasNewPresenceStatus = (value != null);
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x0600CC68 RID: 52328 RVA: 0x003D0B1D File Offset: 0x003CED1D
		// (set) Token: 0x0600CC69 RID: 52329 RVA: 0x003D0B25 File Offset: 0x003CED25
		public PresenceStatus PrevPresenceStatus
		{
			get
			{
				return this._PrevPresenceStatus;
			}
			set
			{
				this._PrevPresenceStatus = value;
				this.HasPrevPresenceStatus = (value != null);
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x0600CC6A RID: 52330 RVA: 0x003D0B38 File Offset: 0x003CED38
		// (set) Token: 0x0600CC6B RID: 52331 RVA: 0x003D0B40 File Offset: 0x003CED40
		public long MillisecondsSincePrev
		{
			get
			{
				return this._MillisecondsSincePrev;
			}
			set
			{
				this._MillisecondsSincePrev = value;
				this.HasMillisecondsSincePrev = true;
			}
		}

		// Token: 0x0600CC6C RID: 52332 RVA: 0x003D0B50 File Offset: 0x003CED50
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
			if (this.HasNewPresenceStatus)
			{
				num ^= this.NewPresenceStatus.GetHashCode();
			}
			if (this.HasPrevPresenceStatus)
			{
				num ^= this.PrevPresenceStatus.GetHashCode();
			}
			if (this.HasMillisecondsSincePrev)
			{
				num ^= this.MillisecondsSincePrev.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC6D RID: 52333 RVA: 0x003D0BDC File Offset: 0x003CEDDC
		public override bool Equals(object obj)
		{
			PresenceChanged presenceChanged = obj as PresenceChanged;
			return presenceChanged != null && this.HasPlayer == presenceChanged.HasPlayer && (!this.HasPlayer || this.Player.Equals(presenceChanged.Player)) && this.HasDeviceInfo == presenceChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(presenceChanged.DeviceInfo)) && this.HasNewPresenceStatus == presenceChanged.HasNewPresenceStatus && (!this.HasNewPresenceStatus || this.NewPresenceStatus.Equals(presenceChanged.NewPresenceStatus)) && this.HasPrevPresenceStatus == presenceChanged.HasPrevPresenceStatus && (!this.HasPrevPresenceStatus || this.PrevPresenceStatus.Equals(presenceChanged.PrevPresenceStatus)) && this.HasMillisecondsSincePrev == presenceChanged.HasMillisecondsSincePrev && (!this.HasMillisecondsSincePrev || this.MillisecondsSincePrev.Equals(presenceChanged.MillisecondsSincePrev));
		}

		// Token: 0x0600CC6E RID: 52334 RVA: 0x003D0CD0 File Offset: 0x003CEED0
		public void Deserialize(Stream stream)
		{
			PresenceChanged.Deserialize(stream, this);
		}

		// Token: 0x0600CC6F RID: 52335 RVA: 0x003D0CDA File Offset: 0x003CEEDA
		public static PresenceChanged Deserialize(Stream stream, PresenceChanged instance)
		{
			return PresenceChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC70 RID: 52336 RVA: 0x003D0CE8 File Offset: 0x003CEEE8
		public static PresenceChanged DeserializeLengthDelimited(Stream stream)
		{
			PresenceChanged presenceChanged = new PresenceChanged();
			PresenceChanged.DeserializeLengthDelimited(stream, presenceChanged);
			return presenceChanged;
		}

		// Token: 0x0600CC71 RID: 52337 RVA: 0x003D0D04 File Offset: 0x003CEF04
		public static PresenceChanged DeserializeLengthDelimited(Stream stream, PresenceChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PresenceChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC72 RID: 52338 RVA: 0x003D0D2C File Offset: 0x003CEF2C
		public static PresenceChanged Deserialize(Stream stream, PresenceChanged instance, long limit)
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
					else if (num != 26)
					{
						if (num != 34)
						{
							if (num == 40)
							{
								instance.MillisecondsSincePrev = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.PrevPresenceStatus == null)
							{
								instance.PrevPresenceStatus = PresenceStatus.DeserializeLengthDelimited(stream);
								continue;
							}
							PresenceStatus.DeserializeLengthDelimited(stream, instance.PrevPresenceStatus);
							continue;
						}
					}
					else
					{
						if (instance.NewPresenceStatus == null)
						{
							instance.NewPresenceStatus = PresenceStatus.DeserializeLengthDelimited(stream);
							continue;
						}
						PresenceStatus.DeserializeLengthDelimited(stream, instance.NewPresenceStatus);
						continue;
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

		// Token: 0x0600CC73 RID: 52339 RVA: 0x003D0E8D File Offset: 0x003CF08D
		public void Serialize(Stream stream)
		{
			PresenceChanged.Serialize(stream, this);
		}

		// Token: 0x0600CC74 RID: 52340 RVA: 0x003D0E98 File Offset: 0x003CF098
		public static void Serialize(Stream stream, PresenceChanged instance)
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
			if (instance.HasNewPresenceStatus)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.NewPresenceStatus.GetSerializedSize());
				PresenceStatus.Serialize(stream, instance.NewPresenceStatus);
			}
			if (instance.HasPrevPresenceStatus)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.PrevPresenceStatus.GetSerializedSize());
				PresenceStatus.Serialize(stream, instance.PrevPresenceStatus);
			}
			if (instance.HasMillisecondsSincePrev)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MillisecondsSincePrev);
			}
		}

		// Token: 0x0600CC75 RID: 52341 RVA: 0x003D0F78 File Offset: 0x003CF178
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
			if (this.HasNewPresenceStatus)
			{
				num += 1U;
				uint serializedSize3 = this.NewPresenceStatus.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasPrevPresenceStatus)
			{
				num += 1U;
				uint serializedSize4 = this.PrevPresenceStatus.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasMillisecondsSincePrev)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.MillisecondsSincePrev);
			}
			return num;
		}

		// Token: 0x0400A08F RID: 41103
		public bool HasPlayer;

		// Token: 0x0400A090 RID: 41104
		private Player _Player;

		// Token: 0x0400A091 RID: 41105
		public bool HasDeviceInfo;

		// Token: 0x0400A092 RID: 41106
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A093 RID: 41107
		public bool HasNewPresenceStatus;

		// Token: 0x0400A094 RID: 41108
		private PresenceStatus _NewPresenceStatus;

		// Token: 0x0400A095 RID: 41109
		public bool HasPrevPresenceStatus;

		// Token: 0x0400A096 RID: 41110
		private PresenceStatus _PrevPresenceStatus;

		// Token: 0x0400A097 RID: 41111
		public bool HasMillisecondsSincePrev;

		// Token: 0x0400A098 RID: 41112
		private long _MillisecondsSincePrev;
	}
}
