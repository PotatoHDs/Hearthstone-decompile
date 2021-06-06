using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047A RID: 1146
	public class SendMessageOptions : IProtoBuf
	{
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x000F58AE File Offset: 0x000F3AAE
		// (set) Token: 0x06004F23 RID: 20259 RVA: 0x000F58B6 File Offset: 0x000F3AB6
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				this._Content = value;
				this.HasContent = (value != null);
			}
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x000F58C9 File Offset: 0x000F3AC9
		public void SetContent(string val)
		{
			this.Content = val;
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x000F58D2 File Offset: 0x000F3AD2
		// (set) Token: 0x06004F26 RID: 20262 RVA: 0x000F58DA File Offset: 0x000F3ADA
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

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x000F58D2 File Offset: 0x000F3AD2
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x000F58E3 File Offset: 0x000F3AE3
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000F58F0 File Offset: 0x000F3AF0
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000F58FE File Offset: 0x000F3AFE
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x000F590B File Offset: 0x000F3B0B
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000F5914 File Offset: 0x000F3B14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasContent)
			{
				num ^= this.Content.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x000F598C File Offset: 0x000F3B8C
		public override bool Equals(object obj)
		{
			SendMessageOptions sendMessageOptions = obj as SendMessageOptions;
			if (sendMessageOptions == null)
			{
				return false;
			}
			if (this.HasContent != sendMessageOptions.HasContent || (this.HasContent && !this.Content.Equals(sendMessageOptions.Content)))
			{
				return false;
			}
			if (this.Attribute.Count != sendMessageOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(sendMessageOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x000F5A22 File Offset: 0x000F3C22
		public static SendMessageOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageOptions>(bs, 0, -1);
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x000F5A2C File Offset: 0x000F3C2C
		public void Deserialize(Stream stream)
		{
			SendMessageOptions.Deserialize(stream, this);
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x000F5A36 File Offset: 0x000F3C36
		public static SendMessageOptions Deserialize(Stream stream, SendMessageOptions instance)
		{
			return SendMessageOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x000F5A44 File Offset: 0x000F3C44
		public static SendMessageOptions DeserializeLengthDelimited(Stream stream)
		{
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			SendMessageOptions.DeserializeLengthDelimited(stream, sendMessageOptions);
			return sendMessageOptions;
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x000F5A60 File Offset: 0x000F3C60
		public static SendMessageOptions DeserializeLengthDelimited(Stream stream, SendMessageOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x000F5A88 File Offset: 0x000F3C88
		public static SendMessageOptions Deserialize(Stream stream, SendMessageOptions instance, long limit)
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
				else if (num != 34)
				{
					if (num != 42)
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
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Content = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x000F5B38 File Offset: 0x000F3D38
		public void Serialize(Stream stream)
		{
			SendMessageOptions.Serialize(stream, this);
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x000F5B44 File Offset: 0x000F3D44
		public static void Serialize(Stream stream, SendMessageOptions instance)
		{
			if (instance.HasContent)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x000F5BE0 File Offset: 0x000F3DE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasContent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
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

		// Token: 0x0400199E RID: 6558
		public bool HasContent;

		// Token: 0x0400199F RID: 6559
		private string _Content;

		// Token: 0x040019A0 RID: 6560
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
