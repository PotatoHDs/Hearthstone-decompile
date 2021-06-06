using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerAchievementStateUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 603,
			System = 0
		}

		private List<PlayerAchievementState> _Achievement = new List<PlayerAchievementState>();

		public List<PlayerAchievementState> Achievement
		{
			get
			{
				return _Achievement;
			}
			set
			{
				_Achievement = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerAchievementState item in Achievement)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = obj as PlayerAchievementStateUpdate;
			if (playerAchievementStateUpdate == null)
			{
				return false;
			}
			if (Achievement.Count != playerAchievementStateUpdate.Achievement.Count)
			{
				return false;
			}
			for (int i = 0; i < Achievement.Count; i++)
			{
				if (!Achievement[i].Equals(playerAchievementStateUpdate.Achievement[i]))
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

		public static PlayerAchievementStateUpdate Deserialize(Stream stream, PlayerAchievementStateUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerAchievementStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = new PlayerAchievementStateUpdate();
			DeserializeLengthDelimited(stream, playerAchievementStateUpdate);
			return playerAchievementStateUpdate;
		}

		public static PlayerAchievementStateUpdate DeserializeLengthDelimited(Stream stream, PlayerAchievementStateUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerAchievementStateUpdate Deserialize(Stream stream, PlayerAchievementStateUpdate instance, long limit)
		{
			if (instance.Achievement == null)
			{
				instance.Achievement = new List<PlayerAchievementState>();
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
				case 10:
					instance.Achievement.Add(PlayerAchievementState.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PlayerAchievementStateUpdate instance)
		{
			if (instance.Achievement.Count <= 0)
			{
				return;
			}
			foreach (PlayerAchievementState item in instance.Achievement)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PlayerAchievementState.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Achievement.Count > 0)
			{
				foreach (PlayerAchievementState item in Achievement)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
