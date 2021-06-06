using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.game_utilities.v2.client
{
	// Token: 0x02000353 RID: 851
	public class ProcessTaskResponse : IProtoBuf
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000B1342 File Offset: 0x000AF542
		// (set) Token: 0x0600357C RID: 13692 RVA: 0x000B134A File Offset: 0x000AF54A
		public List<bnet.protocol.v2.Attribute> Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000B1342 File Offset: 0x000AF542
		public List<bnet.protocol.v2.Attribute> ResultList
		{
			get
			{
				return this._Result;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000B1353 File Offset: 0x000AF553
		public int ResultCount
		{
			get
			{
				return this._Result.Count;
			}
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000B1360 File Offset: 0x000AF560
		public void AddResult(bnet.protocol.v2.Attribute val)
		{
			this._Result.Add(val);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000B136E File Offset: 0x000AF56E
		public void ClearResult()
		{
			this._Result.Clear();
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000B137B File Offset: 0x000AF57B
		public void SetResult(List<bnet.protocol.v2.Attribute> val)
		{
			this.Result = val;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000B1384 File Offset: 0x000AF584
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Result)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000B13E8 File Offset: 0x000AF5E8
		public override bool Equals(object obj)
		{
			ProcessTaskResponse processTaskResponse = obj as ProcessTaskResponse;
			if (processTaskResponse == null)
			{
				return false;
			}
			if (this.Result.Count != processTaskResponse.Result.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Result.Count; i++)
			{
				if (!this.Result[i].Equals(processTaskResponse.Result[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000B1453 File Offset: 0x000AF653
		public static ProcessTaskResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessTaskResponse>(bs, 0, -1);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000B145D File Offset: 0x000AF65D
		public void Deserialize(Stream stream)
		{
			ProcessTaskResponse.Deserialize(stream, this);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000B1467 File Offset: 0x000AF667
		public static ProcessTaskResponse Deserialize(Stream stream, ProcessTaskResponse instance)
		{
			return ProcessTaskResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000B1474 File Offset: 0x000AF674
		public static ProcessTaskResponse DeserializeLengthDelimited(Stream stream)
		{
			ProcessTaskResponse processTaskResponse = new ProcessTaskResponse();
			ProcessTaskResponse.DeserializeLengthDelimited(stream, processTaskResponse);
			return processTaskResponse;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000B1490 File Offset: 0x000AF690
		public static ProcessTaskResponse DeserializeLengthDelimited(Stream stream, ProcessTaskResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessTaskResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000B14B8 File Offset: 0x000AF6B8
		public static ProcessTaskResponse Deserialize(Stream stream, ProcessTaskResponse instance, long limit)
		{
			if (instance.Result == null)
			{
				instance.Result = new List<bnet.protocol.v2.Attribute>();
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
					instance.Result.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600358B RID: 13707 RVA: 0x000B1550 File Offset: 0x000AF750
		public void Serialize(Stream stream)
		{
			ProcessTaskResponse.Serialize(stream, this);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000B155C File Offset: 0x000AF75C
		public static void Serialize(Stream stream, ProcessTaskResponse instance)
		{
			if (instance.Result.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Result)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000B15D4 File Offset: 0x000AF7D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Result.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Result)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400146E RID: 5230
		private List<bnet.protocol.v2.Attribute> _Result = new List<bnet.protocol.v2.Attribute>();
	}
}
