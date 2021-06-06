using System;
using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol
{
	// Token: 0x020002A7 RID: 679
	public class GetEventOptions : IProtoBuf
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0008A748 File Offset: 0x00088948
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x0008A750 File Offset: 0x00088950
		public ulong FetchFrom
		{
			get
			{
				return this._FetchFrom;
			}
			set
			{
				this._FetchFrom = value;
				this.HasFetchFrom = true;
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0008A760 File Offset: 0x00088960
		public void SetFetchFrom(ulong val)
		{
			this.FetchFrom = val;
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x0008A769 File Offset: 0x00088969
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x0008A771 File Offset: 0x00088971
		public ulong FetchUntil
		{
			get
			{
				return this._FetchUntil;
			}
			set
			{
				this._FetchUntil = value;
				this.HasFetchUntil = true;
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0008A781 File Offset: 0x00088981
		public void SetFetchUntil(ulong val)
		{
			this.FetchUntil = val;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0008A78A File Offset: 0x0008898A
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x0008A792 File Offset: 0x00088992
		public uint MaxEvents
		{
			get
			{
				return this._MaxEvents;
			}
			set
			{
				this._MaxEvents = value;
				this.HasMaxEvents = true;
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0008A7A2 File Offset: 0x000889A2
		public void SetMaxEvents(uint val)
		{
			this.MaxEvents = val;
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x0008A7AB File Offset: 0x000889AB
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x0008A7B3 File Offset: 0x000889B3
		public EventOrder Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				this._Order = value;
				this.HasOrder = true;
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0008A7C3 File Offset: 0x000889C3
		public void SetOrder(EventOrder val)
		{
			this.Order = val;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0008A7CC File Offset: 0x000889CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFetchFrom)
			{
				num ^= this.FetchFrom.GetHashCode();
			}
			if (this.HasFetchUntil)
			{
				num ^= this.FetchUntil.GetHashCode();
			}
			if (this.HasMaxEvents)
			{
				num ^= this.MaxEvents.GetHashCode();
			}
			if (this.HasOrder)
			{
				num ^= this.Order.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0008A850 File Offset: 0x00088A50
		public override bool Equals(object obj)
		{
			GetEventOptions getEventOptions = obj as GetEventOptions;
			return getEventOptions != null && this.HasFetchFrom == getEventOptions.HasFetchFrom && (!this.HasFetchFrom || this.FetchFrom.Equals(getEventOptions.FetchFrom)) && this.HasFetchUntil == getEventOptions.HasFetchUntil && (!this.HasFetchUntil || this.FetchUntil.Equals(getEventOptions.FetchUntil)) && this.HasMaxEvents == getEventOptions.HasMaxEvents && (!this.HasMaxEvents || this.MaxEvents.Equals(getEventOptions.MaxEvents)) && this.HasOrder == getEventOptions.HasOrder && (!this.HasOrder || this.Order.Equals(getEventOptions.Order));
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0008A92D File Offset: 0x00088B2D
		public static GetEventOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEventOptions>(bs, 0, -1);
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0008A937 File Offset: 0x00088B37
		public void Deserialize(Stream stream)
		{
			GetEventOptions.Deserialize(stream, this);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0008A941 File Offset: 0x00088B41
		public static GetEventOptions Deserialize(Stream stream, GetEventOptions instance)
		{
			return GetEventOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0008A94C File Offset: 0x00088B4C
		public static GetEventOptions DeserializeLengthDelimited(Stream stream)
		{
			GetEventOptions getEventOptions = new GetEventOptions();
			GetEventOptions.DeserializeLengthDelimited(stream, getEventOptions);
			return getEventOptions;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0008A968 File Offset: 0x00088B68
		public static GetEventOptions DeserializeLengthDelimited(Stream stream, GetEventOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetEventOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0008A990 File Offset: 0x00088B90
		public static GetEventOptions Deserialize(Stream stream, GetEventOptions instance, long limit)
		{
			instance.Order = EventOrder.EVENT_DESCENDING;
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.FetchFrom = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.FetchUntil = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.MaxEvents = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Order = (EventOrder)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002711 RID: 10001 RVA: 0x0008AA68 File Offset: 0x00088C68
		public void Serialize(Stream stream)
		{
			GetEventOptions.Serialize(stream, this);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0008AA74 File Offset: 0x00088C74
		public static void Serialize(Stream stream, GetEventOptions instance)
		{
			if (instance.HasFetchFrom)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.FetchFrom);
			}
			if (instance.HasFetchUntil)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.FetchUntil);
			}
			if (instance.HasMaxEvents)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxEvents);
			}
			if (instance.HasOrder)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Order));
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0008AAF4 File Offset: 0x00088CF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFetchFrom)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.FetchFrom);
			}
			if (this.HasFetchUntil)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.FetchUntil);
			}
			if (this.HasMaxEvents)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxEvents);
			}
			if (this.HasOrder)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Order));
			}
			return num;
		}

		// Token: 0x04001112 RID: 4370
		public bool HasFetchFrom;

		// Token: 0x04001113 RID: 4371
		private ulong _FetchFrom;

		// Token: 0x04001114 RID: 4372
		public bool HasFetchUntil;

		// Token: 0x04001115 RID: 4373
		private ulong _FetchUntil;

		// Token: 0x04001116 RID: 4374
		public bool HasMaxEvents;

		// Token: 0x04001117 RID: 4375
		private uint _MaxEvents;

		// Token: 0x04001118 RID: 4376
		public bool HasOrder;

		// Token: 0x04001119 RID: 4377
		private EventOrder _Order;
	}
}
