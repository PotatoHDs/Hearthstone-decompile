using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002B6 RID: 694
	public class KafkaHeader : IProtoBuf
	{
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0008F7FC File Offset: 0x0008D9FC
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0008F804 File Offset: 0x0008DA04
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

		// Token: 0x060028B1 RID: 10417 RVA: 0x0008F814 File Offset: 0x0008DA14
		public void SetServiceHash(uint val)
		{
			this.ServiceHash = val;
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0008F81D File Offset: 0x0008DA1D
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0008F825 File Offset: 0x0008DA25
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

		// Token: 0x060028B4 RID: 10420 RVA: 0x0008F835 File Offset: 0x0008DA35
		public void SetMethodId(uint val)
		{
			this.MethodId = val;
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0008F83E File Offset: 0x0008DA3E
		// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0008F846 File Offset: 0x0008DA46
		public uint Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = true;
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0008F856 File Offset: 0x0008DA56
		public void SetToken(uint val)
		{
			this.Token = val;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0008F85F File Offset: 0x0008DA5F
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x0008F867 File Offset: 0x0008DA67
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0008F877 File Offset: 0x0008DA77
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0008F880 File Offset: 0x0008DA80
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x0008F888 File Offset: 0x0008DA88
		public uint Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
				this.HasSize = true;
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0008F898 File Offset: 0x0008DA98
		public void SetSize(uint val)
		{
			this.Size = val;
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x0008F8A1 File Offset: 0x0008DAA1
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x0008F8A9 File Offset: 0x0008DAA9
		public uint Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
				this.HasStatus = true;
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0008F8B9 File Offset: 0x0008DAB9
		public void SetStatus(uint val)
		{
			this.Status = val;
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x0008F8C2 File Offset: 0x0008DAC2
		// (set) Token: 0x060028C2 RID: 10434 RVA: 0x0008F8CA File Offset: 0x0008DACA
		public ulong Timeout
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

		// Token: 0x060028C3 RID: 10435 RVA: 0x0008F8DA File Offset: 0x0008DADA
		public void SetTimeout(ulong val)
		{
			this.Timeout = val;
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0008F8E3 File Offset: 0x0008DAE3
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x0008F8EB File Offset: 0x0008DAEB
		public ProcessId ForwardTarget
		{
			get
			{
				return this._ForwardTarget;
			}
			set
			{
				this._ForwardTarget = value;
				this.HasForwardTarget = (value != null);
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0008F8FE File Offset: 0x0008DAFE
		public void SetForwardTarget(ProcessId val)
		{
			this.ForwardTarget = val;
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x0008F907 File Offset: 0x0008DB07
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x0008F90F File Offset: 0x0008DB0F
		public string ReturnTopic
		{
			get
			{
				return this._ReturnTopic;
			}
			set
			{
				this._ReturnTopic = value;
				this.HasReturnTopic = (value != null);
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0008F922 File Offset: 0x0008DB22
		public void SetReturnTopic(string val)
		{
			this.ReturnTopic = val;
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x0008F92B File Offset: 0x0008DB2B
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x0008F933 File Offset: 0x0008DB33
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0008F946 File Offset: 0x0008DB46
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0008F950 File Offset: 0x0008DB50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServiceHash)
			{
				num ^= this.ServiceHash.GetHashCode();
			}
			if (this.HasMethodId)
			{
				num ^= this.MethodId.GetHashCode();
			}
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasSize)
			{
				num ^= this.Size.GetHashCode();
			}
			if (this.HasStatus)
			{
				num ^= this.Status.GetHashCode();
			}
			if (this.HasTimeout)
			{
				num ^= this.Timeout.GetHashCode();
			}
			if (this.HasForwardTarget)
			{
				num ^= this.ForwardTarget.GetHashCode();
			}
			if (this.HasReturnTopic)
			{
				num ^= this.ReturnTopic.GetHashCode();
			}
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0008FA5C File Offset: 0x0008DC5C
		public override bool Equals(object obj)
		{
			KafkaHeader kafkaHeader = obj as KafkaHeader;
			return kafkaHeader != null && this.HasServiceHash == kafkaHeader.HasServiceHash && (!this.HasServiceHash || this.ServiceHash.Equals(kafkaHeader.ServiceHash)) && this.HasMethodId == kafkaHeader.HasMethodId && (!this.HasMethodId || this.MethodId.Equals(kafkaHeader.MethodId)) && this.HasToken == kafkaHeader.HasToken && (!this.HasToken || this.Token.Equals(kafkaHeader.Token)) && this.HasObjectId == kafkaHeader.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(kafkaHeader.ObjectId)) && this.HasSize == kafkaHeader.HasSize && (!this.HasSize || this.Size.Equals(kafkaHeader.Size)) && this.HasStatus == kafkaHeader.HasStatus && (!this.HasStatus || this.Status.Equals(kafkaHeader.Status)) && this.HasTimeout == kafkaHeader.HasTimeout && (!this.HasTimeout || this.Timeout.Equals(kafkaHeader.Timeout)) && this.HasForwardTarget == kafkaHeader.HasForwardTarget && (!this.HasForwardTarget || this.ForwardTarget.Equals(kafkaHeader.ForwardTarget)) && this.HasReturnTopic == kafkaHeader.HasReturnTopic && (!this.HasReturnTopic || this.ReturnTopic.Equals(kafkaHeader.ReturnTopic)) && this.HasClientId == kafkaHeader.HasClientId && (!this.HasClientId || this.ClientId.Equals(kafkaHeader.ClientId));
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0008FC39 File Offset: 0x0008DE39
		public static KafkaHeader ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<KafkaHeader>(bs, 0, -1);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0008FC43 File Offset: 0x0008DE43
		public void Deserialize(Stream stream)
		{
			KafkaHeader.Deserialize(stream, this);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0008FC4D File Offset: 0x0008DE4D
		public static KafkaHeader Deserialize(Stream stream, KafkaHeader instance)
		{
			return KafkaHeader.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0008FC58 File Offset: 0x0008DE58
		public static KafkaHeader DeserializeLengthDelimited(Stream stream)
		{
			KafkaHeader kafkaHeader = new KafkaHeader();
			KafkaHeader.DeserializeLengthDelimited(stream, kafkaHeader);
			return kafkaHeader;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0008FC74 File Offset: 0x0008DE74
		public static KafkaHeader DeserializeLengthDelimited(Stream stream, KafkaHeader instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return KafkaHeader.Deserialize(stream, instance, num);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0008FC9C File Offset: 0x0008DE9C
		public static KafkaHeader Deserialize(Stream stream, KafkaHeader instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ObjectId = 0UL;
			instance.Size = 0U;
			instance.Status = 0U;
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num == 13)
							{
								instance.ServiceHash = binaryReader.ReadUInt32();
								continue;
							}
							if (num == 16)
							{
								instance.MethodId = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.Token = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 32)
							{
								instance.ObjectId = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.Size = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
					}
					else if (num <= 56)
					{
						if (num == 48)
						{
							instance.Status = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 56)
						{
							instance.Timeout = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num != 66)
					{
						if (num == 74)
						{
							instance.ReturnTopic = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 90)
						{
							instance.ClientId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (instance.ForwardTarget == null)
						{
							instance.ForwardTarget = ProcessId.DeserializeLengthDelimited(stream);
							continue;
						}
						ProcessId.DeserializeLengthDelimited(stream, instance.ForwardTarget);
						continue;
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

		// Token: 0x060028D6 RID: 10454 RVA: 0x0008FE54 File Offset: 0x0008E054
		public void Serialize(Stream stream)
		{
			KafkaHeader.Serialize(stream, this);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0008FE60 File Offset: 0x0008E060
		public static void Serialize(Stream stream, KafkaHeader instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceHash)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.ServiceHash);
			}
			if (instance.HasMethodId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasSize)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Size);
			}
			if (instance.HasStatus)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Status);
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.Timeout);
			}
			if (instance.HasForwardTarget)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.ForwardTarget.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ForwardTarget);
			}
			if (instance.HasReturnTopic)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReturnTopic));
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0008FFB4 File Offset: 0x0008E1B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasServiceHash)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMethodId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MethodId);
			}
			if (this.HasToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Token);
			}
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.HasSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Size);
			}
			if (this.HasStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Status);
			}
			if (this.HasTimeout)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Timeout);
			}
			if (this.HasForwardTarget)
			{
				num += 1U;
				uint serializedSize = this.ForwardTarget.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReturnTopic)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ReturnTopic);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04001177 RID: 4471
		public bool HasServiceHash;

		// Token: 0x04001178 RID: 4472
		private uint _ServiceHash;

		// Token: 0x04001179 RID: 4473
		public bool HasMethodId;

		// Token: 0x0400117A RID: 4474
		private uint _MethodId;

		// Token: 0x0400117B RID: 4475
		public bool HasToken;

		// Token: 0x0400117C RID: 4476
		private uint _Token;

		// Token: 0x0400117D RID: 4477
		public bool HasObjectId;

		// Token: 0x0400117E RID: 4478
		private ulong _ObjectId;

		// Token: 0x0400117F RID: 4479
		public bool HasSize;

		// Token: 0x04001180 RID: 4480
		private uint _Size;

		// Token: 0x04001181 RID: 4481
		public bool HasStatus;

		// Token: 0x04001182 RID: 4482
		private uint _Status;

		// Token: 0x04001183 RID: 4483
		public bool HasTimeout;

		// Token: 0x04001184 RID: 4484
		private ulong _Timeout;

		// Token: 0x04001185 RID: 4485
		public bool HasForwardTarget;

		// Token: 0x04001186 RID: 4486
		private ProcessId _ForwardTarget;

		// Token: 0x04001187 RID: 4487
		public bool HasReturnTopic;

		// Token: 0x04001188 RID: 4488
		private string _ReturnTopic;

		// Token: 0x04001189 RID: 4489
		public bool HasClientId;

		// Token: 0x0400118A RID: 4490
		private string _ClientId;
	}
}
