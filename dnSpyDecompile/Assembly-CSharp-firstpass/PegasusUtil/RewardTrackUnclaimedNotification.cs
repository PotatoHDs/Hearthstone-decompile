using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010E RID: 270
	public class RewardTrackUnclaimedNotification : IProtoBuf
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0003EC03 File Offset: 0x0003CE03
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x0003EC0B File Offset: 0x0003CE0B
		public List<RewardTrackUnclaimedRewards> Notif
		{
			get
			{
				return this._Notif;
			}
			set
			{
				this._Notif = value;
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003EC14 File Offset: 0x0003CE14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards in this.Notif)
			{
				num ^= rewardTrackUnclaimedRewards.GetHashCode();
			}
			return num;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003EC78 File Offset: 0x0003CE78
		public override bool Equals(object obj)
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = obj as RewardTrackUnclaimedNotification;
			if (rewardTrackUnclaimedNotification == null)
			{
				return false;
			}
			if (this.Notif.Count != rewardTrackUnclaimedNotification.Notif.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Notif.Count; i++)
			{
				if (!this.Notif[i].Equals(rewardTrackUnclaimedNotification.Notif[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003ECE3 File Offset: 0x0003CEE3
		public void Deserialize(Stream stream)
		{
			RewardTrackUnclaimedNotification.Deserialize(stream, this);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003ECED File Offset: 0x0003CEED
		public static RewardTrackUnclaimedNotification Deserialize(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			return RewardTrackUnclaimedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		public static RewardTrackUnclaimedNotification DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = new RewardTrackUnclaimedNotification();
			RewardTrackUnclaimedNotification.DeserializeLengthDelimited(stream, rewardTrackUnclaimedNotification);
			return rewardTrackUnclaimedNotification;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003ED14 File Offset: 0x0003CF14
		public static RewardTrackUnclaimedNotification DeserializeLengthDelimited(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardTrackUnclaimedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		public static RewardTrackUnclaimedNotification Deserialize(Stream stream, RewardTrackUnclaimedNotification instance, long limit)
		{
			if (instance.Notif == null)
			{
				instance.Notif = new List<RewardTrackUnclaimedRewards>();
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
					instance.Notif.Add(RewardTrackUnclaimedRewards.DeserializeLengthDelimited(stream));
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

		// Token: 0x060011DD RID: 4573 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
		public void Serialize(Stream stream)
		{
			RewardTrackUnclaimedNotification.Serialize(stream, this);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003EDE0 File Offset: 0x0003CFE0
		public static void Serialize(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			if (instance.Notif.Count > 0)
			{
				foreach (RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards in instance.Notif)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, rewardTrackUnclaimedRewards.GetSerializedSize());
					RewardTrackUnclaimedRewards.Serialize(stream, rewardTrackUnclaimedRewards);
				}
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003EE58 File Offset: 0x0003D058
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Notif.Count > 0)
			{
				foreach (RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards in this.Notif)
				{
					num += 1U;
					uint serializedSize = rewardTrackUnclaimedRewards.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400057A RID: 1402
		private List<RewardTrackUnclaimedRewards> _Notif = new List<RewardTrackUnclaimedRewards>();

		// Token: 0x02000610 RID: 1552
		public enum PacketID
		{
			// Token: 0x0400206C RID: 8300
			ID = 619,
			// Token: 0x0400206D RID: 8301
			System = 0
		}
	}
}
