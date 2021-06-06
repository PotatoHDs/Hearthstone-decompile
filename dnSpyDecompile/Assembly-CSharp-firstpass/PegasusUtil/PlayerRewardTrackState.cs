using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003E RID: 62
	public class PlayerRewardTrackState : IProtoBuf
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E4BB File Offset: 0x0000C6BB
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000E4C3 File Offset: 0x0000C6C3
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000E4D3 File Offset: 0x0000C6D3
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000E4DB File Offset: 0x0000C6DB
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000E4EB File Offset: 0x0000C6EB
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000E4F3 File Offset: 0x0000C6F3
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000E503 File Offset: 0x0000C703
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000E50B File Offset: 0x0000C70B
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000E514 File Offset: 0x0000C714
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000E51C File Offset: 0x0000C71C
		public bool IsActiveRewardTrack
		{
			get
			{
				return this._IsActiveRewardTrack;
			}
			set
			{
				this._IsActiveRewardTrack = value;
				this.HasIsActiveRewardTrack = true;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E52C File Offset: 0x0000C72C
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
			if (this.HasIsActiveRewardTrack)
			{
				num ^= this.IsActiveRewardTrack.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public override bool Equals(object obj)
		{
			PlayerRewardTrackState playerRewardTrackState = obj as PlayerRewardTrackState;
			if (playerRewardTrackState == null)
			{
				return false;
			}
			if (this.HasRewardTrackId != playerRewardTrackState.HasRewardTrackId || (this.HasRewardTrackId && !this.RewardTrackId.Equals(playerRewardTrackState.RewardTrackId)))
			{
				return false;
			}
			if (this.HasLevel != playerRewardTrackState.HasLevel || (this.HasLevel && !this.Level.Equals(playerRewardTrackState.Level)))
			{
				return false;
			}
			if (this.HasXp != playerRewardTrackState.HasXp || (this.HasXp && !this.Xp.Equals(playerRewardTrackState.Xp)))
			{
				return false;
			}
			if (this.TrackLevel.Count != playerRewardTrackState.TrackLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TrackLevel.Count; i++)
			{
				if (!this.TrackLevel[i].Equals(playerRewardTrackState.TrackLevel[i]))
				{
					return false;
				}
			}
			return this.HasIsActiveRewardTrack == playerRewardTrackState.HasIsActiveRewardTrack && (!this.HasIsActiveRewardTrack || this.IsActiveRewardTrack.Equals(playerRewardTrackState.IsActiveRewardTrack));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E717 File Offset: 0x0000C917
		public void Deserialize(Stream stream)
		{
			PlayerRewardTrackState.Deserialize(stream, this);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000E721 File Offset: 0x0000C921
		public static PlayerRewardTrackState Deserialize(Stream stream, PlayerRewardTrackState instance)
		{
			return PlayerRewardTrackState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E72C File Offset: 0x0000C92C
		public static PlayerRewardTrackState DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackState playerRewardTrackState = new PlayerRewardTrackState();
			PlayerRewardTrackState.DeserializeLengthDelimited(stream, playerRewardTrackState);
			return playerRewardTrackState;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E748 File Offset: 0x0000C948
		public static PlayerRewardTrackState DeserializeLengthDelimited(Stream stream, PlayerRewardTrackState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerRewardTrackState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000E770 File Offset: 0x0000C970
		public static PlayerRewardTrackState Deserialize(Stream stream, PlayerRewardTrackState instance, long limit)
		{
			if (instance.TrackLevel == null)
			{
				instance.TrackLevel = new List<PlayerRewardTrackLevelState>();
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
						if (num == 40)
						{
							instance.IsActiveRewardTrack = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600038B RID: 907 RVA: 0x0000E871 File Offset: 0x0000CA71
		public void Serialize(Stream stream)
		{
			PlayerRewardTrackState.Serialize(stream, this);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E87C File Offset: 0x0000CA7C
		public static void Serialize(Stream stream, PlayerRewardTrackState instance)
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
			if (instance.HasIsActiveRewardTrack)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsActiveRewardTrack);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E964 File Offset: 0x0000CB64
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
			if (this.HasIsActiveRewardTrack)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000129 RID: 297
		public bool HasRewardTrackId;

		// Token: 0x0400012A RID: 298
		private int _RewardTrackId;

		// Token: 0x0400012B RID: 299
		public bool HasLevel;

		// Token: 0x0400012C RID: 300
		private int _Level;

		// Token: 0x0400012D RID: 301
		public bool HasXp;

		// Token: 0x0400012E RID: 302
		private int _Xp;

		// Token: 0x0400012F RID: 303
		private List<PlayerRewardTrackLevelState> _TrackLevel = new List<PlayerRewardTrackLevelState>();

		// Token: 0x04000130 RID: 304
		public bool HasIsActiveRewardTrack;

		// Token: 0x04000131 RID: 305
		private bool _IsActiveRewardTrack;
	}
}
