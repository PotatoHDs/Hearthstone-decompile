using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class Platform : IProtoBuf
	{
		public bool HasStore;

		private int _Store;

		public bool HasUniqueDeviceIdentifier;

		private string _UniqueDeviceIdentifier;

		public int Os { get; set; }

		public int Screen { get; set; }

		public string Name { get; set; }

		public int Store
		{
			get
			{
				return _Store;
			}
			set
			{
				_Store = value;
				HasStore = true;
			}
		}

		public string UniqueDeviceIdentifier
		{
			get
			{
				return _UniqueDeviceIdentifier;
			}
			set
			{
				_UniqueDeviceIdentifier = value;
				HasUniqueDeviceIdentifier = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Os.GetHashCode();
			hashCode ^= Screen.GetHashCode();
			hashCode ^= Name.GetHashCode();
			if (HasStore)
			{
				hashCode ^= Store.GetHashCode();
			}
			if (HasUniqueDeviceIdentifier)
			{
				hashCode ^= UniqueDeviceIdentifier.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Platform platform = obj as Platform;
			if (platform == null)
			{
				return false;
			}
			if (!Os.Equals(platform.Os))
			{
				return false;
			}
			if (!Screen.Equals(platform.Screen))
			{
				return false;
			}
			if (!Name.Equals(platform.Name))
			{
				return false;
			}
			if (HasStore != platform.HasStore || (HasStore && !Store.Equals(platform.Store)))
			{
				return false;
			}
			if (HasUniqueDeviceIdentifier != platform.HasUniqueDeviceIdentifier || (HasUniqueDeviceIdentifier && !UniqueDeviceIdentifier.Equals(platform.UniqueDeviceIdentifier)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Platform Deserialize(Stream stream, Platform instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Platform DeserializeLengthDelimited(Stream stream)
		{
			Platform platform = new Platform();
			DeserializeLengthDelimited(stream, platform);
			return platform;
		}

		public static Platform DeserializeLengthDelimited(Stream stream, Platform instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Platform Deserialize(Stream stream, Platform instance, long limit)
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
				case 8:
					instance.Os = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Screen = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.Store = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.UniqueDeviceIdentifier = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, Platform instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Os);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Screen);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.HasStore)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Store);
			}
			if (instance.HasUniqueDeviceIdentifier)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UniqueDeviceIdentifier));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Os);
			num += ProtocolParser.SizeOfUInt64((ulong)Screen);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasStore)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Store);
			}
			if (HasUniqueDeviceIdentifier)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(UniqueDeviceIdentifier);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 3;
		}
	}
}
