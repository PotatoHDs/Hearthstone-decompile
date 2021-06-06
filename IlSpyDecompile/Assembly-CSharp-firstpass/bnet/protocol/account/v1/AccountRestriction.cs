using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1.Types;

namespace bnet.protocol.account.v1
{
	public class AccountRestriction : IProtoBuf
	{
		public bool HasRestrictionId;

		private uint _RestrictionId;

		public bool HasProgram;

		private uint _Program;

		public bool HasType;

		private RestrictionType _Type;

		private List<uint> _Platform = new List<uint>();

		public bool HasExpireTime;

		private ulong _ExpireTime;

		public bool HasCreatedTime;

		private ulong _CreatedTime;

		public uint RestrictionId
		{
			get
			{
				return _RestrictionId;
			}
			set
			{
				_RestrictionId = value;
				HasRestrictionId = true;
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

		public RestrictionType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = true;
			}
		}

		public List<uint> Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
			}
		}

		public List<uint> PlatformList => _Platform;

		public int PlatformCount => _Platform.Count;

		public ulong ExpireTime
		{
			get
			{
				return _ExpireTime;
			}
			set
			{
				_ExpireTime = value;
				HasExpireTime = true;
			}
		}

		public ulong CreatedTime
		{
			get
			{
				return _CreatedTime;
			}
			set
			{
				_CreatedTime = value;
				HasCreatedTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRestrictionId(uint val)
		{
			RestrictionId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetType(RestrictionType val)
		{
			Type = val;
		}

		public void AddPlatform(uint val)
		{
			_Platform.Add(val);
		}

		public void ClearPlatform()
		{
			_Platform.Clear();
		}

		public void SetPlatform(List<uint> val)
		{
			Platform = val;
		}

		public void SetExpireTime(ulong val)
		{
			ExpireTime = val;
		}

		public void SetCreatedTime(ulong val)
		{
			CreatedTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRestrictionId)
			{
				num ^= RestrictionId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			foreach (uint item in Platform)
			{
				num ^= item.GetHashCode();
			}
			if (HasExpireTime)
			{
				num ^= ExpireTime.GetHashCode();
			}
			if (HasCreatedTime)
			{
				num ^= CreatedTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountRestriction accountRestriction = obj as AccountRestriction;
			if (accountRestriction == null)
			{
				return false;
			}
			if (HasRestrictionId != accountRestriction.HasRestrictionId || (HasRestrictionId && !RestrictionId.Equals(accountRestriction.RestrictionId)))
			{
				return false;
			}
			if (HasProgram != accountRestriction.HasProgram || (HasProgram && !Program.Equals(accountRestriction.Program)))
			{
				return false;
			}
			if (HasType != accountRestriction.HasType || (HasType && !Type.Equals(accountRestriction.Type)))
			{
				return false;
			}
			if (Platform.Count != accountRestriction.Platform.Count)
			{
				return false;
			}
			for (int i = 0; i < Platform.Count; i++)
			{
				if (!Platform[i].Equals(accountRestriction.Platform[i]))
				{
					return false;
				}
			}
			if (HasExpireTime != accountRestriction.HasExpireTime || (HasExpireTime && !ExpireTime.Equals(accountRestriction.ExpireTime)))
			{
				return false;
			}
			if (HasCreatedTime != accountRestriction.HasCreatedTime || (HasCreatedTime && !CreatedTime.Equals(accountRestriction.CreatedTime)))
			{
				return false;
			}
			return true;
		}

		public static AccountRestriction ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountRestriction>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountRestriction Deserialize(Stream stream, AccountRestriction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountRestriction DeserializeLengthDelimited(Stream stream)
		{
			AccountRestriction accountRestriction = new AccountRestriction();
			DeserializeLengthDelimited(stream, accountRestriction);
			return accountRestriction;
		}

		public static AccountRestriction DeserializeLengthDelimited(Stream stream, AccountRestriction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountRestriction Deserialize(Stream stream, AccountRestriction instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Type = RestrictionType.UNKNOWN;
			if (instance.Platform == null)
			{
				instance.Platform = new List<uint>();
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
				case 8:
					instance.RestrictionId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 24:
					instance.Type = (RestrictionType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 37:
					instance.Platform.Add(binaryReader.ReadUInt32());
					continue;
				case 40:
					instance.ExpireTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CreatedTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AccountRestriction instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRestrictionId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.RestrictionId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			}
			if (instance.Platform.Count > 0)
			{
				foreach (uint item in instance.Platform)
				{
					stream.WriteByte(37);
					binaryWriter.Write(item);
				}
			}
			if (instance.HasExpireTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ExpireTime);
			}
			if (instance.HasCreatedTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreatedTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRestrictionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RestrictionId);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Type);
			}
			if (Platform.Count > 0)
			{
				foreach (uint item in Platform)
				{
					_ = item;
					num++;
					num += 4;
				}
			}
			if (HasExpireTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpireTime);
			}
			if (HasCreatedTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreatedTime);
			}
			return num;
		}
	}
}
