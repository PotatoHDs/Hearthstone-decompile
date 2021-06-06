using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000336 RID: 822
	public class SubscribeNotificationRequest : IProtoBuf
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000A98A6 File Offset: 0x000A7AA6
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000A98AE File Offset: 0x000A7AAE
		public EntityId EntityId { get; set; }

		// Token: 0x060032B1 RID: 12977 RVA: 0x000A98B7 File Offset: 0x000A7AB7
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000A98C0 File Offset: 0x000A7AC0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.EntityId.GetHashCode();
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000A98DC File Offset: 0x000A7ADC
		public override bool Equals(object obj)
		{
			SubscribeNotificationRequest subscribeNotificationRequest = obj as SubscribeNotificationRequest;
			return subscribeNotificationRequest != null && this.EntityId.Equals(subscribeNotificationRequest.EntityId);
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000A990B File Offset: 0x000A7B0B
		public static SubscribeNotificationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeNotificationRequest>(bs, 0, -1);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000A9915 File Offset: 0x000A7B15
		public void Deserialize(Stream stream)
		{
			SubscribeNotificationRequest.Deserialize(stream, this);
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000A991F File Offset: 0x000A7B1F
		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance)
		{
			return SubscribeNotificationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000A992C File Offset: 0x000A7B2C
		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeNotificationRequest subscribeNotificationRequest = new SubscribeNotificationRequest();
			SubscribeNotificationRequest.DeserializeLengthDelimited(stream, subscribeNotificationRequest);
			return subscribeNotificationRequest;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000A9948 File Offset: 0x000A7B48
		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream, SubscribeNotificationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeNotificationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000A9970 File Offset: 0x000A7B70
		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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

		// Token: 0x060032BB RID: 12987 RVA: 0x000A9A0A File Offset: 0x000A7C0A
		public void Serialize(Stream stream)
		{
			SubscribeNotificationRequest.Serialize(stream, this);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000A9A13 File Offset: 0x000A7C13
		public static void Serialize(Stream stream, SubscribeNotificationRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000A9A54 File Offset: 0x000A7C54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.EntityId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}
	}
}
