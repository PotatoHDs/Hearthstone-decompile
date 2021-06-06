using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.channel.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E5 RID: 997
	public class JoinGameRequest : IProtoBuf
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000D0F4F File Offset: 0x000CF14F
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000D0F57 File Offset: 0x000CF157
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

		// Token: 0x060041C2 RID: 16834 RVA: 0x000D0F6A File Offset: 0x000CF16A
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x000D0F73 File Offset: 0x000CF173
		// (set) Token: 0x060041C4 RID: 16836 RVA: 0x000D0F7B File Offset: 0x000CF17B
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

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x000D0F73 File Offset: 0x000CF173
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000D0F84 File Offset: 0x000CF184
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000D0F91 File Offset: 0x000CF191
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000D0F9F File Offset: 0x000CF19F
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x000D0FAC File Offset: 0x000CF1AC
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x000D0FB5 File Offset: 0x000CF1B5
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x000D0FBD File Offset: 0x000CF1BD
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000D0FD0 File Offset: 0x000CF1D0
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000D0FDC File Offset: 0x000CF1DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000D106C File Offset: 0x000CF26C
		public override bool Equals(object obj)
		{
			JoinGameRequest joinGameRequest = obj as JoinGameRequest;
			if (joinGameRequest == null)
			{
				return false;
			}
			if (this.HasGameHandle != joinGameRequest.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(joinGameRequest.GameHandle)))
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
			return this.HasChannelId == joinGameRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(joinGameRequest.ChannelId));
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000D112D File Offset: 0x000CF32D
		public static JoinGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameRequest>(bs, 0, -1);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000D1137 File Offset: 0x000CF337
		public void Deserialize(Stream stream)
		{
			JoinGameRequest.Deserialize(stream, this);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000D1141 File Offset: 0x000CF341
		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance)
		{
			return JoinGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000D114C File Offset: 0x000CF34C
		public static JoinGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinGameRequest joinGameRequest = new JoinGameRequest();
			JoinGameRequest.DeserializeLengthDelimited(stream, joinGameRequest);
			return joinGameRequest;
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000D1168 File Offset: 0x000CF368
		public static JoinGameRequest DeserializeLengthDelimited(Stream stream, JoinGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000D1190 File Offset: 0x000CF390
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
						}
					}
					else
					{
						instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
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

		// Token: 0x060041D6 RID: 16854 RVA: 0x000D1290 File Offset: 0x000CF490
		public void Serialize(Stream stream)
		{
			JoinGameRequest.Serialize(stream, this);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000D129C File Offset: 0x000CF49C
		public static void Serialize(Stream stream, JoinGameRequest instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000D136C File Offset: 0x000CF56C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040016B0 RID: 5808
		public bool HasGameHandle;

		// Token: 0x040016B1 RID: 5809
		private GameHandle _GameHandle;

		// Token: 0x040016B2 RID: 5810
		private List<Player> _Player = new List<Player>();

		// Token: 0x040016B3 RID: 5811
		public bool HasChannelId;

		// Token: 0x040016B4 RID: 5812
		private ChannelId _ChannelId;
	}
}
