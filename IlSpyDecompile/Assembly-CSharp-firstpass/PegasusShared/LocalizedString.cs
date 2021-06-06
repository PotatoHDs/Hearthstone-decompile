using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class LocalizedString : IProtoBuf
	{
		public bool HasDeprecatedValue;

		private string _DeprecatedValue;

		public bool HasDeprecatedLocale;

		private int _DeprecatedLocale;

		private List<LocalizedStringValue> _Values = new List<LocalizedStringValue>();

		public string Key { get; set; }

		public string DeprecatedValue
		{
			get
			{
				return _DeprecatedValue;
			}
			set
			{
				_DeprecatedValue = value;
				HasDeprecatedValue = value != null;
			}
		}

		public int DeprecatedLocale
		{
			get
			{
				return _DeprecatedLocale;
			}
			set
			{
				_DeprecatedLocale = value;
				HasDeprecatedLocale = true;
			}
		}

		public List<LocalizedStringValue> Values
		{
			get
			{
				return _Values;
			}
			set
			{
				_Values = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Key.GetHashCode();
			if (HasDeprecatedValue)
			{
				hashCode ^= DeprecatedValue.GetHashCode();
			}
			if (HasDeprecatedLocale)
			{
				hashCode ^= DeprecatedLocale.GetHashCode();
			}
			foreach (LocalizedStringValue value in Values)
			{
				hashCode ^= value.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			LocalizedString localizedString = obj as LocalizedString;
			if (localizedString == null)
			{
				return false;
			}
			if (!Key.Equals(localizedString.Key))
			{
				return false;
			}
			if (HasDeprecatedValue != localizedString.HasDeprecatedValue || (HasDeprecatedValue && !DeprecatedValue.Equals(localizedString.DeprecatedValue)))
			{
				return false;
			}
			if (HasDeprecatedLocale != localizedString.HasDeprecatedLocale || (HasDeprecatedLocale && !DeprecatedLocale.Equals(localizedString.DeprecatedLocale)))
			{
				return false;
			}
			if (Values.Count != localizedString.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < Values.Count; i++)
			{
				if (!Values[i].Equals(localizedString.Values[i]))
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

		public static LocalizedString Deserialize(Stream stream, LocalizedString instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LocalizedString DeserializeLengthDelimited(Stream stream)
		{
			LocalizedString localizedString = new LocalizedString();
			DeserializeLengthDelimited(stream, localizedString);
			return localizedString;
		}

		public static LocalizedString DeserializeLengthDelimited(Stream stream, LocalizedString instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LocalizedString Deserialize(Stream stream, LocalizedString instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<LocalizedStringValue>();
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
					instance.Key = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.DeprecatedValue = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.DeprecatedLocale = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Values.Add(LocalizedStringValue.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, LocalizedString instance)
		{
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Key));
			if (instance.HasDeprecatedValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedValue));
			}
			if (instance.HasDeprecatedLocale)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedLocale);
			}
			if (instance.Values.Count <= 0)
			{
				return;
			}
			foreach (LocalizedStringValue value in instance.Values)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, value.GetSerializedSize());
				LocalizedStringValue.Serialize(stream, value);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Key);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasDeprecatedValue)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeprecatedValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasDeprecatedLocale)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedLocale);
			}
			if (Values.Count > 0)
			{
				foreach (LocalizedStringValue value in Values)
				{
					num++;
					uint serializedSize = value.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
