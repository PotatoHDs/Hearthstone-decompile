using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class GamePrivacyChangeNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

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

		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return _PrivacyLevel;
			}
			set
			{
				_PrivacyLevel = value;
				HasPrivacyLevel = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(bnet.protocol.games.v2.GameHandle val)
		{
			GameHandle = val;
		}

		public void SetPrivacyLevel(PrivacyLevel val)
		{
			PrivacyLevel = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasPrivacyLevel)
			{
				num ^= PrivacyLevel.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GamePrivacyChangeNotification gamePrivacyChangeNotification = obj as GamePrivacyChangeNotification;
			if (gamePrivacyChangeNotification == null)
			{
				return false;
			}
			if (HasGameHandle != gamePrivacyChangeNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(gamePrivacyChangeNotification.GameHandle)))
			{
				return false;
			}
			if (HasPrivacyLevel != gamePrivacyChangeNotification.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(gamePrivacyChangeNotification.PrivacyLevel)))
			{
				return false;
			}
			return true;
		}

		public static GamePrivacyChangeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GamePrivacyChangeNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GamePrivacyChangeNotification Deserialize(Stream stream, GamePrivacyChangeNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GamePrivacyChangeNotification DeserializeLengthDelimited(Stream stream)
		{
			GamePrivacyChangeNotification gamePrivacyChangeNotification = new GamePrivacyChangeNotification();
			DeserializeLengthDelimited(stream, gamePrivacyChangeNotification);
			return gamePrivacyChangeNotification;
		}

		public static GamePrivacyChangeNotification DeserializeLengthDelimited(Stream stream, GamePrivacyChangeNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GamePrivacyChangeNotification Deserialize(Stream stream, GamePrivacyChangeNotification instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
				case 16:
					instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GamePrivacyChangeNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				bnet.protocol.games.v2.GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
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
			if (HasPrivacyLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrivacyLevel);
			}
			return num;
		}
	}
}
