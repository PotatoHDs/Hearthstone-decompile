using System;
using System.IO;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000319 RID: 793
	public class SessionOptions : IProtoBuf
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000A2EE3 File Offset: 0x000A10E3
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x000A2EEB File Offset: 0x000A10EB
		public bool Billing
		{
			get
			{
				return this._Billing;
			}
			set
			{
				this._Billing = value;
				this.HasBilling = true;
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000A2EFB File Offset: 0x000A10FB
		public void SetBilling(bool val)
		{
			this.Billing = val;
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000A2F04 File Offset: 0x000A1104
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000A2F0C File Offset: 0x000A110C
		public bool Presence
		{
			get
			{
				return this._Presence;
			}
			set
			{
				this._Presence = value;
				this.HasPresence = true;
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000A2F1C File Offset: 0x000A111C
		public void SetPresence(bool val)
		{
			this.Presence = val;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000A2F28 File Offset: 0x000A1128
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBilling)
			{
				num ^= this.Billing.GetHashCode();
			}
			if (this.HasPresence)
			{
				num ^= this.Presence.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000A2F74 File Offset: 0x000A1174
		public override bool Equals(object obj)
		{
			SessionOptions sessionOptions = obj as SessionOptions;
			return sessionOptions != null && this.HasBilling == sessionOptions.HasBilling && (!this.HasBilling || this.Billing.Equals(sessionOptions.Billing)) && this.HasPresence == sessionOptions.HasPresence && (!this.HasPresence || this.Presence.Equals(sessionOptions.Presence));
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000A2FEA File Offset: 0x000A11EA
		public static SessionOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionOptions>(bs, 0, -1);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000A2FF4 File Offset: 0x000A11F4
		public void Deserialize(Stream stream)
		{
			SessionOptions.Deserialize(stream, this);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000A2FFE File Offset: 0x000A11FE
		public static SessionOptions Deserialize(Stream stream, SessionOptions instance)
		{
			return SessionOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000A300C File Offset: 0x000A120C
		public static SessionOptions DeserializeLengthDelimited(Stream stream)
		{
			SessionOptions sessionOptions = new SessionOptions();
			SessionOptions.DeserializeLengthDelimited(stream, sessionOptions);
			return sessionOptions;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000A3028 File Offset: 0x000A1228
		public static SessionOptions DeserializeLengthDelimited(Stream stream, SessionOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000A3050 File Offset: 0x000A1250
		public static SessionOptions Deserialize(Stream stream, SessionOptions instance, long limit)
		{
			instance.Billing = true;
			instance.Presence = true;
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
						instance.Presence = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Billing = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000A30F5 File Offset: 0x000A12F5
		public void Serialize(Stream stream)
		{
			SessionOptions.Serialize(stream, this);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000A30FE File Offset: 0x000A12FE
		public static void Serialize(Stream stream, SessionOptions instance)
		{
			if (instance.HasBilling)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Billing);
			}
			if (instance.HasPresence)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Presence);
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000A3138 File Offset: 0x000A1338
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBilling)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPresence)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400133B RID: 4923
		public bool HasBilling;

		// Token: 0x0400133C RID: 4924
		private bool _Billing;

		// Token: 0x0400133D RID: 4925
		public bool HasPresence;

		// Token: 0x0400133E RID: 4926
		private bool _Presence;
	}
}
