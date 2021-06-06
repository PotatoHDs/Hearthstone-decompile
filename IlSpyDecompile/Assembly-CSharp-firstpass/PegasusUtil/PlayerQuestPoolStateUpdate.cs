using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerQuestPoolStateUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 602,
			System = 0
		}

		private List<PlayerQuestPoolState> _QuestPool = new List<PlayerQuestPoolState>();

		public List<PlayerQuestPoolState> QuestPool
		{
			get
			{
				return _QuestPool;
			}
			set
			{
				_QuestPool = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerQuestPoolState item in QuestPool)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = obj as PlayerQuestPoolStateUpdate;
			if (playerQuestPoolStateUpdate == null)
			{
				return false;
			}
			if (QuestPool.Count != playerQuestPoolStateUpdate.QuestPool.Count)
			{
				return false;
			}
			for (int i = 0; i < QuestPool.Count; i++)
			{
				if (!QuestPool[i].Equals(playerQuestPoolStateUpdate.QuestPool[i]))
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

		public static PlayerQuestPoolStateUpdate Deserialize(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerQuestPoolStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = new PlayerQuestPoolStateUpdate();
			DeserializeLengthDelimited(stream, playerQuestPoolStateUpdate);
			return playerQuestPoolStateUpdate;
		}

		public static PlayerQuestPoolStateUpdate DeserializeLengthDelimited(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerQuestPoolStateUpdate Deserialize(Stream stream, PlayerQuestPoolStateUpdate instance, long limit)
		{
			if (instance.QuestPool == null)
			{
				instance.QuestPool = new List<PlayerQuestPoolState>();
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
					instance.QuestPool.Add(PlayerQuestPoolState.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			if (instance.QuestPool.Count <= 0)
			{
				return;
			}
			foreach (PlayerQuestPoolState item in instance.QuestPool)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PlayerQuestPoolState.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (QuestPool.Count > 0)
			{
				foreach (PlayerQuestPoolState item in QuestPool)
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
