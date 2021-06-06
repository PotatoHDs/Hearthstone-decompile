using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000371 RID: 881
	public class JoinGameRequest : IProtoBuf
	{
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000B74EB File Offset: 0x000B56EB
		// (set) Token: 0x060037E0 RID: 14304 RVA: 0x000B74F3 File Offset: 0x000B56F3
		public GameHandle GameHandle { get; set; }

		// Token: 0x060037E1 RID: 14305 RVA: 0x000B74FC File Offset: 0x000B56FC
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000B7505 File Offset: 0x000B5705
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000B750D File Offset: 0x000B570D
		public List<Player> Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000B7505 File Offset: 0x000B5705
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000B7516 File Offset: 0x000B5716
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000B7523 File Offset: 0x000B5723
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000B7531 File Offset: 0x000B5731
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000B753E File Offset: 0x000B573E
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000B7548 File Offset: 0x000B5748
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			return num;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000B75B8 File Offset: 0x000B57B8
		public override bool Equals(object obj)
		{
			JoinGameRequest joinGameRequest = obj as JoinGameRequest;
			if (joinGameRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(joinGameRequest.GameHandle))
			{
				return false;
			}
			if (this.Player.Count != joinGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(joinGameRequest.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000B7638 File Offset: 0x000B5838
		public static JoinGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameRequest>(bs, 0, -1);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000B7642 File Offset: 0x000B5842
		public void Deserialize(Stream stream)
		{
			JoinGameRequest.Deserialize(stream, this);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000B764C File Offset: 0x000B584C
		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance)
		{
			return JoinGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000B7658 File Offset: 0x000B5858
		public static JoinGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinGameRequest joinGameRequest = new JoinGameRequest();
			JoinGameRequest.DeserializeLengthDelimited(stream, joinGameRequest);
			return joinGameRequest;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000B7674 File Offset: 0x000B5874
		public static JoinGameRequest DeserializeLengthDelimited(Stream stream, JoinGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000B769C File Offset: 0x000B589C
		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
						instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
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

		// Token: 0x060037F2 RID: 14322 RVA: 0x000B7766 File Offset: 0x000B5966
		public void Serialize(Stream stream)
		{
			JoinGameRequest.Serialize(stream, this);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000B7770 File Offset: 0x000B5970
		public static void Serialize(Stream stream, JoinGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, player);
				}
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000B7824 File Offset: 0x000B5A24
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040014DF RID: 5343
		private List<Player> _Player = new List<Player>();
	}
}
