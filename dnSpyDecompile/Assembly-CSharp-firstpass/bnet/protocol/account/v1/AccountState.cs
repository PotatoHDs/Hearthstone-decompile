using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053A RID: 1338
	public class AccountState : IProtoBuf
	{
		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x00123E7F File Offset: 0x0012207F
		// (set) Token: 0x06006073 RID: 24691 RVA: 0x00123E87 File Offset: 0x00122087
		public AccountLevelInfo AccountLevelInfo
		{
			get
			{
				return this._AccountLevelInfo;
			}
			set
			{
				this._AccountLevelInfo = value;
				this.HasAccountLevelInfo = (value != null);
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x00123E9A File Offset: 0x0012209A
		public void SetAccountLevelInfo(AccountLevelInfo val)
		{
			this.AccountLevelInfo = val;
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x00123EA3 File Offset: 0x001220A3
		// (set) Token: 0x06006076 RID: 24694 RVA: 0x00123EAB File Offset: 0x001220AB
		public PrivacyInfo PrivacyInfo
		{
			get
			{
				return this._PrivacyInfo;
			}
			set
			{
				this._PrivacyInfo = value;
				this.HasPrivacyInfo = (value != null);
			}
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x00123EBE File Offset: 0x001220BE
		public void SetPrivacyInfo(PrivacyInfo val)
		{
			this.PrivacyInfo = val;
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x00123EC7 File Offset: 0x001220C7
		// (set) Token: 0x06006079 RID: 24697 RVA: 0x00123ECF File Offset: 0x001220CF
		public ParentalControlInfo ParentalControlInfo
		{
			get
			{
				return this._ParentalControlInfo;
			}
			set
			{
				this._ParentalControlInfo = value;
				this.HasParentalControlInfo = (value != null);
			}
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x00123EE2 File Offset: 0x001220E2
		public void SetParentalControlInfo(ParentalControlInfo val)
		{
			this.ParentalControlInfo = val;
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x00123EEB File Offset: 0x001220EB
		// (set) Token: 0x0600607C RID: 24700 RVA: 0x00123EF3 File Offset: 0x001220F3
		public List<GameLevelInfo> GameLevelInfo
		{
			get
			{
				return this._GameLevelInfo;
			}
			set
			{
				this._GameLevelInfo = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x0600607D RID: 24701 RVA: 0x00123EEB File Offset: 0x001220EB
		public List<GameLevelInfo> GameLevelInfoList
		{
			get
			{
				return this._GameLevelInfo;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x0600607E RID: 24702 RVA: 0x00123EFC File Offset: 0x001220FC
		public int GameLevelInfoCount
		{
			get
			{
				return this._GameLevelInfo.Count;
			}
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x00123F09 File Offset: 0x00122109
		public void AddGameLevelInfo(GameLevelInfo val)
		{
			this._GameLevelInfo.Add(val);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x00123F17 File Offset: 0x00122117
		public void ClearGameLevelInfo()
		{
			this._GameLevelInfo.Clear();
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x00123F24 File Offset: 0x00122124
		public void SetGameLevelInfo(List<GameLevelInfo> val)
		{
			this.GameLevelInfo = val;
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06006082 RID: 24706 RVA: 0x00123F2D File Offset: 0x0012212D
		// (set) Token: 0x06006083 RID: 24707 RVA: 0x00123F35 File Offset: 0x00122135
		public List<GameStatus> GameStatus
		{
			get
			{
				return this._GameStatus;
			}
			set
			{
				this._GameStatus = value;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06006084 RID: 24708 RVA: 0x00123F2D File Offset: 0x0012212D
		public List<GameStatus> GameStatusList
		{
			get
			{
				return this._GameStatus;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06006085 RID: 24709 RVA: 0x00123F3E File Offset: 0x0012213E
		public int GameStatusCount
		{
			get
			{
				return this._GameStatus.Count;
			}
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x00123F4B File Offset: 0x0012214B
		public void AddGameStatus(GameStatus val)
		{
			this._GameStatus.Add(val);
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x00123F59 File Offset: 0x00122159
		public void ClearGameStatus()
		{
			this._GameStatus.Clear();
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x00123F66 File Offset: 0x00122166
		public void SetGameStatus(List<GameStatus> val)
		{
			this.GameStatus = val;
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00123F6F File Offset: 0x0012216F
		// (set) Token: 0x0600608A RID: 24714 RVA: 0x00123F77 File Offset: 0x00122177
		public List<GameAccountList> GameAccounts
		{
			get
			{
				return this._GameAccounts;
			}
			set
			{
				this._GameAccounts = value;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x00123F6F File Offset: 0x0012216F
		public List<GameAccountList> GameAccountsList
		{
			get
			{
				return this._GameAccounts;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x0600608C RID: 24716 RVA: 0x00123F80 File Offset: 0x00122180
		public int GameAccountsCount
		{
			get
			{
				return this._GameAccounts.Count;
			}
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x00123F8D File Offset: 0x0012218D
		public void AddGameAccounts(GameAccountList val)
		{
			this._GameAccounts.Add(val);
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x00123F9B File Offset: 0x0012219B
		public void ClearGameAccounts()
		{
			this._GameAccounts.Clear();
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x00123FA8 File Offset: 0x001221A8
		public void SetGameAccounts(List<GameAccountList> val)
		{
			this.GameAccounts = val;
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x00123FB4 File Offset: 0x001221B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountLevelInfo)
			{
				num ^= this.AccountLevelInfo.GetHashCode();
			}
			if (this.HasPrivacyInfo)
			{
				num ^= this.PrivacyInfo.GetHashCode();
			}
			if (this.HasParentalControlInfo)
			{
				num ^= this.ParentalControlInfo.GetHashCode();
			}
			foreach (GameLevelInfo gameLevelInfo in this.GameLevelInfo)
			{
				num ^= gameLevelInfo.GetHashCode();
			}
			foreach (GameStatus gameStatus in this.GameStatus)
			{
				num ^= gameStatus.GetHashCode();
			}
			foreach (GameAccountList gameAccountList in this.GameAccounts)
			{
				num ^= gameAccountList.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001240E8 File Offset: 0x001222E8
		public override bool Equals(object obj)
		{
			AccountState accountState = obj as AccountState;
			if (accountState == null)
			{
				return false;
			}
			if (this.HasAccountLevelInfo != accountState.HasAccountLevelInfo || (this.HasAccountLevelInfo && !this.AccountLevelInfo.Equals(accountState.AccountLevelInfo)))
			{
				return false;
			}
			if (this.HasPrivacyInfo != accountState.HasPrivacyInfo || (this.HasPrivacyInfo && !this.PrivacyInfo.Equals(accountState.PrivacyInfo)))
			{
				return false;
			}
			if (this.HasParentalControlInfo != accountState.HasParentalControlInfo || (this.HasParentalControlInfo && !this.ParentalControlInfo.Equals(accountState.ParentalControlInfo)))
			{
				return false;
			}
			if (this.GameLevelInfo.Count != accountState.GameLevelInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameLevelInfo.Count; i++)
			{
				if (!this.GameLevelInfo[i].Equals(accountState.GameLevelInfo[i]))
				{
					return false;
				}
			}
			if (this.GameStatus.Count != accountState.GameStatus.Count)
			{
				return false;
			}
			for (int j = 0; j < this.GameStatus.Count; j++)
			{
				if (!this.GameStatus[j].Equals(accountState.GameStatus[j]))
				{
					return false;
				}
			}
			if (this.GameAccounts.Count != accountState.GameAccounts.Count)
			{
				return false;
			}
			for (int k = 0; k < this.GameAccounts.Count; k++)
			{
				if (!this.GameAccounts[k].Equals(accountState.GameAccounts[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06006092 RID: 24722 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x00124276 File Offset: 0x00122476
		public static AccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountState>(bs, 0, -1);
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x00124280 File Offset: 0x00122480
		public void Deserialize(Stream stream)
		{
			AccountState.Deserialize(stream, this);
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x0012428A File Offset: 0x0012248A
		public static AccountState Deserialize(Stream stream, AccountState instance)
		{
			return AccountState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x00124298 File Offset: 0x00122498
		public static AccountState DeserializeLengthDelimited(Stream stream)
		{
			AccountState accountState = new AccountState();
			AccountState.DeserializeLengthDelimited(stream, accountState);
			return accountState;
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001242B4 File Offset: 0x001224B4
		public static AccountState DeserializeLengthDelimited(Stream stream, AccountState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountState.Deserialize(stream, instance, num);
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x001242DC File Offset: 0x001224DC
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									if (instance.ParentalControlInfo == null)
									{
										instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
										continue;
									}
									ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
									continue;
								}
							}
							else
							{
								if (instance.PrivacyInfo == null)
								{
									instance.PrivacyInfo = PrivacyInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								PrivacyInfo.DeserializeLengthDelimited(stream, instance.PrivacyInfo);
								continue;
							}
						}
						else
						{
							if (instance.AccountLevelInfo == null)
							{
								instance.AccountLevelInfo = AccountLevelInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountLevelInfo.DeserializeLengthDelimited(stream, instance.AccountLevelInfo);
							continue;
						}
					}
					else
					{
						if (num == 42)
						{
							instance.GameLevelInfo.Add(bnet.protocol.account.v1.GameLevelInfo.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 50)
						{
							instance.GameStatus.Add(bnet.protocol.account.v1.GameStatus.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 58)
						{
							instance.GameAccounts.Add(GameAccountList.DeserializeLengthDelimited(stream));
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

		// Token: 0x06006099 RID: 24729 RVA: 0x00124484 File Offset: 0x00122684
		public void Serialize(Stream stream)
		{
			AccountState.Serialize(stream, this);
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x00124490 File Offset: 0x00122690
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
				foreach (GameLevelInfo gameLevelInfo in instance.GameLevelInfo)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, gameLevelInfo.GetSerializedSize());
					bnet.protocol.account.v1.GameLevelInfo.Serialize(stream, gameLevelInfo);
				}
			}
			if (instance.GameStatus.Count > 0)
			{
				foreach (GameStatus gameStatus in instance.GameStatus)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, gameStatus.GetSerializedSize());
					bnet.protocol.account.v1.GameStatus.Serialize(stream, gameStatus);
				}
			}
			if (instance.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in instance.GameAccounts)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, gameAccountList.GetSerializedSize());
					GameAccountList.Serialize(stream, gameAccountList);
				}
			}
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x00124658 File Offset: 0x00122858
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountLevelInfo)
			{
				num += 1U;
				uint serializedSize = this.AccountLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPrivacyInfo)
			{
				num += 1U;
				uint serializedSize2 = this.PrivacyInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasParentalControlInfo)
			{
				num += 1U;
				uint serializedSize3 = this.ParentalControlInfo.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.GameLevelInfo.Count > 0)
			{
				foreach (GameLevelInfo gameLevelInfo in this.GameLevelInfo)
				{
					num += 1U;
					uint serializedSize4 = gameLevelInfo.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.GameStatus.Count > 0)
			{
				foreach (GameStatus gameStatus in this.GameStatus)
				{
					num += 1U;
					uint serializedSize5 = gameStatus.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (this.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in this.GameAccounts)
				{
					num += 1U;
					uint serializedSize6 = gameAccountList.GetSerializedSize();
					num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
				}
			}
			return num;
		}

		// Token: 0x04001DB6 RID: 7606
		public bool HasAccountLevelInfo;

		// Token: 0x04001DB7 RID: 7607
		private AccountLevelInfo _AccountLevelInfo;

		// Token: 0x04001DB8 RID: 7608
		public bool HasPrivacyInfo;

		// Token: 0x04001DB9 RID: 7609
		private PrivacyInfo _PrivacyInfo;

		// Token: 0x04001DBA RID: 7610
		public bool HasParentalControlInfo;

		// Token: 0x04001DBB RID: 7611
		private ParentalControlInfo _ParentalControlInfo;

		// Token: 0x04001DBC RID: 7612
		private List<GameLevelInfo> _GameLevelInfo = new List<GameLevelInfo>();

		// Token: 0x04001DBD RID: 7613
		private List<GameStatus> _GameStatus = new List<GameStatus>();

		// Token: 0x04001DBE RID: 7614
		private List<GameAccountList> _GameAccounts = new List<GameAccountList>();
	}
}
