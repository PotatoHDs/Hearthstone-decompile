using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AD RID: 941
	public class AddPlayersRequest : IProtoBuf
	{
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000C50DA File Offset: 0x000C32DA
		// (set) Token: 0x06003D2E RID: 15662 RVA: 0x000C50E2 File Offset: 0x000C32E2
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003D2F RID: 15663 RVA: 0x000C50EB File Offset: 0x000C32EB
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000C50F4 File Offset: 0x000C32F4
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000C50FC File Offset: 0x000C32FC
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

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000C50F4 File Offset: 0x000C32F4
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000C5105 File Offset: 0x000C3305
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000C5112 File Offset: 0x000C3312
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000C5120 File Offset: 0x000C3320
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000C512D File Offset: 0x000C332D
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000C5136 File Offset: 0x000C3336
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x000C513E File Offset: 0x000C333E
		public List<Assignment> Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000C5136 File Offset: 0x000C3336
		public List<Assignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000C5147 File Offset: 0x000C3347
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000C5154 File Offset: 0x000C3354
		public void AddAssignment(Assignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000C5162 File Offset: 0x000C3362
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000C516F File Offset: 0x000C336F
		public void SetAssignment(List<Assignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000C5178 File Offset: 0x000C3378
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x000C5180 File Offset: 0x000C3380
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

		// Token: 0x06003D40 RID: 15680 RVA: 0x000C5193 File Offset: 0x000C3393
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000C519C File Offset: 0x000C339C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			foreach (Assignment assignment in this.Assignment)
			{
				num ^= assignment.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000C5268 File Offset: 0x000C3468
		public override bool Equals(object obj)
		{
			AddPlayersRequest addPlayersRequest = obj as AddPlayersRequest;
			if (addPlayersRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(addPlayersRequest.GameHandle))
			{
				return false;
			}
			if (this.Player.Count != addPlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(addPlayersRequest.Player[i]))
				{
					return false;
				}
			}
			if (this.Assignment.Count != addPlayersRequest.Assignment.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Assignment.Count; j++)
			{
				if (!this.Assignment[j].Equals(addPlayersRequest.Assignment[j]))
				{
					return false;
				}
			}
			return this.HasHost == addPlayersRequest.HasHost && (!this.HasHost || this.Host.Equals(addPlayersRequest.Host));
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000C5364 File Offset: 0x000C3564
		public static AddPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000C536E File Offset: 0x000C356E
		public void Deserialize(Stream stream)
		{
			AddPlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000C5378 File Offset: 0x000C3578
		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance)
		{
			return AddPlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000C5384 File Offset: 0x000C3584
		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersRequest addPlayersRequest = new AddPlayersRequest();
			AddPlayersRequest.DeserializeLengthDelimited(stream, addPlayersRequest);
			return addPlayersRequest;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000C53A0 File Offset: 0x000C35A0
		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream, AddPlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x000C53C8 File Offset: 0x000C35C8
		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<Assignment>();
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
					else
					{
						if (num == 26)
						{
							instance.Assignment.Add(bnet.protocol.games.v1.Assignment.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							if (instance.Host == null)
							{
								instance.Host = ProcessId.DeserializeLengthDelimited(stream);
								continue;
							}
							ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		// Token: 0x06003D4A RID: 15690 RVA: 0x000C5503 File Offset: 0x000C3703
		public void Serialize(Stream stream)
		{
			AddPlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000C550C File Offset: 0x000C370C
		public static void Serialize(Stream stream, AddPlayersRequest instance)
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
			if (instance.Assignment.Count > 0)
			{
				foreach (Assignment assignment in instance.Assignment)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, assignment.GetSerializedSize());
					bnet.protocol.games.v1.Assignment.Serialize(stream, assignment);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000C5650 File Offset: 0x000C3850
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
			if (this.Assignment.Count > 0)
			{
				foreach (Assignment assignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize3 = assignment.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize4 = this.Host.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015D1 RID: 5585
		private List<Player> _Player = new List<Player>();

		// Token: 0x040015D2 RID: 5586
		private List<Assignment> _Assignment = new List<Assignment>();

		// Token: 0x040015D3 RID: 5587
		public bool HasHost;

		// Token: 0x040015D4 RID: 5588
		private ProcessId _Host;
	}
}
