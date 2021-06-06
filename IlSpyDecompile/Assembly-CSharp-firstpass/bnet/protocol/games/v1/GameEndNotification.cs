using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class GameEndNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasReason;

		private GameEndedReason _Reason;

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

		public GameEndedReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(bnet.protocol.games.v2.GameHandle val)
		{
			GameHandle = val;
		}

		public void SetReason(GameEndedReason val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameEndNotification gameEndNotification = obj as GameEndNotification;
			if (gameEndNotification == null)
			{
				return false;
			}
			if (HasGameHandle != gameEndNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(gameEndNotification.GameHandle)))
			{
				return false;
			}
			if (HasReason != gameEndNotification.HasReason || (HasReason && !Reason.Equals(gameEndNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static GameEndNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameEndNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameEndNotification Deserialize(Stream stream, GameEndNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameEndNotification DeserializeLengthDelimited(Stream stream)
		{
			GameEndNotification gameEndNotification = new GameEndNotification();
			DeserializeLengthDelimited(stream, gameEndNotification);
			return gameEndNotification;
		}

		public static GameEndNotification DeserializeLengthDelimited(Stream stream, GameEndNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameEndNotification Deserialize(Stream stream, GameEndNotification instance, long limit)
		{
			instance.Reason = GameEndedReason.GAME_ENDED_REASON_DISSOLVED_BY_GAME_SERVER;
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
					instance.Reason = (GameEndedReason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameEndNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				bnet.protocol.games.v2.GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
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
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			return num;
		}
	}
}
