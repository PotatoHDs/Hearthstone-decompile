using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003B1 RID: 945
	public class UpdateChannelStateNotification : IProtoBuf
	{
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000C6570 File Offset: 0x000C4770
		// (set) Token: 0x06003D9A RID: 15770 RVA: 0x000C6578 File Offset: 0x000C4778
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003D9B RID: 15771 RVA: 0x000C6581 File Offset: 0x000C4781
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000C658A File Offset: 0x000C478A
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000C6592 File Offset: 0x000C4792
		public UpdateChannelStateNotification Note { get; set; }

		// Token: 0x06003D9E RID: 15774 RVA: 0x000C659B File Offset: 0x000C479B
		public void SetNote(UpdateChannelStateNotification val)
		{
			this.Note = val;
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x000C65A4 File Offset: 0x000C47A4
		// (set) Token: 0x06003DA0 RID: 15776 RVA: 0x000C65AC File Offset: 0x000C47AC
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

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000C65BF File Offset: 0x000C47BF
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000C65C8 File Offset: 0x000C47C8
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

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000C6614 File Offset: 0x000C4814
		public override bool Equals(object obj)
		{
			UpdateChannelStateNotification updateChannelStateNotification = obj as UpdateChannelStateNotification;
			return updateChannelStateNotification != null && this.GameHandle.Equals(updateChannelStateNotification.GameHandle) && this.Note.Equals(updateChannelStateNotification.Note) && this.HasHost == updateChannelStateNotification.HasHost && (!this.HasHost || this.Host.Equals(updateChannelStateNotification.Host));
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000C6683 File Offset: 0x000C4883
		public static UpdateChannelStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateNotification>(bs, 0, -1);
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000C668D File Offset: 0x000C488D
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000C6697 File Offset: 0x000C4897
		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance)
		{
			return UpdateChannelStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000C66A4 File Offset: 0x000C48A4
		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateNotification updateChannelStateNotification = new UpdateChannelStateNotification();
			UpdateChannelStateNotification.DeserializeLengthDelimited(stream, updateChannelStateNotification);
			return updateChannelStateNotification;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000C66C0 File Offset: 0x000C48C0
		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream, UpdateChannelStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000C66E8 File Offset: 0x000C48E8
		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance, long limit)
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
						instance.Note = UpdateChannelStateNotification.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateChannelStateNotification.DeserializeLengthDelimited(stream, instance.Note);
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

		// Token: 0x06003DAB RID: 15787 RVA: 0x000C67EA File Offset: 0x000C49EA
		public void Serialize(Stream stream)
		{
			UpdateChannelStateNotification.Serialize(stream, this);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000C67F4 File Offset: 0x000C49F4
		public static void Serialize(Stream stream, UpdateChannelStateNotification instance)
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
			UpdateChannelStateNotification.Serialize(stream, instance.Note);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000C68A8 File Offset: 0x000C4AA8
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

		// Token: 0x040015E2 RID: 5602
		public bool HasHost;

		// Token: 0x040015E3 RID: 5603
		private ProcessId _Host;
	}
}
