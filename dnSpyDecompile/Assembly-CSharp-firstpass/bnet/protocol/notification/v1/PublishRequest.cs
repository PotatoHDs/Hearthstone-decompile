using System;
using System.IO;

namespace bnet.protocol.notification.v1
{
	// Token: 0x02000348 RID: 840
	public class PublishRequest : IProtoBuf
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000AD93B File Offset: 0x000ABB3B
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x000AD943 File Offset: 0x000ABB43
		public Target Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
				this.HasTarget = (value != null);
			}
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000AD956 File Offset: 0x000ABB56
		public void SetTarget(Target val)
		{
			this.Target = val;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000AD95F File Offset: 0x000ABB5F
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x000AD967 File Offset: 0x000ABB67
		public Notification Notification
		{
			get
			{
				return this._Notification;
			}
			set
			{
				this._Notification = value;
				this.HasNotification = (value != null);
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000AD97A File Offset: 0x000ABB7A
		public void SetNotification(Notification val)
		{
			this.Notification = val;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000AD984 File Offset: 0x000ABB84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			if (this.HasNotification)
			{
				num ^= this.Notification.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000AD9CC File Offset: 0x000ABBCC
		public override bool Equals(object obj)
		{
			PublishRequest publishRequest = obj as PublishRequest;
			return publishRequest != null && this.HasTarget == publishRequest.HasTarget && (!this.HasTarget || this.Target.Equals(publishRequest.Target)) && this.HasNotification == publishRequest.HasNotification && (!this.HasNotification || this.Notification.Equals(publishRequest.Notification));
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000ADA3C File Offset: 0x000ABC3C
		public static PublishRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublishRequest>(bs, 0, -1);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000ADA46 File Offset: 0x000ABC46
		public void Deserialize(Stream stream)
		{
			PublishRequest.Deserialize(stream, this);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000ADA50 File Offset: 0x000ABC50
		public static PublishRequest Deserialize(Stream stream, PublishRequest instance)
		{
			return PublishRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000ADA5C File Offset: 0x000ABC5C
		public static PublishRequest DeserializeLengthDelimited(Stream stream)
		{
			PublishRequest publishRequest = new PublishRequest();
			PublishRequest.DeserializeLengthDelimited(stream, publishRequest);
			return publishRequest;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000ADA78 File Offset: 0x000ABC78
		public static PublishRequest DeserializeLengthDelimited(Stream stream, PublishRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PublishRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000ADAA0 File Offset: 0x000ABCA0
		public static PublishRequest Deserialize(Stream stream, PublishRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Notification == null)
					{
						instance.Notification = Notification.DeserializeLengthDelimited(stream);
					}
					else
					{
						Notification.DeserializeLengthDelimited(stream, instance.Notification);
					}
				}
				else if (instance.Target == null)
				{
					instance.Target = Target.DeserializeLengthDelimited(stream);
				}
				else
				{
					Target.DeserializeLengthDelimited(stream, instance.Target);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000ADB72 File Offset: 0x000ABD72
		public void Serialize(Stream stream)
		{
			PublishRequest.Serialize(stream, this);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000ADB7C File Offset: 0x000ABD7C
		public static void Serialize(Stream stream, PublishRequest instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				Target.Serialize(stream, instance.Target);
			}
			if (instance.HasNotification)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Notification.GetSerializedSize());
				Notification.Serialize(stream, instance.Notification);
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000ADBE4 File Offset: 0x000ABDE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasNotification)
			{
				num += 1U;
				uint serializedSize2 = this.Notification.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001420 RID: 5152
		public bool HasTarget;

		// Token: 0x04001421 RID: 5153
		private Target _Target;

		// Token: 0x04001422 RID: 5154
		public bool HasNotification;

		// Token: 0x04001423 RID: 5155
		private Notification _Notification;
	}
}
