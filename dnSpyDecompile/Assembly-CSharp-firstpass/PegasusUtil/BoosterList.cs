using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A6 RID: 166
	public class BoosterList : IProtoBuf
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00029A8A File Offset: 0x00027C8A
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00029A92 File Offset: 0x00027C92
		public List<BoosterInfo> List
		{
			get
			{
				return this._List;
			}
			set
			{
				this._List = value;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00029A9C File Offset: 0x00027C9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BoosterInfo boosterInfo in this.List)
			{
				num ^= boosterInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00029B00 File Offset: 0x00027D00
		public override bool Equals(object obj)
		{
			BoosterList boosterList = obj as BoosterList;
			if (boosterList == null)
			{
				return false;
			}
			if (this.List.Count != boosterList.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(boosterList.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00029B6B File Offset: 0x00027D6B
		public void Deserialize(Stream stream)
		{
			BoosterList.Deserialize(stream, this);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00029B75 File Offset: 0x00027D75
		public static BoosterList Deserialize(Stream stream, BoosterList instance)
		{
			return BoosterList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00029B80 File Offset: 0x00027D80
		public static BoosterList DeserializeLengthDelimited(Stream stream)
		{
			BoosterList boosterList = new BoosterList();
			BoosterList.DeserializeLengthDelimited(stream, boosterList);
			return boosterList;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00029B9C File Offset: 0x00027D9C
		public static BoosterList DeserializeLengthDelimited(Stream stream, BoosterList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoosterList.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00029BC4 File Offset: 0x00027DC4
		public static BoosterList Deserialize(Stream stream, BoosterList instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<BoosterInfo>();
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
					instance.List.Add(BoosterInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000B39 RID: 2873 RVA: 0x00029C5C File Offset: 0x00027E5C
		public void Serialize(Stream stream)
		{
			BoosterList.Serialize(stream, this);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00029C68 File Offset: 0x00027E68
		public static void Serialize(Stream stream, BoosterList instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, boosterInfo.GetSerializedSize());
					BoosterInfo.Serialize(stream, boosterInfo);
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00029CE0 File Offset: 0x00027EE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in this.List)
				{
					num += 1U;
					uint serializedSize = boosterInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040003CB RID: 971
		private List<BoosterInfo> _List = new List<BoosterInfo>();
	}
}
