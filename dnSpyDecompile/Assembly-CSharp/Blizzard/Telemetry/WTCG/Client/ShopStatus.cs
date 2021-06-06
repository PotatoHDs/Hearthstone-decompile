using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F5 RID: 4597
	public class ShopStatus : IProtoBuf
	{
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x0600CDB1 RID: 52657 RVA: 0x003D57B7 File Offset: 0x003D39B7
		// (set) Token: 0x0600CDB2 RID: 52658 RVA: 0x003D57BF File Offset: 0x003D39BF
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

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x0600CDB3 RID: 52659 RVA: 0x003D57D2 File Offset: 0x003D39D2
		// (set) Token: 0x0600CDB4 RID: 52660 RVA: 0x003D57DA File Offset: 0x003D39DA
		public string Error
		{
			get
			{
				return this._Error;
			}
			set
			{
				this._Error = value;
				this.HasError = (value != null);
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x0600CDB5 RID: 52661 RVA: 0x003D57ED File Offset: 0x003D39ED
		// (set) Token: 0x0600CDB6 RID: 52662 RVA: 0x003D57F5 File Offset: 0x003D39F5
		public double TimeInHubSec
		{
			get
			{
				return this._TimeInHubSec;
			}
			set
			{
				this._TimeInHubSec = value;
				this.HasTimeInHubSec = true;
			}
		}

		// Token: 0x0600CDB7 RID: 52663 RVA: 0x003D5808 File Offset: 0x003D3A08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasError)
			{
				num ^= this.Error.GetHashCode();
			}
			if (this.HasTimeInHubSec)
			{
				num ^= this.TimeInHubSec.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CDB8 RID: 52664 RVA: 0x003D5868 File Offset: 0x003D3A68
		public override bool Equals(object obj)
		{
			ShopStatus shopStatus = obj as ShopStatus;
			return shopStatus != null && this.HasPlayer == shopStatus.HasPlayer && (!this.HasPlayer || this.Player.Equals(shopStatus.Player)) && this.HasError == shopStatus.HasError && (!this.HasError || this.Error.Equals(shopStatus.Error)) && this.HasTimeInHubSec == shopStatus.HasTimeInHubSec && (!this.HasTimeInHubSec || this.TimeInHubSec.Equals(shopStatus.TimeInHubSec));
		}

		// Token: 0x0600CDB9 RID: 52665 RVA: 0x003D5906 File Offset: 0x003D3B06
		public void Deserialize(Stream stream)
		{
			ShopStatus.Deserialize(stream, this);
		}

		// Token: 0x0600CDBA RID: 52666 RVA: 0x003D5910 File Offset: 0x003D3B10
		public static ShopStatus Deserialize(Stream stream, ShopStatus instance)
		{
			return ShopStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDBB RID: 52667 RVA: 0x003D591C File Offset: 0x003D3B1C
		public static ShopStatus DeserializeLengthDelimited(Stream stream)
		{
			ShopStatus shopStatus = new ShopStatus();
			ShopStatus.DeserializeLengthDelimited(stream, shopStatus);
			return shopStatus;
		}

		// Token: 0x0600CDBC RID: 52668 RVA: 0x003D5938 File Offset: 0x003D3B38
		public static ShopStatus DeserializeLengthDelimited(Stream stream, ShopStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDBD RID: 52669 RVA: 0x003D5960 File Offset: 0x003D3B60
		public static ShopStatus Deserialize(Stream stream, ShopStatus instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						if (num != 25)
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
							instance.TimeInHubSec = binaryReader.ReadDouble();
						}
					}
					else
					{
						instance.Error = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CDBE RID: 52670 RVA: 0x003D5A35 File Offset: 0x003D3C35
		public void Serialize(Stream stream)
		{
			ShopStatus.Serialize(stream, this);
		}

		// Token: 0x0600CDBF RID: 52671 RVA: 0x003D5A40 File Offset: 0x003D3C40
		public static void Serialize(Stream stream, ShopStatus instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasError)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Error));
			}
			if (instance.HasTimeInHubSec)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.TimeInHubSec);
			}
		}

		// Token: 0x0600CDC0 RID: 52672 RVA: 0x003D5AC4 File Offset: 0x003D3CC4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasError)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Error);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTimeInHubSec)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x0400A122 RID: 41250
		public bool HasPlayer;

		// Token: 0x0400A123 RID: 41251
		private Player _Player;

		// Token: 0x0400A124 RID: 41252
		public bool HasError;

		// Token: 0x0400A125 RID: 41253
		private string _Error;

		// Token: 0x0400A126 RID: 41254
		public bool HasTimeInHubSec;

		// Token: 0x0400A127 RID: 41255
		private double _TimeInHubSec;
	}
}
