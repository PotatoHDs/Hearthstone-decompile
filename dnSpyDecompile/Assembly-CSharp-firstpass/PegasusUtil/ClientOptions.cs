using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000095 RID: 149
	public class ClientOptions : IProtoBuf
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002550B File Offset: 0x0002370B
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00025513 File Offset: 0x00023713
		public List<ClientOption> Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002551C File Offset: 0x0002371C
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00025524 File Offset: 0x00023724
		public bool Failed
		{
			get
			{
				return this._Failed;
			}
			set
			{
				this._Failed = value;
				this.HasFailed = true;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00025534 File Offset: 0x00023734
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ClientOption clientOption in this.Options)
			{
				num ^= clientOption.GetHashCode();
			}
			if (this.HasFailed)
			{
				num ^= this.Failed.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000255B0 File Offset: 0x000237B0
		public override bool Equals(object obj)
		{
			ClientOptions clientOptions = obj as ClientOptions;
			if (clientOptions == null)
			{
				return false;
			}
			if (this.Options.Count != clientOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Options.Count; i++)
			{
				if (!this.Options[i].Equals(clientOptions.Options[i]))
				{
					return false;
				}
			}
			return this.HasFailed == clientOptions.HasFailed && (!this.HasFailed || this.Failed.Equals(clientOptions.Failed));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00025649 File Offset: 0x00023849
		public void Deserialize(Stream stream)
		{
			ClientOptions.Deserialize(stream, this);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00025653 File Offset: 0x00023853
		public static ClientOptions Deserialize(Stream stream, ClientOptions instance)
		{
			return ClientOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00025660 File Offset: 0x00023860
		public static ClientOptions DeserializeLengthDelimited(Stream stream)
		{
			ClientOptions clientOptions = new ClientOptions();
			ClientOptions.DeserializeLengthDelimited(stream, clientOptions);
			return clientOptions;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002567C File Offset: 0x0002387C
		public static ClientOptions DeserializeLengthDelimited(Stream stream, ClientOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000256A4 File Offset: 0x000238A4
		public static ClientOptions Deserialize(Stream stream, ClientOptions instance, long limit)
		{
			if (instance.Options == null)
			{
				instance.Options = new List<ClientOption>();
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
						instance.Failed = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Options.Add(ClientOption.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00025754 File Offset: 0x00023954
		public void Serialize(Stream stream)
		{
			ClientOptions.Serialize(stream, this);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00025760 File Offset: 0x00023960
		public static void Serialize(Stream stream, ClientOptions instance)
		{
			if (instance.Options.Count > 0)
			{
				foreach (ClientOption clientOption in instance.Options)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, clientOption.GetSerializedSize());
					ClientOption.Serialize(stream, clientOption);
				}
			}
			if (instance.HasFailed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Failed);
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000257F4 File Offset: 0x000239F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Options.Count > 0)
			{
				foreach (ClientOption clientOption in this.Options)
				{
					num += 1U;
					uint serializedSize = clientOption.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasFailed)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000381 RID: 897
		private List<ClientOption> _Options = new List<ClientOption>();

		// Token: 0x04000382 RID: 898
		public bool HasFailed;

		// Token: 0x04000383 RID: 899
		private bool _Failed;
	}
}
