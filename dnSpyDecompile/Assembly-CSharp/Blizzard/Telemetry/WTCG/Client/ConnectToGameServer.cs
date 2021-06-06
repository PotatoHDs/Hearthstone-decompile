using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AE RID: 4526
	public class ConnectToGameServer : IProtoBuf
	{
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600C85C RID: 51292 RVA: 0x003C2457 File Offset: 0x003C0657
		// (set) Token: 0x0600C85D RID: 51293 RVA: 0x003C245F File Offset: 0x003C065F
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

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x0600C85E RID: 51294 RVA: 0x003C2472 File Offset: 0x003C0672
		// (set) Token: 0x0600C85F RID: 51295 RVA: 0x003C247A File Offset: 0x003C067A
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

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600C860 RID: 51296 RVA: 0x003C248D File Offset: 0x003C068D
		// (set) Token: 0x0600C861 RID: 51297 RVA: 0x003C2495 File Offset: 0x003C0695
		public uint ResultBnetCode
		{
			get
			{
				return this._ResultBnetCode;
			}
			set
			{
				this._ResultBnetCode = value;
				this.HasResultBnetCode = true;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x0600C862 RID: 51298 RVA: 0x003C24A5 File Offset: 0x003C06A5
		// (set) Token: 0x0600C863 RID: 51299 RVA: 0x003C24AD File Offset: 0x003C06AD
		public string ResultBnetCodeString
		{
			get
			{
				return this._ResultBnetCodeString;
			}
			set
			{
				this._ResultBnetCodeString = value;
				this.HasResultBnetCodeString = (value != null);
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x0600C864 RID: 51300 RVA: 0x003C24C0 File Offset: 0x003C06C0
		// (set) Token: 0x0600C865 RID: 51301 RVA: 0x003C24C8 File Offset: 0x003C06C8
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

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x0600C866 RID: 51302 RVA: 0x003C24D8 File Offset: 0x003C06D8
		// (set) Token: 0x0600C867 RID: 51303 RVA: 0x003C24E0 File Offset: 0x003C06E0
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

		// Token: 0x0600C868 RID: 51304 RVA: 0x003C24F4 File Offset: 0x003C06F4
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
			if (this.HasResultBnetCode)
			{
				num ^= this.ResultBnetCode.GetHashCode();
			}
			if (this.HasResultBnetCodeString)
			{
				num ^= this.ResultBnetCodeString.GetHashCode();
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

		// Token: 0x0600C869 RID: 51305 RVA: 0x003C2598 File Offset: 0x003C0798
		public override bool Equals(object obj)
		{
			ConnectToGameServer connectToGameServer = obj as ConnectToGameServer;
			return connectToGameServer != null && this.HasDeviceInfo == connectToGameServer.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(connectToGameServer.DeviceInfo)) && this.HasPlayer == connectToGameServer.HasPlayer && (!this.HasPlayer || this.Player.Equals(connectToGameServer.Player)) && this.HasResultBnetCode == connectToGameServer.HasResultBnetCode && (!this.HasResultBnetCode || this.ResultBnetCode.Equals(connectToGameServer.ResultBnetCode)) && this.HasResultBnetCodeString == connectToGameServer.HasResultBnetCodeString && (!this.HasResultBnetCodeString || this.ResultBnetCodeString.Equals(connectToGameServer.ResultBnetCodeString)) && this.HasTimeSpentMilliseconds == connectToGameServer.HasTimeSpentMilliseconds && (!this.HasTimeSpentMilliseconds || this.TimeSpentMilliseconds.Equals(connectToGameServer.TimeSpentMilliseconds)) && this.HasGameSessionInfo == connectToGameServer.HasGameSessionInfo && (!this.HasGameSessionInfo || this.GameSessionInfo.Equals(connectToGameServer.GameSessionInfo));
		}

		// Token: 0x0600C86A RID: 51306 RVA: 0x003C26BA File Offset: 0x003C08BA
		public void Deserialize(Stream stream)
		{
			ConnectToGameServer.Deserialize(stream, this);
		}

		// Token: 0x0600C86B RID: 51307 RVA: 0x003C26C4 File Offset: 0x003C08C4
		public static ConnectToGameServer Deserialize(Stream stream, ConnectToGameServer instance)
		{
			return ConnectToGameServer.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C86C RID: 51308 RVA: 0x003C26D0 File Offset: 0x003C08D0
		public static ConnectToGameServer DeserializeLengthDelimited(Stream stream)
		{
			ConnectToGameServer connectToGameServer = new ConnectToGameServer();
			ConnectToGameServer.DeserializeLengthDelimited(stream, connectToGameServer);
			return connectToGameServer;
		}

		// Token: 0x0600C86D RID: 51309 RVA: 0x003C26EC File Offset: 0x003C08EC
		public static ConnectToGameServer DeserializeLengthDelimited(Stream stream, ConnectToGameServer instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectToGameServer.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C86E RID: 51310 RVA: 0x003C2714 File Offset: 0x003C0914
		public static ConnectToGameServer Deserialize(Stream stream, ConnectToGameServer instance, long limit)
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
									instance.ResultBnetCode = ProtocolParser.ReadUInt32(stream);
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
							instance.ResultBnetCodeString = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C86F RID: 51311 RVA: 0x003C2871 File Offset: 0x003C0A71
		public void Serialize(Stream stream)
		{
			ConnectToGameServer.Serialize(stream, this);
		}

		// Token: 0x0600C870 RID: 51312 RVA: 0x003C287C File Offset: 0x003C0A7C
		public static void Serialize(Stream stream, ConnectToGameServer instance)
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
			if (instance.HasResultBnetCode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ResultBnetCode);
			}
			if (instance.HasResultBnetCodeString)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ResultBnetCodeString));
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

		// Token: 0x0600C871 RID: 51313 RVA: 0x003C2970 File Offset: 0x003C0B70
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
			if (this.HasResultBnetCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ResultBnetCode);
			}
			if (this.HasResultBnetCodeString)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ResultBnetCodeString);
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

		// Token: 0x04009EDB RID: 40667
		public bool HasDeviceInfo;

		// Token: 0x04009EDC RID: 40668
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EDD RID: 40669
		public bool HasPlayer;

		// Token: 0x04009EDE RID: 40670
		private Player _Player;

		// Token: 0x04009EDF RID: 40671
		public bool HasResultBnetCode;

		// Token: 0x04009EE0 RID: 40672
		private uint _ResultBnetCode;

		// Token: 0x04009EE1 RID: 40673
		public bool HasResultBnetCodeString;

		// Token: 0x04009EE2 RID: 40674
		private string _ResultBnetCodeString;

		// Token: 0x04009EE3 RID: 40675
		public bool HasTimeSpentMilliseconds;

		// Token: 0x04009EE4 RID: 40676
		private long _TimeSpentMilliseconds;

		// Token: 0x04009EE5 RID: 40677
		public bool HasGameSessionInfo;

		// Token: 0x04009EE6 RID: 40678
		private GameSessionInfo _GameSessionInfo;
	}
}
