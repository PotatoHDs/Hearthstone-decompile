using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037C RID: 892
	public class RegisterUtilitiesRequest : IProtoBuf
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000B9A96 File Offset: 0x000B7C96
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000B9A9E File Offset: 0x000B7C9E
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000B9A96 File Offset: 0x000B7C96
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000B9AA7 File Offset: 0x000B7CA7
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000B9AB4 File Offset: 0x000B7CB4
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000B9AC2 File Offset: 0x000B7CC2
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000B9ACF File Offset: 0x000B7CCF
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000B9AD8 File Offset: 0x000B7CD8
		// (set) Token: 0x060038D8 RID: 14552 RVA: 0x000B9AE0 File Offset: 0x000B7CE0
		public uint Program { get; set; }

		// Token: 0x060038D9 RID: 14553 RVA: 0x000B9AE9 File Offset: 0x000B7CE9
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000B9AF4 File Offset: 0x000B7CF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			num ^= this.Program.GetHashCode();
			return num;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000B9B68 File Offset: 0x000B7D68
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
			return this.Program.Equals(registerUtilitiesRequest.Program);
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000B9BEB File Offset: 0x000B7DEB
		public static RegisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterUtilitiesRequest>(bs, 0, -1);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000B9BF5 File Offset: 0x000B7DF5
		public void Deserialize(Stream stream)
		{
			RegisterUtilitiesRequest.Deserialize(stream, this);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000B9BFF File Offset: 0x000B7DFF
		public static RegisterUtilitiesRequest Deserialize(Stream stream, RegisterUtilitiesRequest instance)
		{
			return RegisterUtilitiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000B9C0C File Offset: 0x000B7E0C
		public static RegisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterUtilitiesRequest registerUtilitiesRequest = new RegisterUtilitiesRequest();
			RegisterUtilitiesRequest.DeserializeLengthDelimited(stream, registerUtilitiesRequest);
			return registerUtilitiesRequest;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000B9C28 File Offset: 0x000B7E28
		public static RegisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, RegisterUtilitiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterUtilitiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000B9C50 File Offset: 0x000B7E50
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
					if (num != 29)
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

		// Token: 0x060038E3 RID: 14563 RVA: 0x000B9D07 File Offset: 0x000B7F07
		public void Serialize(Stream stream)
		{
			RegisterUtilitiesRequest.Serialize(stream, this);
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000B9D10 File Offset: 0x000B7F10
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
			stream.WriteByte(29);
			binaryWriter.Write(instance.Program);
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000B9DA0 File Offset: 0x000B7FA0
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
			num += 4U;
			num += 1U;
			return num;
		}

		// Token: 0x04001507 RID: 5383
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
