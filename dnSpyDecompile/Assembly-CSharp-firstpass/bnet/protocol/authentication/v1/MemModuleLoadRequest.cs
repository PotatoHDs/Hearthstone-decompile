using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F6 RID: 1270
	public class MemModuleLoadRequest : IProtoBuf
	{
		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x00113B1B File Offset: 0x00111D1B
		// (set) Token: 0x06005A3F RID: 23103 RVA: 0x00113B23 File Offset: 0x00111D23
		public ContentHandle Handle { get; set; }

		// Token: 0x06005A40 RID: 23104 RVA: 0x00113B2C File Offset: 0x00111D2C
		public void SetHandle(ContentHandle val)
		{
			this.Handle = val;
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06005A41 RID: 23105 RVA: 0x00113B35 File Offset: 0x00111D35
		// (set) Token: 0x06005A42 RID: 23106 RVA: 0x00113B3D File Offset: 0x00111D3D
		public byte[] Key { get; set; }

		// Token: 0x06005A43 RID: 23107 RVA: 0x00113B46 File Offset: 0x00111D46
		public void SetKey(byte[] val)
		{
			this.Key = val;
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x00113B4F File Offset: 0x00111D4F
		// (set) Token: 0x06005A45 RID: 23109 RVA: 0x00113B57 File Offset: 0x00111D57
		public byte[] Input { get; set; }

		// Token: 0x06005A46 RID: 23110 RVA: 0x00113B60 File Offset: 0x00111D60
		public void SetInput(byte[] val)
		{
			this.Input = val;
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x00113B69 File Offset: 0x00111D69
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Handle.GetHashCode() ^ this.Key.GetHashCode() ^ this.Input.GetHashCode();
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x00113B9C File Offset: 0x00111D9C
		public override bool Equals(object obj)
		{
			MemModuleLoadRequest memModuleLoadRequest = obj as MemModuleLoadRequest;
			return memModuleLoadRequest != null && this.Handle.Equals(memModuleLoadRequest.Handle) && this.Key.Equals(memModuleLoadRequest.Key) && this.Input.Equals(memModuleLoadRequest.Input);
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x00113BF5 File Offset: 0x00111DF5
		public static MemModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadRequest>(bs, 0, -1);
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x00113BFF File Offset: 0x00111DFF
		public void Deserialize(Stream stream)
		{
			MemModuleLoadRequest.Deserialize(stream, this);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x00113C09 File Offset: 0x00111E09
		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance)
		{
			return MemModuleLoadRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x00113C14 File Offset: 0x00111E14
		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadRequest memModuleLoadRequest = new MemModuleLoadRequest();
			MemModuleLoadRequest.DeserializeLengthDelimited(stream, memModuleLoadRequest);
			return memModuleLoadRequest;
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x00113C30 File Offset: 0x00111E30
		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream, MemModuleLoadRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemModuleLoadRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x00113C58 File Offset: 0x00111E58
		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Input = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.Key = ProtocolParser.ReadBytes(stream);
					}
				}
				else if (instance.Handle == null)
				{
					instance.Handle = ContentHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					ContentHandle.DeserializeLengthDelimited(stream, instance.Handle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x00113D26 File Offset: 0x00111F26
		public void Serialize(Stream stream)
		{
			MemModuleLoadRequest.Serialize(stream, this);
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x00113D30 File Offset: 0x00111F30
		public static void Serialize(Stream stream, MemModuleLoadRequest instance)
		{
			if (instance.Handle == null)
			{
				throw new ArgumentNullException("Handle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.Handle);
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, instance.Key);
			if (instance.Input == null)
			{
				throw new ArgumentNullException("Input", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Input);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x00113DD4 File Offset: 0x00111FD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Handle.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + (ProtocolParser.SizeOfUInt32(this.Key.Length) + (uint)this.Key.Length) + (ProtocolParser.SizeOfUInt32(this.Input.Length) + (uint)this.Input.Length) + 3U;
		}
	}
}
