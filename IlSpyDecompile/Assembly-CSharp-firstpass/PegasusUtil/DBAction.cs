using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DBAction : IProtoBuf
	{
		public enum PacketID
		{
			ID = 216
		}

		public bool HasMetaData;

		private long _MetaData;

		public DatabaseAction Action { get; set; }

		public DatabaseResult Result { get; set; }

		public long MetaData
		{
			get
			{
				return _MetaData;
			}
			set
			{
				_MetaData = value;
				HasMetaData = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Action.GetHashCode();
			hashCode ^= Result.GetHashCode();
			if (HasMetaData)
			{
				hashCode ^= MetaData.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DBAction dBAction = obj as DBAction;
			if (dBAction == null)
			{
				return false;
			}
			if (!Action.Equals(dBAction.Action))
			{
				return false;
			}
			if (!Result.Equals(dBAction.Result))
			{
				return false;
			}
			if (HasMetaData != dBAction.HasMetaData || (HasMetaData && !MetaData.Equals(dBAction.MetaData)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DBAction Deserialize(Stream stream, DBAction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DBAction DeserializeLengthDelimited(Stream stream)
		{
			DBAction dBAction = new DBAction();
			DeserializeLengthDelimited(stream, dBAction);
			return dBAction;
		}

		public static DBAction DeserializeLengthDelimited(Stream stream, DBAction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DBAction Deserialize(Stream stream, DBAction instance, long limit)
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
					instance.Action = (DatabaseAction)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Result = (DatabaseResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.MetaData = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DBAction instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Action);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result);
			if (instance.HasMetaData)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MetaData);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Action);
			num += ProtocolParser.SizeOfUInt64((ulong)Result);
			if (HasMetaData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MetaData);
			}
			return num + 2;
		}
	}
}
