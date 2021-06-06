using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000357 RID: 855
	public class ClientResponse : IProtoBuf
	{
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000B22EF File Offset: 0x000B04EF
		// (set) Token: 0x060035D7 RID: 13783 RVA: 0x000B22F7 File Offset: 0x000B04F7
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000B22EF File Offset: 0x000B04EF
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000B2300 File Offset: 0x000B0500
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000B230D File Offset: 0x000B050D
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000B231B File Offset: 0x000B051B
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000B2328 File Offset: 0x000B0528
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000B2334 File Offset: 0x000B0534
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000B2398 File Offset: 0x000B0598
		public override bool Equals(object obj)
		{
			ClientResponse clientResponse = obj as ClientResponse;
			if (clientResponse == null)
			{
				return false;
			}
			if (this.Attribute.Count != clientResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(clientResponse.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000B2403 File Offset: 0x000B0603
		public static ClientResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientResponse>(bs, 0, -1);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000B240D File Offset: 0x000B060D
		public void Deserialize(Stream stream)
		{
			ClientResponse.Deserialize(stream, this);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000B2417 File Offset: 0x000B0617
		public static ClientResponse Deserialize(Stream stream, ClientResponse instance)
		{
			return ClientResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000B2424 File Offset: 0x000B0624
		public static ClientResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientResponse clientResponse = new ClientResponse();
			ClientResponse.DeserializeLengthDelimited(stream, clientResponse);
			return clientResponse;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000B2440 File Offset: 0x000B0640
		public static ClientResponse DeserializeLengthDelimited(Stream stream, ClientResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000B2468 File Offset: 0x000B0668
		public static ClientResponse Deserialize(Stream stream, ClientResponse instance, long limit)
		{
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
				else if (num == 10)
				{
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
				}
				else
				{
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

		// Token: 0x060035E6 RID: 13798 RVA: 0x000B2500 File Offset: 0x000B0700
		public void Serialize(Stream stream)
		{
			ClientResponse.Serialize(stream, this);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000B250C File Offset: 0x000B070C
		public static void Serialize(Stream stream, ClientResponse instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000B2584 File Offset: 0x000B0784
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
			return num;
		}

		// Token: 0x0400147D RID: 5245
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
