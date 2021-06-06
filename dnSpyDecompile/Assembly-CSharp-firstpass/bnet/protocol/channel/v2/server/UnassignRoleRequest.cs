using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x020004A2 RID: 1186
	public class UnassignRoleRequest : IProtoBuf
	{
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x000FFA3A File Offset: 0x000FDC3A
		// (set) Token: 0x060052B2 RID: 21170 RVA: 0x000FFA42 File Offset: 0x000FDC42
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

		// Token: 0x060052B3 RID: 21171 RVA: 0x000FFA55 File Offset: 0x000FDC55
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x000FFA5E File Offset: 0x000FDC5E
		// (set) Token: 0x060052B5 RID: 21173 RVA: 0x000FFA66 File Offset: 0x000FDC66
		public RoleAssignment Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
				this.HasAssignment = (value != null);
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x000FFA79 File Offset: 0x000FDC79
		public void SetAssignment(RoleAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x000FFA84 File Offset: 0x000FDC84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasAssignment)
			{
				num ^= this.Assignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x000FFACC File Offset: 0x000FDCCC
		public override bool Equals(object obj)
		{
			UnassignRoleRequest unassignRoleRequest = obj as UnassignRoleRequest;
			return unassignRoleRequest != null && this.HasChannelId == unassignRoleRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(unassignRoleRequest.ChannelId)) && this.HasAssignment == unassignRoleRequest.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(unassignRoleRequest.Assignment));
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x000FFB3C File Offset: 0x000FDD3C
		public static UnassignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnassignRoleRequest>(bs, 0, -1);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x000FFB46 File Offset: 0x000FDD46
		public void Deserialize(Stream stream)
		{
			UnassignRoleRequest.Deserialize(stream, this);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x000FFB50 File Offset: 0x000FDD50
		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance)
		{
			return UnassignRoleRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x000FFB5C File Offset: 0x000FDD5C
		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			UnassignRoleRequest.DeserializeLengthDelimited(stream, unassignRoleRequest);
			return unassignRoleRequest;
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x000FFB78 File Offset: 0x000FDD78
		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream, UnassignRoleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnassignRoleRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x000FFBA0 File Offset: 0x000FDDA0
		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance, long limit)
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
					else if (instance.Assignment == null)
					{
						instance.Assignment = RoleAssignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						RoleAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
					}
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x000FFC72 File Offset: 0x000FDE72
		public void Serialize(Stream stream)
		{
			UnassignRoleRequest.Serialize(stream, this);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x000FFC7C File Offset: 0x000FDE7C
		public static void Serialize(Stream stream, UnassignRoleRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				RoleAssignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x000FFCE4 File Offset: 0x000FDEE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize2 = this.Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A74 RID: 6772
		public bool HasChannelId;

		// Token: 0x04001A75 RID: 6773
		private ChannelId _ChannelId;

		// Token: 0x04001A76 RID: 6774
		public bool HasAssignment;

		// Token: 0x04001A77 RID: 6775
		private RoleAssignment _Assignment;
	}
}
