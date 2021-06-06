using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004EB RID: 1259
	public class ModuleMessageRequest : IProtoBuf
	{
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x00110CA9 File Offset: 0x0010EEA9
		// (set) Token: 0x06005923 RID: 22819 RVA: 0x00110CB1 File Offset: 0x0010EEB1
		public int ModuleId { get; set; }

		// Token: 0x06005924 RID: 22820 RVA: 0x00110CBA File Offset: 0x0010EEBA
		public void SetModuleId(int val)
		{
			this.ModuleId = val;
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x00110CC3 File Offset: 0x0010EEC3
		// (set) Token: 0x06005926 RID: 22822 RVA: 0x00110CCB File Offset: 0x0010EECB
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

		// Token: 0x06005927 RID: 22823 RVA: 0x00110CDE File Offset: 0x0010EEDE
		public void SetMessage(byte[] val)
		{
			this.Message = val;
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00110CE8 File Offset: 0x0010EEE8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ModuleId.GetHashCode();
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x00110D2C File Offset: 0x0010EF2C
		public override bool Equals(object obj)
		{
			ModuleMessageRequest moduleMessageRequest = obj as ModuleMessageRequest;
			return moduleMessageRequest != null && this.ModuleId.Equals(moduleMessageRequest.ModuleId) && this.HasMessage == moduleMessageRequest.HasMessage && (!this.HasMessage || this.Message.Equals(moduleMessageRequest.Message));
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x0600592A RID: 22826 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x00110D89 File Offset: 0x0010EF89
		public static ModuleMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleMessageRequest>(bs, 0, -1);
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x00110D93 File Offset: 0x0010EF93
		public void Deserialize(Stream stream)
		{
			ModuleMessageRequest.Deserialize(stream, this);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x00110D9D File Offset: 0x0010EF9D
		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance)
		{
			return ModuleMessageRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x00110DA8 File Offset: 0x0010EFA8
		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleMessageRequest moduleMessageRequest = new ModuleMessageRequest();
			ModuleMessageRequest.DeserializeLengthDelimited(stream, moduleMessageRequest);
			return moduleMessageRequest;
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x00110DC4 File Offset: 0x0010EFC4
		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream, ModuleMessageRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleMessageRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x00110DEC File Offset: 0x0010EFEC
		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance, long limit)
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
				else
				{
					instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x00110E84 File Offset: 0x0010F084
		public void Serialize(Stream stream)
		{
			ModuleMessageRequest.Serialize(stream, this);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00110E8D File Offset: 0x0010F08D
		public static void Serialize(Stream stream, ModuleMessageRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ModuleId));
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00110EC0 File Offset: 0x0010F0C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ModuleId));
			if (this.HasMessage)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Message.Length) + (uint)this.Message.Length;
			}
			return num + 1U;
		}

		// Token: 0x04001BD5 RID: 7125
		public bool HasMessage;

		// Token: 0x04001BD6 RID: 7126
		private byte[] _Message;
	}
}
