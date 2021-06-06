using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x0200043E RID: 1086
	public class ConnectResponse : IProtoBuf
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x000E5A8F File Offset: 0x000E3C8F
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x000E5A97 File Offset: 0x000E3C97
		public ProcessId ServerId { get; set; }

		// Token: 0x0600498F RID: 18831 RVA: 0x000E5AA0 File Offset: 0x000E3CA0
		public void SetServerId(ProcessId val)
		{
			this.ServerId = val;
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x000E5AA9 File Offset: 0x000E3CA9
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x000E5AB1 File Offset: 0x000E3CB1
		public ProcessId ClientId
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

		// Token: 0x06004992 RID: 18834 RVA: 0x000E5AC4 File Offset: 0x000E3CC4
		public void SetClientId(ProcessId val)
		{
			this.ClientId = val;
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x000E5ACD File Offset: 0x000E3CCD
		// (set) Token: 0x06004994 RID: 18836 RVA: 0x000E5AD5 File Offset: 0x000E3CD5
		public uint BindResult
		{
			get
			{
				return this._BindResult;
			}
			set
			{
				this._BindResult = value;
				this.HasBindResult = true;
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x000E5AE5 File Offset: 0x000E3CE5
		public void SetBindResult(uint val)
		{
			this.BindResult = val;
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x000E5AEE File Offset: 0x000E3CEE
		// (set) Token: 0x06004997 RID: 18839 RVA: 0x000E5AF6 File Offset: 0x000E3CF6
		public BindResponse BindResponse
		{
			get
			{
				return this._BindResponse;
			}
			set
			{
				this._BindResponse = value;
				this.HasBindResponse = (value != null);
			}
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x000E5B09 File Offset: 0x000E3D09
		public void SetBindResponse(BindResponse val)
		{
			this.BindResponse = val;
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x000E5B12 File Offset: 0x000E3D12
		// (set) Token: 0x0600499A RID: 18842 RVA: 0x000E5B1A File Offset: 0x000E3D1A
		public ConnectionMeteringContentHandles ContentHandleArray
		{
			get
			{
				return this._ContentHandleArray;
			}
			set
			{
				this._ContentHandleArray = value;
				this.HasContentHandleArray = (value != null);
			}
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x000E5B2D File Offset: 0x000E3D2D
		public void SetContentHandleArray(ConnectionMeteringContentHandles val)
		{
			this.ContentHandleArray = val;
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x000E5B36 File Offset: 0x000E3D36
		// (set) Token: 0x0600499D RID: 18845 RVA: 0x000E5B3E File Offset: 0x000E3D3E
		public ulong ServerTime
		{
			get
			{
				return this._ServerTime;
			}
			set
			{
				this._ServerTime = value;
				this.HasServerTime = true;
			}
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x000E5B4E File Offset: 0x000E3D4E
		public void SetServerTime(ulong val)
		{
			this.ServerTime = val;
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x000E5B57 File Offset: 0x000E3D57
		// (set) Token: 0x060049A0 RID: 18848 RVA: 0x000E5B5F File Offset: 0x000E3D5F
		public bool UseBindlessRpc
		{
			get
			{
				return this._UseBindlessRpc;
			}
			set
			{
				this._UseBindlessRpc = value;
				this.HasUseBindlessRpc = true;
			}
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x000E5B6F File Offset: 0x000E3D6F
		public void SetUseBindlessRpc(bool val)
		{
			this.UseBindlessRpc = val;
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x000E5B78 File Offset: 0x000E3D78
		// (set) Token: 0x060049A3 RID: 18851 RVA: 0x000E5B80 File Offset: 0x000E3D80
		public ConnectionMeteringContentHandles BinaryContentHandleArray
		{
			get
			{
				return this._BinaryContentHandleArray;
			}
			set
			{
				this._BinaryContentHandleArray = value;
				this.HasBinaryContentHandleArray = (value != null);
			}
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000E5B93 File Offset: 0x000E3D93
		public void SetBinaryContentHandleArray(ConnectionMeteringContentHandles val)
		{
			this.BinaryContentHandleArray = val;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x000E5B9C File Offset: 0x000E3D9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ServerId.GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			if (this.HasBindResult)
			{
				num ^= this.BindResult.GetHashCode();
			}
			if (this.HasBindResponse)
			{
				num ^= this.BindResponse.GetHashCode();
			}
			if (this.HasContentHandleArray)
			{
				num ^= this.ContentHandleArray.GetHashCode();
			}
			if (this.HasServerTime)
			{
				num ^= this.ServerTime.GetHashCode();
			}
			if (this.HasUseBindlessRpc)
			{
				num ^= this.UseBindlessRpc.GetHashCode();
			}
			if (this.HasBinaryContentHandleArray)
			{
				num ^= this.BinaryContentHandleArray.GetHashCode();
			}
			return num;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x000E5C68 File Offset: 0x000E3E68
		public override bool Equals(object obj)
		{
			ConnectResponse connectResponse = obj as ConnectResponse;
			return connectResponse != null && this.ServerId.Equals(connectResponse.ServerId) && this.HasClientId == connectResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(connectResponse.ClientId)) && this.HasBindResult == connectResponse.HasBindResult && (!this.HasBindResult || this.BindResult.Equals(connectResponse.BindResult)) && this.HasBindResponse == connectResponse.HasBindResponse && (!this.HasBindResponse || this.BindResponse.Equals(connectResponse.BindResponse)) && this.HasContentHandleArray == connectResponse.HasContentHandleArray && (!this.HasContentHandleArray || this.ContentHandleArray.Equals(connectResponse.ContentHandleArray)) && this.HasServerTime == connectResponse.HasServerTime && (!this.HasServerTime || this.ServerTime.Equals(connectResponse.ServerTime)) && this.HasUseBindlessRpc == connectResponse.HasUseBindlessRpc && (!this.HasUseBindlessRpc || this.UseBindlessRpc.Equals(connectResponse.UseBindlessRpc)) && this.HasBinaryContentHandleArray == connectResponse.HasBinaryContentHandleArray && (!this.HasBinaryContentHandleArray || this.BinaryContentHandleArray.Equals(connectResponse.BinaryContentHandleArray));
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x000E5DCD File Offset: 0x000E3FCD
		public static ConnectResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectResponse>(bs, 0, -1);
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x000E5DD7 File Offset: 0x000E3FD7
		public void Deserialize(Stream stream)
		{
			ConnectResponse.Deserialize(stream, this);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x000E5DE1 File Offset: 0x000E3FE1
		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance)
		{
			return ConnectResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x000E5DEC File Offset: 0x000E3FEC
		public static ConnectResponse DeserializeLengthDelimited(Stream stream)
		{
			ConnectResponse connectResponse = new ConnectResponse();
			ConnectResponse.DeserializeLengthDelimited(stream, connectResponse);
			return connectResponse;
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x000E5E08 File Offset: 0x000E4008
		public static ConnectResponse DeserializeLengthDelimited(Stream stream, ConnectResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x000E5E30 File Offset: 0x000E4030
		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance, long limit)
		{
			instance.UseBindlessRpc = false;
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
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									if (instance.ClientId == null)
									{
										instance.ClientId = ProcessId.DeserializeLengthDelimited(stream);
										continue;
									}
									ProcessId.DeserializeLengthDelimited(stream, instance.ClientId);
									continue;
								}
							}
							else
							{
								if (instance.ServerId == null)
								{
									instance.ServerId = ProcessId.DeserializeLengthDelimited(stream);
									continue;
								}
								ProcessId.DeserializeLengthDelimited(stream, instance.ServerId);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.BindResult = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 34)
							{
								if (instance.BindResponse == null)
								{
									instance.BindResponse = BindResponse.DeserializeLengthDelimited(stream);
									continue;
								}
								BindResponse.DeserializeLengthDelimited(stream, instance.BindResponse);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num != 42)
						{
							if (num == 48)
							{
								instance.ServerTime = ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.ContentHandleArray == null)
							{
								instance.ContentHandleArray = ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream);
								continue;
							}
							ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, instance.ContentHandleArray);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.UseBindlessRpc = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 66)
						{
							if (instance.BinaryContentHandleArray == null)
							{
								instance.BinaryContentHandleArray = ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream);
								continue;
							}
							ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, instance.BinaryContentHandleArray);
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

		// Token: 0x060049AE RID: 18862 RVA: 0x000E6014 File Offset: 0x000E4214
		public void Serialize(Stream stream)
		{
			ConnectResponse.Serialize(stream, this);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x000E6020 File Offset: 0x000E4220
		public static void Serialize(Stream stream, ConnectResponse instance)
		{
			if (instance.ServerId == null)
			{
				throw new ArgumentNullException("ServerId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ServerId.GetSerializedSize());
			ProcessId.Serialize(stream, instance.ServerId);
			if (instance.HasClientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.BindResult);
			}
			if (instance.HasBindResponse)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.BindResponse.GetSerializedSize());
				BindResponse.Serialize(stream, instance.BindResponse);
			}
			if (instance.HasContentHandleArray)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ContentHandleArray.GetSerializedSize());
				ConnectionMeteringContentHandles.Serialize(stream, instance.ContentHandleArray);
			}
			if (instance.HasServerTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.ServerTime);
			}
			if (instance.HasUseBindlessRpc)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.UseBindlessRpc);
			}
			if (instance.HasBinaryContentHandleArray)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.BinaryContentHandleArray.GetSerializedSize());
				ConnectionMeteringContentHandles.Serialize(stream, instance.BinaryContentHandleArray);
			}
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x000E6174 File Offset: 0x000E4374
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ServerId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasClientId)
			{
				num += 1U;
				uint serializedSize2 = this.ClientId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasBindResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.BindResult);
			}
			if (this.HasBindResponse)
			{
				num += 1U;
				uint serializedSize3 = this.BindResponse.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasContentHandleArray)
			{
				num += 1U;
				uint serializedSize4 = this.ContentHandleArray.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasServerTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ServerTime);
			}
			if (this.HasUseBindlessRpc)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasBinaryContentHandleArray)
			{
				num += 1U;
				uint serializedSize5 = this.BinaryContentHandleArray.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num + 1U;
		}

		// Token: 0x04001839 RID: 6201
		public bool HasClientId;

		// Token: 0x0400183A RID: 6202
		private ProcessId _ClientId;

		// Token: 0x0400183B RID: 6203
		public bool HasBindResult;

		// Token: 0x0400183C RID: 6204
		private uint _BindResult;

		// Token: 0x0400183D RID: 6205
		public bool HasBindResponse;

		// Token: 0x0400183E RID: 6206
		private BindResponse _BindResponse;

		// Token: 0x0400183F RID: 6207
		public bool HasContentHandleArray;

		// Token: 0x04001840 RID: 6208
		private ConnectionMeteringContentHandles _ContentHandleArray;

		// Token: 0x04001841 RID: 6209
		public bool HasServerTime;

		// Token: 0x04001842 RID: 6210
		private ulong _ServerTime;

		// Token: 0x04001843 RID: 6211
		public bool HasUseBindlessRpc;

		// Token: 0x04001844 RID: 6212
		private bool _UseBindlessRpc;

		// Token: 0x04001845 RID: 6213
		public bool HasBinaryContentHandleArray;

		// Token: 0x04001846 RID: 6214
		private ConnectionMeteringContentHandles _BinaryContentHandleArray;
	}
}
