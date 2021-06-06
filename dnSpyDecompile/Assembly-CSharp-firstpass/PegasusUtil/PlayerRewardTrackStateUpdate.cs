using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000109 RID: 265
	public class PlayerRewardTrackStateUpdate : IProtoBuf
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003D3E6 File Offset: 0x0003B5E6
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x0003D3EE File Offset: 0x0003B5EE
		public int RewardTrackId
		{
			get
			{
				return this._RewardTrackId;
			}
			set
			{
				this._RewardTrackId = value;
				this.HasRewardTrackId = true;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0003D3FE File Offset: 0x0003B5FE
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0003D406 File Offset: 0x0003B606
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003D416 File Offset: 0x0003B616
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0003D41E File Offset: 0x0003B61E
		public int Xp
		{
			get
			{
				return this._Xp;
			}
			set
			{
				this._Xp = value;
				this.HasXp = true;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0003D42E File Offset: 0x0003B62E
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003D436 File Offset: 0x0003B636
		public List<PlayerRewardTrackLevelState> TrackLevel
		{
			get
			{
				return this._TrackLevel;
			}
			set
			{
				this._TrackLevel = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0003D43F File Offset: 0x0003B63F
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0003D447 File Offset: 0x0003B647
		public List<PlayerRewardTrackState> State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003D450 File Offset: 0x0003B650
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRewardTrackId)
			{
				num ^= this.RewardTrackId.GetHashCode();
			}
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasXp)
			{
				num ^= this.Xp.GetHashCode();
			}
			foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in this.TrackLevel)
			{
				num ^= playerRewardTrackLevelState.GetHashCode();
			}
			foreach (PlayerRewardTrackState playerRewardTrackState in this.State)
			{
				num ^= playerRewardTrackState.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003D544 File Offset: 0x0003B744
		public override bool Equals(object obj)
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = obj as PlayerRewardTrackStateUpdate;
			if (playerRewardTrackStateUpdate == null)
			{
				return false;
			}
			if (this.HasRewardTrackId != playerRewardTrackStateUpdate.HasRewardTrackId || (this.HasRewardTrackId && !this.RewardTrackId.Equals(playerRewardTrackStateUpdate.RewardTrackId)))
			{
				return false;
			}
			if (this.HasLevel != playerRewardTrackStateUpdate.HasLevel || (this.HasLevel && !this.Level.Equals(playerRewardTrackStateUpdate.Level)))
			{
				return false;
			}
			if (this.HasXp != playerRewardTrackStateUpdate.HasXp || (this.HasXp && !this.Xp.Equals(playerRewardTrackStateUpdate.Xp)))
			{
				return false;
			}
			if (this.TrackLevel.Count != playerRewardTrackStateUpdate.TrackLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TrackLevel.Count; i++)
			{
				if (!this.TrackLevel[i].Equals(playerRewardTrackStateUpdate.TrackLevel[i]))
				{
					return false;
				}
			}
			if (this.State.Count != playerRewardTrackStateUpdate.State.Count)
			{
				return false;
			}
			for (int j = 0; j < this.State.Count; j++)
			{
				if (!this.State[j].Equals(playerRewardTrackStateUpdate.State[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003D68A File Offset: 0x0003B88A
		public void Deserialize(Stream stream)
		{
			PlayerRewardTrackStateUpdate.Deserialize(stream, this);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003D694 File Offset: 0x0003B894
		public static PlayerRewardTrackStateUpdate Deserialize(Stream stream, PlayerRewardTrackStateUpdate instance)
		{
			return PlayerRewardTrackStateUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		public static PlayerRewardTrackStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = new PlayerRewardTrackStateUpdate();
			PlayerRewardTrackStateUpdate.DeserializeLengthDelimited(stream, playerRewardTrackStateUpdate);
			return playerRewardTrackStateUpdate;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003D6BC File Offset: 0x0003B8BC
		public static PlayerRewardTrackStateUpdate DeserializeLengthDelimited(Stream stream, PlayerRewardTrackStateUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerRewardTrackStateUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003D6E4 File Offset: 0x0003B8E4
		public static PlayerRewardTrackStateUpdate Deserialize(Stream stream, PlayerRewardTrackStateUpdate instance, long limit)
		{
			if (instance.TrackLevel == null)
			{
				instance.TrackLevel = new List<PlayerRewardTrackLevelState>();
			}
			if (instance.State == null)
			{
				instance.State = new List<PlayerRewardTrackState>();
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Level = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Xp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.TrackLevel.Add(PlayerRewardTrackLevelState.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							instance.State.Add(PlayerRewardTrackState.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001184 RID: 4484 RVA: 0x0003D7FD File Offset: 0x0003B9FD
		public void Serialize(Stream stream)
		{
			PlayerRewardTrackStateUpdate.Serialize(stream, this);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003D808 File Offset: 0x0003BA08
		public static void Serialize(Stream stream, PlayerRewardTrackStateUpdate instance)
		{
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTrackId));
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasXp)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Xp));
			}
			if (instance.TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in instance.TrackLevel)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, playerRewardTrackLevelState.GetSerializedSize());
					PlayerRewardTrackLevelState.Serialize(stream, playerRewardTrackLevelState);
				}
			}
			if (instance.State.Count > 0)
			{
				foreach (PlayerRewardTrackState playerRewardTrackState in instance.State)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, playerRewardTrackState.GetSerializedSize());
					PlayerRewardTrackState.Serialize(stream, playerRewardTrackState);
				}
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003D938 File Offset: 0x0003BB38
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRewardTrackId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTrackId));
			}
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasXp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Xp));
			}
			if (this.TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in this.TrackLevel)
				{
					num += 1U;
					uint serializedSize = playerRewardTrackLevelState.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.State.Count > 0)
			{
				foreach (PlayerRewardTrackState playerRewardTrackState in this.State)
				{
					num += 1U;
					uint serializedSize2 = playerRewardTrackState.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04000554 RID: 1364
		public bool HasRewardTrackId;

		// Token: 0x04000555 RID: 1365
		private int _RewardTrackId;

		// Token: 0x04000556 RID: 1366
		public bool HasLevel;

		// Token: 0x04000557 RID: 1367
		private int _Level;

		// Token: 0x04000558 RID: 1368
		public bool HasXp;

		// Token: 0x04000559 RID: 1369
		private int _Xp;

		// Token: 0x0400055A RID: 1370
		private List<PlayerRewardTrackLevelState> _TrackLevel = new List<PlayerRewardTrackLevelState>();

		// Token: 0x0400055B RID: 1371
		private List<PlayerRewardTrackState> _State = new List<PlayerRewardTrackState>();

		// Token: 0x0200060B RID: 1547
		public enum PacketID
		{
			// Token: 0x0400205D RID: 8285
			ID = 614,
			// Token: 0x0400205E RID: 8286
			System = 0
		}
	}
}
