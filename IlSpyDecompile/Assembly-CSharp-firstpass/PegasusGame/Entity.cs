using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class Entity : IProtoBuf
	{
		private List<Tag> _Tags = new List<Tag>();

		public int Id { get; set; }

		public List<Tag> Tags
		{
			get
			{
				return _Tags;
			}
			set
			{
				_Tags = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			foreach (Tag tag in Tags)
			{
				hashCode ^= tag.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Entity entity = obj as Entity;
			if (entity == null)
			{
				return false;
			}
			if (!Id.Equals(entity.Id))
			{
				return false;
			}
			if (Tags.Count != entity.Tags.Count)
			{
				return false;
			}
			for (int i = 0; i < Tags.Count; i++)
			{
				if (!Tags[i].Equals(entity.Tags[i]))
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

		public static Entity Deserialize(Stream stream, Entity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Entity DeserializeLengthDelimited(Stream stream)
		{
			Entity entity = new Entity();
			DeserializeLengthDelimited(stream, entity);
			return entity;
		}

		public static Entity DeserializeLengthDelimited(Stream stream, Entity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Entity Deserialize(Stream stream, Entity instance, long limit)
		{
			if (instance.Tags == null)
			{
				instance.Tags = new List<Tag>();
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
				case 8:
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Tags.Add(Tag.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, Entity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Tags.Count <= 0)
			{
				return;
			}
			foreach (Tag tag in instance.Tags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, tag.GetSerializedSize());
				Tag.Serialize(stream, tag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (Tags.Count > 0)
			{
				foreach (Tag tag in Tags)
				{
					num++;
					uint serializedSize = tag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
