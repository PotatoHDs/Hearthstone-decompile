using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class AITarget : IProtoBuf
	{
		public bool HasTargetScore;

		private int _TargetScore;

		public bool HasTargetChosen;

		private bool _TargetChosen;

		public bool HasPriorProbability;

		private float _PriorProbability;

		public bool HasPuctValue;

		private float _PuctValue;

		public bool HasFinalQValue;

		private float _FinalQValue;

		public bool HasFinalVisitCount;

		private int _FinalVisitCount;

		public bool HasInferenceValue;

		private float _InferenceValue;

		public bool HasHeuristicValue;

		private float _HeuristicValue;

		public bool HasSubtreeValue;

		private float _SubtreeValue;

		public bool HasEdgeCount;

		private int _EdgeCount;

		public string EntityName { get; set; }

		public int EntityID { get; set; }

		public int TargetScore
		{
			get
			{
				return _TargetScore;
			}
			set
			{
				_TargetScore = value;
				HasTargetScore = true;
			}
		}

		public bool TargetChosen
		{
			get
			{
				return _TargetChosen;
			}
			set
			{
				_TargetChosen = value;
				HasTargetChosen = true;
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
			hashCode ^= EntityName.GetHashCode();
			hashCode ^= EntityID.GetHashCode();
			if (HasTargetScore)
			{
				hashCode ^= TargetScore.GetHashCode();
			}
			if (HasTargetChosen)
			{
				hashCode ^= TargetChosen.GetHashCode();
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
			if (HasEdgeCount)
			{
				hashCode ^= EdgeCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AITarget aITarget = obj as AITarget;
			if (aITarget == null)
			{
				return false;
			}
			if (!EntityName.Equals(aITarget.EntityName))
			{
				return false;
			}
			if (!EntityID.Equals(aITarget.EntityID))
			{
				return false;
			}
			if (HasTargetScore != aITarget.HasTargetScore || (HasTargetScore && !TargetScore.Equals(aITarget.TargetScore)))
			{
				return false;
			}
			if (HasTargetChosen != aITarget.HasTargetChosen || (HasTargetChosen && !TargetChosen.Equals(aITarget.TargetChosen)))
			{
				return false;
			}
			if (HasPriorProbability != aITarget.HasPriorProbability || (HasPriorProbability && !PriorProbability.Equals(aITarget.PriorProbability)))
			{
				return false;
			}
			if (HasPuctValue != aITarget.HasPuctValue || (HasPuctValue && !PuctValue.Equals(aITarget.PuctValue)))
			{
				return false;
			}
			if (HasFinalQValue != aITarget.HasFinalQValue || (HasFinalQValue && !FinalQValue.Equals(aITarget.FinalQValue)))
			{
				return false;
			}
			if (HasFinalVisitCount != aITarget.HasFinalVisitCount || (HasFinalVisitCount && !FinalVisitCount.Equals(aITarget.FinalVisitCount)))
			{
				return false;
			}
			if (HasInferenceValue != aITarget.HasInferenceValue || (HasInferenceValue && !InferenceValue.Equals(aITarget.InferenceValue)))
			{
				return false;
			}
			if (HasHeuristicValue != aITarget.HasHeuristicValue || (HasHeuristicValue && !HeuristicValue.Equals(aITarget.HeuristicValue)))
			{
				return false;
			}
			if (HasSubtreeValue != aITarget.HasSubtreeValue || (HasSubtreeValue && !SubtreeValue.Equals(aITarget.SubtreeValue)))
			{
				return false;
			}
			if (HasEdgeCount != aITarget.HasEdgeCount || (HasEdgeCount && !EdgeCount.Equals(aITarget.EdgeCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AITarget Deserialize(Stream stream, AITarget instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AITarget DeserializeLengthDelimited(Stream stream)
		{
			AITarget aITarget = new AITarget();
			DeserializeLengthDelimited(stream, aITarget);
			return aITarget;
		}

		public static AITarget DeserializeLengthDelimited(Stream stream, AITarget instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AITarget Deserialize(Stream stream, AITarget instance, long limit)
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
					instance.EntityName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.TargetScore = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.TargetChosen = ProtocolParser.ReadBool(stream);
					continue;
				case 45:
					instance.PriorProbability = binaryReader.ReadSingle();
					continue;
				case 53:
					instance.PuctValue = binaryReader.ReadSingle();
					continue;
				case 61:
					instance.FinalQValue = binaryReader.ReadSingle();
					continue;
				case 64:
					instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 77:
					instance.InferenceValue = binaryReader.ReadSingle();
					continue;
				case 85:
					instance.HeuristicValue = binaryReader.ReadSingle();
					continue;
				case 93:
					instance.SubtreeValue = binaryReader.ReadSingle();
					continue;
				case 96:
					instance.EdgeCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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

		public static void Serialize(Stream stream, AITarget instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EntityID);
			if (instance.HasTargetScore)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TargetScore);
			}
			if (instance.HasTargetChosen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.TargetChosen);
			}
			if (instance.HasPriorProbability)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.PriorProbability);
			}
			if (instance.HasPuctValue)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.PuctValue);
			}
			if (instance.HasFinalQValue)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.FinalQValue);
			}
			if (instance.HasFinalVisitCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FinalVisitCount);
			}
			if (instance.HasInferenceValue)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.InferenceValue);
			}
			if (instance.HasHeuristicValue)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.HeuristicValue);
			}
			if (instance.HasSubtreeValue)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.SubtreeValue);
			}
			if (instance.HasEdgeCount)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EdgeCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(EntityName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)EntityID);
			if (HasTargetScore)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TargetScore);
			}
			if (HasTargetChosen)
			{
				num++;
				num++;
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
			if (HasEdgeCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)EdgeCount);
			}
			return num + 2;
		}
	}
}
