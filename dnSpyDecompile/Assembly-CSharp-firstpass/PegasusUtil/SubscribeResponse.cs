using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000BD RID: 189
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000304E0 File Offset: 0x0002E6E0
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x000304E8 File Offset: 0x0002E6E8
		public ulong Route
		{
			get
			{
				return this._Route;
			}
			set
			{
				this._Route = value;
				this.HasRoute = true;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000304F8 File Offset: 0x0002E6F8
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00030500 File Offset: 0x0002E700
		public ulong KeepAliveSecs
		{
			get
			{
				return this._KeepAliveSecs;
			}
			set
			{
				this._KeepAliveSecs = value;
				this.HasKeepAliveSecs = true;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00030510 File Offset: 0x0002E710
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00030518 File Offset: 0x0002E718
		public ulong MaxResubscribeAttempts
		{
			get
			{
				return this._MaxResubscribeAttempts;
			}
			set
			{
				this._MaxResubscribeAttempts = value;
				this.HasMaxResubscribeAttempts = true;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00030528 File Offset: 0x0002E728
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00030530 File Offset: 0x0002E730
		public ulong PendingResponseTimeout
		{
			get
			{
				return this._PendingResponseTimeout;
			}
			set
			{
				this._PendingResponseTimeout = value;
				this.HasPendingResponseTimeout = true;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00030540 File Offset: 0x0002E740
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00030548 File Offset: 0x0002E748
		public ulong PendingSubscribeTimeout
		{
			get
			{
				return this._PendingSubscribeTimeout;
			}
			set
			{
				this._PendingSubscribeTimeout = value;
				this.HasPendingSubscribeTimeout = true;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00030558 File Offset: 0x0002E758
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x00030560 File Offset: 0x0002E760
		public SubscribeResponse.ResponseResult Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00030570 File Offset: 0x0002E770
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x00030578 File Offset: 0x0002E778
		public ulong RequestMaxWaitSecs
		{
			get
			{
				return this._RequestMaxWaitSecs;
			}
			set
			{
				this._RequestMaxWaitSecs = value;
				this.HasRequestMaxWaitSecs = true;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00030588 File Offset: 0x0002E788
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRoute)
			{
				num ^= this.Route.GetHashCode();
			}
			if (this.HasKeepAliveSecs)
			{
				num ^= this.KeepAliveSecs.GetHashCode();
			}
			if (this.HasMaxResubscribeAttempts)
			{
				num ^= this.MaxResubscribeAttempts.GetHashCode();
			}
			if (this.HasPendingResponseTimeout)
			{
				num ^= this.PendingResponseTimeout.GetHashCode();
			}
			if (this.HasPendingSubscribeTimeout)
			{
				num ^= this.PendingSubscribeTimeout.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasRequestMaxWaitSecs)
			{
				num ^= this.RequestMaxWaitSecs.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00030658 File Offset: 0x0002E858
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasRoute == subscribeResponse.HasRoute && (!this.HasRoute || this.Route.Equals(subscribeResponse.Route)) && this.HasKeepAliveSecs == subscribeResponse.HasKeepAliveSecs && (!this.HasKeepAliveSecs || this.KeepAliveSecs.Equals(subscribeResponse.KeepAliveSecs)) && this.HasMaxResubscribeAttempts == subscribeResponse.HasMaxResubscribeAttempts && (!this.HasMaxResubscribeAttempts || this.MaxResubscribeAttempts.Equals(subscribeResponse.MaxResubscribeAttempts)) && this.HasPendingResponseTimeout == subscribeResponse.HasPendingResponseTimeout && (!this.HasPendingResponseTimeout || this.PendingResponseTimeout.Equals(subscribeResponse.PendingResponseTimeout)) && this.HasPendingSubscribeTimeout == subscribeResponse.HasPendingSubscribeTimeout && (!this.HasPendingSubscribeTimeout || this.PendingSubscribeTimeout.Equals(subscribeResponse.PendingSubscribeTimeout)) && this.HasResult == subscribeResponse.HasResult && (!this.HasResult || this.Result.Equals(subscribeResponse.Result)) && this.HasRequestMaxWaitSecs == subscribeResponse.HasRequestMaxWaitSecs && (!this.HasRequestMaxWaitSecs || this.RequestMaxWaitSecs.Equals(subscribeResponse.RequestMaxWaitSecs));
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000307BF File Offset: 0x0002E9BF
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000307C9 File Offset: 0x0002E9C9
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000307D4 File Offset: 0x0002E9D4
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000307F0 File Offset: 0x0002E9F0
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00030818 File Offset: 0x0002EA18
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			instance.MaxResubscribeAttempts = 0UL;
			instance.PendingResponseTimeout = 0UL;
			instance.PendingSubscribeTimeout = 0UL;
			instance.Result = SubscribeResponse.ResponseResult.SUCCESS;
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
					if (num <= 32)
					{
						if (num == 8)
						{
							instance.Route = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.KeepAliveSecs = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.MaxResubscribeAttempts = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.PendingResponseTimeout = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.PendingSubscribeTimeout = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.Result = (SubscribeResponse.ResponseResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.RequestMaxWaitSecs = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003095D File Offset: 0x0002EB5D
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00030968 File Offset: 0x0002EB68
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasRoute)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Route);
			}
			if (instance.HasKeepAliveSecs)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.KeepAliveSecs);
			}
			if (instance.HasMaxResubscribeAttempts)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.MaxResubscribeAttempts);
			}
			if (instance.HasPendingResponseTimeout)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.PendingResponseTimeout);
			}
			if (instance.HasPendingSubscribeTimeout)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.PendingSubscribeTimeout);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
			}
			if (instance.HasRequestMaxWaitSecs)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.RequestMaxWaitSecs);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00030A3C File Offset: 0x0002EC3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRoute)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Route);
			}
			if (this.HasKeepAliveSecs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.KeepAliveSecs);
			}
			if (this.HasMaxResubscribeAttempts)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.MaxResubscribeAttempts);
			}
			if (this.HasPendingResponseTimeout)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.PendingResponseTimeout);
			}
			if (this.HasPendingSubscribeTimeout)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.PendingSubscribeTimeout);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
			}
			if (this.HasRequestMaxWaitSecs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.RequestMaxWaitSecs);
			}
			return num;
		}

		// Token: 0x04000475 RID: 1141
		public bool HasRoute;

		// Token: 0x04000476 RID: 1142
		private ulong _Route;

		// Token: 0x04000477 RID: 1143
		public bool HasKeepAliveSecs;

		// Token: 0x04000478 RID: 1144
		private ulong _KeepAliveSecs;

		// Token: 0x04000479 RID: 1145
		public bool HasMaxResubscribeAttempts;

		// Token: 0x0400047A RID: 1146
		private ulong _MaxResubscribeAttempts;

		// Token: 0x0400047B RID: 1147
		public bool HasPendingResponseTimeout;

		// Token: 0x0400047C RID: 1148
		private ulong _PendingResponseTimeout;

		// Token: 0x0400047D RID: 1149
		public bool HasPendingSubscribeTimeout;

		// Token: 0x0400047E RID: 1150
		private ulong _PendingSubscribeTimeout;

		// Token: 0x0400047F RID: 1151
		public bool HasResult;

		// Token: 0x04000480 RID: 1152
		private SubscribeResponse.ResponseResult _Result;

		// Token: 0x04000481 RID: 1153
		public bool HasRequestMaxWaitSecs;

		// Token: 0x04000482 RID: 1154
		private ulong _RequestMaxWaitSecs;

		// Token: 0x020005CB RID: 1483
		public enum PacketID
		{
			// Token: 0x04001FAE RID: 8110
			ID = 315
		}

		// Token: 0x020005CC RID: 1484
		public enum ResponseResult
		{
			// Token: 0x04001FB0 RID: 8112
			SUCCESS = 1,
			// Token: 0x04001FB1 RID: 8113
			FAILED_UNAVAILABLE
		}
	}
}
