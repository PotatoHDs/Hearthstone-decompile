using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002EF RID: 751
	public class AddRecentPlayersRequest : IProtoBuf
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000999ED File Offset: 0x00097BED
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000999F5 File Offset: 0x00097BF5
		public List<RecentPlayer> Players
		{
			get
			{
				return this._Players;
			}
			set
			{
				this._Players = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000999ED File Offset: 0x00097BED
		public List<RecentPlayer> PlayersList
		{
			get
			{
				return this._Players;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000999FE File Offset: 0x00097BFE
		public int PlayersCount
		{
			get
			{
				return this._Players.Count;
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00099A0B File Offset: 0x00097C0B
		public void AddPlayers(RecentPlayer val)
		{
			this._Players.Add(val);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00099A19 File Offset: 0x00097C19
		public void ClearPlayers()
		{
			this._Players.Clear();
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00099A26 File Offset: 0x00097C26
		public void SetPlayers(List<RecentPlayer> val)
		{
			this.Players = val;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x00099A2F File Offset: 0x00097C2F
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x00099A37 File Offset: 0x00097C37
		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00099A4A File Offset: 0x00097C4A
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x00099A53 File Offset: 0x00097C53
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x00099A5B File Offset: 0x00097C5B
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00099A6B File Offset: 0x00097C6B
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00099A74 File Offset: 0x00097C74
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RecentPlayer recentPlayer in this.Players)
			{
				num ^= recentPlayer.GetHashCode();
			}
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00099B08 File Offset: 0x00097D08
		public override bool Equals(object obj)
		{
			AddRecentPlayersRequest addRecentPlayersRequest = obj as AddRecentPlayersRequest;
			if (addRecentPlayersRequest == null)
			{
				return false;
			}
			if (this.Players.Count != addRecentPlayersRequest.Players.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Players.Count; i++)
			{
				if (!this.Players[i].Equals(addRecentPlayersRequest.Players[i]))
				{
					return false;
				}
			}
			return this.HasAgentId == addRecentPlayersRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(addRecentPlayersRequest.AgentId)) && this.HasProgram == addRecentPlayersRequest.HasProgram && (!this.HasProgram || this.Program.Equals(addRecentPlayersRequest.Program));
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x00099BCC File Offset: 0x00097DCC
		public static AddRecentPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddRecentPlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00099BD6 File Offset: 0x00097DD6
		public void Deserialize(Stream stream)
		{
			AddRecentPlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00099BE0 File Offset: 0x00097DE0
		public static AddRecentPlayersRequest Deserialize(Stream stream, AddRecentPlayersRequest instance)
		{
			return AddRecentPlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x00099BEC File Offset: 0x00097DEC
		public static AddRecentPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			AddRecentPlayersRequest addRecentPlayersRequest = new AddRecentPlayersRequest();
			AddRecentPlayersRequest.DeserializeLengthDelimited(stream, addRecentPlayersRequest);
			return addRecentPlayersRequest;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00099C08 File Offset: 0x00097E08
		public static AddRecentPlayersRequest DeserializeLengthDelimited(Stream stream, AddRecentPlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddRecentPlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x00099C30 File Offset: 0x00097E30
		public static AddRecentPlayersRequest Deserialize(Stream stream, AddRecentPlayersRequest instance, long limit)
		{
			if (instance.Players == null)
			{
				instance.Players = new List<RecentPlayer>();
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
						if (num != 24)
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
							instance.Program = ProtocolParser.ReadUInt32(stream);
						}
					}
					else if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
				}
				else
				{
					instance.Players.Add(RecentPlayer.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x00099D16 File Offset: 0x00097F16
		public void Serialize(Stream stream)
		{
			AddRecentPlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x00099D20 File Offset: 0x00097F20
		public static void Serialize(Stream stream, AddRecentPlayersRequest instance)
		{
			if (instance.Players.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in instance.Players)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, recentPlayer.GetSerializedSize());
					RecentPlayer.Serialize(stream, recentPlayer);
				}
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00099DE0 File Offset: 0x00097FE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Players.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in this.Players)
				{
					num += 1U;
					uint serializedSize = recentPlayer.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize2 = this.AgentId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			return num;
		}

		// Token: 0x0400126C RID: 4716
		private List<RecentPlayer> _Players = new List<RecentPlayer>();

		// Token: 0x0400126D RID: 4717
		public bool HasAgentId;

		// Token: 0x0400126E RID: 4718
		private EntityId _AgentId;

		// Token: 0x0400126F RID: 4719
		public bool HasProgram;

		// Token: 0x04001270 RID: 4720
		private uint _Program;
	}
}
