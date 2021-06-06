using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B4 RID: 436
	public class AIDebugInformation : IProtoBuf
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00061392 File Offset: 0x0005F592
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0006139A File Offset: 0x0005F59A
		public int TurnID { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000613A3 File Offset: 0x0005F5A3
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x000613AB File Offset: 0x0005F5AB
		public int MoveID { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000613B4 File Offset: 0x0005F5B4
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x000613BC File Offset: 0x0005F5BC
		public List<AIEvaluation> Evaluations
		{
			get
			{
				return this._Evaluations;
			}
			set
			{
				this._Evaluations = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000613C5 File Offset: 0x0005F5C5
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x000613CD File Offset: 0x0005F5CD
		public float RootValue
		{
			get
			{
				return this._RootValue;
			}
			set
			{
				this._RootValue = value;
				this.HasRootValue = true;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x000613DD File Offset: 0x0005F5DD
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x000613E5 File Offset: 0x0005F5E5
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

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000613F5 File Offset: 0x0005F5F5
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x000613FD File Offset: 0x0005F5FD
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0006140D File Offset: 0x0005F60D
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00061415 File Offset: 0x0005F615
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00061425 File Offset: 0x0005F625
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x0006142D File Offset: 0x0005F62D
		public int TotalVisits
		{
			get
			{
				return this._TotalVisits;
			}
			set
			{
				this._TotalVisits = value;
				this.HasTotalVisits = true;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0006143D File Offset: 0x0005F63D
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00061445 File Offset: 0x0005F645
		public int SubtreeDepth
		{
			get
			{
				return this._SubtreeDepth;
			}
			set
			{
				this._SubtreeDepth = value;
				this.HasSubtreeDepth = true;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00061455 File Offset: 0x0005F655
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x0006145D File Offset: 0x0005F65D
		public int UniqueNodes
		{
			get
			{
				return this._UniqueNodes;
			}
			set
			{
				this._UniqueNodes = value;
				this.HasUniqueNodes = true;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0006146D File Offset: 0x0005F66D
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x00061475 File Offset: 0x0005F675
		public int DebugIteration
		{
			get
			{
				return this._DebugIteration;
			}
			set
			{
				this._DebugIteration = value;
				this.HasDebugIteration = true;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00061485 File Offset: 0x0005F685
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x0006148D File Offset: 0x0005F68D
		public int DebugDepth
		{
			get
			{
				return this._DebugDepth;
			}
			set
			{
				this._DebugDepth = value;
				this.HasDebugDepth = true;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0006149D File Offset: 0x0005F69D
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x000614A5 File Offset: 0x0005F6A5
		public long ModelVersion
		{
			get
			{
				return this._ModelVersion;
			}
			set
			{
				this._ModelVersion = value;
				this.HasModelVersion = true;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000614B8 File Offset: 0x0005F6B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.TurnID.GetHashCode();
			num ^= this.MoveID.GetHashCode();
			foreach (AIEvaluation aievaluation in this.Evaluations)
			{
				num ^= aievaluation.GetHashCode();
			}
			if (this.HasRootValue)
			{
				num ^= this.RootValue.GetHashCode();
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
			if (this.HasTotalVisits)
			{
				num ^= this.TotalVisits.GetHashCode();
			}
			if (this.HasSubtreeDepth)
			{
				num ^= this.SubtreeDepth.GetHashCode();
			}
			if (this.HasUniqueNodes)
			{
				num ^= this.UniqueNodes.GetHashCode();
			}
			if (this.HasDebugIteration)
			{
				num ^= this.DebugIteration.GetHashCode();
			}
			if (this.HasDebugDepth)
			{
				num ^= this.DebugDepth.GetHashCode();
			}
			if (this.HasModelVersion)
			{
				num ^= this.ModelVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0006163C File Offset: 0x0005F83C
		public override bool Equals(object obj)
		{
			AIDebugInformation aidebugInformation = obj as AIDebugInformation;
			if (aidebugInformation == null)
			{
				return false;
			}
			if (!this.TurnID.Equals(aidebugInformation.TurnID))
			{
				return false;
			}
			if (!this.MoveID.Equals(aidebugInformation.MoveID))
			{
				return false;
			}
			if (this.Evaluations.Count != aidebugInformation.Evaluations.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Evaluations.Count; i++)
			{
				if (!this.Evaluations[i].Equals(aidebugInformation.Evaluations[i]))
				{
					return false;
				}
			}
			return this.HasRootValue == aidebugInformation.HasRootValue && (!this.HasRootValue || this.RootValue.Equals(aidebugInformation.RootValue)) && this.HasInferenceValue == aidebugInformation.HasInferenceValue && (!this.HasInferenceValue || this.InferenceValue.Equals(aidebugInformation.InferenceValue)) && this.HasHeuristicValue == aidebugInformation.HasHeuristicValue && (!this.HasHeuristicValue || this.HeuristicValue.Equals(aidebugInformation.HeuristicValue)) && this.HasSubtreeValue == aidebugInformation.HasSubtreeValue && (!this.HasSubtreeValue || this.SubtreeValue.Equals(aidebugInformation.SubtreeValue)) && this.HasTotalVisits == aidebugInformation.HasTotalVisits && (!this.HasTotalVisits || this.TotalVisits.Equals(aidebugInformation.TotalVisits)) && this.HasSubtreeDepth == aidebugInformation.HasSubtreeDepth && (!this.HasSubtreeDepth || this.SubtreeDepth.Equals(aidebugInformation.SubtreeDepth)) && this.HasUniqueNodes == aidebugInformation.HasUniqueNodes && (!this.HasUniqueNodes || this.UniqueNodes.Equals(aidebugInformation.UniqueNodes)) && this.HasDebugIteration == aidebugInformation.HasDebugIteration && (!this.HasDebugIteration || this.DebugIteration.Equals(aidebugInformation.DebugIteration)) && this.HasDebugDepth == aidebugInformation.HasDebugDepth && (!this.HasDebugDepth || this.DebugDepth.Equals(aidebugInformation.DebugDepth)) && this.HasModelVersion == aidebugInformation.HasModelVersion && (!this.HasModelVersion || this.ModelVersion.Equals(aidebugInformation.ModelVersion));
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000618A4 File Offset: 0x0005FAA4
		public void Deserialize(Stream stream)
		{
			AIDebugInformation.Deserialize(stream, this);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000618AE File Offset: 0x0005FAAE
		public static AIDebugInformation Deserialize(Stream stream, AIDebugInformation instance)
		{
			return AIDebugInformation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000618BC File Offset: 0x0005FABC
		public static AIDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			AIDebugInformation aidebugInformation = new AIDebugInformation();
			AIDebugInformation.DeserializeLengthDelimited(stream, aidebugInformation);
			return aidebugInformation;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000618D8 File Offset: 0x0005FAD8
		public static AIDebugInformation DeserializeLengthDelimited(Stream stream, AIDebugInformation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AIDebugInformation.Deserialize(stream, instance, num);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00061900 File Offset: 0x0005FB00
		public static AIDebugInformation Deserialize(Stream stream, AIDebugInformation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Evaluations == null)
			{
				instance.Evaluations = new List<AIEvaluation>();
			}
			instance.RootValue = 0f;
			instance.InferenceValue = 0f;
			instance.HeuristicValue = 0f;
			instance.SubtreeValue = 0f;
			instance.TotalVisits = 0;
			instance.SubtreeDepth = 0;
			instance.UniqueNodes = 0;
			instance.DebugIteration = 0;
			instance.DebugDepth = 0;
			instance.ModelVersion = 0L;
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
					if (num <= 53)
					{
						if (num <= 26)
						{
							if (num == 8)
							{
								instance.TurnID = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.MoveID = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 26)
							{
								instance.Evaluations.Add(AIEvaluation.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (num == 37)
							{
								instance.RootValue = binaryReader.ReadSingle();
								continue;
							}
							if (num == 45)
							{
								instance.InferenceValue = binaryReader.ReadSingle();
								continue;
							}
							if (num == 53)
							{
								instance.HeuristicValue = binaryReader.ReadSingle();
								continue;
							}
						}
					}
					else if (num <= 72)
					{
						if (num == 61)
						{
							instance.SubtreeValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 64)
						{
							instance.TotalVisits = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.SubtreeDepth = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 88)
					{
						if (num == 80)
						{
							instance.UniqueNodes = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.DebugIteration = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 96)
						{
							instance.DebugDepth = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.ModelVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00061B64 File Offset: 0x0005FD64
		public void Serialize(Stream stream)
		{
			AIDebugInformation.Serialize(stream, this);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00061B70 File Offset: 0x0005FD70
		public static void Serialize(Stream stream, AIDebugInformation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TurnID));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MoveID));
			if (instance.Evaluations.Count > 0)
			{
				foreach (AIEvaluation aievaluation in instance.Evaluations)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, aievaluation.GetSerializedSize());
					AIEvaluation.Serialize(stream, aievaluation);
				}
			}
			if (instance.HasRootValue)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.RootValue);
			}
			if (instance.HasInferenceValue)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.InferenceValue);
			}
			if (instance.HasHeuristicValue)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.HeuristicValue);
			}
			if (instance.HasSubtreeValue)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.SubtreeValue);
			}
			if (instance.HasTotalVisits)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalVisits));
			}
			if (instance.HasSubtreeDepth)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubtreeDepth));
			}
			if (instance.HasUniqueNodes)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UniqueNodes));
			}
			if (instance.HasDebugIteration)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DebugIteration));
			}
			if (instance.HasDebugDepth)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DebugDepth));
			}
			if (instance.HasModelVersion)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ModelVersion);
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00061D34 File Offset: 0x0005FF34
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TurnID));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MoveID));
			if (this.Evaluations.Count > 0)
			{
				foreach (AIEvaluation aievaluation in this.Evaluations)
				{
					num += 1U;
					uint serializedSize = aievaluation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasRootValue)
			{
				num += 1U;
				num += 4U;
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
			if (this.HasTotalVisits)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalVisits));
			}
			if (this.HasSubtreeDepth)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubtreeDepth));
			}
			if (this.HasUniqueNodes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UniqueNodes));
			}
			if (this.HasDebugIteration)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DebugIteration));
			}
			if (this.HasDebugDepth)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DebugDepth));
			}
			if (this.HasModelVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ModelVersion);
			}
			num += 2U;
			return num;
		}

		// Token: 0x04000A23 RID: 2595
		private List<AIEvaluation> _Evaluations = new List<AIEvaluation>();

		// Token: 0x04000A24 RID: 2596
		public bool HasRootValue;

		// Token: 0x04000A25 RID: 2597
		private float _RootValue;

		// Token: 0x04000A26 RID: 2598
		public bool HasInferenceValue;

		// Token: 0x04000A27 RID: 2599
		private float _InferenceValue;

		// Token: 0x04000A28 RID: 2600
		public bool HasHeuristicValue;

		// Token: 0x04000A29 RID: 2601
		private float _HeuristicValue;

		// Token: 0x04000A2A RID: 2602
		public bool HasSubtreeValue;

		// Token: 0x04000A2B RID: 2603
		private float _SubtreeValue;

		// Token: 0x04000A2C RID: 2604
		public bool HasTotalVisits;

		// Token: 0x04000A2D RID: 2605
		private int _TotalVisits;

		// Token: 0x04000A2E RID: 2606
		public bool HasSubtreeDepth;

		// Token: 0x04000A2F RID: 2607
		private int _SubtreeDepth;

		// Token: 0x04000A30 RID: 2608
		public bool HasUniqueNodes;

		// Token: 0x04000A31 RID: 2609
		private int _UniqueNodes;

		// Token: 0x04000A32 RID: 2610
		public bool HasDebugIteration;

		// Token: 0x04000A33 RID: 2611
		private int _DebugIteration;

		// Token: 0x04000A34 RID: 2612
		public bool HasDebugDepth;

		// Token: 0x04000A35 RID: 2613
		private int _DebugDepth;

		// Token: 0x04000A36 RID: 2614
		public bool HasModelVersion;

		// Token: 0x04000A37 RID: 2615
		private long _ModelVersion;

		// Token: 0x0200064B RID: 1611
		public enum PacketID
		{
			// Token: 0x0400210A RID: 8458
			ID = 6
		}
	}
}
