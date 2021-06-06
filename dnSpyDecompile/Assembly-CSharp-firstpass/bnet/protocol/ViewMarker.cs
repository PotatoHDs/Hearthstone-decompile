using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002A8 RID: 680
	public class ViewMarker : IProtoBuf
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x0008AB6D File Offset: 0x00088D6D
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x0008AB75 File Offset: 0x00088D75
		public ulong LastReadTime
		{
			get
			{
				return this._LastReadTime;
			}
			set
			{
				this._LastReadTime = value;
				this.HasLastReadTime = true;
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0008AB85 File Offset: 0x00088D85
		public void SetLastReadTime(ulong val)
		{
			this.LastReadTime = val;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0008AB8E File Offset: 0x00088D8E
		// (set) Token: 0x06002719 RID: 10009 RVA: 0x0008AB96 File Offset: 0x00088D96
		public ulong LastMessageTime
		{
			get
			{
				return this._LastMessageTime;
			}
			set
			{
				this._LastMessageTime = value;
				this.HasLastMessageTime = true;
			}
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0008ABA6 File Offset: 0x00088DA6
		public void SetLastMessageTime(ulong val)
		{
			this.LastMessageTime = val;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0008ABB0 File Offset: 0x00088DB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLastReadTime)
			{
				num ^= this.LastReadTime.GetHashCode();
			}
			if (this.HasLastMessageTime)
			{
				num ^= this.LastMessageTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0008ABFC File Offset: 0x00088DFC
		public override bool Equals(object obj)
		{
			ViewMarker viewMarker = obj as ViewMarker;
			return viewMarker != null && this.HasLastReadTime == viewMarker.HasLastReadTime && (!this.HasLastReadTime || this.LastReadTime.Equals(viewMarker.LastReadTime)) && this.HasLastMessageTime == viewMarker.HasLastMessageTime && (!this.HasLastMessageTime || this.LastMessageTime.Equals(viewMarker.LastMessageTime));
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0008AC72 File Offset: 0x00088E72
		public static ViewMarker ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewMarker>(bs, 0, -1);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0008AC7C File Offset: 0x00088E7C
		public void Deserialize(Stream stream)
		{
			ViewMarker.Deserialize(stream, this);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0008AC86 File Offset: 0x00088E86
		public static ViewMarker Deserialize(Stream stream, ViewMarker instance)
		{
			return ViewMarker.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0008AC94 File Offset: 0x00088E94
		public static ViewMarker DeserializeLengthDelimited(Stream stream)
		{
			ViewMarker viewMarker = new ViewMarker();
			ViewMarker.DeserializeLengthDelimited(stream, viewMarker);
			return viewMarker;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0008ACB0 File Offset: 0x00088EB0
		public static ViewMarker DeserializeLengthDelimited(Stream stream, ViewMarker instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewMarker.Deserialize(stream, instance, num);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0008ACD8 File Offset: 0x00088ED8
		public static ViewMarker Deserialize(Stream stream, ViewMarker instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.LastMessageTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.LastReadTime = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0008AD6F File Offset: 0x00088F6F
		public void Serialize(Stream stream)
		{
			ViewMarker.Serialize(stream, this);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0008AD78 File Offset: 0x00088F78
		public static void Serialize(Stream stream, ViewMarker instance)
		{
			if (instance.HasLastReadTime)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.LastReadTime);
			}
			if (instance.HasLastMessageTime)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.LastMessageTime);
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLastReadTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.LastReadTime);
			}
			if (this.HasLastMessageTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.LastMessageTime);
			}
			return num;
		}

		// Token: 0x0400111A RID: 4378
		public bool HasLastReadTime;

		// Token: 0x0400111B RID: 4379
		private ulong _LastReadTime;

		// Token: 0x0400111C RID: 4380
		public bool HasLastMessageTime;

		// Token: 0x0400111D RID: 4381
		private ulong _LastMessageTime;
	}
}
