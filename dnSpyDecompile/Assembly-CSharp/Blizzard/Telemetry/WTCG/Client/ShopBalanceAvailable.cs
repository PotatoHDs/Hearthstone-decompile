using System;
using System.Collections.Generic;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F1 RID: 4593
	public class ShopBalanceAvailable : IProtoBuf
	{
		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x0600CD61 RID: 52577 RVA: 0x003D4433 File Offset: 0x003D2633
		// (set) Token: 0x0600CD62 RID: 52578 RVA: 0x003D443B File Offset: 0x003D263B
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

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600CD63 RID: 52579 RVA: 0x003D444E File Offset: 0x003D264E
		// (set) Token: 0x0600CD64 RID: 52580 RVA: 0x003D4456 File Offset: 0x003D2656
		public List<Balance> Balances
		{
			get
			{
				return this._Balances;
			}
			set
			{
				this._Balances = value;
			}
		}

		// Token: 0x0600CD65 RID: 52581 RVA: 0x003D4460 File Offset: 0x003D2660
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			foreach (Balance balance in this.Balances)
			{
				num ^= balance.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD66 RID: 52582 RVA: 0x003D44D8 File Offset: 0x003D26D8
		public override bool Equals(object obj)
		{
			ShopBalanceAvailable shopBalanceAvailable = obj as ShopBalanceAvailable;
			if (shopBalanceAvailable == null)
			{
				return false;
			}
			if (this.HasPlayer != shopBalanceAvailable.HasPlayer || (this.HasPlayer && !this.Player.Equals(shopBalanceAvailable.Player)))
			{
				return false;
			}
			if (this.Balances.Count != shopBalanceAvailable.Balances.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Balances.Count; i++)
			{
				if (!this.Balances[i].Equals(shopBalanceAvailable.Balances[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CD67 RID: 52583 RVA: 0x003D456E File Offset: 0x003D276E
		public void Deserialize(Stream stream)
		{
			ShopBalanceAvailable.Deserialize(stream, this);
		}

		// Token: 0x0600CD68 RID: 52584 RVA: 0x003D4578 File Offset: 0x003D2778
		public static ShopBalanceAvailable Deserialize(Stream stream, ShopBalanceAvailable instance)
		{
			return ShopBalanceAvailable.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD69 RID: 52585 RVA: 0x003D4584 File Offset: 0x003D2784
		public static ShopBalanceAvailable DeserializeLengthDelimited(Stream stream)
		{
			ShopBalanceAvailable shopBalanceAvailable = new ShopBalanceAvailable();
			ShopBalanceAvailable.DeserializeLengthDelimited(stream, shopBalanceAvailable);
			return shopBalanceAvailable;
		}

		// Token: 0x0600CD6A RID: 52586 RVA: 0x003D45A0 File Offset: 0x003D27A0
		public static ShopBalanceAvailable DeserializeLengthDelimited(Stream stream, ShopBalanceAvailable instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopBalanceAvailable.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD6B RID: 52587 RVA: 0x003D45C8 File Offset: 0x003D27C8
		public static ShopBalanceAvailable Deserialize(Stream stream, ShopBalanceAvailable instance, long limit)
		{
			if (instance.Balances == null)
			{
				instance.Balances = new List<Balance>();
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
						instance.Balances.Add(Balance.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600CD6C RID: 52588 RVA: 0x003D4692 File Offset: 0x003D2892
		public void Serialize(Stream stream)
		{
			ShopBalanceAvailable.Serialize(stream, this);
		}

		// Token: 0x0600CD6D RID: 52589 RVA: 0x003D469C File Offset: 0x003D289C
		public static void Serialize(Stream stream, ShopBalanceAvailable instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.Balances.Count > 0)
			{
				foreach (Balance balance in instance.Balances)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, balance.GetSerializedSize());
					Balance.Serialize(stream, balance);
				}
			}
		}

		// Token: 0x0600CD6E RID: 52590 RVA: 0x003D4740 File Offset: 0x003D2940
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Balances.Count > 0)
			{
				foreach (Balance balance in this.Balances)
				{
					num += 1U;
					uint serializedSize2 = balance.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400A0FF RID: 41215
		public bool HasPlayer;

		// Token: 0x0400A100 RID: 41216
		private Player _Player;

		// Token: 0x0400A101 RID: 41217
		private List<Balance> _Balances = new List<Balance>();
	}
}
