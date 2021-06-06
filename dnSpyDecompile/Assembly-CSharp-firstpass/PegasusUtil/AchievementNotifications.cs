using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D1 RID: 209
	public class AchievementNotifications : IProtoBuf
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003446F File Offset: 0x0003266F
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00034477 File Offset: 0x00032677
		public List<AchievementNotification> AchievementNotifications_
		{
			get
			{
				return this._AchievementNotifications_;
			}
			set
			{
				this._AchievementNotifications_ = value;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00034480 File Offset: 0x00032680
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AchievementNotification achievementNotification in this.AchievementNotifications_)
			{
				num ^= achievementNotification.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000344E4 File Offset: 0x000326E4
		public override bool Equals(object obj)
		{
			AchievementNotifications achievementNotifications = obj as AchievementNotifications;
			if (achievementNotifications == null)
			{
				return false;
			}
			if (this.AchievementNotifications_.Count != achievementNotifications.AchievementNotifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AchievementNotifications_.Count; i++)
			{
				if (!this.AchievementNotifications_[i].Equals(achievementNotifications.AchievementNotifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003454F File Offset: 0x0003274F
		public void Deserialize(Stream stream)
		{
			AchievementNotifications.Deserialize(stream, this);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00034559 File Offset: 0x00032759
		public static AchievementNotifications Deserialize(Stream stream, AchievementNotifications instance)
		{
			return AchievementNotifications.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00034564 File Offset: 0x00032764
		public static AchievementNotifications DeserializeLengthDelimited(Stream stream)
		{
			AchievementNotifications achievementNotifications = new AchievementNotifications();
			AchievementNotifications.DeserializeLengthDelimited(stream, achievementNotifications);
			return achievementNotifications;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00034580 File Offset: 0x00032780
		public static AchievementNotifications DeserializeLengthDelimited(Stream stream, AchievementNotifications instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AchievementNotifications.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000345A8 File Offset: 0x000327A8
		public static AchievementNotifications Deserialize(Stream stream, AchievementNotifications instance, long limit)
		{
			if (instance.AchievementNotifications_ == null)
			{
				instance.AchievementNotifications_ = new List<AchievementNotification>();
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
					instance.AchievementNotifications_.Add(AchievementNotification.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000E54 RID: 3668 RVA: 0x00034640 File Offset: 0x00032840
		public void Serialize(Stream stream)
		{
			AchievementNotifications.Serialize(stream, this);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003464C File Offset: 0x0003284C
		public static void Serialize(Stream stream, AchievementNotifications instance)
		{
			if (instance.AchievementNotifications_.Count > 0)
			{
				foreach (AchievementNotification achievementNotification in instance.AchievementNotifications_)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, achievementNotification.GetSerializedSize());
					AchievementNotification.Serialize(stream, achievementNotification);
				}
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000346C4 File Offset: 0x000328C4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.AchievementNotifications_.Count > 0)
			{
				foreach (AchievementNotification achievementNotification in this.AchievementNotifications_)
				{
					num += 1U;
					uint serializedSize = achievementNotification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004C7 RID: 1223
		private List<AchievementNotification> _AchievementNotifications_ = new List<AchievementNotification>();
	}
}
