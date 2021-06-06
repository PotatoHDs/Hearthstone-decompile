using System.IO;

namespace bnet.protocol.account.v1
{
	public class SubscriberReference : IProtoBuf
	{
		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasEntityId;

		private EntityId _EntityId;

		public bool HasAccountOptions;

		private AccountFieldOptions _AccountOptions;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

		public bool HasGameAccountOptions;

		private GameAccountFieldOptions _GameAccountOptions;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public EntityId EntityId
		{
			get
			{
				return _EntityId;
			}
			set
			{
				_EntityId = value;
				HasEntityId = value != null;
			}
		}

		public AccountFieldOptions AccountOptions
		{
			get
			{
				return _AccountOptions;
			}
			set
			{
				_AccountOptions = value;
				HasAccountOptions = value != null;
			}
		}

		public AccountFieldTags AccountTags
		{
			get
			{
				return _AccountTags;
			}
			set
			{
				_AccountTags = value;
				HasAccountTags = value != null;
			}
		}

		public GameAccountFieldOptions GameAccountOptions
		{
			get
			{
				return _GameAccountOptions;
			}
			set
			{
				_GameAccountOptions = value;
				HasGameAccountOptions = value != null;
			}
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return _GameAccountTags;
			}
			set
			{
				_GameAccountTags = value;
				HasGameAccountTags = value != null;
			}
		}

		public ulong SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetAccountOptions(AccountFieldOptions val)
		{
			AccountOptions = val;
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			AccountTags = val;
		}

		public void SetGameAccountOptions(GameAccountFieldOptions val)
		{
			GameAccountOptions = val;
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			GameAccountTags = val;
		}

		public void SetSubscriberId(ulong val)
		{
			SubscriberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			if (HasAccountOptions)
			{
				num ^= AccountOptions.GetHashCode();
			}
			if (HasAccountTags)
			{
				num ^= AccountTags.GetHashCode();
			}
			if (HasGameAccountOptions)
			{
				num ^= GameAccountOptions.GetHashCode();
			}
			if (HasGameAccountTags)
			{
				num ^= GameAccountTags.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscriberReference subscriberReference = obj as SubscriberReference;
			if (subscriberReference == null)
			{
				return false;
			}
			if (HasObjectId != subscriberReference.HasObjectId || (HasObjectId && !ObjectId.Equals(subscriberReference.ObjectId)))
			{
				return false;
			}
			if (HasEntityId != subscriberReference.HasEntityId || (HasEntityId && !EntityId.Equals(subscriberReference.EntityId)))
			{
				return false;
			}
			if (HasAccountOptions != subscriberReference.HasAccountOptions || (HasAccountOptions && !AccountOptions.Equals(subscriberReference.AccountOptions)))
			{
				return false;
			}
			if (HasAccountTags != subscriberReference.HasAccountTags || (HasAccountTags && !AccountTags.Equals(subscriberReference.AccountTags)))
			{
				return false;
			}
			if (HasGameAccountOptions != subscriberReference.HasGameAccountOptions || (HasGameAccountOptions && !GameAccountOptions.Equals(subscriberReference.GameAccountOptions)))
			{
				return false;
			}
			if (HasGameAccountTags != subscriberReference.HasGameAccountTags || (HasGameAccountTags && !GameAccountTags.Equals(subscriberReference.GameAccountTags)))
			{
				return false;
			}
			if (HasSubscriberId != subscriberReference.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(subscriberReference.SubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static SubscriberReference ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriberReference>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscriberReference DeserializeLengthDelimited(Stream stream)
		{
			SubscriberReference subscriberReference = new SubscriberReference();
			DeserializeLengthDelimited(stream, subscriberReference);
			return subscriberReference;
		}

		public static SubscriberReference DeserializeLengthDelimited(Stream stream, SubscriberReference instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance, long limit)
		{
			instance.ObjectId = 0uL;
			instance.SubscriberId = 0uL;
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
				case 8:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 26:
					if (instance.AccountOptions == null)
					{
						instance.AccountOptions = AccountFieldOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldOptions.DeserializeLengthDelimited(stream, instance.AccountOptions);
					}
					continue;
				case 34:
					if (instance.AccountTags == null)
					{
						instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
					}
					continue;
				case 42:
					if (instance.GameAccountOptions == null)
					{
						instance.GameAccountOptions = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.GameAccountOptions);
					}
					continue;
				case 50:
					if (instance.GameAccountTags == null)
					{
						instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
					}
					continue;
				case 56:
					instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SubscriberReference instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasEntityId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasAccountOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountOptions.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.AccountOptions);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
			if (instance.HasGameAccountOptions)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountOptions.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.GameAccountOptions);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			if (HasEntityId)
			{
				num++;
				uint serializedSize = EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasAccountOptions)
			{
				num++;
				uint serializedSize2 = AccountOptions.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasAccountTags)
			{
				num++;
				uint serializedSize3 = AccountTags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasGameAccountOptions)
			{
				num++;
				uint serializedSize4 = GameAccountOptions.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasGameAccountTags)
			{
				num++;
				uint serializedSize5 = GameAccountTags.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasSubscriberId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SubscriberId);
			}
			return num;
		}
	}
}
