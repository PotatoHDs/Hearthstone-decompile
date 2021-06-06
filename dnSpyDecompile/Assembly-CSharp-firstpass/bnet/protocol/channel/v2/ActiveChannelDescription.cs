using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200048B RID: 1163
	public class ActiveChannelDescription : IProtoBuf
	{
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x000FB3FB File Offset: 0x000F95FB
		// (set) Token: 0x06005100 RID: 20736 RVA: 0x000FB403 File Offset: 0x000F9603
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x000FB416 File Offset: 0x000F9616
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x000FB41F File Offset: 0x000F961F
		// (set) Token: 0x06005103 RID: 20739 RVA: 0x000FB427 File Offset: 0x000F9627
		public ChannelId ChannelId
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

		// Token: 0x06005104 RID: 20740 RVA: 0x000FB43A File Offset: 0x000F963A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x000FB444 File Offset: 0x000F9644
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x000FB48C File Offset: 0x000F968C
		public override bool Equals(object obj)
		{
			ActiveChannelDescription activeChannelDescription = obj as ActiveChannelDescription;
			return activeChannelDescription != null && this.HasType == activeChannelDescription.HasType && (!this.HasType || this.Type.Equals(activeChannelDescription.Type)) && this.HasChannelId == activeChannelDescription.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(activeChannelDescription.ChannelId));
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x000FB4FC File Offset: 0x000F96FC
		public static ActiveChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ActiveChannelDescription>(bs, 0, -1);
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x000FB506 File Offset: 0x000F9706
		public void Deserialize(Stream stream)
		{
			ActiveChannelDescription.Deserialize(stream, this);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x000FB510 File Offset: 0x000F9710
		public static ActiveChannelDescription Deserialize(Stream stream, ActiveChannelDescription instance)
		{
			return ActiveChannelDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x000FB51C File Offset: 0x000F971C
		public static ActiveChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ActiveChannelDescription activeChannelDescription = new ActiveChannelDescription();
			ActiveChannelDescription.DeserializeLengthDelimited(stream, activeChannelDescription);
			return activeChannelDescription;
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x000FB538 File Offset: 0x000F9738
		public static ActiveChannelDescription DeserializeLengthDelimited(Stream stream, ActiveChannelDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ActiveChannelDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x000FB560 File Offset: 0x000F9760
		public static ActiveChannelDescription Deserialize(Stream stream, ActiveChannelDescription instance, long limit)
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
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.Type == null)
				{
					instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
				}
				else
				{
					UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x000FB632 File Offset: 0x000F9832
		public void Serialize(Stream stream)
		{
			ActiveChannelDescription.Serialize(stream, this);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x000FB63C File Offset: 0x000F983C
		public static void Serialize(Stream stream, ActiveChannelDescription instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x000FB6A4 File Offset: 0x000F98A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A0E RID: 6670
		public bool HasType;

		// Token: 0x04001A0F RID: 6671
		private UniqueChannelType _Type;

		// Token: 0x04001A10 RID: 6672
		public bool HasChannelId;

		// Token: 0x04001A11 RID: 6673
		private ChannelId _ChannelId;
	}
}
