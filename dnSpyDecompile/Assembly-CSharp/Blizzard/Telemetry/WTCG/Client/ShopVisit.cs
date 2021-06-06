using System;
using System.Collections.Generic;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F6 RID: 4598
	public class ShopVisit : IProtoBuf
	{
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600CDC2 RID: 52674 RVA: 0x003D5B2F File Offset: 0x003D3D2F
		// (set) Token: 0x0600CDC3 RID: 52675 RVA: 0x003D5B37 File Offset: 0x003D3D37
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

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600CDC4 RID: 52676 RVA: 0x003D5B4A File Offset: 0x003D3D4A
		// (set) Token: 0x0600CDC5 RID: 52677 RVA: 0x003D5B52 File Offset: 0x003D3D52
		public List<ShopCard> Cards
		{
			get
			{
				return this._Cards;
			}
			set
			{
				this._Cards = value;
			}
		}

		// Token: 0x0600CDC6 RID: 52678 RVA: 0x003D5B5C File Offset: 0x003D3D5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			foreach (ShopCard shopCard in this.Cards)
			{
				num ^= shopCard.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CDC7 RID: 52679 RVA: 0x003D5BD4 File Offset: 0x003D3DD4
		public override bool Equals(object obj)
		{
			ShopVisit shopVisit = obj as ShopVisit;
			if (shopVisit == null)
			{
				return false;
			}
			if (this.HasPlayer != shopVisit.HasPlayer || (this.HasPlayer && !this.Player.Equals(shopVisit.Player)))
			{
				return false;
			}
			if (this.Cards.Count != shopVisit.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(shopVisit.Cards[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CDC8 RID: 52680 RVA: 0x003D5C6A File Offset: 0x003D3E6A
		public void Deserialize(Stream stream)
		{
			ShopVisit.Deserialize(stream, this);
		}

		// Token: 0x0600CDC9 RID: 52681 RVA: 0x003D5C74 File Offset: 0x003D3E74
		public static ShopVisit Deserialize(Stream stream, ShopVisit instance)
		{
			return ShopVisit.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDCA RID: 52682 RVA: 0x003D5C80 File Offset: 0x003D3E80
		public static ShopVisit DeserializeLengthDelimited(Stream stream)
		{
			ShopVisit shopVisit = new ShopVisit();
			ShopVisit.DeserializeLengthDelimited(stream, shopVisit);
			return shopVisit;
		}

		// Token: 0x0600CDCB RID: 52683 RVA: 0x003D5C9C File Offset: 0x003D3E9C
		public static ShopVisit DeserializeLengthDelimited(Stream stream, ShopVisit instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopVisit.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDCC RID: 52684 RVA: 0x003D5CC4 File Offset: 0x003D3EC4
		public static ShopVisit Deserialize(Stream stream, ShopVisit instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<ShopCard>();
			}
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
					else
					{
						instance.Cards.Add(ShopCard.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600CDCD RID: 52685 RVA: 0x003D5D8E File Offset: 0x003D3F8E
		public void Serialize(Stream stream)
		{
			ShopVisit.Serialize(stream, this);
		}

		// Token: 0x0600CDCE RID: 52686 RVA: 0x003D5D98 File Offset: 0x003D3F98
		public static void Serialize(Stream stream, ShopVisit instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.Cards.Count > 0)
			{
				foreach (ShopCard shopCard in instance.Cards)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, shopCard.GetSerializedSize());
					ShopCard.Serialize(stream, shopCard);
				}
			}
		}

		// Token: 0x0600CDCF RID: 52687 RVA: 0x003D5E3C File Offset: 0x003D403C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Cards.Count > 0)
			{
				foreach (ShopCard shopCard in this.Cards)
				{
					num += 1U;
					uint serializedSize2 = shopCard.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400A128 RID: 41256
		public bool HasPlayer;

		// Token: 0x0400A129 RID: 41257
		private Player _Player;

		// Token: 0x0400A12A RID: 41258
		private List<ShopCard> _Cards = new List<ShopCard>();
	}
}
