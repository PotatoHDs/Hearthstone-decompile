using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200009A RID: 154
	public class ProfileNotices : IProtoBuf
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0002640B File Offset: 0x0002460B
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00026413 File Offset: 0x00024613
		public List<ProfileNotice> List
		{
			get
			{
				return this._List;
			}
			set
			{
				this._List = value;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002641C File Offset: 0x0002461C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ProfileNotice profileNotice in this.List)
			{
				num ^= profileNotice.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00026480 File Offset: 0x00024680
		public override bool Equals(object obj)
		{
			ProfileNotices profileNotices = obj as ProfileNotices;
			if (profileNotices == null)
			{
				return false;
			}
			if (this.List.Count != profileNotices.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(profileNotices.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000264EB File Offset: 0x000246EB
		public void Deserialize(Stream stream)
		{
			ProfileNotices.Deserialize(stream, this);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000264F5 File Offset: 0x000246F5
		public static ProfileNotices Deserialize(Stream stream, ProfileNotices instance)
		{
			return ProfileNotices.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00026500 File Offset: 0x00024700
		public static ProfileNotices DeserializeLengthDelimited(Stream stream)
		{
			ProfileNotices profileNotices = new ProfileNotices();
			ProfileNotices.DeserializeLengthDelimited(stream, profileNotices);
			return profileNotices;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002651C File Offset: 0x0002471C
		public static ProfileNotices DeserializeLengthDelimited(Stream stream, ProfileNotices instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNotices.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00026544 File Offset: 0x00024744
		public static ProfileNotices Deserialize(Stream stream, ProfileNotices instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<ProfileNotice>();
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
				else if (num == 10)
				{
					instance.List.Add(ProfileNotice.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000A65 RID: 2661 RVA: 0x000265DC File Offset: 0x000247DC
		public void Serialize(Stream stream)
		{
			ProfileNotices.Serialize(stream, this);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000265E8 File Offset: 0x000247E8
		public static void Serialize(Stream stream, ProfileNotices instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (ProfileNotice profileNotice in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, profileNotice.GetSerializedSize());
					ProfileNotice.Serialize(stream, profileNotice);
				}
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00026660 File Offset: 0x00024860
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (ProfileNotice profileNotice in this.List)
				{
					num += 1U;
					uint serializedSize = profileNotice.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400038F RID: 911
		private List<ProfileNotice> _List = new List<ProfileNotice>();
	}
}
