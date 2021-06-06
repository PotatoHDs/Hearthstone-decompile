using System;
using System.IO;

namespace bnet.protocol.broadcast.v1
{
	// Token: 0x020004E6 RID: 1254
	public class StartBroadcastResponse : IProtoBuf
	{
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0010F313 File Offset: 0x0010D513
		// (set) Token: 0x06005896 RID: 22678 RVA: 0x0010F31B File Offset: 0x0010D51B
		public int ConnectedClients
		{
			get
			{
				return this._ConnectedClients;
			}
			set
			{
				this._ConnectedClients = value;
				this.HasConnectedClients = true;
			}
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0010F32B File Offset: 0x0010D52B
		public void SetConnectedClients(int val)
		{
			this.ConnectedClients = val;
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x0010F334 File Offset: 0x0010D534
		// (set) Token: 0x06005899 RID: 22681 RVA: 0x0010F33C File Offset: 0x0010D53C
		public int FilteredClients
		{
			get
			{
				return this._FilteredClients;
			}
			set
			{
				this._FilteredClients = value;
				this.HasFilteredClients = true;
			}
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x0010F34C File Offset: 0x0010D54C
		public void SetFilteredClients(int val)
		{
			this.FilteredClients = val;
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0010F358 File Offset: 0x0010D558
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasConnectedClients)
			{
				num ^= this.ConnectedClients.GetHashCode();
			}
			if (this.HasFilteredClients)
			{
				num ^= this.FilteredClients.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x0010F3A4 File Offset: 0x0010D5A4
		public override bool Equals(object obj)
		{
			StartBroadcastResponse startBroadcastResponse = obj as StartBroadcastResponse;
			return startBroadcastResponse != null && this.HasConnectedClients == startBroadcastResponse.HasConnectedClients && (!this.HasConnectedClients || this.ConnectedClients.Equals(startBroadcastResponse.ConnectedClients)) && this.HasFilteredClients == startBroadcastResponse.HasFilteredClients && (!this.HasFilteredClients || this.FilteredClients.Equals(startBroadcastResponse.FilteredClients));
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0010F41A File Offset: 0x0010D61A
		public static StartBroadcastResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<StartBroadcastResponse>(bs, 0, -1);
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x0010F424 File Offset: 0x0010D624
		public void Deserialize(Stream stream)
		{
			StartBroadcastResponse.Deserialize(stream, this);
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x0010F42E File Offset: 0x0010D62E
		public static StartBroadcastResponse Deserialize(Stream stream, StartBroadcastResponse instance)
		{
			return StartBroadcastResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x0010F43C File Offset: 0x0010D63C
		public static StartBroadcastResponse DeserializeLengthDelimited(Stream stream)
		{
			StartBroadcastResponse startBroadcastResponse = new StartBroadcastResponse();
			StartBroadcastResponse.DeserializeLengthDelimited(stream, startBroadcastResponse);
			return startBroadcastResponse;
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x0010F458 File Offset: 0x0010D658
		public static StartBroadcastResponse DeserializeLengthDelimited(Stream stream, StartBroadcastResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return StartBroadcastResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x0010F480 File Offset: 0x0010D680
		public static StartBroadcastResponse Deserialize(Stream stream, StartBroadcastResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.FilteredClients = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ConnectedClients = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x0010F519 File Offset: 0x0010D719
		public void Serialize(Stream stream)
		{
			StartBroadcastResponse.Serialize(stream, this);
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x0010F522 File Offset: 0x0010D722
		public static void Serialize(Stream stream, StartBroadcastResponse instance)
		{
			if (instance.HasConnectedClients)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ConnectedClients));
			}
			if (instance.HasFilteredClients)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FilteredClients));
			}
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x0010F560 File Offset: 0x0010D760
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasConnectedClients)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ConnectedClients));
			}
			if (this.HasFilteredClients)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FilteredClients));
			}
			return num;
		}

		// Token: 0x04001BBD RID: 7101
		public bool HasConnectedClients;

		// Token: 0x04001BBE RID: 7102
		private int _ConnectedClients;

		// Token: 0x04001BBF RID: 7103
		public bool HasFilteredClients;

		// Token: 0x04001BC0 RID: 7104
		private int _FilteredClients;
	}
}
