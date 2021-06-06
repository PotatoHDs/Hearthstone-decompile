using System.IO;

namespace PegasusUtil
{
	public class OpenBooster : IProtoBuf
	{
		public enum PacketID
		{
			ID = 225,
			System = 0
		}

		public bool HasFsgId;

		private long _FsgId;

		public int BoosterType { get; set; }

		public long FsgId
		{
			get
			{
				return _FsgId;
			}
			set
			{
				_FsgId = value;
				HasFsgId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= BoosterType.GetHashCode();
			if (HasFsgId)
			{
				hashCode ^= FsgId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			OpenBooster openBooster = obj as OpenBooster;
			if (openBooster == null)
			{
				return false;
			}
			if (!BoosterType.Equals(openBooster.BoosterType))
			{
				return false;
			}
			if (HasFsgId != openBooster.HasFsgId || (HasFsgId && !FsgId.Equals(openBooster.FsgId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static OpenBooster Deserialize(Stream stream, OpenBooster instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static OpenBooster DeserializeLengthDelimited(Stream stream)
		{
			OpenBooster openBooster = new OpenBooster();
			DeserializeLengthDelimited(stream, openBooster);
			return openBooster;
		}

		public static OpenBooster DeserializeLengthDelimited(Stream stream, OpenBooster instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static OpenBooster Deserialize(Stream stream, OpenBooster instance, long limit)
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
				case 16:
					instance.BoosterType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, OpenBooster instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterType);
			if (instance.HasFsgId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)BoosterType);
			if (HasFsgId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			}
			return num + 1;
		}
	}
}
