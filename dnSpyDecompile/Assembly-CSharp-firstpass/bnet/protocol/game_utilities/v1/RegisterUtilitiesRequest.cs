using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000363 RID: 867
	public class RegisterUtilitiesRequest : IProtoBuf
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000B4BA7 File Offset: 0x000B2DA7
		// (set) Token: 0x060036D3 RID: 14035 RVA: 0x000B4BAF File Offset: 0x000B2DAF
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000B4BA7 File Offset: 0x000B2DA7
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000B4BB8 File Offset: 0x000B2DB8
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000B4BC5 File Offset: 0x000B2DC5
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000B4BD3 File Offset: 0x000B2DD3
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000B4BE0 File Offset: 0x000B2DE0
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000B4BE9 File Offset: 0x000B2DE9
		// (set) Token: 0x060036DA RID: 14042 RVA: 0x000B4BF1 File Offset: 0x000B2DF1
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000B4C01 File Offset: 0x000B2E01
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000B4C0C File Offset: 0x000B2E0C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000B4C88 File Offset: 0x000B2E88
		public override bool Equals(object obj)
		{
			RegisterUtilitiesRequest registerUtilitiesRequest = obj as RegisterUtilitiesRequest;
			if (registerUtilitiesRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != registerUtilitiesRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(registerUtilitiesRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasProgram == registerUtilitiesRequest.HasProgram && (!this.HasProgram || this.Program.Equals(registerUtilitiesRequest.Program));
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000B4D21 File Offset: 0x000B2F21
		public static RegisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterUtilitiesRequest>(bs, 0, -1);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000B4D2B File Offset: 0x000B2F2B
		public void Deserialize(Stream stream)
		{
			RegisterUtilitiesRequest.Deserialize(stream, this);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000B4D35 File Offset: 0x000B2F35
		public static RegisterUtilitiesRequest Deserialize(Stream stream, RegisterUtilitiesRequest instance)
		{
			return RegisterUtilitiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000B4D40 File Offset: 0x000B2F40
		public static RegisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterUtilitiesRequest registerUtilitiesRequest = new RegisterUtilitiesRequest();
			RegisterUtilitiesRequest.DeserializeLengthDelimited(stream, registerUtilitiesRequest);
			return registerUtilitiesRequest;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000B4D5C File Offset: 0x000B2F5C
		public static RegisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, RegisterUtilitiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterUtilitiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000B4D84 File Offset: 0x000B2F84
		public static RegisterUtilitiesRequest Deserialize(Stream stream, RegisterUtilitiesRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				else if (num != 10)
				{
					if (num != 21)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Program = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B4E3B File Offset: 0x000B303B
		public void Serialize(Stream stream)
		{
			RegisterUtilitiesRequest.Serialize(stream, this);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B4E44 File Offset: 0x000B3044
		public static void Serialize(Stream stream, RegisterUtilitiesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B4EDC File Offset: 0x000B30DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x040014A3 RID: 5283
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x040014A4 RID: 5284
		public bool HasProgram;

		// Token: 0x040014A5 RID: 5285
		private uint _Program;
	}
}
