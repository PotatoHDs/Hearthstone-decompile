using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountFieldTags : IProtoBuf
	{
		public bool HasAccountLevelInfoTag;

		private uint _AccountLevelInfoTag;

		public bool HasPrivacyInfoTag;

		private uint _PrivacyInfoTag;

		public bool HasParentalControlInfoTag;

		private uint _ParentalControlInfoTag;

		private List<ProgramTag> _GameLevelInfoTags = new List<ProgramTag>();

		private List<ProgramTag> _GameStatusTags = new List<ProgramTag>();

		private List<RegionTag> _GameAccountTags = new List<RegionTag>();

		public uint AccountLevelInfoTag
		{
			get
			{
				return _AccountLevelInfoTag;
			}
			set
			{
				_AccountLevelInfoTag = value;
				HasAccountLevelInfoTag = true;
			}
		}

		public uint PrivacyInfoTag
		{
			get
			{
				return _PrivacyInfoTag;
			}
			set
			{
				_PrivacyInfoTag = value;
				HasPrivacyInfoTag = true;
			}
		}

		public uint ParentalControlInfoTag
		{
			get
			{
				return _ParentalControlInfoTag;
			}
			set
			{
				_ParentalControlInfoTag = value;
				HasParentalControlInfoTag = true;
			}
		}

		public List<ProgramTag> GameLevelInfoTags
		{
			get
			{
				return _GameLevelInfoTags;
			}
			set
			{
				_GameLevelInfoTags = value;
			}
		}

		public List<ProgramTag> GameLevelInfoTagsList => _GameLevelInfoTags;

		public int GameLevelInfoTagsCount => _GameLevelInfoTags.Count;

		public List<ProgramTag> GameStatusTags
		{
			get
			{
				return _GameStatusTags;
			}
			set
			{
				_GameStatusTags = value;
			}
		}

		public List<ProgramTag> GameStatusTagsList => _GameStatusTags;

		public int GameStatusTagsCount => _GameStatusTags.Count;

		public List<RegionTag> GameAccountTags
		{
			get
			{
				return _GameAccountTags;
			}
			set
			{
				_GameAccountTags = value;
			}
		}

		public List<RegionTag> GameAccountTagsList => _GameAccountTags;

		public int GameAccountTagsCount => _GameAccountTags.Count;

		public bool IsInitialized => true;

		public void SetAccountLevelInfoTag(uint val)
		{
			AccountLevelInfoTag = val;
		}

		public void SetPrivacyInfoTag(uint val)
		{
			PrivacyInfoTag = val;
		}

		public void SetParentalControlInfoTag(uint val)
		{
			ParentalControlInfoTag = val;
		}

		public void AddGameLevelInfoTags(ProgramTag val)
		{
			_GameLevelInfoTags.Add(val);
		}

		public void ClearGameLevelInfoTags()
		{
			_GameLevelInfoTags.Clear();
		}

		public void SetGameLevelInfoTags(List<ProgramTag> val)
		{
			GameLevelInfoTags = val;
		}

		public void AddGameStatusTags(ProgramTag val)
		{
			_GameStatusTags.Add(val);
		}

		public void ClearGameStatusTags()
		{
			_GameStatusTags.Clear();
		}

		public void SetGameStatusTags(List<ProgramTag> val)
		{
			GameStatusTags = val;
		}

		public void AddGameAccountTags(RegionTag val)
		{
			_GameAccountTags.Add(val);
		}

		public void ClearGameAccountTags()
		{
			_GameAccountTags.Clear();
		}

		public void SetGameAccountTags(List<RegionTag> val)
		{
			GameAccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountLevelInfoTag)
			{
				num ^= AccountLevelInfoTag.GetHashCode();
			}
			if (HasPrivacyInfoTag)
			{
				num ^= PrivacyInfoTag.GetHashCode();
			}
			if (HasParentalControlInfoTag)
			{
				num ^= ParentalControlInfoTag.GetHashCode();
			}
			foreach (ProgramTag gameLevelInfoTag in GameLevelInfoTags)
			{
				num ^= gameLevelInfoTag.GetHashCode();
			}
			foreach (ProgramTag gameStatusTag in GameStatusTags)
			{
				num ^= gameStatusTag.GetHashCode();
			}
			foreach (RegionTag gameAccountTag in GameAccountTags)
			{
				num ^= gameAccountTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountFieldTags accountFieldTags = obj as AccountFieldTags;
			if (accountFieldTags == null)
			{
				return false;
			}
			if (HasAccountLevelInfoTag != accountFieldTags.HasAccountLevelInfoTag || (HasAccountLevelInfoTag && !AccountLevelInfoTag.Equals(accountFieldTags.AccountLevelInfoTag)))
			{
				return false;
			}
			if (HasPrivacyInfoTag != accountFieldTags.HasPrivacyInfoTag || (HasPrivacyInfoTag && !PrivacyInfoTag.Equals(accountFieldTags.PrivacyInfoTag)))
			{
				return false;
			}
			if (HasParentalControlInfoTag != accountFieldTags.HasParentalControlInfoTag || (HasParentalControlInfoTag && !ParentalControlInfoTag.Equals(accountFieldTags.ParentalControlInfoTag)))
			{
				return false;
			}
			if (GameLevelInfoTags.Count != accountFieldTags.GameLevelInfoTags.Count)
			{
				return false;
			}
			for (int i = 0; i < GameLevelInfoTags.Count; i++)
			{
				if (!GameLevelInfoTags[i].Equals(accountFieldTags.GameLevelInfoTags[i]))
				{
					return false;
				}
			}
			if (GameStatusTags.Count != accountFieldTags.GameStatusTags.Count)
			{
				return false;
			}
			for (int j = 0; j < GameStatusTags.Count; j++)
			{
				if (!GameStatusTags[j].Equals(accountFieldTags.GameStatusTags[j]))
				{
					return false;
				}
			}
			if (GameAccountTags.Count != accountFieldTags.GameAccountTags.Count)
			{
				return false;
			}
			for (int k = 0; k < GameAccountTags.Count; k++)
			{
				if (!GameAccountTags[k].Equals(accountFieldTags.GameAccountTags[k]))
				{
					return false;
				}
			}
			return true;
		}

		public static AccountFieldTags ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountFieldTags>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountFieldTags Deserialize(Stream stream, AccountFieldTags instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountFieldTags DeserializeLengthDelimited(Stream stream)
		{
			AccountFieldTags accountFieldTags = new AccountFieldTags();
			DeserializeLengthDelimited(stream, accountFieldTags);
			return accountFieldTags;
		}

		public static AccountFieldTags DeserializeLengthDelimited(Stream stream, AccountFieldTags instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountFieldTags Deserialize(Stream stream, AccountFieldTags instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.GameLevelInfoTags == null)
			{
				instance.GameLevelInfoTags = new List<ProgramTag>();
			}
			if (instance.GameStatusTags == null)
			{
				instance.GameStatusTags = new List<ProgramTag>();
			}
			if (instance.GameAccountTags == null)
			{
				instance.GameAccountTags = new List<RegionTag>();
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
				case 21:
					instance.AccountLevelInfoTag = binaryReader.ReadUInt32();
					continue;
				case 29:
					instance.PrivacyInfoTag = binaryReader.ReadUInt32();
					continue;
				case 37:
					instance.ParentalControlInfoTag = binaryReader.ReadUInt32();
					continue;
				case 58:
					instance.GameLevelInfoTags.Add(ProgramTag.DeserializeLengthDelimited(stream));
					continue;
				case 74:
					instance.GameStatusTags.Add(ProgramTag.DeserializeLengthDelimited(stream));
					continue;
				case 90:
					instance.GameAccountTags.Add(RegionTag.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AccountFieldTags instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountLevelInfoTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.AccountLevelInfoTag);
			}
			if (instance.HasPrivacyInfoTag)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.PrivacyInfoTag);
			}
			if (instance.HasParentalControlInfoTag)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ParentalControlInfoTag);
			}
			if (instance.GameLevelInfoTags.Count > 0)
			{
				foreach (ProgramTag gameLevelInfoTag in instance.GameLevelInfoTags)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, gameLevelInfoTag.GetSerializedSize());
					ProgramTag.Serialize(stream, gameLevelInfoTag);
				}
			}
			if (instance.GameStatusTags.Count > 0)
			{
				foreach (ProgramTag gameStatusTag in instance.GameStatusTags)
				{
					stream.WriteByte(74);
					ProtocolParser.WriteUInt32(stream, gameStatusTag.GetSerializedSize());
					ProgramTag.Serialize(stream, gameStatusTag);
				}
			}
			if (instance.GameAccountTags.Count <= 0)
			{
				return;
			}
			foreach (RegionTag gameAccountTag in instance.GameAccountTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, gameAccountTag.GetSerializedSize());
				RegionTag.Serialize(stream, gameAccountTag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountLevelInfoTag)
			{
				num++;
				num += 4;
			}
			if (HasPrivacyInfoTag)
			{
				num++;
				num += 4;
			}
			if (HasParentalControlInfoTag)
			{
				num++;
				num += 4;
			}
			if (GameLevelInfoTags.Count > 0)
			{
				foreach (ProgramTag gameLevelInfoTag in GameLevelInfoTags)
				{
					num++;
					uint serializedSize = gameLevelInfoTag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (GameStatusTags.Count > 0)
			{
				foreach (ProgramTag gameStatusTag in GameStatusTags)
				{
					num++;
					uint serializedSize2 = gameStatusTag.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (GameAccountTags.Count > 0)
			{
				foreach (RegionTag gameAccountTag in GameAccountTags)
				{
					num++;
					uint serializedSize3 = gameAccountTag.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
