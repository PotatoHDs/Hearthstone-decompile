using System;
using System.IO;

namespace bnet.protocol.report.v2
{
	// Token: 0x02000320 RID: 800
	public class ReportItem : IProtoBuf
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000A4DE6 File Offset: 0x000A2FE6
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000A4DEE File Offset: 0x000A2FEE
		public MessageId MessageId
		{
			get
			{
				return this._MessageId;
			}
			set
			{
				this._MessageId = value;
				this.HasMessageId = (value != null);
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000A4E01 File Offset: 0x000A3001
		public void SetMessageId(MessageId val)
		{
			this.MessageId = val;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000A4E0C File Offset: 0x000A300C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMessageId)
			{
				num ^= this.MessageId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000A4E3C File Offset: 0x000A303C
		public override bool Equals(object obj)
		{
			ReportItem reportItem = obj as ReportItem;
			return reportItem != null && this.HasMessageId == reportItem.HasMessageId && (!this.HasMessageId || this.MessageId.Equals(reportItem.MessageId));
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000A4E81 File Offset: 0x000A3081
		public static ReportItem ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReportItem>(bs, 0, -1);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000A4E8B File Offset: 0x000A308B
		public void Deserialize(Stream stream)
		{
			ReportItem.Deserialize(stream, this);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000A4E95 File Offset: 0x000A3095
		public static ReportItem Deserialize(Stream stream, ReportItem instance)
		{
			return ReportItem.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000A4EA0 File Offset: 0x000A30A0
		public static ReportItem DeserializeLengthDelimited(Stream stream)
		{
			ReportItem reportItem = new ReportItem();
			ReportItem.DeserializeLengthDelimited(stream, reportItem);
			return reportItem;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000A4EBC File Offset: 0x000A30BC
		public static ReportItem DeserializeLengthDelimited(Stream stream, ReportItem instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReportItem.Deserialize(stream, instance, num);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000A4EE4 File Offset: 0x000A30E4
		public static ReportItem Deserialize(Stream stream, ReportItem instance, long limit)
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
					if (instance.MessageId == null)
					{
						instance.MessageId = MessageId.DeserializeLengthDelimited(stream);
					}
					else
					{
						MessageId.DeserializeLengthDelimited(stream, instance.MessageId);
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

		// Token: 0x06003106 RID: 12550 RVA: 0x000A4F7E File Offset: 0x000A317E
		public void Serialize(Stream stream)
		{
			ReportItem.Serialize(stream, this);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000A4F87 File Offset: 0x000A3187
		public static void Serialize(Stream stream, ReportItem instance)
		{
			if (instance.HasMessageId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MessageId.GetSerializedSize());
				MessageId.Serialize(stream, instance.MessageId);
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000A4FB8 File Offset: 0x000A31B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMessageId)
			{
				num += 1U;
				uint serializedSize = this.MessageId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001370 RID: 4976
		public bool HasMessageId;

		// Token: 0x04001371 RID: 4977
		private MessageId _MessageId;
	}
}
