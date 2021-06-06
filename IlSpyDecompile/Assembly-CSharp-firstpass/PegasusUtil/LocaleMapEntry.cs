using System.IO;

namespace PegasusUtil
{
	public class LocaleMapEntry : IProtoBuf
	{
		public bool HasCatalogLocaleId;

		private long _CatalogLocaleId;

		public bool HasGameLocaleId;

		private long _GameLocaleId;

		public long CatalogLocaleId
		{
			get
			{
				return _CatalogLocaleId;
			}
			set
			{
				_CatalogLocaleId = value;
				HasCatalogLocaleId = true;
			}
		}

		public long GameLocaleId
		{
			get
			{
				return _GameLocaleId;
			}
			set
			{
				_GameLocaleId = value;
				HasGameLocaleId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCatalogLocaleId)
			{
				num ^= CatalogLocaleId.GetHashCode();
			}
			if (HasGameLocaleId)
			{
				num ^= GameLocaleId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LocaleMapEntry localeMapEntry = obj as LocaleMapEntry;
			if (localeMapEntry == null)
			{
				return false;
			}
			if (HasCatalogLocaleId != localeMapEntry.HasCatalogLocaleId || (HasCatalogLocaleId && !CatalogLocaleId.Equals(localeMapEntry.CatalogLocaleId)))
			{
				return false;
			}
			if (HasGameLocaleId != localeMapEntry.HasGameLocaleId || (HasGameLocaleId && !GameLocaleId.Equals(localeMapEntry.GameLocaleId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LocaleMapEntry Deserialize(Stream stream, LocaleMapEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LocaleMapEntry DeserializeLengthDelimited(Stream stream)
		{
			LocaleMapEntry localeMapEntry = new LocaleMapEntry();
			DeserializeLengthDelimited(stream, localeMapEntry);
			return localeMapEntry;
		}

		public static LocaleMapEntry DeserializeLengthDelimited(Stream stream, LocaleMapEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LocaleMapEntry Deserialize(Stream stream, LocaleMapEntry instance, long limit)
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
					instance.CatalogLocaleId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GameLocaleId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LocaleMapEntry instance)
		{
			if (instance.HasCatalogLocaleId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CatalogLocaleId);
			}
			if (instance.HasGameLocaleId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameLocaleId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCatalogLocaleId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CatalogLocaleId);
			}
			if (HasGameLocaleId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameLocaleId);
			}
			return num;
		}
	}
}
