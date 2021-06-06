using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A5 RID: 4517
	public class BlizzardCheckoutPurchaseCompletedSuccess : IProtoBuf
	{
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x0600C7CB RID: 51147 RVA: 0x003C0877 File Offset: 0x003BEA77
		// (set) Token: 0x0600C7CC RID: 51148 RVA: 0x003C087F File Offset: 0x003BEA7F
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

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x0600C7CD RID: 51149 RVA: 0x003C0892 File Offset: 0x003BEA92
		// (set) Token: 0x0600C7CE RID: 51150 RVA: 0x003C089A File Offset: 0x003BEA9A
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600C7CF RID: 51151 RVA: 0x003C08AD File Offset: 0x003BEAAD
		// (set) Token: 0x0600C7D0 RID: 51152 RVA: 0x003C08B5 File Offset: 0x003BEAB5
		public string TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = (value != null);
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x0600C7D1 RID: 51153 RVA: 0x003C08C8 File Offset: 0x003BEAC8
		// (set) Token: 0x0600C7D2 RID: 51154 RVA: 0x003C08D0 File Offset: 0x003BEAD0
		public string ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				this._ProductId = value;
				this.HasProductId = (value != null);
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600C7D3 RID: 51155 RVA: 0x003C08E3 File Offset: 0x003BEAE3
		// (set) Token: 0x0600C7D4 RID: 51156 RVA: 0x003C08EB File Offset: 0x003BEAEB
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				this._Currency = value;
				this.HasCurrency = (value != null);
			}
		}

		// Token: 0x0600C7D5 RID: 51157 RVA: 0x003C0900 File Offset: 0x003BEB00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasProductId)
			{
				num ^= this.ProductId.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C7D6 RID: 51158 RVA: 0x003C0988 File Offset: 0x003BEB88
		public override bool Equals(object obj)
		{
			BlizzardCheckoutPurchaseCompletedSuccess blizzardCheckoutPurchaseCompletedSuccess = obj as BlizzardCheckoutPurchaseCompletedSuccess;
			return blizzardCheckoutPurchaseCompletedSuccess != null && this.HasPlayer == blizzardCheckoutPurchaseCompletedSuccess.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutPurchaseCompletedSuccess.Player)) && this.HasDeviceInfo == blizzardCheckoutPurchaseCompletedSuccess.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutPurchaseCompletedSuccess.DeviceInfo)) && this.HasTransactionId == blizzardCheckoutPurchaseCompletedSuccess.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(blizzardCheckoutPurchaseCompletedSuccess.TransactionId)) && this.HasProductId == blizzardCheckoutPurchaseCompletedSuccess.HasProductId && (!this.HasProductId || this.ProductId.Equals(blizzardCheckoutPurchaseCompletedSuccess.ProductId)) && this.HasCurrency == blizzardCheckoutPurchaseCompletedSuccess.HasCurrency && (!this.HasCurrency || this.Currency.Equals(blizzardCheckoutPurchaseCompletedSuccess.Currency));
		}

		// Token: 0x0600C7D7 RID: 51159 RVA: 0x003C0A79 File Offset: 0x003BEC79
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedSuccess.Deserialize(stream, this);
		}

		// Token: 0x0600C7D8 RID: 51160 RVA: 0x003C0A83 File Offset: 0x003BEC83
		public static BlizzardCheckoutPurchaseCompletedSuccess Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
		{
			return BlizzardCheckoutPurchaseCompletedSuccess.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C7D9 RID: 51161 RVA: 0x003C0A90 File Offset: 0x003BEC90
		public static BlizzardCheckoutPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedSuccess blizzardCheckoutPurchaseCompletedSuccess = new BlizzardCheckoutPurchaseCompletedSuccess();
			BlizzardCheckoutPurchaseCompletedSuccess.DeserializeLengthDelimited(stream, blizzardCheckoutPurchaseCompletedSuccess);
			return blizzardCheckoutPurchaseCompletedSuccess;
		}

		// Token: 0x0600C7DA RID: 51162 RVA: 0x003C0AAC File Offset: 0x003BECAC
		public static BlizzardCheckoutPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutPurchaseCompletedSuccess.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C7DB RID: 51163 RVA: 0x003C0AD4 File Offset: 0x003BECD4
		public static BlizzardCheckoutPurchaseCompletedSuccess Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Currency = ProtocolParser.ReadString(stream);
							continue;
						}
					}
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

		// Token: 0x0600C7DC RID: 51164 RVA: 0x003C0BF8 File Offset: 0x003BEDF8
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedSuccess.Serialize(stream, this);
		}

		// Token: 0x0600C7DD RID: 51165 RVA: 0x003C0C04 File Offset: 0x003BEE04
		public static void Serialize(Stream stream, BlizzardCheckoutPurchaseCompletedSuccess instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
		}

		// Token: 0x0600C7DE RID: 51166 RVA: 0x003C0CE0 File Offset: 0x003BEEE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasTransactionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProductId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009EAD RID: 40621
		public bool HasPlayer;

		// Token: 0x04009EAE RID: 40622
		private Player _Player;

		// Token: 0x04009EAF RID: 40623
		public bool HasDeviceInfo;

		// Token: 0x04009EB0 RID: 40624
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EB1 RID: 40625
		public bool HasTransactionId;

		// Token: 0x04009EB2 RID: 40626
		private string _TransactionId;

		// Token: 0x04009EB3 RID: 40627
		public bool HasProductId;

		// Token: 0x04009EB4 RID: 40628
		private string _ProductId;

		// Token: 0x04009EB5 RID: 40629
		public bool HasCurrency;

		// Token: 0x04009EB6 RID: 40630
		private string _Currency;
	}
}
