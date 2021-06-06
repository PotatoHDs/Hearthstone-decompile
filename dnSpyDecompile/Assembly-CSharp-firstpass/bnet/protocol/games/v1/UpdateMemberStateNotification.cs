using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003B2 RID: 946
	public class UpdateMemberStateNotification : IProtoBuf
	{
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000C690D File Offset: 0x000C4B0D
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x000C6915 File Offset: 0x000C4B15
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000C691E File Offset: 0x000C4B1E
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x000C6927 File Offset: 0x000C4B27
		// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x000C692F File Offset: 0x000C4B2F
		public UpdateMemberStateNotification Note { get; set; }

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000C6938 File Offset: 0x000C4B38
		public void SetNote(UpdateMemberStateNotification val)
		{
			this.Note = val;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x000C6941 File Offset: 0x000C4B41
		// (set) Token: 0x06003DB6 RID: 15798 RVA: 0x000C6949 File Offset: 0x000C4B49
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000C695C File Offset: 0x000C4B5C
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000C6968 File Offset: 0x000C4B68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			num ^= this.Note.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000C69B4 File Offset: 0x000C4BB4
		public override bool Equals(object obj)
		{
			UpdateMemberStateNotification updateMemberStateNotification = obj as UpdateMemberStateNotification;
			return updateMemberStateNotification != null && this.GameHandle.Equals(updateMemberStateNotification.GameHandle) && this.Note.Equals(updateMemberStateNotification.Note) && this.HasHost == updateMemberStateNotification.HasHost && (!this.HasHost || this.Host.Equals(updateMemberStateNotification.Host));
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000C6A23 File Offset: 0x000C4C23
		public static UpdateMemberStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateNotification>(bs, 0, -1);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000C6A2D File Offset: 0x000C4C2D
		public void Deserialize(Stream stream)
		{
			UpdateMemberStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000C6A37 File Offset: 0x000C4C37
		public static UpdateMemberStateNotification Deserialize(Stream stream, UpdateMemberStateNotification instance)
		{
			return UpdateMemberStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000C6A44 File Offset: 0x000C4C44
		public static UpdateMemberStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateNotification updateMemberStateNotification = new UpdateMemberStateNotification();
			UpdateMemberStateNotification.DeserializeLengthDelimited(stream, updateMemberStateNotification);
			return updateMemberStateNotification;
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000C6A60 File Offset: 0x000C4C60
		public static UpdateMemberStateNotification DeserializeLengthDelimited(Stream stream, UpdateMemberStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000C6A88 File Offset: 0x000C4C88
		public static UpdateMemberStateNotification Deserialize(Stream stream, UpdateMemberStateNotification instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Host == null)
						{
							instance.Host = ProcessId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ProcessId.DeserializeLengthDelimited(stream, instance.Host);
						}
					}
					else if (instance.Note == null)
					{
						instance.Note = UpdateMemberStateNotification.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateMemberStateNotification.DeserializeLengthDelimited(stream, instance.Note);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000C6B8A File Offset: 0x000C4D8A
		public void Serialize(Stream stream)
		{
			UpdateMemberStateNotification.Serialize(stream, this);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000C6B94 File Offset: 0x000C4D94
		public static void Serialize(Stream stream, UpdateMemberStateNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Note == null)
			{
				throw new ArgumentNullException("Note", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Note.GetSerializedSize());
			UpdateMemberStateNotification.Serialize(stream, instance.Note);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000C6C48 File Offset: 0x000C4E48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.Note.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize3 = this.Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2U;
		}

		// Token: 0x040015E6 RID: 5606
		public bool HasHost;

		// Token: 0x040015E7 RID: 5607
		private ProcessId _Host;
	}
}
