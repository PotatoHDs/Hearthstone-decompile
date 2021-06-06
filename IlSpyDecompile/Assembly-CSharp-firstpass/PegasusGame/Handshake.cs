using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	public class Handshake : IProtoBuf
	{
		public enum PacketID
		{
			ID = 168
		}

		public bool HasMission;

		private int _Mission;

		public int GameHandle { get; set; }

		public string Password { get; set; }

		public long ClientHandle { get; set; }

		public int Mission
		{
			get
			{
				return _Mission;
			}
			set
			{
				_Mission = value;
				HasMission = true;
			}
		}

		public string Version { get; set; }

		public Platform Platform { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			hashCode ^= Password.GetHashCode();
			hashCode ^= ClientHandle.GetHashCode();
			if (HasMission)
			{
				hashCode ^= Mission.GetHashCode();
			}
			hashCode ^= Version.GetHashCode();
			return hashCode ^ Platform.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Handshake handshake = obj as Handshake;
			if (handshake == null)
			{
				return false;
			}
			if (!GameHandle.Equals(handshake.GameHandle))
			{
				return false;
			}
			if (!Password.Equals(handshake.Password))
			{
				return false;
			}
			if (!ClientHandle.Equals(handshake.ClientHandle))
			{
				return false;
			}
			if (HasMission != handshake.HasMission || (HasMission && !Mission.Equals(handshake.Mission)))
			{
				return false;
			}
			if (!Version.Equals(handshake.Version))
			{
				return false;
			}
			if (!Platform.Equals(handshake.Platform))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Handshake Deserialize(Stream stream, Handshake instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Handshake DeserializeLengthDelimited(Stream stream)
		{
			Handshake handshake = new Handshake();
			DeserializeLengthDelimited(stream, handshake);
			return handshake;
		}

		public static Handshake DeserializeLengthDelimited(Stream stream, Handshake instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Handshake Deserialize(Stream stream, Handshake instance, long limit)
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
					instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Password = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Mission = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Version = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
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

		public static void Serialize(Stream stream, Handshake instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GameHandle);
			if (instance.Password == null)
			{
				throw new ArgumentNullException("Password", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Password));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			if (instance.HasMission)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Mission);
			}
			if (instance.Version == null)
			{
				throw new ArgumentNullException("Version", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(58);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)GameHandle);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Password);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)ClientHandle);
			if (HasMission)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Mission);
			}
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Version);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			uint serializedSize = Platform.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 5;
		}
	}
}
