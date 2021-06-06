using System;
using System.IO;

namespace SpectatorProto
{
	public class SecretJoinInfo : IProtoBuf
	{
		public bool HasSpecificSourceIdentity;

		private long _SpecificSourceIdentity;

		public SecretSource Source { get; set; }

		public long SpecificSourceIdentity
		{
			get
			{
				return _SpecificSourceIdentity;
			}
			set
			{
				_SpecificSourceIdentity = value;
				HasSpecificSourceIdentity = true;
			}
		}

		public byte[] EncryptedMessage { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Source.GetHashCode();
			if (HasSpecificSourceIdentity)
			{
				hashCode ^= SpecificSourceIdentity.GetHashCode();
			}
			return hashCode ^ EncryptedMessage.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SecretJoinInfo secretJoinInfo = obj as SecretJoinInfo;
			if (secretJoinInfo == null)
			{
				return false;
			}
			if (!Source.Equals(secretJoinInfo.Source))
			{
				return false;
			}
			if (HasSpecificSourceIdentity != secretJoinInfo.HasSpecificSourceIdentity || (HasSpecificSourceIdentity && !SpecificSourceIdentity.Equals(secretJoinInfo.SpecificSourceIdentity)))
			{
				return false;
			}
			if (!EncryptedMessage.Equals(secretJoinInfo.EncryptedMessage))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SecretJoinInfo Deserialize(Stream stream, SecretJoinInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SecretJoinInfo DeserializeLengthDelimited(Stream stream)
		{
			SecretJoinInfo secretJoinInfo = new SecretJoinInfo();
			DeserializeLengthDelimited(stream, secretJoinInfo);
			return secretJoinInfo;
		}

		public static SecretJoinInfo DeserializeLengthDelimited(Stream stream, SecretJoinInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SecretJoinInfo Deserialize(Stream stream, SecretJoinInfo instance, long limit)
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
					instance.Source = (SecretSource)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.SpecificSourceIdentity = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.EncryptedMessage = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, SecretJoinInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
			if (instance.HasSpecificSourceIdentity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SpecificSourceIdentity);
			}
			if (instance.EncryptedMessage == null)
			{
				throw new ArgumentNullException("EncryptedMessage", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.EncryptedMessage);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Source);
			if (HasSpecificSourceIdentity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SpecificSourceIdentity);
			}
			num += (uint)((int)ProtocolParser.SizeOfUInt32(EncryptedMessage.Length) + EncryptedMessage.Length);
			return num + 2;
		}
	}
}
