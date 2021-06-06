using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000FD RID: 253
	public class PlayerQuestPoolStateUpdate : IProtoBuf
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0003B5AD File Offset: 0x000397AD
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0003B5B5 File Offset: 0x000397B5
		public List<PlayerQuestPoolState> QuestPool
		{
			get
			{
				return this._QuestPool;
			}
			set
			{
				this._QuestPool = value;
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003B5C0 File Offset: 0x000397C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerQuestPoolState playerQuestPoolState in this.QuestPool)
			{
				num ^= playerQuestPoolState.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003B624 File Offset: 0x00039824
		public override bool Equals(object obj)
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = obj as PlayerQuestPoolStateUpdate;
			if (playerQuestPoolStateUpdate == null)
			{
				return false;
			}
			if (this.QuestPool.Count != playerQuestPoolStateUpdate.QuestPool.Count)
			{
				return false;
			}
			for (int i = 0; i < this.QuestPool.Count; i++)
			{
				if (!this.QuestPool[i].Equals(playerQuestPoolStateUpdate.QuestPool[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003B68F File Offset: 0x0003988F
		public void Deserialize(Stream stream)
		{
			PlayerQuestPoolStateUpdate.Deserialize(stream, this);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003B699 File Offset: 0x00039899
		public static PlayerQuestPoolStateUpdate Deserialize(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			return PlayerQuestPoolStateUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003B6A4 File Offset: 0x000398A4
		public static PlayerQuestPoolStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = new PlayerQuestPoolStateUpdate();
			PlayerQuestPoolStateUpdate.DeserializeLengthDelimited(stream, playerQuestPoolStateUpdate);
			return playerQuestPoolStateUpdate;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003B6C0 File Offset: 0x000398C0
		public static PlayerQuestPoolStateUpdate DeserializeLengthDelimited(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerQuestPoolStateUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003B6E8 File Offset: 0x000398E8
		public static PlayerQuestPoolStateUpdate Deserialize(Stream stream, PlayerQuestPoolStateUpdate instance, long limit)
		{
			if (instance.QuestPool == null)
			{
				instance.QuestPool = new List<PlayerQuestPoolState>();
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
					instance.QuestPool.Add(PlayerQuestPoolState.DeserializeLengthDelimited(stream));
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

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003B780 File Offset: 0x00039980
		public void Serialize(Stream stream)
		{
			PlayerQuestPoolStateUpdate.Serialize(stream, this);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003B78C File Offset: 0x0003998C
		public static void Serialize(Stream stream, PlayerQuestPoolStateUpdate instance)
		{
			if (instance.QuestPool.Count > 0)
			{
				foreach (PlayerQuestPoolState playerQuestPoolState in instance.QuestPool)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerQuestPoolState.GetSerializedSize());
					PlayerQuestPoolState.Serialize(stream, playerQuestPoolState);
				}
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003B804 File Offset: 0x00039A04
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.QuestPool.Count > 0)
			{
				foreach (PlayerQuestPoolState playerQuestPoolState in this.QuestPool)
				{
					num += 1U;
					uint serializedSize = playerQuestPoolState.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000535 RID: 1333
		private List<PlayerQuestPoolState> _QuestPool = new List<PlayerQuestPoolState>();

		// Token: 0x020005FF RID: 1535
		public enum PacketID
		{
			// Token: 0x0400203A RID: 8250
			ID = 602,
			// Token: 0x0400203B RID: 8251
			System = 0
		}
	}
}
