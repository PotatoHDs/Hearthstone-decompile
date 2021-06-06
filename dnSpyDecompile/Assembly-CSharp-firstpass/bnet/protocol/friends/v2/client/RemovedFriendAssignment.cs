using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000413 RID: 1043
	public class RemovedFriendAssignment : IProtoBuf
	{
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x000DA5C5 File Offset: 0x000D87C5
		// (set) Token: 0x06004587 RID: 17799 RVA: 0x000DA5CD File Offset: 0x000D87CD
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x000DA5DD File Offset: 0x000D87DD
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000DA5E8 File Offset: 0x000D87E8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000DA61C File Offset: 0x000D881C
		public override bool Equals(object obj)
		{
			RemovedFriendAssignment removedFriendAssignment = obj as RemovedFriendAssignment;
			return removedFriendAssignment != null && this.HasId == removedFriendAssignment.HasId && (!this.HasId || this.Id.Equals(removedFriendAssignment.Id));
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x000DA664 File Offset: 0x000D8864
		public static RemovedFriendAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovedFriendAssignment>(bs, 0, -1);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000DA66E File Offset: 0x000D886E
		public void Deserialize(Stream stream)
		{
			RemovedFriendAssignment.Deserialize(stream, this);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x000DA678 File Offset: 0x000D8878
		public static RemovedFriendAssignment Deserialize(Stream stream, RemovedFriendAssignment instance)
		{
			return RemovedFriendAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000DA684 File Offset: 0x000D8884
		public static RemovedFriendAssignment DeserializeLengthDelimited(Stream stream)
		{
			RemovedFriendAssignment removedFriendAssignment = new RemovedFriendAssignment();
			RemovedFriendAssignment.DeserializeLengthDelimited(stream, removedFriendAssignment);
			return removedFriendAssignment;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000DA6A0 File Offset: 0x000D88A0
		public static RemovedFriendAssignment DeserializeLengthDelimited(Stream stream, RemovedFriendAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemovedFriendAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000DA6C8 File Offset: 0x000D88C8
		public static RemovedFriendAssignment Deserialize(Stream stream, RemovedFriendAssignment instance, long limit)
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
				else if (num == 8)
				{
					instance.Id = ProtocolParser.ReadUInt64(stream);
				}
				else
				{
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

		// Token: 0x06004592 RID: 17810 RVA: 0x000DA747 File Offset: 0x000D8947
		public void Serialize(Stream stream)
		{
			RemovedFriendAssignment.Serialize(stream, this);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x000DA750 File Offset: 0x000D8950
		public static void Serialize(Stream stream, RemovedFriendAssignment instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000DA770 File Offset: 0x000D8970
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Id);
			}
			return num;
		}

		// Token: 0x04001767 RID: 5991
		public bool HasId;

		// Token: 0x04001768 RID: 5992
		private ulong _Id;
	}
}
