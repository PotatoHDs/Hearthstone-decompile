using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000A9 RID: 169
	public class MedalInfo : IProtoBuf
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002B382 File Offset: 0x00029582
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0002B38A File Offset: 0x0002958A
		public List<MedalInfoData> MedalData
		{
			get
			{
				return this._MedalData;
			}
			set
			{
				this._MedalData = value;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002B394 File Offset: 0x00029594
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (MedalInfoData medalInfoData in this.MedalData)
			{
				num ^= medalInfoData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002B3F8 File Offset: 0x000295F8
		public override bool Equals(object obj)
		{
			MedalInfo medalInfo = obj as MedalInfo;
			if (medalInfo == null)
			{
				return false;
			}
			if (this.MedalData.Count != medalInfo.MedalData.Count)
			{
				return false;
			}
			for (int i = 0; i < this.MedalData.Count; i++)
			{
				if (!this.MedalData[i].Equals(medalInfo.MedalData[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002B463 File Offset: 0x00029663
		public void Deserialize(Stream stream)
		{
			MedalInfo.Deserialize(stream, this);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002B46D File Offset: 0x0002966D
		public static MedalInfo Deserialize(Stream stream, MedalInfo instance)
		{
			return MedalInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002B478 File Offset: 0x00029678
		public static MedalInfo DeserializeLengthDelimited(Stream stream)
		{
			MedalInfo medalInfo = new MedalInfo();
			MedalInfo.DeserializeLengthDelimited(stream, medalInfo);
			return medalInfo;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002B494 File Offset: 0x00029694
		public static MedalInfo DeserializeLengthDelimited(Stream stream, MedalInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MedalInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002B4BC File Offset: 0x000296BC
		public static MedalInfo Deserialize(Stream stream, MedalInfo instance, long limit)
		{
			if (instance.MedalData == null)
			{
				instance.MedalData = new List<MedalInfoData>();
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
				else if (num == 26)
				{
					instance.MedalData.Add(MedalInfoData.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002B554 File Offset: 0x00029754
		public void Serialize(Stream stream)
		{
			MedalInfo.Serialize(stream, this);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002B560 File Offset: 0x00029760
		public static void Serialize(Stream stream, MedalInfo instance)
		{
			if (instance.MedalData.Count > 0)
			{
				foreach (MedalInfoData medalInfoData in instance.MedalData)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, medalInfoData.GetSerializedSize());
					MedalInfoData.Serialize(stream, medalInfoData);
				}
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002B5D8 File Offset: 0x000297D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.MedalData.Count > 0)
			{
				foreach (MedalInfoData medalInfoData in this.MedalData)
				{
					num += 1U;
					uint serializedSize = medalInfoData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040003FC RID: 1020
		private List<MedalInfoData> _MedalData = new List<MedalInfoData>();

		// Token: 0x020005B1 RID: 1457
		public enum PacketID
		{
			// Token: 0x04001F68 RID: 8040
			ID = 232
		}
	}
}
