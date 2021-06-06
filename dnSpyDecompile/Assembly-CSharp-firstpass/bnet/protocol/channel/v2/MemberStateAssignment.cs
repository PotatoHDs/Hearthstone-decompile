using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200048A RID: 1162
	public class MemberStateAssignment : IProtoBuf
	{
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x000FAFFF File Offset: 0x000F91FF
		// (set) Token: 0x060050E9 RID: 20713 RVA: 0x000FB007 File Offset: 0x000F9207
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x000FB01A File Offset: 0x000F921A
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x000FB023 File Offset: 0x000F9223
		// (set) Token: 0x060050EC RID: 20716 RVA: 0x000FB02B File Offset: 0x000F922B
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

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x000FB023 File Offset: 0x000F9223
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060050EE RID: 20718 RVA: 0x000FB034 File Offset: 0x000F9234
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x000FB041 File Offset: 0x000F9241
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x000FB04F File Offset: 0x000F924F
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x000FB05C File Offset: 0x000F925C
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x000FB068 File Offset: 0x000F9268
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x000FB0E0 File Offset: 0x000F92E0
		public override bool Equals(object obj)
		{
			MemberStateAssignment memberStateAssignment = obj as MemberStateAssignment;
			if (memberStateAssignment == null)
			{
				return false;
			}
			if (this.HasMemberId != memberStateAssignment.HasMemberId || (this.HasMemberId && !this.MemberId.Equals(memberStateAssignment.MemberId)))
			{
				return false;
			}
			if (this.Attribute.Count != memberStateAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(memberStateAssignment.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060050F4 RID: 20724 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x000FB176 File Offset: 0x000F9376
		public static MemberStateAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberStateAssignment>(bs, 0, -1);
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x000FB180 File Offset: 0x000F9380
		public void Deserialize(Stream stream)
		{
			MemberStateAssignment.Deserialize(stream, this);
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x000FB18A File Offset: 0x000F938A
		public static MemberStateAssignment Deserialize(Stream stream, MemberStateAssignment instance)
		{
			return MemberStateAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x000FB198 File Offset: 0x000F9398
		public static MemberStateAssignment DeserializeLengthDelimited(Stream stream)
		{
			MemberStateAssignment memberStateAssignment = new MemberStateAssignment();
			MemberStateAssignment.DeserializeLengthDelimited(stream, memberStateAssignment);
			return memberStateAssignment;
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x000FB1B4 File Offset: 0x000F93B4
		public static MemberStateAssignment DeserializeLengthDelimited(Stream stream, MemberStateAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberStateAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x000FB1DC File Offset: 0x000F93DC
		public static MemberStateAssignment Deserialize(Stream stream, MemberStateAssignment instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
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
					else
					{
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.MemberId == null)
				{
					instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x000FB2A6 File Offset: 0x000F94A6
		public void Serialize(Stream stream)
		{
			MemberStateAssignment.Serialize(stream, this);
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x000FB2B0 File Offset: 0x000F94B0
		public static void Serialize(Stream stream, MemberStateAssignment instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
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
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x000FB354 File Offset: 0x000F9554
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001A0B RID: 6667
		public bool HasMemberId;

		// Token: 0x04001A0C RID: 6668
		private GameAccountHandle _MemberId;

		// Token: 0x04001A0D RID: 6669
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
