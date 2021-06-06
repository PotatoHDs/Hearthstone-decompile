using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class PlayerJoinNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasAssignment;

		private bnet.protocol.games.v2.Assignment _Assignment;

		public bnet.protocol.games.v2.GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public bnet.protocol.games.v2.Assignment Assignment
		{
			get
			{
				return _Assignment;
			}
			set
			{
				_Assignment = value;
				HasAssignment = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(bnet.protocol.games.v2.GameHandle val)
		{
			GameHandle = val;
		}

		public void SetAssignment(bnet.protocol.games.v2.Assignment val)
		{
			Assignment = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasAssignment)
			{
				num ^= Assignment.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerJoinNotification playerJoinNotification = obj as PlayerJoinNotification;
			if (playerJoinNotification == null)
			{
				return false;
			}
			if (HasGameHandle != playerJoinNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(playerJoinNotification.GameHandle)))
			{
				return false;
			}
			if (HasAssignment != playerJoinNotification.HasAssignment || (HasAssignment && !Assignment.Equals(playerJoinNotification.Assignment)))
			{
				return false;
			}
			return true;
		}

		public static PlayerJoinNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerJoinNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerJoinNotification Deserialize(Stream stream, PlayerJoinNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerJoinNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerJoinNotification playerJoinNotification = new PlayerJoinNotification();
			DeserializeLengthDelimited(stream, playerJoinNotification);
			return playerJoinNotification;
		}

		public static PlayerJoinNotification DeserializeLengthDelimited(Stream stream, PlayerJoinNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerJoinNotification Deserialize(Stream stream, PlayerJoinNotification instance, long limit)
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = bnet.protocol.games.v2.GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.games.v2.GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					if (instance.Assignment == null)
					{
						instance.Assignment = bnet.protocol.games.v2.Assignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.games.v2.Assignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		public static void Serialize(Stream stream, PlayerJoinNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				bnet.protocol.games.v2.GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				bnet.protocol.games.v2.Assignment.Serialize(stream, instance.Assignment);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasAssignment)
			{
				num++;
				uint serializedSize2 = Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
