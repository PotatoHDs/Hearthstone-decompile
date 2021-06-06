using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class AIDebugInformation : IProtoBuf
	{
		public enum PacketID
		{
			ID = 6
		}

		private List<AIEvaluation> _Evaluations = new List<AIEvaluation>();

		public bool HasRootValue;

		private float _RootValue;

		public bool HasInferenceValue;

		private float _InferenceValue;

		public bool HasHeuristicValue;

		private float _HeuristicValue;

		public bool HasSubtreeValue;

		private float _SubtreeValue;

		public bool HasTotalVisits;

		private int _TotalVisits;

		public bool HasSubtreeDepth;

		private int _SubtreeDepth;

		public bool HasUniqueNodes;

		private int _UniqueNodes;

		public bool HasDebugIteration;

		private int _DebugIteration;

		public bool HasDebugDepth;

		private int _DebugDepth;

		public bool HasModelVersion;

		private long _ModelVersion;

		public int TurnID { get; set; }

		public int MoveID { get; set; }

		public List<AIEvaluation> Evaluations
		{
			get
			{
				return _Evaluations;
			}
			set
			{
				_Evaluations = value;
			}
		}

		public float RootValue
		{
			get
			{
				return _RootValue;
			}
			set
			{
				_RootValue = value;
				HasRootValue = true;
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

		public int TotalVisits
		{
			get
			{
				return _TotalVisits;
			}
			set
			{
				_TotalVisits = value;
				HasTotalVisits = true;
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

		public int UniqueNodes
		{
			get
			{
				return _UniqueNodes;
			}
			set
			{
				_UniqueNodes = value;
				HasUniqueNodes = true;
			}
		}

		public int DebugIteration
		{
			get
			{
				return _DebugIteration;
			}
			set
			{
				_DebugIteration = value;
				HasDebugIteration = true;
			}
		}

		public int DebugDepth
		{
			get
			{
				return _DebugDepth;
			}
			set
			{
				_DebugDepth = value;
				HasDebugDepth = true;
			}
		}

		public long ModelVersion
		{
			get
			{
				return _ModelVersion;
			}
			set
			{
				_ModelVersion = value;
				HasModelVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= TurnID.GetHashCode();
			hashCode ^= MoveID.GetHashCode();
			foreach (AIEvaluation evaluation in Evaluations)
			{
				hashCode ^= evaluation.GetHashCode();
			}
			if (HasRootValue)
			{
				hashCode ^= RootValue.GetHashCode();
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
			if (HasTotalVisits)
			{
				hashCode ^= TotalVisits.GetHashCode();
			}
			if (HasSubtreeDepth)
			{
				hashCode ^= SubtreeDepth.GetHashCode();
			}
			if (HasUniqueNodes)
			{
				hashCode ^= UniqueNodes.GetHashCode();
			}
			if (HasDebugIteration)
			{
				hashCode ^= DebugIteration.GetHashCode();
			}
			if (HasDebugDepth)
			{
				hashCode ^= DebugDepth.GetHashCode();
			}
			if (HasModelVersion)
			{
				hashCode ^= ModelVersion.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AIDebugInformation aIDebugInformation = obj as AIDebugInformation;
			if (aIDebugInformation == null)
			{
				return false;
			}
			if (!TurnID.Equals(aIDebugInformation.TurnID))
			{
				return false;
			}
			if (!MoveID.Equals(aIDebugInformation.MoveID))
			{
				return false;
			}
			if (Evaluations.Count != aIDebugInformation.Evaluations.Count)
			{
				return false;
			}
			for (int i = 0; i < Evaluations.Count; i++)
			{
				if (!Evaluations[i].Equals(aIDebugInformation.Evaluations[i]))
				{
					return false;
				}
			}
			if (HasRootValue != aIDebugInformation.HasRootValue || (HasRootValue && !RootValue.Equals(aIDebugInformation.RootValue)))
			{
				return false;
			}
			if (HasInferenceValue != aIDebugInformation.HasInferenceValue || (HasInferenceValue && !InferenceValue.Equals(aIDebugInformation.InferenceValue)))
			{
				return false;
			}
			if (HasHeuristicValue != aIDebugInformation.HasHeuristicValue || (HasHeuristicValue && !HeuristicValue.Equals(aIDebugInformation.HeuristicValue)))
			{
				return false;
			}
			if (HasSubtreeValue != aIDebugInformation.HasSubtreeValue || (HasSubtreeValue && !SubtreeValue.Equals(aIDebugInformation.SubtreeValue)))
			{
				return false;
			}
			if (HasTotalVisits != aIDebugInformation.HasTotalVisits || (HasTotalVisits && !TotalVisits.Equals(aIDebugInformation.TotalVisits)))
			{
				return false;
			}
			if (HasSubtreeDepth != aIDebugInformation.HasSubtreeDepth || (HasSubtreeDepth && !SubtreeDepth.Equals(aIDebugInformation.SubtreeDepth)))
			{
				return false;
			}
			if (HasUniqueNodes != aIDebugInformation.HasUniqueNodes || (HasUniqueNodes && !UniqueNodes.Equals(aIDebugInformation.UniqueNodes)))
			{
				return false;
			}
			if (HasDebugIteration != aIDebugInformation.HasDebugIteration || (HasDebugIteration && !DebugIteration.Equals(aIDebugInformation.DebugIteration)))
			{
				return false;
			}
			if (HasDebugDepth != aIDebugInformation.HasDebugDepth || (HasDebugDepth && !DebugDepth.Equals(aIDebugInformation.DebugDepth)))
			{
				return false;
			}
			if (HasModelVersion != aIDebugInformation.HasModelVersion || (HasModelVersion && !ModelVersion.Equals(aIDebugInformation.ModelVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AIDebugInformation Deserialize(Stream stream, AIDebugInformation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AIDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			AIDebugInformation aIDebugInformation = new AIDebugInformation();
			DeserializeLengthDelimited(stream, aIDebugInformation);
			return aIDebugInformation;
		}

		public static AIDebugInformation DeserializeLengthDelimited(Stream stream, AIDebugInformation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
				case 8:
					instance.TurnID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.MoveID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Evaluations.Add(AIEvaluation.DeserializeLengthDelimited(stream));
					continue;
				case 37:
					instance.RootValue = binaryReader.ReadSingle();
					continue;
				case 45:
					instance.InferenceValue = binaryReader.ReadSingle();
					continue;
				case 53:
					instance.HeuristicValue = binaryReader.ReadSingle();
					continue;
				case 61:
					instance.SubtreeValue = binaryReader.ReadSingle();
					continue;
				case 64:
					instance.TotalVisits = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.SubtreeDepth = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.UniqueNodes = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.DebugIteration = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.DebugDepth = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.ModelVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AIDebugInformation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TurnID);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MoveID);
			if (instance.Evaluations.Count > 0)
			{
				foreach (AIEvaluation evaluation in instance.Evaluations)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, evaluation.GetSerializedSize());
					AIEvaluation.Serialize(stream, evaluation);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalVisits);
			}
			if (instance.HasSubtreeDepth)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SubtreeDepth);
			}
			if (instance.HasUniqueNodes)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UniqueNodes);
			}
			if (instance.HasDebugIteration)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DebugIteration);
			}
			if (instance.HasDebugDepth)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DebugDepth);
			}
			if (instance.HasModelVersion)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ModelVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)TurnID);
			num += ProtocolParser.SizeOfUInt64((ulong)MoveID);
			if (Evaluations.Count > 0)
			{
				foreach (AIEvaluation evaluation in Evaluations)
				{
					num++;
					uint serializedSize = evaluation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasRootValue)
			{
				num++;
				num += 4;
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
			if (HasTotalVisits)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalVisits);
			}
			if (HasSubtreeDepth)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SubtreeDepth);
			}
			if (HasUniqueNodes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)UniqueNodes);
			}
			if (HasDebugIteration)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DebugIteration);
			}
			if (HasDebugDepth)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DebugDepth);
			}
			if (HasModelVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ModelVersion);
			}
			return num + 2;
		}
	}
}
