using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class ScriptDebugVariable : IProtoBuf
	{
		private List<int> _IntValue = new List<int>();

		private List<string> _StringValue = new List<string>();

		public string VariableType { get; set; }

		public string VariableName { get; set; }

		public List<int> IntValue
		{
			get
			{
				return _IntValue;
			}
			set
			{
				_IntValue = value;
			}
		}

		public List<string> StringValue
		{
			get
			{
				return _StringValue;
			}
			set
			{
				_StringValue = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= VariableType.GetHashCode();
			hashCode ^= VariableName.GetHashCode();
			foreach (int item in IntValue)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (string item2 in StringValue)
			{
				hashCode ^= item2.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ScriptDebugVariable scriptDebugVariable = obj as ScriptDebugVariable;
			if (scriptDebugVariable == null)
			{
				return false;
			}
			if (!VariableType.Equals(scriptDebugVariable.VariableType))
			{
				return false;
			}
			if (!VariableName.Equals(scriptDebugVariable.VariableName))
			{
				return false;
			}
			if (IntValue.Count != scriptDebugVariable.IntValue.Count)
			{
				return false;
			}
			for (int i = 0; i < IntValue.Count; i++)
			{
				if (!IntValue[i].Equals(scriptDebugVariable.IntValue[i]))
				{
					return false;
				}
			}
			if (StringValue.Count != scriptDebugVariable.StringValue.Count)
			{
				return false;
			}
			for (int j = 0; j < StringValue.Count; j++)
			{
				if (!StringValue[j].Equals(scriptDebugVariable.StringValue[j]))
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

		public static ScriptDebugVariable Deserialize(Stream stream, ScriptDebugVariable instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScriptDebugVariable DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugVariable scriptDebugVariable = new ScriptDebugVariable();
			DeserializeLengthDelimited(stream, scriptDebugVariable);
			return scriptDebugVariable;
		}

		public static ScriptDebugVariable DeserializeLengthDelimited(Stream stream, ScriptDebugVariable instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ScriptDebugVariable Deserialize(Stream stream, ScriptDebugVariable instance, long limit)
		{
			if (instance.IntValue == null)
			{
				instance.IntValue = new List<int>();
			}
			if (instance.StringValue == null)
			{
				instance.StringValue = new List<string>();
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
					instance.VariableType = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.VariableName = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.IntValue.Add((int)ProtocolParser.ReadUInt64(stream));
					continue;
				case 34:
					instance.StringValue.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, ScriptDebugVariable instance)
		{
			if (instance.VariableType == null)
			{
				throw new ArgumentNullException("VariableType", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VariableType));
			if (instance.VariableName == null)
			{
				throw new ArgumentNullException("VariableName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VariableName));
			if (instance.IntValue.Count > 0)
			{
				foreach (int item in instance.IntValue)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)item);
				}
			}
			if (instance.StringValue.Count <= 0)
			{
				return;
			}
			foreach (string item2 in instance.StringValue)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item2));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(VariableType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(VariableName);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (IntValue.Count > 0)
			{
				foreach (int item in IntValue)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			if (StringValue.Count > 0)
			{
				foreach (string item2 in StringValue)
				{
					num++;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(item2);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			return num + 2;
		}
	}
}
