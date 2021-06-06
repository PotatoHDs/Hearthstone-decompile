using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x0200019B RID: 411
	public class ScriptDebugCall : IProtoBuf
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0005B5FE File Offset: 0x000597FE
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0005B606 File Offset: 0x00059806
		public string OpcodeName { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0005B60F File Offset: 0x0005980F
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0005B617 File Offset: 0x00059817
		public ScriptDebugVariable Output { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0005B620 File Offset: 0x00059820
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0005B628 File Offset: 0x00059828
		public List<ScriptDebugVariable> Inputs
		{
			get
			{
				return this._Inputs;
			}
			set
			{
				this._Inputs = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0005B631 File Offset: 0x00059831
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x0005B639 File Offset: 0x00059839
		public List<string> ErrorStrings
		{
			get
			{
				return this._ErrorStrings;
			}
			set
			{
				this._ErrorStrings = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0005B642 File Offset: 0x00059842
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x0005B64A File Offset: 0x0005984A
		public List<ScriptDebugVariable> Variables
		{
			get
			{
				return this._Variables;
			}
			set
			{
				this._Variables = value;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0005B654 File Offset: 0x00059854
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.OpcodeName.GetHashCode();
			num ^= this.Output.GetHashCode();
			foreach (ScriptDebugVariable scriptDebugVariable in this.Inputs)
			{
				num ^= scriptDebugVariable.GetHashCode();
			}
			foreach (string text in this.ErrorStrings)
			{
				num ^= text.GetHashCode();
			}
			foreach (ScriptDebugVariable scriptDebugVariable2 in this.Variables)
			{
				num ^= scriptDebugVariable2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0005B760 File Offset: 0x00059960
		public override bool Equals(object obj)
		{
			ScriptDebugCall scriptDebugCall = obj as ScriptDebugCall;
			if (scriptDebugCall == null)
			{
				return false;
			}
			if (!this.OpcodeName.Equals(scriptDebugCall.OpcodeName))
			{
				return false;
			}
			if (!this.Output.Equals(scriptDebugCall.Output))
			{
				return false;
			}
			if (this.Inputs.Count != scriptDebugCall.Inputs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				if (!this.Inputs[i].Equals(scriptDebugCall.Inputs[i]))
				{
					return false;
				}
			}
			if (this.ErrorStrings.Count != scriptDebugCall.ErrorStrings.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ErrorStrings.Count; j++)
			{
				if (!this.ErrorStrings[j].Equals(scriptDebugCall.ErrorStrings[j]))
				{
					return false;
				}
			}
			if (this.Variables.Count != scriptDebugCall.Variables.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Variables.Count; k++)
			{
				if (!this.Variables[k].Equals(scriptDebugCall.Variables[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0005B897 File Offset: 0x00059A97
		public void Deserialize(Stream stream)
		{
			ScriptDebugCall.Deserialize(stream, this);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0005B8A1 File Offset: 0x00059AA1
		public static ScriptDebugCall Deserialize(Stream stream, ScriptDebugCall instance)
		{
			return ScriptDebugCall.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0005B8AC File Offset: 0x00059AAC
		public static ScriptDebugCall DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugCall scriptDebugCall = new ScriptDebugCall();
			ScriptDebugCall.DeserializeLengthDelimited(stream, scriptDebugCall);
			return scriptDebugCall;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0005B8C8 File Offset: 0x00059AC8
		public static ScriptDebugCall DeserializeLengthDelimited(Stream stream, ScriptDebugCall instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScriptDebugCall.Deserialize(stream, instance, num);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0005B8F0 File Offset: 0x00059AF0
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
							instance.OpcodeName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.Output == null)
							{
								instance.Output = ScriptDebugVariable.DeserializeLengthDelimited(stream);
								continue;
							}
							ScriptDebugVariable.DeserializeLengthDelimited(stream, instance.Output);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Inputs.Add(ScriptDebugVariable.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							instance.ErrorStrings.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 42)
						{
							instance.Variables.Add(ScriptDebugVariable.DeserializeLengthDelimited(stream));
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

		// Token: 0x060019E1 RID: 6625 RVA: 0x0005BA3F File Offset: 0x00059C3F
		public void Serialize(Stream stream)
		{
			ScriptDebugCall.Serialize(stream, this);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0005BA48 File Offset: 0x00059C48
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
				foreach (ScriptDebugVariable scriptDebugVariable in instance.Inputs)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, scriptDebugVariable.GetSerializedSize());
					ScriptDebugVariable.Serialize(stream, scriptDebugVariable);
				}
			}
			if (instance.ErrorStrings.Count > 0)
			{
				foreach (string s in instance.ErrorStrings)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.Variables.Count > 0)
			{
				foreach (ScriptDebugVariable scriptDebugVariable2 in instance.Variables)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, scriptDebugVariable2.GetSerializedSize());
					ScriptDebugVariable.Serialize(stream, scriptDebugVariable2);
				}
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0005BBFC File Offset: 0x00059DFC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.OpcodeName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint serializedSize = this.Output.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Inputs.Count > 0)
			{
				foreach (ScriptDebugVariable scriptDebugVariable in this.Inputs)
				{
					num += 1U;
					uint serializedSize2 = scriptDebugVariable.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.ErrorStrings.Count > 0)
			{
				foreach (string s in this.ErrorStrings)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (this.Variables.Count > 0)
			{
				foreach (ScriptDebugVariable scriptDebugVariable2 in this.Variables)
				{
					num += 1U;
					uint serializedSize3 = scriptDebugVariable2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x040009B9 RID: 2489
		private List<ScriptDebugVariable> _Inputs = new List<ScriptDebugVariable>();

		// Token: 0x040009BA RID: 2490
		private List<string> _ErrorStrings = new List<string>();

		// Token: 0x040009BB RID: 2491
		private List<ScriptDebugVariable> _Variables = new List<ScriptDebugVariable>();
	}
}
