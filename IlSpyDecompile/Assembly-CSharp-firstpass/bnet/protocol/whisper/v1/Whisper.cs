using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class Whisper : IProtoBuf
	{
		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasRecipientId;

		private AccountId _RecipientId;

		public bool HasContent;

		private string _Content;

		private List<EmbedInfo> _Embed = new List<EmbedInfo>();

		public bool HasCreationTime;

		private ulong _CreationTime;

		public bool HasProgram;

		private uint _Program;

		public bool HasMessageId;

		private MessageId _MessageId;

		public AccountId SenderId
		{
			get
			{
				return _SenderId;
			}
			set
			{
				_SenderId = value;
				HasSenderId = value != null;
			}
		}

		public AccountId RecipientId
		{
			get
			{
				return _RecipientId;
			}
			set
			{
				_RecipientId = value;
				HasRecipientId = value != null;
			}
		}

		public string Content
		{
			get
			{
				return _Content;
			}
			set
			{
				_Content = value;
				HasContent = value != null;
			}
		}

		public List<EmbedInfo> Embed
		{
			get
			{
				return _Embed;
			}
			set
			{
				_Embed = value;
			}
		}

		public List<EmbedInfo> EmbedList => _Embed;

		public int EmbedCount => _Embed.Count;

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public MessageId MessageId
		{
			get
			{
				return _MessageId;
			}
			set
			{
				_MessageId = value;
				HasMessageId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSenderId(AccountId val)
		{
			SenderId = val;
		}

		public void SetRecipientId(AccountId val)
		{
			RecipientId = val;
		}

		public void SetContent(string val)
		{
			Content = val;
		}

		public void AddEmbed(EmbedInfo val)
		{
			_Embed.Add(val);
		}

		public void ClearEmbed()
		{
			_Embed.Clear();
		}

		public void SetEmbed(List<EmbedInfo> val)
		{
			Embed = val;
		}

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetMessageId(MessageId val)
		{
			MessageId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSenderId)
			{
				num ^= SenderId.GetHashCode();
			}
			if (HasRecipientId)
			{
				num ^= RecipientId.GetHashCode();
			}
			if (HasContent)
			{
				num ^= Content.GetHashCode();
			}
			foreach (EmbedInfo item in Embed)
			{
				num ^= item.GetHashCode();
			}
			if (HasCreationTime)
			{
				num ^= CreationTime.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasMessageId)
			{
				num ^= MessageId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Whisper whisper = obj as Whisper;
			if (whisper == null)
			{
				return false;
			}
			if (HasSenderId != whisper.HasSenderId || (HasSenderId && !SenderId.Equals(whisper.SenderId)))
			{
				return false;
			}
			if (HasRecipientId != whisper.HasRecipientId || (HasRecipientId && !RecipientId.Equals(whisper.RecipientId)))
			{
				return false;
			}
			if (HasContent != whisper.HasContent || (HasContent && !Content.Equals(whisper.Content)))
			{
				return false;
			}
			if (Embed.Count != whisper.Embed.Count)
			{
				return false;
			}
			for (int i = 0; i < Embed.Count; i++)
			{
				if (!Embed[i].Equals(whisper.Embed[i]))
				{
					return false;
				}
			}
			if (HasCreationTime != whisper.HasCreationTime || (HasCreationTime && !CreationTime.Equals(whisper.CreationTime)))
			{
				return false;
			}
			if (HasProgram != whisper.HasProgram || (HasProgram && !Program.Equals(whisper.Program)))
			{
				return false;
			}
			if (HasMessageId != whisper.HasMessageId || (HasMessageId && !MessageId.Equals(whisper.MessageId)))
			{
				return false;
			}
			return true;
		}

		public static Whisper ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Whisper>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Whisper Deserialize(Stream stream, Whisper instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Whisper DeserializeLengthDelimited(Stream stream)
		{
			Whisper whisper = new Whisper();
			DeserializeLengthDelimited(stream, whisper);
			return whisper;
		}

		public static Whisper DeserializeLengthDelimited(Stream stream, Whisper instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Whisper Deserialize(Stream stream, Whisper instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Embed == null)
			{
				instance.Embed = new List<EmbedInfo>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
					continue;
				case 18:
					if (instance.RecipientId == null)
					{
						instance.RecipientId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.RecipientId);
					}
					continue;
				case 26:
					instance.Content = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Embed.Add(EmbedInfo.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 61:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 66:
					if (instance.MessageId == null)
					{
						instance.MessageId = MessageId.DeserializeLengthDelimited(stream);
					}
					else
					{
						MessageId.DeserializeLengthDelimited(stream, instance.MessageId);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Whisper instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasRecipientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RecipientId.GetSerializedSize());
				AccountId.Serialize(stream, instance.RecipientId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Embed.Count > 0)
			{
				foreach (EmbedInfo item in instance.Embed)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					EmbedInfo.Serialize(stream, item);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasMessageId)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.MessageId.GetSerializedSize());
				MessageId.Serialize(stream, instance.MessageId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSenderId)
			{
				num++;
				uint serializedSize = SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRecipientId)
			{
				num++;
				uint serializedSize2 = RecipientId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasContent)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (Embed.Count > 0)
			{
				foreach (EmbedInfo item in Embed)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasMessageId)
			{
				num++;
				uint serializedSize4 = MessageId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
