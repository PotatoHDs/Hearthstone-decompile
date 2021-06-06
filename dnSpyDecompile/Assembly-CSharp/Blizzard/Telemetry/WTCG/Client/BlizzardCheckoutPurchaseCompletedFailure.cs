using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A4 RID: 4516
	public class BlizzardCheckoutPurchaseCompletedFailure : IProtoBuf
	{
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600C7B4 RID: 51124 RVA: 0x003C0176 File Offset: 0x003BE376
		// (set) Token: 0x0600C7B5 RID: 51125 RVA: 0x003C017E File Offset: 0x003BE37E
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

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x0600C7B6 RID: 51126 RVA: 0x003C0191 File Offset: 0x003BE391
		// (set) Token: 0x0600C7B7 RID: 51127 RVA: 0x003C0199 File Offset: 0x003BE399
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

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x0600C7B8 RID: 51128 RVA: 0x003C01AC File Offset: 0x003BE3AC
		// (set) Token: 0x0600C7B9 RID: 51129 RVA: 0x003C01B4 File Offset: 0x003BE3B4
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

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x0600C7BA RID: 51130 RVA: 0x003C01C7 File Offset: 0x003BE3C7
		// (set) Token: 0x0600C7BB RID: 51131 RVA: 0x003C01CF File Offset: 0x003BE3CF
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

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x0600C7BC RID: 51132 RVA: 0x003C01E2 File Offset: 0x003BE3E2
		// (set) Token: 0x0600C7BD RID: 51133 RVA: 0x003C01EA File Offset: 0x003BE3EA
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

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600C7BE RID: 51134 RVA: 0x003C01FD File Offset: 0x003BE3FD
		// (set) Token: 0x0600C7BF RID: 51135 RVA: 0x003C0205 File Offset: 0x003BE405
		public List<string> ErrorCodes
		{
			get
			{
				return this._ErrorCodes;
			}
			set
			{
				this._ErrorCodes = value;
			}
		}

		// Token: 0x0600C7C0 RID: 51136 RVA: 0x003C0210 File Offset: 0x003BE410
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
			foreach (string text in this.ErrorCodes)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C7C1 RID: 51137 RVA: 0x003C02E0 File Offset: 0x003BE4E0
		public override bool Equals(object obj)
		{
			BlizzardCheckoutPurchaseCompletedFailure blizzardCheckoutPurchaseCompletedFailure = obj as BlizzardCheckoutPurchaseCompletedFailure;
			if (blizzardCheckoutPurchaseCompletedFailure == null)
			{
				return false;
			}
			if (this.HasPlayer != blizzardCheckoutPurchaseCompletedFailure.HasPlayer || (this.HasPlayer && !this.Player.Equals(blizzardCheckoutPurchaseCompletedFailure.Player)))
			{
				return false;
			}
			if (this.HasDeviceInfo != blizzardCheckoutPurchaseCompletedFailure.HasDeviceInfo || (this.HasDeviceInfo && !this.DeviceInfo.Equals(blizzardCheckoutPurchaseCompletedFailure.DeviceInfo)))
			{
				return false;
			}
			if (this.HasTransactionId != blizzardCheckoutPurchaseCompletedFailure.HasTransactionId || (this.HasTransactionId && !this.TransactionId.Equals(blizzardCheckoutPurchaseCompletedFailure.TransactionId)))
			{
				return false;
			}
			if (this.HasProductId != blizzardCheckoutPurchaseCompletedFailure.HasProductId || (this.HasProductId && !this.ProductId.Equals(blizzardCheckoutPurchaseCompletedFailure.ProductId)))
			{
				return false;
			}
			if (this.HasCurrency != blizzardCheckoutPurchaseCompletedFailure.HasCurrency || (this.HasCurrency && !this.Currency.Equals(blizzardCheckoutPurchaseCompletedFailure.Currency)))
			{
				return false;
			}
			if (this.ErrorCodes.Count != blizzardCheckoutPurchaseCompletedFailure.ErrorCodes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ErrorCodes.Count; i++)
			{
				if (!this.ErrorCodes[i].Equals(blizzardCheckoutPurchaseCompletedFailure.ErrorCodes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C7C2 RID: 51138 RVA: 0x003C0422 File Offset: 0x003BE622
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedFailure.Deserialize(stream, this);
		}

		// Token: 0x0600C7C3 RID: 51139 RVA: 0x003C042C File Offset: 0x003BE62C
		public static BlizzardCheckoutPurchaseCompletedFailure Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedFailure instance)
		{
			return BlizzardCheckoutPurchaseCompletedFailure.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C7C4 RID: 51140 RVA: 0x003C0438 File Offset: 0x003BE638
		public static BlizzardCheckoutPurchaseCompletedFailure DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedFailure blizzardCheckoutPurchaseCompletedFailure = new BlizzardCheckoutPurchaseCompletedFailure();
			BlizzardCheckoutPurchaseCompletedFailure.DeserializeLengthDelimited(stream, blizzardCheckoutPurchaseCompletedFailure);
			return blizzardCheckoutPurchaseCompletedFailure;
		}

		// Token: 0x0600C7C5 RID: 51141 RVA: 0x003C0454 File Offset: 0x003BE654
		public static BlizzardCheckoutPurchaseCompletedFailure DeserializeLengthDelimited(Stream stream, BlizzardCheckoutPurchaseCompletedFailure instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutPurchaseCompletedFailure.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C7C6 RID: 51142 RVA: 0x003C047C File Offset: 0x003BE67C
		public static BlizzardCheckoutPurchaseCompletedFailure Deserialize(Stream stream, BlizzardCheckoutPurchaseCompletedFailure instance, long limit)
		{
			if (instance.ErrorCodes == null)
			{
				instance.ErrorCodes = new List<string>();
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
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									instance.TransactionId = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
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
						if (num == 50)
						{
							instance.ErrorCodes.Add(ProtocolParser.ReadString(stream));
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

		// Token: 0x0600C7C7 RID: 51143 RVA: 0x003C05D7 File Offset: 0x003BE7D7
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCompletedFailure.Serialize(stream, this);
		}

		// Token: 0x0600C7C8 RID: 51144 RVA: 0x003C05E0 File Offset: 0x003BE7E0
		public static void Serialize(Stream stream, BlizzardCheckoutPurchaseCompletedFailure instance)
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
			if (instance.ErrorCodes.Count > 0)
			{
				foreach (string s in instance.ErrorCodes)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x0600C7C9 RID: 51145 RVA: 0x003C0720 File Offset: 0x003BE920
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
			if (this.ErrorCodes.Count > 0)
			{
				foreach (string s in this.ErrorCodes)
				{
					num += 1U;
					uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
				}
			}
			return num;
		}

		// Token: 0x04009EA2 RID: 40610
		public bool HasPlayer;

		// Token: 0x04009EA3 RID: 40611
		private Player _Player;

		// Token: 0x04009EA4 RID: 40612
		public bool HasDeviceInfo;

		// Token: 0x04009EA5 RID: 40613
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EA6 RID: 40614
		public bool HasTransactionId;

		// Token: 0x04009EA7 RID: 40615
		private string _TransactionId;

		// Token: 0x04009EA8 RID: 40616
		public bool HasProductId;

		// Token: 0x04009EA9 RID: 40617
		private string _ProductId;

		// Token: 0x04009EAA RID: 40618
		public bool HasCurrency;

		// Token: 0x04009EAB RID: 40619
		private string _Currency;

		// Token: 0x04009EAC RID: 40620
		private List<string> _ErrorCodes = new List<string>();
	}
}
