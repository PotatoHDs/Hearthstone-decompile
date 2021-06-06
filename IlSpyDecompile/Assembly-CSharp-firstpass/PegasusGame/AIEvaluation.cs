using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class AIEvaluation : IProtoBuf
	{
		public bool HasBaseScore;

		private int _BaseScore;

		public bool HasBonusScore;

		private int _BonusScore;

		public bool HasOptionChosen;

		private bool _OptionChosen;

		private List<AITarget> _TargetScores = new List<AITarget>();

		private List<AIContextualValue> _ContextualScore = new List<AIContextualValue>();

		public bool HasPriorProbability;

		private float _PriorProbability;

		public bool HasPuctValue;

		private float _PuctValue;

		public bool HasFinalQValue;

		private float _FinalQValue;

		public bool HasFinalVisitCount;

		private int _FinalVisitCount;

		public bool HasSubtreeDepth;

		private int _SubtreeDepth;

		public bool HasInferenceValue;

		private float _InferenceValue;

		public bool HasHeuristicValue;

		private float _HeuristicValue;

		public bool HasSubtreeValue;

		private float _SubtreeValue;

		private List<AIPosition> _PositionScores = new List<AIPosition>();

		public bool HasEdgeCount;

		private int _EdgeCount;

		public string OptionName { get; set; }

		public int EntityID { get; set; }

		public int BaseScore
		{
			get
			{
				return _BaseScore;
			}
			set
			{
				_BaseScore = value;
				HasBaseScore = true;
			}
		}

		public int BonusScore
		{
			get
			{
				return _BonusScore;
			}
			set
			{
				_BonusScore = value;
				HasBonusScore = true;
			}
		}

		public bool OptionChosen
		{
			get
			{
				return _OptionChosen;
			}
			set
			{
				_OptionChosen = value;
				HasOptionChosen = true;
			}
		}

		public List<AITarget> TargetScores
		{
			get
			{
				return _TargetScores;
			}
			set
			{
				_TargetScores = value;
			}
		}

		public List<AIContextualValue> ContextualScore
		{
			get
			{
				return _ContextualScore;
			}
			set
			{
				_ContextualScore = value;
			}
		}

		public float PriorProbability
		{
			get
			{
				return _PriorProbability;
			}
			set
			{
				_PriorProbability = value;
				HasPriorProbability = true;
			}
		}

		public float PuctValue
		{
			get
			{
				return _PuctValue;
			}
			set
			{
				_PuctValue = value;
				HasPuctValue = true;
			}
		}

		public float FinalQValue
		{
			get
			{
				return _FinalQValue;
			}
			set
			{
				_FinalQValue = value;
				HasFinalQValue = true;
			}
		}

		public int FinalVisitCount
		{
			get
			{
				return _FinalVisitCount;
			}
			set
			{
				_FinalVisitCount = value;
				HasFinalVisitCount = true;
			}
		}

		public int SubtreeDepth
		{
			get
			{
				return _SubtreeDepth;
			}
			set
			{
				_SubtreeDepth = value;
				HasSubtreeDepth = true;
			}
		}

		public float InferenceValue
		{
			get
			{
				return _InferenceValue;
			}
			set
			{
				_InferenceValue = value;
				HasInferenceValue = true;
			}
		}

		public float HeuristicValue
		{
			get
			{
				return _HeuristicValue;
			}
			set
			{
				_HeuristicValue = value;
				HasHeuristicValue = true;
			}
		}

		public float SubtreeValue
		{
			get
			{
				return _SubtreeValue;
			}
			set
			{
				_SubtreeValue = value;
				HasSubtreeValue = true;
			}
		}

		public List<AIPosition> PositionScores
		{
			get
			{
				return _PositionScores;
			}
			set
			{
				_PositionScores = value;
			}
		}

		public int EdgeCount
		{
			get
			{
				return _EdgeCount;
			}
			set
			{
				_EdgeCount = value;
				HasEdgeCount = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= OptionName.GetHashCode();
			hashCode ^= EntityID.GetHashCode();
			if (HasBaseScore)
			{
				hashCode ^= BaseScore.GetHashCode();
			}
			if (HasBonusScore)
			{
				hashCode ^= BonusScore.GetHashCode();
			}
			if (HasOptionChosen)
			{
				hashCode ^= OptionChosen.GetHashCode();
			}
			foreach (AITarget targetScore in TargetScores)
			{
				hashCode ^= targetScore.GetHashCode();
			}
			foreach (AIContextualValue item in ContextualScore)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasPriorProbability)
			{
				hashCode ^= PriorProbability.GetHashCode();
			}
			if (HasPuctValue)
			{
				hashCode ^= PuctValue.GetHashCode();
			}
			if (HasFinalQValue)
			{
				hashCode ^= FinalQValue.GetHashCode();
			}
			if (HasFinalVisitCount)
			{
				hashCode ^= FinalVisitCount.GetHashCode();
			}
			if (HasSubtreeDepth)
			{
				hashCode ^= SubtreeDepth.GetHashCode();
			}
			if (HasInferenceValue)
			{
				hashCode ^= InferenceValue.GetHashCode();
			}
			if (HasHeuristicValue)
			{
				hashCode ^= HeuristicValue.GetHashCode();
			}
			if (HasSubtreeValue)
			{
				hashCode ^= SubtreeValue.GetHashCode();
			}
			foreach (AIPosition positionScore in PositionScores)
			{
				hashCode ^= positionScore.GetHashCode();
			}
			if (HasEdgeCount)
			{
				hashCode ^= EdgeCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AIEvaluation aIEvaluation = obj as AIEvaluation;
			if (aIEvaluation == null)
			{
				return false;
			}
			if (!OptionName.Equals(aIEvaluation.OptionName))
			{
				return false;
			}
			if (!EntityID.Equals(aIEvaluation.EntityID))
			{
				return false;
			}
			if (HasBaseScore != aIEvaluation.HasBaseScore || (HasBaseScore && !BaseScore.Equals(aIEvaluation.BaseScore)))
			{
				return false;
			}
			if (HasBonusScore != aIEvaluation.HasBonusScore || (HasBonusScore && !BonusScore.Equals(aIEvaluation.BonusScore)))
			{
				return false;
			}
			if (HasOptionChosen != aIEvaluation.HasOptionChosen || (HasOptionChosen && !OptionChosen.Equals(aIEvaluation.OptionChosen)))
			{
				return false;
			}
			if (TargetScores.Count != aIEvaluation.TargetScores.Count)
			{
				return false;
			}
			for (int i = 0; i < TargetScores.Count; i++)
			{
				if (!TargetScores[i].Equals(aIEvaluation.TargetScores[i]))
				{
					return false;
				}
			}
			if (ContextualScore.Count != aIEvaluation.ContextualScore.Count)
			{
				return false;
			}
			for (int j = 0; j < ContextualScore.Count; j++)
			{
				if (!ContextualScore[j].Equals(aIEvaluation.ContextualScore[j]))
				{
					return false;
				}
			}
			if (HasPriorProbability != aIEvaluation.HasPriorProbability || (HasPriorProbability && !PriorProbability.Equals(aIEvaluation.PriorProbability)))
			{
				return false;
			}
			if (HasPuctValue != aIEvaluation.HasPuctValue || (HasPuctValue && !PuctValue.Equals(aIEvaluation.PuctValue)))
			{
				return false;
			}
			if (HasFinalQValue != aIEvaluation.HasFinalQValue || (HasFinalQValue && !FinalQValue.Equals(aIEvaluation.FinalQValue)))
			{
				return false;
			}
			if (HasFinalVisitCount != aIEvaluation.HasFinalVisitCount || (HasFinalVisitCount && !FinalVisitCount.Equals(aIEvaluation.FinalVisitCount)))
			{
				return false;
			}
			if (HasSubtreeDepth != aIEvaluation.HasSubtreeDepth || (HasSubtreeDepth && !SubtreeDepth.Equals(aIEvaluation.SubtreeDepth)))
			{
				return false;
			}
			if (HasInferenceValue != aIEvaluation.HasInferenceValue || (HasInferenceValue && !InferenceValue.Equals(aIEvaluation.InferenceValue)))
			{
				return false;
			}
			if (HasHeuristicValue != aIEvaluation.HasHeuristicValue || (HasHeuristicValue && !HeuristicValue.Equals(aIEvaluation.HeuristicValue)))
			{
				return false;
			}
			if (HasSubtreeValue != aIEvaluation.HasSubtreeValue || (HasSubtreeValue && !SubtreeValue.Equals(aIEvaluation.SubtreeValue)))
			{
				return false;
			}
			if (PositionScores.Count != aIEvaluation.PositionScores.Count)
			{
				return false;
			}
			for (int k = 0; k < PositionScores.Count; k++)
			{
				if (!PositionScores[k].Equals(aIEvaluation.PositionScores[k]))
				{
					return false;
				}
			}
			if (HasEdgeCount != aIEvaluation.HasEdgeCount || (HasEdgeCount && !EdgeCount.Equals(aIEvaluation.EdgeCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AIEvaluation Deserialize(Stream stream, AIEvaluation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AIEvaluation DeserializeLengthDelimited(Stream stream)
		{
			AIEvaluation aIEvaluation = new AIEvaluation();
			DeserializeLengthDelimited(stream, aIEvaluation);
			return aIEvaluation;
		}

		public static AIEvaluation DeserializeLengthDelimited(Stream stream, AIEvaluation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.OptionName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BaseScore = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.BonusScore = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.OptionChosen = ProtocolParser.ReadBool(stream);
					continue;
				case 50:
					instance.TargetScores.Add(AITarget.DeserializeLengthDelimited(stream));
					continue;
				case 58:
					instance.ContextualScore.Add(AIContextualValue.DeserializeLengthDelimited(stream));
					continue;
				case 69:
					instance.PriorProbability = binaryReader.ReadSingle();
					continue;
				case 77:
					instance.PuctValue = binaryReader.ReadSingle();
					continue;
				case 85:
					instance.FinalQValue = binaryReader.ReadSingle();
					continue;
				case 88:
					instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.SubtreeDepth = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 109:
					instance.InferenceValue = binaryReader.ReadSingle();
					continue;
				case 117:
					instance.HeuristicValue = binaryReader.ReadSingle();
					continue;
				case 125:
					instance.SubtreeValue = binaryReader.ReadSingle();
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.PositionScores.Add(AIPosition.DeserializeLengthDelimited(stream));
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.EdgeCount = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EntityID);
			if (instance.HasBaseScore)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BaseScore);
			}
			if (instance.HasBonusScore)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BonusScore);
			}
			if (instance.HasOptionChosen)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.OptionChosen);
			}
			if (instance.TargetScores.Count > 0)
			{
				foreach (AITarget targetScore in instance.TargetScores)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, targetScore.GetSerializedSize());
					AITarget.Serialize(stream, targetScore);
				}
			}
			if (instance.ContextualScore.Count > 0)
			{
				foreach (AIContextualValue item in instance.ContextualScore)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					AIContextualValue.Serialize(stream, item);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FinalVisitCount);
			}
			if (instance.HasSubtreeDepth)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SubtreeDepth);
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
				foreach (AIPosition positionScore in instance.PositionScores)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, positionScore.GetSerializedSize());
					AIPosition.Serialize(stream, positionScore);
				}
			}
			if (instance.HasEdgeCount)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EdgeCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(OptionName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)EntityID);
			if (HasBaseScore)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BaseScore);
			}
			if (HasBonusScore)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BonusScore);
			}
			if (HasOptionChosen)
			{
				num++;
				num++;
			}
			if (TargetScores.Count > 0)
			{
				foreach (AITarget targetScore in TargetScores)
				{
					num++;
					uint serializedSize = targetScore.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (ContextualScore.Count > 0)
			{
				foreach (AIContextualValue item in ContextualScore)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasPriorProbability)
			{
				num++;
				num += 4;
			}
			if (HasPuctValue)
			{
				num++;
				num += 4;
			}
			if (HasFinalQValue)
			{
				num++;
				num += 4;
			}
			if (HasFinalVisitCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FinalVisitCount);
			}
			if (HasSubtreeDepth)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SubtreeDepth);
			}
			if (HasInferenceValue)
			{
				num++;
				num += 4;
			}
			if (HasHeuristicValue)
			{
				num++;
				num += 4;
			}
			if (HasSubtreeValue)
			{
				num++;
				num += 4;
			}
			if (PositionScores.Count > 0)
			{
				foreach (AIPosition positionScore in PositionScores)
				{
					num += 2;
					uint serializedSize3 = positionScore.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasEdgeCount)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)EdgeCount);
			}
			return num + 2;
		}
	}
}
