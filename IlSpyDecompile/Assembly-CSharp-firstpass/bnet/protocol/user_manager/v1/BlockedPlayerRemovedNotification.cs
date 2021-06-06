using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class BlockedPlayerRemovedNotification : IProtoBuf
	{
		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasAccountId;

		private EntityId _AccountId;

		public BlockedPlayer Player { get; set; }

		public EntityId GameAccountId
		{
			get
			{
				return _GameAccountId;
			}
			set
			{
				_GameAccountId = value;
				HasGameAccountId = value != null;
			}
		}

		public EntityId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetPlayer(BlockedPlayer val)
		{
			Player = val;
		}

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Player.GetHashCode();
			if (HasGameAccountId)
			{
				hashCode ^= GameAccountId.GetHashCode();
			}
			if (HasAccountId)
			{
				hashCode ^= AccountId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BlockedPlayerRemovedNotification blockedPlayerRemovedNotification = obj as BlockedPlayerRemovedNotification;
			if (blockedPlayerRemovedNotification == null)
			{
				return false;
			}
			if (!Player.Equals(blockedPlayerRemovedNotification.Player))
			{
				return false;
			}
			if (HasGameAccountId != blockedPlayerRemovedNotification.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(blockedPlayerRemovedNotification.GameAccountId)))
			{
				return false;
			}
			if (HasAccountId != blockedPlayerRemovedNotification.HasAccountId || (HasAccountId && !AccountId.Equals(blockedPlayerRemovedNotification.AccountId)))
			{
				return false;
			}
			return true;
		}

		public static BlockedPlayerRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockedPlayerRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlockedPlayerRemovedNotification Deserialize(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlockedPlayerRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			BlockedPlayerRemovedNotification blockedPlayerRemovedNotification = new BlockedPlayerRemovedNotification();
			DeserializeLengthDelimited(stream, blockedPlayerRemovedNotification);
			return blockedPlayerRemovedNotification;
		}

		public static BlockedPlayerRemovedNotification DeserializeLengthDelimited(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlockedPlayerRemovedNotification Deserialize(Stream stream, BlockedPlayerRemovedNotification instance, long limit)
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
					if (instance.Player == null)
					{
						instance.Player = BlockedPlayer.DeserializeLengthDelimited(stream);
					}
					else
					{
						BlockedPlayer.DeserializeLengthDelimited(stream, instance.Player);
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
				case 26:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
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

		public static void Serialize(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			if (instance.Player == null)
			{
				throw new ArgumentNullException("Player", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
			BlockedPlayer.Serialize(stream, instance.Player);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Player.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasGameAccountId)
			{
				num++;
				uint serializedSize2 = GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasAccountId)
			{
				num++;
				uint serializedSize3 = AccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1;
		}
	}
}
