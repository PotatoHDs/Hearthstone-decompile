using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class PlayerLeftNotification : IProtoBuf
	{
		public bool HasReason;

		private uint _Reason;

		public GameHandle GameHandle { get; set; }

		public EntityId GameAccountId { get; set; }

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

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			hashCode ^= GameAccountId.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PlayerLeftNotification playerLeftNotification = obj as PlayerLeftNotification;
			if (playerLeftNotification == null)
			{
				return false;
			}
			if (!GameHandle.Equals(playerLeftNotification.GameHandle))
			{
				return false;
			}
			if (!GameAccountId.Equals(playerLeftNotification.GameAccountId))
			{
				return false;
			}
			if (HasReason != playerLeftNotification.HasReason || (HasReason && !Reason.Equals(playerLeftNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static PlayerLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerLeftNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerLeftNotification playerLeftNotification = new PlayerLeftNotification();
			DeserializeLengthDelimited(stream, playerLeftNotification);
			return playerLeftNotification;
		}

		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream, PlayerLeftNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance, long limit)
		{
			instance.Reason = 1u;
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
				case 18:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 24:
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

		public static void Serialize(Stream stream, PlayerLeftNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = GameAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			return num + 2;
		}
	}
}
