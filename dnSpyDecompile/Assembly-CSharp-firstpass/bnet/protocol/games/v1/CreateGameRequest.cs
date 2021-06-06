using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A8 RID: 936
	public class CreateGameRequest : IProtoBuf
	{
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000C3B39 File Offset: 0x000C1D39
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x000C3B41 File Offset: 0x000C1D41
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000C3B4A File Offset: 0x000C1D4A
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000C3B53 File Offset: 0x000C1D53
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x000C3B5B File Offset: 0x000C1D5B
		public List<Attribute> CreationAttributes
		{
			get
			{
				return this._CreationAttributes;
			}
			set
			{
				this._CreationAttributes = value;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000C3B53 File Offset: 0x000C1D53
		public List<Attribute> CreationAttributesList
		{
			get
			{
				return this._CreationAttributes;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000C3B64 File Offset: 0x000C1D64
		public int CreationAttributesCount
		{
			get
			{
				return this._CreationAttributes.Count;
			}
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000C3B71 File Offset: 0x000C1D71
		public void AddCreationAttributes(Attribute val)
		{
			this._CreationAttributes.Add(val);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000C3B7F File Offset: 0x000C1D7F
		public void ClearCreationAttributes()
		{
			this._CreationAttributes.Clear();
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000C3B8C File Offset: 0x000C1D8C
		public void SetCreationAttributes(List<Attribute> val)
		{
			this.CreationAttributes = val;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000C3B95 File Offset: 0x000C1D95
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x000C3B9D File Offset: 0x000C1D9D
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

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000C3B95 File Offset: 0x000C1D95
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000C3BA6 File Offset: 0x000C1DA6
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000C3BB3 File Offset: 0x000C1DB3
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x000C3BC1 File Offset: 0x000C1DC1
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000C3BCE File Offset: 0x000C1DCE
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000C3BD7 File Offset: 0x000C1DD7
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000C3BDF File Offset: 0x000C1DDF
		public List<Team> Team
		{
			get
			{
				return this._Team;
			}
			set
			{
				this._Team = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000C3BD7 File Offset: 0x000C1DD7
		public List<Team> TeamList
		{
			get
			{
				return this._Team;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000C3BE8 File Offset: 0x000C1DE8
		public int TeamCount
		{
			get
			{
				return this._Team.Count;
			}
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000C3BF5 File Offset: 0x000C1DF5
		public void AddTeam(Team val)
		{
			this._Team.Add(val);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000C3C03 File Offset: 0x000C1E03
		public void ClearTeam()
		{
			this._Team.Clear();
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000C3C10 File Offset: 0x000C1E10
		public void SetTeam(List<Team> val)
		{
			this.Team = val;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000C3C19 File Offset: 0x000C1E19
		// (set) Token: 0x06003CCB RID: 15563 RVA: 0x000C3C21 File Offset: 0x000C1E21
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

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000C3C19 File Offset: 0x000C1E19
		public List<Assignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000C3C2A File Offset: 0x000C1E2A
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000C3C37 File Offset: 0x000C1E37
		public void AddAssignment(Assignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000C3C45 File Offset: 0x000C1E45
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000C3C52 File Offset: 0x000C1E52
		public void SetAssignment(List<Assignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000C3C5B File Offset: 0x000C1E5B
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x000C3C63 File Offset: 0x000C1E63
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

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000C3C80 File Offset: 0x000C1E80
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			foreach (Attribute attribute in this.CreationAttributes)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			foreach (Team team in this.Team)
			{
				num ^= team.GetHashCode();
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

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000C3DDC File Offset: 0x000C1FDC
		public override bool Equals(object obj)
		{
			CreateGameRequest createGameRequest = obj as CreateGameRequest;
			if (createGameRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(createGameRequest.GameHandle))
			{
				return false;
			}
			if (this.CreationAttributes.Count != createGameRequest.CreationAttributes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CreationAttributes.Count; i++)
			{
				if (!this.CreationAttributes[i].Equals(createGameRequest.CreationAttributes[i]))
				{
					return false;
				}
			}
			if (this.Player.Count != createGameRequest.Player.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Player.Count; j++)
			{
				if (!this.Player[j].Equals(createGameRequest.Player[j]))
				{
					return false;
				}
			}
			if (this.Team.Count != createGameRequest.Team.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Team.Count; k++)
			{
				if (!this.Team[k].Equals(createGameRequest.Team[k]))
				{
					return false;
				}
			}
			if (this.Assignment.Count != createGameRequest.Assignment.Count)
			{
				return false;
			}
			for (int l = 0; l < this.Assignment.Count; l++)
			{
				if (!this.Assignment[l].Equals(createGameRequest.Assignment[l]))
				{
					return false;
				}
			}
			return this.HasHost == createGameRequest.HasHost && (!this.HasHost || this.Host.Equals(createGameRequest.Host));
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000C3F80 File Offset: 0x000C2180
		public static CreateGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000C3F8A File Offset: 0x000C218A
		public void Deserialize(Stream stream)
		{
			CreateGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000C3F94 File Offset: 0x000C2194
		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance)
		{
			return CreateGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000C3FA0 File Offset: 0x000C21A0
		public static CreateGameRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameRequest createGameRequest = new CreateGameRequest();
			CreateGameRequest.DeserializeLengthDelimited(stream, createGameRequest);
			return createGameRequest;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000C3FBC File Offset: 0x000C21BC
		public static CreateGameRequest DeserializeLengthDelimited(Stream stream, CreateGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000C3FE4 File Offset: 0x000C21E4
		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance, long limit)
		{
			if (instance.CreationAttributes == null)
			{
				instance.CreationAttributes = new List<Attribute>();
			}
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.Team == null)
			{
				instance.Team = new List<Team>();
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
					if (num <= 34)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.CreationAttributes.Add(Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 34)
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
						if (num == 42)
						{
							instance.Team.Add(bnet.protocol.games.v1.Team.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 50)
						{
							instance.Assignment.Add(bnet.protocol.games.v1.Assignment.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 58)
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

		// Token: 0x06003CDD RID: 15581 RVA: 0x000C4184 File Offset: 0x000C2384
		public void Serialize(Stream stream)
		{
			CreateGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000C4190 File Offset: 0x000C2390
		public static void Serialize(Stream stream, CreateGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.CreationAttributes.Count > 0)
			{
				foreach (Attribute attribute in instance.CreationAttributes)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.Team.Count > 0)
			{
				foreach (Team team in instance.Team)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, team.GetSerializedSize());
					bnet.protocol.games.v1.Team.Serialize(stream, team);
				}
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (Assignment assignment in instance.Assignment)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, assignment.GetSerializedSize());
					bnet.protocol.games.v1.Assignment.Serialize(stream, assignment);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000C43A4 File Offset: 0x000C25A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.CreationAttributes.Count > 0)
			{
				foreach (Attribute attribute in this.CreationAttributes)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize3 = player.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.Team.Count > 0)
			{
				foreach (Team team in this.Team)
				{
					num += 1U;
					uint serializedSize4 = team.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.Assignment.Count > 0)
			{
				foreach (Assignment assignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize5 = assignment.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize6 = this.Host.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015C5 RID: 5573
		private List<Attribute> _CreationAttributes = new List<Attribute>();

		// Token: 0x040015C6 RID: 5574
		private List<Player> _Player = new List<Player>();

		// Token: 0x040015C7 RID: 5575
		private List<Team> _Team = new List<Team>();

		// Token: 0x040015C8 RID: 5576
		private List<Assignment> _Assignment = new List<Assignment>();

		// Token: 0x040015C9 RID: 5577
		public bool HasHost;

		// Token: 0x040015CA RID: 5578
		private ProcessId _Host;
	}
}
