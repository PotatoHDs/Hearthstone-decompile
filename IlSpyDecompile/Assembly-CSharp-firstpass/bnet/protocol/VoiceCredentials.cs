using System.IO;
using System.Text;
using bnet.protocol.Types;

namespace bnet.protocol
{
	public class VoiceCredentials : IProtoBuf
	{
		public bool HasVoiceId;

		private string _VoiceId;

		public bool HasToken;

		private string _Token;

		public bool HasUrl;

		private string _Url;

		public bool HasJoinType;

		private VoiceJoinType _JoinType;

		public bool HasMuteReason;

		private VoiceMuteReason _MuteReason;

		public string VoiceId
		{
			get
			{
				return _VoiceId;
			}
			set
			{
				_VoiceId = value;
				HasVoiceId = value != null;
			}
		}

		public string Token
		{
			get
			{
				return _Token;
			}
			set
			{
				_Token = value;
				HasToken = value != null;
			}
		}

		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				_Url = value;
				HasUrl = value != null;
			}
		}

		public VoiceJoinType JoinType
		{
			get
			{
				return _JoinType;
			}
			set
			{
				_JoinType = value;
				HasJoinType = true;
			}
		}

		public VoiceMuteReason MuteReason
		{
			get
			{
				return _MuteReason;
			}
			set
			{
				_MuteReason = value;
				HasMuteReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetVoiceId(string val)
		{
			VoiceId = val;
		}

		public void SetToken(string val)
		{
			Token = val;
		}

		public void SetUrl(string val)
		{
			Url = val;
		}

		public void SetJoinType(VoiceJoinType val)
		{
			JoinType = val;
		}

		public void SetMuteReason(VoiceMuteReason val)
		{
			MuteReason = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasVoiceId)
			{
				num ^= VoiceId.GetHashCode();
			}
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			if (HasUrl)
			{
				num ^= Url.GetHashCode();
			}
			if (HasJoinType)
			{
				num ^= JoinType.GetHashCode();
			}
			if (HasMuteReason)
			{
				num ^= MuteReason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VoiceCredentials voiceCredentials = obj as VoiceCredentials;
			if (voiceCredentials == null)
			{
				return false;
			}
			if (HasVoiceId != voiceCredentials.HasVoiceId || (HasVoiceId && !VoiceId.Equals(voiceCredentials.VoiceId)))
			{
				return false;
			}
			if (HasToken != voiceCredentials.HasToken || (HasToken && !Token.Equals(voiceCredentials.Token)))
			{
				return false;
			}
			if (HasUrl != voiceCredentials.HasUrl || (HasUrl && !Url.Equals(voiceCredentials.Url)))
			{
				return false;
			}
			if (HasJoinType != voiceCredentials.HasJoinType || (HasJoinType && !JoinType.Equals(voiceCredentials.JoinType)))
			{
				return false;
			}
			if (HasMuteReason != voiceCredentials.HasMuteReason || (HasMuteReason && !MuteReason.Equals(voiceCredentials.MuteReason)))
			{
				return false;
			}
			return true;
		}

		public static VoiceCredentials ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VoiceCredentials>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VoiceCredentials Deserialize(Stream stream, VoiceCredentials instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VoiceCredentials DeserializeLengthDelimited(Stream stream)
		{
			VoiceCredentials voiceCredentials = new VoiceCredentials();
			DeserializeLengthDelimited(stream, voiceCredentials);
			return voiceCredentials;
		}

		public static VoiceCredentials DeserializeLengthDelimited(Stream stream, VoiceCredentials instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VoiceCredentials Deserialize(Stream stream, VoiceCredentials instance, long limit)
		{
			instance.JoinType = VoiceJoinType.VOICE_JOIN_NORMAL;
			instance.MuteReason = VoiceMuteReason.VOICE_MUTE_REASON_NONE;
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
					instance.VoiceId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Token = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Url = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.JoinType = (VoiceJoinType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.MuteReason = (VoiceMuteReason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, VoiceCredentials instance)
		{
			if (instance.HasVoiceId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VoiceId));
			}
			if (instance.HasToken)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Token));
			}
			if (instance.HasUrl)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
			if (instance.HasJoinType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.JoinType);
			}
			if (instance.HasMuteReason)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MuteReason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasVoiceId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(VoiceId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasToken)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Token);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasUrl)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Url);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasJoinType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)JoinType);
			}
			if (HasMuteReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MuteReason);
			}
			return num;
		}
	}
}
