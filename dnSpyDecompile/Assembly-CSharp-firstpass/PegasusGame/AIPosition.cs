using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000198 RID: 408
	public class AIPosition : IProtoBuf
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0005A614 File Offset: 0x00058814
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x0005A61C File Offset: 0x0005881C
		public int Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				this._Position = value;
				this.HasPosition = true;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0005A62C File Offset: 0x0005882C
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0005A634 File Offset: 0x00058834
		public float PriorProbability
		{
			get
			{
				return this._PriorProbability;
			}
			set
			{
				this._PriorProbability = value;
				this.HasPriorProbability = true;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005A644 File Offset: 0x00058844
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0005A64C File Offset: 0x0005884C
		public float PuctValue
		{
			get
			{
				return this._PuctValue;
			}
			set
			{
				this._PuctValue = value;
				this.HasPuctValue = true;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0005A65C File Offset: 0x0005885C
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x0005A664 File Offset: 0x00058864
		public float FinalQValue
		{
			get
			{
				return this._FinalQValue;
			}
			set
			{
				this._FinalQValue = value;
				this.HasFinalQValue = true;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0005A674 File Offset: 0x00058874
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x0005A67C File Offset: 0x0005887C
		public int FinalVisitCount
		{
			get
			{
				return this._FinalVisitCount;
			}
			set
			{
				this._FinalVisitCount = value;
				this.HasFinalVisitCount = true;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0005A68C File Offset: 0x0005888C
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x0005A694 File Offset: 0x00058894
		public float InferenceValue
		{
			get
			{
				return this._InferenceValue;
			}
			set
			{
				this._InferenceValue = value;
				this.HasInferenceValue = true;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0005A6A4 File Offset: 0x000588A4
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x0005A6AC File Offset: 0x000588AC
		public float HeuristicValue
		{
			get
			{
				return this._HeuristicValue;
			}
			set
			{
				this._HeuristicValue = value;
				this.HasHeuristicValue = true;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0005A6BC File Offset: 0x000588BC
		// (set) Token: 0x0600199E RID: 6558 RVA: 0x0005A6C4 File Offset: 0x000588C4
		public float SubtreeValue
		{
			get
			{
				return this._SubtreeValue;
			}
			set
			{
				this._SubtreeValue = value;
				this.HasSubtreeValue = true;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0005A6D4 File Offset: 0x000588D4
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0005A6DC File Offset: 0x000588DC
		public int EdgeCount
		{
			get
			{
				return this._EdgeCount;
			}
			set
			{
				this._EdgeCount = value;
				this.HasEdgeCount = true;
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0005A6EC File Offset: 0x000588EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPosition)
			{
				num ^= this.Position.GetHashCode();
			}
			if (this.HasPriorProbability)
			{
				num ^= this.PriorProbability.GetHashCode();
			}
			if (this.HasPuctValue)
			{
				num ^= this.PuctValue.GetHashCode();
			}
			if (this.HasFinalQValue)
			{
				num ^= this.FinalQValue.GetHashCode();
			}
			if (this.HasFinalVisitCount)
			{
				num ^= this.FinalVisitCount.GetHashCode();
			}
			if (this.HasInferenceValue)
			{
				num ^= this.InferenceValue.GetHashCode();
			}
			if (this.HasHeuristicValue)
			{
				num ^= this.HeuristicValue.GetHashCode();
			}
			if (this.HasSubtreeValue)
			{
				num ^= this.SubtreeValue.GetHashCode();
			}
			if (this.HasEdgeCount)
			{
				num ^= this.EdgeCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005A7E8 File Offset: 0x000589E8
		public override bool Equals(object obj)
		{
			AIPosition aiposition = obj as AIPosition;
			return aiposition != null && this.HasPosition == aiposition.HasPosition && (!this.HasPosition || this.Position.Equals(aiposition.Position)) && this.HasPriorProbability == aiposition.HasPriorProbability && (!this.HasPriorProbability || this.PriorProbability.Equals(aiposition.PriorProbability)) && this.HasPuctValue == aiposition.HasPuctValue && (!this.HasPuctValue || this.PuctValue.Equals(aiposition.PuctValue)) && this.HasFinalQValue == aiposition.HasFinalQValue && (!this.HasFinalQValue || this.FinalQValue.Equals(aiposition.FinalQValue)) && this.HasFinalVisitCount == aiposition.HasFinalVisitCount && (!this.HasFinalVisitCount || this.FinalVisitCount.Equals(aiposition.FinalVisitCount)) && this.HasInferenceValue == aiposition.HasInferenceValue && (!this.HasInferenceValue || this.InferenceValue.Equals(aiposition.InferenceValue)) && this.HasHeuristicValue == aiposition.HasHeuristicValue && (!this.HasHeuristicValue || this.HeuristicValue.Equals(aiposition.HeuristicValue)) && this.HasSubtreeValue == aiposition.HasSubtreeValue && (!this.HasSubtreeValue || this.SubtreeValue.Equals(aiposition.SubtreeValue)) && this.HasEdgeCount == aiposition.HasEdgeCount && (!this.HasEdgeCount || this.EdgeCount.Equals(aiposition.EdgeCount));
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		public void Deserialize(Stream stream)
		{
			AIPosition.Deserialize(stream, this);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005A9AA File Offset: 0x00058BAA
		public static AIPosition Deserialize(Stream stream, AIPosition instance)
		{
			return AIPosition.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005A9B8 File Offset: 0x00058BB8
		public static AIPosition DeserializeLengthDelimited(Stream stream)
		{
			AIPosition aiposition = new AIPosition();
			AIPosition.DeserializeLengthDelimited(stream, aiposition);
			return aiposition;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005A9D4 File Offset: 0x00058BD4
		public static AIPosition DeserializeLengthDelimited(Stream stream, AIPosition instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AIPosition.Deserialize(stream, instance, num);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005A9FC File Offset: 0x00058BFC
		public static AIPosition Deserialize(Stream stream, AIPosition instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.PriorProbability = 0f;
			instance.PuctValue = 0f;
			instance.FinalQValue = 0f;
			instance.FinalVisitCount = 0;
			instance.InferenceValue = 0f;
			instance.HeuristicValue = 0f;
			instance.SubtreeValue = 0f;
			instance.EdgeCount = 0;
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
				else
				{
					if (num <= 37)
					{
						if (num <= 21)
						{
							if (num == 8)
							{
								instance.Position = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 21)
							{
								instance.PriorProbability = binaryReader.ReadSingle();
								continue;
							}
						}
						else
						{
							if (num == 29)
							{
								instance.PuctValue = binaryReader.ReadSingle();
								continue;
							}
							if (num == 37)
							{
								instance.FinalQValue = binaryReader.ReadSingle();
								continue;
							}
						}
					}
					else if (num <= 53)
					{
						if (num == 40)
						{
							instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 53)
						{
							instance.InferenceValue = binaryReader.ReadSingle();
							continue;
						}
					}
					else
					{
						if (num == 61)
						{
							instance.HeuristicValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 69)
						{
							instance.SubtreeValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 72)
						{
							instance.EdgeCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x060019A8 RID: 6568 RVA: 0x0005ABBA File Offset: 0x00058DBA
		public void Serialize(Stream stream)
		{
			AIPosition.Serialize(stream, this);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0005ABC4 File Offset: 0x00058DC4
		public static void Serialize(Stream stream, AIPosition instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPosition)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Position));
			}
			if (instance.HasPriorProbability)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.PriorProbability);
			}
			if (instance.HasPuctValue)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.PuctValue);
			}
			if (instance.HasFinalQValue)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.FinalQValue);
			}
			if (instance.HasFinalVisitCount)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FinalVisitCount));
			}
			if (instance.HasInferenceValue)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.InferenceValue);
			}
			if (instance.HasHeuristicValue)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.HeuristicValue);
			}
			if (instance.HasSubtreeValue)
			{
				stream.WriteByte(69);
				binaryWriter.Write(instance.SubtreeValue);
			}
			if (instance.HasEdgeCount)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EdgeCount));
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0005ACD8 File Offset: 0x00058ED8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPosition)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Position));
			}
			if (this.HasPriorProbability)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasPuctValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFinalQValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFinalVisitCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FinalVisitCount));
			}
			if (this.HasInferenceValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasHeuristicValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasSubtreeValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasEdgeCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EdgeCount));
			}
			return num;
		}

		// Token: 0x0400099E RID: 2462
		public bool HasPosition;

		// Token: 0x0400099F RID: 2463
		private int _Position;

		// Token: 0x040009A0 RID: 2464
		public bool HasPriorProbability;

		// Token: 0x040009A1 RID: 2465
		private float _PriorProbability;

		// Token: 0x040009A2 RID: 2466
		public bool HasPuctValue;

		// Token: 0x040009A3 RID: 2467
		private float _PuctValue;

		// Token: 0x040009A4 RID: 2468
		public bool HasFinalQValue;

		// Token: 0x040009A5 RID: 2469
		private float _FinalQValue;

		// Token: 0x040009A6 RID: 2470
		public bool HasFinalVisitCount;

		// Token: 0x040009A7 RID: 2471
		private int _FinalVisitCount;

		// Token: 0x040009A8 RID: 2472
		public bool HasInferenceValue;

		// Token: 0x040009A9 RID: 2473
		private float _InferenceValue;

		// Token: 0x040009AA RID: 2474
		public bool HasHeuristicValue;

		// Token: 0x040009AB RID: 2475
		private float _HeuristicValue;

		// Token: 0x040009AC RID: 2476
		public bool HasSubtreeValue;

		// Token: 0x040009AD RID: 2477
		private float _SubtreeValue;

		// Token: 0x040009AE RID: 2478
		public bool HasEdgeCount;

		// Token: 0x040009AF RID: 2479
		private int _EdgeCount;
	}
}
