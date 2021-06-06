using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000316 RID: 790
	public class SessionDestroyedNotification : IProtoBuf
	{
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x000A2366 File Offset: 0x000A0566
		// (set) Token: 0x06003003 RID: 12291 RVA: 0x000A236E File Offset: 0x000A056E
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

		// Token: 0x06003004 RID: 12292 RVA: 0x000A2381 File Offset: 0x000A0581
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000A238A File Offset: 0x000A058A
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x000A2392 File Offset: 0x000A0592
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

		// Token: 0x06003007 RID: 12295 RVA: 0x000A23A2 File Offset: 0x000A05A2
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000A23AB File Offset: 0x000A05AB
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000A23B3 File Offset: 0x000A05B3
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

		// Token: 0x0600300A RID: 12298 RVA: 0x000A23C6 File Offset: 0x000A05C6
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000A23D0 File Offset: 0x000A05D0
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

		// Token: 0x0600300C RID: 12300 RVA: 0x000A2430 File Offset: 0x000A0630
		public override bool Equals(object obj)
		{
			SessionDestroyedNotification sessionDestroyedNotification = obj as SessionDestroyedNotification;
			return sessionDestroyedNotification != null && this.HasIdentity == sessionDestroyedNotification.HasIdentity && (!this.HasIdentity || this.Identity.Equals(sessionDestroyedNotification.Identity)) && this.HasReason == sessionDestroyedNotification.HasReason && (!this.HasReason || this.Reason.Equals(sessionDestroyedNotification.Reason)) && this.HasSessionId == sessionDestroyedNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(sessionDestroyedNotification.SessionId));
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000A24CE File Offset: 0x000A06CE
		public static SessionDestroyedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionDestroyedNotification>(bs, 0, -1);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000A24D8 File Offset: 0x000A06D8
		public void Deserialize(Stream stream)
		{
			SessionDestroyedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000A24E2 File Offset: 0x000A06E2
		public static SessionDestroyedNotification Deserialize(Stream stream, SessionDestroyedNotification instance)
		{
			return SessionDestroyedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000A24F0 File Offset: 0x000A06F0
		public static SessionDestroyedNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionDestroyedNotification sessionDestroyedNotification = new SessionDestroyedNotification();
			SessionDestroyedNotification.DeserializeLengthDelimited(stream, sessionDestroyedNotification);
			return sessionDestroyedNotification;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000A250C File Offset: 0x000A070C
		public static SessionDestroyedNotification DeserializeLengthDelimited(Stream stream, SessionDestroyedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionDestroyedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000A2534 File Offset: 0x000A0734
		public static SessionDestroyedNotification Deserialize(Stream stream, SessionDestroyedNotification instance, long limit)
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

		// Token: 0x06003014 RID: 12308 RVA: 0x000A2602 File Offset: 0x000A0802
		public void Serialize(Stream stream)
		{
			SessionDestroyedNotification.Serialize(stream, this);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000A260C File Offset: 0x000A080C
		public static void Serialize(Stream stream, SessionDestroyedNotification instance)
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

		// Token: 0x06003016 RID: 12310 RVA: 0x000A2688 File Offset: 0x000A0888
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

		// Token: 0x04001327 RID: 4903
		public bool HasIdentity;

		// Token: 0x04001328 RID: 4904
		private Identity _Identity;

		// Token: 0x04001329 RID: 4905
		public bool HasReason;

		// Token: 0x0400132A RID: 4906
		private uint _Reason;

		// Token: 0x0400132B RID: 4907
		public bool HasSessionId;

		// Token: 0x0400132C RID: 4908
		private string _SessionId;
	}
}
