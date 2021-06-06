using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000317 RID: 791
	public class SessionUpdatedNotification : IProtoBuf
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x000A26FD File Offset: 0x000A08FD
		// (set) Token: 0x06003019 RID: 12313 RVA: 0x000A2705 File Offset: 0x000A0905
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

		// Token: 0x0600301A RID: 12314 RVA: 0x000A2718 File Offset: 0x000A0918
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000A2721 File Offset: 0x000A0921
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x000A2729 File Offset: 0x000A0929
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000A2739 File Offset: 0x000A0939
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000A2742 File Offset: 0x000A0942
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x000A274A File Offset: 0x000A094A
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

		// Token: 0x06003020 RID: 12320 RVA: 0x000A275D File Offset: 0x000A095D
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000A2768 File Offset: 0x000A0968
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000A27C8 File Offset: 0x000A09C8
		public override bool Equals(object obj)
		{
			SessionUpdatedNotification sessionUpdatedNotification = obj as SessionUpdatedNotification;
			return sessionUpdatedNotification != null && this.HasIdentity == sessionUpdatedNotification.HasIdentity && (!this.HasIdentity || this.Identity.Equals(sessionUpdatedNotification.Identity)) && this.HasReason == sessionUpdatedNotification.HasReason && (!this.HasReason || this.Reason.Equals(sessionUpdatedNotification.Reason)) && this.HasSessionId == sessionUpdatedNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(sessionUpdatedNotification.SessionId));
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000A2866 File Offset: 0x000A0A66
		public static SessionUpdatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionUpdatedNotification>(bs, 0, -1);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000A2870 File Offset: 0x000A0A70
		public void Deserialize(Stream stream)
		{
			SessionUpdatedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000A287A File Offset: 0x000A0A7A
		public static SessionUpdatedNotification Deserialize(Stream stream, SessionUpdatedNotification instance)
		{
			return SessionUpdatedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000A2888 File Offset: 0x000A0A88
		public static SessionUpdatedNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionUpdatedNotification sessionUpdatedNotification = new SessionUpdatedNotification();
			SessionUpdatedNotification.DeserializeLengthDelimited(stream, sessionUpdatedNotification);
			return sessionUpdatedNotification;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000A28A4 File Offset: 0x000A0AA4
		public static SessionUpdatedNotification DeserializeLengthDelimited(Stream stream, SessionUpdatedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionUpdatedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000A28CC File Offset: 0x000A0ACC
		public static SessionUpdatedNotification Deserialize(Stream stream, SessionUpdatedNotification instance, long limit)
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
					if (num != 16)
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
					else
					{
						instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x0600302A RID: 12330 RVA: 0x000A299A File Offset: 0x000A0B9A
		public void Serialize(Stream stream)
		{
			SessionUpdatedNotification.Serialize(stream, this);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000A29A4 File Offset: 0x000A0BA4
		public static void Serialize(Stream stream, SessionUpdatedNotification instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000A2A20 File Offset: 0x000A0C20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400132D RID: 4909
		public bool HasIdentity;

		// Token: 0x0400132E RID: 4910
		private Identity _Identity;

		// Token: 0x0400132F RID: 4911
		public bool HasReason;

		// Token: 0x04001330 RID: 4912
		private uint _Reason;

		// Token: 0x04001331 RID: 4913
		public bool HasSessionId;

		// Token: 0x04001332 RID: 4914
		private string _SessionId;
	}
}
