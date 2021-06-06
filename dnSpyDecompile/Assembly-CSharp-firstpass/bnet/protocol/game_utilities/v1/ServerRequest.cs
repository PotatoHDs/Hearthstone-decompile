using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000358 RID: 856
	public class ServerRequest : IProtoBuf
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000B260B File Offset: 0x000B080B
		// (set) Token: 0x060035EB RID: 13803 RVA: 0x000B2613 File Offset: 0x000B0813
		public List<Attribute> Attribute
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000B260B File Offset: 0x000B080B
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000B261C File Offset: 0x000B081C
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000B2629 File Offset: 0x000B0829
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000B2637 File Offset: 0x000B0837
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000B2644 File Offset: 0x000B0844
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000B264D File Offset: 0x000B084D
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000B2655 File Offset: 0x000B0855
		public uint Program { get; set; }

		// Token: 0x060035F3 RID: 13811 RVA: 0x000B265E File Offset: 0x000B085E
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000B2667 File Offset: 0x000B0867
		// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000B266F File Offset: 0x000B086F
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

		// Token: 0x060035F6 RID: 13814 RVA: 0x000B2682 File Offset: 0x000B0882
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000B268C File Offset: 0x000B088C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			num ^= this.Program.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000B2718 File Offset: 0x000B0918
		public override bool Equals(object obj)
		{
			ServerRequest serverRequest = obj as ServerRequest;
			if (serverRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != serverRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(serverRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.Program.Equals(serverRequest.Program) && this.HasHost == serverRequest.HasHost && (!this.HasHost || this.Host.Equals(serverRequest.Host));
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000B27C6 File Offset: 0x000B09C6
		public static ServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerRequest>(bs, 0, -1);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000B27D0 File Offset: 0x000B09D0
		public void Deserialize(Stream stream)
		{
			ServerRequest.Deserialize(stream, this);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000B27DA File Offset: 0x000B09DA
		public static ServerRequest Deserialize(Stream stream, ServerRequest instance)
		{
			return ServerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000B27E8 File Offset: 0x000B09E8
		public static ServerRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerRequest serverRequest = new ServerRequest();
			ServerRequest.DeserializeLengthDelimited(stream, serverRequest);
			return serverRequest;
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000B2804 File Offset: 0x000B0A04
		public static ServerRequest DeserializeLengthDelimited(Stream stream, ServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000B282C File Offset: 0x000B0A2C
		public static ServerRequest Deserialize(Stream stream, ServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
					if (num != 21)
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
					else
					{
						instance.Program = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000B2919 File Offset: 0x000B0B19
		public void Serialize(Stream stream)
		{
			ServerRequest.Serialize(stream, this);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000B2924 File Offset: 0x000B0B24
		public static void Serialize(Stream stream, ServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000B29E4 File Offset: 0x000B0BE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 4U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400147E RID: 5246
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001480 RID: 5248
		public bool HasHost;

		// Token: 0x04001481 RID: 5249
		private ProcessId _Host;
	}
}
