using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002A6 RID: 678
	public class TimeSeriesId : IProtoBuf
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0008A4BE File Offset: 0x000886BE
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x0008A4C6 File Offset: 0x000886C6
		public ulong Epoch
		{
			get
			{
				return this._Epoch;
			}
			set
			{
				this._Epoch = value;
				this.HasEpoch = true;
			}
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0008A4D6 File Offset: 0x000886D6
		public void SetEpoch(ulong val)
		{
			this.Epoch = val;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0008A4DF File Offset: 0x000886DF
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x0008A4E7 File Offset: 0x000886E7
		public ulong Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				this._Position = value;
				this.HasPosition = true;
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008A4F7 File Offset: 0x000886F7
		public void SetPosition(ulong val)
		{
			this.Position = val;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0008A500 File Offset: 0x00088700
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEpoch)
			{
				num ^= this.Epoch.GetHashCode();
			}
			if (this.HasPosition)
			{
				num ^= this.Position.GetHashCode();
			}
			return num;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008A54C File Offset: 0x0008874C
		public override bool Equals(object obj)
		{
			TimeSeriesId timeSeriesId = obj as TimeSeriesId;
			return timeSeriesId != null && this.HasEpoch == timeSeriesId.HasEpoch && (!this.HasEpoch || this.Epoch.Equals(timeSeriesId.Epoch)) && this.HasPosition == timeSeriesId.HasPosition && (!this.HasPosition || this.Position.Equals(timeSeriesId.Position));
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0008A5C2 File Offset: 0x000887C2
		public static TimeSeriesId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<TimeSeriesId>(bs, 0, -1);
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0008A5CC File Offset: 0x000887CC
		public void Deserialize(Stream stream)
		{
			TimeSeriesId.Deserialize(stream, this);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0008A5D6 File Offset: 0x000887D6
		public static TimeSeriesId Deserialize(Stream stream, TimeSeriesId instance)
		{
			return TimeSeriesId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0008A5E4 File Offset: 0x000887E4
		public static TimeSeriesId DeserializeLengthDelimited(Stream stream)
		{
			TimeSeriesId timeSeriesId = new TimeSeriesId();
			TimeSeriesId.DeserializeLengthDelimited(stream, timeSeriesId);
			return timeSeriesId;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0008A600 File Offset: 0x00088800
		public static TimeSeriesId DeserializeLengthDelimited(Stream stream, TimeSeriesId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TimeSeriesId.Deserialize(stream, instance, num);
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0008A628 File Offset: 0x00088828
		public static TimeSeriesId Deserialize(Stream stream, TimeSeriesId instance, long limit)
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
						instance.Position = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Epoch = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0008A6BF File Offset: 0x000888BF
		public void Serialize(Stream stream)
		{
			TimeSeriesId.Serialize(stream, this);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008A6C8 File Offset: 0x000888C8
		public static void Serialize(Stream stream, TimeSeriesId instance)
		{
			if (instance.HasEpoch)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Epoch);
			}
			if (instance.HasPosition)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Position);
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008A704 File Offset: 0x00088904
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEpoch)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Epoch);
			}
			if (this.HasPosition)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Position);
			}
			return num;
		}

		// Token: 0x0400110E RID: 4366
		public bool HasEpoch;

		// Token: 0x0400110F RID: 4367
		private ulong _Epoch;

		// Token: 0x04001110 RID: 4368
		public bool HasPosition;

		// Token: 0x04001111 RID: 4369
		private ulong _Position;
	}
}
