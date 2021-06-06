using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000388 RID: 904
	public class GameCreatedNotification : IProtoBuf
	{
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000BB9B4 File Offset: 0x000B9BB4
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000BB9BC File Offset: 0x000B9BBC
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

		// Token: 0x060039B8 RID: 14776 RVA: 0x000BB9CF File Offset: 0x000B9BCF
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000BB9D8 File Offset: 0x000B9BD8
		// (set) Token: 0x060039BA RID: 14778 RVA: 0x000BB9E0 File Offset: 0x000B9BE0
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

		// Token: 0x060039BB RID: 14779 RVA: 0x000BB9F0 File Offset: 0x000B9BF0
		public void SetErrorId(uint val)
		{
			this.ErrorId = val;
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000BB9F9 File Offset: 0x000B9BF9
		// (set) Token: 0x060039BD RID: 14781 RVA: 0x000BBA01 File Offset: 0x000B9C01
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

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000BB9F9 File Offset: 0x000B9BF9
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000BBA0A File Offset: 0x000B9C0A
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000BBA17 File Offset: 0x000B9C17
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000BBA25 File Offset: 0x000B9C25
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000BBA32 File Offset: 0x000B9C32
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000BBA3C File Offset: 0x000B9C3C
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
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000BBAD0 File Offset: 0x000B9CD0
		public override bool Equals(object obj)
		{
			GameCreatedNotification gameCreatedNotification = obj as GameCreatedNotification;
			if (gameCreatedNotification == null)
			{
				return false;
			}
			if (this.HasGameHandle != gameCreatedNotification.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(gameCreatedNotification.GameHandle)))
			{
				return false;
			}
			if (this.HasErrorId != gameCreatedNotification.HasErrorId || (this.HasErrorId && !this.ErrorId.Equals(gameCreatedNotification.ErrorId)))
			{
				return false;
			}
			if (this.ConnectInfo.Count != gameCreatedNotification.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ConnectInfo.Count; i++)
			{
				if (!this.ConnectInfo[i].Equals(gameCreatedNotification.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000BBB94 File Offset: 0x000B9D94
		public static GameCreatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameCreatedNotification>(bs, 0, -1);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000BBB9E File Offset: 0x000B9D9E
		public void Deserialize(Stream stream)
		{
			GameCreatedNotification.Deserialize(stream, this);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000BBBA8 File Offset: 0x000B9DA8
		public static GameCreatedNotification Deserialize(Stream stream, GameCreatedNotification instance)
		{
			return GameCreatedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000BBBB4 File Offset: 0x000B9DB4
		public static GameCreatedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameCreatedNotification gameCreatedNotification = new GameCreatedNotification();
			GameCreatedNotification.DeserializeLengthDelimited(stream, gameCreatedNotification);
			return gameCreatedNotification;
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000BBBD0 File Offset: 0x000B9DD0
		public static GameCreatedNotification DeserializeLengthDelimited(Stream stream, GameCreatedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameCreatedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000BBBF8 File Offset: 0x000B9DF8
		public static GameCreatedNotification Deserialize(Stream stream, GameCreatedNotification instance, long limit)
		{
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
						else
						{
							instance.ConnectInfo.Add(bnet.protocol.games.v1.ConnectInfo.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.ErrorId = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000BBCDE File Offset: 0x000B9EDE
		public void Serialize(Stream stream)
		{
			GameCreatedNotification.Serialize(stream, this);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000BBCE8 File Offset: 0x000B9EE8
		public static void Serialize(Stream stream, GameCreatedNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ErrorId);
			}
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.games.v1.ConnectInfo.Serialize(stream, connectInfo);
				}
			}
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000BBDA8 File Offset: 0x000B9FA8
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
				num += ProtocolParser.SizeOfUInt32(this.ErrorId);
			}
			if (this.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in this.ConnectInfo)
				{
					num += 1U;
					uint serializedSize2 = connectInfo.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400151F RID: 5407
		public bool HasGameHandle;

		// Token: 0x04001520 RID: 5408
		private GameHandle _GameHandle;

		// Token: 0x04001521 RID: 5409
		public bool HasErrorId;

		// Token: 0x04001522 RID: 5410
		private uint _ErrorId;

		// Token: 0x04001523 RID: 5411
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
