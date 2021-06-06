using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B5 RID: 181
	public class HeroXP : IProtoBuf
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002F266 File Offset: 0x0002D466
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0002F26E File Offset: 0x0002D46E
		public List<HeroXPInfo> XpInfos
		{
			get
			{
				return this._XpInfos;
			}
			set
			{
				this._XpInfos = value;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002F278 File Offset: 0x0002D478
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (HeroXPInfo heroXPInfo in this.XpInfos)
			{
				num ^= heroXPInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public override bool Equals(object obj)
		{
			HeroXP heroXP = obj as HeroXP;
			if (heroXP == null)
			{
				return false;
			}
			if (this.XpInfos.Count != heroXP.XpInfos.Count)
			{
				return false;
			}
			for (int i = 0; i < this.XpInfos.Count; i++)
			{
				if (!this.XpInfos[i].Equals(heroXP.XpInfos[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002F347 File Offset: 0x0002D547
		public void Deserialize(Stream stream)
		{
			HeroXP.Deserialize(stream, this);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002F351 File Offset: 0x0002D551
		public static HeroXP Deserialize(Stream stream, HeroXP instance)
		{
			return HeroXP.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002F35C File Offset: 0x0002D55C
		public static HeroXP DeserializeLengthDelimited(Stream stream)
		{
			HeroXP heroXP = new HeroXP();
			HeroXP.DeserializeLengthDelimited(stream, heroXP);
			return heroXP;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002F378 File Offset: 0x0002D578
		public static HeroXP DeserializeLengthDelimited(Stream stream, HeroXP instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HeroXP.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002F3A0 File Offset: 0x0002D5A0
		public static HeroXP Deserialize(Stream stream, HeroXP instance, long limit)
		{
			if (instance.XpInfos == null)
			{
				instance.XpInfos = new List<HeroXPInfo>();
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
					instance.XpInfos.Add(HeroXPInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002F438 File Offset: 0x0002D638
		public void Serialize(Stream stream)
		{
			HeroXP.Serialize(stream, this);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002F444 File Offset: 0x0002D644
		public static void Serialize(Stream stream, HeroXP instance)
		{
			if (instance.XpInfos.Count > 0)
			{
				foreach (HeroXPInfo heroXPInfo in instance.XpInfos)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, heroXPInfo.GetSerializedSize());
					HeroXPInfo.Serialize(stream, heroXPInfo);
				}
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.XpInfos.Count > 0)
			{
				foreach (HeroXPInfo heroXPInfo in this.XpInfos)
				{
					num += 1U;
					uint serializedSize = heroXPInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000465 RID: 1125
		private List<HeroXPInfo> _XpInfos = new List<HeroXPInfo>();

		// Token: 0x020005C0 RID: 1472
		public enum PacketID
		{
			// Token: 0x04001F8F RID: 8079
			ID = 283
		}
	}
}
