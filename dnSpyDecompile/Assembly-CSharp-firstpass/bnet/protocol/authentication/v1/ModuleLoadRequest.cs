using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004E9 RID: 1257
	public class ModuleLoadRequest : IProtoBuf
	{
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x00110766 File Offset: 0x0010E966
		// (set) Token: 0x060058FD RID: 22781 RVA: 0x0011076E File Offset: 0x0010E96E
		public ContentHandle ModuleHandle { get; set; }

		// Token: 0x060058FE RID: 22782 RVA: 0x00110777 File Offset: 0x0010E977
		public void SetModuleHandle(ContentHandle val)
		{
			this.ModuleHandle = val;
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x00110780 File Offset: 0x0010E980
		// (set) Token: 0x06005900 RID: 22784 RVA: 0x00110788 File Offset: 0x0010E988
		public byte[] Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.HasMessage = (value != null);
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0011079B File Offset: 0x0010E99B
		public void SetMessage(byte[] val)
		{
			this.Message = val;
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x001107A4 File Offset: 0x0010E9A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ModuleHandle.GetHashCode();
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x001107E4 File Offset: 0x0010E9E4
		public override bool Equals(object obj)
		{
			ModuleLoadRequest moduleLoadRequest = obj as ModuleLoadRequest;
			return moduleLoadRequest != null && this.ModuleHandle.Equals(moduleLoadRequest.ModuleHandle) && this.HasMessage == moduleLoadRequest.HasMessage && (!this.HasMessage || this.Message.Equals(moduleLoadRequest.Message));
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0011083E File Offset: 0x0010EA3E
		public static ModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleLoadRequest>(bs, 0, -1);
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00110848 File Offset: 0x0010EA48
		public void Deserialize(Stream stream)
		{
			ModuleLoadRequest.Deserialize(stream, this);
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00110852 File Offset: 0x0010EA52
		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance)
		{
			return ModuleLoadRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00110860 File Offset: 0x0010EA60
		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleLoadRequest moduleLoadRequest = new ModuleLoadRequest();
			ModuleLoadRequest.DeserializeLengthDelimited(stream, moduleLoadRequest);
			return moduleLoadRequest;
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0011087C File Offset: 0x0010EA7C
		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream, ModuleLoadRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleLoadRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x001108A4 File Offset: 0x0010EAA4
		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Message = ProtocolParser.ReadBytes(stream);
					}
				}
				else if (instance.ModuleHandle == null)
				{
					instance.ModuleHandle = ContentHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					ContentHandle.DeserializeLengthDelimited(stream, instance.ModuleHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00110956 File Offset: 0x0010EB56
		public void Serialize(Stream stream)
		{
			ModuleLoadRequest.Serialize(stream, this);
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x00110960 File Offset: 0x0010EB60
		public static void Serialize(Stream stream, ModuleLoadRequest instance)
		{
			if (instance.ModuleHandle == null)
			{
				throw new ArgumentNullException("ModuleHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ModuleHandle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.ModuleHandle);
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x001109C8 File Offset: 0x0010EBC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ModuleHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasMessage)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Message.Length) + (uint)this.Message.Length;
			}
			return num + 1U;
		}

		// Token: 0x04001BCE RID: 7118
		public bool HasMessage;

		// Token: 0x04001BCF RID: 7119
		private byte[] _Message;
	}
}
