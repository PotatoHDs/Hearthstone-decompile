using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class CreateSessionRequest : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasPlatform;

		private uint _Platform;

		public bool HasLocale;

		private uint _Locale;

		public bool HasClientAddress;

		private string _ClientAddress;

		public bool HasApplicationVersion;

		private int _ApplicationVersion;

		public bool HasUserAgent;

		private string _UserAgent;

		public bool HasSessionKey;

		private byte[] _SessionKey;

		public bool HasOptions;

		private SessionOptions _Options;

		public bool HasRequiresMarkAlive;

		private bool _RequiresMarkAlive;

		public bool HasMacAddress;

		private string _MacAddress;

		public bnet.protocol.account.v1.Identity Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public uint Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = true;
			}
		}

		public uint Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = true;
			}
		}

		public string ClientAddress
		{
			get
			{
				return _ClientAddress;
			}
			set
			{
				_ClientAddress = value;
				HasClientAddress = value != null;
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

		public byte[] SessionKey
		{
			get
			{
				return _SessionKey;
			}
			set
			{
				_SessionKey = value;
				HasSessionKey = value != null;
			}
		}

		public SessionOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool RequiresMarkAlive
		{
			get
			{
				return _RequiresMarkAlive;
			}
			set
			{
				_RequiresMarkAlive = value;
				HasRequiresMarkAlive = true;
			}
		}

		public string MacAddress
		{
			get
			{
				return _MacAddress;
			}
			set
			{
				_MacAddress = value;
				HasMacAddress = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void SetPlatform(uint val)
		{
			Platform = val;
		}

		public void SetLocale(uint val)
		{
			Locale = val;
		}

		public void SetClientAddress(string val)
		{
			ClientAddress = val;
		}

		public void SetApplicationVersion(int val)
		{
			ApplicationVersion = val;
		}

		public void SetUserAgent(string val)
		{
			UserAgent = val;
		}

		public void SetSessionKey(byte[] val)
		{
			SessionKey = val;
		}

		public void SetOptions(SessionOptions val)
		{
			Options = val;
		}

		public void SetRequiresMarkAlive(bool val)
		{
			RequiresMarkAlive = val;
		}

		public void SetMacAddress(string val)
		{
			MacAddress = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasPlatform)
			{
				num ^= Platform.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasClientAddress)
			{
				num ^= ClientAddress.GetHashCode();
			}
			if (HasApplicationVersion)
			{
				num ^= ApplicationVersion.GetHashCode();
			}
			if (HasUserAgent)
			{
				num ^= UserAgent.GetHashCode();
			}
			if (HasSessionKey)
			{
				num ^= SessionKey.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasRequiresMarkAlive)
			{
				num ^= RequiresMarkAlive.GetHashCode();
			}
			if (HasMacAddress)
			{
				num ^= MacAddress.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateSessionRequest createSessionRequest = obj as CreateSessionRequest;
			if (createSessionRequest == null)
			{
				return false;
			}
			if (HasIdentity != createSessionRequest.HasIdentity || (HasIdentity && !Identity.Equals(createSessionRequest.Identity)))
			{
				return false;
			}
			if (HasPlatform != createSessionRequest.HasPlatform || (HasPlatform && !Platform.Equals(createSessionRequest.Platform)))
			{
				return false;
			}
			if (HasLocale != createSessionRequest.HasLocale || (HasLocale && !Locale.Equals(createSessionRequest.Locale)))
			{
				return false;
			}
			if (HasClientAddress != createSessionRequest.HasClientAddress || (HasClientAddress && !ClientAddress.Equals(createSessionRequest.ClientAddress)))
			{
				return false;
			}
			if (HasApplicationVersion != createSessionRequest.HasApplicationVersion || (HasApplicationVersion && !ApplicationVersion.Equals(createSessionRequest.ApplicationVersion)))
			{
				return false;
			}
			if (HasUserAgent != createSessionRequest.HasUserAgent || (HasUserAgent && !UserAgent.Equals(createSessionRequest.UserAgent)))
			{
				return false;
			}
			if (HasSessionKey != createSessionRequest.HasSessionKey || (HasSessionKey && !SessionKey.Equals(createSessionRequest.SessionKey)))
			{
				return false;
			}
			if (HasOptions != createSessionRequest.HasOptions || (HasOptions && !Options.Equals(createSessionRequest.Options)))
			{
				return false;
			}
			if (HasRequiresMarkAlive != createSessionRequest.HasRequiresMarkAlive || (HasRequiresMarkAlive && !RequiresMarkAlive.Equals(createSessionRequest.RequiresMarkAlive)))
			{
				return false;
			}
			if (HasMacAddress != createSessionRequest.HasMacAddress || (HasMacAddress && !MacAddress.Equals(createSessionRequest.MacAddress)))
			{
				return false;
			}
			return true;
		}

		public static CreateSessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateSessionRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateSessionRequest Deserialize(Stream stream, CreateSessionRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateSessionRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateSessionRequest createSessionRequest = new CreateSessionRequest();
			DeserializeLengthDelimited(stream, createSessionRequest);
			return createSessionRequest;
		}

		public static CreateSessionRequest DeserializeLengthDelimited(Stream stream, CreateSessionRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateSessionRequest Deserialize(Stream stream, CreateSessionRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.RequiresMarkAlive = false;
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
					if (instance.Identity == null)
					{
						instance.Identity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 21:
					instance.Platform = binaryReader.ReadUInt32();
					continue;
				case 29:
					instance.Locale = binaryReader.ReadUInt32();
					continue;
				case 34:
					instance.ClientAddress = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.ApplicationVersion = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.UserAgent = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
					continue;
				case 66:
					if (instance.Options == null)
					{
						instance.Options = SessionOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SessionOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 72:
					instance.RequiresMarkAlive = ProtocolParser.ReadBool(stream);
					continue;
				case 82:
					instance.MacAddress = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, CreateSessionRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Platform);
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.HasClientAddress)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasApplicationVersion)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ApplicationVersion);
			}
			if (instance.HasUserAgent)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserAgent));
			}
			if (instance.HasSessionKey)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SessionOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasRequiresMarkAlive)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.RequiresMarkAlive);
			}
			if (instance.HasMacAddress)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MacAddress));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPlatform)
			{
				num++;
				num += 4;
			}
			if (HasLocale)
			{
				num++;
				num += 4;
			}
			if (HasClientAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasApplicationVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ApplicationVersion);
			}
			if (HasUserAgent)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasSessionKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SessionKey.Length) + SessionKey.Length);
			}
			if (HasOptions)
			{
				num++;
				uint serializedSize2 = Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRequiresMarkAlive)
			{
				num++;
				num++;
			}
			if (HasMacAddress)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(MacAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
