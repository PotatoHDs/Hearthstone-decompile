using System.IO;

namespace PegasusUtil
{
	public class SetFavoriteCardBack : IProtoBuf
	{
		public enum PacketID
		{
			ID = 291,
			System = 0
		}

		public bool HasDeprecatedDeckId;

		private long _DeprecatedDeckId;

		public int CardBack { get; set; }

		public long DeprecatedDeckId
		{
			get
			{
				return _DeprecatedDeckId;
			}
			set
			{
				_DeprecatedDeckId = value;
				HasDeprecatedDeckId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= CardBack.GetHashCode();
			if (HasDeprecatedDeckId)
			{
				hashCode ^= DeprecatedDeckId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SetFavoriteCardBack setFavoriteCardBack = obj as SetFavoriteCardBack;
			if (setFavoriteCardBack == null)
			{
				return false;
			}
			if (!CardBack.Equals(setFavoriteCardBack.CardBack))
			{
				return false;
			}
			if (HasDeprecatedDeckId != setFavoriteCardBack.HasDeprecatedDeckId || (HasDeprecatedDeckId && !DeprecatedDeckId.Equals(setFavoriteCardBack.DeprecatedDeckId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteCardBack Deserialize(Stream stream, SetFavoriteCardBack instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteCardBack DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCardBack setFavoriteCardBack = new SetFavoriteCardBack();
			DeserializeLengthDelimited(stream, setFavoriteCardBack);
			return setFavoriteCardBack;
		}

		public static SetFavoriteCardBack DeserializeLengthDelimited(Stream stream, SetFavoriteCardBack instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteCardBack Deserialize(Stream stream, SetFavoriteCardBack instance, long limit)
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
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.DeprecatedDeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetFavoriteCardBack instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
			if (instance.HasDeprecatedDeckId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedDeckId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)CardBack);
			if (HasDeprecatedDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedDeckId);
			}
			return num + 1;
		}
	}
}
