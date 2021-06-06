using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A9 RID: 4521
	public class ChangePackQuantity : IProtoBuf
	{
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600C813 RID: 51219 RVA: 0x003C181C File Offset: 0x003BFA1C
		// (set) Token: 0x0600C814 RID: 51220 RVA: 0x003C1824 File Offset: 0x003BFA24
		public int BoosterId
		{
			get
			{
				return this._BoosterId;
			}
			set
			{
				this._BoosterId = value;
				this.HasBoosterId = true;
			}
		}

		// Token: 0x0600C815 RID: 51221 RVA: 0x003C1834 File Offset: 0x003BFA34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBoosterId)
			{
				num ^= this.BoosterId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C816 RID: 51222 RVA: 0x003C1868 File Offset: 0x003BFA68
		public override bool Equals(object obj)
		{
			ChangePackQuantity changePackQuantity = obj as ChangePackQuantity;
			return changePackQuantity != null && this.HasBoosterId == changePackQuantity.HasBoosterId && (!this.HasBoosterId || this.BoosterId.Equals(changePackQuantity.BoosterId));
		}

		// Token: 0x0600C817 RID: 51223 RVA: 0x003C18B0 File Offset: 0x003BFAB0
		public void Deserialize(Stream stream)
		{
			ChangePackQuantity.Deserialize(stream, this);
		}

		// Token: 0x0600C818 RID: 51224 RVA: 0x003C18BA File Offset: 0x003BFABA
		public static ChangePackQuantity Deserialize(Stream stream, ChangePackQuantity instance)
		{
			return ChangePackQuantity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C819 RID: 51225 RVA: 0x003C18C8 File Offset: 0x003BFAC8
		public static ChangePackQuantity DeserializeLengthDelimited(Stream stream)
		{
			ChangePackQuantity changePackQuantity = new ChangePackQuantity();
			ChangePackQuantity.DeserializeLengthDelimited(stream, changePackQuantity);
			return changePackQuantity;
		}

		// Token: 0x0600C81A RID: 51226 RVA: 0x003C18E4 File Offset: 0x003BFAE4
		public static ChangePackQuantity DeserializeLengthDelimited(Stream stream, ChangePackQuantity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChangePackQuantity.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C81B RID: 51227 RVA: 0x003C190C File Offset: 0x003BFB0C
		public static ChangePackQuantity Deserialize(Stream stream, ChangePackQuantity instance, long limit)
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
				else if (num == 8)
				{
					instance.BoosterId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C81C RID: 51228 RVA: 0x003C198C File Offset: 0x003BFB8C
		public void Serialize(Stream stream)
		{
			ChangePackQuantity.Serialize(stream, this);
		}

		// Token: 0x0600C81D RID: 51229 RVA: 0x003C1995 File Offset: 0x003BFB95
		public static void Serialize(Stream stream, ChangePackQuantity instance)
		{
			if (instance.HasBoosterId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterId));
			}
		}

		// Token: 0x0600C81E RID: 51230 RVA: 0x003C19B4 File Offset: 0x003BFBB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBoosterId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterId));
			}
			return num;
		}

		// Token: 0x04009EC9 RID: 40649
		public bool HasBoosterId;

		// Token: 0x04009ECA RID: 40650
		private int _BoosterId;
	}
}
