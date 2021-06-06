using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004EC RID: 1260
	public class LogonRequest : IProtoBuf
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x00110F08 File Offset: 0x0010F108
		// (set) Token: 0x06005936 RID: 22838 RVA: 0x00110F10 File Offset: 0x0010F110
		public string Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = (value != null);
			}
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00110F23 File Offset: 0x0010F123
		public void SetProgram(string val)
		{
			this.Program = val;
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06005938 RID: 22840 RVA: 0x00110F2C File Offset: 0x0010F12C
		// (set) Token: 0x06005939 RID: 22841 RVA: 0x00110F34 File Offset: 0x0010F134
		public string Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x00110F47 File Offset: 0x0010F147
		public void SetPlatform(string val)
		{
			this.Platform = val;
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x00110F50 File Offset: 0x0010F150
		// (set) Token: 0x0600593C RID: 22844 RVA: 0x00110F58 File Offset: 0x0010F158
		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00110F6B File Offset: 0x0010F16B
		public void SetLocale(string val)
		{
			this.Locale = val;
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x00110F74 File Offset: 0x0010F174
		// (set) Token: 0x0600593F RID: 22847 RVA: 0x00110F7C File Offset: 0x0010F17C
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x00110F8F File Offset: 0x0010F18F
		public void SetEmail(string val)
		{
			this.Email = val;
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06005941 RID: 22849 RVA: 0x00110F98 File Offset: 0x0010F198
		// (set) Token: 0x06005942 RID: 22850 RVA: 0x00110FA0 File Offset: 0x0010F1A0
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = (value != null);
			}
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x00110FB3 File Offset: 0x0010F1B3
		public void SetVersion(string val)
		{
			this.Version = val;
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x00110FBC File Offset: 0x0010F1BC
		// (set) Token: 0x06005945 RID: 22853 RVA: 0x00110FC4 File Offset: 0x0010F1C4
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

		// Token: 0x06005946 RID: 22854 RVA: 0x00110FD4 File Offset: 0x0010F1D4
		public void SetApplicationVersion(int val)
		{
			this.ApplicationVersion = val;
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x00110FDD File Offset: 0x0010F1DD
		// (set) Token: 0x06005948 RID: 22856 RVA: 0x00110FE5 File Offset: 0x0010F1E5
		public bool PublicComputer
		{
			get
			{
				return this._PublicComputer;
			}
			set
			{
				this._PublicComputer = value;
				this.HasPublicComputer = true;
			}
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00110FF5 File Offset: 0x0010F1F5
		public void SetPublicComputer(bool val)
		{
			this.PublicComputer = val;
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x00110FFE File Offset: 0x0010F1FE
		// (set) Token: 0x0600594B RID: 22859 RVA: 0x00111006 File Offset: 0x0010F206
		public bool AllowLogonQueueNotifications
		{
			get
			{
				return this._AllowLogonQueueNotifications;
			}
			set
			{
				this._AllowLogonQueueNotifications = value;
				this.HasAllowLogonQueueNotifications = true;
			}
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00111016 File Offset: 0x0010F216
		public void SetAllowLogonQueueNotifications(bool val)
		{
			this.AllowLogonQueueNotifications = val;
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x0011101F File Offset: 0x0010F21F
		// (set) Token: 0x0600594E RID: 22862 RVA: 0x00111027 File Offset: 0x0010F227
		public byte[] CachedWebCredentials
		{
			get
			{
				return this._CachedWebCredentials;
			}
			set
			{
				this._CachedWebCredentials = value;
				this.HasCachedWebCredentials = (value != null);
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0011103A File Offset: 0x0010F23A
		public void SetCachedWebCredentials(byte[] val)
		{
			this.CachedWebCredentials = val;
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005950 RID: 22864 RVA: 0x00111043 File Offset: 0x0010F243
		// (set) Token: 0x06005951 RID: 22865 RVA: 0x0011104B File Offset: 0x0010F24B
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

		// Token: 0x06005952 RID: 22866 RVA: 0x0011105E File Offset: 0x0010F25E
		public void SetUserAgent(string val)
		{
			this.UserAgent = val;
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x00111067 File Offset: 0x0010F267
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x0011106F File Offset: 0x0010F26F
		public string DeviceId
		{
			get
			{
				return this._DeviceId;
			}
			set
			{
				this._DeviceId = value;
				this.HasDeviceId = (value != null);
			}
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x00111082 File Offset: 0x0010F282
		public void SetDeviceId(string val)
		{
			this.DeviceId = val;
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0011108C File Offset: 0x0010F28C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			if (this.HasApplicationVersion)
			{
				num ^= this.ApplicationVersion.GetHashCode();
			}
			if (this.HasPublicComputer)
			{
				num ^= this.PublicComputer.GetHashCode();
			}
			if (this.HasAllowLogonQueueNotifications)
			{
				num ^= this.AllowLogonQueueNotifications.GetHashCode();
			}
			if (this.HasCachedWebCredentials)
			{
				num ^= this.CachedWebCredentials.GetHashCode();
			}
			if (this.HasUserAgent)
			{
				num ^= this.UserAgent.GetHashCode();
			}
			if (this.HasDeviceId)
			{
				num ^= this.DeviceId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x001111A4 File Offset: 0x0010F3A4
		public override bool Equals(object obj)
		{
			LogonRequest logonRequest = obj as LogonRequest;
			return logonRequest != null && this.HasProgram == logonRequest.HasProgram && (!this.HasProgram || this.Program.Equals(logonRequest.Program)) && this.HasPlatform == logonRequest.HasPlatform && (!this.HasPlatform || this.Platform.Equals(logonRequest.Platform)) && this.HasLocale == logonRequest.HasLocale && (!this.HasLocale || this.Locale.Equals(logonRequest.Locale)) && this.HasEmail == logonRequest.HasEmail && (!this.HasEmail || this.Email.Equals(logonRequest.Email)) && this.HasVersion == logonRequest.HasVersion && (!this.HasVersion || this.Version.Equals(logonRequest.Version)) && this.HasApplicationVersion == logonRequest.HasApplicationVersion && (!this.HasApplicationVersion || this.ApplicationVersion.Equals(logonRequest.ApplicationVersion)) && this.HasPublicComputer == logonRequest.HasPublicComputer && (!this.HasPublicComputer || this.PublicComputer.Equals(logonRequest.PublicComputer)) && this.HasAllowLogonQueueNotifications == logonRequest.HasAllowLogonQueueNotifications && (!this.HasAllowLogonQueueNotifications || this.AllowLogonQueueNotifications.Equals(logonRequest.AllowLogonQueueNotifications)) && this.HasCachedWebCredentials == logonRequest.HasCachedWebCredentials && (!this.HasCachedWebCredentials || this.CachedWebCredentials.Equals(logonRequest.CachedWebCredentials)) && this.HasUserAgent == logonRequest.HasUserAgent && (!this.HasUserAgent || this.UserAgent.Equals(logonRequest.UserAgent)) && this.HasDeviceId == logonRequest.HasDeviceId && (!this.HasDeviceId || this.DeviceId.Equals(logonRequest.DeviceId));
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x001113A0 File Offset: 0x0010F5A0
		public static LogonRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonRequest>(bs, 0, -1);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x001113AA File Offset: 0x0010F5AA
		public void Deserialize(Stream stream)
		{
			LogonRequest.Deserialize(stream, this);
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x001113B4 File Offset: 0x0010F5B4
		public static LogonRequest Deserialize(Stream stream, LogonRequest instance)
		{
			return LogonRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001113C0 File Offset: 0x0010F5C0
		public static LogonRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonRequest logonRequest = new LogonRequest();
			LogonRequest.DeserializeLengthDelimited(stream, logonRequest);
			return logonRequest;
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x001113DC File Offset: 0x0010F5DC
		public static LogonRequest DeserializeLengthDelimited(Stream stream, LogonRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00111404 File Offset: 0x0010F604
		public static LogonRequest Deserialize(Stream stream, LogonRequest instance, long limit)
		{
			instance.AllowLogonQueueNotifications = false;
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
					if (num <= 42)
					{
						if (num <= 18)
						{
							if (num == 10)
							{
								instance.Program = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 18)
							{
								instance.Platform = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Locale = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 34)
							{
								instance.Email = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 42)
							{
								instance.Version = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 80)
					{
						if (num == 48)
						{
							instance.ApplicationVersion = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.PublicComputer = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.AllowLogonQueueNotifications = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 98)
						{
							instance.CachedWebCredentials = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 114)
						{
							instance.UserAgent = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 122)
						{
							instance.DeviceId = ProtocolParser.ReadString(stream);
							continue;
						}
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

		// Token: 0x0600595F RID: 22879 RVA: 0x001115AC File Offset: 0x0010F7AC
		public void Serialize(Stream stream)
		{
			LogonRequest.Serialize(stream, this);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x001115B8 File Offset: 0x0010F7B8
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ApplicationVersion));
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

		// Token: 0x06005961 RID: 22881 RVA: 0x00111740 File Offset: 0x0010F940
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProgram)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Program);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPlatform)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Platform);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasLocale)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasEmail)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasVersion)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Version);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasApplicationVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ApplicationVersion));
			}
			if (this.HasPublicComputer)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasAllowLogonQueueNotifications)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCachedWebCredentials)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CachedWebCredentials.Length) + (uint)this.CachedWebCredentials.Length;
			}
			if (this.HasUserAgent)
			{
				num += 1U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasDeviceId)
			{
				num += 1U;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			return num;
		}

		// Token: 0x04001BD7 RID: 7127
		public bool HasProgram;

		// Token: 0x04001BD8 RID: 7128
		private string _Program;

		// Token: 0x04001BD9 RID: 7129
		public bool HasPlatform;

		// Token: 0x04001BDA RID: 7130
		private string _Platform;

		// Token: 0x04001BDB RID: 7131
		public bool HasLocale;

		// Token: 0x04001BDC RID: 7132
		private string _Locale;

		// Token: 0x04001BDD RID: 7133
		public bool HasEmail;

		// Token: 0x04001BDE RID: 7134
		private string _Email;

		// Token: 0x04001BDF RID: 7135
		public bool HasVersion;

		// Token: 0x04001BE0 RID: 7136
		private string _Version;

		// Token: 0x04001BE1 RID: 7137
		public bool HasApplicationVersion;

		// Token: 0x04001BE2 RID: 7138
		private int _ApplicationVersion;

		// Token: 0x04001BE3 RID: 7139
		public bool HasPublicComputer;

		// Token: 0x04001BE4 RID: 7140
		private bool _PublicComputer;

		// Token: 0x04001BE5 RID: 7141
		public bool HasAllowLogonQueueNotifications;

		// Token: 0x04001BE6 RID: 7142
		private bool _AllowLogonQueueNotifications;

		// Token: 0x04001BE7 RID: 7143
		public bool HasCachedWebCredentials;

		// Token: 0x04001BE8 RID: 7144
		private byte[] _CachedWebCredentials;

		// Token: 0x04001BE9 RID: 7145
		public bool HasUserAgent;

		// Token: 0x04001BEA RID: 7146
		private string _UserAgent;

		// Token: 0x04001BEB RID: 7147
		public bool HasDeviceId;

		// Token: 0x04001BEC RID: 7148
		private string _DeviceId;
	}
}
