using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerRewardTrackState : IProtoBuf
	{
		public bool HasRewardTrackId;

		private int _RewardTrackId;

		public bool HasLevel;

		private int _Level;

		public bool HasXp;

		private int _Xp;

		private List<PlayerRewardTrackLevelState> _TrackLevel = new List<PlayerRewardTrackLevelState>();

		public bool HasIsActiveRewardTrack;

		private bool _IsActiveRewardTrack;

		public int RewardTrackId
		{
			get
			{
				return _RewardTrackId;
			}
			set
			{
				_RewardTrackId = value;
				HasRewardTrackId = true;
			}
		}

		public int Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
			}
		}

		public int Xp
		{
			get
			{
				return _Xp;
			}
			set
			{
				_Xp = value;
				HasXp = true;
			}
		}

		public List<PlayerRewardTrackLevelState> TrackLevel
		{
			get
			{
				return _TrackLevel;
			}
			set
			{
				_TrackLevel = value;
			}
		}

		public bool IsActiveRewardTrack
		{
			get
			{
				return _IsActiveRewardTrack;
			}
			set
			{
				_IsActiveRewardTrack = value;
				HasIsActiveRewardTrack = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardTrackId)
			{
				num ^= RewardTrackId.GetHashCode();
			}
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			if (HasXp)
			{
				num ^= Xp.GetHashCode();
			}
			foreach (PlayerRewardTrackLevelState item in TrackLevel)
			{
				num ^= item.GetHashCode();
			}
			if (HasIsActiveRewardTrack)
			{
				num ^= IsActiveRewardTrack.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerRewardTrackState playerRewardTrackState = obj as PlayerRewardTrackState;
			if (playerRewardTrackState == null)
			{
				return false;
			}
			if (HasRewardTrackId != playerRewardTrackState.HasRewardTrackId || (HasRewardTrackId && !RewardTrackId.Equals(playerRewardTrackState.RewardTrackId)))
			{
				return false;
			}
			if (HasLevel != playerRewardTrackState.HasLevel || (HasLevel && !Level.Equals(playerRewardTrackState.Level)))
			{
				return false;
			}
			if (HasXp != playerRewardTrackState.HasXp || (HasXp && !Xp.Equals(playerRewardTrackState.Xp)))
			{
				return false;
			}
			if (TrackLevel.Count != playerRewardTrackState.TrackLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < TrackLevel.Count; i++)
			{
				if (!TrackLevel[i].Equals(playerRewardTrackState.TrackLevel[i]))
				{
					return false;
				}
			}
			if (HasIsActiveRewardTrack != playerRewardTrackState.HasIsActiveRewardTrack || (HasIsActiveRewardTrack && !IsActiveRewardTrack.Equals(playerRewardTrackState.IsActiveRewardTrack)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerRewardTrackState Deserialize(Stream stream, PlayerRewardTrackState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerRewardTrackState DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackState playerRewardTrackState = new PlayerRewardTrackState();
			DeserializeLengthDelimited(stream, playerRewardTrackState);
			return playerRewardTrackState;
		}

		public static PlayerRewardTrackState DeserializeLengthDelimited(Stream stream, PlayerRewardTrackState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerRewardTrackState Deserialize(Stream stream, PlayerRewardTrackState instance, long limit)
		{
			if (instance.TrackLevel == null)
			{
				instance.TrackLevel = new List<PlayerRewardTrackLevelState>();
			}
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
					instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Level = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Xp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.TrackLevel.Add(PlayerRewardTrackLevelState.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.IsActiveRewardTrack = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PlayerRewardTrackState instance)
		{
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardTrackId);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasXp)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Xp);
			}
			if (instance.TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState item in instance.TrackLevel)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					PlayerRewardTrackLevelState.Serialize(stream, item);
				}
			}
			if (instance.HasIsActiveRewardTrack)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsActiveRewardTrack);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardTrackId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardTrackId);
			}
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
			}
			if (HasXp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Xp);
			}
			if (TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState item in TrackLevel)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasIsActiveRewardTrack)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
