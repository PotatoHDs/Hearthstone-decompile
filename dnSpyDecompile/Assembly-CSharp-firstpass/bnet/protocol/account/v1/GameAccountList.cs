using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000539 RID: 1337
	public class GameAccountList : IProtoBuf
	{
		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x00123AB2 File Offset: 0x00121CB2
		// (set) Token: 0x0600605C RID: 24668 RVA: 0x00123ABA File Offset: 0x00121CBA
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x00123ACA File Offset: 0x00121CCA
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x00123AD3 File Offset: 0x00121CD3
		// (set) Token: 0x0600605F RID: 24671 RVA: 0x00123ADB File Offset: 0x00121CDB
		public List<GameAccountHandle> Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				this._Handle = value;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06006060 RID: 24672 RVA: 0x00123AD3 File Offset: 0x00121CD3
		public List<GameAccountHandle> HandleList
		{
			get
			{
				return this._Handle;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x00123AE4 File Offset: 0x00121CE4
		public int HandleCount
		{
			get
			{
				return this._Handle.Count;
			}
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00123AF1 File Offset: 0x00121CF1
		public void AddHandle(GameAccountHandle val)
		{
			this._Handle.Add(val);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00123AFF File Offset: 0x00121CFF
		public void ClearHandle()
		{
			this._Handle.Clear();
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x00123B0C File Offset: 0x00121D0C
		public void SetHandle(List<GameAccountHandle> val)
		{
			this.Handle = val;
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00123B18 File Offset: 0x00121D18
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.Handle)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x00123B94 File Offset: 0x00121D94
		public override bool Equals(object obj)
		{
			GameAccountList gameAccountList = obj as GameAccountList;
			if (gameAccountList == null)
			{
				return false;
			}
			if (this.HasRegion != gameAccountList.HasRegion || (this.HasRegion && !this.Region.Equals(gameAccountList.Region)))
			{
				return false;
			}
			if (this.Handle.Count != gameAccountList.Handle.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Handle.Count; i++)
			{
				if (!this.Handle[i].Equals(gameAccountList.Handle[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06006067 RID: 24679 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x00123C2D File Offset: 0x00121E2D
		public static GameAccountList ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountList>(bs, 0, -1);
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x00123C37 File Offset: 0x00121E37
		public void Deserialize(Stream stream)
		{
			GameAccountList.Deserialize(stream, this);
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x00123C41 File Offset: 0x00121E41
		public static GameAccountList Deserialize(Stream stream, GameAccountList instance)
		{
			return GameAccountList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x00123C4C File Offset: 0x00121E4C
		public static GameAccountList DeserializeLengthDelimited(Stream stream)
		{
			GameAccountList gameAccountList = new GameAccountList();
			GameAccountList.DeserializeLengthDelimited(stream, gameAccountList);
			return gameAccountList;
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x00123C68 File Offset: 0x00121E68
		public static GameAccountList DeserializeLengthDelimited(Stream stream, GameAccountList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountList.Deserialize(stream, instance, num);
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x00123C90 File Offset: 0x00121E90
		public static GameAccountList Deserialize(Stream stream, GameAccountList instance, long limit)
		{
			if (instance.Handle == null)
			{
				instance.Handle = new List<GameAccountHandle>();
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
				else if (num != 24)
				{
					if (num != 34)
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
						instance.Handle.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Region = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x00123D40 File Offset: 0x00121F40
		public void Serialize(Stream stream)
		{
			GameAccountList.Serialize(stream, this);
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x00123D4C File Offset: 0x00121F4C
		public static void Serialize(Stream stream, GameAccountList instance)
		{
			if (instance.HasRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.Handle.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.Handle)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x00123DE0 File Offset: 0x00121FE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			if (this.Handle.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.Handle)
				{
					num += 1U;
					uint serializedSize = gameAccountHandle.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001DB3 RID: 7603
		public bool HasRegion;

		// Token: 0x04001DB4 RID: 7604
		private uint _Region;

		// Token: 0x04001DB5 RID: 7605
		private List<GameAccountHandle> _Handle = new List<GameAccountHandle>();
	}
}
