using System;
using System.IO;
using System.Text;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000498 RID: 1176
	public class GetActiveChannelsRequest : IProtoBuf
	{
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060051F2 RID: 20978 RVA: 0x000FDCFB File Offset: 0x000FBEFB
		// (set) Token: 0x060051F3 RID: 20979 RVA: 0x000FDD03 File Offset: 0x000FBF03
		public string CollectionId
		{
			get
			{
				return this._CollectionId;
			}
			set
			{
				this._CollectionId = value;
				this.HasCollectionId = (value != null);
			}
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x000FDD16 File Offset: 0x000FBF16
		public void SetCollectionId(string val)
		{
			this.CollectionId = val;
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x000FDD1F File Offset: 0x000FBF1F
		// (set) Token: 0x060051F6 RID: 20982 RVA: 0x000FDD27 File Offset: 0x000FBF27
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x000FDD3A File Offset: 0x000FBF3A
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x000FDD43 File Offset: 0x000FBF43
		// (set) Token: 0x060051F9 RID: 20985 RVA: 0x000FDD4B File Offset: 0x000FBF4B
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x000FDD5B File Offset: 0x000FBF5B
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x000FDD64 File Offset: 0x000FBF64
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCollectionId)
			{
				num ^= this.CollectionId.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x000FDDC4 File Offset: 0x000FBFC4
		public override bool Equals(object obj)
		{
			GetActiveChannelsRequest getActiveChannelsRequest = obj as GetActiveChannelsRequest;
			return getActiveChannelsRequest != null && this.HasCollectionId == getActiveChannelsRequest.HasCollectionId && (!this.HasCollectionId || this.CollectionId.Equals(getActiveChannelsRequest.CollectionId)) && this.HasType == getActiveChannelsRequest.HasType && (!this.HasType || this.Type.Equals(getActiveChannelsRequest.Type)) && this.HasContinuation == getActiveChannelsRequest.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getActiveChannelsRequest.Continuation));
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x000FDE62 File Offset: 0x000FC062
		public static GetActiveChannelsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetActiveChannelsRequest>(bs, 0, -1);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x000FDE6C File Offset: 0x000FC06C
		public void Deserialize(Stream stream)
		{
			GetActiveChannelsRequest.Deserialize(stream, this);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x000FDE76 File Offset: 0x000FC076
		public static GetActiveChannelsRequest Deserialize(Stream stream, GetActiveChannelsRequest instance)
		{
			return GetActiveChannelsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x000FDE84 File Offset: 0x000FC084
		public static GetActiveChannelsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetActiveChannelsRequest getActiveChannelsRequest = new GetActiveChannelsRequest();
			GetActiveChannelsRequest.DeserializeLengthDelimited(stream, getActiveChannelsRequest);
			return getActiveChannelsRequest;
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x000FDEA0 File Offset: 0x000FC0A0
		public static GetActiveChannelsRequest DeserializeLengthDelimited(Stream stream, GetActiveChannelsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetActiveChannelsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x000FDEC8 File Offset: 0x000FC0C8
		public static GetActiveChannelsRequest Deserialize(Stream stream, GetActiveChannelsRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
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
							instance.Continuation = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
				}
				else
				{
					instance.CollectionId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x000FDF96 File Offset: 0x000FC196
		public void Serialize(Stream stream)
		{
			GetActiveChannelsRequest.Serialize(stream, this);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x000FDFA0 File Offset: 0x000FC1A0
		public static void Serialize(Stream stream, GetActiveChannelsRequest instance)
		{
			if (instance.HasCollectionId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CollectionId));
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x000FE01C File Offset: 0x000FC21C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCollectionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CollectionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x04001A4F RID: 6735
		public bool HasCollectionId;

		// Token: 0x04001A50 RID: 6736
		private string _CollectionId;

		// Token: 0x04001A51 RID: 6737
		public bool HasType;

		// Token: 0x04001A52 RID: 6738
		private UniqueChannelType _Type;

		// Token: 0x04001A53 RID: 6739
		public bool HasContinuation;

		// Token: 0x04001A54 RID: 6740
		private ulong _Continuation;
	}
}
