using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeDisconnectedGameResult : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 4
		}

		public enum GameResult
		{
			GR_UNKNOWN,
			GR_PLAYING,
			GR_WINNER,
			GR_TIE
		}

		public enum PlayerResult
		{
			PR_UNKNOWN,
			PR_WON,
			PR_LOST,
			PR_DISCONNECTED,
			PR_QUIT
		}

		public bool HasGameType;

		private GameType _GameType;

		public bool HasMissionId;

		private int _MissionId;

		public bool HasGameResult_;

		private GameResult _GameResult_;

		public bool HasYourResult;

		private PlayerResult _YourResult;

		public bool HasOpponentResult;

		private PlayerResult _OpponentResult;

		public bool HasFormatType;

		private FormatType _FormatType;

		public GameType GameType
		{
			get
			{
				return _GameType;
			}
			set
			{
				_GameType = value;
				HasGameType = true;
			}
		}

		public int MissionId
		{
			get
			{
				return _MissionId;
			}
			set
			{
				_MissionId = value;
				HasMissionId = true;
			}
		}

		public GameResult GameResult_
		{
			get
			{
				return _GameResult_;
			}
			set
			{
				_GameResult_ = value;
				HasGameResult_ = true;
			}
		}

		public PlayerResult YourResult
		{
			get
			{
				return _YourResult;
			}
			set
			{
				_YourResult = value;
				HasYourResult = true;
			}
		}

		public PlayerResult OpponentResult
		{
			get
			{
				return _OpponentResult;
			}
			set
			{
				_OpponentResult = value;
				HasOpponentResult = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasMissionId)
			{
				num ^= MissionId.GetHashCode();
			}
			if (HasGameResult_)
			{
				num ^= GameResult_.GetHashCode();
			}
			if (HasYourResult)
			{
				num ^= YourResult.GetHashCode();
			}
			if (HasOpponentResult)
			{
				num ^= OpponentResult.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeDisconnectedGameResult profileNoticeDisconnectedGameResult = obj as ProfileNoticeDisconnectedGameResult;
			if (profileNoticeDisconnectedGameResult == null)
			{
				return false;
			}
			if (HasGameType != profileNoticeDisconnectedGameResult.HasGameType || (HasGameType && !GameType.Equals(profileNoticeDisconnectedGameResult.GameType)))
			{
				return false;
			}
			if (HasMissionId != profileNoticeDisconnectedGameResult.HasMissionId || (HasMissionId && !MissionId.Equals(profileNoticeDisconnectedGameResult.MissionId)))
			{
				return false;
			}
			if (HasGameResult_ != profileNoticeDisconnectedGameResult.HasGameResult_ || (HasGameResult_ && !GameResult_.Equals(profileNoticeDisconnectedGameResult.GameResult_)))
			{
				return false;
			}
			if (HasYourResult != profileNoticeDisconnectedGameResult.HasYourResult || (HasYourResult && !YourResult.Equals(profileNoticeDisconnectedGameResult.YourResult)))
			{
				return false;
			}
			if (HasOpponentResult != profileNoticeDisconnectedGameResult.HasOpponentResult || (HasOpponentResult && !OpponentResult.Equals(profileNoticeDisconnectedGameResult.OpponentResult)))
			{
				return false;
			}
			if (HasFormatType != profileNoticeDisconnectedGameResult.HasFormatType || (HasFormatType && !FormatType.Equals(profileNoticeDisconnectedGameResult.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeDisconnectedGameResult Deserialize(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeDisconnectedGameResult DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDisconnectedGameResult profileNoticeDisconnectedGameResult = new ProfileNoticeDisconnectedGameResult();
			DeserializeLengthDelimited(stream, profileNoticeDisconnectedGameResult);
			return profileNoticeDisconnectedGameResult;
		}

		public static ProfileNoticeDisconnectedGameResult DeserializeLengthDelimited(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeDisconnectedGameResult Deserialize(Stream stream, ProfileNoticeDisconnectedGameResult instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.GameResult_ = GameResult.GR_UNKNOWN;
			instance.YourResult = PlayerResult.PR_UNKNOWN;
			instance.OpponentResult = PlayerResult.PR_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 64:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.GameResult_ = (GameResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.YourResult = (PlayerResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.OpponentResult = (PlayerResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ProfileNoticeDisconnectedGameResult instance)
		{
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MissionId);
			}
			if (instance.HasGameResult_)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameResult_);
			}
			if (instance.HasYourResult)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.YourResult);
			}
			if (instance.HasOpponentResult)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OpponentResult);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameType);
			}
			if (HasMissionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MissionId);
			}
			if (HasGameResult_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameResult_);
			}
			if (HasYourResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)YourResult);
			}
			if (HasOpponentResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OpponentResult);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num;
		}
	}
}
