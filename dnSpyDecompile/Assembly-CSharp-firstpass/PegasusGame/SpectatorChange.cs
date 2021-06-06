using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001D0 RID: 464
	public class SpectatorChange : IProtoBuf
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0006835B File Offset: 0x0006655B
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x00068363 File Offset: 0x00066563
		public BnetId GameAccountId { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0006836C File Offset: 0x0006656C
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x00068374 File Offset: 0x00066574
		public bool IsRemoved { get; set; }

		// Token: 0x06001D9F RID: 7583 RVA: 0x00068380 File Offset: 0x00066580
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GameAccountId.GetHashCode() ^ this.IsRemoved.GetHashCode();
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000683B4 File Offset: 0x000665B4
		public override bool Equals(object obj)
		{
			SpectatorChange spectatorChange = obj as SpectatorChange;
			return spectatorChange != null && this.GameAccountId.Equals(spectatorChange.GameAccountId) && this.IsRemoved.Equals(spectatorChange.IsRemoved);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000683FB File Offset: 0x000665FB
		public void Deserialize(Stream stream)
		{
			SpectatorChange.Deserialize(stream, this);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00068405 File Offset: 0x00066605
		public static SpectatorChange Deserialize(Stream stream, SpectatorChange instance)
		{
			return SpectatorChange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00068410 File Offset: 0x00066610
		public static SpectatorChange DeserializeLengthDelimited(Stream stream)
		{
			SpectatorChange spectatorChange = new SpectatorChange();
			SpectatorChange.DeserializeLengthDelimited(stream, spectatorChange);
			return spectatorChange;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006842C File Offset: 0x0006662C
		public static SpectatorChange DeserializeLengthDelimited(Stream stream, SpectatorChange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpectatorChange.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00068454 File Offset: 0x00066654
		public static SpectatorChange Deserialize(Stream stream, SpectatorChange instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.IsRemoved = ProtocolParser.ReadBool(stream);
					}
				}
				else if (instance.GameAccountId == null)
				{
					instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
				}
				else
				{
					BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00068506 File Offset: 0x00066706
		public void Serialize(Stream stream)
		{
			SpectatorChange.Serialize(stream, this);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00068510 File Offset: 0x00066710
		public static void Serialize(Stream stream, SpectatorChange instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.IsRemoved);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00068570 File Offset: 0x00066770
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U + 2U;
		}
	}
}
