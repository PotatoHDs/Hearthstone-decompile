using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class AIContextualValue : IProtoBuf
	{
		public string EntityName { get; set; }

		public int EntityID { get; set; }

		public int ContextualScore { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ EntityName.GetHashCode() ^ EntityID.GetHashCode() ^ ContextualScore.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AIContextualValue aIContextualValue = obj as AIContextualValue;
			if (aIContextualValue == null)
			{
				return false;
			}
			if (!EntityName.Equals(aIContextualValue.EntityName))
			{
				return false;
			}
			if (!EntityID.Equals(aIContextualValue.EntityID))
			{
				return false;
			}
			if (!ContextualScore.Equals(aIContextualValue.ContextualScore))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AIContextualValue Deserialize(Stream stream, AIContextualValue instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AIContextualValue DeserializeLengthDelimited(Stream stream)
		{
			AIContextualValue aIContextualValue = new AIContextualValue();
			DeserializeLengthDelimited(stream, aIContextualValue);
			return aIContextualValue;
		}

		public static AIContextualValue DeserializeLengthDelimited(Stream stream, AIContextualValue instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AIContextualValue Deserialize(Stream stream, AIContextualValue instance, long limit)
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
					instance.EntityName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.ContextualScore = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AIContextualValue instance)
		{
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EntityID);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ContextualScore);
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(EntityName);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)EntityID) + ProtocolParser.SizeOfUInt64((ulong)ContextualScore) + 3;
		}
	}
}
