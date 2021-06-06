using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051F RID: 1311
	public class GameAccountNotification : IProtoBuf
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x0011BE38 File Offset: 0x0011A038
		// (set) Token: 0x06005D8E RID: 23950 RVA: 0x0011BE40 File Offset: 0x0011A040
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

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0011BE38 File Offset: 0x0011A038
		public List<GameAccountList> GameAccountsList
		{
			get
			{
				return this._GameAccounts;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x0011BE49 File Offset: 0x0011A049
		public int GameAccountsCount
		{
			get
			{
				return this._GameAccounts.Count;
			}
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x0011BE56 File Offset: 0x0011A056
		public void AddGameAccounts(GameAccountList val)
		{
			this._GameAccounts.Add(val);
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x0011BE64 File Offset: 0x0011A064
		public void ClearGameAccounts()
		{
			this._GameAccounts.Clear();
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x0011BE71 File Offset: 0x0011A071
		public void SetGameAccounts(List<GameAccountList> val)
		{
			this.GameAccounts = val;
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x0011BE7A File Offset: 0x0011A07A
		// (set) Token: 0x06005D95 RID: 23957 RVA: 0x0011BE82 File Offset: 0x0011A082
		public ulong SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = true;
			}
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x0011BE92 File Offset: 0x0011A092
		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x0011BE9B File Offset: 0x0011A09B
		// (set) Token: 0x06005D98 RID: 23960 RVA: 0x0011BEA3 File Offset: 0x0011A0A3
		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x0011BEB6 File Offset: 0x0011A0B6
		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0011BEC0 File Offset: 0x0011A0C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameAccountList gameAccountList in this.GameAccounts)
			{
				num ^= gameAccountList.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x0011BF54 File Offset: 0x0011A154
		public override bool Equals(object obj)
		{
			GameAccountNotification gameAccountNotification = obj as GameAccountNotification;
			if (gameAccountNotification == null)
			{
				return false;
			}
			if (this.GameAccounts.Count != gameAccountNotification.GameAccounts.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccounts.Count; i++)
			{
				if (!this.GameAccounts[i].Equals(gameAccountNotification.GameAccounts[i]))
				{
					return false;
				}
			}
			return this.HasSubscriberId == gameAccountNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(gameAccountNotification.SubscriberId)) && this.HasAccountTags == gameAccountNotification.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(gameAccountNotification.AccountTags));
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06005D9C RID: 23964 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x0011C018 File Offset: 0x0011A218
		public static GameAccountNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountNotification>(bs, 0, -1);
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x0011C022 File Offset: 0x0011A222
		public void Deserialize(Stream stream)
		{
			GameAccountNotification.Deserialize(stream, this);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x0011C02C File Offset: 0x0011A22C
		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance)
		{
			return GameAccountNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x0011C038 File Offset: 0x0011A238
		public static GameAccountNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountNotification gameAccountNotification = new GameAccountNotification();
			GameAccountNotification.DeserializeLengthDelimited(stream, gameAccountNotification);
			return gameAccountNotification;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x0011C054 File Offset: 0x0011A254
		public static GameAccountNotification DeserializeLengthDelimited(Stream stream, GameAccountNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x0011C07C File Offset: 0x0011A27C
		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.AccountTags == null)
						{
							instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
						}
						else
						{
							AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
						}
					}
					else
					{
						instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.GameAccounts.Add(GameAccountList.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x0011C162 File Offset: 0x0011A362
		public void Serialize(Stream stream)
		{
			GameAccountNotification.Serialize(stream, this);
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x0011C16C File Offset: 0x0011A36C
		public static void Serialize(Stream stream, GameAccountNotification instance)
		{
			if (instance.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in instance.GameAccounts)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameAccountList.GetSerializedSize());
					GameAccountList.Serialize(stream, gameAccountList);
				}
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x0011C22C File Offset: 0x0011A42C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in this.GameAccounts)
				{
					num += 1U;
					uint serializedSize = gameAccountList.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			if (this.HasAccountTags)
			{
				num += 1U;
				uint serializedSize2 = this.AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001CD8 RID: 7384
		private List<GameAccountList> _GameAccounts = new List<GameAccountList>();

		// Token: 0x04001CD9 RID: 7385
		public bool HasSubscriberId;

		// Token: 0x04001CDA RID: 7386
		private ulong _SubscriberId;

		// Token: 0x04001CDB RID: 7387
		public bool HasAccountTags;

		// Token: 0x04001CDC RID: 7388
		private AccountFieldTags _AccountTags;
	}
}
