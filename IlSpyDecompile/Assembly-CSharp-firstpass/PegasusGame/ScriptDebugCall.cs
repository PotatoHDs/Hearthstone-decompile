using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class ScriptDebugCall : IProtoBuf
	{
		private List<ScriptDebugVariable> _Inputs = new List<ScriptDebugVariable>();

		private List<string> _ErrorStrings = new List<string>();

		private List<ScriptDebugVariable> _Variables = new List<ScriptDebugVariable>();

		public string OpcodeName { get; set; }

		public ScriptDebugVariable Output { get; set; }

		public List<ScriptDebugVariable> Inputs
		{
			get
			{
				return _Inputs;
			}
			set
			{
				_Inputs = value;
			}
		}

		public List<string> ErrorStrings
		{
			get
			{
				return _ErrorStrings;
			}
			set
			{
				_ErrorStrings = value;
			}
		}

		public List<ScriptDebugVariable> Variables
		{
			get
			{
				return _Variables;
			}
			set
			{
				_Variables = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= OpcodeName.GetHashCode();
			hashCode ^= Output.GetHashCode();
			foreach (ScriptDebugVariable input in Inputs)
			{
				hashCode ^= input.GetHashCode();
			}
			foreach (string errorString in ErrorStrings)
			{
				hashCode ^= errorString.GetHashCode();
			}
			foreach (ScriptDebugVariable variable in Variables)
			{
				hashCode ^= variable.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ScriptDebugCall scriptDebugCall = obj as ScriptDebugCall;
			if (scriptDebugCall == null)
			{
				return false;
			}
			if (!OpcodeName.Equals(scriptDebugCall.OpcodeName))
			{
				return false;
			}
			if (!Output.Equals(scriptDebugCall.Output))
			{
				return false;
			}
			if (Inputs.Count != scriptDebugCall.Inputs.Count)
			{
				return false;
			}
			for (int i = 0; i < Inputs.Count; i++)
			{
				if (!Inputs[i].Equals(scriptDebugCall.Inputs[i]))
				{
					return false;
				}
			}
			if (ErrorStrings.Count != scriptDebugCall.ErrorStrings.Count)
			{
				return false;
			}
			for (int j = 0; j < ErrorStrings.Count; j++)
			{
				if (!ErrorStrings[j].Equals(scriptDebugCall.ErrorStrings[j]))
				{
					return false;
				}
			}
			if (Variables.Count != scriptDebugCall.Variables.Count)
			{
				return false;
			}
			for (int k = 0; k < Variables.Count; k++)
			{
				if (!Variables[k].Equals(scriptDebugCall.Variables[k]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ScriptDebugCall Deserialize(Stream stream, ScriptDebugCall instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScriptDebugCall DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugCall scriptDebugCall = new ScriptDebugCall();
			DeserializeLengthDelimited(stream, scriptDebugCall);
			return scriptDebugCall;
		}

		public static ScriptDebugCall DeserializeLengthDelimited(Stream stream, ScriptDebugCall instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ScriptDebugCall Deserialize(Stream stream, ScriptDebugCall instance, long limit)
		{
			if (instance.Inputs == null)
			{
				instance.Inputs = new List<ScriptDebugVariable>();
			}
			if (instance.ErrorStrings == null)
			{
				instance.ErrorStrings = new List<string>();
			}
			if (instance.Variables == null)
			{
				instance.Variables = new List<ScriptDebugVariable>();
			}
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
					instance.OpcodeName = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.Output == null)
					{
						instance.Output = ScriptDebugVariable.DeserializeLengthDelimited(stream);
					}
					else
					{
						ScriptDebugVariable.DeserializeLengthDelimited(stream, instance.Output);
					}
					continue;
				case 26:
					instance.Inputs.Add(ScriptDebugVariable.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.ErrorStrings.Add(ProtocolParser.ReadString(stream));
					continue;
				case 42:
					instance.Variables.Add(ScriptDebugVariable.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ScriptDebugCall instance)
		{
			if (instance.OpcodeName == null)
			{
				throw new ArgumentNullException("OpcodeName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OpcodeName));
			if (instance.Output == null)
			{
				throw new ArgumentNullException("Output", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Output.GetSerializedSize());
			ScriptDebugVariable.Serialize(stream, instance.Output);
			if (instance.Inputs.Count > 0)
			{
				foreach (ScriptDebugVariable input in instance.Inputs)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, input.GetSerializedSize());
					ScriptDebugVariable.Serialize(stream, input);
				}
			}
			if (instance.ErrorStrings.Count > 0)
			{
				foreach (string errorString in instance.ErrorStrings)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(errorString));
				}
			}
			if (instance.Variables.Count <= 0)
			{
				return;
			}
			foreach (ScriptDebugVariable variable in instance.Variables)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, variable.GetSerializedSize());
				ScriptDebugVariable.Serialize(stream, variable);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(OpcodeName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint serializedSize = Output.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Inputs.Count > 0)
			{
				foreach (ScriptDebugVariable input in Inputs)
				{
					num++;
					uint serializedSize2 = input.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (ErrorStrings.Count > 0)
			{
				foreach (string errorString in ErrorStrings)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(errorString);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (Variables.Count > 0)
			{
				foreach (ScriptDebugVariable variable in Variables)
				{
					num++;
					uint serializedSize3 = variable.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num + 2;
		}
	}
}
