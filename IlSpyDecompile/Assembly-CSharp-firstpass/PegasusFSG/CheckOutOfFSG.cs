using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class CheckOutOfFSG : IProtoBuf
	{
		public enum PacketID
		{
			ID = 503,
			System = 3
		}

		public bool HasPlatform;

		private Platform _Platform;

		public long FsgId { get; set; }

		public Platform Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= FsgId.GetHashCode();
			if (HasPlatform)
			{
				hashCode ^= Platform.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CheckOutOfFSG checkOutOfFSG = obj as CheckOutOfFSG;
			if (checkOutOfFSG == null)
			{
				return false;
			}
			if (!FsgId.Equals(checkOutOfFSG.FsgId))
			{
				return false;
			}
			if (HasPlatform != checkOutOfFSG.HasPlatform || (HasPlatform && !Platform.Equals(checkOutOfFSG.Platform)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CheckOutOfFSG Deserialize(Stream stream, CheckOutOfFSG instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CheckOutOfFSG DeserializeLengthDelimited(Stream stream)
		{
			CheckOutOfFSG checkOutOfFSG = new CheckOutOfFSG();
			DeserializeLengthDelimited(stream, checkOutOfFSG);
			return checkOutOfFSG;
		}

		public static CheckOutOfFSG DeserializeLengthDelimited(Stream stream, CheckOutOfFSG instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CheckOutOfFSG Deserialize(Stream stream, CheckOutOfFSG instance, long limit)
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
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
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

		public static void Serialize(Stream stream, CheckOutOfFSG instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.HasPlatform)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			if (HasPlatform)
			{
				num++;
				uint serializedSize = Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
