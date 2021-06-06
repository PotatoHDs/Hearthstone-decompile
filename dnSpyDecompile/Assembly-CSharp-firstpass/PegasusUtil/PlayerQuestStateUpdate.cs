using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000FC RID: 252
	public class PlayerQuestStateUpdate : IProtoBuf
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0003AEDF File Offset: 0x000390DF
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x0003AEE7 File Offset: 0x000390E7
		public List<PlayerQuestState> Quest
		{
			get
			{
				return this._Quest;
			}
			set
			{
				this._Quest = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0003AEF0 File Offset: 0x000390F0
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x0003AEF8 File Offset: 0x000390F8
		public List<int> ShowQuestNotificationForPoolType
		{
			get
			{
				return this._ShowQuestNotificationForPoolType;
			}
			set
			{
				this._ShowQuestNotificationForPoolType = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003AF01 File Offset: 0x00039101
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x0003AF09 File Offset: 0x00039109
		public List<long> GrantDate
		{
			get
			{
				return this._GrantDate;
			}
			set
			{
				this._GrantDate = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003AF12 File Offset: 0x00039112
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x0003AF1A File Offset: 0x0003911A
		public bool DeprecatedField2
		{
			get
			{
				return this._DeprecatedField2;
			}
			set
			{
				this._DeprecatedField2 = value;
				this.HasDeprecatedField2 = true;
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003AF2C File Offset: 0x0003912C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerQuestState playerQuestState in this.Quest)
			{
				num ^= playerQuestState.GetHashCode();
			}
			foreach (int num2 in this.ShowQuestNotificationForPoolType)
			{
				num ^= num2.GetHashCode();
			}
			foreach (long num3 in this.GrantDate)
			{
				num ^= num3.GetHashCode();
			}
			if (this.HasDeprecatedField2)
			{
				num ^= this.DeprecatedField2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003B038 File Offset: 0x00039238
		public override bool Equals(object obj)
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = obj as PlayerQuestStateUpdate;
			if (playerQuestStateUpdate == null)
			{
				return false;
			}
			if (this.Quest.Count != playerQuestStateUpdate.Quest.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Quest.Count; i++)
			{
				if (!this.Quest[i].Equals(playerQuestStateUpdate.Quest[i]))
				{
					return false;
				}
			}
			if (this.ShowQuestNotificationForPoolType.Count != playerQuestStateUpdate.ShowQuestNotificationForPoolType.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ShowQuestNotificationForPoolType.Count; j++)
			{
				if (!this.ShowQuestNotificationForPoolType[j].Equals(playerQuestStateUpdate.ShowQuestNotificationForPoolType[j]))
				{
					return false;
				}
			}
			if (this.GrantDate.Count != playerQuestStateUpdate.GrantDate.Count)
			{
				return false;
			}
			for (int k = 0; k < this.GrantDate.Count; k++)
			{
				if (!this.GrantDate[k].Equals(playerQuestStateUpdate.GrantDate[k]))
				{
					return false;
				}
			}
			return this.HasDeprecatedField2 == playerQuestStateUpdate.HasDeprecatedField2 && (!this.HasDeprecatedField2 || this.DeprecatedField2.Equals(playerQuestStateUpdate.DeprecatedField2));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003B181 File Offset: 0x00039381
		public void Deserialize(Stream stream)
		{
			PlayerQuestStateUpdate.Deserialize(stream, this);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003B18B File Offset: 0x0003938B
		public static PlayerQuestStateUpdate Deserialize(Stream stream, PlayerQuestStateUpdate instance)
		{
			return PlayerQuestStateUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003B198 File Offset: 0x00039398
		public static PlayerQuestStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = new PlayerQuestStateUpdate();
			PlayerQuestStateUpdate.DeserializeLengthDelimited(stream, playerQuestStateUpdate);
			return playerQuestStateUpdate;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003B1B4 File Offset: 0x000393B4
		public static PlayerQuestStateUpdate DeserializeLengthDelimited(Stream stream, PlayerQuestStateUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerQuestStateUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003B1DC File Offset: 0x000393DC
		public static PlayerQuestStateUpdate Deserialize(Stream stream, PlayerQuestStateUpdate instance, long limit)
		{
			if (instance.Quest == null)
			{
				instance.Quest = new List<PlayerQuestState>();
			}
			if (instance.ShowQuestNotificationForPoolType == null)
			{
				instance.ShowQuestNotificationForPoolType = new List<int>();
			}
			if (instance.GrantDate == null)
			{
				instance.GrantDate = new List<long>();
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
						if (num == 10)
						{
							instance.Quest.Add(PlayerQuestState.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 16)
						{
							instance.DeprecatedField2 = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ShowQuestNotificationForPoolType.Add((int)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 32)
						{
							instance.GrantDate.Add((long)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003B2F9 File Offset: 0x000394F9
		public void Serialize(Stream stream)
		{
			PlayerQuestStateUpdate.Serialize(stream, this);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003B304 File Offset: 0x00039504
		public static void Serialize(Stream stream, PlayerQuestStateUpdate instance)
		{
			if (instance.Quest.Count > 0)
			{
				foreach (PlayerQuestState playerQuestState in instance.Quest)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerQuestState.GetSerializedSize());
					PlayerQuestState.Serialize(stream, playerQuestState);
				}
			}
			if (instance.ShowQuestNotificationForPoolType.Count > 0)
			{
				foreach (int num in instance.ShowQuestNotificationForPoolType)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			if (instance.GrantDate.Count > 0)
			{
				foreach (long val in instance.GrantDate)
				{
					stream.WriteByte(32);
					ProtocolParser.WriteUInt64(stream, (ulong)val);
				}
			}
			if (instance.HasDeprecatedField2)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.DeprecatedField2);
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003B44C File Offset: 0x0003964C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Quest.Count > 0)
			{
				foreach (PlayerQuestState playerQuestState in this.Quest)
				{
					num += 1U;
					uint serializedSize = playerQuestState.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ShowQuestNotificationForPoolType.Count > 0)
			{
				foreach (int num2 in this.ShowQuestNotificationForPoolType)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			if (this.GrantDate.Count > 0)
			{
				foreach (long val in this.GrantDate)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)val);
				}
			}
			if (this.HasDeprecatedField2)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000530 RID: 1328
		private List<PlayerQuestState> _Quest = new List<PlayerQuestState>();

		// Token: 0x04000531 RID: 1329
		private List<int> _ShowQuestNotificationForPoolType = new List<int>();

		// Token: 0x04000532 RID: 1330
		private List<long> _GrantDate = new List<long>();

		// Token: 0x04000533 RID: 1331
		public bool HasDeprecatedField2;

		// Token: 0x04000534 RID: 1332
		private bool _DeprecatedField2;

		// Token: 0x020005FE RID: 1534
		public enum PacketID
		{
			// Token: 0x04002037 RID: 8247
			ID = 601,
			// Token: 0x04002038 RID: 8248
			System = 0
		}
	}
}
