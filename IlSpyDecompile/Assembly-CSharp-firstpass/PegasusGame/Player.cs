using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class Player : IProtoBuf
	{
		public int Id { get; set; }

		public BnetId GameAccountId { get; set; }

		public int CardBack { get; set; }

		public Entity Entity { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode() ^ GameAccountId.GetHashCode() ^ CardBack.GetHashCode() ^ Entity.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (!Id.Equals(player.Id))
			{
				return false;
			}
			if (!GameAccountId.Equals(player.GameAccountId))
			{
				return false;
			}
			if (!CardBack.Equals(player.CardBack))
			{
				return false;
			}
			if (!Entity.Equals(player.Entity))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Player Deserialize(Stream stream, Player instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			DeserializeLengthDelimited(stream, player);
			return player;
		}

		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Player Deserialize(Stream stream, Player instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 24:
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.Entity == null)
					{
						instance.Entity = Entity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Entity.DeserializeLengthDelimited(stream, instance.Entity);
					}
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

		public static void Serialize(Stream stream, Player instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
			if (instance.Entity == null)
			{
				throw new ArgumentNullException("Entity", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.Entity.GetSerializedSize());
			Entity.Serialize(stream, instance.Entity);
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)Id);
			uint serializedSize = GameAccountId.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)CardBack);
			uint serializedSize2 = Entity.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 4;
		}
	}
}
