using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerRewardTrackStateUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 614,
			System = 0
		}

		public bool HasRewardTrackId;

		private int _RewardTrackId;

		public bool HasLevel;

		private int _Level;

		public bool HasXp;

		private int _Xp;

		private List<PlayerRewardTrackLevelState> _TrackLevel = new List<PlayerRewardTrackLevelState>();

		private List<PlayerRewardTrackState> _State = new List<PlayerRewardTrackState>();

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

		public List<PlayerRewardTrackState> State
		{
			get
			{
				return _State;
			}
			set
			{
				_State = value;
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
			foreach (PlayerRewardTrackState item2 in State)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = obj as PlayerRewardTrackStateUpdate;
			if (playerRewardTrackStateUpdate == null)
			{
				return false;
			}
			if (HasRewardTrackId != playerRewardTrackStateUpdate.HasRewardTrackId || (HasRewardTrackId && !RewardTrackId.Equals(playerRewardTrackStateUpdate.RewardTrackId)))
			{
				return false;
			}
			if (HasLevel != playerRewardTrackStateUpdate.HasLevel || (HasLevel && !Level.Equals(playerRewardTrackStateUpdate.Level)))
			{
				return false;
			}
			if (HasXp != playerRewardTrackStateUpdate.HasXp || (HasXp && !Xp.Equals(playerRewardTrackStateUpdate.Xp)))
			{
				return false;
			}
			if (TrackLevel.Count != playerRewardTrackStateUpdate.TrackLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < TrackLevel.Count; i++)
			{
				if (!TrackLevel[i].Equals(playerRewardTrackStateUpdate.TrackLevel[i]))
				{
					return false;
				}
			}
			if (State.Count != playerRewardTrackStateUpdate.State.Count)
			{
				return false;
			}
			for (int j = 0; j < State.Count; j++)
			{
				if (!State[j].Equals(playerRewardTrackStateUpdate.State[j]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerRewardTrackStateUpdate Deserialize(Stream stream, PlayerRewardTrackStateUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerRewardTrackStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = new PlayerRewardTrackStateUpdate();
			DeserializeLengthDelimited(stream, playerRewardTrackStateUpdate);
			return playerRewardTrackStateUpdate;
		}

		public static PlayerRewardTrackStateUpdate DeserializeLengthDelimited(Stream stream, PlayerRewardTrackStateUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerRewardTrackStateUpdate Deserialize(Stream stream, PlayerRewardTrackStateUpdate instance, long limit)
		{
			if (instance.TrackLevel == null)
			{
				instance.TrackLevel = new List<PlayerRewardTrackLevelState>();
			}
			if (instance.State == null)
			{
				instance.State = new List<PlayerRewardTrackState>();
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
				case 42:
					instance.State.Add(PlayerRewardTrackState.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PlayerRewardTrackStateUpdate instance)
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
			if (instance.State.Count <= 0)
			{
				return;
			}
			foreach (PlayerRewardTrackState item2 in instance.State)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				PlayerRewardTrackState.Serialize(stream, item2);
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
			if (State.Count > 0)
			{
				foreach (PlayerRewardTrackState item2 in State)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
