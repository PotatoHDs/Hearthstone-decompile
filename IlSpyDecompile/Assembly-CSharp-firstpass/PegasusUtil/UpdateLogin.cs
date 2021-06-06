using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class UpdateLogin : IProtoBuf
	{
		public enum PacketID
		{
			ID = 205,
			System = 0
		}

		public bool HasReplyRequired;

		private bool _ReplyRequired;

		public bool HasReferral;

		private string _Referral;

		public bool HasDeviceModelDeprecated;

		private string _DeviceModelDeprecated;

		public bool ReplyRequired
		{
			get
			{
				return _ReplyRequired;
			}
			set
			{
				_ReplyRequired = value;
				HasReplyRequired = true;
			}
		}

		public string Referral
		{
			get
			{
				return _Referral;
			}
			set
			{
				_Referral = value;
				HasReferral = value != null;
			}
		}

		public string DeviceModelDeprecated
		{
			get
			{
				return _DeviceModelDeprecated;
			}
			set
			{
				_DeviceModelDeprecated = value;
				HasDeviceModelDeprecated = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasReplyRequired)
			{
				num ^= ReplyRequired.GetHashCode();
			}
			if (HasReferral)
			{
				num ^= Referral.GetHashCode();
			}
			if (HasDeviceModelDeprecated)
			{
				num ^= DeviceModelDeprecated.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateLogin updateLogin = obj as UpdateLogin;
			if (updateLogin == null)
			{
				return false;
			}
			if (HasReplyRequired != updateLogin.HasReplyRequired || (HasReplyRequired && !ReplyRequired.Equals(updateLogin.ReplyRequired)))
			{
				return false;
			}
			if (HasReferral != updateLogin.HasReferral || (HasReferral && !Referral.Equals(updateLogin.Referral)))
			{
				return false;
			}
			if (HasDeviceModelDeprecated != updateLogin.HasDeviceModelDeprecated || (HasDeviceModelDeprecated && !DeviceModelDeprecated.Equals(updateLogin.DeviceModelDeprecated)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateLogin Deserialize(Stream stream, UpdateLogin instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateLogin DeserializeLengthDelimited(Stream stream)
		{
			UpdateLogin updateLogin = new UpdateLogin();
			DeserializeLengthDelimited(stream, updateLogin);
			return updateLogin;
		}

		public static UpdateLogin DeserializeLengthDelimited(Stream stream, UpdateLogin instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateLogin Deserialize(Stream stream, UpdateLogin instance, long limit)
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
					instance.ReplyRequired = ProtocolParser.ReadBool(stream);
					continue;
				case 18:
					instance.Referral = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.DeviceModelDeprecated = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, UpdateLogin instance)
		{
			if (instance.HasReplyRequired)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.ReplyRequired);
			}
			if (instance.HasReferral)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Referral));
			}
			if (instance.HasDeviceModelDeprecated)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceModelDeprecated));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasReplyRequired)
			{
				num++;
				num++;
			}
			if (HasReferral)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Referral);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceModelDeprecated)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceModelDeprecated);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
