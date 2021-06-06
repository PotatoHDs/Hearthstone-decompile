using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053C RID: 1340
	public class GameAccountState : IProtoBuf
	{
		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x060060B0 RID: 24752 RVA: 0x00124B22 File Offset: 0x00122D22
		// (set) Token: 0x060060B1 RID: 24753 RVA: 0x00124B2A File Offset: 0x00122D2A
		public GameLevelInfo GameLevelInfo
		{
			get
			{
				return this._GameLevelInfo;
			}
			set
			{
				this._GameLevelInfo = value;
				this.HasGameLevelInfo = (value != null);
			}
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x00124B3D File Offset: 0x00122D3D
		public void SetGameLevelInfo(GameLevelInfo val)
		{
			this.GameLevelInfo = val;
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x00124B46 File Offset: 0x00122D46
		// (set) Token: 0x060060B4 RID: 24756 RVA: 0x00124B4E File Offset: 0x00122D4E
		public GameTimeInfo GameTimeInfo
		{
			get
			{
				return this._GameTimeInfo;
			}
			set
			{
				this._GameTimeInfo = value;
				this.HasGameTimeInfo = (value != null);
			}
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x00124B61 File Offset: 0x00122D61
		public void SetGameTimeInfo(GameTimeInfo val)
		{
			this.GameTimeInfo = val;
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x060060B6 RID: 24758 RVA: 0x00124B6A File Offset: 0x00122D6A
		// (set) Token: 0x060060B7 RID: 24759 RVA: 0x00124B72 File Offset: 0x00122D72
		public GameStatus GameStatus
		{
			get
			{
				return this._GameStatus;
			}
			set
			{
				this._GameStatus = value;
				this.HasGameStatus = (value != null);
			}
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x00124B85 File Offset: 0x00122D85
		public void SetGameStatus(GameStatus val)
		{
			this.GameStatus = val;
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x00124B8E File Offset: 0x00122D8E
		// (set) Token: 0x060060BA RID: 24762 RVA: 0x00124B96 File Offset: 0x00122D96
		public RAFInfo RafInfo
		{
			get
			{
				return this._RafInfo;
			}
			set
			{
				this._RafInfo = value;
				this.HasRafInfo = (value != null);
			}
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x00124BA9 File Offset: 0x00122DA9
		public void SetRafInfo(RAFInfo val)
		{
			this.RafInfo = val;
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x00124BB4 File Offset: 0x00122DB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameLevelInfo)
			{
				num ^= this.GameLevelInfo.GetHashCode();
			}
			if (this.HasGameTimeInfo)
			{
				num ^= this.GameTimeInfo.GetHashCode();
			}
			if (this.HasGameStatus)
			{
				num ^= this.GameStatus.GetHashCode();
			}
			if (this.HasRafInfo)
			{
				num ^= this.RafInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x00124C28 File Offset: 0x00122E28
		public override bool Equals(object obj)
		{
			GameAccountState gameAccountState = obj as GameAccountState;
			return gameAccountState != null && this.HasGameLevelInfo == gameAccountState.HasGameLevelInfo && (!this.HasGameLevelInfo || this.GameLevelInfo.Equals(gameAccountState.GameLevelInfo)) && this.HasGameTimeInfo == gameAccountState.HasGameTimeInfo && (!this.HasGameTimeInfo || this.GameTimeInfo.Equals(gameAccountState.GameTimeInfo)) && this.HasGameStatus == gameAccountState.HasGameStatus && (!this.HasGameStatus || this.GameStatus.Equals(gameAccountState.GameStatus)) && this.HasRafInfo == gameAccountState.HasRafInfo && (!this.HasRafInfo || this.RafInfo.Equals(gameAccountState.RafInfo));
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x00124CEE File Offset: 0x00122EEE
		public static GameAccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountState>(bs, 0, -1);
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x00124CF8 File Offset: 0x00122EF8
		public void Deserialize(Stream stream)
		{
			GameAccountState.Deserialize(stream, this);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x00124D02 File Offset: 0x00122F02
		public static GameAccountState Deserialize(Stream stream, GameAccountState instance)
		{
			return GameAccountState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x00124D10 File Offset: 0x00122F10
		public static GameAccountState DeserializeLengthDelimited(Stream stream)
		{
			GameAccountState gameAccountState = new GameAccountState();
			GameAccountState.DeserializeLengthDelimited(stream, gameAccountState);
			return gameAccountState;
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x00124D2C File Offset: 0x00122F2C
		public static GameAccountState DeserializeLengthDelimited(Stream stream, GameAccountState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountState.Deserialize(stream, instance, num);
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x00124D54 File Offset: 0x00122F54
		public static GameAccountState Deserialize(Stream stream, GameAccountState instance, long limit)
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
								if (instance.GameTimeInfo == null)
								{
									instance.GameTimeInfo = GameTimeInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								GameTimeInfo.DeserializeLengthDelimited(stream, instance.GameTimeInfo);
								continue;
							}
						}
						else
						{
							if (instance.GameLevelInfo == null)
							{
								instance.GameLevelInfo = GameLevelInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							GameLevelInfo.DeserializeLengthDelimited(stream, instance.GameLevelInfo);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							if (instance.RafInfo == null)
							{
								instance.RafInfo = RAFInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							RAFInfo.DeserializeLengthDelimited(stream, instance.RafInfo);
							continue;
						}
					}
					else
					{
						if (instance.GameStatus == null)
						{
							instance.GameStatus = GameStatus.DeserializeLengthDelimited(stream);
							continue;
						}
						GameStatus.DeserializeLengthDelimited(stream, instance.GameStatus);
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

		// Token: 0x060060C5 RID: 24773 RVA: 0x00124E99 File Offset: 0x00123099
		public void Serialize(Stream stream)
		{
			GameAccountState.Serialize(stream, this);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x00124EA4 File Offset: 0x001230A4
		public static void Serialize(Stream stream, GameAccountState instance)
		{
			if (instance.HasGameLevelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameLevelInfo.GetSerializedSize());
				GameLevelInfo.Serialize(stream, instance.GameLevelInfo);
			}
			if (instance.HasGameTimeInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeInfo.GetSerializedSize());
				GameTimeInfo.Serialize(stream, instance.GameTimeInfo);
			}
			if (instance.HasGameStatus)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameStatus.GetSerializedSize());
				GameStatus.Serialize(stream, instance.GameStatus);
			}
			if (instance.HasRafInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RafInfo.GetSerializedSize());
				RAFInfo.Serialize(stream, instance.RafInfo);
			}
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x00124F68 File Offset: 0x00123168
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameLevelInfo)
			{
				num += 1U;
				uint serializedSize = this.GameLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameTimeInfo)
			{
				num += 1U;
				uint serializedSize2 = this.GameTimeInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameStatus)
			{
				num += 1U;
				uint serializedSize3 = this.GameStatus.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasRafInfo)
			{
				num += 1U;
				uint serializedSize4 = this.RafInfo.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001DC3 RID: 7619
		public bool HasGameLevelInfo;

		// Token: 0x04001DC4 RID: 7620
		private GameLevelInfo _GameLevelInfo;

		// Token: 0x04001DC5 RID: 7621
		public bool HasGameTimeInfo;

		// Token: 0x04001DC6 RID: 7622
		private GameTimeInfo _GameTimeInfo;

		// Token: 0x04001DC7 RID: 7623
		public bool HasGameStatus;

		// Token: 0x04001DC8 RID: 7624
		private GameStatus _GameStatus;

		// Token: 0x04001DC9 RID: 7625
		public bool HasRafInfo;

		// Token: 0x04001DCA RID: 7626
		private RAFInfo _RafInfo;
	}
}
