using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x0200019A RID: 410
	public class ScriptDebugVariable : IProtoBuf
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0005B038 File Offset: 0x00059238
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x0005B040 File Offset: 0x00059240
		public string VariableType { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0005B049 File Offset: 0x00059249
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0005B051 File Offset: 0x00059251
		public string VariableName { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0005B05A File Offset: 0x0005925A
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0005B062 File Offset: 0x00059262
		public List<int> IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				this._IntValue = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0005B06B File Offset: 0x0005926B
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0005B073 File Offset: 0x00059273
		public List<string> StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0005B07C File Offset: 0x0005927C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.VariableType.GetHashCode();
			num ^= this.VariableName.GetHashCode();
			foreach (int num2 in this.IntValue)
			{
				num ^= num2.GetHashCode();
			}
			foreach (string text in this.StringValue)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0005B144 File Offset: 0x00059344
		public override bool Equals(object obj)
		{
			ScriptDebugVariable scriptDebugVariable = obj as ScriptDebugVariable;
			if (scriptDebugVariable == null)
			{
				return false;
			}
			if (!this.VariableType.Equals(scriptDebugVariable.VariableType))
			{
				return false;
			}
			if (!this.VariableName.Equals(scriptDebugVariable.VariableName))
			{
				return false;
			}
			if (this.IntValue.Count != scriptDebugVariable.IntValue.Count)
			{
				return false;
			}
			for (int i = 0; i < this.IntValue.Count; i++)
			{
				if (!this.IntValue[i].Equals(scriptDebugVariable.IntValue[i]))
				{
					return false;
				}
			}
			if (this.StringValue.Count != scriptDebugVariable.StringValue.Count)
			{
				return false;
			}
			for (int j = 0; j < this.StringValue.Count; j++)
			{
				if (!this.StringValue[j].Equals(scriptDebugVariable.StringValue[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0005B22D File Offset: 0x0005942D
		public void Deserialize(Stream stream)
		{
			ScriptDebugVariable.Deserialize(stream, this);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005B237 File Offset: 0x00059437
		public static ScriptDebugVariable Deserialize(Stream stream, ScriptDebugVariable instance)
		{
			return ScriptDebugVariable.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005B244 File Offset: 0x00059444
		public static ScriptDebugVariable DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugVariable scriptDebugVariable = new ScriptDebugVariable();
			ScriptDebugVariable.DeserializeLengthDelimited(stream, scriptDebugVariable);
			return scriptDebugVariable;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005B260 File Offset: 0x00059460
		public static ScriptDebugVariable DeserializeLengthDelimited(Stream stream, ScriptDebugVariable instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScriptDebugVariable.Deserialize(stream, instance, num);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005B288 File Offset: 0x00059488
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
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.VariableType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.VariableName = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.IntValue.Add((int)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 34)
						{
							instance.StringValue.Add(ProtocolParser.ReadString(stream));
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

		// Token: 0x060019CC RID: 6604 RVA: 0x0005B38A File Offset: 0x0005958A
		public void Serialize(Stream stream)
		{
			ScriptDebugVariable.Serialize(stream, this);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005B394 File Offset: 0x00059594
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
				foreach (int num in instance.IntValue)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			if (instance.StringValue.Count > 0)
			{
				foreach (string s in instance.StringValue)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0005B4CC File Offset: 0x000596CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.VariableType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.VariableName);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.IntValue.Count > 0)
			{
				foreach (int num2 in this.IntValue)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			if (this.StringValue.Count > 0)
			{
				foreach (string s in this.StringValue)
				{
					num += 1U;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x040009B5 RID: 2485
		private List<int> _IntValue = new List<int>();

		// Token: 0x040009B6 RID: 2486
		private List<string> _StringValue = new List<string>();
	}
}
