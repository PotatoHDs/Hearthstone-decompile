using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	public class LogonRequest : IProtoBuf
	{
		public bool HasProgram;

		private string _Program;

		public bool HasPlatform;

		private string _Platform;

		public bool HasLocale;

		private string _Locale;

		public bool HasEmail;

		private string _Email;

		public bool HasVersion;

		private string _Version;

		public bool HasApplicationVersion;

		private int _ApplicationVersion;

		public bool HasPublicComputer;

		private bool _PublicComputer;

		public bool HasAllowLogonQueueNotifications;

		private bool _AllowLogonQueueNotifications;

		public bool HasCachedWebCredentials;

		private byte[] _CachedWebCredentials;

		public bool HasUserAgent;

		private string _UserAgent;

		public bool HasDeviceId;

		private string _DeviceId;

		public string Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = value != null;
			}
		}

		public string Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = value != null;
			}
		}

		public string Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = value != null;
			}
		}

		public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				_Email = value;
				HasEmail = value != null;
			}
		}

		public string Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = value != null;
			}
		}

		public int ApplicationVersion
		{
			get
			{
				return _ApplicationVersion;
			}
			set
			{
				_ApplicationVersion = value;
				HasApplicationVersion = true;
			}
		}

		public bool PublicComputer
		{
			get
			{
				return _PublicComputer;
			}
			set
			{
				_PublicComputer = value;
				HasPublicComputer = true;
			}
		}

		public bool AllowLogonQueueNotifications
		{
			get
			{
				return _AllowLogonQueueNotifications;
			}
			set
			{
				_AllowLogonQueueNotifications = value;
				HasAllowLogonQueueNotifications = true;
			}
		}

		public byte[] CachedWebCredentials
		{
			get
			{
				return _CachedWebCredentials;
			}
			set
			{
				_CachedWebCredentials = value;
				HasCachedWebCredentials = value != null;
			}
		}

		public string UserAgent
		{
			get
			{
				return _UserAgent;
			}
			set
			{
				_UserAgent = value;
				HasUserAgent = value != null;
			}
		}

		public string DeviceId
		{
			get
			{
				return _DeviceId;
			}
			set
			{
				_DeviceId = value;
				HasDeviceId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetProgram(string val)
		{
			Program = val;
		}

		public void SetPlatform(string val)
		{
			Platform = val;
		}

		public void SetLocale(string val)
		{
			Locale = val;
		}

		public void SetEmail(string val)
		{
			Email = val;
		}

		public void SetVersion(string val)
		{
			Version = val;
		}

		public void SetApplicationVersion(int val)
		{
			ApplicationVersion = val;
		}

		public void SetPublicComputer(bool val)
		{
			PublicComputer = val;
		}

		public void SetAllowLogonQueueNotifications(bool val)
		{
			AllowLogonQueueNotifications = val;
		}

		public void SetCachedWebCredentials(byte[] val)
		{
			CachedWebCredentials = val;
		}

		public void SetUserAgent(string val)
		{
			UserAgent = val;
		}

		public void SetDeviceId(string val)
		{
			DeviceId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasPlatform)
			{
				num ^= Platform.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasEmail)
			{
				num ^= Email.GetHashCode();
			}
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			if (HasApplicationVersion)
			{
				num ^= ApplicationVersion.GetHashCode();
			}
			if (HasPublicComputer)
			{
				num ^= PublicComputer.GetHashCode();
			}
			if (HasAllowLogonQueueNotifications)
			{
				num ^= AllowLogonQueueNotifications.GetHashCode();
			}
			if (HasCachedWebCredentials)
			{
				num ^= CachedWebCredentials.GetHashCode();
			}
			if (HasUserAgent)
			{
				num ^= UserAgent.GetHashCode();
			}
			if (HasDeviceId)
			{
				num ^= DeviceId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LogonRequest logonRequest = obj as LogonRequest;
			if (logonRequest == null)
			{
				return false;
			}
			if (HasProgram != logonRequest.HasProgram || (HasProgram && !Program.Equals(logonRequest.Program)))
			{
				return false;
			}
			if (HasPlatform != logonRequest.HasPlatform || (HasPlatform && !Platform.Equals(logonRequest.Platform)))
			{
				return false;
			}
			if (HasLocale != logonRequest.HasLocale || (HasLocale && !Locale.Equals(logonRequest.Locale)))
			{
				return false;
			}
			if (HasEmail != logonRequest.HasEmail || (HasEmail && !Email.Equals(logonRequest.Email)))
			{
				return false;
			}
			if (HasVersion != logonRequest.HasVersion || (HasVersion && !Version.Equals(logonRequest.Version)))
			{
				return false;
			}
			if (HasApplicationVersion != logonRequest.HasApplicationVersion || (HasApplicationVersion && !ApplicationVersion.Equals(logonRequest.ApplicationVersion)))
			{
				return false;
			}
			if (HasPublicComputer != logonRequest.HasPublicComputer || (HasPublicComputer && !PublicComputer.Equals(logonRequest.PublicComputer)))
			{
				return false;
			}
			if (HasAllowLogonQueueNotifications != logonRequest.HasAllowLogonQueueNotifications || (HasAllowLogonQueueNotifications && !AllowLogonQueueNotifications.Equals(logonRequest.AllowLogonQueueNotifications)))
			{
				return false;
			}
			if (HasCachedWebCredentials != logonRequest.HasCachedWebCredentials || (HasCachedWebCredentials && !CachedWebCredentials.Equals(logonRequest.CachedWebCredentials)))
			{
				return false;
			}
			if (HasUserAgent != logonRequest.HasUserAgent || (HasUserAgent && !UserAgent.Equals(logonRequest.UserAgent)))
			{
				return false;
			}
			if (HasDeviceId != logonRequest.HasDeviceId || (HasDeviceId && !DeviceId.Equals(logonRequest.DeviceId)))
			{
				return false;
			}
			return true;
		}

		public static LogonRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LogonRequest Deserialize(Stream stream, LogonRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LogonRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonRequest logonRequest = new LogonRequest();
			DeserializeLengthDelimited(stream, logonRequest);
			return logonRequest;
		}

		public static LogonRequest DeserializeLengthDelimited(Stream stream, LogonRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LogonRequest Deserialize(Stream stream, LogonRequest instance, long limit)
		{
			instance.AllowLogonQueueNotifications = false;
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
					instance.Program = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Platform = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Locale = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Email = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.Version = ProtocolParser.ReadString(stream);
					continue;
				case 48:
					instance.ApplicationVersion = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.PublicComputer = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.AllowLogonQueueNotifications = ProtocolParser.ReadBool(stream);
					continue;
				case 98:
					instance.CachedWebCredentials = ProtocolParser.ReadBytes(stream);
					continue;
				case 114:
					instance.UserAgent = ProtocolParser.ReadString(stream);
					continue;
				case 122:
					instance.DeviceId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, LogonRequest instance)
		{
			if (instance.HasProgram)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Program));
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Platform));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasApplicationVersion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ApplicationVersion);
			}
			if (instance.HasPublicComputer)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.PublicComputer);
			}
			if (instance.HasAllowLogonQueueNotifications)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.AllowLogonQueueNotifications);
			}
			if (instance.HasCachedWebCredentials)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, instance.CachedWebCredentials);
			}
			if (instance.HasUserAgent)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserAgent));
			}
			if (instance.HasDeviceId)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasProgram)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Program);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPlatform)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Platform);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasLocale)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasEmail)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Email);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasVersion)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(Version);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasApplicationVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ApplicationVersion);
			}
			if (HasPublicComputer)
			{
				num++;
				num++;
			}
			if (HasAllowLogonQueueNotifications)
			{
				num++;
				num++;
			}
			if (HasCachedWebCredentials)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(CachedWebCredentials.Length) + CachedWebCredentials.Length);
			}
			if (HasUserAgent)
			{
				num++;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (HasDeviceId)
			{
				num++;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(DeviceId);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			return num;
		}
	}
}
