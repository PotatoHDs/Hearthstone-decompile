using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeDisconnectedGameResultNew : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 23
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
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeDisconnectedGameResultNew profileNoticeDisconnectedGameResultNew = obj as ProfileNoticeDisconnectedGameResultNew;
			if (profileNoticeDisconnectedGameResultNew == null)
			{
				return false;
			}
			if (HasGameType != profileNoticeDisconnectedGameResultNew.HasGameType || (HasGameType && !GameType.Equals(profileNoticeDisconnectedGameResultNew.GameType)))
			{
				return false;
			}
			if (HasMissionId != profileNoticeDisconnectedGameResultNew.HasMissionId || (HasMissionId && !MissionId.Equals(profileNoticeDisconnectedGameResultNew.MissionId)))
			{
				return false;
			}
			if (HasGameResult_ != profileNoticeDisconnectedGameResultNew.HasGameResult_ || (HasGameResult_ && !GameResult_.Equals(profileNoticeDisconnectedGameResultNew.GameResult_)))
			{
				return false;
			}
			if (HasYourResult != profileNoticeDisconnectedGameResultNew.HasYourResult || (HasYourResult && !YourResult.Equals(profileNoticeDisconnectedGameResultNew.YourResult)))
			{
				return false;
			}
			if (HasFormatType != profileNoticeDisconnectedGameResultNew.HasFormatType || (HasFormatType && !FormatType.Equals(profileNoticeDisconnectedGameResultNew.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeDisconnectedGameResultNew Deserialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeDisconnectedGameResultNew DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDisconnectedGameResultNew profileNoticeDisconnectedGameResultNew = new ProfileNoticeDisconnectedGameResultNew();
			DeserializeLengthDelimited(stream, profileNoticeDisconnectedGameResultNew);
			return profileNoticeDisconnectedGameResultNew;
		}

		public static ProfileNoticeDisconnectedGameResultNew DeserializeLengthDelimited(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeDisconnectedGameResultNew Deserialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.GameResult_ = GameResult.GR_UNKNOWN;
			instance.YourResult = PlayerResult.PR_UNKNOWN;
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
				case 8:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.GameResult_ = (GameResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.YourResult = (PlayerResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
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

		public static void Serialize(Stream stream, ProfileNoticeDisconnectedGameResultNew instance)
		{
			if (instance.HasGameType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MissionId);
			}
			if (instance.HasGameResult_)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameResult_);
			}
			if (instance.HasYourResult)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.YourResult);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(40);
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
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num;
		}
	}
}
