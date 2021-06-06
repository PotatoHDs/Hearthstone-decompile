using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameHandle : IProtoBuf
	{
		public ulong FactoryId { get; set; }

		public EntityId GameId { get; set; }

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetGameId(EntityId val)
		{
			GameId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FactoryId.GetHashCode() ^ GameId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			if (gameHandle == null)
			{
				return false;
			}
			if (!FactoryId.Equals(gameHandle.FactoryId))
			{
				return false;
			}
			if (!GameId.Equals(gameHandle.GameId))
			{
				return false;
			}
			return true;
		}

		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 18:
					if (instance.GameId == null)
					{
						instance.GameId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameId);
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

		public static void Serialize(Stream stream, GameHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.GameId == null)
			{
				throw new ArgumentNullException("GameId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameId);
		}

		public uint GetSerializedSize()
		{
			int num = 0 + 8;
			uint serializedSize = GameId.GetSerializedSize();
			return (uint)(num + (int)(serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2);
		}
	}
}
