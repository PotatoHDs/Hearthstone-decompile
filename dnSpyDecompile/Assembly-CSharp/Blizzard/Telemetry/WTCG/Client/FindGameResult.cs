using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BF RID: 4543
	public class FindGameResult : IProtoBuf
	{
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x0600C99B RID: 51611 RVA: 0x003C6A87 File Offset: 0x003C4C87
		// (set) Token: 0x0600C99C RID: 51612 RVA: 0x003C6A8F File Offset: 0x003C4C8F
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

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x0600C99D RID: 51613 RVA: 0x003C6AA2 File Offset: 0x003C4CA2
		// (set) Token: 0x0600C99E RID: 51614 RVA: 0x003C6AAA File Offset: 0x003C4CAA
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

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x0600C99F RID: 51615 RVA: 0x003C6ABD File Offset: 0x003C4CBD
		// (set) Token: 0x0600C9A0 RID: 51616 RVA: 0x003C6AC5 File Offset: 0x003C4CC5
		public uint ResultCode
		{
			get
			{
				return this._ResultCode;
			}
			set
			{
				this._ResultCode = value;
				this.HasResultCode = true;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600C9A1 RID: 51617 RVA: 0x003C6AD5 File Offset: 0x003C4CD5
		// (set) Token: 0x0600C9A2 RID: 51618 RVA: 0x003C6ADD File Offset: 0x003C4CDD
		public string ResultCodeString
		{
			get
			{
				return this._ResultCodeString;
			}
			set
			{
				this._ResultCodeString = value;
				this.HasResultCodeString = (value != null);
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x0600C9A3 RID: 51619 RVA: 0x003C6AF0 File Offset: 0x003C4CF0
		// (set) Token: 0x0600C9A4 RID: 51620 RVA: 0x003C6AF8 File Offset: 0x003C4CF8
		public long TimeSpentMilliseconds
		{
			get
			{
				return this._TimeSpentMilliseconds;
			}
			set
			{
				this._TimeSpentMilliseconds = value;
				this.HasTimeSpentMilliseconds = true;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600C9A5 RID: 51621 RVA: 0x003C6B08 File Offset: 0x003C4D08
		// (set) Token: 0x0600C9A6 RID: 51622 RVA: 0x003C6B10 File Offset: 0x003C4D10
		public GameSessionInfo GameSessionInfo
		{
			get
			{
				return this._GameSessionInfo;
			}
			set
			{
				this._GameSessionInfo = value;
				this.HasGameSessionInfo = (value != null);
			}
		}

		// Token: 0x0600C9A7 RID: 51623 RVA: 0x003C6B24 File Offset: 0x003C4D24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasResultCode)
			{
				num ^= this.ResultCode.GetHashCode();
			}
			if (this.HasResultCodeString)
			{
				num ^= this.ResultCodeString.GetHashCode();
			}
			if (this.HasTimeSpentMilliseconds)
			{
				num ^= this.TimeSpentMilliseconds.GetHashCode();
			}
			if (this.HasGameSessionInfo)
			{
				num ^= this.GameSessionInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C9A8 RID: 51624 RVA: 0x003C6BC8 File Offset: 0x003C4DC8
		public override bool Equals(object obj)
		{
			FindGameResult findGameResult = obj as FindGameResult;
			return findGameResult != null && this.HasDeviceInfo == findGameResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(findGameResult.DeviceInfo)) && this.HasPlayer == findGameResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(findGameResult.Player)) && this.HasResultCode == findGameResult.HasResultCode && (!this.HasResultCode || this.ResultCode.Equals(findGameResult.ResultCode)) && this.HasResultCodeString == findGameResult.HasResultCodeString && (!this.HasResultCodeString || this.ResultCodeString.Equals(findGameResult.ResultCodeString)) && this.HasTimeSpentMilliseconds == findGameResult.HasTimeSpentMilliseconds && (!this.HasTimeSpentMilliseconds || this.TimeSpentMilliseconds.Equals(findGameResult.TimeSpentMilliseconds)) && this.HasGameSessionInfo == findGameResult.HasGameSessionInfo && (!this.HasGameSessionInfo || this.GameSessionInfo.Equals(findGameResult.GameSessionInfo));
		}

		// Token: 0x0600C9A9 RID: 51625 RVA: 0x003C6CEA File Offset: 0x003C4EEA
		public void Deserialize(Stream stream)
		{
			FindGameResult.Deserialize(stream, this);
		}

		// Token: 0x0600C9AA RID: 51626 RVA: 0x003C6CF4 File Offset: 0x003C4EF4
		public static FindGameResult Deserialize(Stream stream, FindGameResult instance)
		{
			return FindGameResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C9AB RID: 51627 RVA: 0x003C6D00 File Offset: 0x003C4F00
		public static FindGameResult DeserializeLengthDelimited(Stream stream)
		{
			FindGameResult findGameResult = new FindGameResult();
			FindGameResult.DeserializeLengthDelimited(stream, findGameResult);
			return findGameResult;
		}

		// Token: 0x0600C9AC RID: 51628 RVA: 0x003C6D1C File Offset: 0x003C4F1C
		public static FindGameResult DeserializeLengthDelimited(Stream stream, FindGameResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindGameResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C9AD RID: 51629 RVA: 0x003C6D44 File Offset: 0x003C4F44
		public static FindGameResult Deserialize(Stream stream, FindGameResult instance, long limit)
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 24)
								{
									instance.ResultCode = ProtocolParser.ReadUInt32(stream);
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
						if (num == 34)
						{
							instance.ResultCodeString = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.TimeSpentMilliseconds = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							if (instance.GameSessionInfo == null)
							{
								instance.GameSessionInfo = GameSessionInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							GameSessionInfo.DeserializeLengthDelimited(stream, instance.GameSessionInfo);
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

		// Token: 0x0600C9AE RID: 51630 RVA: 0x003C6EA1 File Offset: 0x003C50A1
		public void Serialize(Stream stream)
		{
			FindGameResult.Serialize(stream, this);
		}

		// Token: 0x0600C9AF RID: 51631 RVA: 0x003C6EAC File Offset: 0x003C50AC
		public static void Serialize(Stream stream, FindGameResult instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasResultCode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ResultCode);
			}
			if (instance.HasResultCodeString)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ResultCodeString));
			}
			if (instance.HasTimeSpentMilliseconds)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TimeSpentMilliseconds);
			}
			if (instance.HasGameSessionInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameSessionInfo.GetSerializedSize());
				GameSessionInfo.Serialize(stream, instance.GameSessionInfo);
			}
		}

		// Token: 0x0600C9B0 RID: 51632 RVA: 0x003C6FA0 File Offset: 0x003C51A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize2 = this.Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasResultCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ResultCode);
			}
			if (this.HasResultCodeString)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ResultCodeString);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTimeSpentMilliseconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TimeSpentMilliseconds);
			}
			if (this.HasGameSessionInfo)
			{
				num += 1U;
				uint serializedSize3 = this.GameSessionInfo.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04009F5F RID: 40799
		public bool HasDeviceInfo;

		// Token: 0x04009F60 RID: 40800
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F61 RID: 40801
		public bool HasPlayer;

		// Token: 0x04009F62 RID: 40802
		private Player _Player;

		// Token: 0x04009F63 RID: 40803
		public bool HasResultCode;

		// Token: 0x04009F64 RID: 40804
		private uint _ResultCode;

		// Token: 0x04009F65 RID: 40805
		public bool HasResultCodeString;

		// Token: 0x04009F66 RID: 40806
		private string _ResultCodeString;

		// Token: 0x04009F67 RID: 40807
		public bool HasTimeSpentMilliseconds;

		// Token: 0x04009F68 RID: 40808
		private long _TimeSpentMilliseconds;

		// Token: 0x04009F69 RID: 40809
		public bool HasGameSessionInfo;

		// Token: 0x04009F6A RID: 40810
		private GameSessionInfo _GameSessionInfo;
	}
}
