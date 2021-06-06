using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002B5 RID: 693
	public class Header : IProtoBuf
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x0008E809 File Offset: 0x0008CA09
		// (set) Token: 0x06002869 RID: 10345 RVA: 0x0008E811 File Offset: 0x0008CA11
		public uint ServiceId { get; set; }

		// Token: 0x0600286A RID: 10346 RVA: 0x0008E81A File Offset: 0x0008CA1A
		public void SetServiceId(uint val)
		{
			this.ServiceId = val;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x0008E823 File Offset: 0x0008CA23
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x0008E82B File Offset: 0x0008CA2B
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

		// Token: 0x0600286D RID: 10349 RVA: 0x0008E83B File Offset: 0x0008CA3B
		public void SetMethodId(uint val)
		{
			this.MethodId = val;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x0008E844 File Offset: 0x0008CA44
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x0008E84C File Offset: 0x0008CA4C
		public uint Token { get; set; }

		// Token: 0x06002870 RID: 10352 RVA: 0x0008E855 File Offset: 0x0008CA55
		public void SetToken(uint val)
		{
			this.Token = val;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x0008E85E File Offset: 0x0008CA5E
		// (set) Token: 0x06002872 RID: 10354 RVA: 0x0008E866 File Offset: 0x0008CA66
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

		// Token: 0x06002873 RID: 10355 RVA: 0x0008E876 File Offset: 0x0008CA76
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0008E87F File Offset: 0x0008CA7F
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x0008E887 File Offset: 0x0008CA87
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

		// Token: 0x06002876 RID: 10358 RVA: 0x0008E897 File Offset: 0x0008CA97
		public void SetSize(uint val)
		{
			this.Size = val;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x0008E8A0 File Offset: 0x0008CAA0
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x0008E8A8 File Offset: 0x0008CAA8
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

		// Token: 0x06002879 RID: 10361 RVA: 0x0008E8B8 File Offset: 0x0008CAB8
		public void SetStatus(uint val)
		{
			this.Status = val;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0008E8C1 File Offset: 0x0008CAC1
		// (set) Token: 0x0600287B RID: 10363 RVA: 0x0008E8C9 File Offset: 0x0008CAC9
		public List<ErrorInfo> Error
		{
			get
			{
				return this._Error;
			}
			set
			{
				this._Error = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0008E8C1 File Offset: 0x0008CAC1
		public List<ErrorInfo> ErrorList
		{
			get
			{
				return this._Error;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0008E8D2 File Offset: 0x0008CAD2
		public int ErrorCount
		{
			get
			{
				return this._Error.Count;
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0008E8DF File Offset: 0x0008CADF
		public void AddError(ErrorInfo val)
		{
			this._Error.Add(val);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0008E8ED File Offset: 0x0008CAED
		public void ClearError()
		{
			this._Error.Clear();
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0008E8FA File Offset: 0x0008CAFA
		public void SetError(List<ErrorInfo> val)
		{
			this.Error = val;
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0008E903 File Offset: 0x0008CB03
		// (set) Token: 0x06002882 RID: 10370 RVA: 0x0008E90B File Offset: 0x0008CB0B
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

		// Token: 0x06002883 RID: 10371 RVA: 0x0008E91B File Offset: 0x0008CB1B
		public void SetTimeout(ulong val)
		{
			this.Timeout = val;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0008E924 File Offset: 0x0008CB24
		// (set) Token: 0x06002885 RID: 10373 RVA: 0x0008E92C File Offset: 0x0008CB2C
		public bool IsResponse
		{
			get
			{
				return this._IsResponse;
			}
			set
			{
				this._IsResponse = value;
				this.HasIsResponse = true;
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0008E93C File Offset: 0x0008CB3C
		public void SetIsResponse(bool val)
		{
			this.IsResponse = val;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x0008E945 File Offset: 0x0008CB45
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x0008E94D File Offset: 0x0008CB4D
		public List<ProcessId> ForwardTargets
		{
			get
			{
				return this._ForwardTargets;
			}
			set
			{
				this._ForwardTargets = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x0008E945 File Offset: 0x0008CB45
		public List<ProcessId> ForwardTargetsList
		{
			get
			{
				return this._ForwardTargets;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x0008E956 File Offset: 0x0008CB56
		public int ForwardTargetsCount
		{
			get
			{
				return this._ForwardTargets.Count;
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0008E963 File Offset: 0x0008CB63
		public void AddForwardTargets(ProcessId val)
		{
			this._ForwardTargets.Add(val);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0008E971 File Offset: 0x0008CB71
		public void ClearForwardTargets()
		{
			this._ForwardTargets.Clear();
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0008E97E File Offset: 0x0008CB7E
		public void SetForwardTargets(List<ProcessId> val)
		{
			this.ForwardTargets = val;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x0008E987 File Offset: 0x0008CB87
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x0008E98F File Offset: 0x0008CB8F
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

		// Token: 0x06002890 RID: 10384 RVA: 0x0008E99F File Offset: 0x0008CB9F
		public void SetServiceHash(uint val)
		{
			this.ServiceHash = val;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0008E9A8 File Offset: 0x0008CBA8
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
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

		// Token: 0x06002893 RID: 10387 RVA: 0x0008E9C3 File Offset: 0x0008CBC3
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x0008E9CC File Offset: 0x0008CBCC
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x0008E9D4 File Offset: 0x0008CBD4
		public List<FanoutTarget> FanoutTarget
		{
			get
			{
				return this._FanoutTarget;
			}
			set
			{
				this._FanoutTarget = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0008E9CC File Offset: 0x0008CBCC
		public List<FanoutTarget> FanoutTargetList
		{
			get
			{
				return this._FanoutTarget;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0008E9DD File Offset: 0x0008CBDD
		public int FanoutTargetCount
		{
			get
			{
				return this._FanoutTarget.Count;
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0008E9EA File Offset: 0x0008CBEA
		public void AddFanoutTarget(FanoutTarget val)
		{
			this._FanoutTarget.Add(val);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0008E9F8 File Offset: 0x0008CBF8
		public void ClearFanoutTarget()
		{
			this._FanoutTarget.Clear();
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0008EA05 File Offset: 0x0008CC05
		public void SetFanoutTarget(List<FanoutTarget> val)
		{
			this.FanoutTarget = val;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x0008EA0E File Offset: 0x0008CC0E
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x0008EA16 File Offset: 0x0008CC16
		public List<string> ClientIdFanoutTarget
		{
			get
			{
				return this._ClientIdFanoutTarget;
			}
			set
			{
				this._ClientIdFanoutTarget = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x0008EA0E File Offset: 0x0008CC0E
		public List<string> ClientIdFanoutTargetList
		{
			get
			{
				return this._ClientIdFanoutTarget;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x0008EA1F File Offset: 0x0008CC1F
		public int ClientIdFanoutTargetCount
		{
			get
			{
				return this._ClientIdFanoutTarget.Count;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0008EA2C File Offset: 0x0008CC2C
		public void AddClientIdFanoutTarget(string val)
		{
			this._ClientIdFanoutTarget.Add(val);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0008EA3A File Offset: 0x0008CC3A
		public void ClearClientIdFanoutTarget()
		{
			this._ClientIdFanoutTarget.Clear();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0008EA47 File Offset: 0x0008CC47
		public void SetClientIdFanoutTarget(List<string> val)
		{
			this.ClientIdFanoutTarget = val;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0008EA50 File Offset: 0x0008CC50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ServiceId.GetHashCode();
			if (this.HasMethodId)
			{
				num ^= this.MethodId.GetHashCode();
			}
			num ^= this.Token.GetHashCode();
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
			foreach (ErrorInfo errorInfo in this.Error)
			{
				num ^= errorInfo.GetHashCode();
			}
			if (this.HasTimeout)
			{
				num ^= this.Timeout.GetHashCode();
			}
			if (this.HasIsResponse)
			{
				num ^= this.IsResponse.GetHashCode();
			}
			foreach (ProcessId processId in this.ForwardTargets)
			{
				num ^= processId.GetHashCode();
			}
			if (this.HasServiceHash)
			{
				num ^= this.ServiceHash.GetHashCode();
			}
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			foreach (FanoutTarget fanoutTarget in this.FanoutTarget)
			{
				num ^= fanoutTarget.GetHashCode();
			}
			foreach (string text in this.ClientIdFanoutTarget)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0008EC74 File Offset: 0x0008CE74
		public override bool Equals(object obj)
		{
			Header header = obj as Header;
			if (header == null)
			{
				return false;
			}
			if (!this.ServiceId.Equals(header.ServiceId))
			{
				return false;
			}
			if (this.HasMethodId != header.HasMethodId || (this.HasMethodId && !this.MethodId.Equals(header.MethodId)))
			{
				return false;
			}
			if (!this.Token.Equals(header.Token))
			{
				return false;
			}
			if (this.HasObjectId != header.HasObjectId || (this.HasObjectId && !this.ObjectId.Equals(header.ObjectId)))
			{
				return false;
			}
			if (this.HasSize != header.HasSize || (this.HasSize && !this.Size.Equals(header.Size)))
			{
				return false;
			}
			if (this.HasStatus != header.HasStatus || (this.HasStatus && !this.Status.Equals(header.Status)))
			{
				return false;
			}
			if (this.Error.Count != header.Error.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Error.Count; i++)
			{
				if (!this.Error[i].Equals(header.Error[i]))
				{
					return false;
				}
			}
			if (this.HasTimeout != header.HasTimeout || (this.HasTimeout && !this.Timeout.Equals(header.Timeout)))
			{
				return false;
			}
			if (this.HasIsResponse != header.HasIsResponse || (this.HasIsResponse && !this.IsResponse.Equals(header.IsResponse)))
			{
				return false;
			}
			if (this.ForwardTargets.Count != header.ForwardTargets.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ForwardTargets.Count; j++)
			{
				if (!this.ForwardTargets[j].Equals(header.ForwardTargets[j]))
				{
					return false;
				}
			}
			if (this.HasServiceHash != header.HasServiceHash || (this.HasServiceHash && !this.ServiceHash.Equals(header.ServiceHash)))
			{
				return false;
			}
			if (this.HasClientId != header.HasClientId || (this.HasClientId && !this.ClientId.Equals(header.ClientId)))
			{
				return false;
			}
			if (this.FanoutTarget.Count != header.FanoutTarget.Count)
			{
				return false;
			}
			for (int k = 0; k < this.FanoutTarget.Count; k++)
			{
				if (!this.FanoutTarget[k].Equals(header.FanoutTarget[k]))
				{
					return false;
				}
			}
			if (this.ClientIdFanoutTarget.Count != header.ClientIdFanoutTarget.Count)
			{
				return false;
			}
			for (int l = 0; l < this.ClientIdFanoutTarget.Count; l++)
			{
				if (!this.ClientIdFanoutTarget[l].Equals(header.ClientIdFanoutTarget[l]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0008EF82 File Offset: 0x0008D182
		public static Header ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Header>(bs, 0, -1);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0008EF8C File Offset: 0x0008D18C
		public void Deserialize(Stream stream)
		{
			Header.Deserialize(stream, this);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0008EF96 File Offset: 0x0008D196
		public static Header Deserialize(Stream stream, Header instance)
		{
			return Header.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0008EFA4 File Offset: 0x0008D1A4
		public static Header DeserializeLengthDelimited(Stream stream)
		{
			Header header = new Header();
			Header.DeserializeLengthDelimited(stream, header);
			return header;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0008EFC0 File Offset: 0x0008D1C0
		public static Header DeserializeLengthDelimited(Stream stream, Header instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Header.Deserialize(stream, instance, num);
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
		public static Header Deserialize(Stream stream, Header instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ObjectId = 0UL;
			instance.Size = 0U;
			instance.Status = 0U;
			if (instance.Error == null)
			{
				instance.Error = new List<ErrorInfo>();
			}
			if (instance.ForwardTargets == null)
			{
				instance.ForwardTargets = new List<ProcessId>();
			}
			if (instance.FanoutTarget == null)
			{
				instance.FanoutTarget = new List<FanoutTarget>();
			}
			if (instance.ClientIdFanoutTarget == null)
			{
				instance.ClientIdFanoutTarget = new List<string>();
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
					if (num <= 58)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.ServiceId = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 16)
							{
								instance.MethodId = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 24)
							{
								instance.Token = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
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
						else
						{
							if (num == 48)
							{
								instance.Status = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 58)
							{
								instance.Error.Add(ErrorInfo.DeserializeLengthDelimited(stream));
								continue;
							}
						}
					}
					else if (num <= 82)
					{
						if (num == 64)
						{
							instance.Timeout = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.IsResponse = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 82)
						{
							instance.ForwardTargets.Add(ProcessId.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num == 93)
						{
							instance.ServiceHash = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 106)
						{
							instance.ClientId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 114)
						{
							instance.FanoutTarget.Add(bnet.protocol.FanoutTarget.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 122)
						{
							instance.ClientIdFanoutTarget.Add(ProtocolParser.ReadString(stream));
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

		// Token: 0x060028AB RID: 10411 RVA: 0x0008F26F File Offset: 0x0008D46F
		public void Serialize(Stream stream)
		{
			Header.Serialize(stream, this);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0008F278 File Offset: 0x0008D478
		public static void Serialize(Stream stream, Header instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ServiceId);
			if (instance.HasMethodId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Token);
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
			if (instance.Error.Count > 0)
			{
				foreach (ErrorInfo errorInfo in instance.Error)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, errorInfo.GetSerializedSize());
					ErrorInfo.Serialize(stream, errorInfo);
				}
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.Timeout);
			}
			if (instance.HasIsResponse)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsResponse);
			}
			if (instance.ForwardTargets.Count > 0)
			{
				foreach (ProcessId processId in instance.ForwardTargets)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, processId.GetSerializedSize());
					ProcessId.Serialize(stream, processId);
				}
			}
			if (instance.HasServiceHash)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.ServiceHash);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
			if (instance.FanoutTarget.Count > 0)
			{
				foreach (FanoutTarget fanoutTarget in instance.FanoutTarget)
				{
					stream.WriteByte(114);
					ProtocolParser.WriteUInt32(stream, fanoutTarget.GetSerializedSize());
					bnet.protocol.FanoutTarget.Serialize(stream, fanoutTarget);
				}
			}
			if (instance.ClientIdFanoutTarget.Count > 0)
			{
				foreach (string s in instance.ClientIdFanoutTarget)
				{
					stream.WriteByte(122);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x0008F53C File Offset: 0x0008D73C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.ServiceId);
			if (this.HasMethodId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MethodId);
			}
			num += ProtocolParser.SizeOfUInt32(this.Token);
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
			if (this.Error.Count > 0)
			{
				foreach (ErrorInfo errorInfo in this.Error)
				{
					num += 1U;
					uint serializedSize = errorInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasTimeout)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Timeout);
			}
			if (this.HasIsResponse)
			{
				num += 1U;
				num += 1U;
			}
			if (this.ForwardTargets.Count > 0)
			{
				foreach (ProcessId processId in this.ForwardTargets)
				{
					num += 1U;
					uint serializedSize2 = processId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasServiceHash)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.FanoutTarget.Count > 0)
			{
				foreach (FanoutTarget fanoutTarget in this.FanoutTarget)
				{
					num += 1U;
					uint serializedSize3 = fanoutTarget.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.ClientIdFanoutTarget.Count > 0)
			{
				foreach (string s in this.ClientIdFanoutTarget)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x04001162 RID: 4450
		public bool HasMethodId;

		// Token: 0x04001163 RID: 4451
		private uint _MethodId;

		// Token: 0x04001165 RID: 4453
		public bool HasObjectId;

		// Token: 0x04001166 RID: 4454
		private ulong _ObjectId;

		// Token: 0x04001167 RID: 4455
		public bool HasSize;

		// Token: 0x04001168 RID: 4456
		private uint _Size;

		// Token: 0x04001169 RID: 4457
		public bool HasStatus;

		// Token: 0x0400116A RID: 4458
		private uint _Status;

		// Token: 0x0400116B RID: 4459
		private List<ErrorInfo> _Error = new List<ErrorInfo>();

		// Token: 0x0400116C RID: 4460
		public bool HasTimeout;

		// Token: 0x0400116D RID: 4461
		private ulong _Timeout;

		// Token: 0x0400116E RID: 4462
		public bool HasIsResponse;

		// Token: 0x0400116F RID: 4463
		private bool _IsResponse;

		// Token: 0x04001170 RID: 4464
		private List<ProcessId> _ForwardTargets = new List<ProcessId>();

		// Token: 0x04001171 RID: 4465
		public bool HasServiceHash;

		// Token: 0x04001172 RID: 4466
		private uint _ServiceHash;

		// Token: 0x04001173 RID: 4467
		public bool HasClientId;

		// Token: 0x04001174 RID: 4468
		private string _ClientId;

		// Token: 0x04001175 RID: 4469
		private List<FanoutTarget> _FanoutTarget = new List<FanoutTarget>();

		// Token: 0x04001176 RID: 4470
		private List<string> _ClientIdFanoutTarget = new List<string>();
	}
}
