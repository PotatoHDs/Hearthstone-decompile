using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000359 RID: 857
	public class ServerResponse : IProtoBuf
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000B2A93 File Offset: 0x000B0C93
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000B2A9B File Offset: 0x000B0C9B
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

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000B2A93 File Offset: 0x000B0C93
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000B2AA4 File Offset: 0x000B0CA4
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000B2AB1 File Offset: 0x000B0CB1
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000B2ABF File Offset: 0x000B0CBF
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000B2ACC File Offset: 0x000B0CCC
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000B2AD8 File Offset: 0x000B0CD8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000B2B3C File Offset: 0x000B0D3C
		public override bool Equals(object obj)
		{
			ServerResponse serverResponse = obj as ServerResponse;
			if (serverResponse == null)
			{
				return false;
			}
			if (this.Attribute.Count != serverResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(serverResponse.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000B2BA7 File Offset: 0x000B0DA7
		public static ServerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerResponse>(bs, 0, -1);
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000B2BB1 File Offset: 0x000B0DB1
		public void Deserialize(Stream stream)
		{
			ServerResponse.Deserialize(stream, this);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000B2BBB File Offset: 0x000B0DBB
		public static ServerResponse Deserialize(Stream stream, ServerResponse instance)
		{
			return ServerResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000B2BC8 File Offset: 0x000B0DC8
		public static ServerResponse DeserializeLengthDelimited(Stream stream)
		{
			ServerResponse serverResponse = new ServerResponse();
			ServerResponse.DeserializeLengthDelimited(stream, serverResponse);
			return serverResponse;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000B2BE4 File Offset: 0x000B0DE4
		public static ServerResponse DeserializeLengthDelimited(Stream stream, ServerResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000B2C0C File Offset: 0x000B0E0C
		public static ServerResponse Deserialize(Stream stream, ServerResponse instance, long limit)
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

		// Token: 0x06003614 RID: 13844 RVA: 0x000B2CA4 File Offset: 0x000B0EA4
		public void Serialize(Stream stream)
		{
			ServerResponse.Serialize(stream, this);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000B2CB0 File Offset: 0x000B0EB0
		public static void Serialize(Stream stream, ServerResponse instance)
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

		// Token: 0x06003616 RID: 13846 RVA: 0x000B2D28 File Offset: 0x000B0F28
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

		// Token: 0x04001482 RID: 5250
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
