using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000059 RID: 89
	public class Subscribe : IProtoBuf
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00016E79 File Offset: 0x00015079
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00016E81 File Offset: 0x00015081
		public bool FirstSubscribeForRoute
		{
			get
			{
				return this._FirstSubscribeForRoute;
			}
			set
			{
				this._FirstSubscribeForRoute = value;
				this.HasFirstSubscribeForRoute = true;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00016E91 File Offset: 0x00015091
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00016E99 File Offset: 0x00015099
		public bool FirstSubscribe
		{
			get
			{
				return this._FirstSubscribe;
			}
			set
			{
				this._FirstSubscribe = value;
				this.HasFirstSubscribe = true;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00016EA9 File Offset: 0x000150A9
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00016EB1 File Offset: 0x000150B1
		public int UtilSystemId
		{
			get
			{
				return this._UtilSystemId;
			}
			set
			{
				this._UtilSystemId = value;
				this.HasUtilSystemId = true;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00016EC4 File Offset: 0x000150C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFirstSubscribeForRoute)
			{
				num ^= this.FirstSubscribeForRoute.GetHashCode();
			}
			if (this.HasFirstSubscribe)
			{
				num ^= this.FirstSubscribe.GetHashCode();
			}
			if (this.HasUtilSystemId)
			{
				num ^= this.UtilSystemId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00016F2C File Offset: 0x0001512C
		public override bool Equals(object obj)
		{
			Subscribe subscribe = obj as Subscribe;
			return subscribe != null && this.HasFirstSubscribeForRoute == subscribe.HasFirstSubscribeForRoute && (!this.HasFirstSubscribeForRoute || this.FirstSubscribeForRoute.Equals(subscribe.FirstSubscribeForRoute)) && this.HasFirstSubscribe == subscribe.HasFirstSubscribe && (!this.HasFirstSubscribe || this.FirstSubscribe.Equals(subscribe.FirstSubscribe)) && this.HasUtilSystemId == subscribe.HasUtilSystemId && (!this.HasUtilSystemId || this.UtilSystemId.Equals(subscribe.UtilSystemId));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00016FD0 File Offset: 0x000151D0
		public void Deserialize(Stream stream)
		{
			Subscribe.Deserialize(stream, this);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00016FDA File Offset: 0x000151DA
		public static Subscribe Deserialize(Stream stream, Subscribe instance)
		{
			return Subscribe.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00016FE8 File Offset: 0x000151E8
		public static Subscribe DeserializeLengthDelimited(Stream stream)
		{
			Subscribe subscribe = new Subscribe();
			Subscribe.DeserializeLengthDelimited(stream, subscribe);
			return subscribe;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017004 File Offset: 0x00015204
		public static Subscribe DeserializeLengthDelimited(Stream stream, Subscribe instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Subscribe.Deserialize(stream, instance, num);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001702C File Offset: 0x0001522C
		public static Subscribe Deserialize(Stream stream, Subscribe instance, long limit)
		{
			instance.FirstSubscribeForRoute = false;
			instance.FirstSubscribe = false;
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
					if (num != 16)
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
							instance.UtilSystemId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.FirstSubscribe = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.FirstSubscribeForRoute = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000170E8 File Offset: 0x000152E8
		public void Serialize(Stream stream)
		{
			Subscribe.Serialize(stream, this);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000170F4 File Offset: 0x000152F4
		public static void Serialize(Stream stream, Subscribe instance)
		{
			if (instance.HasFirstSubscribeForRoute)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.FirstSubscribeForRoute);
			}
			if (instance.HasFirstSubscribe)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FirstSubscribe);
			}
			if (instance.HasUtilSystemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UtilSystemId));
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00017158 File Offset: 0x00015358
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFirstSubscribeForRoute)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFirstSubscribe)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasUtilSystemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UtilSystemId));
			}
			return num;
		}

		// Token: 0x04000208 RID: 520
		public bool HasFirstSubscribeForRoute;

		// Token: 0x04000209 RID: 521
		private bool _FirstSubscribeForRoute;

		// Token: 0x0400020A RID: 522
		public bool HasFirstSubscribe;

		// Token: 0x0400020B RID: 523
		private bool _FirstSubscribe;

		// Token: 0x0400020C RID: 524
		public bool HasUtilSystemId;

		// Token: 0x0400020D RID: 525
		private int _UtilSystemId;

		// Token: 0x0200056B RID: 1387
		public enum PacketID
		{
			// Token: 0x04001E99 RID: 7833
			ID = 314
		}
	}
}
