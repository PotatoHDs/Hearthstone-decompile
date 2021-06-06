using System;
using System.IO;
using System.Text;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000445 RID: 1093
	public class DisconnectNotification : IProtoBuf
	{
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000E7A72 File Offset: 0x000E5C72
		// (set) Token: 0x06004A42 RID: 19010 RVA: 0x000E7A7A File Offset: 0x000E5C7A
		public uint ErrorCode { get; set; }

		// Token: 0x06004A43 RID: 19011 RVA: 0x000E7A83 File Offset: 0x000E5C83
		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x000E7A8C File Offset: 0x000E5C8C
		// (set) Token: 0x06004A45 RID: 19013 RVA: 0x000E7A94 File Offset: 0x000E5C94
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = (value != null);
			}
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x000E7AA7 File Offset: 0x000E5CA7
		public void SetReason(string val)
		{
			this.Reason = val;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x000E7AB0 File Offset: 0x000E5CB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x000E7AF4 File Offset: 0x000E5CF4
		public override bool Equals(object obj)
		{
			DisconnectNotification disconnectNotification = obj as DisconnectNotification;
			return disconnectNotification != null && this.ErrorCode.Equals(disconnectNotification.ErrorCode) && this.HasReason == disconnectNotification.HasReason && (!this.HasReason || this.Reason.Equals(disconnectNotification.Reason));
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x000E7B51 File Offset: 0x000E5D51
		public static DisconnectNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectNotification>(bs, 0, -1);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x000E7B5B File Offset: 0x000E5D5B
		public void Deserialize(Stream stream)
		{
			DisconnectNotification.Deserialize(stream, this);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x000E7B65 File Offset: 0x000E5D65
		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance)
		{
			return DisconnectNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x000E7B70 File Offset: 0x000E5D70
		public static DisconnectNotification DeserializeLengthDelimited(Stream stream)
		{
			DisconnectNotification disconnectNotification = new DisconnectNotification();
			DisconnectNotification.DeserializeLengthDelimited(stream, disconnectNotification);
			return disconnectNotification;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000E7B8C File Offset: 0x000E5D8C
		public static DisconnectNotification DeserializeLengthDelimited(Stream stream, DisconnectNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DisconnectNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x000E7BB4 File Offset: 0x000E5DB4
		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance, long limit)
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
						instance.Reason = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x000E7C4B File Offset: 0x000E5E4B
		public void Serialize(Stream stream)
		{
			DisconnectNotification.Serialize(stream, this);
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x000E7C54 File Offset: 0x000E5E54
		public static void Serialize(Stream stream, DisconnectNotification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x000E7C90 File Offset: 0x000E5E90
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			if (this.HasReason)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1U;
		}

		// Token: 0x0400185E RID: 6238
		public bool HasReason;

		// Token: 0x0400185F RID: 6239
		private string _Reason;
	}
}
