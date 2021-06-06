using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000309 RID: 777
	public class UpdateSessionRequest : IProtoBuf
	{
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x0009FBA8 File Offset: 0x0009DDA8
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x0009FBB0 File Offset: 0x0009DDB0
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0009FBC3 File Offset: 0x0009DDC3
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x0009FBCC File Offset: 0x0009DDCC
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x0009FBD4 File Offset: 0x0009DDD4
		public SessionOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0009FBE7 File Offset: 0x0009DDE7
		public void SetOptions(SessionOptions val)
		{
			this.Options = val;
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x0009FBF0 File Offset: 0x0009DDF0
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x0009FBF8 File Offset: 0x0009DDF8
		public string SessionId
		{
			get
			{
				return this._SessionId;
			}
			set
			{
				this._SessionId = value;
				this.HasSessionId = (value != null);
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0009FC0B File Offset: 0x0009DE0B
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0009FC14 File Offset: 0x0009DE14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0009FC70 File Offset: 0x0009DE70
		public override bool Equals(object obj)
		{
			UpdateSessionRequest updateSessionRequest = obj as UpdateSessionRequest;
			return updateSessionRequest != null && this.HasIdentity == updateSessionRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(updateSessionRequest.Identity)) && this.HasOptions == updateSessionRequest.HasOptions && (!this.HasOptions || this.Options.Equals(updateSessionRequest.Options)) && this.HasSessionId == updateSessionRequest.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(updateSessionRequest.SessionId));
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0009FD0B File Offset: 0x0009DF0B
		public static UpdateSessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateSessionRequest>(bs, 0, -1);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0009FD15 File Offset: 0x0009DF15
		public void Deserialize(Stream stream)
		{
			UpdateSessionRequest.Deserialize(stream, this);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0009FD1F File Offset: 0x0009DF1F
		public static UpdateSessionRequest Deserialize(Stream stream, UpdateSessionRequest instance)
		{
			return UpdateSessionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0009FD2C File Offset: 0x0009DF2C
		public static UpdateSessionRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateSessionRequest updateSessionRequest = new UpdateSessionRequest();
			UpdateSessionRequest.DeserializeLengthDelimited(stream, updateSessionRequest);
			return updateSessionRequest;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0009FD48 File Offset: 0x0009DF48
		public static UpdateSessionRequest DeserializeLengthDelimited(Stream stream, UpdateSessionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateSessionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0009FD70 File Offset: 0x0009DF70
		public static UpdateSessionRequest Deserialize(Stream stream, UpdateSessionRequest instance, long limit)
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
							instance.SessionId = ProtocolParser.ReadString(stream);
						}
					}
					else if (instance.Options == null)
					{
						instance.Options = SessionOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SessionOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0009FE58 File Offset: 0x0009E058
		public void Serialize(Stream stream)
		{
			UpdateSessionRequest.Serialize(stream, this);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0009FE64 File Offset: 0x0009E064
		public static void Serialize(Stream stream, UpdateSessionRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SessionOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040012F6 RID: 4854
		public bool HasIdentity;

		// Token: 0x040012F7 RID: 4855
		private Identity _Identity;

		// Token: 0x040012F8 RID: 4856
		public bool HasOptions;

		// Token: 0x040012F9 RID: 4857
		private SessionOptions _Options;

		// Token: 0x040012FA RID: 4858
		public bool HasSessionId;

		// Token: 0x040012FB RID: 4859
		private string _SessionId;
	}
}
