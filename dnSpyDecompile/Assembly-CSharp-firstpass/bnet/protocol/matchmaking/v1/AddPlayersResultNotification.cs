using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DF RID: 991
	public class AddPlayersResultNotification : IProtoBuf
	{
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x000CF9FA File Offset: 0x000CDBFA
		// (set) Token: 0x06004140 RID: 16704 RVA: 0x000CFA02 File Offset: 0x000CDC02
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000CFA15 File Offset: 0x000CDC15
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000CFA1E File Offset: 0x000CDC1E
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000CFA26 File Offset: 0x000CDC26
		public List<GameAccountHandle> GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000CFA1E File Offset: 0x000CDC1E
		public List<GameAccountHandle> GameAccountList
		{
			get
			{
				return this._GameAccount;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x000CFA2F File Offset: 0x000CDC2F
		public int GameAccountCount
		{
			get
			{
				return this._GameAccount.Count;
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000CFA3C File Offset: 0x000CDC3C
		public void AddGameAccount(GameAccountHandle val)
		{
			this._GameAccount.Add(val);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000CFA4A File Offset: 0x000CDC4A
		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000CFA57 File Offset: 0x000CDC57
		public void SetGameAccount(List<GameAccountHandle> val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000CFA60 File Offset: 0x000CDC60
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x000CFA68 File Offset: 0x000CDC68
		public uint ErrorId
		{
			get
			{
				return this._ErrorId;
			}
			set
			{
				this._ErrorId = value;
				this.HasErrorId = true;
			}
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000CFA78 File Offset: 0x000CDC78
		public void SetErrorId(uint val)
		{
			this.ErrorId = val;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x000CFA81 File Offset: 0x000CDC81
		// (set) Token: 0x0600414D RID: 16717 RVA: 0x000CFA89 File Offset: 0x000CDC89
		public List<ConnectInfo> ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x000CFA81 File Offset: 0x000CDC81
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000CFA92 File Offset: 0x000CDC92
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000CFA9F File Offset: 0x000CDC9F
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000CFAAD File Offset: 0x000CDCAD
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000CFABA File Offset: 0x000CDCBA
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000CFAC4 File Offset: 0x000CDCC4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			if (this.HasErrorId)
			{
				num ^= this.ErrorId.GetHashCode();
			}
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000CFB9C File Offset: 0x000CDD9C
		public override bool Equals(object obj)
		{
			AddPlayersResultNotification addPlayersResultNotification = obj as AddPlayersResultNotification;
			if (addPlayersResultNotification == null)
			{
				return false;
			}
			if (this.HasGameHandle != addPlayersResultNotification.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(addPlayersResultNotification.GameHandle)))
			{
				return false;
			}
			if (this.GameAccount.Count != addPlayersResultNotification.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccount.Count; i++)
			{
				if (!this.GameAccount[i].Equals(addPlayersResultNotification.GameAccount[i]))
				{
					return false;
				}
			}
			if (this.HasErrorId != addPlayersResultNotification.HasErrorId || (this.HasErrorId && !this.ErrorId.Equals(addPlayersResultNotification.ErrorId)))
			{
				return false;
			}
			if (this.ConnectInfo.Count != addPlayersResultNotification.ConnectInfo.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ConnectInfo.Count; j++)
			{
				if (!this.ConnectInfo[j].Equals(addPlayersResultNotification.ConnectInfo[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000CFCB1 File Offset: 0x000CDEB1
		public static AddPlayersResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersResultNotification>(bs, 0, -1);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000CFCBB File Offset: 0x000CDEBB
		public void Deserialize(Stream stream)
		{
			AddPlayersResultNotification.Deserialize(stream, this);
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000CFCC5 File Offset: 0x000CDEC5
		public static AddPlayersResultNotification Deserialize(Stream stream, AddPlayersResultNotification instance)
		{
			return AddPlayersResultNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000CFCD0 File Offset: 0x000CDED0
		public static AddPlayersResultNotification DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersResultNotification addPlayersResultNotification = new AddPlayersResultNotification();
			AddPlayersResultNotification.DeserializeLengthDelimited(stream, addPlayersResultNotification);
			return addPlayersResultNotification;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000CFCEC File Offset: 0x000CDEEC
		public static AddPlayersResultNotification DeserializeLengthDelimited(Stream stream, AddPlayersResultNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersResultNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000CFD14 File Offset: 0x000CDF14
		public static AddPlayersResultNotification Deserialize(Stream stream, AddPlayersResultNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<GameAccountHandle>();
			}
			if (instance.ConnectInfo == null)
			{
				instance.ConnectInfo = new List<ConnectInfo>();
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.GameHandle == null)
							{
								instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
							continue;
						}
					}
					else
					{
						if (num == 29)
						{
							instance.ErrorId = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 34)
						{
							instance.ConnectInfo.Add(bnet.protocol.matchmaking.v1.ConnectInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600415C RID: 16732 RVA: 0x000CFE36 File Offset: 0x000CE036
		public void Serialize(Stream stream)
		{
			AddPlayersResultNotification.Serialize(stream, this);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000CFE40 File Offset: 0x000CE040
		public static void Serialize(Stream stream, AddPlayersResultNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.GameAccount)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.ErrorId);
			}
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.matchmaking.v1.ConnectInfo.Serialize(stream, connectInfo);
				}
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000CFF6C File Offset: 0x000CE16C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
				{
					num += 1U;
					uint serializedSize2 = gameAccountHandle.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasErrorId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in this.ConnectInfo)
				{
					num += 1U;
					uint serializedSize3 = connectInfo.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001699 RID: 5785
		public bool HasGameHandle;

		// Token: 0x0400169A RID: 5786
		private GameHandle _GameHandle;

		// Token: 0x0400169B RID: 5787
		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

		// Token: 0x0400169C RID: 5788
		public bool HasErrorId;

		// Token: 0x0400169D RID: 5789
		private uint _ErrorId;

		// Token: 0x0400169E RID: 5790
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
