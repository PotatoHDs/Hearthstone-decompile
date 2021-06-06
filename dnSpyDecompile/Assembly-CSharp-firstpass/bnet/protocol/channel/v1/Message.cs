using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D2 RID: 1234
	public class Message : IProtoBuf
	{
		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x0010A3AB File Offset: 0x001085AB
		// (set) Token: 0x060056C3 RID: 22211 RVA: 0x0010A3B3 File Offset: 0x001085B3
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

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x0010A3AB File Offset: 0x001085AB
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x0010A3BC File Offset: 0x001085BC
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x0010A3C9 File Offset: 0x001085C9
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x0010A3D7 File Offset: 0x001085D7
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x0010A3E4 File Offset: 0x001085E4
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x0010A3F0 File Offset: 0x001085F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x0010A454 File Offset: 0x00108654
		public override bool Equals(object obj)
		{
			Message message = obj as Message;
			if (message == null)
			{
				return false;
			}
			if (this.Attribute.Count != message.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(message.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060056CB RID: 22219 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x0010A4BF File Offset: 0x001086BF
		public static Message ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Message>(bs, 0, -1);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x0010A4C9 File Offset: 0x001086C9
		public void Deserialize(Stream stream)
		{
			Message.Deserialize(stream, this);
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x0010A4D3 File Offset: 0x001086D3
		public static Message Deserialize(Stream stream, Message instance)
		{
			return Message.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x0010A4E0 File Offset: 0x001086E0
		public static Message DeserializeLengthDelimited(Stream stream)
		{
			Message message = new Message();
			Message.DeserializeLengthDelimited(stream, message);
			return message;
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x0010A4FC File Offset: 0x001086FC
		public static Message DeserializeLengthDelimited(Stream stream, Message instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Message.Deserialize(stream, instance, num);
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x0010A524 File Offset: 0x00108724
		public static Message Deserialize(Stream stream, Message instance, long limit)
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

		// Token: 0x060056D2 RID: 22226 RVA: 0x0010A5BC File Offset: 0x001087BC
		public void Serialize(Stream stream)
		{
			Message.Serialize(stream, this);
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x0010A5C8 File Offset: 0x001087C8
		public static void Serialize(Stream stream, Message instance)
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

		// Token: 0x060056D4 RID: 22228 RVA: 0x0010A640 File Offset: 0x00108840
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

		// Token: 0x04001B4E RID: 6990
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
