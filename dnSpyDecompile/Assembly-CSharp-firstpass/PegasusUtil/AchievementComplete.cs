using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010D RID: 269
	public class AchievementComplete : IProtoBuf
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0003E887 File Offset: 0x0003CA87
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0003E88F File Offset: 0x0003CA8F
		public int AchievementId
		{
			get
			{
				return this._AchievementId;
			}
			set
			{
				this._AchievementId = value;
				this.HasAchievementId = true;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003E89F File Offset: 0x0003CA9F
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0003E8A7 File Offset: 0x0003CAA7
		public List<int> AchievementIds
		{
			get
			{
				return this._AchievementIds;
			}
			set
			{
				this._AchievementIds = value;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementId)
			{
				num ^= this.AchievementId.GetHashCode();
			}
			foreach (int num2 in this.AchievementIds)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public override bool Equals(object obj)
		{
			AchievementComplete achievementComplete = obj as AchievementComplete;
			if (achievementComplete == null)
			{
				return false;
			}
			if (this.HasAchievementId != achievementComplete.HasAchievementId || (this.HasAchievementId && !this.AchievementId.Equals(achievementComplete.AchievementId)))
			{
				return false;
			}
			if (this.AchievementIds.Count != achievementComplete.AchievementIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AchievementIds.Count; i++)
			{
				if (!this.AchievementIds[i].Equals(achievementComplete.AchievementIds[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003E9C8 File Offset: 0x0003CBC8
		public void Deserialize(Stream stream)
		{
			AchievementComplete.Deserialize(stream, this);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003E9D2 File Offset: 0x0003CBD2
		public static AchievementComplete Deserialize(Stream stream, AchievementComplete instance)
		{
			return AchievementComplete.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003E9E0 File Offset: 0x0003CBE0
		public static AchievementComplete DeserializeLengthDelimited(Stream stream)
		{
			AchievementComplete achievementComplete = new AchievementComplete();
			AchievementComplete.DeserializeLengthDelimited(stream, achievementComplete);
			return achievementComplete;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003E9FC File Offset: 0x0003CBFC
		public static AchievementComplete DeserializeLengthDelimited(Stream stream, AchievementComplete instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AchievementComplete.Deserialize(stream, instance, num);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003EA24 File Offset: 0x0003CC24
		public static AchievementComplete Deserialize(Stream stream, AchievementComplete instance, long limit)
		{
			if (instance.AchievementIds == null)
			{
				instance.AchievementIds = new List<int>();
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.AchievementIds.Add((int)ProtocolParser.ReadUInt64(stream));
					}
				}
				else
				{
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003EAD5 File Offset: 0x0003CCD5
		public void Serialize(Stream stream)
		{
			AchievementComplete.Serialize(stream, this);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003EAE0 File Offset: 0x0003CCE0
		public static void Serialize(Stream stream, AchievementComplete instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchievementId));
			}
			if (instance.AchievementIds.Count > 0)
			{
				foreach (int num in instance.AchievementIds)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003EB68 File Offset: 0x0003CD68
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchievementId));
			}
			if (this.AchievementIds.Count > 0)
			{
				foreach (int num2 in this.AchievementIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			return num;
		}

		// Token: 0x04000577 RID: 1399
		public bool HasAchievementId;

		// Token: 0x04000578 RID: 1400
		private int _AchievementId;

		// Token: 0x04000579 RID: 1401
		private List<int> _AchievementIds = new List<int>();

		// Token: 0x0200060F RID: 1551
		public enum PacketID
		{
			// Token: 0x04002069 RID: 8297
			ID = 618,
			// Token: 0x0400206A RID: 8298
			System = 0
		}
	}
}
