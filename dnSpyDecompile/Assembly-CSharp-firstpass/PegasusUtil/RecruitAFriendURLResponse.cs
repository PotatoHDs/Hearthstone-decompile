using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000DC RID: 220
	public class RecruitAFriendURLResponse : IProtoBuf
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000368F5 File Offset: 0x00034AF5
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x000368FD File Offset: 0x00034AFD
		public string RafUrl { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00036906 File Offset: 0x00034B06
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0003690E File Offset: 0x00034B0E
		public RAFServiceStatus RafServiceStatus { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00036917 File Offset: 0x00034B17
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0003691F File Offset: 0x00034B1F
		public string RafUrlFull { get; set; }

		// Token: 0x06000F00 RID: 3840 RVA: 0x00036928 File Offset: 0x00034B28
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.RafUrl.GetHashCode() ^ this.RafServiceStatus.GetHashCode() ^ this.RafUrlFull.GetHashCode();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00036970 File Offset: 0x00034B70
		public override bool Equals(object obj)
		{
			RecruitAFriendURLResponse recruitAFriendURLResponse = obj as RecruitAFriendURLResponse;
			return recruitAFriendURLResponse != null && this.RafUrl.Equals(recruitAFriendURLResponse.RafUrl) && this.RafServiceStatus.Equals(recruitAFriendURLResponse.RafServiceStatus) && this.RafUrlFull.Equals(recruitAFriendURLResponse.RafUrlFull);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000369D7 File Offset: 0x00034BD7
		public void Deserialize(Stream stream)
		{
			RecruitAFriendURLResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000369E1 File Offset: 0x00034BE1
		public static RecruitAFriendURLResponse Deserialize(Stream stream, RecruitAFriendURLResponse instance)
		{
			return RecruitAFriendURLResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000369EC File Offset: 0x00034BEC
		public static RecruitAFriendURLResponse DeserializeLengthDelimited(Stream stream)
		{
			RecruitAFriendURLResponse recruitAFriendURLResponse = new RecruitAFriendURLResponse();
			RecruitAFriendURLResponse.DeserializeLengthDelimited(stream, recruitAFriendURLResponse);
			return recruitAFriendURLResponse;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00036A08 File Offset: 0x00034C08
		public static RecruitAFriendURLResponse DeserializeLengthDelimited(Stream stream, RecruitAFriendURLResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecruitAFriendURLResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00036A30 File Offset: 0x00034C30
		public static RecruitAFriendURLResponse Deserialize(Stream stream, RecruitAFriendURLResponse instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.RafUrlFull = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.RafServiceStatus = (RAFServiceStatus)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.RafUrl = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00036ADF File Offset: 0x00034CDF
		public void Serialize(Stream stream)
		{
			RecruitAFriendURLResponse.Serialize(stream, this);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00036AE8 File Offset: 0x00034CE8
		public static void Serialize(Stream stream, RecruitAFriendURLResponse instance)
		{
			if (instance.RafUrl == null)
			{
				throw new ArgumentNullException("RafUrl", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RafUrl));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RafServiceStatus));
			if (instance.RafUrlFull == null)
			{
				throw new ArgumentNullException("RafUrlFull", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RafUrlFull));
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00036B78 File Offset: 0x00034D78
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RafUrl);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)((long)this.RafServiceStatus));
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.RafUrlFull);
			return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 3U;
		}

		// Token: 0x020005E1 RID: 1505
		public enum PacketID
		{
			// Token: 0x04001FED RID: 8173
			ID = 336
		}
	}
}
