using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D2 RID: 210
	public class NoticeNotification : IProtoBuf
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003474B File Offset: 0x0003294B
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00034753 File Offset: 0x00032953
		public ProfileNotice Notice { get; set; }

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003475C File Offset: 0x0003295C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Notice.GetHashCode();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00034778 File Offset: 0x00032978
		public override bool Equals(object obj)
		{
			NoticeNotification noticeNotification = obj as NoticeNotification;
			return noticeNotification != null && this.Notice.Equals(noticeNotification.Notice);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000347A7 File Offset: 0x000329A7
		public void Deserialize(Stream stream)
		{
			NoticeNotification.Deserialize(stream, this);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000347B1 File Offset: 0x000329B1
		public static NoticeNotification Deserialize(Stream stream, NoticeNotification instance)
		{
			return NoticeNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000347BC File Offset: 0x000329BC
		public static NoticeNotification DeserializeLengthDelimited(Stream stream)
		{
			NoticeNotification noticeNotification = new NoticeNotification();
			NoticeNotification.DeserializeLengthDelimited(stream, noticeNotification);
			return noticeNotification;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000347D8 File Offset: 0x000329D8
		public static NoticeNotification DeserializeLengthDelimited(Stream stream, NoticeNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NoticeNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00034800 File Offset: 0x00032A00
		public static NoticeNotification Deserialize(Stream stream, NoticeNotification instance, long limit)
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
					if (instance.Notice == null)
					{
						instance.Notice = ProfileNotice.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNotice.DeserializeLengthDelimited(stream, instance.Notice);
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

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003489A File Offset: 0x00032A9A
		public void Serialize(Stream stream)
		{
			NoticeNotification.Serialize(stream, this);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000348A3 File Offset: 0x00032AA3
		public static void Serialize(Stream stream, NoticeNotification instance)
		{
			if (instance.Notice == null)
			{
				throw new ArgumentNullException("Notice", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Notice.GetSerializedSize());
			ProfileNotice.Serialize(stream, instance.Notice);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000348E4 File Offset: 0x00032AE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Notice.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}
	}
}
