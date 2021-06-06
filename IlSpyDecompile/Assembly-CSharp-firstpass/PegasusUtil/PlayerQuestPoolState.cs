using System.IO;

namespace PegasusUtil
{
	public class PlayerQuestPoolState : IProtoBuf
	{
		public bool HasQuestPoolId;

		private int _QuestPoolId;

		public bool HasSecondsUntilNextGrant;

		private int _SecondsUntilNextGrant;

		public bool HasRerollAvailableCount;

		private int _RerollAvailableCount;

		public int QuestPoolId
		{
			get
			{
				return _QuestPoolId;
			}
			set
			{
				_QuestPoolId = value;
				HasQuestPoolId = true;
			}
		}

		public int SecondsUntilNextGrant
		{
			get
			{
				return _SecondsUntilNextGrant;
			}
			set
			{
				_SecondsUntilNextGrant = value;
				HasSecondsUntilNextGrant = true;
			}
		}

		public int RerollAvailableCount
		{
			get
			{
				return _RerollAvailableCount;
			}
			set
			{
				_RerollAvailableCount = value;
				HasRerollAvailableCount = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasQuestPoolId)
			{
				num ^= QuestPoolId.GetHashCode();
			}
			if (HasSecondsUntilNextGrant)
			{
				num ^= SecondsUntilNextGrant.GetHashCode();
			}
			if (HasRerollAvailableCount)
			{
				num ^= RerollAvailableCount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerQuestPoolState playerQuestPoolState = obj as PlayerQuestPoolState;
			if (playerQuestPoolState == null)
			{
				return false;
			}
			if (HasQuestPoolId != playerQuestPoolState.HasQuestPoolId || (HasQuestPoolId && !QuestPoolId.Equals(playerQuestPoolState.QuestPoolId)))
			{
				return false;
			}
			if (HasSecondsUntilNextGrant != playerQuestPoolState.HasSecondsUntilNextGrant || (HasSecondsUntilNextGrant && !SecondsUntilNextGrant.Equals(playerQuestPoolState.SecondsUntilNextGrant)))
			{
				return false;
			}
			if (HasRerollAvailableCount != playerQuestPoolState.HasRerollAvailableCount || (HasRerollAvailableCount && !RerollAvailableCount.Equals(playerQuestPoolState.RerollAvailableCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerQuestPoolState Deserialize(Stream stream, PlayerQuestPoolState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerQuestPoolState DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestPoolState playerQuestPoolState = new PlayerQuestPoolState();
			DeserializeLengthDelimited(stream, playerQuestPoolState);
			return playerQuestPoolState;
		}

		public static PlayerQuestPoolState DeserializeLengthDelimited(Stream stream, PlayerQuestPoolState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerQuestPoolState Deserialize(Stream stream, PlayerQuestPoolState instance, long limit)
		{
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
					instance.QuestPoolId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.SecondsUntilNextGrant = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.RerollAvailableCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PlayerQuestPoolState instance)
		{
			if (instance.HasQuestPoolId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.QuestPoolId);
			}
			if (instance.HasSecondsUntilNextGrant)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecondsUntilNextGrant);
			}
			if (instance.HasRerollAvailableCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RerollAvailableCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasQuestPoolId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)QuestPoolId);
			}
			if (HasSecondsUntilNextGrant)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SecondsUntilNextGrant);
			}
			if (HasRerollAvailableCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RerollAvailableCount);
			}
			return num;
		}
	}
}
