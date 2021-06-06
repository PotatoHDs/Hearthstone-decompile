using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000435 RID: 1077
	public class AcceptInvitationOptions : IProtoBuf
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x000E3ED0 File Offset: 0x000E20D0
		// (set) Token: 0x060048D5 RID: 18645 RVA: 0x000E3ED8 File Offset: 0x000E20D8
		public uint Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
				this.HasRole = true;
			}
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x000E3EE8 File Offset: 0x000E20E8
		public void SetRole(uint val)
		{
			this.Role = val;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x000E3EF1 File Offset: 0x000E20F1
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x000E3EF9 File Offset: 0x000E20F9
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x000E3F09 File Offset: 0x000E2109
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x000E3F14 File Offset: 0x000E2114
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRole)
			{
				num ^= this.Role.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x000E3F60 File Offset: 0x000E2160
		public override bool Equals(object obj)
		{
			AcceptInvitationOptions acceptInvitationOptions = obj as AcceptInvitationOptions;
			return acceptInvitationOptions != null && this.HasRole == acceptInvitationOptions.HasRole && (!this.HasRole || this.Role.Equals(acceptInvitationOptions.Role)) && this.HasProgram == acceptInvitationOptions.HasProgram && (!this.HasProgram || this.Program.Equals(acceptInvitationOptions.Program));
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x000E3FD6 File Offset: 0x000E21D6
		public static AcceptInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationOptions>(bs, 0, -1);
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x000E3FE0 File Offset: 0x000E21E0
		public void Deserialize(Stream stream)
		{
			AcceptInvitationOptions.Deserialize(stream, this);
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x000E3FEA File Offset: 0x000E21EA
		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance)
		{
			return AcceptInvitationOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x000E3FF8 File Offset: 0x000E21F8
		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationOptions acceptInvitationOptions = new AcceptInvitationOptions();
			AcceptInvitationOptions.DeserializeLengthDelimited(stream, acceptInvitationOptions);
			return acceptInvitationOptions;
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x000E4014 File Offset: 0x000E2214
		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream, AcceptInvitationOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x000E403C File Offset: 0x000E223C
		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 8)
				{
					if (num != 21)
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
						instance.Program = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Role = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x000E40DA File Offset: 0x000E22DA
		public void Serialize(Stream stream)
		{
			AcceptInvitationOptions.Serialize(stream, this);
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x000E40E4 File Offset: 0x000E22E4
		public static void Serialize(Stream stream, AcceptInvitationOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRole)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Role);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x000E4130 File Offset: 0x000E2330
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRole)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Role);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001823 RID: 6179
		public bool HasRole;

		// Token: 0x04001824 RID: 6180
		private uint _Role;

		// Token: 0x04001825 RID: 6181
		public bool HasProgram;

		// Token: 0x04001826 RID: 6182
		private uint _Program;
	}
}
