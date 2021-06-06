using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B1 RID: 689
	public class ObjectAddress : IProtoBuf
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0008DE56 File Offset: 0x0008C056
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x0008DE5E File Offset: 0x0008C05E
		public ProcessId Host { get; set; }

		// Token: 0x0600281E RID: 10270 RVA: 0x0008DE67 File Offset: 0x0008C067
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x0008DE70 File Offset: 0x0008C070
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x0008DE78 File Offset: 0x0008C078
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0008DE88 File Offset: 0x0008C088
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0008DE94 File Offset: 0x0008C094
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Host.GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0008DED8 File Offset: 0x0008C0D8
		public override bool Equals(object obj)
		{
			ObjectAddress objectAddress = obj as ObjectAddress;
			return objectAddress != null && this.Host.Equals(objectAddress.Host) && this.HasObjectId == objectAddress.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(objectAddress.ObjectId));
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0008DF35 File Offset: 0x0008C135
		public static ObjectAddress ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ObjectAddress>(bs, 0, -1);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0008DF3F File Offset: 0x0008C13F
		public void Deserialize(Stream stream)
		{
			ObjectAddress.Deserialize(stream, this);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0008DF49 File Offset: 0x0008C149
		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance)
		{
			return ObjectAddress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0008DF54 File Offset: 0x0008C154
		public static ObjectAddress DeserializeLengthDelimited(Stream stream)
		{
			ObjectAddress objectAddress = new ObjectAddress();
			ObjectAddress.DeserializeLengthDelimited(stream, objectAddress);
			return objectAddress;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0008DF70 File Offset: 0x0008C170
		public static ObjectAddress DeserializeLengthDelimited(Stream stream, ObjectAddress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ObjectAddress.Deserialize(stream, instance, num);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0008DF98 File Offset: 0x0008C198
		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance, long limit)
		{
			instance.ObjectId = 0UL;
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
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Host == null)
				{
					instance.Host = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.Host);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0008E052 File Offset: 0x0008C252
		public void Serialize(Stream stream)
		{
			ObjectAddress.Serialize(stream, this);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0008E05C File Offset: 0x0008C25C
		public static void Serialize(Stream stream, ObjectAddress instance)
		{
			if (instance.Host == null)
			{
				throw new ArgumentNullException("Host", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
			ProcessId.Serialize(stream, instance.Host);
			if (instance.HasObjectId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0008E0C4 File Offset: 0x0008C2C4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Host.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num + 1U;
		}

		// Token: 0x04001157 RID: 4439
		public bool HasObjectId;

		// Token: 0x04001158 RID: 4440
		private ulong _ObjectId;
	}
}
