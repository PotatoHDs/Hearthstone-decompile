using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D4 RID: 980
	public class MatchmakerControlProperties : IProtoBuf
	{
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000CD1AF File Offset: 0x000CB3AF
		// (set) Token: 0x0600404D RID: 16461 RVA: 0x000CD1B7 File Offset: 0x000CB3B7
		public bool AcceptNewEntries
		{
			get
			{
				return this._AcceptNewEntries;
			}
			set
			{
				this._AcceptNewEntries = value;
				this.HasAcceptNewEntries = true;
			}
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000CD1C7 File Offset: 0x000CB3C7
		public void SetAcceptNewEntries(bool val)
		{
			this.AcceptNewEntries = val;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000CD1D0 File Offset: 0x000CB3D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAcceptNewEntries)
			{
				num ^= this.AcceptNewEntries.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000CD204 File Offset: 0x000CB404
		public override bool Equals(object obj)
		{
			MatchmakerControlProperties matchmakerControlProperties = obj as MatchmakerControlProperties;
			return matchmakerControlProperties != null && this.HasAcceptNewEntries == matchmakerControlProperties.HasAcceptNewEntries && (!this.HasAcceptNewEntries || this.AcceptNewEntries.Equals(matchmakerControlProperties.AcceptNewEntries));
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000CD24C File Offset: 0x000CB44C
		public static MatchmakerControlProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerControlProperties>(bs, 0, -1);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000CD256 File Offset: 0x000CB456
		public void Deserialize(Stream stream)
		{
			MatchmakerControlProperties.Deserialize(stream, this);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000CD260 File Offset: 0x000CB460
		public static MatchmakerControlProperties Deserialize(Stream stream, MatchmakerControlProperties instance)
		{
			return MatchmakerControlProperties.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000CD26C File Offset: 0x000CB46C
		public static MatchmakerControlProperties DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerControlProperties matchmakerControlProperties = new MatchmakerControlProperties();
			MatchmakerControlProperties.DeserializeLengthDelimited(stream, matchmakerControlProperties);
			return matchmakerControlProperties;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000CD288 File Offset: 0x000CB488
		public static MatchmakerControlProperties DeserializeLengthDelimited(Stream stream, MatchmakerControlProperties instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakerControlProperties.Deserialize(stream, instance, num);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x000CD2B0 File Offset: 0x000CB4B0
		public static MatchmakerControlProperties Deserialize(Stream stream, MatchmakerControlProperties instance, long limit)
		{
			instance.AcceptNewEntries = true;
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
				else if (num == 8)
				{
					instance.AcceptNewEntries = ProtocolParser.ReadBool(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000CD336 File Offset: 0x000CB536
		public void Serialize(Stream stream)
		{
			MatchmakerControlProperties.Serialize(stream, this);
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000CD33F File Offset: 0x000CB53F
		public static void Serialize(Stream stream, MatchmakerControlProperties instance)
		{
			if (instance.HasAcceptNewEntries)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AcceptNewEntries);
			}
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000CD35C File Offset: 0x000CB55C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAcceptNewEntries)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001670 RID: 5744
		public bool HasAcceptNewEntries;

		// Token: 0x04001671 RID: 5745
		private bool _AcceptNewEntries;
	}
}
