using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F6 RID: 758
	public class RecentPlayersRemovedNotification : IProtoBuf
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x0009B27F File Offset: 0x0009947F
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x0009B287 File Offset: 0x00099487
		public List<RecentPlayer> Player
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

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x0009B27F File Offset: 0x0009947F
		public List<RecentPlayer> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x0009B290 File Offset: 0x00099490
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0009B29D File Offset: 0x0009949D
		public void AddPlayer(RecentPlayer val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x0009B2AB File Offset: 0x000994AB
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x0009B2B8 File Offset: 0x000994B8
		public void SetPlayer(List<RecentPlayer> val)
		{
			this.Player = val;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0009B2C4 File Offset: 0x000994C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RecentPlayer recentPlayer in this.Player)
			{
				num ^= recentPlayer.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0009B328 File Offset: 0x00099528
		public override bool Equals(object obj)
		{
			RecentPlayersRemovedNotification recentPlayersRemovedNotification = obj as RecentPlayersRemovedNotification;
			if (recentPlayersRemovedNotification == null)
			{
				return false;
			}
			if (this.Player.Count != recentPlayersRemovedNotification.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(recentPlayersRemovedNotification.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0009B393 File Offset: 0x00099593
		public static RecentPlayersRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RecentPlayersRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0009B39D File Offset: 0x0009959D
		public void Deserialize(Stream stream)
		{
			RecentPlayersRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0009B3A7 File Offset: 0x000995A7
		public static RecentPlayersRemovedNotification Deserialize(Stream stream, RecentPlayersRemovedNotification instance)
		{
			return RecentPlayersRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x0009B3B4 File Offset: 0x000995B4
		public static RecentPlayersRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			RecentPlayersRemovedNotification recentPlayersRemovedNotification = new RecentPlayersRemovedNotification();
			RecentPlayersRemovedNotification.DeserializeLengthDelimited(stream, recentPlayersRemovedNotification);
			return recentPlayersRemovedNotification;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x0009B3D0 File Offset: 0x000995D0
		public static RecentPlayersRemovedNotification DeserializeLengthDelimited(Stream stream, RecentPlayersRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecentPlayersRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x0009B3F8 File Offset: 0x000995F8
		public static RecentPlayersRemovedNotification Deserialize(Stream stream, RecentPlayersRemovedNotification instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<RecentPlayer>();
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
				else if (num == 10)
				{
					instance.Player.Add(RecentPlayer.DeserializeLengthDelimited(stream));
				}
				else
				{
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

		// Token: 0x06002D5A RID: 11610 RVA: 0x0009B490 File Offset: 0x00099690
		public void Serialize(Stream stream)
		{
			RecentPlayersRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0009B49C File Offset: 0x0009969C
		public static void Serialize(Stream stream, RecentPlayersRemovedNotification instance)
		{
			if (instance.Player.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in instance.Player)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, recentPlayer.GetSerializedSize());
					RecentPlayer.Serialize(stream, recentPlayer);
				}
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0009B514 File Offset: 0x00099714
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Player.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in this.Player)
				{
					num += 1U;
					uint serializedSize = recentPlayer.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001288 RID: 4744
		private List<RecentPlayer> _Player = new List<RecentPlayer>();
	}
}
