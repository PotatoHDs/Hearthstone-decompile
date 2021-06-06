using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class GameLevelInfo : IProtoBuf
	{
		public bool HasIsTrial;

		private bool _IsTrial;

		public bool HasIsLifetime;

		private bool _IsLifetime;

		public bool HasIsRestricted;

		private bool _IsRestricted;

		public bool HasIsBeta;

		private bool _IsBeta;

		public bool HasName;

		private string _Name;

		public bool HasProgram;

		private uint _Program;

		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasRealmPermissions;

		private uint _RealmPermissions;

		public bool IsTrial
		{
			get
			{
				return _IsTrial;
			}
			set
			{
				_IsTrial = value;
				HasIsTrial = true;
			}
		}

		public bool IsLifetime
		{
			get
			{
				return _IsLifetime;
			}
			set
			{
				_IsLifetime = value;
				HasIsLifetime = true;
			}
		}

		public bool IsRestricted
		{
			get
			{
				return _IsRestricted;
			}
			set
			{
				_IsRestricted = value;
				HasIsRestricted = true;
			}
		}

		public bool IsBeta
		{
			get
			{
				return _IsBeta;
			}
			set
			{
				_IsBeta = value;
				HasIsBeta = true;
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public List<AccountLicense> Licenses
		{
			get
			{
				return _Licenses;
			}
			set
			{
				_Licenses = value;
			}
		}

		public List<AccountLicense> LicensesList => _Licenses;

		public int LicensesCount => _Licenses.Count;

		public uint RealmPermissions
		{
			get
			{
				return _RealmPermissions;
			}
			set
			{
				_RealmPermissions = value;
				HasRealmPermissions = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIsTrial(bool val)
		{
			IsTrial = val;
		}

		public void SetIsLifetime(bool val)
		{
			IsLifetime = val;
		}

		public void SetIsRestricted(bool val)
		{
			IsRestricted = val;
		}

		public void SetIsBeta(bool val)
		{
			IsBeta = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void AddLicenses(AccountLicense val)
		{
			_Licenses.Add(val);
		}

		public void ClearLicenses()
		{
			_Licenses.Clear();
		}

		public void SetLicenses(List<AccountLicense> val)
		{
			Licenses = val;
		}

		public void SetRealmPermissions(uint val)
		{
			RealmPermissions = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIsTrial)
			{
				num ^= IsTrial.GetHashCode();
			}
			if (HasIsLifetime)
			{
				num ^= IsLifetime.GetHashCode();
			}
			if (HasIsRestricted)
			{
				num ^= IsRestricted.GetHashCode();
			}
			if (HasIsBeta)
			{
				num ^= IsBeta.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			foreach (AccountLicense license in Licenses)
			{
				num ^= license.GetHashCode();
			}
			if (HasRealmPermissions)
			{
				num ^= RealmPermissions.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameLevelInfo gameLevelInfo = obj as GameLevelInfo;
			if (gameLevelInfo == null)
			{
				return false;
			}
			if (HasIsTrial != gameLevelInfo.HasIsTrial || (HasIsTrial && !IsTrial.Equals(gameLevelInfo.IsTrial)))
			{
				return false;
			}
			if (HasIsLifetime != gameLevelInfo.HasIsLifetime || (HasIsLifetime && !IsLifetime.Equals(gameLevelInfo.IsLifetime)))
			{
				return false;
			}
			if (HasIsRestricted != gameLevelInfo.HasIsRestricted || (HasIsRestricted && !IsRestricted.Equals(gameLevelInfo.IsRestricted)))
			{
				return false;
			}
			if (HasIsBeta != gameLevelInfo.HasIsBeta || (HasIsBeta && !IsBeta.Equals(gameLevelInfo.IsBeta)))
			{
				return false;
			}
			if (HasName != gameLevelInfo.HasName || (HasName && !Name.Equals(gameLevelInfo.Name)))
			{
				return false;
			}
			if (HasProgram != gameLevelInfo.HasProgram || (HasProgram && !Program.Equals(gameLevelInfo.Program)))
			{
				return false;
			}
			if (Licenses.Count != gameLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < Licenses.Count; i++)
			{
				if (!Licenses[i].Equals(gameLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			if (HasRealmPermissions != gameLevelInfo.HasRealmPermissions || (HasRealmPermissions && !RealmPermissions.Equals(gameLevelInfo.RealmPermissions)))
			{
				return false;
			}
			return true;
		}

		public static GameLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameLevelInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			GameLevelInfo gameLevelInfo = new GameLevelInfo();
			DeserializeLengthDelimited(stream, gameLevelInfo);
			return gameLevelInfo;
		}

		public static GameLevelInfo DeserializeLengthDelimited(Stream stream, GameLevelInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
				case 32:
					instance.IsTrial = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.IsLifetime = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.IsRestricted = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.IsBeta = ProtocolParser.ReadBool(stream);
					continue;
				case 66:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 77:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 82:
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
					continue;
				case 88:
					instance.RealmPermissions = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsTrial)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsTrial);
			}
			if (instance.HasIsLifetime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsLifetime);
			}
			if (instance.HasIsRestricted)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsRestricted);
			}
			if (instance.HasIsBeta)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsBeta);
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense license in instance.Licenses)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, license.GetSerializedSize());
					AccountLicense.Serialize(stream, license);
				}
			}
			if (instance.HasRealmPermissions)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.RealmPermissions);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIsTrial)
			{
				num++;
				num++;
			}
			if (HasIsLifetime)
			{
				num++;
				num++;
			}
			if (HasIsRestricted)
			{
				num++;
				num++;
			}
			if (HasIsBeta)
			{
				num++;
				num++;
			}
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (Licenses.Count > 0)
			{
				foreach (AccountLicense license in Licenses)
				{
					num++;
					uint serializedSize = license.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasRealmPermissions)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RealmPermissions);
			}
			return num;
		}
	}
}
