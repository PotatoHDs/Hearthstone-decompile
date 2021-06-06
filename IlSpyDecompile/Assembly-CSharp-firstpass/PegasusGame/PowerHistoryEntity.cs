using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class PowerHistoryEntity : IProtoBuf
	{
		private List<Tag> _Tags = new List<Tag>();

		private List<Tag> _DefTags = new List<Tag>();

		public int Entity { get; set; }

		public string Name { get; set; }

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

		public List<Tag> DefTags
		{
			get
			{
				return _DefTags;
			}
			set
			{
				_DefTags = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Entity.GetHashCode();
			hashCode ^= Name.GetHashCode();
			foreach (Tag tag in Tags)
			{
				hashCode ^= tag.GetHashCode();
			}
			foreach (Tag defTag in DefTags)
			{
				hashCode ^= defTag.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryEntity powerHistoryEntity = obj as PowerHistoryEntity;
			if (powerHistoryEntity == null)
			{
				return false;
			}
			if (!Entity.Equals(powerHistoryEntity.Entity))
			{
				return false;
			}
			if (!Name.Equals(powerHistoryEntity.Name))
			{
				return false;
			}
			if (Tags.Count != powerHistoryEntity.Tags.Count)
			{
				return false;
			}
			for (int i = 0; i < Tags.Count; i++)
			{
				if (!Tags[i].Equals(powerHistoryEntity.Tags[i]))
				{
					return false;
				}
			}
			if (DefTags.Count != powerHistoryEntity.DefTags.Count)
			{
				return false;
			}
			for (int j = 0; j < DefTags.Count; j++)
			{
				if (!DefTags[j].Equals(powerHistoryEntity.DefTags[j]))
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

		public static PowerHistoryEntity Deserialize(Stream stream, PowerHistoryEntity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryEntity DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryEntity powerHistoryEntity = new PowerHistoryEntity();
			DeserializeLengthDelimited(stream, powerHistoryEntity);
			return powerHistoryEntity;
		}

		public static PowerHistoryEntity DeserializeLengthDelimited(Stream stream, PowerHistoryEntity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryEntity Deserialize(Stream stream, PowerHistoryEntity instance, long limit)
		{
			if (instance.Tags == null)
			{
				instance.Tags = new List<Tag>();
			}
			if (instance.DefTags == null)
			{
				instance.DefTags = new List<Tag>();
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
					instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Tags.Add(Tag.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.DefTags.Add(Tag.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PowerHistoryEntity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entity);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Tags.Count > 0)
			{
				foreach (Tag tag in instance.Tags)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, tag.GetSerializedSize());
					Tag.Serialize(stream, tag);
				}
			}
			if (instance.DefTags.Count <= 0)
			{
				return;
			}
			foreach (Tag defTag in instance.DefTags)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, defTag.GetSerializedSize());
				Tag.Serialize(stream, defTag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Entity);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Tags.Count > 0)
			{
				foreach (Tag tag in Tags)
				{
					num++;
					uint serializedSize = tag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (DefTags.Count > 0)
			{
				foreach (Tag defTag in DefTags)
				{
					num++;
					uint serializedSize2 = defTag.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 2;
		}
	}
}
