using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000489 RID: 1161
	public class UpdateMemberStateOptions : IProtoBuf
	{
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x000FAC03 File Offset: 0x000F8E03
		// (set) Token: 0x060050D2 RID: 20690 RVA: 0x000FAC0B File Offset: 0x000F8E0B
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

		// Token: 0x060050D3 RID: 20691 RVA: 0x000FAC1E File Offset: 0x000F8E1E
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x000FAC27 File Offset: 0x000F8E27
		// (set) Token: 0x060050D5 RID: 20693 RVA: 0x000FAC2F File Offset: 0x000F8E2F
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

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x000FAC27 File Offset: 0x000F8E27
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x000FAC38 File Offset: 0x000F8E38
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x000FAC45 File Offset: 0x000F8E45
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x000FAC53 File Offset: 0x000F8E53
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x000FAC60 File Offset: 0x000F8E60
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x000FAC6C File Offset: 0x000F8E6C
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

		// Token: 0x060050DC RID: 20700 RVA: 0x000FACE4 File Offset: 0x000F8EE4
		public override bool Equals(object obj)
		{
			UpdateMemberStateOptions updateMemberStateOptions = obj as UpdateMemberStateOptions;
			if (updateMemberStateOptions == null)
			{
				return false;
			}
			if (this.HasMemberId != updateMemberStateOptions.HasMemberId || (this.HasMemberId && !this.MemberId.Equals(updateMemberStateOptions.MemberId)))
			{
				return false;
			}
			if (this.Attribute.Count != updateMemberStateOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(updateMemberStateOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x000FAD7A File Offset: 0x000F8F7A
		public static UpdateMemberStateOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateOptions>(bs, 0, -1);
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x000FAD84 File Offset: 0x000F8F84
		public void Deserialize(Stream stream)
		{
			UpdateMemberStateOptions.Deserialize(stream, this);
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x000FAD8E File Offset: 0x000F8F8E
		public static UpdateMemberStateOptions Deserialize(Stream stream, UpdateMemberStateOptions instance)
		{
			return UpdateMemberStateOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x000FAD9C File Offset: 0x000F8F9C
		public static UpdateMemberStateOptions DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateOptions updateMemberStateOptions = new UpdateMemberStateOptions();
			UpdateMemberStateOptions.DeserializeLengthDelimited(stream, updateMemberStateOptions);
			return updateMemberStateOptions;
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x000FADB8 File Offset: 0x000F8FB8
		public static UpdateMemberStateOptions DeserializeLengthDelimited(Stream stream, UpdateMemberStateOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x000FADE0 File Offset: 0x000F8FE0
		public static UpdateMemberStateOptions Deserialize(Stream stream, UpdateMemberStateOptions instance, long limit)
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

		// Token: 0x060050E4 RID: 20708 RVA: 0x000FAEAA File Offset: 0x000F90AA
		public void Serialize(Stream stream)
		{
			UpdateMemberStateOptions.Serialize(stream, this);
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x000FAEB4 File Offset: 0x000F90B4
		public static void Serialize(Stream stream, UpdateMemberStateOptions instance)
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

		// Token: 0x060050E6 RID: 20710 RVA: 0x000FAF58 File Offset: 0x000F9158
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

		// Token: 0x04001A08 RID: 6664
		public bool HasMemberId;

		// Token: 0x04001A09 RID: 6665
		private GameAccountHandle _MemberId;

		// Token: 0x04001A0A RID: 6666
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
