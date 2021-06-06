using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000389 RID: 905
	public class GameRequestEntry : IProtoBuf
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000BBE6B File Offset: 0x000BA06B
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x000BBE73 File Offset: 0x000BA073
		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000BBE83 File Offset: 0x000BA083
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000BBE8C File Offset: 0x000BA08C
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000BBE94 File Offset: 0x000BA094
		public uint NumGames
		{
			get
			{
				return this._NumGames;
			}
			set
			{
				this._NumGames = value;
				this.HasNumGames = true;
			}
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000BBEA4 File Offset: 0x000BA0A4
		public void SetNumGames(uint val)
		{
			this.NumGames = val;
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000BBEAD File Offset: 0x000BA0AD
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000BBEB5 File Offset: 0x000BA0B5
		public uint ServerCost
		{
			get
			{
				return this._ServerCost;
			}
			set
			{
				this._ServerCost = value;
				this.HasServerCost = true;
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000BBEC5 File Offset: 0x000BA0C5
		public void SetServerCost(uint val)
		{
			this.ServerCost = val;
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000BBED0 File Offset: 0x000BA0D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasNumGames)
			{
				num ^= this.NumGames.GetHashCode();
			}
			if (this.HasServerCost)
			{
				num ^= this.ServerCost.GetHashCode();
			}
			return num;
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000BBF38 File Offset: 0x000BA138
		public override bool Equals(object obj)
		{
			GameRequestEntry gameRequestEntry = obj as GameRequestEntry;
			return gameRequestEntry != null && this.HasFactoryId == gameRequestEntry.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(gameRequestEntry.FactoryId)) && this.HasNumGames == gameRequestEntry.HasNumGames && (!this.HasNumGames || this.NumGames.Equals(gameRequestEntry.NumGames)) && this.HasServerCost == gameRequestEntry.HasServerCost && (!this.HasServerCost || this.ServerCost.Equals(gameRequestEntry.ServerCost));
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000BBFDC File Offset: 0x000BA1DC
		public static GameRequestEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameRequestEntry>(bs, 0, -1);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000BBFE6 File Offset: 0x000BA1E6
		public void Deserialize(Stream stream)
		{
			GameRequestEntry.Deserialize(stream, this);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000BBFF0 File Offset: 0x000BA1F0
		public static GameRequestEntry Deserialize(Stream stream, GameRequestEntry instance)
		{
			return GameRequestEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		public static GameRequestEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRequestEntry gameRequestEntry = new GameRequestEntry();
			GameRequestEntry.DeserializeLengthDelimited(stream, gameRequestEntry);
			return gameRequestEntry;
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000BC018 File Offset: 0x000BA218
		public static GameRequestEntry DeserializeLengthDelimited(Stream stream, GameRequestEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRequestEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000BC040 File Offset: 0x000BA240
		public static GameRequestEntry Deserialize(Stream stream, GameRequestEntry instance, long limit)
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
				else if (num != 9)
				{
					if (num != 16)
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
							instance.ServerCost = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.NumGames = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.FactoryId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000BC0F5 File Offset: 0x000BA2F5
		public void Serialize(Stream stream)
		{
			GameRequestEntry.Serialize(stream, this);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000BC100 File Offset: 0x000BA300
		public static void Serialize(Stream stream, GameRequestEntry instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasNumGames)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.NumGames);
			}
			if (instance.HasServerCost)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ServerCost);
			}
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000BC168 File Offset: 0x000BA368
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasNumGames)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.NumGames);
			}
			if (this.HasServerCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ServerCost);
			}
			return num;
		}

		// Token: 0x04001524 RID: 5412
		public bool HasFactoryId;

		// Token: 0x04001525 RID: 5413
		private ulong _FactoryId;

		// Token: 0x04001526 RID: 5414
		public bool HasNumGames;

		// Token: 0x04001527 RID: 5415
		private uint _NumGames;

		// Token: 0x04001528 RID: 5416
		public bool HasServerCost;

		// Token: 0x04001529 RID: 5417
		private uint _ServerCost;
	}
}
