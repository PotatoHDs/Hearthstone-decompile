using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class EntityChoices : IProtoBuf
	{
		public enum PacketID
		{
			ID = 17
		}

		private List<int> _Entities = new List<int>();

		public bool HasSource;

		private int _Source;

		public int Id { get; set; }

		public int ChoiceType { get; set; }

		public int CountMin { get; set; }

		public int CountMax { get; set; }

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

		public int Source
		{
			get
			{
				return _Source;
			}
			set
			{
				_Source = value;
				HasSource = true;
			}
		}

		public int PlayerId { get; set; }

		public bool HideChosen { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= ChoiceType.GetHashCode();
			hashCode ^= CountMin.GetHashCode();
			hashCode ^= CountMax.GetHashCode();
			foreach (int entity in Entities)
			{
				hashCode ^= entity.GetHashCode();
			}
			if (HasSource)
			{
				hashCode ^= Source.GetHashCode();
			}
			hashCode ^= PlayerId.GetHashCode();
			return hashCode ^ HideChosen.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			EntityChoices entityChoices = obj as EntityChoices;
			if (entityChoices == null)
			{
				return false;
			}
			if (!Id.Equals(entityChoices.Id))
			{
				return false;
			}
			if (!ChoiceType.Equals(entityChoices.ChoiceType))
			{
				return false;
			}
			if (!CountMin.Equals(entityChoices.CountMin))
			{
				return false;
			}
			if (!CountMax.Equals(entityChoices.CountMax))
			{
				return false;
			}
			if (Entities.Count != entityChoices.Entities.Count)
			{
				return false;
			}
			for (int i = 0; i < Entities.Count; i++)
			{
				if (!Entities[i].Equals(entityChoices.Entities[i]))
				{
					return false;
				}
			}
			if (HasSource != entityChoices.HasSource || (HasSource && !Source.Equals(entityChoices.Source)))
			{
				return false;
			}
			if (!PlayerId.Equals(entityChoices.PlayerId))
			{
				return false;
			}
			if (!HideChosen.Equals(entityChoices.HideChosen))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EntityChoices Deserialize(Stream stream, EntityChoices instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EntityChoices DeserializeLengthDelimited(Stream stream)
		{
			EntityChoices entityChoices = new EntityChoices();
			DeserializeLengthDelimited(stream, entityChoices);
			return entityChoices;
		}

		public static EntityChoices DeserializeLengthDelimited(Stream stream, EntityChoices instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EntityChoices Deserialize(Stream stream, EntityChoices instance, long limit)
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
				case 16:
					instance.ChoiceType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CountMin = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.CountMax = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
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
				case 56:
					instance.Source = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.HideChosen = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, EntityChoices instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ChoiceType);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CountMin);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CountMax);
			if (instance.Entities.Count > 0)
			{
				stream.WriteByte(50);
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
			if (instance.HasSource)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
			}
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.HideChosen);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)ChoiceType);
			num += ProtocolParser.SizeOfUInt64((ulong)CountMin);
			num += ProtocolParser.SizeOfUInt64((ulong)CountMax);
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
			if (HasSource)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Source);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			num++;
			return num + 6;
		}
	}
}
