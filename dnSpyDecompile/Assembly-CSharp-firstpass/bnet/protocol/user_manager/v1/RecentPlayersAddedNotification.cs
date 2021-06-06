using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F5 RID: 757
	public class RecentPlayersAddedNotification : IProtoBuf
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x0009AF65 File Offset: 0x00099165
		// (set) Token: 0x06002D37 RID: 11575 RVA: 0x0009AF6D File Offset: 0x0009916D
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

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0009AF65 File Offset: 0x00099165
		public List<RecentPlayer> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x0009AF76 File Offset: 0x00099176
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0009AF83 File Offset: 0x00099183
		public void AddPlayer(RecentPlayer val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0009AF91 File Offset: 0x00099191
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0009AF9E File Offset: 0x0009919E
		public void SetPlayer(List<RecentPlayer> val)
		{
			this.Player = val;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0009AFA8 File Offset: 0x000991A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RecentPlayer recentPlayer in this.Player)
			{
				num ^= recentPlayer.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0009B00C File Offset: 0x0009920C
		public override bool Equals(object obj)
		{
			RecentPlayersAddedNotification recentPlayersAddedNotification = obj as RecentPlayersAddedNotification;
			if (recentPlayersAddedNotification == null)
			{
				return false;
			}
			if (this.Player.Count != recentPlayersAddedNotification.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(recentPlayersAddedNotification.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0009B077 File Offset: 0x00099277
		public static RecentPlayersAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RecentPlayersAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0009B081 File Offset: 0x00099281
		public void Deserialize(Stream stream)
		{
			RecentPlayersAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0009B08B File Offset: 0x0009928B
		public static RecentPlayersAddedNotification Deserialize(Stream stream, RecentPlayersAddedNotification instance)
		{
			return RecentPlayersAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0009B098 File Offset: 0x00099298
		public static RecentPlayersAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			RecentPlayersAddedNotification recentPlayersAddedNotification = new RecentPlayersAddedNotification();
			RecentPlayersAddedNotification.DeserializeLengthDelimited(stream, recentPlayersAddedNotification);
			return recentPlayersAddedNotification;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0009B0B4 File Offset: 0x000992B4
		public static RecentPlayersAddedNotification DeserializeLengthDelimited(Stream stream, RecentPlayersAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecentPlayersAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0009B0DC File Offset: 0x000992DC
		public static RecentPlayersAddedNotification Deserialize(Stream stream, RecentPlayersAddedNotification instance, long limit)
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

		// Token: 0x06002D46 RID: 11590 RVA: 0x0009B174 File Offset: 0x00099374
		public void Serialize(Stream stream)
		{
			RecentPlayersAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0009B180 File Offset: 0x00099380
		public static void Serialize(Stream stream, RecentPlayersAddedNotification instance)
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

		// Token: 0x06002D48 RID: 11592 RVA: 0x0009B1F8 File Offset: 0x000993F8
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

		// Token: 0x04001287 RID: 4743
		private List<RecentPlayer> _Player = new List<RecentPlayer>();
	}
}
