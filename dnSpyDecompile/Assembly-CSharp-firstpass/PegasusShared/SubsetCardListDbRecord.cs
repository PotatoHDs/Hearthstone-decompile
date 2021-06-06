using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000153 RID: 339
	public class SubsetCardListDbRecord : IProtoBuf
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0004E277 File Offset: 0x0004C477
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0004E27F File Offset: 0x0004C47F
		public int SubsetId { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0004E288 File Offset: 0x0004C488
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x0004E290 File Offset: 0x0004C490
		public List<int> CardIds
		{
			get
			{
				return this._CardIds;
			}
			set
			{
				this._CardIds = value;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0004E29C File Offset: 0x0004C49C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.SubsetId.GetHashCode();
			foreach (int num2 in this.CardIds)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004E310 File Offset: 0x0004C510
		public override bool Equals(object obj)
		{
			SubsetCardListDbRecord subsetCardListDbRecord = obj as SubsetCardListDbRecord;
			if (subsetCardListDbRecord == null)
			{
				return false;
			}
			if (!this.SubsetId.Equals(subsetCardListDbRecord.SubsetId))
			{
				return false;
			}
			if (this.CardIds.Count != subsetCardListDbRecord.CardIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardIds.Count; i++)
			{
				if (!this.CardIds[i].Equals(subsetCardListDbRecord.CardIds[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0004E396 File Offset: 0x0004C596
		public void Deserialize(Stream stream)
		{
			SubsetCardListDbRecord.Deserialize(stream, this);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004E3A0 File Offset: 0x0004C5A0
		public static SubsetCardListDbRecord Deserialize(Stream stream, SubsetCardListDbRecord instance)
		{
			return SubsetCardListDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0004E3AC File Offset: 0x0004C5AC
		public static SubsetCardListDbRecord DeserializeLengthDelimited(Stream stream)
		{
			SubsetCardListDbRecord subsetCardListDbRecord = new SubsetCardListDbRecord();
			SubsetCardListDbRecord.DeserializeLengthDelimited(stream, subsetCardListDbRecord);
			return subsetCardListDbRecord;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0004E3C8 File Offset: 0x0004C5C8
		public static SubsetCardListDbRecord DeserializeLengthDelimited(Stream stream, SubsetCardListDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubsetCardListDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004E3F0 File Offset: 0x0004C5F0
		public static SubsetCardListDbRecord Deserialize(Stream stream, SubsetCardListDbRecord instance, long limit)
		{
			if (instance.CardIds == null)
			{
				instance.CardIds = new List<int>();
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
						instance.CardIds.Add((int)ProtocolParser.ReadUInt64(stream));
					}
				}
				else
				{
					instance.SubsetId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0004E4A1 File Offset: 0x0004C6A1
		public void Serialize(Stream stream)
		{
			SubsetCardListDbRecord.Serialize(stream, this);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004E4AC File Offset: 0x0004C6AC
		public static void Serialize(Stream stream, SubsetCardListDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubsetId));
			if (instance.CardIds.Count > 0)
			{
				foreach (int num in instance.CardIds)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0004E52C File Offset: 0x0004C72C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubsetId));
			if (this.CardIds.Count > 0)
			{
				foreach (int num2 in this.CardIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000704 RID: 1796
		private List<int> _CardIds = new List<int>();
	}
}
