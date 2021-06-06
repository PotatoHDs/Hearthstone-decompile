using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002ED RID: 749
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x00099049 File Offset: 0x00097249
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x00099051 File Offset: 0x00097251
		public List<BlockedPlayer> BlockedPlayers
		{
			get
			{
				return this._BlockedPlayers;
			}
			set
			{
				this._BlockedPlayers = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x00099049 File Offset: 0x00097249
		public List<BlockedPlayer> BlockedPlayersList
		{
			get
			{
				return this._BlockedPlayers;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x0009905A File Offset: 0x0009725A
		public int BlockedPlayersCount
		{
			get
			{
				return this._BlockedPlayers.Count;
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x00099067 File Offset: 0x00097267
		public void AddBlockedPlayers(BlockedPlayer val)
		{
			this._BlockedPlayers.Add(val);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x00099075 File Offset: 0x00097275
		public void ClearBlockedPlayers()
		{
			this._BlockedPlayers.Clear();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00099082 File Offset: 0x00097282
		public void SetBlockedPlayers(List<BlockedPlayer> val)
		{
			this.BlockedPlayers = val;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x0009908B File Offset: 0x0009728B
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x00099093 File Offset: 0x00097293
		public List<RecentPlayer> RecentPlayers
		{
			get
			{
				return this._RecentPlayers;
			}
			set
			{
				this._RecentPlayers = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x0009908B File Offset: 0x0009728B
		public List<RecentPlayer> RecentPlayersList
		{
			get
			{
				return this._RecentPlayers;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x0009909C File Offset: 0x0009729C
		public int RecentPlayersCount
		{
			get
			{
				return this._RecentPlayers.Count;
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000990A9 File Offset: 0x000972A9
		public void AddRecentPlayers(RecentPlayer val)
		{
			this._RecentPlayers.Add(val);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000990B7 File Offset: 0x000972B7
		public void ClearRecentPlayers()
		{
			this._RecentPlayers.Clear();
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000990C4 File Offset: 0x000972C4
		public void SetRecentPlayers(List<RecentPlayer> val)
		{
			this.RecentPlayers = val;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000990CD File Offset: 0x000972CD
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x000990D5 File Offset: 0x000972D5
		public List<Role> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000990CD File Offset: 0x000972CD
		public List<Role> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000990DE File Offset: 0x000972DE
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000990EB File Offset: 0x000972EB
		public void AddRole(Role val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000990F9 File Offset: 0x000972F9
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x00099106 File Offset: 0x00097306
		public void SetRole(List<Role> val)
		{
			this.Role = val;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x00099110 File Offset: 0x00097310
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BlockedPlayer blockedPlayer in this.BlockedPlayers)
			{
				num ^= blockedPlayer.GetHashCode();
			}
			foreach (RecentPlayer recentPlayer in this.RecentPlayers)
			{
				num ^= recentPlayer.GetHashCode();
			}
			foreach (Role role in this.Role)
			{
				num ^= role.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x00099200 File Offset: 0x00097400
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (this.BlockedPlayers.Count != subscribeResponse.BlockedPlayers.Count)
			{
				return false;
			}
			for (int i = 0; i < this.BlockedPlayers.Count; i++)
			{
				if (!this.BlockedPlayers[i].Equals(subscribeResponse.BlockedPlayers[i]))
				{
					return false;
				}
			}
			if (this.RecentPlayers.Count != subscribeResponse.RecentPlayers.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RecentPlayers.Count; j++)
			{
				if (!this.RecentPlayers[j].Equals(subscribeResponse.RecentPlayers[j]))
				{
					return false;
				}
			}
			if (this.Role.Count != subscribeResponse.Role.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Role.Count; k++)
			{
				if (!this.Role[k].Equals(subscribeResponse.Role[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0009930D File Offset: 0x0009750D
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00099317 File Offset: 0x00097517
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00099321 File Offset: 0x00097521
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0009932C File Offset: 0x0009752C
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x00099348 File Offset: 0x00097548
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x00099370 File Offset: 0x00097570
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.BlockedPlayers == null)
			{
				instance.BlockedPlayers = new List<BlockedPlayer>();
			}
			if (instance.RecentPlayers == null)
			{
				instance.RecentPlayers = new List<RecentPlayer>();
			}
			if (instance.Role == null)
			{
				instance.Role = new List<Role>();
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
						else
						{
							instance.Role.Add(bnet.protocol.Role.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.RecentPlayers.Add(RecentPlayer.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.BlockedPlayers.Add(BlockedPlayer.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x00099469 File Offset: 0x00097669
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x00099474 File Offset: 0x00097674
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.BlockedPlayers.Count > 0)
			{
				foreach (BlockedPlayer blockedPlayer in instance.BlockedPlayers)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, blockedPlayer.GetSerializedSize());
					BlockedPlayer.Serialize(stream, blockedPlayer);
				}
			}
			if (instance.RecentPlayers.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in instance.RecentPlayers)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, recentPlayer.GetSerializedSize());
					RecentPlayer.Serialize(stream, recentPlayer);
				}
			}
			if (instance.Role.Count > 0)
			{
				foreach (Role role in instance.Role)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, role.GetSerializedSize());
					bnet.protocol.Role.Serialize(stream, role);
				}
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000995B8 File Offset: 0x000977B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.BlockedPlayers.Count > 0)
			{
				foreach (BlockedPlayer blockedPlayer in this.BlockedPlayers)
				{
					num += 1U;
					uint serializedSize = blockedPlayer.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.RecentPlayers.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in this.RecentPlayers)
				{
					num += 1U;
					uint serializedSize2 = recentPlayer.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Role.Count > 0)
			{
				foreach (Role role in this.Role)
				{
					num += 1U;
					uint serializedSize3 = role.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001265 RID: 4709
		private List<BlockedPlayer> _BlockedPlayers = new List<BlockedPlayer>();

		// Token: 0x04001266 RID: 4710
		private List<RecentPlayer> _RecentPlayers = new List<RecentPlayer>();

		// Token: 0x04001267 RID: 4711
		private List<Role> _Role = new List<Role>();
	}
}
