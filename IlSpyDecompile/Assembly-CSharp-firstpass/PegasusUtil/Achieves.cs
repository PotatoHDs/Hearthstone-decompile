using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class Achieves : IProtoBuf
	{
		private List<Achieve> _List = new List<Achieve>();

		public List<Achieve> List
		{
			get
			{
				return _List;
			}
			set
			{
				_List = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Achieve item in List)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Achieves achieves = obj as Achieves;
			if (achieves == null)
			{
				return false;
			}
			if (List.Count != achieves.List.Count)
			{
				return false;
			}
			for (int i = 0; i < List.Count; i++)
			{
				if (!List[i].Equals(achieves.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Achieves Deserialize(Stream stream, Achieves instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Achieves DeserializeLengthDelimited(Stream stream)
		{
			Achieves achieves = new Achieves();
			DeserializeLengthDelimited(stream, achieves);
			return achieves;
		}

		public static Achieves DeserializeLengthDelimited(Stream stream, Achieves instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Achieves Deserialize(Stream stream, Achieves instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<Achieve>();
			}
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
					instance.List.Add(Achieve.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, Achieves instance)
		{
			if (instance.List.Count <= 0)
			{
				return;
			}
			foreach (Achieve item in instance.List)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				Achieve.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (List.Count > 0)
			{
				foreach (Achieve item in List)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
