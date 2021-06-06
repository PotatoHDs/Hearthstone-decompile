using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class ChooseEntities : IProtoBuf
	{
		public enum PacketID
		{
			ID = 3
		}

		private List<int> _Entities = new List<int>();

		public int Id { get; set; }

		public List<int> Entities
		{
			get
			{
				return _Entities;
			}
			set
			{
				_Entities = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			foreach (int entity in Entities)
			{
				hashCode ^= entity.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ChooseEntities chooseEntities = obj as ChooseEntities;
			if (chooseEntities == null)
			{
				return false;
			}
			if (!Id.Equals(chooseEntities.Id))
			{
				return false;
			}
			if (Entities.Count != chooseEntities.Entities.Count)
			{
				return false;
			}
			for (int i = 0; i < Entities.Count; i++)
			{
				if (!Entities[i].Equals(chooseEntities.Entities[i]))
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

		public static ChooseEntities Deserialize(Stream stream, ChooseEntities instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChooseEntities DeserializeLengthDelimited(Stream stream)
		{
			ChooseEntities chooseEntities = new ChooseEntities();
			DeserializeLengthDelimited(stream, chooseEntities);
			return chooseEntities;
		}

		public static ChooseEntities DeserializeLengthDelimited(Stream stream, ChooseEntities instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChooseEntities Deserialize(Stream stream, ChooseEntities instance, long limit)
		{
			if (instance.Entities == null)
			{
				instance.Entities = new List<int>();
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
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Entities.Add((int)ProtocolParser.ReadUInt64(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
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

		public static void Serialize(Stream stream, ChooseEntities instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Entities.Count <= 0)
			{
				return;
			}
			stream.WriteByte(18);
			uint num = 0u;
			foreach (int entity in instance.Entities)
			{
				num += ProtocolParser.SizeOfUInt64((ulong)entity);
			}
			ProtocolParser.WriteUInt32(stream, num);
			foreach (int entity2 in instance.Entities)
			{
				ProtocolParser.WriteUInt64(stream, (ulong)entity2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (Entities.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (int entity in Entities)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)entity);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num + 1;
		}
	}
}
