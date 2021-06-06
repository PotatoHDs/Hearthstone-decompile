using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x02000196 RID: 406
	public class AIEvaluation : IProtoBuf
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00058C73 File Offset: 0x00056E73
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x00058C7B File Offset: 0x00056E7B
		public string OptionName { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00058C84 File Offset: 0x00056E84
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x00058C8C File Offset: 0x00056E8C
		public int EntityID { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00058C95 File Offset: 0x00056E95
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x00058C9D File Offset: 0x00056E9D
		public int BaseScore
		{
			get
			{
				return this._BaseScore;
			}
			set
			{
				this._BaseScore = value;
				this.HasBaseScore = true;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00058CAD File Offset: 0x00056EAD
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x00058CB5 File Offset: 0x00056EB5
		public int BonusScore
		{
			get
			{
				return this._BonusScore;
			}
			set
			{
				this._BonusScore = value;
				this.HasBonusScore = true;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00058CC5 File Offset: 0x00056EC5
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x00058CCD File Offset: 0x00056ECD
		public bool OptionChosen
		{
			get
			{
				return this._OptionChosen;
			}
			set
			{
				this._OptionChosen = value;
				this.HasOptionChosen = true;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00058CDD File Offset: 0x00056EDD
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x00058CE5 File Offset: 0x00056EE5
		public List<AITarget> TargetScores
		{
			get
			{
				return this._TargetScores;
			}
			set
			{
				this._TargetScores = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00058CEE File Offset: 0x00056EEE
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x00058CF6 File Offset: 0x00056EF6
		public List<AIContextualValue> ContextualScore
		{
			get
			{
				return this._ContextualScore;
			}
			set
			{
				this._ContextualScore = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00058CFF File Offset: 0x00056EFF
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x00058D07 File Offset: 0x00056F07
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

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00058D17 File Offset: 0x00056F17
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x00058D1F File Offset: 0x00056F1F
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

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00058D2F File Offset: 0x00056F2F
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00058D37 File Offset: 0x00056F37
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00058D47 File Offset: 0x00056F47
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00058D4F File Offset: 0x00056F4F
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

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00058D5F File Offset: 0x00056F5F
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x00058D67 File Offset: 0x00056F67
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00058D77 File Offset: 0x00056F77
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x00058D7F File Offset: 0x00056F7F
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

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00058D8F File Offset: 0x00056F8F
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x00058D97 File Offset: 0x00056F97
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

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00058DA7 File Offset: 0x00056FA7
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x00058DAF File Offset: 0x00056FAF
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00058DBF File Offset: 0x00056FBF
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x00058DC7 File Offset: 0x00056FC7
		public List<AIPosition> PositionScores
		{
			get
			{
				return this._PositionScores;
			}
			set
			{
				this._PositionScores = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00058DD0 File Offset: 0x00056FD0
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00058DD8 File Offset: 0x00056FD8
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

		// Token: 0x06001961 RID: 6497 RVA: 0x00058DE8 File Offset: 0x00056FE8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.OptionName.GetHashCode();
			num ^= this.EntityID.GetHashCode();
			if (this.HasBaseScore)
			{
				num ^= this.BaseScore.GetHashCode();
			}
			if (this.HasBonusScore)
			{
				num ^= this.BonusScore.GetHashCode();
			}
			if (this.HasOptionChosen)
			{
				num ^= this.OptionChosen.GetHashCode();
			}
			foreach (AITarget aitarget in this.TargetScores)
			{
				num ^= aitarget.GetHashCode();
			}
			foreach (AIContextualValue aicontextualValue in this.ContextualScore)
			{
				num ^= aicontextualValue.GetHashCode();
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
			if (this.HasSubtreeDepth)
			{
				num ^= this.SubtreeDepth.GetHashCode();
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
			foreach (AIPosition aiposition in this.PositionScores)
			{
				num ^= aiposition.GetHashCode();
			}
			if (this.HasEdgeCount)
			{
				num ^= this.EdgeCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005902C File Offset: 0x0005722C
		public override bool Equals(object obj)
		{
			AIEvaluation aievaluation = obj as AIEvaluation;
			if (aievaluation == null)
			{
				return false;
			}
			if (!this.OptionName.Equals(aievaluation.OptionName))
			{
				return false;
			}
			if (!this.EntityID.Equals(aievaluation.EntityID))
			{
				return false;
			}
			if (this.HasBaseScore != aievaluation.HasBaseScore || (this.HasBaseScore && !this.BaseScore.Equals(aievaluation.BaseScore)))
			{
				return false;
			}
			if (this.HasBonusScore != aievaluation.HasBonusScore || (this.HasBonusScore && !this.BonusScore.Equals(aievaluation.BonusScore)))
			{
				return false;
			}
			if (this.HasOptionChosen != aievaluation.HasOptionChosen || (this.HasOptionChosen && !this.OptionChosen.Equals(aievaluation.OptionChosen)))
			{
				return false;
			}
			if (this.TargetScores.Count != aievaluation.TargetScores.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TargetScores.Count; i++)
			{
				if (!this.TargetScores[i].Equals(aievaluation.TargetScores[i]))
				{
					return false;
				}
			}
			if (this.ContextualScore.Count != aievaluation.ContextualScore.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ContextualScore.Count; j++)
			{
				if (!this.ContextualScore[j].Equals(aievaluation.ContextualScore[j]))
				{
					return false;
				}
			}
			if (this.HasPriorProbability != aievaluation.HasPriorProbability || (this.HasPriorProbability && !this.PriorProbability.Equals(aievaluation.PriorProbability)))
			{
				return false;
			}
			if (this.HasPuctValue != aievaluation.HasPuctValue || (this.HasPuctValue && !this.PuctValue.Equals(aievaluation.PuctValue)))
			{
				return false;
			}
			if (this.HasFinalQValue != aievaluation.HasFinalQValue || (this.HasFinalQValue && !this.FinalQValue.Equals(aievaluation.FinalQValue)))
			{
				return false;
			}
			if (this.HasFinalVisitCount != aievaluation.HasFinalVisitCount || (this.HasFinalVisitCount && !this.FinalVisitCount.Equals(aievaluation.FinalVisitCount)))
			{
				return false;
			}
			if (this.HasSubtreeDepth != aievaluation.HasSubtreeDepth || (this.HasSubtreeDepth && !this.SubtreeDepth.Equals(aievaluation.SubtreeDepth)))
			{
				return false;
			}
			if (this.HasInferenceValue != aievaluation.HasInferenceValue || (this.HasInferenceValue && !this.InferenceValue.Equals(aievaluation.InferenceValue)))
			{
				return false;
			}
			if (this.HasHeuristicValue != aievaluation.HasHeuristicValue || (this.HasHeuristicValue && !this.HeuristicValue.Equals(aievaluation.HeuristicValue)))
			{
				return false;
			}
			if (this.HasSubtreeValue != aievaluation.HasSubtreeValue || (this.HasSubtreeValue && !this.SubtreeValue.Equals(aievaluation.SubtreeValue)))
			{
				return false;
			}
			if (this.PositionScores.Count != aievaluation.PositionScores.Count)
			{
				return false;
			}
			for (int k = 0; k < this.PositionScores.Count; k++)
			{
				if (!this.PositionScores[k].Equals(aievaluation.PositionScores[k]))
				{
					return false;
				}
			}
			return this.HasEdgeCount == aievaluation.HasEdgeCount && (!this.HasEdgeCount || this.EdgeCount.Equals(aievaluation.EdgeCount));
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000593A0 File Offset: 0x000575A0
		public void Deserialize(Stream stream)
		{
			AIEvaluation.Deserialize(stream, this);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000593AA File Offset: 0x000575AA
		public static AIEvaluation Deserialize(Stream stream, AIEvaluation instance)
		{
			return AIEvaluation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000593B8 File Offset: 0x000575B8
		public static AIEvaluation DeserializeLengthDelimited(Stream stream)
		{
			AIEvaluation aievaluation = new AIEvaluation();
			AIEvaluation.DeserializeLengthDelimited(stream, aievaluation);
			return aievaluation;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000593D4 File Offset: 0x000575D4
		public static AIEvaluation DeserializeLengthDelimited(Stream stream, AIEvaluation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AIEvaluation.Deserialize(stream, instance, num);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000593FC File Offset: 0x000575FC
		public static AIEvaluation Deserialize(Stream stream, AIEvaluation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.BaseScore = 0;
			instance.BonusScore = 0;
			instance.OptionChosen = false;
			if (instance.TargetScores == null)
			{
				instance.TargetScores = new List<AITarget>();
			}
			if (instance.ContextualScore == null)
			{
				instance.ContextualScore = new List<AIContextualValue>();
			}
			instance.PriorProbability = 0f;
			instance.PuctValue = 0f;
			instance.FinalQValue = 0f;
			instance.FinalVisitCount = 0;
			instance.SubtreeDepth = 0;
			instance.InferenceValue = 0f;
			instance.HeuristicValue = 0f;
			instance.SubtreeValue = 0f;
			if (instance.PositionScores == null)
			{
				instance.PositionScores = new List<AIPosition>();
			}
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
					if (num <= 58)
					{
						if (num <= 24)
						{
							if (num == 10)
							{
								instance.OptionName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.BaseScore = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.BonusScore = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.OptionChosen = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 50)
							{
								instance.TargetScores.Add(AITarget.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 58)
							{
								instance.ContextualScore.Add(AIContextualValue.DeserializeLengthDelimited(stream));
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 77)
						{
							if (num == 69)
							{
								instance.PriorProbability = binaryReader.ReadSingle();
								continue;
							}
							if (num == 77)
							{
								instance.PuctValue = binaryReader.ReadSingle();
								continue;
							}
						}
						else
						{
							if (num == 85)
							{
								instance.FinalQValue = binaryReader.ReadSingle();
								continue;
							}
							if (num == 88)
							{
								instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 109)
					{
						if (num == 96)
						{
							instance.SubtreeDepth = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 109)
						{
							instance.InferenceValue = binaryReader.ReadSingle();
							continue;
						}
					}
					else
					{
						if (num == 117)
						{
							instance.HeuristicValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 125)
						{
							instance.SubtreeValue = binaryReader.ReadSingle();
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 16U)
					{
						if (field != 17U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.EdgeCount = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.PositionScores.Add(AIPosition.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00059730 File Offset: 0x00057930
		public void Serialize(Stream stream)
		{
			AIEvaluation.Serialize(stream, this);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0005973C File Offset: 0x0005793C
		public static void Serialize(Stream stream, AIEvaluation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.OptionName == null)
			{
				throw new ArgumentNullException("OptionName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OptionName));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EntityID));
			if (instance.HasBaseScore)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BaseScore));
			}
			if (instance.HasBonusScore)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BonusScore));
			}
			if (instance.HasOptionChosen)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.OptionChosen);
			}
			if (instance.TargetScores.Count > 0)
			{
				foreach (AITarget aitarget in instance.TargetScores)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, aitarget.GetSerializedSize());
					AITarget.Serialize(stream, aitarget);
				}
			}
			if (instance.ContextualScore.Count > 0)
			{
				foreach (AIContextualValue aicontextualValue in instance.ContextualScore)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, aicontextualValue.GetSerializedSize());
					AIContextualValue.Serialize(stream, aicontextualValue);
				}
			}
			if (instance.HasPriorProbability)
			{
				stream.WriteByte(69);
				binaryWriter.Write(instance.PriorProbability);
			}
			if (instance.HasPuctValue)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.PuctValue);
			}
			if (instance.HasFinalQValue)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.FinalQValue);
			}
			if (instance.HasFinalVisitCount)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FinalVisitCount));
			}
			if (instance.HasSubtreeDepth)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubtreeDepth));
			}
			if (instance.HasInferenceValue)
			{
				stream.WriteByte(109);
				binaryWriter.Write(instance.InferenceValue);
			}
			if (instance.HasHeuristicValue)
			{
				stream.WriteByte(117);
				binaryWriter.Write(instance.HeuristicValue);
			}
			if (instance.HasSubtreeValue)
			{
				stream.WriteByte(125);
				binaryWriter.Write(instance.SubtreeValue);
			}
			if (instance.PositionScores.Count > 0)
			{
				foreach (AIPosition aiposition in instance.PositionScores)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, aiposition.GetSerializedSize());
					AIPosition.Serialize(stream, aiposition);
				}
			}
			if (instance.HasEdgeCount)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EdgeCount));
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00059A3C File Offset: 0x00057C3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.OptionName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EntityID));
			if (this.HasBaseScore)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BaseScore));
			}
			if (this.HasBonusScore)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BonusScore));
			}
			if (this.HasOptionChosen)
			{
				num += 1U;
				num += 1U;
			}
			if (this.TargetScores.Count > 0)
			{
				foreach (AITarget aitarget in this.TargetScores)
				{
					num += 1U;
					uint serializedSize = aitarget.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ContextualScore.Count > 0)
			{
				foreach (AIContextualValue aicontextualValue in this.ContextualScore)
				{
					num += 1U;
					uint serializedSize2 = aicontextualValue.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
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
			if (this.HasSubtreeDepth)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubtreeDepth));
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
			if (this.PositionScores.Count > 0)
			{
				foreach (AIPosition aiposition in this.PositionScores)
				{
					num += 2U;
					uint serializedSize3 = aiposition.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasEdgeCount)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EdgeCount));
			}
			num += 2U;
			return num;
		}

		// Token: 0x0400096D RID: 2413
		public bool HasBaseScore;

		// Token: 0x0400096E RID: 2414
		private int _BaseScore;

		// Token: 0x0400096F RID: 2415
		public bool HasBonusScore;

		// Token: 0x04000970 RID: 2416
		private int _BonusScore;

		// Token: 0x04000971 RID: 2417
		public bool HasOptionChosen;

		// Token: 0x04000972 RID: 2418
		private bool _OptionChosen;

		// Token: 0x04000973 RID: 2419
		private List<AITarget> _TargetScores = new List<AITarget>();

		// Token: 0x04000974 RID: 2420
		private List<AIContextualValue> _ContextualScore = new List<AIContextualValue>();

		// Token: 0x04000975 RID: 2421
		public bool HasPriorProbability;

		// Token: 0x04000976 RID: 2422
		private float _PriorProbability;

		// Token: 0x04000977 RID: 2423
		public bool HasPuctValue;

		// Token: 0x04000978 RID: 2424
		private float _PuctValue;

		// Token: 0x04000979 RID: 2425
		public bool HasFinalQValue;

		// Token: 0x0400097A RID: 2426
		private float _FinalQValue;

		// Token: 0x0400097B RID: 2427
		public bool HasFinalVisitCount;

		// Token: 0x0400097C RID: 2428
		private int _FinalVisitCount;

		// Token: 0x0400097D RID: 2429
		public bool HasSubtreeDepth;

		// Token: 0x0400097E RID: 2430
		private int _SubtreeDepth;

		// Token: 0x0400097F RID: 2431
		public bool HasInferenceValue;

		// Token: 0x04000980 RID: 2432
		private float _InferenceValue;

		// Token: 0x04000981 RID: 2433
		public bool HasHeuristicValue;

		// Token: 0x04000982 RID: 2434
		private float _HeuristicValue;

		// Token: 0x04000983 RID: 2435
		public bool HasSubtreeValue;

		// Token: 0x04000984 RID: 2436
		private float _SubtreeValue;

		// Token: 0x04000985 RID: 2437
		private List<AIPosition> _PositionScores = new List<AIPosition>();

		// Token: 0x04000986 RID: 2438
		public bool HasEdgeCount;

		// Token: 0x04000987 RID: 2439
		private int _EdgeCount;
	}
}
