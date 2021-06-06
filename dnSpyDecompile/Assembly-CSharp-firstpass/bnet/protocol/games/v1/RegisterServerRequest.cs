using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000379 RID: 889
	public class RegisterServerRequest : IProtoBuf
	{
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003899 RID: 14489 RVA: 0x000B9374 File Offset: 0x000B7574
		// (set) Token: 0x0600389A RID: 14490 RVA: 0x000B937C File Offset: 0x000B757C
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

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000B9374 File Offset: 0x000B7574
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000B9385 File Offset: 0x000B7585
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000B9392 File Offset: 0x000B7592
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000B93A0 File Offset: 0x000B75A0
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000B93AD File Offset: 0x000B75AD
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000B93B6 File Offset: 0x000B75B6
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000B93BE File Offset: 0x000B75BE
		public uint Program { get; set; }

		// Token: 0x060038A2 RID: 14498 RVA: 0x000B93C7 File Offset: 0x000B75C7
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x000B93D0 File Offset: 0x000B75D0
		// (set) Token: 0x060038A4 RID: 14500 RVA: 0x000B93D8 File Offset: 0x000B75D8
		public uint Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				this._Cost = value;
				this.HasCost = true;
			}
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000B93E8 File Offset: 0x000B75E8
		public void SetCost(uint val)
		{
			this.Cost = val;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000B93F4 File Offset: 0x000B75F4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			num ^= this.Program.GetHashCode();
			if (this.HasCost)
			{
				num ^= this.Cost.GetHashCode();
			}
			return num;
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000B9480 File Offset: 0x000B7680
		public override bool Equals(object obj)
		{
			RegisterServerRequest registerServerRequest = obj as RegisterServerRequest;
			if (registerServerRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != registerServerRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(registerServerRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.Program.Equals(registerServerRequest.Program) && this.HasCost == registerServerRequest.HasCost && (!this.HasCost || this.Cost.Equals(registerServerRequest.Cost));
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000B9531 File Offset: 0x000B7731
		public static RegisterServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterServerRequest>(bs, 0, -1);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000B953B File Offset: 0x000B773B
		public void Deserialize(Stream stream)
		{
			RegisterServerRequest.Deserialize(stream, this);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000B9545 File Offset: 0x000B7745
		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance)
		{
			return RegisterServerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000B9550 File Offset: 0x000B7750
		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterServerRequest registerServerRequest = new RegisterServerRequest();
			RegisterServerRequest.DeserializeLengthDelimited(stream, registerServerRequest);
			return registerServerRequest;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000B956C File Offset: 0x000B776C
		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream, RegisterServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterServerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000B9594 File Offset: 0x000B7794
		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.Cost = 0U;
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
						if (num != 32)
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
							instance.Cost = ProtocolParser.ReadUInt32(stream);
						}
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

		// Token: 0x060038AF RID: 14511 RVA: 0x000B9668 File Offset: 0x000B7868
		public void Serialize(Stream stream)
		{
			RegisterServerRequest.Serialize(stream, this);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000B9674 File Offset: 0x000B7874
		public static void Serialize(Stream stream, RegisterServerRequest instance)
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
			if (instance.HasCost)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Cost);
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000B9720 File Offset: 0x000B7920
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
			if (this.HasCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Cost);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001501 RID: 5377
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001503 RID: 5379
		public bool HasCost;

		// Token: 0x04001504 RID: 5380
		private uint _Cost;
	}
}
