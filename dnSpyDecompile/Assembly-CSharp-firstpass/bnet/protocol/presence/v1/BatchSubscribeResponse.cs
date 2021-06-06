using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033E RID: 830
	public class BatchSubscribeResponse : IProtoBuf
	{
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000AB95D File Offset: 0x000A9B5D
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000AB965 File Offset: 0x000A9B65
		public List<SubscribeResult> SubscribeFailed
		{
			get
			{
				return this._SubscribeFailed;
			}
			set
			{
				this._SubscribeFailed = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000AB95D File Offset: 0x000A9B5D
		public List<SubscribeResult> SubscribeFailedList
		{
			get
			{
				return this._SubscribeFailed;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000AB96E File Offset: 0x000A9B6E
		public int SubscribeFailedCount
		{
			get
			{
				return this._SubscribeFailed.Count;
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000AB97B File Offset: 0x000A9B7B
		public void AddSubscribeFailed(SubscribeResult val)
		{
			this._SubscribeFailed.Add(val);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000AB989 File Offset: 0x000A9B89
		public void ClearSubscribeFailed()
		{
			this._SubscribeFailed.Clear();
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000AB996 File Offset: 0x000A9B96
		public void SetSubscribeFailed(List<SubscribeResult> val)
		{
			this.SubscribeFailed = val;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000AB9A0 File Offset: 0x000A9BA0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SubscribeResult subscribeResult in this.SubscribeFailed)
			{
				num ^= subscribeResult.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000ABA04 File Offset: 0x000A9C04
		public override bool Equals(object obj)
		{
			BatchSubscribeResponse batchSubscribeResponse = obj as BatchSubscribeResponse;
			if (batchSubscribeResponse == null)
			{
				return false;
			}
			if (this.SubscribeFailed.Count != batchSubscribeResponse.SubscribeFailed.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SubscribeFailed.Count; i++)
			{
				if (!this.SubscribeFailed[i].Equals(batchSubscribeResponse.SubscribeFailed[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000ABA6F File Offset: 0x000A9C6F
		public static BatchSubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchSubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000ABA79 File Offset: 0x000A9C79
		public void Deserialize(Stream stream)
		{
			BatchSubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000ABA83 File Offset: 0x000A9C83
		public static BatchSubscribeResponse Deserialize(Stream stream, BatchSubscribeResponse instance)
		{
			return BatchSubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000ABA90 File Offset: 0x000A9C90
		public static BatchSubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			BatchSubscribeResponse batchSubscribeResponse = new BatchSubscribeResponse();
			BatchSubscribeResponse.DeserializeLengthDelimited(stream, batchSubscribeResponse);
			return batchSubscribeResponse;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000ABAAC File Offset: 0x000A9CAC
		public static BatchSubscribeResponse DeserializeLengthDelimited(Stream stream, BatchSubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BatchSubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
		public static BatchSubscribeResponse Deserialize(Stream stream, BatchSubscribeResponse instance, long limit)
		{
			if (instance.SubscribeFailed == null)
			{
				instance.SubscribeFailed = new List<SubscribeResult>();
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
					instance.SubscribeFailed.Add(SubscribeResult.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600337E RID: 13182 RVA: 0x000ABB6C File Offset: 0x000A9D6C
		public void Serialize(Stream stream)
		{
			BatchSubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000ABB78 File Offset: 0x000A9D78
		public static void Serialize(Stream stream, BatchSubscribeResponse instance)
		{
			if (instance.SubscribeFailed.Count > 0)
			{
				foreach (SubscribeResult subscribeResult in instance.SubscribeFailed)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, subscribeResult.GetSerializedSize());
					SubscribeResult.Serialize(stream, subscribeResult);
				}
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000ABBF0 File Offset: 0x000A9DF0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.SubscribeFailed.Count > 0)
			{
				foreach (SubscribeResult subscribeResult in this.SubscribeFailed)
				{
					num += 1U;
					uint serializedSize = subscribeResult.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001401 RID: 5121
		private List<SubscribeResult> _SubscribeFailed = new List<SubscribeResult>();
	}
}
