using System.IO;
using System.Text;

namespace BobNetProto
{
	public class Deadend : IProtoBuf
	{
		public enum PacketID
		{
			ID = 169
		}

		public bool HasReply1;

		private string _Reply1;

		public bool HasReply2;

		private string _Reply2;

		public bool HasReply3;

		private string _Reply3;

		public string Reply1
		{
			get
			{
				return _Reply1;
			}
			set
			{
				_Reply1 = value;
				HasReply1 = value != null;
			}
		}

		public string Reply2
		{
			get
			{
				return _Reply2;
			}
			set
			{
				_Reply2 = value;
				HasReply2 = value != null;
			}
		}

		public string Reply3
		{
			get
			{
				return _Reply3;
			}
			set
			{
				_Reply3 = value;
				HasReply3 = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasReply1)
			{
				num ^= Reply1.GetHashCode();
			}
			if (HasReply2)
			{
				num ^= Reply2.GetHashCode();
			}
			if (HasReply3)
			{
				num ^= Reply3.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Deadend deadend = obj as Deadend;
			if (deadend == null)
			{
				return false;
			}
			if (HasReply1 != deadend.HasReply1 || (HasReply1 && !Reply1.Equals(deadend.Reply1)))
			{
				return false;
			}
			if (HasReply2 != deadend.HasReply2 || (HasReply2 && !Reply2.Equals(deadend.Reply2)))
			{
				return false;
			}
			if (HasReply3 != deadend.HasReply3 || (HasReply3 && !Reply3.Equals(deadend.Reply3)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Deadend Deserialize(Stream stream, Deadend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Deadend DeserializeLengthDelimited(Stream stream)
		{
			Deadend deadend = new Deadend();
			DeserializeLengthDelimited(stream, deadend);
			return deadend;
		}

		public static Deadend DeserializeLengthDelimited(Stream stream, Deadend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Deadend Deserialize(Stream stream, Deadend instance, long limit)
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
					instance.Reply1 = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Reply2 = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Reply3 = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, Deadend instance)
		{
			if (instance.HasReply1)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply1));
			}
			if (instance.HasReply2)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply2));
			}
			if (instance.HasReply3)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply3));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasReply1)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Reply1);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasReply2)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Reply2);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasReply3)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Reply3);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
