using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameEndedNotification : IProtoBuf
	{
		public bool HasReason;

		private uint _Reason;

		public GameHandle GameHandle { get; set; }

		public uint Reason
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

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameEndedNotification gameEndedNotification = obj as GameEndedNotification;
			if (gameEndedNotification == null)
			{
				return false;
			}
			if (!GameHandle.Equals(gameEndedNotification.GameHandle))
			{
				return false;
			}
			if (HasReason != gameEndedNotification.HasReason || (HasReason && !Reason.Equals(gameEndedNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static GameEndedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameEndedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameEndedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameEndedNotification gameEndedNotification = new GameEndedNotification();
			DeserializeLengthDelimited(stream, gameEndedNotification);
			return gameEndedNotification;
		}

		public static GameEndedNotification DeserializeLengthDelimited(Stream stream, GameEndedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance, long limit)
		{
			instance.Reason = 0u;
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
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 16:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameEndedNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			return num + 1;
		}
	}
}
