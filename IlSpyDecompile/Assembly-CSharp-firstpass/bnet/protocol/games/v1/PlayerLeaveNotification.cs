using System.IO;
using bnet.protocol.games.v2;
using bnet.protocol.games.v2.Types;

namespace bnet.protocol.games.v1
{
	public class PlayerLeaveNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasAssignment;

		private bnet.protocol.games.v2.Assignment _Assignment;

		public bool HasReason;

		private PlayerLeaveReason _Reason;

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

		public PlayerLeaveReason Reason
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

		public void SetAssignment(bnet.protocol.games.v2.Assignment val)
		{
			Assignment = val;
		}

		public void SetReason(PlayerLeaveReason val)
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
			if (HasAssignment)
			{
				num ^= Assignment.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerLeaveNotification playerLeaveNotification = obj as PlayerLeaveNotification;
			if (playerLeaveNotification == null)
			{
				return false;
			}
			if (HasGameHandle != playerLeaveNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(playerLeaveNotification.GameHandle)))
			{
				return false;
			}
			if (HasAssignment != playerLeaveNotification.HasAssignment || (HasAssignment && !Assignment.Equals(playerLeaveNotification.Assignment)))
			{
				return false;
			}
			if (HasReason != playerLeaveNotification.HasReason || (HasReason && !Reason.Equals(playerLeaveNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static PlayerLeaveNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerLeaveNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerLeaveNotification Deserialize(Stream stream, PlayerLeaveNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerLeaveNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerLeaveNotification playerLeaveNotification = new PlayerLeaveNotification();
			DeserializeLengthDelimited(stream, playerLeaveNotification);
			return playerLeaveNotification;
		}

		public static PlayerLeaveNotification DeserializeLengthDelimited(Stream stream, PlayerLeaveNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerLeaveNotification Deserialize(Stream stream, PlayerLeaveNotification instance, long limit)
		{
			instance.Reason = PlayerLeaveReason.PLAYER_LEAVE_REASON_PLAYER_REMOVED_BY_GAME_SERVER;
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
				case 24:
					instance.Reason = (PlayerLeaveReason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PlayerLeaveNotification instance)
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
			if (instance.HasReason)
			{
				stream.WriteByte(24);
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
			if (HasAssignment)
			{
				num++;
				uint serializedSize2 = Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
