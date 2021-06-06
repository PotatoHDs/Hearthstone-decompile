using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000487 RID: 1159
	public class ChannelStateOptions : IProtoBuf
	{
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x000FA36A File Offset: 0x000F856A
		// (set) Token: 0x060050A1 RID: 20641 RVA: 0x000FA372 File Offset: 0x000F8572
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060050A2 RID: 20642 RVA: 0x000FA36A File Offset: 0x000F856A
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060050A3 RID: 20643 RVA: 0x000FA37B File Offset: 0x000F857B
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x000FA388 File Offset: 0x000F8588
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x000FA396 File Offset: 0x000F8596
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x000FA3A3 File Offset: 0x000F85A3
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x000FA3AC File Offset: 0x000F85AC
		// (set) Token: 0x060050A8 RID: 20648 RVA: 0x000FA3B4 File Offset: 0x000F85B4
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x000FA3C4 File Offset: 0x000F85C4
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x000FA3D0 File Offset: 0x000F85D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x000FA454 File Offset: 0x000F8654
		public override bool Equals(object obj)
		{
			ChannelStateOptions channelStateOptions = obj as ChannelStateOptions;
			if (channelStateOptions == null)
			{
				return false;
			}
			if (this.Attribute.Count != channelStateOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelStateOptions.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasPrivacyLevel == channelStateOptions.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(channelStateOptions.PrivacyLevel));
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x000FA4F8 File Offset: 0x000F86F8
		public static ChannelStateOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelStateOptions>(bs, 0, -1);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x000FA502 File Offset: 0x000F8702
		public void Deserialize(Stream stream)
		{
			ChannelStateOptions.Deserialize(stream, this);
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x000FA50C File Offset: 0x000F870C
		public static ChannelStateOptions Deserialize(Stream stream, ChannelStateOptions instance)
		{
			return ChannelStateOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x000FA518 File Offset: 0x000F8718
		public static ChannelStateOptions DeserializeLengthDelimited(Stream stream)
		{
			ChannelStateOptions channelStateOptions = new ChannelStateOptions();
			ChannelStateOptions.DeserializeLengthDelimited(stream, channelStateOptions);
			return channelStateOptions;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x000FA534 File Offset: 0x000F8734
		public static ChannelStateOptions DeserializeLengthDelimited(Stream stream, ChannelStateOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelStateOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x000FA55C File Offset: 0x000F875C
		public static ChannelStateOptions Deserialize(Stream stream, ChannelStateOptions instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
						instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x000FA614 File Offset: 0x000F8814
		public void Serialize(Stream stream)
		{
			ChannelStateOptions.Serialize(stream, this);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x000FA620 File Offset: 0x000F8820
		public static void Serialize(Stream stream, ChannelStateOptions instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x000FA6B4 File Offset: 0x000F88B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			return num;
		}

		// Token: 0x04001A00 RID: 6656
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x04001A01 RID: 6657
		public bool HasPrivacyLevel;

		// Token: 0x04001A02 RID: 6658
		private PrivacyLevel _PrivacyLevel;
	}
}
