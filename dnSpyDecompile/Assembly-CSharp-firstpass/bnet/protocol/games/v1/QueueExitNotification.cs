using System;
using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000399 RID: 921
	public class QueueExitNotification : IProtoBuf
	{
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000BF514 File Offset: 0x000BD714
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x000BF51C File Offset: 0x000BD71C
		public FindGameRequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000BF52F File Offset: 0x000BD72F
		public void SetRequestId(FindGameRequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000BF538 File Offset: 0x000BD738
		// (set) Token: 0x06003B28 RID: 15144 RVA: 0x000BF540 File Offset: 0x000BD740
		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000BF550 File Offset: 0x000BD750
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000BF55C File Offset: 0x000BD75C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000BF5A8 File Offset: 0x000BD7A8
		public override bool Equals(object obj)
		{
			QueueExitNotification queueExitNotification = obj as QueueExitNotification;
			return queueExitNotification != null && this.HasRequestId == queueExitNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueExitNotification.RequestId)) && this.HasResult == queueExitNotification.HasResult && (!this.HasResult || this.Result.Equals(queueExitNotification.Result));
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000BF61B File Offset: 0x000BD81B
		public static QueueExitNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueExitNotification>(bs, 0, -1);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000BF625 File Offset: 0x000BD825
		public void Deserialize(Stream stream)
		{
			QueueExitNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000BF62F File Offset: 0x000BD82F
		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance)
		{
			return QueueExitNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000BF63C File Offset: 0x000BD83C
		public static QueueExitNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueExitNotification queueExitNotification = new QueueExitNotification();
			QueueExitNotification.DeserializeLengthDelimited(stream, queueExitNotification);
			return queueExitNotification;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000BF658 File Offset: 0x000BD858
		public static QueueExitNotification DeserializeLengthDelimited(Stream stream, QueueExitNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueExitNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000BF680 File Offset: 0x000BD880
		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Result = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000BF732 File Offset: 0x000BD932
		public void Serialize(Stream stream)
		{
			QueueExitNotification.Serialize(stream, this);
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000BF73C File Offset: 0x000BD93C
		public static void Serialize(Stream stream, QueueExitNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000BF794 File Offset: 0x000BD994
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			return num;
		}

		// Token: 0x04001564 RID: 5476
		public bool HasRequestId;

		// Token: 0x04001565 RID: 5477
		private FindGameRequestId _RequestId;

		// Token: 0x04001566 RID: 5478
		public bool HasResult;

		// Token: 0x04001567 RID: 5479
		private uint _Result;
	}
}
