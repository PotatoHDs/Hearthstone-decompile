using System;
using System.IO;
using PegasusShared;

namespace PegasusClient
{
	// Token: 0x02000029 RID: 41
	public class GamePresenceRankData : IProtoBuf
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008DDE File Offset: 0x00006FDE
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00008DE6 File Offset: 0x00006FE6
		public int LeagueId
		{
			get
			{
				return this._LeagueId;
			}
			set
			{
				this._LeagueId = value;
				this.HasLeagueId = true;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008DF6 File Offset: 0x00006FF6
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00008DFE File Offset: 0x00006FFE
		public int StarLevel
		{
			get
			{
				return this._StarLevel;
			}
			set
			{
				this._StarLevel = value;
				this.HasStarLevel = true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008E0E File Offset: 0x0000700E
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008E16 File Offset: 0x00007016
		public int LegendRank
		{
			get
			{
				return this._LegendRank;
			}
			set
			{
				this._LegendRank = value;
				this.HasLegendRank = true;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008E26 File Offset: 0x00007026
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00008E2E File Offset: 0x0000702E
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

		// Token: 0x0600020C RID: 524 RVA: 0x00008E40 File Offset: 0x00007040
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLeagueId)
			{
				num ^= this.LeagueId.GetHashCode();
			}
			if (this.HasStarLevel)
			{
				num ^= this.StarLevel.GetHashCode();
			}
			if (this.HasLegendRank)
			{
				num ^= this.LegendRank.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008EC4 File Offset: 0x000070C4
		public override bool Equals(object obj)
		{
			GamePresenceRankData gamePresenceRankData = obj as GamePresenceRankData;
			return gamePresenceRankData != null && this.HasLeagueId == gamePresenceRankData.HasLeagueId && (!this.HasLeagueId || this.LeagueId.Equals(gamePresenceRankData.LeagueId)) && this.HasStarLevel == gamePresenceRankData.HasStarLevel && (!this.HasStarLevel || this.StarLevel.Equals(gamePresenceRankData.StarLevel)) && this.HasLegendRank == gamePresenceRankData.HasLegendRank && (!this.HasLegendRank || this.LegendRank.Equals(gamePresenceRankData.LegendRank)) && this.HasFormatType == gamePresenceRankData.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(gamePresenceRankData.FormatType));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008FA1 File Offset: 0x000071A1
		public void Deserialize(Stream stream)
		{
			GamePresenceRankData.Deserialize(stream, this);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008FAB File Offset: 0x000071AB
		public static GamePresenceRankData Deserialize(Stream stream, GamePresenceRankData instance)
		{
			return GamePresenceRankData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008FB8 File Offset: 0x000071B8
		public static GamePresenceRankData DeserializeLengthDelimited(Stream stream)
		{
			GamePresenceRankData gamePresenceRankData = new GamePresenceRankData();
			GamePresenceRankData.DeserializeLengthDelimited(stream, gamePresenceRankData);
			return gamePresenceRankData;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008FD4 File Offset: 0x000071D4
		public static GamePresenceRankData DeserializeLengthDelimited(Stream stream, GamePresenceRankData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GamePresenceRankData.Deserialize(stream, instance, num);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008FFC File Offset: 0x000071FC
		public static GamePresenceRankData Deserialize(Stream stream, GamePresenceRankData instance, long limit)
		{
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000213 RID: 531 RVA: 0x000090D7 File Offset: 0x000072D7
		public void Serialize(Stream stream)
		{
			GamePresenceRankData.Serialize(stream, this);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000090E0 File Offset: 0x000072E0
		public static void Serialize(Stream stream, GamePresenceRankData instance)
		{
			if (instance.HasLeagueId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LeagueId));
			}
			if (instance.HasStarLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StarLevel));
			}
			if (instance.HasLegendRank)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LegendRank));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009160 File Offset: 0x00007360
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLeagueId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LeagueId));
			}
			if (this.HasStarLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StarLevel));
			}
			if (this.HasLegendRank)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LegendRank));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num;
		}

		// Token: 0x04000086 RID: 134
		public bool HasLeagueId;

		// Token: 0x04000087 RID: 135
		private int _LeagueId;

		// Token: 0x04000088 RID: 136
		public bool HasStarLevel;

		// Token: 0x04000089 RID: 137
		private int _StarLevel;

		// Token: 0x0400008A RID: 138
		public bool HasLegendRank;

		// Token: 0x0400008B RID: 139
		private int _LegendRank;

		// Token: 0x0400008C RID: 140
		public bool HasFormatType;

		// Token: 0x0400008D RID: 141
		private FormatType _FormatType;
	}
}
