using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	public class Locale : IProtoBuf
	{
		private List<string> _Flag = new List<string>();

		public string Identifier { get; set; }

		public string Description { get; set; }

		public List<string> Flag
		{
			get
			{
				return _Flag;
			}
			set
			{
				_Flag = value;
			}
		}

		public List<string> FlagList => _Flag;

		public int FlagCount => _Flag.Count;

		public bool IsInitialized => true;

		public void SetIdentifier(string val)
		{
			Identifier = val;
		}

		public void SetDescription(string val)
		{
			Description = val;
		}

		public void AddFlag(string val)
		{
			_Flag.Add(val);
		}

		public void ClearFlag()
		{
			_Flag.Clear();
		}

		public void SetFlag(List<string> val)
		{
			Flag = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Identifier.GetHashCode();
			hashCode ^= Description.GetHashCode();
			foreach (string item in Flag)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Locale locale = obj as Locale;
			if (locale == null)
			{
				return false;
			}
			if (!Identifier.Equals(locale.Identifier))
			{
				return false;
			}
			if (!Description.Equals(locale.Description))
			{
				return false;
			}
			if (Flag.Count != locale.Flag.Count)
			{
				return false;
			}
			for (int i = 0; i < Flag.Count; i++)
			{
				if (!Flag[i].Equals(locale.Flag[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static Locale ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Locale>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Locale Deserialize(Stream stream, Locale instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Locale DeserializeLengthDelimited(Stream stream)
		{
			Locale locale = new Locale();
			DeserializeLengthDelimited(stream, locale);
			return locale;
		}

		public static Locale DeserializeLengthDelimited(Stream stream, Locale instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Locale Deserialize(Stream stream, Locale instance, long limit)
		{
			if (instance.Flag == null)
			{
				instance.Flag = new List<string>();
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
					instance.Identifier = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Description = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Flag.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, Locale instance)
		{
			if (instance.Identifier == null)
			{
				throw new ArgumentNullException("Identifier", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identifier));
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			if (instance.Flag.Count <= 0)
			{
				return;
			}
			foreach (string item in instance.Flag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Identifier);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Description);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (Flag.Count > 0)
			{
				foreach (string item in Flag)
				{
					num++;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(item);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			return num + 2;
		}
	}
}
