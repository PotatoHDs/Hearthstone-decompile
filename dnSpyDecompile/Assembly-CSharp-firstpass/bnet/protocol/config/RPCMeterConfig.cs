using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	// Token: 0x0200034F RID: 847
	public class RPCMeterConfig : IProtoBuf
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000B02C2 File Offset: 0x000AE4C2
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000B02CA File Offset: 0x000AE4CA
		public List<RPCMethodConfig> Method
		{
			get
			{
				return this._Method;
			}
			set
			{
				this._Method = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000B02C2 File Offset: 0x000AE4C2
		public List<RPCMethodConfig> MethodList
		{
			get
			{
				return this._Method;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000B02D3 File Offset: 0x000AE4D3
		public int MethodCount
		{
			get
			{
				return this._Method.Count;
			}
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000B02E0 File Offset: 0x000AE4E0
		public void AddMethod(RPCMethodConfig val)
		{
			this._Method.Add(val);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000B02EE File Offset: 0x000AE4EE
		public void ClearMethod()
		{
			this._Method.Clear();
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000B02FB File Offset: 0x000AE4FB
		public void SetMethod(List<RPCMethodConfig> val)
		{
			this.Method = val;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000B0304 File Offset: 0x000AE504
		// (set) Token: 0x06003521 RID: 13601 RVA: 0x000B030C File Offset: 0x000AE50C
		public uint IncomePerSecond
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

		// Token: 0x06003522 RID: 13602 RVA: 0x000B031C File Offset: 0x000AE51C
		public void SetIncomePerSecond(uint val)
		{
			this.IncomePerSecond = val;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000B0325 File Offset: 0x000AE525
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x000B032D File Offset: 0x000AE52D
		public uint InitialBalance
		{
			get
			{
				return this._InitialBalance;
			}
			set
			{
				this._InitialBalance = value;
				this.HasInitialBalance = true;
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000B033D File Offset: 0x000AE53D
		public void SetInitialBalance(uint val)
		{
			this.InitialBalance = val;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000B0346 File Offset: 0x000AE546
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000B034E File Offset: 0x000AE54E
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

		// Token: 0x06003528 RID: 13608 RVA: 0x000B035E File Offset: 0x000AE55E
		public void SetCapBalance(uint val)
		{
			this.CapBalance = val;
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000B0367 File Offset: 0x000AE567
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000B036F File Offset: 0x000AE56F
		public float StartupPeriod
		{
			get
			{
				return this._StartupPeriod;
			}
			set
			{
				this._StartupPeriod = value;
				this.HasStartupPeriod = true;
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000B037F File Offset: 0x000AE57F
		public void SetStartupPeriod(float val)
		{
			this.StartupPeriod = val;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000B0388 File Offset: 0x000AE588
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RPCMethodConfig rpcmethodConfig in this.Method)
			{
				num ^= rpcmethodConfig.GetHashCode();
			}
			if (this.HasIncomePerSecond)
			{
				num ^= this.IncomePerSecond.GetHashCode();
			}
			if (this.HasInitialBalance)
			{
				num ^= this.InitialBalance.GetHashCode();
			}
			if (this.HasCapBalance)
			{
				num ^= this.CapBalance.GetHashCode();
			}
			if (this.HasStartupPeriod)
			{
				num ^= this.StartupPeriod.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000B0450 File Offset: 0x000AE650
		public override bool Equals(object obj)
		{
			RPCMeterConfig rpcmeterConfig = obj as RPCMeterConfig;
			if (rpcmeterConfig == null)
			{
				return false;
			}
			if (this.Method.Count != rpcmeterConfig.Method.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Method.Count; i++)
			{
				if (!this.Method[i].Equals(rpcmeterConfig.Method[i]))
				{
					return false;
				}
			}
			return this.HasIncomePerSecond == rpcmeterConfig.HasIncomePerSecond && (!this.HasIncomePerSecond || this.IncomePerSecond.Equals(rpcmeterConfig.IncomePerSecond)) && this.HasInitialBalance == rpcmeterConfig.HasInitialBalance && (!this.HasInitialBalance || this.InitialBalance.Equals(rpcmeterConfig.InitialBalance)) && this.HasCapBalance == rpcmeterConfig.HasCapBalance && (!this.HasCapBalance || this.CapBalance.Equals(rpcmeterConfig.CapBalance)) && this.HasStartupPeriod == rpcmeterConfig.HasStartupPeriod && (!this.HasStartupPeriod || this.StartupPeriod.Equals(rpcmeterConfig.StartupPeriod));
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000B0573 File Offset: 0x000AE773
		public static RPCMeterConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMeterConfig>(bs, 0, -1);
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000B057D File Offset: 0x000AE77D
		public void Deserialize(Stream stream)
		{
			RPCMeterConfig.Deserialize(stream, this);
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000B0587 File Offset: 0x000AE787
		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance)
		{
			return RPCMeterConfig.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000B0594 File Offset: 0x000AE794
		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMeterConfig rpcmeterConfig = new RPCMeterConfig();
			RPCMeterConfig.DeserializeLengthDelimited(stream, rpcmeterConfig);
			return rpcmeterConfig;
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000B05B0 File Offset: 0x000AE7B0
		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream, RPCMeterConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RPCMeterConfig.Deserialize(stream, instance, num);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000B05D8 File Offset: 0x000AE7D8
		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Method == null)
			{
				instance.Method = new List<RPCMethodConfig>();
			}
			instance.IncomePerSecond = 1U;
			instance.StartupPeriod = 0f;
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
					if (num <= 16)
					{
						if (num == 10)
						{
							instance.Method.Add(RPCMethodConfig.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 16)
						{
							instance.IncomePerSecond = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.InitialBalance = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.CapBalance = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 45)
						{
							instance.StartupPeriod = binaryReader.ReadSingle();
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

		// Token: 0x06003535 RID: 13621 RVA: 0x000B06F3 File Offset: 0x000AE8F3
		public void Serialize(Stream stream)
		{
			RPCMeterConfig.Serialize(stream, this);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000B06FC File Offset: 0x000AE8FC
		public static void Serialize(Stream stream, RPCMeterConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Method.Count > 0)
			{
				foreach (RPCMethodConfig rpcmethodConfig in instance.Method)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, rpcmethodConfig.GetSerializedSize());
					RPCMethodConfig.Serialize(stream, rpcmethodConfig);
				}
			}
			if (instance.HasIncomePerSecond)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.IncomePerSecond);
			}
			if (instance.HasInitialBalance)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.InitialBalance);
			}
			if (instance.HasCapBalance)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.CapBalance);
			}
			if (instance.HasStartupPeriod)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.StartupPeriod);
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000B07E8 File Offset: 0x000AE9E8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Method.Count > 0)
			{
				foreach (RPCMethodConfig rpcmethodConfig in this.Method)
				{
					num += 1U;
					uint serializedSize = rpcmethodConfig.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasIncomePerSecond)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.IncomePerSecond);
			}
			if (this.HasInitialBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.InitialBalance);
			}
			if (this.HasCapBalance)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CapBalance);
			}
			if (this.HasStartupPeriod)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001460 RID: 5216
		private List<RPCMethodConfig> _Method = new List<RPCMethodConfig>();

		// Token: 0x04001461 RID: 5217
		public bool HasIncomePerSecond;

		// Token: 0x04001462 RID: 5218
		private uint _IncomePerSecond;

		// Token: 0x04001463 RID: 5219
		public bool HasInitialBalance;

		// Token: 0x04001464 RID: 5220
		private uint _InitialBalance;

		// Token: 0x04001465 RID: 5221
		public bool HasCapBalance;

		// Token: 0x04001466 RID: 5222
		private uint _CapBalance;

		// Token: 0x04001467 RID: 5223
		public bool HasStartupPeriod;

		// Token: 0x04001468 RID: 5224
		private float _StartupPeriod;
	}
}
