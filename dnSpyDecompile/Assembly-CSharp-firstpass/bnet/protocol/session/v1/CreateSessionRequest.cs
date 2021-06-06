using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000307 RID: 775
	public class CreateSessionRequest : IProtoBuf
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x0009F098 File Offset: 0x0009D298
		// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x0009F0A0 File Offset: 0x0009D2A0
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0009F0B3 File Offset: 0x0009D2B3
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x0009F0BC File Offset: 0x0009D2BC
		// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
		public uint Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = true;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0009F0D4 File Offset: 0x0009D2D4
		public void SetPlatform(uint val)
		{
			this.Platform = val;
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x0009F0DD File Offset: 0x0009D2DD
		// (set) Token: 0x06002ECB RID: 11979 RVA: 0x0009F0E5 File Offset: 0x0009D2E5
		public uint Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = true;
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0009F0F5 File Offset: 0x0009D2F5
		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0009F0FE File Offset: 0x0009D2FE
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x0009F106 File Offset: 0x0009D306
		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0009F119 File Offset: 0x0009D319
		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x0009F122 File Offset: 0x0009D322
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x0009F12A File Offset: 0x0009D32A
		public int ApplicationVersion
		{
			get
			{
				return this._ApplicationVersion;
			}
			set
			{
				this._ApplicationVersion = value;
				this.HasApplicationVersion = true;
			}
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0009F13A File Offset: 0x0009D33A
		public void SetApplicationVersion(int val)
		{
			this.ApplicationVersion = val;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x0009F143 File Offset: 0x0009D343
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x0009F14B File Offset: 0x0009D34B
		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this._UserAgent = value;
				this.HasUserAgent = (value != null);
			}
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0009F15E File Offset: 0x0009D35E
		public void SetUserAgent(string val)
		{
			this.UserAgent = val;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x0009F167 File Offset: 0x0009D367
		// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x0009F16F File Offset: 0x0009D36F
		public byte[] SessionKey
		{
			get
			{
				return this._SessionKey;
			}
			set
			{
				this._SessionKey = value;
				this.HasSessionKey = (value != null);
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0009F182 File Offset: 0x0009D382
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x0009F18B File Offset: 0x0009D38B
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x0009F193 File Offset: 0x0009D393
		public SessionOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0009F1A6 File Offset: 0x0009D3A6
		public void SetOptions(SessionOptions val)
		{
			this.Options = val;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x0009F1AF File Offset: 0x0009D3AF
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x0009F1B7 File Offset: 0x0009D3B7
		public bool RequiresMarkAlive
		{
			get
			{
				return this._RequiresMarkAlive;
			}
			set
			{
				this._RequiresMarkAlive = value;
				this.HasRequiresMarkAlive = true;
			}
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0009F1C7 File Offset: 0x0009D3C7
		public void SetRequiresMarkAlive(bool val)
		{
			this.RequiresMarkAlive = val;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x0009F1D0 File Offset: 0x0009D3D0
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
		public string MacAddress
		{
			get
			{
				return this._MacAddress;
			}
			set
			{
				this._MacAddress = value;
				this.HasMacAddress = (value != null);
			}
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x0009F1EB File Offset: 0x0009D3EB
		public void SetMacAddress(string val)
		{
			this.MacAddress = val;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0009F1F4 File Offset: 0x0009D3F4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasApplicationVersion)
			{
				num ^= this.ApplicationVersion.GetHashCode();
			}
			if (this.HasUserAgent)
			{
				num ^= this.UserAgent.GetHashCode();
			}
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasRequiresMarkAlive)
			{
				num ^= this.RequiresMarkAlive.GetHashCode();
			}
			if (this.HasMacAddress)
			{
				num ^= this.MacAddress.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0009F2F8 File Offset: 0x0009D4F8
		public override bool Equals(object obj)
		{
			CreateSessionRequest createSessionRequest = obj as CreateSessionRequest;
			return createSessionRequest != null && this.HasIdentity == createSessionRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(createSessionRequest.Identity)) && this.HasPlatform == createSessionRequest.HasPlatform && (!this.HasPlatform || this.Platform.Equals(createSessionRequest.Platform)) && this.HasLocale == createSessionRequest.HasLocale && (!this.HasLocale || this.Locale.Equals(createSessionRequest.Locale)) && this.HasClientAddress == createSessionRequest.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(createSessionRequest.ClientAddress)) && this.HasApplicationVersion == createSessionRequest.HasApplicationVersion && (!this.HasApplicationVersion || this.ApplicationVersion.Equals(createSessionRequest.ApplicationVersion)) && this.HasUserAgent == createSessionRequest.HasUserAgent && (!this.HasUserAgent || this.UserAgent.Equals(createSessionRequest.UserAgent)) && this.HasSessionKey == createSessionRequest.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(createSessionRequest.SessionKey)) && this.HasOptions == createSessionRequest.HasOptions && (!this.HasOptions || this.Options.Equals(createSessionRequest.Options)) && this.HasRequiresMarkAlive == createSessionRequest.HasRequiresMarkAlive && (!this.HasRequiresMarkAlive || this.RequiresMarkAlive.Equals(createSessionRequest.RequiresMarkAlive)) && this.HasMacAddress == createSessionRequest.HasMacAddress && (!this.HasMacAddress || this.MacAddress.Equals(createSessionRequest.MacAddress));
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x0009F4CC File Offset: 0x0009D6CC
		public static CreateSessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateSessionRequest>(bs, 0, -1);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x0009F4D6 File Offset: 0x0009D6D6
		public void Deserialize(Stream stream)
		{
			CreateSessionRequest.Deserialize(stream, this);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x0009F4E0 File Offset: 0x0009D6E0
		public static CreateSessionRequest Deserialize(Stream stream, CreateSessionRequest instance)
		{
			return CreateSessionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x0009F4EC File Offset: 0x0009D6EC
		public static CreateSessionRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateSessionRequest createSessionRequest = new CreateSessionRequest();
			CreateSessionRequest.DeserializeLengthDelimited(stream, createSessionRequest);
			return createSessionRequest;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x0009F508 File Offset: 0x0009D708
		public static CreateSessionRequest DeserializeLengthDelimited(Stream stream, CreateSessionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateSessionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x0009F530 File Offset: 0x0009D730
		public static CreateSessionRequest Deserialize(Stream stream, CreateSessionRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.RequiresMarkAlive = false;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 40)
					{
						if (num <= 21)
						{
							if (num != 10)
							{
								if (num == 21)
								{
									instance.Platform = binaryReader.ReadUInt32();
									continue;
								}
							}
							else
							{
								if (instance.Identity == null)
								{
									instance.Identity = Identity.DeserializeLengthDelimited(stream);
									continue;
								}
								Identity.DeserializeLengthDelimited(stream, instance.Identity);
								continue;
							}
						}
						else
						{
							if (num == 29)
							{
								instance.Locale = binaryReader.ReadUInt32();
								continue;
							}
							if (num == 34)
							{
								instance.ClientAddress = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.ApplicationVersion = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 58)
					{
						if (num == 50)
						{
							instance.UserAgent = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
						{
							instance.SessionKey = ProtocolParser.ReadBytes(stream);
							continue;
						}
					}
					else if (num != 66)
					{
						if (num == 72)
						{
							instance.RequiresMarkAlive = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 82)
						{
							instance.MacAddress = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (instance.Options == null)
						{
							instance.Options = SessionOptions.DeserializeLengthDelimited(stream);
							continue;
						}
						SessionOptions.DeserializeLengthDelimited(stream, instance.Options);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x0009F703 File Offset: 0x0009D903
		public void Serialize(Stream stream)
		{
			CreateSessionRequest.Serialize(stream, this);
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x0009F70C File Offset: 0x0009D90C
		public static void Serialize(Stream stream, CreateSessionRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ApplicationVersion));
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

		// Token: 0x06002EED RID: 12013 RVA: 0x0009F87C File Offset: 0x0009DA7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlatform)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasLocale)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasApplicationVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ApplicationVersion));
			}
			if (this.HasUserAgent)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasSessionKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionKey.Length) + (uint)this.SessionKey.Length;
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRequiresMarkAlive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasMacAddress)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.MacAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x040012E0 RID: 4832
		public bool HasIdentity;

		// Token: 0x040012E1 RID: 4833
		private Identity _Identity;

		// Token: 0x040012E2 RID: 4834
		public bool HasPlatform;

		// Token: 0x040012E3 RID: 4835
		private uint _Platform;

		// Token: 0x040012E4 RID: 4836
		public bool HasLocale;

		// Token: 0x040012E5 RID: 4837
		private uint _Locale;

		// Token: 0x040012E6 RID: 4838
		public bool HasClientAddress;

		// Token: 0x040012E7 RID: 4839
		private string _ClientAddress;

		// Token: 0x040012E8 RID: 4840
		public bool HasApplicationVersion;

		// Token: 0x040012E9 RID: 4841
		private int _ApplicationVersion;

		// Token: 0x040012EA RID: 4842
		public bool HasUserAgent;

		// Token: 0x040012EB RID: 4843
		private string _UserAgent;

		// Token: 0x040012EC RID: 4844
		public bool HasSessionKey;

		// Token: 0x040012ED RID: 4845
		private byte[] _SessionKey;

		// Token: 0x040012EE RID: 4846
		public bool HasOptions;

		// Token: 0x040012EF RID: 4847
		private SessionOptions _Options;

		// Token: 0x040012F0 RID: 4848
		public bool HasRequiresMarkAlive;

		// Token: 0x040012F1 RID: 4849
		private bool _RequiresMarkAlive;

		// Token: 0x040012F2 RID: 4850
		public bool HasMacAddress;

		// Token: 0x040012F3 RID: 4851
		private string _MacAddress;
	}
}
