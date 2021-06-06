using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class Assignment : IProtoBuf
	{
		public EntityId GameAccountId { get; set; }

		public uint TeamIndex { get; set; }

		public bool IsInitialized => true;

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetTeamIndex(uint val)
		{
			TeamIndex = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GameAccountId.GetHashCode() ^ TeamIndex.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Assignment assignment = obj as Assignment;
			if (assignment == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(assignment.GameAccountId))
			{
				return false;
			}
			if (!TeamIndex.Equals(assignment.TeamIndex))
			{
				return false;
			}
			return true;
		}

		public static Assignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Assignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Assignment Deserialize(Stream stream, Assignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Assignment DeserializeLengthDelimited(Stream stream)
		{
			Assignment assignment = new Assignment();
			DeserializeLengthDelimited(stream, assignment);
			return assignment;
		}

		public static Assignment DeserializeLengthDelimited(Stream stream, Assignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Assignment Deserialize(Stream stream, Assignment instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 16:
					instance.TeamIndex = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, Assignment instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.TeamIndex);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = GameAccountId.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(TeamIndex) + 2;
		}
	}
}
