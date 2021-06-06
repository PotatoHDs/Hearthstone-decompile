using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000DB RID: 219
	public class GetRecruitAFriendURL : IProtoBuf
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00036737 File Offset: 0x00034937
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0003673F File Offset: 0x0003493F
		public Platform Platform { get; set; }

		// Token: 0x06000EEF RID: 3823 RVA: 0x00036748 File Offset: 0x00034948
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Platform.GetHashCode();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00036764 File Offset: 0x00034964
		public override bool Equals(object obj)
		{
			GetRecruitAFriendURL getRecruitAFriendURL = obj as GetRecruitAFriendURL;
			return getRecruitAFriendURL != null && this.Platform.Equals(getRecruitAFriendURL.Platform);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00036793 File Offset: 0x00034993
		public void Deserialize(Stream stream)
		{
			GetRecruitAFriendURL.Deserialize(stream, this);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003679D File Offset: 0x0003499D
		public static GetRecruitAFriendURL Deserialize(Stream stream, GetRecruitAFriendURL instance)
		{
			return GetRecruitAFriendURL.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000367A8 File Offset: 0x000349A8
		public static GetRecruitAFriendURL DeserializeLengthDelimited(Stream stream)
		{
			GetRecruitAFriendURL getRecruitAFriendURL = new GetRecruitAFriendURL();
			GetRecruitAFriendURL.DeserializeLengthDelimited(stream, getRecruitAFriendURL);
			return getRecruitAFriendURL;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000367C4 File Offset: 0x000349C4
		public static GetRecruitAFriendURL DeserializeLengthDelimited(Stream stream, GetRecruitAFriendURL instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetRecruitAFriendURL.Deserialize(stream, instance, num);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000367EC File Offset: 0x000349EC
		public static GetRecruitAFriendURL Deserialize(Stream stream, GetRecruitAFriendURL instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
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

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00036886 File Offset: 0x00034A86
		public void Serialize(Stream stream)
		{
			GetRecruitAFriendURL.Serialize(stream, this);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003688F File Offset: 0x00034A8F
		public static void Serialize(Stream stream, GetRecruitAFriendURL instance)
		{
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000368D0 File Offset: 0x00034AD0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Platform.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x020005E0 RID: 1504
		public enum PacketID
		{
			// Token: 0x04001FEA RID: 8170
			ID = 335,
			// Token: 0x04001FEB RID: 8171
			System = 2
		}
	}
}
