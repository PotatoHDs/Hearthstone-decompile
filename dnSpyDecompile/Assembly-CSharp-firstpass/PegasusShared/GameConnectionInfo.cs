using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000168 RID: 360
	public class GameConnectionInfo : IProtoBuf
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00056D9B File Offset: 0x00054F9B
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00056DA3 File Offset: 0x00054FA3
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this._Address = value;
				this.HasAddress = (value != null);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00056DB6 File Offset: 0x00054FB6
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x00056DBE File Offset: 0x00054FBE
		public int GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = true;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00056DCE File Offset: 0x00054FCE
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x00056DD6 File Offset: 0x00054FD6
		public long ClientHandle
		{
			get
			{
				return this._ClientHandle;
			}
			set
			{
				this._ClientHandle = value;
				this.HasClientHandle = true;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00056DE6 File Offset: 0x00054FE6
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x00056DEE File Offset: 0x00054FEE
		public int Port
		{
			get
			{
				return this._Port;
			}
			set
			{
				this._Port = value;
				this.HasPort = true;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00056DFE File Offset: 0x00054FFE
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x00056E06 File Offset: 0x00055006
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = (value != null);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00056E19 File Offset: 0x00055019
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x00056E21 File Offset: 0x00055021
		public string AuroraPassword
		{
			get
			{
				return this._AuroraPassword;
			}
			set
			{
				this._AuroraPassword = value;
				this.HasAuroraPassword = (value != null);
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00056E34 File Offset: 0x00055034
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x00056E3C File Offset: 0x0005503C
		public int Scenario
		{
			get
			{
				return this._Scenario;
			}
			set
			{
				this._Scenario = value;
				this.HasScenario = true;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00056E4C File Offset: 0x0005504C
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00056E54 File Offset: 0x00055054
		public GameType GameType
		{
			get
			{
				return this._GameType;
			}
			set
			{
				this._GameType = value;
				this.HasGameType = true;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00056E64 File Offset: 0x00055064
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00056E6C File Offset: 0x0005506C
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00056E7C File Offset: 0x0005507C
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x00056E84 File Offset: 0x00055084
		public int BrawlLibraryItemId
		{
			get
			{
				return this._BrawlLibraryItemId;
			}
			set
			{
				this._BrawlLibraryItemId = value;
				this.HasBrawlLibraryItemId = true;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00056E94 File Offset: 0x00055094
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x00056E9C File Offset: 0x0005509C
		public bool LoadGameState
		{
			get
			{
				return this._LoadGameState;
			}
			set
			{
				this._LoadGameState = value;
				this.HasLoadGameState = true;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00056EAC File Offset: 0x000550AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAddress)
			{
				num ^= this.Address.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasClientHandle)
			{
				num ^= this.ClientHandle.GetHashCode();
			}
			if (this.HasPort)
			{
				num ^= this.Port.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			if (this.HasAuroraPassword)
			{
				num ^= this.AuroraPassword.GetHashCode();
			}
			if (this.HasScenario)
			{
				num ^= this.Scenario.GetHashCode();
			}
			if (this.HasGameType)
			{
				num ^= this.GameType.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			if (this.HasLoadGameState)
			{
				num ^= this.LoadGameState.GetHashCode();
			}
			return num;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00056FE0 File Offset: 0x000551E0
		public override bool Equals(object obj)
		{
			GameConnectionInfo gameConnectionInfo = obj as GameConnectionInfo;
			return gameConnectionInfo != null && this.HasAddress == gameConnectionInfo.HasAddress && (!this.HasAddress || this.Address.Equals(gameConnectionInfo.Address)) && this.HasGameHandle == gameConnectionInfo.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(gameConnectionInfo.GameHandle)) && this.HasClientHandle == gameConnectionInfo.HasClientHandle && (!this.HasClientHandle || this.ClientHandle.Equals(gameConnectionInfo.ClientHandle)) && this.HasPort == gameConnectionInfo.HasPort && (!this.HasPort || this.Port.Equals(gameConnectionInfo.Port)) && this.HasVersion == gameConnectionInfo.HasVersion && (!this.HasVersion || this.Version.Equals(gameConnectionInfo.Version)) && this.HasAuroraPassword == gameConnectionInfo.HasAuroraPassword && (!this.HasAuroraPassword || this.AuroraPassword.Equals(gameConnectionInfo.AuroraPassword)) && this.HasScenario == gameConnectionInfo.HasScenario && (!this.HasScenario || this.Scenario.Equals(gameConnectionInfo.Scenario)) && this.HasGameType == gameConnectionInfo.HasGameType && (!this.HasGameType || this.GameType.Equals(gameConnectionInfo.GameType)) && this.HasFormatType == gameConnectionInfo.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(gameConnectionInfo.FormatType)) && this.HasBrawlLibraryItemId == gameConnectionInfo.HasBrawlLibraryItemId && (!this.HasBrawlLibraryItemId || this.BrawlLibraryItemId.Equals(gameConnectionInfo.BrawlLibraryItemId)) && this.HasLoadGameState == gameConnectionInfo.HasLoadGameState && (!this.HasLoadGameState || this.LoadGameState.Equals(gameConnectionInfo.LoadGameState));
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00057203 File Offset: 0x00055403
		public void Deserialize(Stream stream)
		{
			GameConnectionInfo.Deserialize(stream, this);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0005720D File Offset: 0x0005540D
		public static GameConnectionInfo Deserialize(Stream stream, GameConnectionInfo instance)
		{
			return GameConnectionInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00057218 File Offset: 0x00055418
		public static GameConnectionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameConnectionInfo gameConnectionInfo = new GameConnectionInfo();
			GameConnectionInfo.DeserializeLengthDelimited(stream, gameConnectionInfo);
			return gameConnectionInfo;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00057234 File Offset: 0x00055434
		public static GameConnectionInfo DeserializeLengthDelimited(Stream stream, GameConnectionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameConnectionInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0005725C File Offset: 0x0005545C
		public static GameConnectionInfo Deserialize(Stream stream, GameConnectionInfo instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					if (num <= 42)
					{
						if (num <= 16)
						{
							if (num == 10)
							{
								instance.Address = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.Port = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 42)
							{
								instance.Version = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 64)
					{
						if (num == 50)
						{
							instance.AuroraPassword = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 56)
						{
							instance.Scenario = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 72)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 80)
						{
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.LoadGameState = ProtocolParser.ReadBool(stream);
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

		// Token: 0x060018D3 RID: 6355 RVA: 0x00057413 File Offset: 0x00055613
		public void Serialize(Stream stream)
		{
			GameConnectionInfo.Serialize(stream, this);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0005741C File Offset: 0x0005561C
		public static void Serialize(Stream stream, GameConnectionInfo instance)
		{
			if (instance.HasAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address));
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameHandle));
			}
			if (instance.HasClientHandle)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			}
			if (instance.HasPort)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Port));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasAuroraPassword)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AuroraPassword));
			}
			if (instance.HasScenario)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Scenario));
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
			if (instance.HasLoadGameState)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.LoadGameState);
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00057584 File Offset: 0x00055784
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Address);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameHandle));
			}
			if (this.HasClientHandle)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientHandle);
			}
			if (this.HasPort)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Port));
			}
			if (this.HasVersion)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Version);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasAuroraPassword)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.AuroraPassword);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasScenario)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Scenario));
			}
			if (this.HasGameType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameType));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasBrawlLibraryItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			if (this.HasLoadGameState)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040007E8 RID: 2024
		public bool HasAddress;

		// Token: 0x040007E9 RID: 2025
		private string _Address;

		// Token: 0x040007EA RID: 2026
		public bool HasGameHandle;

		// Token: 0x040007EB RID: 2027
		private int _GameHandle;

		// Token: 0x040007EC RID: 2028
		public bool HasClientHandle;

		// Token: 0x040007ED RID: 2029
		private long _ClientHandle;

		// Token: 0x040007EE RID: 2030
		public bool HasPort;

		// Token: 0x040007EF RID: 2031
		private int _Port;

		// Token: 0x040007F0 RID: 2032
		public bool HasVersion;

		// Token: 0x040007F1 RID: 2033
		private string _Version;

		// Token: 0x040007F2 RID: 2034
		public bool HasAuroraPassword;

		// Token: 0x040007F3 RID: 2035
		private string _AuroraPassword;

		// Token: 0x040007F4 RID: 2036
		public bool HasScenario;

		// Token: 0x040007F5 RID: 2037
		private int _Scenario;

		// Token: 0x040007F6 RID: 2038
		public bool HasGameType;

		// Token: 0x040007F7 RID: 2039
		private GameType _GameType;

		// Token: 0x040007F8 RID: 2040
		public bool HasFormatType;

		// Token: 0x040007F9 RID: 2041
		private FormatType _FormatType;

		// Token: 0x040007FA RID: 2042
		public bool HasBrawlLibraryItemId;

		// Token: 0x040007FB RID: 2043
		private int _BrawlLibraryItemId;

		// Token: 0x040007FC RID: 2044
		public bool HasLoadGameState;

		// Token: 0x040007FD RID: 2045
		private bool _LoadGameState;
	}
}
