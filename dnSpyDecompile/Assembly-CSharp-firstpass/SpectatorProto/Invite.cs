using System;
using System.IO;
using PegasusShared;

namespace SpectatorProto
{
	// Token: 0x0200002D RID: 45
	public class Invite : IProtoBuf
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A267 File Offset: 0x00008467
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A26F File Offset: 0x0000846F
		public BnetId InviterGameAccountId { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A278 File Offset: 0x00008478
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A280 File Offset: 0x00008480
		public JoinInfo JoinInfo { get; set; }

		// Token: 0x06000251 RID: 593 RVA: 0x0000A289 File Offset: 0x00008489
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.InviterGameAccountId.GetHashCode() ^ this.JoinInfo.GetHashCode();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public override bool Equals(object obj)
		{
			Invite invite = obj as Invite;
			return invite != null && this.InviterGameAccountId.Equals(invite.InviterGameAccountId) && this.JoinInfo.Equals(invite.JoinInfo);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public void Deserialize(Stream stream)
		{
			Invite.Deserialize(stream, this);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A2FE File Offset: 0x000084FE
		public static Invite Deserialize(Stream stream, Invite instance)
		{
			return Invite.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A30C File Offset: 0x0000850C
		public static Invite DeserializeLengthDelimited(Stream stream)
		{
			Invite invite = new Invite();
			Invite.DeserializeLengthDelimited(stream, invite);
			return invite;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A328 File Offset: 0x00008528
		public static Invite DeserializeLengthDelimited(Stream stream, Invite instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Invite.Deserialize(stream, instance, num);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A350 File Offset: 0x00008550
		public static Invite Deserialize(Stream stream, Invite instance, long limit)
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
					else if (instance.JoinInfo == null)
					{
						instance.JoinInfo = JoinInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						JoinInfo.DeserializeLengthDelimited(stream, instance.JoinInfo);
					}
				}
				else if (instance.InviterGameAccountId == null)
				{
					instance.InviterGameAccountId = BnetId.DeserializeLengthDelimited(stream);
				}
				else
				{
					BnetId.DeserializeLengthDelimited(stream, instance.InviterGameAccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A422 File Offset: 0x00008622
		public void Serialize(Stream stream)
		{
			Invite.Serialize(stream, this);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A42C File Offset: 0x0000862C
		public static void Serialize(Stream stream, Invite instance)
		{
			if (instance.InviterGameAccountId == null)
			{
				throw new ArgumentNullException("InviterGameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.InviterGameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.InviterGameAccountId);
			if (instance.JoinInfo == null)
			{
				throw new ArgumentNullException("JoinInfo", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.JoinInfo.GetSerializedSize());
			JoinInfo.Serialize(stream, instance.JoinInfo);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.InviterGameAccountId.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.JoinInfo.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
