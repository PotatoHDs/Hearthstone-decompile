using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DE RID: 990
	public class CreateGameResultNotification : IProtoBuf
	{
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x000CF362 File Offset: 0x000CD562
		// (set) Token: 0x0600411F RID: 16671 RVA: 0x000CF36A File Offset: 0x000CD56A
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

		// Token: 0x06004120 RID: 16672 RVA: 0x000CF37D File Offset: 0x000CD57D
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000CF386 File Offset: 0x000CD586
		// (set) Token: 0x06004122 RID: 16674 RVA: 0x000CF38E File Offset: 0x000CD58E
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

		// Token: 0x06004123 RID: 16675 RVA: 0x000CF39E File Offset: 0x000CD59E
		public void SetErrorId(uint val)
		{
			this.ErrorId = val;
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x000CF3A7 File Offset: 0x000CD5A7
		// (set) Token: 0x06004125 RID: 16677 RVA: 0x000CF3AF File Offset: 0x000CD5AF
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

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000CF3A7 File Offset: 0x000CD5A7
		public List<GameAccountHandle> GameAccountList
		{
			get
			{
				return this._GameAccount;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000CF3B8 File Offset: 0x000CD5B8
		public int GameAccountCount
		{
			get
			{
				return this._GameAccount.Count;
			}
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000CF3C5 File Offset: 0x000CD5C5
		public void AddGameAccount(GameAccountHandle val)
		{
			this._GameAccount.Add(val);
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x000CF3D3 File Offset: 0x000CD5D3
		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000CF3E0 File Offset: 0x000CD5E0
		public void SetGameAccount(List<GameAccountHandle> val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x000CF3E9 File Offset: 0x000CD5E9
		// (set) Token: 0x0600412C RID: 16684 RVA: 0x000CF3F1 File Offset: 0x000CD5F1
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

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x000CF3E9 File Offset: 0x000CD5E9
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x000CF3FA File Offset: 0x000CD5FA
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000CF407 File Offset: 0x000CD607
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000CF415 File Offset: 0x000CD615
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000CF422 File Offset: 0x000CD622
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000CF42C File Offset: 0x000CD62C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasErrorId)
			{
				num ^= this.ErrorId.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000CF504 File Offset: 0x000CD704
		public override bool Equals(object obj)
		{
			CreateGameResultNotification createGameResultNotification = obj as CreateGameResultNotification;
			if (createGameResultNotification == null)
			{
				return false;
			}
			if (this.HasGameHandle != createGameResultNotification.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(createGameResultNotification.GameHandle)))
			{
				return false;
			}
			if (this.HasErrorId != createGameResultNotification.HasErrorId || (this.HasErrorId && !this.ErrorId.Equals(createGameResultNotification.ErrorId)))
			{
				return false;
			}
			if (this.GameAccount.Count != createGameResultNotification.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccount.Count; i++)
			{
				if (!this.GameAccount[i].Equals(createGameResultNotification.GameAccount[i]))
				{
					return false;
				}
			}
			if (this.ConnectInfo.Count != createGameResultNotification.ConnectInfo.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ConnectInfo.Count; j++)
			{
				if (!this.ConnectInfo[j].Equals(createGameResultNotification.ConnectInfo[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000CF619 File Offset: 0x000CD819
		public static CreateGameResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameResultNotification>(bs, 0, -1);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000CF623 File Offset: 0x000CD823
		public void Deserialize(Stream stream)
		{
			CreateGameResultNotification.Deserialize(stream, this);
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000CF62D File Offset: 0x000CD82D
		public static CreateGameResultNotification Deserialize(Stream stream, CreateGameResultNotification instance)
		{
			return CreateGameResultNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000CF638 File Offset: 0x000CD838
		public static CreateGameResultNotification DeserializeLengthDelimited(Stream stream)
		{
			CreateGameResultNotification createGameResultNotification = new CreateGameResultNotification();
			CreateGameResultNotification.DeserializeLengthDelimited(stream, createGameResultNotification);
			return createGameResultNotification;
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000CF654 File Offset: 0x000CD854
		public static CreateGameResultNotification DeserializeLengthDelimited(Stream stream, CreateGameResultNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameResultNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000CF67C File Offset: 0x000CD87C
		public static CreateGameResultNotification Deserialize(Stream stream, CreateGameResultNotification instance, long limit)
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
					if (num <= 21)
					{
						if (num != 10)
						{
							if (num == 21)
							{
								instance.ErrorId = binaryReader.ReadUInt32();
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
						if (num == 26)
						{
							instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600413B RID: 16699 RVA: 0x000CF79E File Offset: 0x000CD99E
		public void Serialize(Stream stream)
		{
			CreateGameResultNotification.Serialize(stream, this);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000CF7A8 File Offset: 0x000CD9A8
		public static void Serialize(Stream stream, CreateGameResultNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.ErrorId);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.GameAccount)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
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

		// Token: 0x0600413D RID: 16701 RVA: 0x000CF8D4 File Offset: 0x000CDAD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasErrorId)
			{
				num += 1U;
				num += 4U;
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

		// Token: 0x04001693 RID: 5779
		public bool HasGameHandle;

		// Token: 0x04001694 RID: 5780
		private GameHandle _GameHandle;

		// Token: 0x04001695 RID: 5781
		public bool HasErrorId;

		// Token: 0x04001696 RID: 5782
		private uint _ErrorId;

		// Token: 0x04001697 RID: 5783
		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

		// Token: 0x04001698 RID: 5784
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
