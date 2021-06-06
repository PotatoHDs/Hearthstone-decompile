using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	public class Locales : IProtoBuf
	{
		private List<Locale> _Locale = new List<Locale>();

		public List<Locale> Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
			}
		}

		public List<Locale> LocaleList => _Locale;

		public int LocaleCount => _Locale.Count;

		public bool IsInitialized => true;

		public void AddLocale(Locale val)
		{
			_Locale.Add(val);
		}

		public void ClearLocale()
		{
			_Locale.Clear();
		}

		public void SetLocale(List<Locale> val)
		{
			Locale = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Locale item in Locale)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Locales locales = obj as Locales;
			if (locales == null)
			{
				return false;
			}
			if (Locale.Count != locales.Locale.Count)
			{
				return false;
			}
			for (int i = 0; i < Locale.Count; i++)
			{
				if (!Locale[i].Equals(locales.Locale[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static Locales ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Locales>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Locales Deserialize(Stream stream, Locales instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Locales DeserializeLengthDelimited(Stream stream)
		{
			Locales locales = new Locales();
			DeserializeLengthDelimited(stream, locales);
			return locales;
		}

		public static Locales DeserializeLengthDelimited(Stream stream, Locales instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Locales Deserialize(Stream stream, Locales instance, long limit)
		{
			if (instance.Locale == null)
			{
				instance.Locale = new List<Locale>();
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
					instance.Locale.Add(bnet.protocol.config.Locale.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, Locales instance)
		{
			if (instance.Locale.Count <= 0)
			{
				return;
			}
			foreach (Locale item in instance.Locale)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.config.Locale.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Locale.Count > 0)
			{
				foreach (Locale item in Locale)
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
