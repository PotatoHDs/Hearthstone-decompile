using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000FE RID: 254
	public class PlayerAchievementStateUpdate : IProtoBuf
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0003B88B File Offset: 0x00039A8B
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x0003B893 File Offset: 0x00039A93
		public List<PlayerAchievementState> Achievement
		{
			get
			{
				return this._Achievement;
			}
			set
			{
				this._Achievement = value;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003B89C File Offset: 0x00039A9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerAchievementState playerAchievementState in this.Achievement)
			{
				num ^= playerAchievementState.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003B900 File Offset: 0x00039B00
		public override bool Equals(object obj)
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = obj as PlayerAchievementStateUpdate;
			if (playerAchievementStateUpdate == null)
			{
				return false;
			}
			if (this.Achievement.Count != playerAchievementStateUpdate.Achievement.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Achievement.Count; i++)
			{
				if (!this.Achievement[i].Equals(playerAchievementStateUpdate.Achievement[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003B96B File Offset: 0x00039B6B
		public void Deserialize(Stream stream)
		{
			PlayerAchievementStateUpdate.Deserialize(stream, this);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003B975 File Offset: 0x00039B75
		public static PlayerAchievementStateUpdate Deserialize(Stream stream, PlayerAchievementStateUpdate instance)
		{
			return PlayerAchievementStateUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003B980 File Offset: 0x00039B80
		public static PlayerAchievementStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = new PlayerAchievementStateUpdate();
			PlayerAchievementStateUpdate.DeserializeLengthDelimited(stream, playerAchievementStateUpdate);
			return playerAchievementStateUpdate;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003B99C File Offset: 0x00039B9C
		public static PlayerAchievementStateUpdate DeserializeLengthDelimited(Stream stream, PlayerAchievementStateUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerAchievementStateUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		public static PlayerAchievementStateUpdate Deserialize(Stream stream, PlayerAchievementStateUpdate instance, long limit)
		{
			if (instance.Achievement == null)
			{
				instance.Achievement = new List<PlayerAchievementState>();
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
					instance.Achievement.Add(PlayerAchievementState.DeserializeLengthDelimited(stream));
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

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003BA5C File Offset: 0x00039C5C
		public void Serialize(Stream stream)
		{
			PlayerAchievementStateUpdate.Serialize(stream, this);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003BA68 File Offset: 0x00039C68
		public static void Serialize(Stream stream, PlayerAchievementStateUpdate instance)
		{
			if (instance.Achievement.Count > 0)
			{
				foreach (PlayerAchievementState playerAchievementState in instance.Achievement)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerAchievementState.GetSerializedSize());
					PlayerAchievementState.Serialize(stream, playerAchievementState);
				}
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003BAE0 File Offset: 0x00039CE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Achievement.Count > 0)
			{
				foreach (PlayerAchievementState playerAchievementState in this.Achievement)
				{
					num += 1U;
					uint serializedSize = playerAchievementState.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000536 RID: 1334
		private List<PlayerAchievementState> _Achievement = new List<PlayerAchievementState>();

		// Token: 0x02000600 RID: 1536
		public enum PacketID
		{
			// Token: 0x0400203D RID: 8253
			ID = 603,
			// Token: 0x0400203E RID: 8254
			System = 0
		}
	}
}
