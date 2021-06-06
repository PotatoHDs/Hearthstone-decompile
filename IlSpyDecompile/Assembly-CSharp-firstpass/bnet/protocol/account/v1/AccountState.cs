using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountState : IProtoBuf
	{
		public bool HasAccountLevelInfo;

		private AccountLevelInfo _AccountLevelInfo;

		public bool HasPrivacyInfo;

		private PrivacyInfo _PrivacyInfo;

		public bool HasParentalControlInfo;

		private ParentalControlInfo _ParentalControlInfo;

		private List<GameLevelInfo> _GameLevelInfo = new List<GameLevelInfo>();

		private List<GameStatus> _GameStatus = new List<GameStatus>();

		private List<GameAccountList> _GameAccounts = new List<GameAccountList>();

		public AccountLevelInfo AccountLevelInfo
		{
			get
			{
				return _AccountLevelInfo;
			}
			set
			{
				_AccountLevelInfo = value;
				HasAccountLevelInfo = value != null;
			}
		}

		public PrivacyInfo PrivacyInfo
		{
			get
			{
				return _PrivacyInfo;
			}
			set
			{
				_PrivacyInfo = value;
				HasPrivacyInfo = value != null;
			}
		}

		public ParentalControlInfo ParentalControlInfo
		{
			get
			{
				return _ParentalControlInfo;
			}
			set
			{
				_ParentalControlInfo = value;
				HasParentalControlInfo = value != null;
			}
		}

		public List<GameLevelInfo> GameLevelInfo
		{
			get
			{
				return _GameLevelInfo;
			}
			set
			{
				_GameLevelInfo = value;
			}
		}

		public List<GameLevelInfo> GameLevelInfoList => _GameLevelInfo;

		public int GameLevelInfoCount => _GameLevelInfo.Count;

		public List<GameStatus> GameStatus
		{
			get
			{
				return _GameStatus;
			}
			set
			{
				_GameStatus = value;
			}
		}

		public List<GameStatus> GameStatusList => _GameStatus;

		public int GameStatusCount => _GameStatus.Count;

		public List<GameAccountList> GameAccounts
		{
			get
			{
				return _GameAccounts;
			}
			set
			{
				_GameAccounts = value;
			}
		}

		public List<GameAccountList> GameAccountsList => _GameAccounts;

		public int GameAccountsCount => _GameAccounts.Count;

		public bool IsInitialized => true;

		public void SetAccountLevelInfo(AccountLevelInfo val)
		{
			AccountLevelInfo = val;
		}

		public void SetPrivacyInfo(PrivacyInfo val)
		{
			PrivacyInfo = val;
		}

		public void SetParentalControlInfo(ParentalControlInfo val)
		{
			ParentalControlInfo = val;
		}

		public void AddGameLevelInfo(GameLevelInfo val)
		{
			_GameLevelInfo.Add(val);
		}

		public void ClearGameLevelInfo()
		{
			_GameLevelInfo.Clear();
		}

		public void SetGameLevelInfo(List<GameLevelInfo> val)
		{
			GameLevelInfo = val;
		}

		public void AddGameStatus(GameStatus val)
		{
			_GameStatus.Add(val);
		}

		public void ClearGameStatus()
		{
			_GameStatus.Clear();
		}

		public void SetGameStatus(List<GameStatus> val)
		{
			GameStatus = val;
		}

		public void AddGameAccounts(GameAccountList val)
		{
			_GameAccounts.Add(val);
		}

		public void ClearGameAccounts()
		{
			_GameAccounts.Clear();
		}

		public void SetGameAccounts(List<GameAccountList> val)
		{
			GameAccounts = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountLevelInfo)
			{
				num ^= AccountLevelInfo.GetHashCode();
			}
			if (HasPrivacyInfo)
			{
				num ^= PrivacyInfo.GetHashCode();
			}
			if (HasParentalControlInfo)
			{
				num ^= ParentalControlInfo.GetHashCode();
			}
			foreach (GameLevelInfo item in GameLevelInfo)
			{
				num ^= item.GetHashCode();
			}
			foreach (GameStatus item2 in GameStatus)
			{
				num ^= item2.GetHashCode();
			}
			foreach (GameAccountList gameAccount in GameAccounts)
			{
				num ^= gameAccount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountState accountState = obj as AccountState;
			if (accountState == null)
			{
				return false;
			}
			if (HasAccountLevelInfo != accountState.HasAccountLevelInfo || (HasAccountLevelInfo && !AccountLevelInfo.Equals(accountState.AccountLevelInfo)))
			{
				return false;
			}
			if (HasPrivacyInfo != accountState.HasPrivacyInfo || (HasPrivacyInfo && !PrivacyInfo.Equals(accountState.PrivacyInfo)))
			{
				return false;
			}
			if (HasParentalControlInfo != accountState.HasParentalControlInfo || (HasParentalControlInfo && !ParentalControlInfo.Equals(accountState.ParentalControlInfo)))
			{
				return false;
			}
			if (GameLevelInfo.Count != accountState.GameLevelInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < GameLevelInfo.Count; i++)
			{
				if (!GameLevelInfo[i].Equals(accountState.GameLevelInfo[i]))
				{
					return false;
				}
			}
			if (GameStatus.Count != accountState.GameStatus.Count)
			{
				return false;
			}
			for (int j = 0; j < GameStatus.Count; j++)
			{
				if (!GameStatus[j].Equals(accountState.GameStatus[j]))
				{
					return false;
				}
			}
			if (GameAccounts.Count != accountState.GameAccounts.Count)
			{
				return false;
			}
			for (int k = 0; k < GameAccounts.Count; k++)
			{
				if (!GameAccounts[k].Equals(accountState.GameAccounts[k]))
				{
					return false;
				}
			}
			return true;
		}

		public static AccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountState Deserialize(Stream stream, AccountState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountState DeserializeLengthDelimited(Stream stream)
		{
			AccountState accountState = new AccountState();
			DeserializeLengthDelimited(stream, accountState);
			return accountState;
		}

		public static AccountState DeserializeLengthDelimited(Stream stream, AccountState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountState Deserialize(Stream stream, AccountState instance, long limit)
		{
			if (instance.GameLevelInfo == null)
			{
				instance.GameLevelInfo = new List<GameLevelInfo>();
			}
			if (instance.GameStatus == null)
			{
				instance.GameStatus = new List<GameStatus>();
			}
			if (instance.GameAccounts == null)
			{
				instance.GameAccounts = new List<GameAccountList>();
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
				case 10:
					if (instance.AccountLevelInfo == null)
					{
						instance.AccountLevelInfo = AccountLevelInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountLevelInfo.DeserializeLengthDelimited(stream, instance.AccountLevelInfo);
					}
					continue;
				case 18:
					if (instance.PrivacyInfo == null)
					{
						instance.PrivacyInfo = PrivacyInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						PrivacyInfo.DeserializeLengthDelimited(stream, instance.PrivacyInfo);
					}
					continue;
				case 26:
					if (instance.ParentalControlInfo == null)
					{
						instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
					}
					continue;
				case 42:
					instance.GameLevelInfo.Add(bnet.protocol.account.v1.GameLevelInfo.DeserializeLengthDelimited(stream));
					continue;
				case 50:
					instance.GameStatus.Add(bnet.protocol.account.v1.GameStatus.DeserializeLengthDelimited(stream));
					continue;
				case 58:
					instance.GameAccounts.Add(GameAccountList.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AccountState instance)
		{
			if (instance.HasAccountLevelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountLevelInfo.GetSerializedSize());
				AccountLevelInfo.Serialize(stream, instance.AccountLevelInfo);
			}
			if (instance.HasPrivacyInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PrivacyInfo.GetSerializedSize());
				PrivacyInfo.Serialize(stream, instance.PrivacyInfo);
			}
			if (instance.HasParentalControlInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ParentalControlInfo.GetSerializedSize());
				ParentalControlInfo.Serialize(stream, instance.ParentalControlInfo);
			}
			if (instance.GameLevelInfo.Count > 0)
			{
				foreach (GameLevelInfo item in instance.GameLevelInfo)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.account.v1.GameLevelInfo.Serialize(stream, item);
				}
			}
			if (instance.GameStatus.Count > 0)
			{
				foreach (GameStatus item2 in instance.GameStatus)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.account.v1.GameStatus.Serialize(stream, item2);
				}
			}
			if (instance.GameAccounts.Count <= 0)
			{
				return;
			}
			foreach (GameAccountList gameAccount in instance.GameAccounts)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, gameAccount.GetSerializedSize());
				GameAccountList.Serialize(stream, gameAccount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountLevelInfo)
			{
				num++;
				uint serializedSize = AccountLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPrivacyInfo)
			{
				num++;
				uint serializedSize2 = PrivacyInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasParentalControlInfo)
			{
				num++;
				uint serializedSize3 = ParentalControlInfo.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (GameLevelInfo.Count > 0)
			{
				foreach (GameLevelInfo item in GameLevelInfo)
				{
					num++;
					uint serializedSize4 = item.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (GameStatus.Count > 0)
			{
				foreach (GameStatus item2 in GameStatus)
				{
					num++;
					uint serializedSize5 = item2.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccount in GameAccounts)
				{
					num++;
					uint serializedSize6 = gameAccount.GetSerializedSize();
					num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
				}
				return num;
			}
			return num;
		}
	}
}
