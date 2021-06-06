using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D8 RID: 984
	public class GameCreationProperties : IProtoBuf
	{
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x000CDC74 File Offset: 0x000CBE74
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x000CDC7C File Offset: 0x000CBE7C
		public List<bnet.protocol.v2.Attribute> Attribute
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

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x000CDC74 File Offset: 0x000CBE74
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x000CDC85 File Offset: 0x000CBE85
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000CDC92 File Offset: 0x000CBE92
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000CDCA0 File Offset: 0x000CBEA0
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000CDCAD File Offset: 0x000CBEAD
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000CDCB8 File Offset: 0x000CBEB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000CDD1C File Offset: 0x000CBF1C
		public override bool Equals(object obj)
		{
			GameCreationProperties gameCreationProperties = obj as GameCreationProperties;
			if (gameCreationProperties == null)
			{
				return false;
			}
			if (this.Attribute.Count != gameCreationProperties.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(gameCreationProperties.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000CDD87 File Offset: 0x000CBF87
		public static GameCreationProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameCreationProperties>(bs, 0, -1);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000CDD91 File Offset: 0x000CBF91
		public void Deserialize(Stream stream)
		{
			GameCreationProperties.Deserialize(stream, this);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000CDD9B File Offset: 0x000CBF9B
		public static GameCreationProperties Deserialize(Stream stream, GameCreationProperties instance)
		{
			return GameCreationProperties.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000CDDA8 File Offset: 0x000CBFA8
		public static GameCreationProperties DeserializeLengthDelimited(Stream stream)
		{
			GameCreationProperties gameCreationProperties = new GameCreationProperties();
			GameCreationProperties.DeserializeLengthDelimited(stream, gameCreationProperties);
			return gameCreationProperties;
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x000CDDC4 File Offset: 0x000CBFC4
		public static GameCreationProperties DeserializeLengthDelimited(Stream stream, GameCreationProperties instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameCreationProperties.Deserialize(stream, instance, num);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000CDDEC File Offset: 0x000CBFEC
		public static GameCreationProperties Deserialize(Stream stream, GameCreationProperties instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x060040A7 RID: 16551 RVA: 0x000CDE84 File Offset: 0x000CC084
		public void Serialize(Stream stream)
		{
			GameCreationProperties.Serialize(stream, this);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000CDE90 File Offset: 0x000CC090
		public static void Serialize(Stream stream, GameCreationProperties instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000CDF08 File Offset: 0x000CC108
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001678 RID: 5752
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
