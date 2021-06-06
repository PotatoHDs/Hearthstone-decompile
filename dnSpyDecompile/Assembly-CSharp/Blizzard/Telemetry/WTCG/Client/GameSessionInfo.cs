using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F0 RID: 4592
	public class GameSessionInfo : IProtoBuf
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600CD38 RID: 52536 RVA: 0x003D382E File Offset: 0x003D1A2E
		// (set) Token: 0x0600CD39 RID: 52537 RVA: 0x003D3836 File Offset: 0x003D1A36
		public string GameServerIpAddress
		{
			get
			{
				return this._GameServerIpAddress;
			}
			set
			{
				this._GameServerIpAddress = value;
				this.HasGameServerIpAddress = (value != null);
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600CD3A RID: 52538 RVA: 0x003D3849 File Offset: 0x003D1A49
		// (set) Token: 0x0600CD3B RID: 52539 RVA: 0x003D3851 File Offset: 0x003D1A51
		public uint GameServerPort
		{
			get
			{
				return this._GameServerPort;
			}
			set
			{
				this._GameServerPort = value;
				this.HasGameServerPort = true;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600CD3C RID: 52540 RVA: 0x003D3861 File Offset: 0x003D1A61
		// (set) Token: 0x0600CD3D RID: 52541 RVA: 0x003D3869 File Offset: 0x003D1A69
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

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600CD3E RID: 52542 RVA: 0x003D387C File Offset: 0x003D1A7C
		// (set) Token: 0x0600CD3F RID: 52543 RVA: 0x003D3884 File Offset: 0x003D1A84
		public uint GameHandle
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

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600CD40 RID: 52544 RVA: 0x003D3894 File Offset: 0x003D1A94
		// (set) Token: 0x0600CD41 RID: 52545 RVA: 0x003D389C File Offset: 0x003D1A9C
		public int ScenarioId
		{
			get
			{
				return this._ScenarioId;
			}
			set
			{
				this._ScenarioId = value;
				this.HasScenarioId = true;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600CD42 RID: 52546 RVA: 0x003D38AC File Offset: 0x003D1AAC
		// (set) Token: 0x0600CD43 RID: 52547 RVA: 0x003D38B4 File Offset: 0x003D1AB4
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

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600CD44 RID: 52548 RVA: 0x003D38C4 File Offset: 0x003D1AC4
		// (set) Token: 0x0600CD45 RID: 52549 RVA: 0x003D38CC File Offset: 0x003D1ACC
		public int SeasonId
		{
			get
			{
				return this._SeasonId;
			}
			set
			{
				this._SeasonId = value;
				this.HasSeasonId = true;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x0600CD46 RID: 52550 RVA: 0x003D38DC File Offset: 0x003D1ADC
		// (set) Token: 0x0600CD47 RID: 52551 RVA: 0x003D38E4 File Offset: 0x003D1AE4
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

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x0600CD48 RID: 52552 RVA: 0x003D38F4 File Offset: 0x003D1AF4
		// (set) Token: 0x0600CD49 RID: 52553 RVA: 0x003D38FC File Offset: 0x003D1AFC
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

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x0600CD4A RID: 52554 RVA: 0x003D390C File Offset: 0x003D1B0C
		// (set) Token: 0x0600CD4B RID: 52555 RVA: 0x003D3914 File Offset: 0x003D1B14
		public bool IsReconnect
		{
			get
			{
				return this._IsReconnect;
			}
			set
			{
				this._IsReconnect = value;
				this.HasIsReconnect = true;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600CD4C RID: 52556 RVA: 0x003D3924 File Offset: 0x003D1B24
		// (set) Token: 0x0600CD4D RID: 52557 RVA: 0x003D392C File Offset: 0x003D1B2C
		public bool IsSpectating
		{
			get
			{
				return this._IsSpectating;
			}
			set
			{
				this._IsSpectating = value;
				this.HasIsSpectating = true;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600CD4E RID: 52558 RVA: 0x003D393C File Offset: 0x003D1B3C
		// (set) Token: 0x0600CD4F RID: 52559 RVA: 0x003D3944 File Offset: 0x003D1B44
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

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600CD50 RID: 52560 RVA: 0x003D3954 File Offset: 0x003D1B54
		// (set) Token: 0x0600CD51 RID: 52561 RVA: 0x003D395C File Offset: 0x003D1B5C
		public long ClientDeckId
		{
			get
			{
				return this._ClientDeckId;
			}
			set
			{
				this._ClientDeckId = value;
				this.HasClientDeckId = true;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600CD52 RID: 52562 RVA: 0x003D396C File Offset: 0x003D1B6C
		// (set) Token: 0x0600CD53 RID: 52563 RVA: 0x003D3974 File Offset: 0x003D1B74
		public long AiDeckId
		{
			get
			{
				return this._AiDeckId;
			}
			set
			{
				this._AiDeckId = value;
				this.HasAiDeckId = true;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x0600CD54 RID: 52564 RVA: 0x003D3984 File Offset: 0x003D1B84
		// (set) Token: 0x0600CD55 RID: 52565 RVA: 0x003D398C File Offset: 0x003D1B8C
		public long ClientHeroCardId
		{
			get
			{
				return this._ClientHeroCardId;
			}
			set
			{
				this._ClientHeroCardId = value;
				this.HasClientHeroCardId = true;
			}
		}

		// Token: 0x0600CD56 RID: 52566 RVA: 0x003D399C File Offset: 0x003D1B9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameServerIpAddress)
			{
				num ^= this.GameServerIpAddress.GetHashCode();
			}
			if (this.HasGameServerPort)
			{
				num ^= this.GameServerPort.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasScenarioId)
			{
				num ^= this.ScenarioId.GetHashCode();
			}
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			if (this.HasSeasonId)
			{
				num ^= this.SeasonId.GetHashCode();
			}
			if (this.HasGameType)
			{
				num ^= this.GameType.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasIsReconnect)
			{
				num ^= this.IsReconnect.GetHashCode();
			}
			if (this.HasIsSpectating)
			{
				num ^= this.IsSpectating.GetHashCode();
			}
			if (this.HasClientHandle)
			{
				num ^= this.ClientHandle.GetHashCode();
			}
			if (this.HasClientDeckId)
			{
				num ^= this.ClientDeckId.GetHashCode();
			}
			if (this.HasAiDeckId)
			{
				num ^= this.AiDeckId.GetHashCode();
			}
			if (this.HasClientHeroCardId)
			{
				num ^= this.ClientHeroCardId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD57 RID: 52567 RVA: 0x003D3B3C File Offset: 0x003D1D3C
		public override bool Equals(object obj)
		{
			GameSessionInfo gameSessionInfo = obj as GameSessionInfo;
			return gameSessionInfo != null && this.HasGameServerIpAddress == gameSessionInfo.HasGameServerIpAddress && (!this.HasGameServerIpAddress || this.GameServerIpAddress.Equals(gameSessionInfo.GameServerIpAddress)) && this.HasGameServerPort == gameSessionInfo.HasGameServerPort && (!this.HasGameServerPort || this.GameServerPort.Equals(gameSessionInfo.GameServerPort)) && this.HasVersion == gameSessionInfo.HasVersion && (!this.HasVersion || this.Version.Equals(gameSessionInfo.Version)) && this.HasGameHandle == gameSessionInfo.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(gameSessionInfo.GameHandle)) && this.HasScenarioId == gameSessionInfo.HasScenarioId && (!this.HasScenarioId || this.ScenarioId.Equals(gameSessionInfo.ScenarioId)) && this.HasBrawlLibraryItemId == gameSessionInfo.HasBrawlLibraryItemId && (!this.HasBrawlLibraryItemId || this.BrawlLibraryItemId.Equals(gameSessionInfo.BrawlLibraryItemId)) && this.HasSeasonId == gameSessionInfo.HasSeasonId && (!this.HasSeasonId || this.SeasonId.Equals(gameSessionInfo.SeasonId)) && this.HasGameType == gameSessionInfo.HasGameType && (!this.HasGameType || this.GameType.Equals(gameSessionInfo.GameType)) && this.HasFormatType == gameSessionInfo.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(gameSessionInfo.FormatType)) && this.HasIsReconnect == gameSessionInfo.HasIsReconnect && (!this.HasIsReconnect || this.IsReconnect.Equals(gameSessionInfo.IsReconnect)) && this.HasIsSpectating == gameSessionInfo.HasIsSpectating && (!this.HasIsSpectating || this.IsSpectating.Equals(gameSessionInfo.IsSpectating)) && this.HasClientHandle == gameSessionInfo.HasClientHandle && (!this.HasClientHandle || this.ClientHandle.Equals(gameSessionInfo.ClientHandle)) && this.HasClientDeckId == gameSessionInfo.HasClientDeckId && (!this.HasClientDeckId || this.ClientDeckId.Equals(gameSessionInfo.ClientDeckId)) && this.HasAiDeckId == gameSessionInfo.HasAiDeckId && (!this.HasAiDeckId || this.AiDeckId.Equals(gameSessionInfo.AiDeckId)) && this.HasClientHeroCardId == gameSessionInfo.HasClientHeroCardId && (!this.HasClientHeroCardId || this.ClientHeroCardId.Equals(gameSessionInfo.ClientHeroCardId));
		}

		// Token: 0x0600CD58 RID: 52568 RVA: 0x003D3E1F File Offset: 0x003D201F
		public void Deserialize(Stream stream)
		{
			GameSessionInfo.Deserialize(stream, this);
		}

		// Token: 0x0600CD59 RID: 52569 RVA: 0x003D3E29 File Offset: 0x003D2029
		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance)
		{
			return GameSessionInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD5A RID: 52570 RVA: 0x003D3E34 File Offset: 0x003D2034
		public static GameSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionInfo gameSessionInfo = new GameSessionInfo();
			GameSessionInfo.DeserializeLengthDelimited(stream, gameSessionInfo);
			return gameSessionInfo;
		}

		// Token: 0x0600CD5B RID: 52571 RVA: 0x003D3E50 File Offset: 0x003D2050
		public static GameSessionInfo DeserializeLengthDelimited(Stream stream, GameSessionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD5C RID: 52572 RVA: 0x003D3E78 File Offset: 0x003D2078
		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance, long limit)
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
					if (num <= 56)
					{
						if (num <= 26)
						{
							if (num == 10)
							{
								instance.GameServerIpAddress = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.GameServerPort = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 26)
							{
								instance.Version = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.GameHandle = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 40)
							{
								instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 56)
							{
								instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 72)
							{
								instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.IsReconnect = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 88)
							{
								instance.IsSpectating = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.ClientDeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.AiDeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 120)
						{
							instance.ClientHeroCardId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CD5D RID: 52573 RVA: 0x003D40B9 File Offset: 0x003D22B9
		public void Serialize(Stream stream)
		{
			GameSessionInfo.Serialize(stream, this);
		}

		// Token: 0x0600CD5E RID: 52574 RVA: 0x003D40C4 File Offset: 0x003D22C4
		public static void Serialize(Stream stream, GameSessionInfo instance)
		{
			if (instance.HasGameServerIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameServerIpAddress));
			}
			if (instance.HasGameServerPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.GameServerPort);
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle);
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
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
			if (instance.HasIsReconnect)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.IsReconnect);
			}
			if (instance.HasIsSpectating)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.IsSpectating);
			}
			if (instance.HasClientHandle)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			}
			if (instance.HasClientDeckId)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientDeckId);
			}
			if (instance.HasAiDeckId)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AiDeckId);
			}
			if (instance.HasClientHeroCardId)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHeroCardId);
			}
		}

		// Token: 0x0600CD5F RID: 52575 RVA: 0x003D4290 File Offset: 0x003D2490
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameServerIpAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.GameServerIpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasGameServerPort)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.GameServerPort);
			}
			if (this.HasVersion)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Version);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.GameHandle);
			}
			if (this.HasScenarioId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId));
			}
			if (this.HasBrawlLibraryItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			if (this.HasSeasonId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
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
			if (this.HasIsReconnect)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsSpectating)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasClientHandle)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientHandle);
			}
			if (this.HasClientDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientDeckId);
			}
			if (this.HasAiDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.AiDeckId);
			}
			if (this.HasClientHeroCardId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientHeroCardId);
			}
			return num;
		}

		// Token: 0x0400A0E1 RID: 41185
		public bool HasGameServerIpAddress;

		// Token: 0x0400A0E2 RID: 41186
		private string _GameServerIpAddress;

		// Token: 0x0400A0E3 RID: 41187
		public bool HasGameServerPort;

		// Token: 0x0400A0E4 RID: 41188
		private uint _GameServerPort;

		// Token: 0x0400A0E5 RID: 41189
		public bool HasVersion;

		// Token: 0x0400A0E6 RID: 41190
		private string _Version;

		// Token: 0x0400A0E7 RID: 41191
		public bool HasGameHandle;

		// Token: 0x0400A0E8 RID: 41192
		private uint _GameHandle;

		// Token: 0x0400A0E9 RID: 41193
		public bool HasScenarioId;

		// Token: 0x0400A0EA RID: 41194
		private int _ScenarioId;

		// Token: 0x0400A0EB RID: 41195
		public bool HasBrawlLibraryItemId;

		// Token: 0x0400A0EC RID: 41196
		private int _BrawlLibraryItemId;

		// Token: 0x0400A0ED RID: 41197
		public bool HasSeasonId;

		// Token: 0x0400A0EE RID: 41198
		private int _SeasonId;

		// Token: 0x0400A0EF RID: 41199
		public bool HasGameType;

		// Token: 0x0400A0F0 RID: 41200
		private GameType _GameType;

		// Token: 0x0400A0F1 RID: 41201
		public bool HasFormatType;

		// Token: 0x0400A0F2 RID: 41202
		private FormatType _FormatType;

		// Token: 0x0400A0F3 RID: 41203
		public bool HasIsReconnect;

		// Token: 0x0400A0F4 RID: 41204
		private bool _IsReconnect;

		// Token: 0x0400A0F5 RID: 41205
		public bool HasIsSpectating;

		// Token: 0x0400A0F6 RID: 41206
		private bool _IsSpectating;

		// Token: 0x0400A0F7 RID: 41207
		public bool HasClientHandle;

		// Token: 0x0400A0F8 RID: 41208
		private long _ClientHandle;

		// Token: 0x0400A0F9 RID: 41209
		public bool HasClientDeckId;

		// Token: 0x0400A0FA RID: 41210
		private long _ClientDeckId;

		// Token: 0x0400A0FB RID: 41211
		public bool HasAiDeckId;

		// Token: 0x0400A0FC RID: 41212
		private long _AiDeckId;

		// Token: 0x0400A0FD RID: 41213
		public bool HasClientHeroCardId;

		// Token: 0x0400A0FE RID: 41214
		private long _ClientHeroCardId;
	}
}
