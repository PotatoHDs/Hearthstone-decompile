using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000488 RID: 1160
	public class ChannelStateAssignment : IProtoBuf
	{
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x000FA757 File Offset: 0x000F8957
		// (set) Token: 0x060050B8 RID: 20664 RVA: 0x000FA75F File Offset: 0x000F895F
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x000FA772 File Offset: 0x000F8972
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x000FA77B File Offset: 0x000F897B
		// (set) Token: 0x060050BB RID: 20667 RVA: 0x000FA783 File Offset: 0x000F8983
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

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x000FA77B File Offset: 0x000F897B
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x000FA78C File Offset: 0x000F898C
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x000FA799 File Offset: 0x000F8999
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x000FA7A7 File Offset: 0x000F89A7
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x000FA7B4 File Offset: 0x000F89B4
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x000FA7BD File Offset: 0x000F89BD
		// (set) Token: 0x060050C2 RID: 20674 RVA: 0x000FA7C5 File Offset: 0x000F89C5
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

		// Token: 0x060050C3 RID: 20675 RVA: 0x000FA7D5 File Offset: 0x000F89D5
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x000FA7E0 File Offset: 0x000F89E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
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

		// Token: 0x060050C5 RID: 20677 RVA: 0x000FA878 File Offset: 0x000F8A78
		public override bool Equals(object obj)
		{
			ChannelStateAssignment channelStateAssignment = obj as ChannelStateAssignment;
			if (channelStateAssignment == null)
			{
				return false;
			}
			if (this.HasName != channelStateAssignment.HasName || (this.HasName && !this.Name.Equals(channelStateAssignment.Name)))
			{
				return false;
			}
			if (this.Attribute.Count != channelStateAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelStateAssignment.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasPrivacyLevel == channelStateAssignment.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(channelStateAssignment.PrivacyLevel));
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x000FA947 File Offset: 0x000F8B47
		public static ChannelStateAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelStateAssignment>(bs, 0, -1);
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x000FA951 File Offset: 0x000F8B51
		public void Deserialize(Stream stream)
		{
			ChannelStateAssignment.Deserialize(stream, this);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x000FA95B File Offset: 0x000F8B5B
		public static ChannelStateAssignment Deserialize(Stream stream, ChannelStateAssignment instance)
		{
			return ChannelStateAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x000FA968 File Offset: 0x000F8B68
		public static ChannelStateAssignment DeserializeLengthDelimited(Stream stream)
		{
			ChannelStateAssignment channelStateAssignment = new ChannelStateAssignment();
			ChannelStateAssignment.DeserializeLengthDelimited(stream, channelStateAssignment);
			return channelStateAssignment;
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x000FA984 File Offset: 0x000F8B84
		public static ChannelStateAssignment DeserializeLengthDelimited(Stream stream, ChannelStateAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelStateAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x000FA9AC File Offset: 0x000F8BAC
		public static ChannelStateAssignment Deserialize(Stream stream, ChannelStateAssignment instance, long limit)
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
					if (num != 18)
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
							instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Name = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x000FAA7A File Offset: 0x000F8C7A
		public void Serialize(Stream stream)
		{
			ChannelStateAssignment.Serialize(stream, this);
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x000FAA84 File Offset: 0x000F8C84
		public static void Serialize(Stream stream, ChannelStateAssignment instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x000FAB3C File Offset: 0x000F8D3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
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

		// Token: 0x04001A03 RID: 6659
		public bool HasName;

		// Token: 0x04001A04 RID: 6660
		private string _Name;

		// Token: 0x04001A05 RID: 6661
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x04001A06 RID: 6662
		public bool HasPrivacyLevel;

		// Token: 0x04001A07 RID: 6663
		private PrivacyLevel _PrivacyLevel;
	}
}
