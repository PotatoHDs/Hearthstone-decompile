using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D6 RID: 982
	public class Player : IProtoBuf
	{
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000CD697 File Offset: 0x000CB897
		// (set) Token: 0x06004071 RID: 16497 RVA: 0x000CD69F File Offset: 0x000CB89F
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000CD6B2 File Offset: 0x000CB8B2
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000CD6BB File Offset: 0x000CB8BB
		// (set) Token: 0x06004074 RID: 16500 RVA: 0x000CD6C3 File Offset: 0x000CB8C3
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000CD6BB File Offset: 0x000CB8BB
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000CD6CC File Offset: 0x000CB8CC
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000CD6D9 File Offset: 0x000CB8D9
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000CD6E7 File Offset: 0x000CB8E7
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000CD6F4 File Offset: 0x000CB8F4
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000CD700 File Offset: 0x000CB900
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000CD778 File Offset: 0x000CB978
		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (this.HasGameAccount != player.HasGameAccount || (this.HasGameAccount && !this.GameAccount.Equals(player.GameAccount)))
			{
				return false;
			}
			if (this.Attribute.Count != player.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(player.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000CD80E File Offset: 0x000CBA0E
		public static Player ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Player>(bs, 0, -1);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000CD818 File Offset: 0x000CBA18
		public void Deserialize(Stream stream)
		{
			Player.Deserialize(stream, this);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000CD822 File Offset: 0x000CBA22
		public static Player Deserialize(Stream stream, Player instance)
		{
			return Player.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000CD830 File Offset: 0x000CBA30
		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			Player.DeserializeLengthDelimited(stream, player);
			return player;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000CD84C File Offset: 0x000CBA4C
		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Player.Deserialize(stream, instance, num);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000CD874 File Offset: 0x000CBA74
		public static Player Deserialize(Stream stream, Player instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
					if (num != 18)
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
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000CD93E File Offset: 0x000CBB3E
		public void Serialize(Stream stream)
		{
			Player.Serialize(stream, this);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000CD948 File Offset: 0x000CBB48
		public static void Serialize(Stream stream, Player instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000CD9EC File Offset: 0x000CBBEC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001673 RID: 5747
		public bool HasGameAccount;

		// Token: 0x04001674 RID: 5748
		private GameAccountHandle _GameAccount;

		// Token: 0x04001675 RID: 5749
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
