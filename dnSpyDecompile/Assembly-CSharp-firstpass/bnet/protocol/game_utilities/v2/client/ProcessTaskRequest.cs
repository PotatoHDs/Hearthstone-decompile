using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.game_utilities.v2.client
{
	// Token: 0x02000352 RID: 850
	public class ProcessTaskRequest : IProtoBuf
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000B0E4F File Offset: 0x000AF04F
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x000B0E57 File Offset: 0x000AF057
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

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000B0E4F File Offset: 0x000AF04F
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000B0E60 File Offset: 0x000AF060
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000B0E6D File Offset: 0x000AF06D
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000B0E7B File Offset: 0x000AF07B
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000B0E88 File Offset: 0x000AF088
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000B0E91 File Offset: 0x000AF091
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x000B0E99 File Offset: 0x000AF099
		public List<bnet.protocol.v2.Attribute> Payload
		{
			get
			{
				return this._Payload;
			}
			set
			{
				this._Payload = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000B0E91 File Offset: 0x000AF091
		public List<bnet.protocol.v2.Attribute> PayloadList
		{
			get
			{
				return this._Payload;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000B0EA2 File Offset: 0x000AF0A2
		public int PayloadCount
		{
			get
			{
				return this._Payload.Count;
			}
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000B0EAF File Offset: 0x000AF0AF
		public void AddPayload(bnet.protocol.v2.Attribute val)
		{
			this._Payload.Add(val);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000B0EBD File Offset: 0x000AF0BD
		public void ClearPayload()
		{
			this._Payload.Clear();
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000B0ECA File Offset: 0x000AF0CA
		public void SetPayload(List<bnet.protocol.v2.Attribute> val)
		{
			this.Payload = val;
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000B0ED4 File Offset: 0x000AF0D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute2 in this.Payload)
			{
				num ^= attribute2.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000B0F7C File Offset: 0x000AF17C
		public override bool Equals(object obj)
		{
			ProcessTaskRequest processTaskRequest = obj as ProcessTaskRequest;
			if (processTaskRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != processTaskRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(processTaskRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Payload.Count != processTaskRequest.Payload.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Payload.Count; j++)
			{
				if (!this.Payload[j].Equals(processTaskRequest.Payload[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003570 RID: 13680 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000B1038 File Offset: 0x000AF238
		public static ProcessTaskRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessTaskRequest>(bs, 0, -1);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000B1042 File Offset: 0x000AF242
		public void Deserialize(Stream stream)
		{
			ProcessTaskRequest.Deserialize(stream, this);
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000B104C File Offset: 0x000AF24C
		public static ProcessTaskRequest Deserialize(Stream stream, ProcessTaskRequest instance)
		{
			return ProcessTaskRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000B1058 File Offset: 0x000AF258
		public static ProcessTaskRequest DeserializeLengthDelimited(Stream stream)
		{
			ProcessTaskRequest processTaskRequest = new ProcessTaskRequest();
			ProcessTaskRequest.DeserializeLengthDelimited(stream, processTaskRequest);
			return processTaskRequest;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000B1074 File Offset: 0x000AF274
		public static ProcessTaskRequest DeserializeLengthDelimited(Stream stream, ProcessTaskRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessTaskRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000B109C File Offset: 0x000AF29C
		public static ProcessTaskRequest Deserialize(Stream stream, ProcessTaskRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Payload == null)
			{
				instance.Payload = new List<bnet.protocol.v2.Attribute>();
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
					if (num != 18)
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
						instance.Payload.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000B1164 File Offset: 0x000AF364
		public void Serialize(Stream stream)
		{
			ProcessTaskRequest.Serialize(stream, this);
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000B1170 File Offset: 0x000AF370
		public static void Serialize(Stream stream, ProcessTaskRequest instance)
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
			if (instance.Payload.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute2 in instance.Payload)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute2.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute2);
				}
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000B124C File Offset: 0x000AF44C
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
			if (this.Payload.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute2 in this.Payload)
				{
					num += 1U;
					uint serializedSize2 = attribute2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400146C RID: 5228
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400146D RID: 5229
		private List<bnet.protocol.v2.Attribute> _Payload = new List<bnet.protocol.v2.Attribute>();
	}
}
