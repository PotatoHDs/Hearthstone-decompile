using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	public class SpectatorHandshake : IProtoBuf
	{
		public enum PacketID
		{
			ID = 22
		}

		public int GameHandle { get; set; }

		public string Password { get; set; }

		public string Version { get; set; }

		public Platform Platform { get; set; }

		public BnetId GameAccountId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GameHandle.GetHashCode() ^ Password.GetHashCode() ^ Version.GetHashCode() ^ Platform.GetHashCode() ^ GameAccountId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SpectatorHandshake spectatorHandshake = obj as SpectatorHandshake;
			if (spectatorHandshake == null)
			{
				return false;
			}
			if (!GameHandle.Equals(spectatorHandshake.GameHandle))
			{
				return false;
			}
			if (!Password.Equals(spectatorHandshake.Password))
			{
				return false;
			}
			if (!Version.Equals(spectatorHandshake.Version))
			{
				return false;
			}
			if (!Platform.Equals(spectatorHandshake.Platform))
			{
				return false;
			}
			if (!GameAccountId.Equals(spectatorHandshake.GameAccountId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpectatorHandshake Deserialize(Stream stream, SpectatorHandshake instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpectatorHandshake DeserializeLengthDelimited(Stream stream)
		{
			SpectatorHandshake spectatorHandshake = new SpectatorHandshake();
			DeserializeLengthDelimited(stream, spectatorHandshake);
			return spectatorHandshake;
		}

		public static SpectatorHandshake DeserializeLengthDelimited(Stream stream, SpectatorHandshake instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpectatorHandshake Deserialize(Stream stream, SpectatorHandshake instance, long limit)
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
				case 26:
					instance.Version = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
					continue;
				case 42:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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

		public static void Serialize(Stream stream, SpectatorHandshake instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GameHandle);
			if (instance.Password == null)
			{
				throw new ArgumentNullException("Password", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Password));
			if (instance.Version == null)
			{
				throw new ArgumentNullException("Version", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)GameHandle);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Password);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Version);
			uint num3 = num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2);
			uint serializedSize = Platform.GetSerializedSize();
			uint num4 = num3 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = GameAccountId.GetSerializedSize();
			return num4 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 5;
		}
	}
}
