using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class Option : IProtoBuf
	{
		public enum Type
		{
			PASS = 1,
			END_TURN,
			POWER
		}

		public bool HasMainOption;

		private SubOption _MainOption;

		private List<SubOption> _SubOptions = new List<SubOption>();

		public Type Type_ { get; set; }

		public SubOption MainOption
		{
			get
			{
				return _MainOption;
			}
			set
			{
				_MainOption = value;
				HasMainOption = value != null;
			}
		}

		public List<SubOption> SubOptions
		{
			get
			{
				return _SubOptions;
			}
			set
			{
				_SubOptions = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type_.GetHashCode();
			if (HasMainOption)
			{
				hashCode ^= MainOption.GetHashCode();
			}
			foreach (SubOption subOption in SubOptions)
			{
				hashCode ^= subOption.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Option option = obj as Option;
			if (option == null)
			{
				return false;
			}
			if (!Type_.Equals(option.Type_))
			{
				return false;
			}
			if (HasMainOption != option.HasMainOption || (HasMainOption && !MainOption.Equals(option.MainOption)))
			{
				return false;
			}
			if (SubOptions.Count != option.SubOptions.Count)
			{
				return false;
			}
			for (int i = 0; i < SubOptions.Count; i++)
			{
				if (!SubOptions[i].Equals(option.SubOptions[i]))
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

		public static Option Deserialize(Stream stream, Option instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Option DeserializeLengthDelimited(Stream stream)
		{
			Option option = new Option();
			DeserializeLengthDelimited(stream, option);
			return option;
		}

		public static Option DeserializeLengthDelimited(Stream stream, Option instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Option Deserialize(Stream stream, Option instance, long limit)
		{
			if (instance.SubOptions == null)
			{
				instance.SubOptions = new List<SubOption>();
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
					instance.Type_ = (Type)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.MainOption == null)
					{
						instance.MainOption = SubOption.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubOption.DeserializeLengthDelimited(stream, instance.MainOption);
					}
					continue;
				case 26:
					instance.SubOptions.Add(SubOption.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, Option instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type_);
			if (instance.HasMainOption)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.MainOption.GetSerializedSize());
				SubOption.Serialize(stream, instance.MainOption);
			}
			if (instance.SubOptions.Count <= 0)
			{
				return;
			}
			foreach (SubOption subOption in instance.SubOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, subOption.GetSerializedSize());
				SubOption.Serialize(stream, subOption);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type_);
			if (HasMainOption)
			{
				num++;
				uint serializedSize = MainOption.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (SubOptions.Count > 0)
			{
				foreach (SubOption subOption in SubOptions)
				{
					num++;
					uint serializedSize2 = subOption.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 1;
		}
	}
}
