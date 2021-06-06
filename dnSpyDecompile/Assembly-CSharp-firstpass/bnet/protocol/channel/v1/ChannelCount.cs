using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BB RID: 1211
	public class ChannelCount : IProtoBuf
	{
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0010457B File Offset: 0x0010277B
		// (set) Token: 0x060054A9 RID: 21673 RVA: 0x00104583 File Offset: 0x00102783
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

		// Token: 0x060054AA RID: 21674 RVA: 0x00104596 File Offset: 0x00102796
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x0010459F File Offset: 0x0010279F
		// (set) Token: 0x060054AC RID: 21676 RVA: 0x001045A7 File Offset: 0x001027A7
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x001045BA File Offset: 0x001027BA
		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x001045C3 File Offset: 0x001027C3
		// (set) Token: 0x060054AF RID: 21679 RVA: 0x001045CB File Offset: 0x001027CB
		public string ChannelName
		{
			get
			{
				return this._ChannelName;
			}
			set
			{
				this._ChannelName = value;
				this.HasChannelName = (value != null);
			}
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x001045DE File Offset: 0x001027DE
		public void SetChannelName(string val)
		{
			this.ChannelName = val;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x001045E8 File Offset: 0x001027E8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			if (this.HasChannelName)
			{
				num ^= this.ChannelName.GetHashCode();
			}
			return num;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00104644 File Offset: 0x00102844
		public override bool Equals(object obj)
		{
			ChannelCount channelCount = obj as ChannelCount;
			return channelCount != null && this.HasChannelId == channelCount.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(channelCount.ChannelId)) && this.HasChannelType == channelCount.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(channelCount.ChannelType)) && this.HasChannelName == channelCount.HasChannelName && (!this.HasChannelName || this.ChannelName.Equals(channelCount.ChannelName));
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060054B3 RID: 21683 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x001046DF File Offset: 0x001028DF
		public static ChannelCount ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelCount>(bs, 0, -1);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x001046E9 File Offset: 0x001028E9
		public void Deserialize(Stream stream)
		{
			ChannelCount.Deserialize(stream, this);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x001046F3 File Offset: 0x001028F3
		public static ChannelCount Deserialize(Stream stream, ChannelCount instance)
		{
			return ChannelCount.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x00104700 File Offset: 0x00102900
		public static ChannelCount DeserializeLengthDelimited(Stream stream)
		{
			ChannelCount channelCount = new ChannelCount();
			ChannelCount.DeserializeLengthDelimited(stream, channelCount);
			return channelCount;
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0010471C File Offset: 0x0010291C
		public static ChannelCount DeserializeLengthDelimited(Stream stream, ChannelCount instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelCount.Deserialize(stream, instance, num);
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x00104744 File Offset: 0x00102944
		public static ChannelCount Deserialize(Stream stream, ChannelCount instance, long limit)
		{
			instance.ChannelType = "default";
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
						if (num != 26)
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
							instance.ChannelName = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.ChannelType = ProtocolParser.ReadString(stream);
					}
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0010481D File Offset: 0x00102A1D
		public void Serialize(Stream stream)
		{
			ChannelCount.Serialize(stream, this);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00104828 File Offset: 0x00102A28
		public static void Serialize(Stream stream, ChannelCount instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasChannelName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelName));
			}
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x001048B0 File Offset: 0x00102AB0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasChannelName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChannelName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04001AD1 RID: 6865
		public bool HasChannelId;

		// Token: 0x04001AD2 RID: 6866
		private EntityId _ChannelId;

		// Token: 0x04001AD3 RID: 6867
		public bool HasChannelType;

		// Token: 0x04001AD4 RID: 6868
		private string _ChannelType;

		// Token: 0x04001AD5 RID: 6869
		public bool HasChannelName;

		// Token: 0x04001AD6 RID: 6870
		private string _ChannelName;
	}
}
