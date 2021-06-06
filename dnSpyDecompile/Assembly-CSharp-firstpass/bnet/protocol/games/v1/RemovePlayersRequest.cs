using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AF RID: 943
	public class RemovePlayersRequest : IProtoBuf
	{
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000C5AA3 File Offset: 0x000C3CA3
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x000C5AAB File Offset: 0x000C3CAB
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003D64 RID: 15716 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000C5ABD File Offset: 0x000C3CBD
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x000C5AC5 File Offset: 0x000C3CC5
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

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000C5ABD File Offset: 0x000C3CBD
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000C5ACE File Offset: 0x000C3CCE
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000C5ADB File Offset: 0x000C3CDB
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000C5AE9 File Offset: 0x000C3CE9
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x000C5AF6 File Offset: 0x000C3CF6
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000C5AFF File Offset: 0x000C3CFF
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x000C5B07 File Offset: 0x000C3D07
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000C5B1A File Offset: 0x000C3D1A
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000C5B23 File Offset: 0x000C3D23
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x000C5B2B File Offset: 0x000C3D2B
		public List<uint> Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000C5B23 File Offset: 0x000C3D23
		public List<uint> ReasonList
		{
			get
			{
				return this._Reason;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000C5B34 File Offset: 0x000C3D34
		public int ReasonCount
		{
			get
			{
				return this._Reason.Count;
			}
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000C5B41 File Offset: 0x000C3D41
		public void AddReason(uint val)
		{
			this._Reason.Add(val);
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000C5B4F File Offset: 0x000C3D4F
		public void ClearReason()
		{
			this._Reason.Clear();
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000C5B5C File Offset: 0x000C3D5C
		public void SetReason(List<uint> val)
		{
			this.Reason = val;
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000C5B68 File Offset: 0x000C3D68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			foreach (uint num2 in this.Reason)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000C5C34 File Offset: 0x000C3E34
		public override bool Equals(object obj)
		{
			RemovePlayersRequest removePlayersRequest = obj as RemovePlayersRequest;
			if (removePlayersRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(removePlayersRequest.GameHandle))
			{
				return false;
			}
			if (this.Player.Count != removePlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(removePlayersRequest.Player[i]))
				{
					return false;
				}
			}
			if (this.HasHost != removePlayersRequest.HasHost || (this.HasHost && !this.Host.Equals(removePlayersRequest.Host)))
			{
				return false;
			}
			if (this.Reason.Count != removePlayersRequest.Reason.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Reason.Count; j++)
			{
				if (!this.Reason[j].Equals(removePlayersRequest.Reason[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000C5D33 File Offset: 0x000C3F33
		public static RemovePlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovePlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000C5D3D File Offset: 0x000C3F3D
		public void Deserialize(Stream stream)
		{
			RemovePlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000C5D47 File Offset: 0x000C3F47
		public static RemovePlayersRequest Deserialize(Stream stream, RemovePlayersRequest instance)
		{
			return RemovePlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000C5D54 File Offset: 0x000C3F54
		public static RemovePlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			RemovePlayersRequest removePlayersRequest = new RemovePlayersRequest();
			RemovePlayersRequest.DeserializeLengthDelimited(stream, removePlayersRequest);
			return removePlayersRequest;
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000C5D70 File Offset: 0x000C3F70
		public static RemovePlayersRequest DeserializeLengthDelimited(Stream stream, RemovePlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemovePlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000C5D98 File Offset: 0x000C3F98
		public static RemovePlayersRequest Deserialize(Stream stream, RemovePlayersRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.Reason == null)
			{
				instance.Reason = new List<uint>();
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
								instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
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
					else if (num != 26)
					{
						if (num == 34)
						{
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.Reason.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						if (instance.Host == null)
						{
							instance.Host = ProcessId.DeserializeLengthDelimited(stream);
							continue;
						}
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
						continue;
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

		// Token: 0x06003D7F RID: 15743 RVA: 0x000C5F04 File Offset: 0x000C4104
		public void Serialize(Stream stream)
		{
			RemovePlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000C5F10 File Offset: 0x000C4110
		public static void Serialize(Stream stream, RemovePlayersRequest instance)
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
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.Reason.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0U;
				foreach (uint val in instance.Reason)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Reason)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000C609C File Offset: 0x000C429C
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
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize3 = this.Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.Reason.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.Reason)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015D7 RID: 5591
		private List<Player> _Player = new List<Player>();

		// Token: 0x040015D8 RID: 5592
		public bool HasHost;

		// Token: 0x040015D9 RID: 5593
		private ProcessId _Host;

		// Token: 0x040015DA RID: 5594
		private List<uint> _Reason = new List<uint>();
	}
}
