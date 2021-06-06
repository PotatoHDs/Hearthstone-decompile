using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ReturningPlayerDeckNotCreated : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasABGroup;

		private uint _ABGroup;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public uint ABGroup
		{
			get
			{
				return _ABGroup;
			}
			set
			{
				_ABGroup = value;
				HasABGroup = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasABGroup)
			{
				num ^= ABGroup.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReturningPlayerDeckNotCreated returningPlayerDeckNotCreated = obj as ReturningPlayerDeckNotCreated;
			if (returningPlayerDeckNotCreated == null)
			{
				return false;
			}
			if (HasPlayer != returningPlayerDeckNotCreated.HasPlayer || (HasPlayer && !Player.Equals(returningPlayerDeckNotCreated.Player)))
			{
				return false;
			}
			if (HasABGroup != returningPlayerDeckNotCreated.HasABGroup || (HasABGroup && !ABGroup.Equals(returningPlayerDeckNotCreated.ABGroup)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReturningPlayerDeckNotCreated Deserialize(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReturningPlayerDeckNotCreated DeserializeLengthDelimited(Stream stream)
		{
			ReturningPlayerDeckNotCreated returningPlayerDeckNotCreated = new ReturningPlayerDeckNotCreated();
			DeserializeLengthDelimited(stream, returningPlayerDeckNotCreated);
			return returningPlayerDeckNotCreated;
		}

		public static ReturningPlayerDeckNotCreated DeserializeLengthDelimited(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReturningPlayerDeckNotCreated Deserialize(Stream stream, ReturningPlayerDeckNotCreated instance, long limit)
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
				case 10:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 16:
					instance.ABGroup = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasABGroup)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ABGroup);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasABGroup)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ABGroup);
			}
			return num;
		}
	}
}
