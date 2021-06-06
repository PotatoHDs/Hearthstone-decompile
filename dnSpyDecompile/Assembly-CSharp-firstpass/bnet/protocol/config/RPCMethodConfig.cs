using System;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	// Token: 0x0200034E RID: 846
	public class RPCMethodConfig : IProtoBuf
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000AF64F File Offset: 0x000AD84F
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000AF657 File Offset: 0x000AD857
		public string ServiceName
		{
			get
			{
				return this._ServiceName;
			}
			set
			{
				this._ServiceName = value;
				this.HasServiceName = (value != null);
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000AF66A File Offset: 0x000AD86A
		public void SetServiceName(string val)
		{
			this.ServiceName = val;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000AF673 File Offset: 0x000AD873
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000AF67B File Offset: 0x000AD87B
		public string MethodName
		{
			get
			{
				return this._MethodName;
			}
			set
			{
				this._MethodName = value;
				this.HasMethodName = (value != null);
			}
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000AF68E File Offset: 0x000AD88E
		public void SetMethodName(string val)
		{
			this.MethodName = val;
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000AF697 File Offset: 0x000AD897
		// (set) Token: 0x060034E6 RID: 13542 RVA: 0x000AF69F File Offset: 0x000AD89F
		public uint FixedCallCost
		{
			get
			{
				return this._FixedCallCost;
			}
			set
			{
				this._FixedCallCost = value;
				this.HasFixedCallCost = true;
			}
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000AF6AF File Offset: 0x000AD8AF
		public void SetFixedCallCost(uint val)
		{
			this.FixedCallCost = val;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000AF6B8 File Offset: 0x000AD8B8
		// (set) Token: 0x060034E9 RID: 13545 RVA: 0x000AF6C0 File Offset: 0x000AD8C0
		public uint FixedPacketSize
		{
			get
			{
				return this._FixedPacketSize;
			}
			set
			{
				this._FixedPacketSize = value;
				this.HasFixedPacketSize = true;
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000AF6D0 File Offset: 0x000AD8D0
		public void SetFixedPacketSize(uint val)
		{
			this.FixedPacketSize = val;
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000AF6D9 File Offset: 0x000AD8D9
		// (set) Token: 0x060034EC RID: 13548 RVA: 0x000AF6E1 File Offset: 0x000AD8E1
		public float VariableMultiplier
		{
			get
			{
				return this._VariableMultiplier;
			}
			set
			{
				this._VariableMultiplier = value;
				this.HasVariableMultiplier = true;
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000AF6F1 File Offset: 0x000AD8F1
		public void SetVariableMultiplier(float val)
		{
			this.VariableMultiplier = val;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000AF6FA File Offset: 0x000AD8FA
		// (set) Token: 0x060034EF RID: 13551 RVA: 0x000AF702 File Offset: 0x000AD902
		public float Multiplier
		{
			get
			{
				return this._Multiplier;
			}
			set
			{
				this._Multiplier = value;
				this.HasMultiplier = true;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000AF712 File Offset: 0x000AD912
		public void SetMultiplier(float val)
		{
			this.Multiplier = val;
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000AF71B File Offset: 0x000AD91B
		// (set) Token: 0x060034F2 RID: 13554 RVA: 0x000AF723 File Offset: 0x000AD923
		public uint RateLimitCount
		{
			get
			{
				return this._RateLimitCount;
			}
			set
			{
				this._RateLimitCount = value;
				this.HasRateLimitCount = true;
			}
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000AF733 File Offset: 0x000AD933
		public void SetRateLimitCount(uint val)
		{
			this.RateLimitCount = val;
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000AF73C File Offset: 0x000AD93C
		// (set) Token: 0x060034F5 RID: 13557 RVA: 0x000AF744 File Offset: 0x000AD944
		public uint RateLimitSeconds
		{
			get
			{
				return this._RateLimitSeconds;
			}
			set
			{
				this._RateLimitSeconds = value;
				this.HasRateLimitSeconds = true;
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000AF754 File Offset: 0x000AD954
		public void SetRateLimitSeconds(uint val)
		{
			this.RateLimitSeconds = val;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000AF75D File Offset: 0x000AD95D
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000AF765 File Offset: 0x000AD965
		public uint MaxPacketSize
		{
			get
			{
				return this._MaxPacketSize;
			}
			set
			{
				this._MaxPacketSize = value;
				this.HasMaxPacketSize = true;
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000AF775 File Offset: 0x000AD975
		public void SetMaxPacketSize(uint val)
		{
			this.MaxPacketSize = val;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000AF77E File Offset: 0x000AD97E
		// (set) Token: 0x060034FB RID: 13563 RVA: 0x000AF786 File Offset: 0x000AD986
		public uint MaxEncodedSize
		{
			get
			{
				return this._MaxEncodedSize;
			}
			set
			{
				this._MaxEncodedSize = value;
				this.HasMaxEncodedSize = true;
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000AF796 File Offset: 0x000AD996
		public void SetMaxEncodedSize(uint val)
		{
			this.MaxEncodedSize = val;
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000AF79F File Offset: 0x000AD99F
		// (set) Token: 0x060034FE RID: 13566 RVA: 0x000AF7A7 File Offset: 0x000AD9A7
		public float Timeout
		{
			get
			{
				return this._Timeout;
			}
			set
			{
				this._Timeout = value;
				this.HasTimeout = true;
			}
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000AF7B7 File Offset: 0x000AD9B7
		public void SetTimeout(float val)
		{
			this.Timeout = val;
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000AF7C0 File Offset: 0x000AD9C0
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x000AF7C8 File Offset: 0x000AD9C8
		public uint CapBalance
		{
			get
			{
				return this._CapBalance;
			}
			set
			{
				this._CapBalance = value;
				this.HasCapBalance = true;
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000AF7D8 File Offset: 0x000AD9D8
		public void SetCapBalance(uint val)
		{
			this.CapBalance = val;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000AF7E1 File Offset: 0x000AD9E1
		// (set) Token: 0x06003504 RID: 13572 RVA: 0x000AF7E9 File Offset: 0x000AD9E9
		public float IncomePerSecond
		{
			get
			{
				return this._IncomePerSecond;
			}
			set
			{
				this._IncomePerSecond = value;
				this.HasIncomePerSecond = true;
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000AF7F9 File Offset: 0x000AD9F9
		public void SetIncomePerSecond(float val)
		{
			this.IncomePerSecond = val;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000AF802 File Offset: 0x000ADA02
		// (set) Token: 0x06003507 RID: 13575 RVA: 0x000AF80A File Offset: 0x000ADA0A
		public uint ServiceHash
		{
			get
			{
				return this._ServiceHash;
			}
			set
			{
				this._ServiceHash = value;
				this.HasServiceHash = true;
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000AF81A File Offset: 0x000ADA1A
		public void SetServiceHash(uint val)
		{
			this.ServiceHash = val;
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000AF823 File Offset: 0x000ADA23
		// (set) Token: 0x0600350A RID: 13578 RVA: 0x000AF82B File Offset: 0x000ADA2B
		public uint MethodId
		{
			get
			{
				return this._MethodId;
			}
			set
			{
				this._MethodId = value;
				this.HasMethodId = true;
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000AF83B File Offset: 0x000ADA3B
		public void SetMethodId(uint val)
		{
			this.MethodId = val;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000AF844 File Offset: 0x000ADA44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServiceName)
			{
				num ^= this.ServiceName.GetHashCode();
			}
			if (this.HasMethodName)
			{
				num ^= this.MethodName.GetHashCode();
			}
			if (this.HasFixedCallCost)
			{
				num ^= this.FixedCallCost.GetHashCode();
			}
			if (this.HasFixedPacketSize)
			{
				num ^= this.FixedPacketSize.GetHashCode();
			}
			if (this.HasVariableMultiplier)
			{
				num ^= this.VariableMultiplier.GetHashCode();
			}
			if (this.HasMultiplier)
			{
				num ^= this.Multiplier.GetHashCode();
			}
			if (this.HasRateLimitCount)
			{
				num ^= this.RateLimitCount.GetHashCode();
			}
			if (this.HasRateLimitSeconds)
			{
				num ^= this.RateLimitSeconds.GetHashCode();
			}
			if (this.HasMaxPacketSize)
			{
				num ^= this.MaxPacketSize.GetHashCode();
			}
			if (this.HasMaxEncodedSize)
			{
				num ^= this.MaxEncodedSize.GetHashCode();
			}
			if (this.HasTimeout)
			{
				num ^= this.Timeout.GetHashCode();
			}
			if (this.HasCapBalance)
			{
				num ^= this.CapBalance.GetHashCode();
			}
			if (this.HasIncomePerSecond)
			{
				num ^= this.IncomePerSecond.GetHashCode();
			}
			if (this.HasServiceHash)
			{
				num ^= this.ServiceHash.GetHashCode();
			}
			if (this.HasMethodId)
			{
				num ^= this.MethodId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000AF9D0 File Offset: 0x000ADBD0
		public override bool Equals(object obj)
		{
			RPCMethodConfig rpcmethodConfig = obj as RPCMethodConfig;
			return rpcmethodConfig != null && this.HasServiceName == rpcmethodConfig.HasServiceName && (!this.HasServiceName || this.ServiceName.Equals(rpcmethodConfig.ServiceName)) && this.HasMethodName == rpcmethodConfig.HasMethodName && (!this.HasMethodName || this.MethodName.Equals(rpcmethodConfig.MethodName)) && this.HasFixedCallCost == rpcmethodConfig.HasFixedCallCost && (!this.HasFixedCallCost || this.FixedCallCost.Equals(rpcmethodConfig.FixedCallCost)) && this.HasFixedPacketSize == rpcmethodConfig.HasFixedPacketSize && (!this.HasFixedPacketSize || this.FixedPacketSize.Equals(rpcmethodConfig.FixedPacketSize)) && this.HasVariableMultiplier == rpcmethodConfig.HasVariableMultiplier && (!this.HasVariableMultiplier || this.VariableMultiplier.Equals(rpcmethodConfig.VariableMultiplier)) && this.HasMultiplier == rpcmethodConfig.HasMultiplier && (!this.HasMultiplier || this.Multiplier.Equals(rpcmethodConfig.Multiplier)) && this.HasRateLimitCount == rpcmethodConfig.HasRateLimitCount && (!this.HasRateLimitCount || this.RateLimitCount.Equals(rpcmethodConfig.RateLimitCount)) && this.HasRateLimitSeconds == rpcmethodConfig.HasRateLimitSeconds && (!this.HasRateLimitSeconds || this.RateLimitSeconds.Equals(rpcmethodConfig.RateLimitSeconds)) && this.HasMaxPacketSize == rpcmethodConfig.HasMaxPacketSize && (!this.HasMaxPacketSize || this.MaxPacketSize.Equals(rpcmethodConfig.MaxPacketSize)) && this.HasMaxEncodedSize == rpcmethodConfig.HasMaxEncodedSize && (!this.HasMaxEncodedSize || this.MaxEncodedSize.Equals(rpcmethodConfig.MaxEncodedSize)) && this.HasTimeout == rpcmethodConfig.HasTimeout && (!this.HasTimeout || this.Timeout.Equals(rpcmethodConfig.Timeout)) && this.HasCapBalance == rpcmethodConfig.HasCapBalance && (!this.HasCapBalance || this.CapBalance.Equals(rpcmethodConfig.CapBalance)) && this.HasIncomePerSecond == rpcmethodConfig.HasIncomePerSecond && (!this.HasIncomePerSecond || this.IncomePerSecond.Equals(rpcmethodConfig.IncomePerSecond)) && this.HasServiceHash == rpcmethodConfig.HasServiceHash && (!this.HasServiceHash || this.ServiceHash.Equals(rpcmethodConfig.ServiceHash)) && this.HasMethodId == rpcmethodConfig.HasMethodId && (!this.HasMethodId || this.MethodId.Equals(rpcmethodConfig.MethodId));
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000AFC96 File Offset: 0x000ADE96
		public static RPCMethodConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMethodConfig>(bs, 0, -1);
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000AFCA0 File Offset: 0x000ADEA0
		public void Deserialize(Stream stream)
		{
			RPCMethodConfig.Deserialize(stream, this);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000AFCAA File Offset: 0x000ADEAA
		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance)
		{
			return RPCMethodConfig.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000AFCB8 File Offset: 0x000ADEB8
		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMethodConfig rpcmethodConfig = new RPCMethodConfig();
			RPCMethodConfig.DeserializeLengthDelimited(stream, rpcmethodConfig);
			return rpcmethodConfig;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000AFCD4 File Offset: 0x000ADED4
		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream, RPCMethodConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RPCMethodConfig.Deserialize(stream, instance, num);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000AFCFC File Offset: 0x000ADEFC
		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FixedCallCost = 1U;
			instance.FixedPacketSize = 0U;
			instance.VariableMultiplier = 0f;
			instance.Multiplier = 1f;
			instance.IncomePerSecond = 0f;
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 10)
							{
								instance.ServiceName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 18)
							{
								instance.MethodName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 24)
							{
								instance.FixedCallCost = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else if (num <= 45)
						{
							if (num == 32)
							{
								instance.FixedPacketSize = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 45)
							{
								instance.VariableMultiplier = binaryReader.ReadSingle();
								continue;
							}
						}
						else
						{
							if (num == 53)
							{
								instance.Multiplier = binaryReader.ReadSingle();
								continue;
							}
							if (num == 56)
							{
								instance.RateLimitCount = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
					}
					else if (num <= 93)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.RateLimitSeconds = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 72)
							{
								instance.MaxPacketSize = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.MaxEncodedSize = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 93)
							{
								instance.Timeout = binaryReader.ReadSingle();
								continue;
							}
						}
					}
					else if (num <= 109)
					{
						if (num == 96)
						{
							instance.CapBalance = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 109)
						{
							instance.IncomePerSecond = binaryReader.ReadSingle();
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.ServiceHash = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 120)
						{
							instance.MethodId = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06003515 RID: 13589 RVA: 0x000AFF60 File Offset: 0x000AE160
		public void Serialize(Stream stream)
		{
			RPCMethodConfig.Serialize(stream, this);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000AFF6C File Offset: 0x000AE16C
		public static void Serialize(Stream stream, RPCMethodConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServiceName));
			}
			if (instance.HasMethodName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MethodName));
			}
			if (instance.HasFixedCallCost)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.FixedCallCost);
			}
			if (instance.HasFixedPacketSize)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.FixedPacketSize);
			}
			if (instance.HasVariableMultiplier)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.VariableMultiplier);
			}
			if (instance.HasMultiplier)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Multiplier);
			}
			if (instance.HasRateLimitCount)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitCount);
			}
			if (instance.HasRateLimitSeconds)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitSeconds);
			}
			if (instance.HasMaxPacketSize)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.MaxPacketSize);
			}
			if (instance.HasMaxEncodedSize)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.MaxEncodedSize);
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Timeout);
			}
			if (instance.HasCapBalance)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt32(stream, instance.CapBalance);
			}
			if (instance.HasIncomePerSecond)
			{
				stream.WriteByte(109);
				binaryWriter.Write(instance.IncomePerSecond);
			}
			if (instance.HasServiceHash)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt32(stream, instance.ServiceHash);
			}
			if (instance.HasMethodId)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000B0138 File Offset: 0x000AE338
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasServiceName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ServiceName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMethodName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.MethodName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFixedCallCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.FixedCallCost);
			}
			if (this.HasFixedPacketSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.FixedPacketSize);
			}
			if (this.HasVariableMultiplier)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMultiplier)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasRateLimitCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RateLimitCount);
			}
			if (this.HasRateLimitSeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RateLimitSeconds);
			}
			if (this.HasMaxPacketSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxPacketSize);
			}
			if (this.HasMaxEncodedSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxEncodedSize);
			}
			if (this.HasTimeout)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCapBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CapBalance);
			}
			if (this.HasIncomePerSecond)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasServiceHash)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ServiceHash);
			}
			if (this.HasMethodId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MethodId);
			}
			return num;
		}

		// Token: 0x04001442 RID: 5186
		public bool HasServiceName;

		// Token: 0x04001443 RID: 5187
		private string _ServiceName;

		// Token: 0x04001444 RID: 5188
		public bool HasMethodName;

		// Token: 0x04001445 RID: 5189
		private string _MethodName;

		// Token: 0x04001446 RID: 5190
		public bool HasFixedCallCost;

		// Token: 0x04001447 RID: 5191
		private uint _FixedCallCost;

		// Token: 0x04001448 RID: 5192
		public bool HasFixedPacketSize;

		// Token: 0x04001449 RID: 5193
		private uint _FixedPacketSize;

		// Token: 0x0400144A RID: 5194
		public bool HasVariableMultiplier;

		// Token: 0x0400144B RID: 5195
		private float _VariableMultiplier;

		// Token: 0x0400144C RID: 5196
		public bool HasMultiplier;

		// Token: 0x0400144D RID: 5197
		private float _Multiplier;

		// Token: 0x0400144E RID: 5198
		public bool HasRateLimitCount;

		// Token: 0x0400144F RID: 5199
		private uint _RateLimitCount;

		// Token: 0x04001450 RID: 5200
		public bool HasRateLimitSeconds;

		// Token: 0x04001451 RID: 5201
		private uint _RateLimitSeconds;

		// Token: 0x04001452 RID: 5202
		public bool HasMaxPacketSize;

		// Token: 0x04001453 RID: 5203
		private uint _MaxPacketSize;

		// Token: 0x04001454 RID: 5204
		public bool HasMaxEncodedSize;

		// Token: 0x04001455 RID: 5205
		private uint _MaxEncodedSize;

		// Token: 0x04001456 RID: 5206
		public bool HasTimeout;

		// Token: 0x04001457 RID: 5207
		private float _Timeout;

		// Token: 0x04001458 RID: 5208
		public bool HasCapBalance;

		// Token: 0x04001459 RID: 5209
		private uint _CapBalance;

		// Token: 0x0400145A RID: 5210
		public bool HasIncomePerSecond;

		// Token: 0x0400145B RID: 5211
		private float _IncomePerSecond;

		// Token: 0x0400145C RID: 5212
		public bool HasServiceHash;

		// Token: 0x0400145D RID: 5213
		private uint _ServiceHash;

		// Token: 0x0400145E RID: 5214
		public bool HasMethodId;

		// Token: 0x0400145F RID: 5215
		private uint _MethodId;
	}
}
