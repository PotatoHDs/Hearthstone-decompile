using System;
using System.IO;

namespace PegasusGame
{
	public class EntitiesChosen : IProtoBuf
	{
		public enum PacketID
		{
			ID = 13
		}

		public bool HasChoiceType;

		private int _ChoiceType;

		public ChooseEntities ChooseEntities { get; set; }

		public int PlayerId { get; set; }

		public int ChoiceType
		{
			get
			{
				return _ChoiceType;
			}
			set
			{
				_ChoiceType = value;
				HasChoiceType = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ChooseEntities.GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			if (HasChoiceType)
			{
				hashCode ^= ChoiceType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			EntitiesChosen entitiesChosen = obj as EntitiesChosen;
			if (entitiesChosen == null)
			{
				return false;
			}
			if (!ChooseEntities.Equals(entitiesChosen.ChooseEntities))
			{
				return false;
			}
			if (!PlayerId.Equals(entitiesChosen.PlayerId))
			{
				return false;
			}
			if (HasChoiceType != entitiesChosen.HasChoiceType || (HasChoiceType && !ChoiceType.Equals(entitiesChosen.ChoiceType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EntitiesChosen Deserialize(Stream stream, EntitiesChosen instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EntitiesChosen DeserializeLengthDelimited(Stream stream)
		{
			EntitiesChosen entitiesChosen = new EntitiesChosen();
			DeserializeLengthDelimited(stream, entitiesChosen);
			return entitiesChosen;
		}

		public static EntitiesChosen DeserializeLengthDelimited(Stream stream, EntitiesChosen instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EntitiesChosen Deserialize(Stream stream, EntitiesChosen instance, long limit)
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
				case 26:
					if (instance.ChooseEntities == null)
					{
						instance.ChooseEntities = ChooseEntities.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChooseEntities.DeserializeLengthDelimited(stream, instance.ChooseEntities);
					}
					continue;
				case 32:
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.ChoiceType = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, EntitiesChosen instance)
		{
			if (instance.ChooseEntities == null)
			{
				throw new ArgumentNullException("ChooseEntities", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChooseEntities.GetSerializedSize());
			ChooseEntities.Serialize(stream, instance.ChooseEntities);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			if (instance.HasChoiceType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ChoiceType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ChooseEntities.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			if (HasChoiceType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ChoiceType);
			}
			return num + 2;
		}
	}
}
