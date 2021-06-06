using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetAccountStateRequest : IProtoBuf
	{
		public bool HasEntityId;

		private EntityId _EntityId;

		public bool HasProgram;

		private uint _Program;

		public bool HasRegion;

		private uint _Region;

		public bool HasOptions;

		private AccountFieldOptions _Options;

		public bool HasTags;

		private AccountFieldTags _Tags;

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

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public AccountFieldOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public AccountFieldTags Tags
		{
			get
			{
				return _Tags;
			}
			set
			{
				_Tags = value;
				HasTags = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public void SetOptions(AccountFieldOptions val)
		{
			Options = val;
		}

		public void SetTags(AccountFieldTags val)
		{
			Tags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasTags)
			{
				num ^= Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAccountStateRequest getAccountStateRequest = obj as GetAccountStateRequest;
			if (getAccountStateRequest == null)
			{
				return false;
			}
			if (HasEntityId != getAccountStateRequest.HasEntityId || (HasEntityId && !EntityId.Equals(getAccountStateRequest.EntityId)))
			{
				return false;
			}
			if (HasProgram != getAccountStateRequest.HasProgram || (HasProgram && !Program.Equals(getAccountStateRequest.Program)))
			{
				return false;
			}
			if (HasRegion != getAccountStateRequest.HasRegion || (HasRegion && !Region.Equals(getAccountStateRequest.Region)))
			{
				return false;
			}
			if (HasOptions != getAccountStateRequest.HasOptions || (HasOptions && !Options.Equals(getAccountStateRequest.Options)))
			{
				return false;
			}
			if (HasTags != getAccountStateRequest.HasTags || (HasTags && !Tags.Equals(getAccountStateRequest.Tags)))
			{
				return false;
			}
			return true;
		}

		public static GetAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			DeserializeLengthDelimited(stream, getAccountStateRequest);
			return getAccountStateRequest;
		}

		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream, GetAccountStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 16:
					instance.Program = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.Region = ProtocolParser.ReadUInt32(stream);
					continue;
				case 82:
					if (instance.Options == null)
					{
						instance.Options = AccountFieldOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 90:
					if (instance.Tags == null)
					{
						instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
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

		public static void Serialize(Stream stream, GetAccountStateRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEntityId)
			{
				num++;
				uint serializedSize = EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProgram)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Program);
			}
			if (HasRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Region);
			}
			if (HasOptions)
			{
				num++;
				uint serializedSize2 = Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasTags)
			{
				num++;
				uint serializedSize3 = Tags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
