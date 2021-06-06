using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F3 RID: 4595
	public class ShopCardClick : IProtoBuf
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600CD85 RID: 52613 RVA: 0x003D4CC8 File Offset: 0x003D2EC8
		// (set) Token: 0x0600CD86 RID: 52614 RVA: 0x003D4CD0 File Offset: 0x003D2ED0
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600CD87 RID: 52615 RVA: 0x003D4CE3 File Offset: 0x003D2EE3
		// (set) Token: 0x0600CD88 RID: 52616 RVA: 0x003D4CEB File Offset: 0x003D2EEB
		public ShopCard Shopcard
		{
			get
			{
				return this._Shopcard;
			}
			set
			{
				this._Shopcard = value;
				this.HasShopcard = (value != null);
			}
		}

		// Token: 0x0600CD89 RID: 52617 RVA: 0x003D4D00 File Offset: 0x003D2F00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasShopcard)
			{
				num ^= this.Shopcard.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD8A RID: 52618 RVA: 0x003D4D48 File Offset: 0x003D2F48
		public override bool Equals(object obj)
		{
			ShopCardClick shopCardClick = obj as ShopCardClick;
			return shopCardClick != null && this.HasPlayer == shopCardClick.HasPlayer && (!this.HasPlayer || this.Player.Equals(shopCardClick.Player)) && this.HasShopcard == shopCardClick.HasShopcard && (!this.HasShopcard || this.Shopcard.Equals(shopCardClick.Shopcard));
		}

		// Token: 0x0600CD8B RID: 52619 RVA: 0x003D4DB8 File Offset: 0x003D2FB8
		public void Deserialize(Stream stream)
		{
			ShopCardClick.Deserialize(stream, this);
		}

		// Token: 0x0600CD8C RID: 52620 RVA: 0x003D4DC2 File Offset: 0x003D2FC2
		public static ShopCardClick Deserialize(Stream stream, ShopCardClick instance)
		{
			return ShopCardClick.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD8D RID: 52621 RVA: 0x003D4DD0 File Offset: 0x003D2FD0
		public static ShopCardClick DeserializeLengthDelimited(Stream stream)
		{
			ShopCardClick shopCardClick = new ShopCardClick();
			ShopCardClick.DeserializeLengthDelimited(stream, shopCardClick);
			return shopCardClick;
		}

		// Token: 0x0600CD8E RID: 52622 RVA: 0x003D4DEC File Offset: 0x003D2FEC
		public static ShopCardClick DeserializeLengthDelimited(Stream stream, ShopCardClick instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopCardClick.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD8F RID: 52623 RVA: 0x003D4E14 File Offset: 0x003D3014
		public static ShopCardClick Deserialize(Stream stream, ShopCardClick instance, long limit)
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
					else if (instance.Shopcard == null)
					{
						instance.Shopcard = ShopCard.DeserializeLengthDelimited(stream);
					}
					else
					{
						ShopCard.DeserializeLengthDelimited(stream, instance.Shopcard);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CD90 RID: 52624 RVA: 0x003D4EE6 File Offset: 0x003D30E6
		public void Serialize(Stream stream)
		{
			ShopCardClick.Serialize(stream, this);
		}

		// Token: 0x0600CD91 RID: 52625 RVA: 0x003D4EF0 File Offset: 0x003D30F0
		public static void Serialize(Stream stream, ShopCardClick instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasShopcard)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Shopcard.GetSerializedSize());
				ShopCard.Serialize(stream, instance.Shopcard);
			}
		}

		// Token: 0x0600CD92 RID: 52626 RVA: 0x003D4F58 File Offset: 0x003D3158
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasShopcard)
			{
				num += 1U;
				uint serializedSize2 = this.Shopcard.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400A10C RID: 41228
		public bool HasPlayer;

		// Token: 0x0400A10D RID: 41229
		private Player _Player;

		// Token: 0x0400A10E RID: 41230
		public bool HasShopcard;

		// Token: 0x0400A10F RID: 41231
		private ShopCard _Shopcard;
	}
}
