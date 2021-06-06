using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000DF RID: 223
	public class RecruitAFriendDataResponse : IProtoBuf
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00037119 File Offset: 0x00035319
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00037121 File Offset: 0x00035321
		public uint TotalRecruitCount { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0003712A File Offset: 0x0003532A
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00037132 File Offset: 0x00035332
		public List<RecruitData> TopRecruits
		{
			get
			{
				return this._TopRecruits;
			}
			set
			{
				this._TopRecruits = value;
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003713C File Offset: 0x0003533C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.TotalRecruitCount.GetHashCode();
			foreach (RecruitData recruitData in this.TopRecruits)
			{
				num ^= recruitData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000371B0 File Offset: 0x000353B0
		public override bool Equals(object obj)
		{
			RecruitAFriendDataResponse recruitAFriendDataResponse = obj as RecruitAFriendDataResponse;
			if (recruitAFriendDataResponse == null)
			{
				return false;
			}
			if (!this.TotalRecruitCount.Equals(recruitAFriendDataResponse.TotalRecruitCount))
			{
				return false;
			}
			if (this.TopRecruits.Count != recruitAFriendDataResponse.TopRecruits.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TopRecruits.Count; i++)
			{
				if (!this.TopRecruits[i].Equals(recruitAFriendDataResponse.TopRecruits[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00037233 File Offset: 0x00035433
		public void Deserialize(Stream stream)
		{
			RecruitAFriendDataResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003723D File Offset: 0x0003543D
		public static RecruitAFriendDataResponse Deserialize(Stream stream, RecruitAFriendDataResponse instance)
		{
			return RecruitAFriendDataResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00037248 File Offset: 0x00035448
		public static RecruitAFriendDataResponse DeserializeLengthDelimited(Stream stream)
		{
			RecruitAFriendDataResponse recruitAFriendDataResponse = new RecruitAFriendDataResponse();
			RecruitAFriendDataResponse.DeserializeLengthDelimited(stream, recruitAFriendDataResponse);
			return recruitAFriendDataResponse;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00037264 File Offset: 0x00035464
		public static RecruitAFriendDataResponse DeserializeLengthDelimited(Stream stream, RecruitAFriendDataResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecruitAFriendDataResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003728C File Offset: 0x0003548C
		public static RecruitAFriendDataResponse Deserialize(Stream stream, RecruitAFriendDataResponse instance, long limit)
		{
			if (instance.TopRecruits == null)
			{
				instance.TopRecruits = new List<RecruitData>();
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
					if (num != 18)
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
						instance.TopRecruits.Add(RecruitData.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.TotalRecruitCount = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003733B File Offset: 0x0003553B
		public void Serialize(Stream stream)
		{
			RecruitAFriendDataResponse.Serialize(stream, this);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00037344 File Offset: 0x00035544
		public static void Serialize(Stream stream, RecruitAFriendDataResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.TotalRecruitCount);
			if (instance.TopRecruits.Count > 0)
			{
				foreach (RecruitData recruitData in instance.TopRecruits)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, recruitData.GetSerializedSize());
					RecruitData.Serialize(stream, recruitData);
				}
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000373CC File Offset: 0x000355CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.TotalRecruitCount);
			if (this.TopRecruits.Count > 0)
			{
				foreach (RecruitData recruitData in this.TopRecruits)
				{
					num += 1U;
					uint serializedSize = recruitData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040004FA RID: 1274
		private List<RecruitData> _TopRecruits = new List<RecruitData>();

		// Token: 0x020005E3 RID: 1507
		public enum PacketID
		{
			// Token: 0x04001FF2 RID: 8178
			ID = 338
		}
	}
}
