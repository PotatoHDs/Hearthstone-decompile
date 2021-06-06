using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003B0 RID: 944
	public class FinishGameRequest : IProtoBuf
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000C61DA File Offset: 0x000C43DA
		// (set) Token: 0x06003D84 RID: 15748 RVA: 0x000C61E2 File Offset: 0x000C43E2
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003D85 RID: 15749 RVA: 0x000C61EB File Offset: 0x000C43EB
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000C61F4 File Offset: 0x000C43F4
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x000C61FC File Offset: 0x000C43FC
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

		// Token: 0x06003D88 RID: 15752 RVA: 0x000C620F File Offset: 0x000C440F
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000C6218 File Offset: 0x000C4418
		// (set) Token: 0x06003D8A RID: 15754 RVA: 0x000C6220 File Offset: 0x000C4420
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000C6230 File Offset: 0x000C4430
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000C623C File Offset: 0x000C443C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000C6294 File Offset: 0x000C4494
		public override bool Equals(object obj)
		{
			FinishGameRequest finishGameRequest = obj as FinishGameRequest;
			return finishGameRequest != null && this.GameHandle.Equals(finishGameRequest.GameHandle) && this.HasHost == finishGameRequest.HasHost && (!this.HasHost || this.Host.Equals(finishGameRequest.Host)) && this.HasReason == finishGameRequest.HasReason && (!this.HasReason || this.Reason.Equals(finishGameRequest.Reason));
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000C631C File Offset: 0x000C451C
		public static FinishGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FinishGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000C6326 File Offset: 0x000C4526
		public void Deserialize(Stream stream)
		{
			FinishGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000C6330 File Offset: 0x000C4530
		public static FinishGameRequest Deserialize(Stream stream, FinishGameRequest instance)
		{
			return FinishGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000C633C File Offset: 0x000C453C
		public static FinishGameRequest DeserializeLengthDelimited(Stream stream)
		{
			FinishGameRequest finishGameRequest = new FinishGameRequest();
			FinishGameRequest.DeserializeLengthDelimited(stream, finishGameRequest);
			return finishGameRequest;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000C6358 File Offset: 0x000C4558
		public static FinishGameRequest DeserializeLengthDelimited(Stream stream, FinishGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FinishGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000C6380 File Offset: 0x000C4580
		public static FinishGameRequest Deserialize(Stream stream, FinishGameRequest instance, long limit)
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
						if (num != 24)
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
							instance.Reason = ProtocolParser.ReadUInt32(stream);
						}
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

		// Token: 0x06003D95 RID: 15765 RVA: 0x000C6468 File Offset: 0x000C4668
		public void Serialize(Stream stream)
		{
			FinishGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000C6474 File Offset: 0x000C4674
		public static void Serialize(Stream stream, FinishGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000C6508 File Offset: 0x000C4708
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1U;
		}

		// Token: 0x040015DC RID: 5596
		public bool HasHost;

		// Token: 0x040015DD RID: 5597
		private ProcessId _Host;

		// Token: 0x040015DE RID: 5598
		public bool HasReason;

		// Token: 0x040015DF RID: 5599
		private uint _Reason;
	}
}
