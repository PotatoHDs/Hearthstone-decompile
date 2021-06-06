using System.IO;
using System.Text;

namespace PegasusShared
{
	public class GameSaveKey : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public long Id { get; set; }

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasName)
			{
				hashCode ^= Name.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameSaveKey gameSaveKey = obj as GameSaveKey;
			if (gameSaveKey == null)
			{
				return false;
			}
			if (!Id.Equals(gameSaveKey.Id))
			{
				return false;
			}
			if (HasName != gameSaveKey.HasName || (HasName && !Name.Equals(gameSaveKey.Name)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSaveKey Deserialize(Stream stream, GameSaveKey instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSaveKey DeserializeLengthDelimited(Stream stream)
		{
			GameSaveKey gameSaveKey = new GameSaveKey();
			DeserializeLengthDelimited(stream, gameSaveKey);
			return gameSaveKey;
		}

		public static GameSaveKey DeserializeLengthDelimited(Stream stream, GameSaveKey instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSaveKey Deserialize(Stream stream, GameSaveKey instance, long limit)
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
					instance.Id = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GameSaveKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
