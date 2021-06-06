using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x020004A1 RID: 1185
	public class AssignRoleRequest : IProtoBuf
	{
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x000FF73A File Offset: 0x000FD93A
		// (set) Token: 0x0600529F RID: 21151 RVA: 0x000FF742 File Offset: 0x000FD942
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

		// Token: 0x060052A0 RID: 21152 RVA: 0x000FF755 File Offset: 0x000FD955
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x000FF75E File Offset: 0x000FD95E
		// (set) Token: 0x060052A2 RID: 21154 RVA: 0x000FF766 File Offset: 0x000FD966
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

		// Token: 0x060052A3 RID: 21155 RVA: 0x000FF779 File Offset: 0x000FD979
		public void SetAssignment(RoleAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x000FF784 File Offset: 0x000FD984
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

		// Token: 0x060052A5 RID: 21157 RVA: 0x000FF7CC File Offset: 0x000FD9CC
		public override bool Equals(object obj)
		{
			AssignRoleRequest assignRoleRequest = obj as AssignRoleRequest;
			return assignRoleRequest != null && this.HasChannelId == assignRoleRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(assignRoleRequest.ChannelId)) && this.HasAssignment == assignRoleRequest.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(assignRoleRequest.Assignment));
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x000FF83C File Offset: 0x000FDA3C
		public static AssignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AssignRoleRequest>(bs, 0, -1);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x000FF846 File Offset: 0x000FDA46
		public void Deserialize(Stream stream)
		{
			AssignRoleRequest.Deserialize(stream, this);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x000FF850 File Offset: 0x000FDA50
		public static AssignRoleRequest Deserialize(Stream stream, AssignRoleRequest instance)
		{
			return AssignRoleRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x000FF85C File Offset: 0x000FDA5C
		public static AssignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			AssignRoleRequest assignRoleRequest = new AssignRoleRequest();
			AssignRoleRequest.DeserializeLengthDelimited(stream, assignRoleRequest);
			return assignRoleRequest;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x000FF878 File Offset: 0x000FDA78
		public static AssignRoleRequest DeserializeLengthDelimited(Stream stream, AssignRoleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssignRoleRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x000FF8A0 File Offset: 0x000FDAA0
		public static AssignRoleRequest Deserialize(Stream stream, AssignRoleRequest instance, long limit)
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

		// Token: 0x060052AD RID: 21165 RVA: 0x000FF972 File Offset: 0x000FDB72
		public void Serialize(Stream stream)
		{
			AssignRoleRequest.Serialize(stream, this);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x000FF97C File Offset: 0x000FDB7C
		public static void Serialize(Stream stream, AssignRoleRequest instance)
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

		// Token: 0x060052AF RID: 21167 RVA: 0x000FF9E4 File Offset: 0x000FDBE4
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

		// Token: 0x04001A70 RID: 6768
		public bool HasChannelId;

		// Token: 0x04001A71 RID: 6769
		private ChannelId _ChannelId;

		// Token: 0x04001A72 RID: 6770
		public bool HasAssignment;

		// Token: 0x04001A73 RID: 6771
		private RoleAssignment _Assignment;
	}
}
