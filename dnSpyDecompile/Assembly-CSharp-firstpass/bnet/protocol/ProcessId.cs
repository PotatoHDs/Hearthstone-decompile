using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B0 RID: 688
	public class ProcessId : IProtoBuf
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x0008DC52 File Offset: 0x0008BE52
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x0008DC5A File Offset: 0x0008BE5A
		public uint Label { get; set; }

		// Token: 0x0600280B RID: 10251 RVA: 0x0008DC63 File Offset: 0x0008BE63
		public void SetLabel(uint val)
		{
			this.Label = val;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x0008DC6C File Offset: 0x0008BE6C
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x0008DC74 File Offset: 0x0008BE74
		public uint Epoch { get; set; }

		// Token: 0x0600280E RID: 10254 RVA: 0x0008DC7D File Offset: 0x0008BE7D
		public void SetEpoch(uint val)
		{
			this.Epoch = val;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0008DC88 File Offset: 0x0008BE88
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Label.GetHashCode() ^ this.Epoch.GetHashCode();
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0008DCC0 File Offset: 0x0008BEC0
		public override bool Equals(object obj)
		{
			ProcessId processId = obj as ProcessId;
			return processId != null && this.Label.Equals(processId.Label) && this.Epoch.Equals(processId.Epoch);
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0008DD0A File Offset: 0x0008BF0A
		public static ProcessId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessId>(bs, 0, -1);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0008DD14 File Offset: 0x0008BF14
		public void Deserialize(Stream stream)
		{
			ProcessId.Deserialize(stream, this);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0008DD1E File Offset: 0x0008BF1E
		public static ProcessId Deserialize(Stream stream, ProcessId instance)
		{
			return ProcessId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0008DD2C File Offset: 0x0008BF2C
		public static ProcessId DeserializeLengthDelimited(Stream stream)
		{
			ProcessId processId = new ProcessId();
			ProcessId.DeserializeLengthDelimited(stream, processId);
			return processId;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0008DD48 File Offset: 0x0008BF48
		public static ProcessId DeserializeLengthDelimited(Stream stream, ProcessId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessId.Deserialize(stream, instance, num);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0008DD70 File Offset: 0x0008BF70
		public static ProcessId Deserialize(Stream stream, ProcessId instance, long limit)
		{
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.Epoch = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Label = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0008DE07 File Offset: 0x0008C007
		public void Serialize(Stream stream)
		{
			ProcessId.Serialize(stream, this);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0008DE10 File Offset: 0x0008C010
		public static void Serialize(Stream stream, ProcessId instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Label);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Epoch);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0008DE39 File Offset: 0x0008C039
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.Label) + ProtocolParser.SizeOfUInt32(this.Epoch) + 2U;
		}
	}
}
