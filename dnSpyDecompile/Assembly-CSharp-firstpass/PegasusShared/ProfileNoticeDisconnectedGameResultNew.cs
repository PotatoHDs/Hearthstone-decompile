using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013B RID: 315
	public class ProfileNoticeDisconnectedGameResultNew : IProtoBuf
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00046D8A File Offset: 0x00044F8A
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x00046D92 File Offset: 0x00044F92
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00046DA2 File Offset: 0x00044FA2
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x00046DAA File Offset: 0x00044FAA
		public int MissionId
		{
			get
			{
				return this._MissionId;
			}
			set
			{
				this._MissionId = value;
				this.HasMissionId = true;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00046DBA File Offset: 0x00044FBA
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00046DC2 File Offset: 0x00044FC2
		public ProfileNoticeDisconnectedGameResultNew.GameResult GameResult_
		{
			get
			{
				return this._GameResult_;
			}
			set
			{
				this._GameResult_ = value;
				this.HasGameResult_ = true;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00046DD2 File Offset: 0x00044FD2
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00046DDA File Offset: 0x00044FDA
		public ProfileNoticeDisconnectedGameResultNew.PlayerResult YourResult
		{
			get
			{
				return this._YourResult;
			}
			set
			{
				this._YourResult = value;
				this.HasYourResult = true;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00046DEA File Offset: 0x00044FEA
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x00046DF2 File Offset: 0x00044FF2
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

		// Token: 0x060014B5 RID: 5301 RVA: 0x00046E04 File Offset: 0x00045004
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameType)
			{
				num ^= this.GameType.GetHashCode();
			}
			if (this.HasMissionId)
			{
				num ^= this.MissionId.GetHashCode();
			}
			if (this.HasGameResult_)
			{
				num ^= this.GameResult_.GetHashCode();
			}
			if (this.HasYourResult)
			{
				num ^= this.YourResult.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00046EB8 File Offset: 0x000450B8
		public override bool Equals(object obj)
		{
			ProfileNoticeDisconnectedGameResultNew profileNoticeDisconnectedGameResultNew = obj as ProfileNoticeDisconnectedGameResultNew;
			return profileNoticeDisconnectedGameResultNew != null && this.HasGameType == profileNoticeDisconnectedGameResultNew.HasGameType && (!this.HasGameType || this.GameType.Equals(profileNoticeDisconnectedGameResultNew.GameType)) && this.HasMissionId == profileNoticeDisconnectedGameResultNew.HasMissionId && (!this.HasMissionId || this.MissionId.Equals(profileNoticeDisconnectedGameResultNew.MissionId)) && this.HasGameResult_ == profileNoticeDisconnectedGameResultNew.HasGameResult_ && (!this.HasGameResult_ || this.GameResult_.Equals(profileNoticeDisconnectedGameResultNew.GameResult_)) && this.HasYourResult == profileNoticeDisconnectedGameResultNew.HasYourResult && (!this.HasYourResult || this.YourResult.Equals(profileNoticeDisconnectedGameResultNew.YourResult)) && this.HasFormatType == profileNoticeDisconnectedGameResultNew.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(profileNoticeDisconnectedGameResultNew.FormatType));
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00046FE6 File Offset: 0x000451E6
		public void Deserialize(Stream stream)
		{
			ProfileNoticeDisconnectedGameResultNew.Deserialize(stream, this);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00046FF0 File Offset: 0x000451F0
		public static ProfileNoticeDisconnectedGameResultNew Deserialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			return ProfileNoticeDisconnectedGameResultNew.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00046FFC File Offset: 0x000451FC
		public static ProfileNoticeDisconnectedGameResultNew DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDisconnectedGameResultNew profileNoticeDisconnectedGameResultNew = new ProfileNoticeDisconnectedGameResultNew();
			ProfileNoticeDisconnectedGameResultNew.DeserializeLengthDelimited(stream, profileNoticeDisconnectedGameResultNew);
			return profileNoticeDisconnectedGameResultNew;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00047018 File Offset: 0x00045218
		public static ProfileNoticeDisconnectedGameResultNew DeserializeLengthDelimited(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeDisconnectedGameResultNew.Deserialize(stream, instance, num);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00047040 File Offset: 0x00045240
		public static ProfileNoticeDisconnectedGameResultNew Deserialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.GameResult_ = ProfileNoticeDisconnectedGameResultNew.GameResult.GR_UNKNOWN;
			instance.YourResult = ProfileNoticeDisconnectedGameResultNew.PlayerResult.PR_UNKNOWN;
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
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.GameResult_ = (ProfileNoticeDisconnectedGameResultNew.GameResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.YourResult = (ProfileNoticeDisconnectedGameResultNew.PlayerResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
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

		// Token: 0x060014BC RID: 5308 RVA: 0x00047147 File Offset: 0x00045347
		public void Serialize(Stream stream)
		{
			ProfileNoticeDisconnectedGameResultNew.Serialize(stream, this);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00047150 File Offset: 0x00045350
		public static void Serialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			if (instance.HasGameType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MissionId));
			}
			if (instance.HasGameResult_)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameResult_));
			}
			if (instance.HasYourResult)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.YourResult));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000471F0 File Offset: 0x000453F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameType));
			}
			if (this.HasMissionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MissionId));
			}
			if (this.HasGameResult_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameResult_));
			}
			if (this.HasYourResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.YourResult));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num;
		}

		// Token: 0x0400064A RID: 1610
		public bool HasGameType;

		// Token: 0x0400064B RID: 1611
		private GameType _GameType;

		// Token: 0x0400064C RID: 1612
		public bool HasMissionId;

		// Token: 0x0400064D RID: 1613
		private int _MissionId;

		// Token: 0x0400064E RID: 1614
		public bool HasGameResult_;

		// Token: 0x0400064F RID: 1615
		private ProfileNoticeDisconnectedGameResultNew.GameResult _GameResult_;

		// Token: 0x04000650 RID: 1616
		public bool HasYourResult;

		// Token: 0x04000651 RID: 1617
		private ProfileNoticeDisconnectedGameResultNew.PlayerResult _YourResult;

		// Token: 0x04000652 RID: 1618
		public bool HasFormatType;

		// Token: 0x04000653 RID: 1619
		private FormatType _FormatType;

		// Token: 0x0200062F RID: 1583
		public enum NoticeID
		{
			// Token: 0x040020B8 RID: 8376
			ID = 23
		}

		// Token: 0x02000630 RID: 1584
		public enum GameResult
		{
			// Token: 0x040020BA RID: 8378
			GR_UNKNOWN,
			// Token: 0x040020BB RID: 8379
			GR_PLAYING,
			// Token: 0x040020BC RID: 8380
			GR_WINNER,
			// Token: 0x040020BD RID: 8381
			GR_TIE
		}

		// Token: 0x02000631 RID: 1585
		public enum PlayerResult
		{
			// Token: 0x040020BF RID: 8383
			PR_UNKNOWN,
			// Token: 0x040020C0 RID: 8384
			PR_WON,
			// Token: 0x040020C1 RID: 8385
			PR_LOST,
			// Token: 0x040020C2 RID: 8386
			PR_DISCONNECTED,
			// Token: 0x040020C3 RID: 8387
			PR_QUIT
		}
	}
}
