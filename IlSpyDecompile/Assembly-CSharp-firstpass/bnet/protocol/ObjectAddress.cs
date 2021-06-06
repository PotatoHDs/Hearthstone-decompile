using System;
using System.IO;

namespace bnet.protocol
{
	public class ObjectAddress : IProtoBuf
	{
		public bool HasObjectId;

		private ulong _ObjectId;

		public ProcessId Host { get; set; }

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

		public bool IsInitialized => true;

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Host.GetHashCode();
			if (HasObjectId)
			{
				hashCode ^= ObjectId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ObjectAddress objectAddress = obj as ObjectAddress;
			if (objectAddress == null)
			{
				return false;
			}
			if (!Host.Equals(objectAddress.Host))
			{
				return false;
			}
			if (HasObjectId != objectAddress.HasObjectId || (HasObjectId && !ObjectId.Equals(objectAddress.ObjectId)))
			{
				return false;
			}
			return true;
		}

		public static ObjectAddress ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ObjectAddress>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ObjectAddress DeserializeLengthDelimited(Stream stream)
		{
			ObjectAddress objectAddress = new ObjectAddress();
			DeserializeLengthDelimited(stream, objectAddress);
			return objectAddress;
		}

		public static ObjectAddress DeserializeLengthDelimited(Stream stream, ObjectAddress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance, long limit)
		{
			instance.ObjectId = 0uL;
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
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 16:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ObjectAddress instance)
		{
			if (instance.Host == null)
			{
				throw new ArgumentNullException("Host", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
			ProcessId.Serialize(stream, instance.Host);
			if (instance.HasObjectId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Host.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			return num + 1;
		}
	}
}
