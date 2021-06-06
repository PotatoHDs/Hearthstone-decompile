using System;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class PresenceChannelCreatedRequest : IProtoBuf
	{
		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasHost;

		private ProcessId _Host;

		public EntityId Id { get; set; }

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

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(EntityId val)
		{
			Id = val;
		}

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasGameAccountId)
			{
				hashCode ^= GameAccountId.GetHashCode();
			}
			if (HasAccountId)
			{
				hashCode ^= AccountId.GetHashCode();
			}
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = obj as PresenceChannelCreatedRequest;
			if (presenceChannelCreatedRequest == null)
			{
				return false;
			}
			if (!Id.Equals(presenceChannelCreatedRequest.Id))
			{
				return false;
			}
			if (HasGameAccountId != presenceChannelCreatedRequest.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(presenceChannelCreatedRequest.GameAccountId)))
			{
				return false;
			}
			if (HasAccountId != presenceChannelCreatedRequest.HasAccountId || (HasAccountId && !AccountId.Equals(presenceChannelCreatedRequest.AccountId)))
			{
				return false;
			}
			if (HasHost != presenceChannelCreatedRequest.HasHost || (HasHost && !Host.Equals(presenceChannelCreatedRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static PresenceChannelCreatedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PresenceChannelCreatedRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = new PresenceChannelCreatedRequest();
			DeserializeLengthDelimited(stream, presenceChannelCreatedRequest);
			return presenceChannelCreatedRequest;
		}

		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream, PresenceChannelCreatedRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance, long limit)
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
					if (instance.Id == null)
					{
						instance.Id = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.Id);
					}
					continue;
				case 26:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 34:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 42:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		public static void Serialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			if (instance.Id == null)
			{
				throw new ArgumentNullException("Id", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
			EntityId.Serialize(stream, instance.Id);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Id.GetSerializedSize();
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
			if (HasHost)
			{
				num++;
				uint serializedSize4 = Host.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1;
		}
	}
}
