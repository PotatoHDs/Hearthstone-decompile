using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B3 RID: 1203
	public class ListChannelCountRequest : IProtoBuf
	{
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x001029A7 File Offset: 0x00100BA7
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x001029AF File Offset: 0x00100BAF
		public EntityId MemberId { get; set; }

		// Token: 0x060053F9 RID: 21497 RVA: 0x001029B8 File Offset: 0x00100BB8
		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x001029C1 File Offset: 0x00100BC1
		// (set) Token: 0x060053FB RID: 21499 RVA: 0x001029C9 File Offset: 0x00100BC9
		public uint ServiceType { get; set; }

		// Token: 0x060053FC RID: 21500 RVA: 0x001029D2 File Offset: 0x00100BD2
		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x001029DC File Offset: 0x00100BDC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.MemberId.GetHashCode() ^ this.ServiceType.GetHashCode();
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x00102A10 File Offset: 0x00100C10
		public override bool Equals(object obj)
		{
			ListChannelCountRequest listChannelCountRequest = obj as ListChannelCountRequest;
			return listChannelCountRequest != null && this.MemberId.Equals(listChannelCountRequest.MemberId) && this.ServiceType.Equals(listChannelCountRequest.ServiceType);
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x00102A57 File Offset: 0x00100C57
		public static ListChannelCountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelCountRequest>(bs, 0, -1);
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00102A61 File Offset: 0x00100C61
		public void Deserialize(Stream stream)
		{
			ListChannelCountRequest.Deserialize(stream, this);
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00102A6B File Offset: 0x00100C6B
		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance)
		{
			return ListChannelCountRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00102A78 File Offset: 0x00100C78
		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream)
		{
			ListChannelCountRequest listChannelCountRequest = new ListChannelCountRequest();
			ListChannelCountRequest.DeserializeLengthDelimited(stream, listChannelCountRequest);
			return listChannelCountRequest;
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00102A94 File Offset: 0x00100C94
		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream, ListChannelCountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelCountRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00102ABC File Offset: 0x00100CBC
		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance, long limit)
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
					if (num != 16)
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
						instance.ServiceType = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.MemberId == null)
				{
					instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00102B6E File Offset: 0x00100D6E
		public void Serialize(Stream stream)
		{
			ListChannelCountRequest.Serialize(stream, this);
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00102B78 File Offset: 0x00100D78
		public static void Serialize(Stream stream, ListChannelCountRequest instance)
		{
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00102BD8 File Offset: 0x00100DD8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.MemberId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(this.ServiceType) + 2U;
		}
	}
}
