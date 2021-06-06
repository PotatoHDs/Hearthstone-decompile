using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;
using bnet.protocol.games.v2.Types;

namespace bnet.protocol.games.v1
{
	public class ExitGameNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

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

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
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

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
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
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ExitGameNotification exitGameNotification = obj as ExitGameNotification;
			if (exitGameNotification == null)
			{
				return false;
			}
			if (HasGameHandle != exitGameNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(exitGameNotification.GameHandle)))
			{
				return false;
			}
			if (HasGameAccount != exitGameNotification.HasGameAccount || (HasGameAccount && !GameAccount.Equals(exitGameNotification.GameAccount)))
			{
				return false;
			}
			if (HasReason != exitGameNotification.HasReason || (HasReason && !Reason.Equals(exitGameNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static ExitGameNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ExitGameNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ExitGameNotification Deserialize(Stream stream, ExitGameNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ExitGameNotification DeserializeLengthDelimited(Stream stream)
		{
			ExitGameNotification exitGameNotification = new ExitGameNotification();
			DeserializeLengthDelimited(stream, exitGameNotification);
			return exitGameNotification;
		}

		public static ExitGameNotification DeserializeLengthDelimited(Stream stream, ExitGameNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ExitGameNotification Deserialize(Stream stream, ExitGameNotification instance, long limit)
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
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
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

		public static void Serialize(Stream stream, ExitGameNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				bnet.protocol.games.v2.GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
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
			if (HasGameAccount)
			{
				num++;
				uint serializedSize2 = GameAccount.GetSerializedSize();
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
