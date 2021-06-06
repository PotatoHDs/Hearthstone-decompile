using System.IO;

namespace PegasusGame
{
	public class AIPosition : IProtoBuf
	{
		public bool HasPosition;

		private int _Position;

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

		public int Position
		{
			get
			{
				return _Position;
			}
			set
			{
				_Position = value;
				HasPosition = true;
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
			int num = GetType().GetHashCode();
			if (HasPosition)
			{
				num ^= Position.GetHashCode();
			}
			if (HasPriorProbability)
			{
				num ^= PriorProbability.GetHashCode();
			}
			if (HasPuctValue)
			{
				num ^= PuctValue.GetHashCode();
			}
			if (HasFinalQValue)
			{
				num ^= FinalQValue.GetHashCode();
			}
			if (HasFinalVisitCount)
			{
				num ^= FinalVisitCount.GetHashCode();
			}
			if (HasInferenceValue)
			{
				num ^= InferenceValue.GetHashCode();
			}
			if (HasHeuristicValue)
			{
				num ^= HeuristicValue.GetHashCode();
			}
			if (HasSubtreeValue)
			{
				num ^= SubtreeValue.GetHashCode();
			}
			if (HasEdgeCount)
			{
				num ^= EdgeCount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AIPosition aIPosition = obj as AIPosition;
			if (aIPosition == null)
			{
				return false;
			}
			if (HasPosition != aIPosition.HasPosition || (HasPosition && !Position.Equals(aIPosition.Position)))
			{
				return false;
			}
			if (HasPriorProbability != aIPosition.HasPriorProbability || (HasPriorProbability && !PriorProbability.Equals(aIPosition.PriorProbability)))
			{
				return false;
			}
			if (HasPuctValue != aIPosition.HasPuctValue || (HasPuctValue && !PuctValue.Equals(aIPosition.PuctValue)))
			{
				return false;
			}
			if (HasFinalQValue != aIPosition.HasFinalQValue || (HasFinalQValue && !FinalQValue.Equals(aIPosition.FinalQValue)))
			{
				return false;
			}
			if (HasFinalVisitCount != aIPosition.HasFinalVisitCount || (HasFinalVisitCount && !FinalVisitCount.Equals(aIPosition.FinalVisitCount)))
			{
				return false;
			}
			if (HasInferenceValue != aIPosition.HasInferenceValue || (HasInferenceValue && !InferenceValue.Equals(aIPosition.InferenceValue)))
			{
				return false;
			}
			if (HasHeuristicValue != aIPosition.HasHeuristicValue || (HasHeuristicValue && !HeuristicValue.Equals(aIPosition.HeuristicValue)))
			{
				return false;
			}
			if (HasSubtreeValue != aIPosition.HasSubtreeValue || (HasSubtreeValue && !SubtreeValue.Equals(aIPosition.SubtreeValue)))
			{
				return false;
			}
			if (HasEdgeCount != aIPosition.HasEdgeCount || (HasEdgeCount && !EdgeCount.Equals(aIPosition.EdgeCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AIPosition Deserialize(Stream stream, AIPosition instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AIPosition DeserializeLengthDelimited(Stream stream)
		{
			AIPosition aIPosition = new AIPosition();
			DeserializeLengthDelimited(stream, aIPosition);
			return aIPosition;
		}

		public static AIPosition DeserializeLengthDelimited(Stream stream, AIPosition instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
					instance.Position = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 21:
					instance.PriorProbability = binaryReader.ReadSingle();
					continue;
				case 29:
					instance.PuctValue = binaryReader.ReadSingle();
					continue;
				case 37:
					instance.FinalQValue = binaryReader.ReadSingle();
					continue;
				case 40:
					instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 53:
					instance.InferenceValue = binaryReader.ReadSingle();
					continue;
				case 61:
					instance.HeuristicValue = binaryReader.ReadSingle();
					continue;
				case 69:
					instance.SubtreeValue = binaryReader.ReadSingle();
					continue;
				case 72:
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

		public static void Serialize(Stream stream, AIPosition instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPosition)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Position);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FinalVisitCount);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EdgeCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPosition)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Position);
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
			return num;
		}
	}
}
