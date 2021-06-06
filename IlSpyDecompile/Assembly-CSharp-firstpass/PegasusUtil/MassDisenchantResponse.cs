using System.IO;

namespace PegasusUtil
{
	public class MassDisenchantResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 269
		}

		public bool HasCollectionVersion;

		private long _CollectionVersion;

		public int Amount { get; set; }

		public long CollectionVersion
		{
			get
			{
				return _CollectionVersion;
			}
			set
			{
				_CollectionVersion = value;
				HasCollectionVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Amount.GetHashCode();
			if (HasCollectionVersion)
			{
				hashCode ^= CollectionVersion.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			MassDisenchantResponse massDisenchantResponse = obj as MassDisenchantResponse;
			if (massDisenchantResponse == null)
			{
				return false;
			}
			if (!Amount.Equals(massDisenchantResponse.Amount))
			{
				return false;
			}
			if (HasCollectionVersion != massDisenchantResponse.HasCollectionVersion || (HasCollectionVersion && !CollectionVersion.Equals(massDisenchantResponse.CollectionVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MassDisenchantResponse Deserialize(Stream stream, MassDisenchantResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MassDisenchantResponse DeserializeLengthDelimited(Stream stream)
		{
			MassDisenchantResponse massDisenchantResponse = new MassDisenchantResponse();
			DeserializeLengthDelimited(stream, massDisenchantResponse);
			return massDisenchantResponse;
		}

		public static MassDisenchantResponse DeserializeLengthDelimited(Stream stream, MassDisenchantResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MassDisenchantResponse Deserialize(Stream stream, MassDisenchantResponse instance, long limit)
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
				case 8:
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, MassDisenchantResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Amount);
			if (HasCollectionVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CollectionVersion);
			}
			return num + 1;
		}
	}
}
