using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000380 RID: 896
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000BA286 File Offset: 0x000B8486
		// (set) Token: 0x06003915 RID: 14613 RVA: 0x000BA28E File Offset: 0x000B848E
		public ulong SubscriptionId
		{
			get
			{
				return this._SubscriptionId;
			}
			set
			{
				this._SubscriptionId = value;
				this.HasSubscriptionId = true;
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000BA29E File Offset: 0x000B849E
		public void SetSubscriptionId(ulong val)
		{
			this.SubscriptionId = val;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000BA2A8 File Offset: 0x000B84A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriptionId)
			{
				num ^= this.SubscriptionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000BA2DC File Offset: 0x000B84DC
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasSubscriptionId == subscribeResponse.HasSubscriptionId && (!this.HasSubscriptionId || this.SubscriptionId.Equals(subscribeResponse.SubscriptionId));
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000BA324 File Offset: 0x000B8524
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000BA32E File Offset: 0x000B852E
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000BA338 File Offset: 0x000B8538
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000BA344 File Offset: 0x000B8544
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000BA360 File Offset: 0x000B8560
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000BA388 File Offset: 0x000B8588
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.SubscriptionId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06003920 RID: 14624 RVA: 0x000BA407 File Offset: 0x000B8607
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000BA410 File Offset: 0x000B8610
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasSubscriptionId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.SubscriptionId);
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000BA430 File Offset: 0x000B8630
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriptionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SubscriptionId);
			}
			return num;
		}

		// Token: 0x0400150C RID: 5388
		public bool HasSubscriptionId;

		// Token: 0x0400150D RID: 5389
		private ulong _SubscriptionId;
	}
}
