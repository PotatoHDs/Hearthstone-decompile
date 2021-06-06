using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DB RID: 4571
	public class MASDKGuestCreationFailure : IProtoBuf
	{
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x0600CBC1 RID: 52161 RVA: 0x003CE840 File Offset: 0x003CCA40
		// (set) Token: 0x0600CBC2 RID: 52162 RVA: 0x003CE848 File Offset: 0x003CCA48
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

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x0600CBC3 RID: 52163 RVA: 0x003CE85B File Offset: 0x003CCA5B
		// (set) Token: 0x0600CBC4 RID: 52164 RVA: 0x003CE863 File Offset: 0x003CCA63
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

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600CBC5 RID: 52165 RVA: 0x003CE876 File Offset: 0x003CCA76
		// (set) Token: 0x0600CBC6 RID: 52166 RVA: 0x003CE87E File Offset: 0x003CCA7E
		public int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x0600CBC7 RID: 52167 RVA: 0x003CE890 File Offset: 0x003CCA90
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
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CBC8 RID: 52168 RVA: 0x003CE8F0 File Offset: 0x003CCAF0
		public override bool Equals(object obj)
		{
			MASDKGuestCreationFailure masdkguestCreationFailure = obj as MASDKGuestCreationFailure;
			return masdkguestCreationFailure != null && this.HasPlayer == masdkguestCreationFailure.HasPlayer && (!this.HasPlayer || this.Player.Equals(masdkguestCreationFailure.Player)) && this.HasDeviceInfo == masdkguestCreationFailure.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(masdkguestCreationFailure.DeviceInfo)) && this.HasErrorCode == masdkguestCreationFailure.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(masdkguestCreationFailure.ErrorCode));
		}

		// Token: 0x0600CBC9 RID: 52169 RVA: 0x003CE98E File Offset: 0x003CCB8E
		public void Deserialize(Stream stream)
		{
			MASDKGuestCreationFailure.Deserialize(stream, this);
		}

		// Token: 0x0600CBCA RID: 52170 RVA: 0x003CE998 File Offset: 0x003CCB98
		public static MASDKGuestCreationFailure Deserialize(Stream stream, MASDKGuestCreationFailure instance)
		{
			return MASDKGuestCreationFailure.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CBCB RID: 52171 RVA: 0x003CE9A4 File Offset: 0x003CCBA4
		public static MASDKGuestCreationFailure DeserializeLengthDelimited(Stream stream)
		{
			MASDKGuestCreationFailure masdkguestCreationFailure = new MASDKGuestCreationFailure();
			MASDKGuestCreationFailure.DeserializeLengthDelimited(stream, masdkguestCreationFailure);
			return masdkguestCreationFailure;
		}

		// Token: 0x0600CBCC RID: 52172 RVA: 0x003CE9C0 File Offset: 0x003CCBC0
		public static MASDKGuestCreationFailure DeserializeLengthDelimited(Stream stream, MASDKGuestCreationFailure instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MASDKGuestCreationFailure.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CBCD RID: 52173 RVA: 0x003CE9E8 File Offset: 0x003CCBE8
		public static MASDKGuestCreationFailure Deserialize(Stream stream, MASDKGuestCreationFailure instance, long limit)
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
						if (num != 24)
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
							instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CBCE RID: 52174 RVA: 0x003CEAD1 File Offset: 0x003CCCD1
		public void Serialize(Stream stream)
		{
			MASDKGuestCreationFailure.Serialize(stream, this);
		}

		// Token: 0x0600CBCF RID: 52175 RVA: 0x003CEADC File Offset: 0x003CCCDC
		public static void Serialize(Stream stream, MASDKGuestCreationFailure instance)
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
			if (instance.HasErrorCode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
		}

		// Token: 0x0600CBD0 RID: 52176 RVA: 0x003CEB60 File Offset: 0x003CCD60
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
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			return num;
		}

		// Token: 0x0400A051 RID: 41041
		public bool HasPlayer;

		// Token: 0x0400A052 RID: 41042
		private Player _Player;

		// Token: 0x0400A053 RID: 41043
		public bool HasDeviceInfo;

		// Token: 0x0400A054 RID: 41044
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A055 RID: 41045
		public bool HasErrorCode;

		// Token: 0x0400A056 RID: 41046
		private int _ErrorCode;
	}
}
