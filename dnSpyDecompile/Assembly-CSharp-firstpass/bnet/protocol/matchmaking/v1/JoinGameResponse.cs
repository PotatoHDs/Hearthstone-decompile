using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E6 RID: 998
	public class JoinGameResponse : IProtoBuf
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x000D143B File Offset: 0x000CF63B
		// (set) Token: 0x060041DB RID: 16859 RVA: 0x000D1443 File Offset: 0x000CF643
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000D1456 File Offset: 0x000CF656
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000D1460 File Offset: 0x000CF660
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000D1490 File Offset: 0x000CF690
		public override bool Equals(object obj)
		{
			JoinGameResponse joinGameResponse = obj as JoinGameResponse;
			return joinGameResponse != null && this.HasRequestId == joinGameResponse.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(joinGameResponse.RequestId));
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000D14D5 File Offset: 0x000CF6D5
		public static JoinGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameResponse>(bs, 0, -1);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000D14DF File Offset: 0x000CF6DF
		public void Deserialize(Stream stream)
		{
			JoinGameResponse.Deserialize(stream, this);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000D14E9 File Offset: 0x000CF6E9
		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance)
		{
			return JoinGameResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000D14F4 File Offset: 0x000CF6F4
		public static JoinGameResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinGameResponse joinGameResponse = new JoinGameResponse();
			JoinGameResponse.DeserializeLengthDelimited(stream, joinGameResponse);
			return joinGameResponse;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000D1510 File Offset: 0x000CF710
		public static JoinGameResponse DeserializeLengthDelimited(Stream stream, JoinGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000D1538 File Offset: 0x000CF738
		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
				}
				else
				{
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

		// Token: 0x060041E6 RID: 16870 RVA: 0x000D15D2 File Offset: 0x000CF7D2
		public void Serialize(Stream stream)
		{
			JoinGameResponse.Serialize(stream, this);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000D15DB File Offset: 0x000CF7DB
		public static void Serialize(Stream stream, JoinGameResponse instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000D160C File Offset: 0x000CF80C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040016B5 RID: 5813
		public bool HasRequestId;

		// Token: 0x040016B6 RID: 5814
		private RequestId _RequestId;
	}
}
