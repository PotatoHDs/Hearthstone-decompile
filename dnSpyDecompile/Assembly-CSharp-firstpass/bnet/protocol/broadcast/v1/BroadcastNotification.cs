using System;
using System.IO;
using System.Text;

namespace bnet.protocol.broadcast.v1
{
	// Token: 0x020004E5 RID: 1253
	public class BroadcastNotification : IProtoBuf
	{
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005882 RID: 22658 RVA: 0x0010F037 File Offset: 0x0010D237
		// (set) Token: 0x06005883 RID: 22659 RVA: 0x0010F03F File Offset: 0x0010D23F
		public Broadcast Broadcast
		{
			get
			{
				return this._Broadcast;
			}
			set
			{
				this._Broadcast = value;
				this.HasBroadcast = (value != null);
			}
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x0010F052 File Offset: 0x0010D252
		public void SetBroadcast(Broadcast val)
		{
			this.Broadcast = val;
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x0010F05B File Offset: 0x0010D25B
		// (set) Token: 0x06005886 RID: 22662 RVA: 0x0010F063 File Offset: 0x0010D263
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x0010F076 File Offset: 0x0010D276
		public void SetId(string val)
		{
			this.Id = val;
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0010F080 File Offset: 0x0010D280
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBroadcast)
			{
				num ^= this.Broadcast.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0010F0C8 File Offset: 0x0010D2C8
		public override bool Equals(object obj)
		{
			BroadcastNotification broadcastNotification = obj as BroadcastNotification;
			return broadcastNotification != null && this.HasBroadcast == broadcastNotification.HasBroadcast && (!this.HasBroadcast || this.Broadcast.Equals(broadcastNotification.Broadcast)) && this.HasId == broadcastNotification.HasId && (!this.HasId || this.Id.Equals(broadcastNotification.Id));
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x0010F138 File Offset: 0x0010D338
		public static BroadcastNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BroadcastNotification>(bs, 0, -1);
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0010F142 File Offset: 0x0010D342
		public void Deserialize(Stream stream)
		{
			BroadcastNotification.Deserialize(stream, this);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0010F14C File Offset: 0x0010D34C
		public static BroadcastNotification Deserialize(Stream stream, BroadcastNotification instance)
		{
			return BroadcastNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0010F158 File Offset: 0x0010D358
		public static BroadcastNotification DeserializeLengthDelimited(Stream stream)
		{
			BroadcastNotification broadcastNotification = new BroadcastNotification();
			BroadcastNotification.DeserializeLengthDelimited(stream, broadcastNotification);
			return broadcastNotification;
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x0010F174 File Offset: 0x0010D374
		public static BroadcastNotification DeserializeLengthDelimited(Stream stream, BroadcastNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BroadcastNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x0010F19C File Offset: 0x0010D39C
		public static BroadcastNotification Deserialize(Stream stream, BroadcastNotification instance, long limit)
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
						instance.Id = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Broadcast == null)
				{
					instance.Broadcast = Broadcast.DeserializeLengthDelimited(stream);
				}
				else
				{
					Broadcast.DeserializeLengthDelimited(stream, instance.Broadcast);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x0010F24E File Offset: 0x0010D44E
		public void Serialize(Stream stream)
		{
			BroadcastNotification.Serialize(stream, this);
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0010F258 File Offset: 0x0010D458
		public static void Serialize(Stream stream, BroadcastNotification instance)
		{
			if (instance.HasBroadcast)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Broadcast.GetSerializedSize());
				Broadcast.Serialize(stream, instance.Broadcast);
			}
			if (instance.HasId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Id));
			}
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0010F2B8 File Offset: 0x0010D4B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBroadcast)
			{
				num += 1U;
				uint serializedSize = this.Broadcast.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Id);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001BB9 RID: 7097
		public bool HasBroadcast;

		// Token: 0x04001BBA RID: 7098
		private Broadcast _Broadcast;

		// Token: 0x04001BBB RID: 7099
		public bool HasId;

		// Token: 0x04001BBC RID: 7100
		private string _Id;
	}
}
