using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001BD RID: 445
	public class PowerHistoryCreateGame : IProtoBuf
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00063746 File Offset: 0x00061946
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x0006374E File Offset: 0x0006194E
		public Entity GameEntity { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00063757 File Offset: 0x00061957
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0006375F File Offset: 0x0006195F
		public List<Player> Players
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x00063768 File Offset: 0x00061968
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x00063770 File Offset: 0x00061970
		public List<SharedPlayerInfo> PlayerInfos
		{
			get
			{
				return this._PlayerInfos;
			}
			set
			{
				this._PlayerInfos = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00063779 File Offset: 0x00061979
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x00063781 File Offset: 0x00061981
		public string GameUuid
		{
			get
			{
				return this._GameUuid;
			}
			set
			{
				this._GameUuid = value;
				this.HasGameUuid = (value != null);
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00063794 File Offset: 0x00061994
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameEntity.GetHashCode();
			foreach (Player player in this.Players)
			{
				num ^= player.GetHashCode();
			}
			foreach (SharedPlayerInfo sharedPlayerInfo in this.PlayerInfos)
			{
				num ^= sharedPlayerInfo.GetHashCode();
			}
			if (this.HasGameUuid)
			{
				num ^= this.GameUuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00063860 File Offset: 0x00061A60
		public override bool Equals(object obj)
		{
			PowerHistoryCreateGame powerHistoryCreateGame = obj as PowerHistoryCreateGame;
			if (powerHistoryCreateGame == null)
			{
				return false;
			}
			if (!this.GameEntity.Equals(powerHistoryCreateGame.GameEntity))
			{
				return false;
			}
			if (this.Players.Count != powerHistoryCreateGame.Players.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Players.Count; i++)
			{
				if (!this.Players[i].Equals(powerHistoryCreateGame.Players[i]))
				{
					return false;
				}
			}
			if (this.PlayerInfos.Count != powerHistoryCreateGame.PlayerInfos.Count)
			{
				return false;
			}
			for (int j = 0; j < this.PlayerInfos.Count; j++)
			{
				if (!this.PlayerInfos[j].Equals(powerHistoryCreateGame.PlayerInfos[j]))
				{
					return false;
				}
			}
			return this.HasGameUuid == powerHistoryCreateGame.HasGameUuid && (!this.HasGameUuid || this.GameUuid.Equals(powerHistoryCreateGame.GameUuid));
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0006395C File Offset: 0x00061B5C
		public void Deserialize(Stream stream)
		{
			PowerHistoryCreateGame.Deserialize(stream, this);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00063966 File Offset: 0x00061B66
		public static PowerHistoryCreateGame Deserialize(Stream stream, PowerHistoryCreateGame instance)
		{
			return PowerHistoryCreateGame.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00063974 File Offset: 0x00061B74
		public static PowerHistoryCreateGame DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryCreateGame powerHistoryCreateGame = new PowerHistoryCreateGame();
			PowerHistoryCreateGame.DeserializeLengthDelimited(stream, powerHistoryCreateGame);
			return powerHistoryCreateGame;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00063990 File Offset: 0x00061B90
		public static PowerHistoryCreateGame DeserializeLengthDelimited(Stream stream, PowerHistoryCreateGame instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryCreateGame.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000639B8 File Offset: 0x00061BB8
		public static PowerHistoryCreateGame Deserialize(Stream stream, PowerHistoryCreateGame instance, long limit)
		{
			if (instance.Players == null)
			{
				instance.Players = new List<Player>();
			}
			if (instance.PlayerInfos == null)
			{
				instance.PlayerInfos = new List<SharedPlayerInfo>();
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
								instance.Players.Add(Player.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.GameEntity == null)
							{
								instance.GameEntity = Entity.DeserializeLengthDelimited(stream);
								continue;
							}
							Entity.DeserializeLengthDelimited(stream, instance.GameEntity);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.PlayerInfos.Add(SharedPlayerInfo.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							instance.GameUuid = ProtocolParser.ReadString(stream);
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

		// Token: 0x06001C4D RID: 7245 RVA: 0x00063AD3 File Offset: 0x00061CD3
		public void Serialize(Stream stream)
		{
			PowerHistoryCreateGame.Serialize(stream, this);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00063ADC File Offset: 0x00061CDC
		public static void Serialize(Stream stream, PowerHistoryCreateGame instance)
		{
			if (instance.GameEntity == null)
			{
				throw new ArgumentNullException("GameEntity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameEntity.GetSerializedSize());
			Entity.Serialize(stream, instance.GameEntity);
			if (instance.Players.Count > 0)
			{
				foreach (Player player in instance.Players)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					Player.Serialize(stream, player);
				}
			}
			if (instance.PlayerInfos.Count > 0)
			{
				foreach (SharedPlayerInfo sharedPlayerInfo in instance.PlayerInfos)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, sharedPlayerInfo.GetSerializedSize());
					SharedPlayerInfo.Serialize(stream, sharedPlayerInfo);
				}
			}
			if (instance.HasGameUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameUuid));
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00063C18 File Offset: 0x00061E18
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameEntity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Players.Count > 0)
			{
				foreach (Player player in this.Players)
				{
					num += 1U;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.PlayerInfos.Count > 0)
			{
				foreach (SharedPlayerInfo sharedPlayerInfo in this.PlayerInfos)
				{
					num += 1U;
					uint serializedSize3 = sharedPlayerInfo.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasGameUuid)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.GameUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000A52 RID: 2642
		private List<Player> _Players = new List<Player>();

		// Token: 0x04000A53 RID: 2643
		private List<SharedPlayerInfo> _PlayerInfos = new List<SharedPlayerInfo>();

		// Token: 0x04000A54 RID: 2644
		public bool HasGameUuid;

		// Token: 0x04000A55 RID: 2645
		private string _GameUuid;
	}
}
