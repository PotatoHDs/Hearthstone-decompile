using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012A RID: 298
	public class ProfileNoticeDisconnectedGameResult : IProtoBuf
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000445AA File Offset: 0x000427AA
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x000445B2 File Offset: 0x000427B2
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000445C2 File Offset: 0x000427C2
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x000445CA File Offset: 0x000427CA
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000445DA File Offset: 0x000427DA
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000445E2 File Offset: 0x000427E2
		public ProfileNoticeDisconnectedGameResult.GameResult GameResult_
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

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000445F2 File Offset: 0x000427F2
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x000445FA File Offset: 0x000427FA
		public ProfileNoticeDisconnectedGameResult.PlayerResult YourResult
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0004460A File Offset: 0x0004280A
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x00044612 File Offset: 0x00042812
		public ProfileNoticeDisconnectedGameResult.PlayerResult OpponentResult
		{
			get
			{
				return this._OpponentResult;
			}
			set
			{
				this._OpponentResult = value;
				this.HasOpponentResult = true;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00044622 File Offset: 0x00042822
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x0004462A File Offset: 0x0004282A
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

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004463C File Offset: 0x0004283C
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
			if (this.HasOpponentResult)
			{
				num ^= this.OpponentResult.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00044710 File Offset: 0x00042910
		public override bool Equals(object obj)
		{
			ProfileNoticeDisconnectedGameResult profileNoticeDisconnectedGameResult = obj as ProfileNoticeDisconnectedGameResult;
			return profileNoticeDisconnectedGameResult != null && this.HasGameType == profileNoticeDisconnectedGameResult.HasGameType && (!this.HasGameType || this.GameType.Equals(profileNoticeDisconnectedGameResult.GameType)) && this.HasMissionId == profileNoticeDisconnectedGameResult.HasMissionId && (!this.HasMissionId || this.MissionId.Equals(profileNoticeDisconnectedGameResult.MissionId)) && this.HasGameResult_ == profileNoticeDisconnectedGameResult.HasGameResult_ && (!this.HasGameResult_ || this.GameResult_.Equals(profileNoticeDisconnectedGameResult.GameResult_)) && this.HasYourResult == profileNoticeDisconnectedGameResult.HasYourResult && (!this.HasYourResult || this.YourResult.Equals(profileNoticeDisconnectedGameResult.YourResult)) && this.HasOpponentResult == profileNoticeDisconnectedGameResult.HasOpponentResult && (!this.HasOpponentResult || this.OpponentResult.Equals(profileNoticeDisconnectedGameResult.OpponentResult)) && this.HasFormatType == profileNoticeDisconnectedGameResult.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(profileNoticeDisconnectedGameResult.FormatType));
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00044878 File Offset: 0x00042A78
		public void Deserialize(Stream stream)
		{
			ProfileNoticeDisconnectedGameResult.Deserialize(stream, this);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00044882 File Offset: 0x00042A82
		public static ProfileNoticeDisconnectedGameResult Deserialize(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			return ProfileNoticeDisconnectedGameResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00044890 File Offset: 0x00042A90
		public static ProfileNoticeDisconnectedGameResult DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDisconnectedGameResult profileNoticeDisconnectedGameResult = new ProfileNoticeDisconnectedGameResult();
			ProfileNoticeDisconnectedGameResult.DeserializeLengthDelimited(stream, profileNoticeDisconnectedGameResult);
			return profileNoticeDisconnectedGameResult;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000448AC File Offset: 0x00042AAC
		public static ProfileNoticeDisconnectedGameResult DeserializeLengthDelimited(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeDisconnectedGameResult.Deserialize(stream, instance, num);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000448D4 File Offset: 0x00042AD4
		public static ProfileNoticeDisconnectedGameResult Deserialize(Stream stream, ProfileNoticeDisconnectedGameResult instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.GameResult_ = ProfileNoticeDisconnectedGameResult.GameResult.GR_UNKNOWN;
			instance.YourResult = ProfileNoticeDisconnectedGameResult.PlayerResult.PR_UNKNOWN;
			instance.OpponentResult = ProfileNoticeDisconnectedGameResult.PlayerResult.PR_UNKNOWN;
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
					if (num <= 80)
					{
						if (num == 64)
						{
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 80)
						{
							instance.GameResult_ = (ProfileNoticeDisconnectedGameResult.GameResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 88)
						{
							instance.YourResult = (ProfileNoticeDisconnectedGameResult.PlayerResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 96)
						{
							instance.OpponentResult = (ProfileNoticeDisconnectedGameResult.PlayerResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
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

		// Token: 0x060013B9 RID: 5049 RVA: 0x000449FD File Offset: 0x00042BFD
		public void Serialize(Stream stream)
		{
			ProfileNoticeDisconnectedGameResult.Serialize(stream, this);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00044A08 File Offset: 0x00042C08
		public static void Serialize(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MissionId));
			}
			if (instance.HasGameResult_)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameResult_));
			}
			if (instance.HasYourResult)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.YourResult));
			}
			if (instance.HasOpponentResult)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OpponentResult));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00044AC4 File Offset: 0x00042CC4
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
			if (this.HasOpponentResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OpponentResult));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num;
		}

		// Token: 0x04000616 RID: 1558
		public bool HasGameType;

		// Token: 0x04000617 RID: 1559
		private GameType _GameType;

		// Token: 0x04000618 RID: 1560
		public bool HasMissionId;

		// Token: 0x04000619 RID: 1561
		private int _MissionId;

		// Token: 0x0400061A RID: 1562
		public bool HasGameResult_;

		// Token: 0x0400061B RID: 1563
		private ProfileNoticeDisconnectedGameResult.GameResult _GameResult_;

		// Token: 0x0400061C RID: 1564
		public bool HasYourResult;

		// Token: 0x0400061D RID: 1565
		private ProfileNoticeDisconnectedGameResult.PlayerResult _YourResult;

		// Token: 0x0400061E RID: 1566
		public bool HasOpponentResult;

		// Token: 0x0400061F RID: 1567
		private ProfileNoticeDisconnectedGameResult.PlayerResult _OpponentResult;

		// Token: 0x04000620 RID: 1568
		public bool HasFormatType;

		// Token: 0x04000621 RID: 1569
		private FormatType _FormatType;

		// Token: 0x0200061C RID: 1564
		public enum NoticeID
		{
			// Token: 0x0400208B RID: 8331
			ID = 4
		}

		// Token: 0x0200061D RID: 1565
		public enum GameResult
		{
			// Token: 0x0400208D RID: 8333
			GR_UNKNOWN,
			// Token: 0x0400208E RID: 8334
			GR_PLAYING,
			// Token: 0x0400208F RID: 8335
			GR_WINNER,
			// Token: 0x04002090 RID: 8336
			GR_TIE
		}

		// Token: 0x0200061E RID: 1566
		public enum PlayerResult
		{
			// Token: 0x04002092 RID: 8338
			PR_UNKNOWN,
			// Token: 0x04002093 RID: 8339
			PR_WON,
			// Token: 0x04002094 RID: 8340
			PR_LOST,
			// Token: 0x04002095 RID: 8341
			PR_DISCONNECTED,
			// Token: 0x04002096 RID: 8342
			PR_QUIT
		}
	}
}
