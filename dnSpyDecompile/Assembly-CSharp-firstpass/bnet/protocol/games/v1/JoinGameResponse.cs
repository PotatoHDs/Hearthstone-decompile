using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000372 RID: 882
	public class JoinGameResponse : IProtoBuf
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x000B78CB File Offset: 0x000B5ACB
		// (set) Token: 0x060037F7 RID: 14327 RVA: 0x000B78D3 File Offset: 0x000B5AD3
		public ulong RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000B78E3 File Offset: 0x000B5AE3
		public void SetRequestId(ulong val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000B78EC File Offset: 0x000B5AEC
		// (set) Token: 0x060037FA RID: 14330 RVA: 0x000B78F4 File Offset: 0x000B5AF4
		public bool Queued
		{
			get
			{
				return this._Queued;
			}
			set
			{
				this._Queued = value;
				this.HasQueued = true;
			}
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000B7904 File Offset: 0x000B5B04
		public void SetQueued(bool val)
		{
			this.Queued = val;
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000B790D File Offset: 0x000B5B0D
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000B7915 File Offset: 0x000B5B15
		public List<ConnectInfo> ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000B790D File Offset: 0x000B5B0D
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000B791E File Offset: 0x000B5B1E
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000B792B File Offset: 0x000B5B2B
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000B7939 File Offset: 0x000B5B39
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000B7946 File Offset: 0x000B5B46
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000B7950 File Offset: 0x000B5B50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasQueued)
			{
				num ^= this.Queued.GetHashCode();
			}
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000B79E8 File Offset: 0x000B5BE8
		public override bool Equals(object obj)
		{
			JoinGameResponse joinGameResponse = obj as JoinGameResponse;
			if (joinGameResponse == null)
			{
				return false;
			}
			if (this.HasRequestId != joinGameResponse.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(joinGameResponse.RequestId)))
			{
				return false;
			}
			if (this.HasQueued != joinGameResponse.HasQueued || (this.HasQueued && !this.Queued.Equals(joinGameResponse.Queued)))
			{
				return false;
			}
			if (this.ConnectInfo.Count != joinGameResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ConnectInfo.Count; i++)
			{
				if (!this.ConnectInfo[i].Equals(joinGameResponse.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000B7AAF File Offset: 0x000B5CAF
		public static JoinGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameResponse>(bs, 0, -1);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000B7AB9 File Offset: 0x000B5CB9
		public void Deserialize(Stream stream)
		{
			JoinGameResponse.Deserialize(stream, this);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000B7AC3 File Offset: 0x000B5CC3
		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance)
		{
			return JoinGameResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000B7AD0 File Offset: 0x000B5CD0
		public static JoinGameResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinGameResponse joinGameResponse = new JoinGameResponse();
			JoinGameResponse.DeserializeLengthDelimited(stream, joinGameResponse);
			return joinGameResponse;
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000B7AEC File Offset: 0x000B5CEC
		public static JoinGameResponse DeserializeLengthDelimited(Stream stream, JoinGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000B7B14 File Offset: 0x000B5D14
		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
			if (instance.ConnectInfo == null)
			{
				instance.ConnectInfo = new List<ConnectInfo>();
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
				else if (num != 9)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.ConnectInfo.Add(bnet.protocol.games.v1.ConnectInfo.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Queued = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.RequestId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000B7BE8 File Offset: 0x000B5DE8
		public void Serialize(Stream stream)
		{
			JoinGameResponse.Serialize(stream, this);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000B7BF4 File Offset: 0x000B5DF4
		public static void Serialize(Stream stream, JoinGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Queued);
			}
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.games.v1.ConnectInfo.Serialize(stream, connectInfo);
				}
			}
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000B7CA8 File Offset: 0x000B5EA8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasQueued)
			{
				num += 1U;
				num += 1U;
			}
			if (this.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in this.ConnectInfo)
				{
					num += 1U;
					uint serializedSize = connectInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040014E0 RID: 5344
		public bool HasRequestId;

		// Token: 0x040014E1 RID: 5345
		private ulong _RequestId;

		// Token: 0x040014E2 RID: 5346
		public bool HasQueued;

		// Token: 0x040014E3 RID: 5347
		private bool _Queued;

		// Token: 0x040014E4 RID: 5348
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
