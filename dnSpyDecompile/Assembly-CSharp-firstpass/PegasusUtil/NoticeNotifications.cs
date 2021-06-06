using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D3 RID: 211
	public class NoticeNotifications : IProtoBuf
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00034909 File Offset: 0x00032B09
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00034911 File Offset: 0x00032B11
		public List<NoticeNotification> NoticeNotifications_
		{
			get
			{
				return this._NoticeNotifications_;
			}
			set
			{
				this._NoticeNotifications_ = value;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003491C File Offset: 0x00032B1C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (NoticeNotification noticeNotification in this.NoticeNotifications_)
			{
				num ^= noticeNotification.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00034980 File Offset: 0x00032B80
		public override bool Equals(object obj)
		{
			NoticeNotifications noticeNotifications = obj as NoticeNotifications;
			if (noticeNotifications == null)
			{
				return false;
			}
			if (this.NoticeNotifications_.Count != noticeNotifications.NoticeNotifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.NoticeNotifications_.Count; i++)
			{
				if (!this.NoticeNotifications_[i].Equals(noticeNotifications.NoticeNotifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000349EB File Offset: 0x00032BEB
		public void Deserialize(Stream stream)
		{
			NoticeNotifications.Deserialize(stream, this);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000349F5 File Offset: 0x00032BF5
		public static NoticeNotifications Deserialize(Stream stream, NoticeNotifications instance)
		{
			return NoticeNotifications.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00034A00 File Offset: 0x00032C00
		public static NoticeNotifications DeserializeLengthDelimited(Stream stream)
		{
			NoticeNotifications noticeNotifications = new NoticeNotifications();
			NoticeNotifications.DeserializeLengthDelimited(stream, noticeNotifications);
			return noticeNotifications;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00034A1C File Offset: 0x00032C1C
		public static NoticeNotifications DeserializeLengthDelimited(Stream stream, NoticeNotifications instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NoticeNotifications.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00034A44 File Offset: 0x00032C44
		public static NoticeNotifications Deserialize(Stream stream, NoticeNotifications instance, long limit)
		{
			if (instance.NoticeNotifications_ == null)
			{
				instance.NoticeNotifications_ = new List<NoticeNotification>();
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
					instance.NoticeNotifications_.Add(NoticeNotification.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000E6E RID: 3694 RVA: 0x00034ADC File Offset: 0x00032CDC
		public void Serialize(Stream stream)
		{
			NoticeNotifications.Serialize(stream, this);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00034AE8 File Offset: 0x00032CE8
		public static void Serialize(Stream stream, NoticeNotifications instance)
		{
			if (instance.NoticeNotifications_.Count > 0)
			{
				foreach (NoticeNotification noticeNotification in instance.NoticeNotifications_)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, noticeNotification.GetSerializedSize());
					NoticeNotification.Serialize(stream, noticeNotification);
				}
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00034B60 File Offset: 0x00032D60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.NoticeNotifications_.Count > 0)
			{
				foreach (NoticeNotification noticeNotification in this.NoticeNotifications_)
				{
					num += 1U;
					uint serializedSize = noticeNotification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004C9 RID: 1225
		private List<NoticeNotification> _NoticeNotifications_ = new List<NoticeNotification>();
	}
}
