using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001AF RID: 431
	public class InviteToSpectate : IProtoBuf
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000603CA File Offset: 0x0005E5CA
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x000603D2 File Offset: 0x0005E5D2
		public BnetId TargetBnetAccountId
		{
			get
			{
				return this._TargetBnetAccountId;
			}
			set
			{
				this._TargetBnetAccountId = value;
				this.HasTargetBnetAccountId = (value != null);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000603E5 File Offset: 0x0005E5E5
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x000603ED File Offset: 0x0005E5ED
		public BnetId TargetGameAccountId { get; set; }

		// Token: 0x06001B42 RID: 6978 RVA: 0x000603F8 File Offset: 0x0005E5F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetBnetAccountId)
			{
				num ^= this.TargetBnetAccountId.GetHashCode();
			}
			return num ^ this.TargetGameAccountId.GetHashCode();
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00060438 File Offset: 0x0005E638
		public override bool Equals(object obj)
		{
			InviteToSpectate inviteToSpectate = obj as InviteToSpectate;
			return inviteToSpectate != null && this.HasTargetBnetAccountId == inviteToSpectate.HasTargetBnetAccountId && (!this.HasTargetBnetAccountId || this.TargetBnetAccountId.Equals(inviteToSpectate.TargetBnetAccountId)) && this.TargetGameAccountId.Equals(inviteToSpectate.TargetGameAccountId);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00060492 File Offset: 0x0005E692
		public void Deserialize(Stream stream)
		{
			InviteToSpectate.Deserialize(stream, this);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0006049C File Offset: 0x0005E69C
		public static InviteToSpectate Deserialize(Stream stream, InviteToSpectate instance)
		{
			return InviteToSpectate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000604A8 File Offset: 0x0005E6A8
		public static InviteToSpectate DeserializeLengthDelimited(Stream stream)
		{
			InviteToSpectate inviteToSpectate = new InviteToSpectate();
			InviteToSpectate.DeserializeLengthDelimited(stream, inviteToSpectate);
			return inviteToSpectate;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000604C4 File Offset: 0x0005E6C4
		public static InviteToSpectate DeserializeLengthDelimited(Stream stream, InviteToSpectate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InviteToSpectate.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000604EC File Offset: 0x0005E6EC
		public static InviteToSpectate Deserialize(Stream stream, InviteToSpectate instance, long limit)
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
					else if (instance.TargetGameAccountId == null)
					{
						instance.TargetGameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.TargetGameAccountId);
					}
				}
				else if (instance.TargetBnetAccountId == null)
				{
					instance.TargetBnetAccountId = BnetId.DeserializeLengthDelimited(stream);
				}
				else
				{
					BnetId.DeserializeLengthDelimited(stream, instance.TargetBnetAccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000605BE File Offset: 0x0005E7BE
		public void Serialize(Stream stream)
		{
			InviteToSpectate.Serialize(stream, this);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000605C8 File Offset: 0x0005E7C8
		public static void Serialize(Stream stream, InviteToSpectate instance)
		{
			if (instance.HasTargetBnetAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetBnetAccountId.GetSerializedSize());
				BnetId.Serialize(stream, instance.TargetBnetAccountId);
			}
			if (instance.TargetGameAccountId == null)
			{
				throw new ArgumentNullException("TargetGameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetGameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.TargetGameAccountId);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00060640 File Offset: 0x0005E840
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetBnetAccountId)
			{
				num += 1U;
				uint serializedSize = this.TargetBnetAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetGameAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1U;
		}

		// Token: 0x04000A0B RID: 2571
		public bool HasTargetBnetAccountId;

		// Token: 0x04000A0C RID: 2572
		private BnetId _TargetBnetAccountId;

		// Token: 0x02000646 RID: 1606
		public enum PacketID
		{
			// Token: 0x04002100 RID: 8448
			ID = 25
		}
	}
}
