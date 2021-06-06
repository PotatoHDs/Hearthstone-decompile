using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class AllOptions : IProtoBuf
	{
		public enum PacketID
		{
			ID = 14
		}

		private List<Option> _Options = new List<Option>();

		public int Id { get; set; }

		public List<Option> Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			foreach (Option option in Options)
			{
				hashCode ^= option.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AllOptions allOptions = obj as AllOptions;
			if (allOptions == null)
			{
				return false;
			}
			if (!Id.Equals(allOptions.Id))
			{
				return false;
			}
			if (Options.Count != allOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < Options.Count; i++)
			{
				if (!Options[i].Equals(allOptions.Options[i]))
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

		public static AllOptions Deserialize(Stream stream, AllOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AllOptions DeserializeLengthDelimited(Stream stream)
		{
			AllOptions allOptions = new AllOptions();
			DeserializeLengthDelimited(stream, allOptions);
			return allOptions;
		}

		public static AllOptions DeserializeLengthDelimited(Stream stream, AllOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AllOptions Deserialize(Stream stream, AllOptions instance, long limit)
		{
			if (instance.Options == null)
			{
				instance.Options = new List<Option>();
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
					instance.Options.Add(Option.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AllOptions instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Options.Count <= 0)
			{
				return;
			}
			foreach (Option option in instance.Options)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, option.GetSerializedSize());
				Option.Serialize(stream, option);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (Options.Count > 0)
			{
				foreach (Option option in Options)
				{
					num++;
					uint serializedSize = option.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
