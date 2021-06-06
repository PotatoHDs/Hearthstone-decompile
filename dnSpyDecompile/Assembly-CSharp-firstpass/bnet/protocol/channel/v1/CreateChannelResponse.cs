using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BD RID: 1213
	public class CreateChannelResponse : IProtoBuf
	{
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x00104DDB File Offset: 0x00102FDB
		// (set) Token: 0x060054D8 RID: 21720 RVA: 0x00104DE3 File Offset: 0x00102FE3
		public ulong ObjectId { get; set; }

		// Token: 0x060054D9 RID: 21721 RVA: 0x00104DEC File Offset: 0x00102FEC
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x00104DF5 File Offset: 0x00102FF5
		// (set) Token: 0x060054DB RID: 21723 RVA: 0x00104DFD File Offset: 0x00102FFD
		public EntityId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x00104E10 File Offset: 0x00103010
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00104E1C File Offset: 0x0010301C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00104E60 File Offset: 0x00103060
		public override bool Equals(object obj)
		{
			CreateChannelResponse createChannelResponse = obj as CreateChannelResponse;
			return createChannelResponse != null && this.ObjectId.Equals(createChannelResponse.ObjectId) && this.HasChannelId == createChannelResponse.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(createChannelResponse.ChannelId));
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00104EBD File Offset: 0x001030BD
		public static CreateChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelResponse>(bs, 0, -1);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x00104EC7 File Offset: 0x001030C7
		public void Deserialize(Stream stream)
		{
			CreateChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00104ED1 File Offset: 0x001030D1
		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance)
		{
			return CreateChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00104EDC File Offset: 0x001030DC
		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelResponse createChannelResponse = new CreateChannelResponse();
			CreateChannelResponse.DeserializeLengthDelimited(stream, createChannelResponse);
			return createChannelResponse;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00104EF8 File Offset: 0x001030F8
		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream, CreateChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x00104F20 File Offset: 0x00103120
		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00104FD1 File Offset: 0x001031D1
		public void Serialize(Stream stream)
		{
			CreateChannelResponse.Serialize(stream, this);
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x00104FDC File Offset: 0x001031DC
		public static void Serialize(Stream stream, CreateChannelResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0010502C File Offset: 0x0010322C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x04001AE0 RID: 6880
		public bool HasChannelId;

		// Token: 0x04001AE1 RID: 6881
		private EntityId _ChannelId;
	}
}
