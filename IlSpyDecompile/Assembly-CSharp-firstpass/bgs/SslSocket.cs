using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace bgs
{
	public class SslSocket
	{
		private class SslStreamValidateContext
		{
			public SslSocket m_sslSocket;
		}

		public delegate void BeginConnectDelegate(bool connectFailed, bool isEncrypted, bool isSigned);

		public delegate void BeginSendDelegate(bool wasSent);

		public delegate void BeginReceiveDelegate(int bytesReceived);

		private enum HexStrToBytesError
		{
			[Description("OK")]
			OK,
			[Description("Hex string has an odd number of digits")]
			ODD_NUMBER_OF_DIGITS,
			[Description("Unknown error parsing hex string")]
			UNKNOWN
		}

		private struct BundleInfo
		{
			public List<byte[]> bundleKeyHashs;

			public List<string> bundleUris;

			public List<X509Certificate2> bundleCerts;
		}

		[Serializable]
		private class CertBundle
		{
			[Serializable]
			public class PublicKey
			{
				public string Uri;

				public string ShaHashPublicKeyInfo;
			}

			[Serializable]
			public class SigningCertificate
			{
				public string RawData;
			}

			public PublicKey[] PublicKeys;

			public SigningCertificate[] SigningCertificates;
		}

		private enum CertValidationResult
		{
			OK,
			FAILED_SERVER_RESPONSE,
			FAILED_CERT_BUNDLE
		}

		private readonly IFileUtil m_fileUtil;

		private readonly IJsonSerializer m_jsonSerializer;

		private string m_address;

		public SslCertBundleSettings m_bundleSettings;

		private static Map<SslStream, SslStreamValidateContext> s_streamValidationContexts;

		private static string s_magicBundleSignaturePreamble;

		private const int PUBKEY_MODULUS_SIZE_BITS = 2048;

		private const int PUBKEY_MODULUS_SIZE_BYTES = 256;

		private const int PUBKEY_EXP_SIZE_BYTES = 4;

		private static byte[] s_standardPublicExponent;

		private static byte[] s_standardPublicModulus;

		private static LogThreadHelper s_log;

		private TcpConnection m_connection = new TcpConnection();

		private SslStream m_sslStream;

		private BeginConnectDelegate m_beginConnectDelegate;

		private static X509Certificate2 s_rootCertificate;

		public bool m_canSend = true;

		public bool Connected
		{
			get
			{
				if (Socket == null)
				{
					return false;
				}
				return Socket.Connected;
			}
		}

		private Socket Socket => m_connection.Socket;

		public SslSocket(IFileUtil fileUtil, IJsonSerializer jsonSerializer)
		{
			m_fileUtil = fileUtil;
			m_jsonSerializer = jsonSerializer;
		}

		static SslSocket()
		{
			s_streamValidationContexts = new Map<SslStream, SslStreamValidateContext>();
			s_magicBundleSignaturePreamble = "NGIS";
			s_standardPublicExponent = new byte[4] { 1, 0, 1, 0 };
			s_standardPublicModulus = new byte[256]
			{
				53, 255, 23, 231, 51, 196, 211, 212, 240, 55,
				164, 181, 124, 27, 240, 78, 49, 232, 255, 179,
				12, 30, 136, 16, 77, 175, 19, 11, 88, 86,
				88, 25, 88, 55, 21, 249, 235, 236, 152, 203,
				157, 204, 253, 24, 241, 71, 9, 27, 227, 123,
				56, 40, 158, 14, 155, 31, 159, 149, 218, 157,
				97, 117, 242, 31, 160, 61, 162, 153, 189, 178,
				29, 14, 105, 202, 188, 115, 27, 229, 235, 15,
				231, 251, 43, 123, 178, 53, 5, 143, 245, 181,
				154, 59, 18, 173, 161, 164, 140, 247, 144, 102,
				136, 23, 214, 31, 147, 132, 16, 174, 242, 239,
				42, 122, 95, 65, 123, 92, 128, 210, 94, 26,
				253, 219, 16, 118, 147, 188, 139, 213, 230, 178,
				80, 245, 81, 155, 3, 226, 83, 155, 168, 176,
				177, 55, 213, 37, 102, 69, 8, 129, 32, 15,
				136, 97, 174, 187, 245, 68, 245, 132, 158, 118,
				39, 21, 116, 23, 198, 183, 143, 224, 45, 55,
				92, 248, 82, 49, 50, 63, 250, 68, 127, 239,
				36, 61, 91, 89, 249, 253, 80, 80, 202, 160,
				54, 77, 98, 217, 68, 13, 105, 166, 239, 43,
				206, 204, 194, 163, 188, 245, 162, 28, 238, 119,
				69, 228, 51, 240, 87, 32, 191, 46, 7, 134,
				43, 149, 187, 58, 252, 4, 60, 69, 63, 0,
				52, 11, 54, 187, 75, 193, 15, 149, 24, 195,
				217, 250, 54, 66, 202, 150, 170, 236, 122, 46,
				136, 130, 60, 29, 152, 148
			};
			s_log = new LogThreadHelper("SslSocket");
			string s = ((BattleNet.Client().GetMobileEnvironment() != constants.MobileEnv.PRODUCTION) ? "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----" : "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----");
			s_rootCertificate = new X509Certificate2(Encoding.ASCII.GetBytes(s));
		}

		public static void Process()
		{
			s_log.Process();
		}

		public static string GetBundleStoragePath()
		{
			string text = BattleNet.Client().GetBasePersistentDataPath();
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text + "dlcertbundle";
		}

		public void BeginConnect(string address, uint port, SslCertBundleSettings bundleSettings, BeginConnectDelegate connectDelegate, int tryCount)
		{
			m_beginConnectDelegate = connectDelegate;
			m_bundleSettings = bundleSettings;
			m_connection.LogDebug = s_log.LogDebug;
			m_connection.LogWarning = s_log.LogWarning;
			m_connection.OnFailure = delegate
			{
				ExecuteBeginConnectDelegate(connectFailed: true);
			};
			m_connection.OnSuccess = ConnectCallback;
			m_connection.Connect(address, port, tryCount);
		}

		public void BeginSend(byte[] bytes, BeginSendDelegate sendDelegate)
		{
			try
			{
				if (m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				m_canSend = false;
				m_sslStream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, sendDelegate);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("Exception while trying to call BeginWrite. {0}", ex);
				sendDelegate?.Invoke(wasSent: false);
			}
		}

		public void BeginReceive(byte[] buffer, int size, BeginReceiveDelegate beginReceiveDelegate)
		{
			try
			{
				if (m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				m_sslStream.BeginRead(buffer, 0, size, ReadCallback, beginReceiveDelegate);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("Exception while trying to call BeginRead. {0}", ex);
				beginReceiveDelegate?.Invoke(0);
			}
		}

		public void Close()
		{
			SslStream sslStream = m_sslStream;
			m_sslStream = null;
			try
			{
				m_connection.Disconnect();
				sslStream?.Close();
			}
			catch (Exception)
			{
			}
		}

		private void ConnectCallback()
		{
			try
			{
				ResolveSSLAddress();
				if (m_fileUtil.LoadFromDrive(GetBundleStoragePath(), out var data))
				{
					m_bundleSettings.bundle = new SslCertBundle(data);
				}
				RemoteCertificateValidationCallback userCertificateValidationCallback = OnValidateServerCertificate;
				m_sslStream = new SslStream(new NetworkStream(Socket, ownsSocket: true), leaveInnerStreamOpen: false, userCertificateValidationCallback);
				SslStreamValidateContext sslStreamValidateContext = new SslStreamValidateContext();
				sslStreamValidateContext.m_sslSocket = this;
				s_streamValidationContexts.Add(m_sslStream, sslStreamValidateContext);
				m_sslStream.BeginAuthenticateAsClient(m_address, OnAuthenticateAsClient, null);
			}
			catch (Exception ex)
			{
				s_log.LogError("Exception while trying to authenticate. {0}", ex);
				ExecuteBeginConnectDelegate(connectFailed: true);
			}
		}

		private void ResolveSSLAddress()
		{
			if (UriUtils.GetHostAddressAsIp(m_connection.Host, out var _))
			{
				m_address = ((m_connection.ResolvedAddress.AddressFamily == AddressFamily.InterNetworkV6) ? m_connection.ResolvedAddress.ToString() : ("::ffff:" + m_connection.ResolvedAddress.ToString()));
			}
			else
			{
				m_address = m_connection.Host;
			}
			s_log.LogInfo("ResolveSSLAddress address: {0}", m_address);
		}

		private void WriteCallback(IAsyncResult ar)
		{
			BeginSendDelegate beginSendDelegate = (BeginSendDelegate)ar.AsyncState;
			if (Socket == null || m_sslStream == null)
			{
				beginSendDelegate?.Invoke(wasSent: false);
				return;
			}
			try
			{
				m_sslStream.EndWrite(ar);
				m_canSend = true;
				beginSendDelegate?.Invoke(wasSent: true);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("Exception while trying to call EndWrite. {0}", ex);
				beginSendDelegate?.Invoke(wasSent: false);
			}
		}

		private void ReadCallback(IAsyncResult ar)
		{
			BeginReceiveDelegate beginReceiveDelegate = (BeginReceiveDelegate)ar.AsyncState;
			if (Socket == null || m_sslStream == null)
			{
				beginReceiveDelegate?.Invoke(0);
				return;
			}
			try
			{
				int bytesReceived = m_sslStream.EndRead(ar);
				beginReceiveDelegate?.Invoke(bytesReceived);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("Exception while trying to call EndRead. {0}", ex);
				beginReceiveDelegate?.Invoke(0);
			}
		}

		private static HexStrToBytesError HexStrToBytes(string hex, out byte[] outBytes)
		{
			outBytes = null;
			int length = hex.Length;
			if (length % 2 == 1)
			{
				return HexStrToBytesError.ODD_NUMBER_OF_DIGITS;
			}
			outBytes = new byte[length / 2];
			int num = 0;
			int num2 = 0;
			while (num < length)
			{
				string value = hex.Substring(num, 2);
				outBytes[num2] = Convert.ToByte(value, 16);
				num += 2;
				num2++;
			}
			return HexStrToBytesError.OK;
		}

		private static List<string> GetCommonNamesFromCertSubject(string certSubject)
		{
			List<string> list = new List<string>();
			char[] separator = new char[1] { ',' };
			string[] array = certSubject.Split(separator);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.StartsWith("CN="))
				{
					string item = text.Substring(3);
					list.Add(item);
				}
			}
			return list;
		}

		private bool GetBundleInfo(byte[] unsignedBundleBytes, out BundleInfo info)
		{
			info.bundleKeyHashs = new List<byte[]>();
			info.bundleUris = new List<string>();
			info.bundleCerts = new List<X509Certificate2>();
			string text = null;
			string @string = Encoding.ASCII.GetString(unsignedBundleBytes);
			try
			{
				CertBundle certBundle = m_jsonSerializer.Deserialize<CertBundle>(@string);
				CertBundle.PublicKey[] publicKeys = certBundle.PublicKeys;
				foreach (CertBundle.PublicKey publicKey in publicKeys)
				{
					byte[] outBytes = null;
					HexStrToBytesError hexStrToBytesError = HexStrToBytes(publicKey.ShaHashPublicKeyInfo, out outBytes);
					if (hexStrToBytesError != 0)
					{
						text = EnumUtils.GetString(hexStrToBytesError);
						break;
					}
					info.bundleKeyHashs.Add(outBytes);
					info.bundleUris.Add(publicKey.Uri);
				}
				CertBundle.SigningCertificate[] signingCertificates = certBundle.SigningCertificates;
				foreach (CertBundle.SigningCertificate signingCertificate in signingCertificates)
				{
					X509Certificate2 item = new X509Certificate2(Encoding.ASCII.GetBytes(signingCertificate.RawData));
					info.bundleCerts.Add(item);
				}
			}
			catch (Exception ex)
			{
				text = ex.ToString();
			}
			if (text != null)
			{
				int num = 1024;
				string text2 = ((@string.Length < num) ? @string : (@string.Substring(0, num) + " | .... [remaining output truncated]"));
				s_log.LogWarning("Exception while trying to parse certificate bundle:\nerrorString={0}\ncertBundleString={1}", text, text2);
				return false;
			}
			return true;
		}

		private static bool IsAllowlistedInCertBundle(BundleInfo bundleInfo, string uri, byte[] publicKey)
		{
			byte[] first = SHA256.Create().ComputeHash(publicKey);
			for (int i = 0; i < bundleInfo.bundleKeyHashs.Count; i++)
			{
				byte[] second = bundleInfo.bundleKeyHashs[i];
				if (first.SequenceEqual(second) && bundleInfo.bundleUris[i].Equals(uri))
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsCertSignedByBlizzard(X509Certificate cert)
		{
			string issuer = cert.Issuer;
			char[] separator = new char[1] { ',' };
			string[] array = issuer.Split(separator);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("CN=Battle.net Certificate Authority");
			string[] array2 = array;
			foreach (string item in array2)
			{
				hashSet.Remove(item);
			}
			return hashSet.Count == 0;
		}

		private static byte[] GetUnsignedBundleBytes(byte[] signedBundleBytes)
		{
			int num = signedBundleBytes.Length - (s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			Array.Copy(signedBundleBytes, array, num);
			return array;
		}

		private static bool VerifyBundleSignature(byte[] signedBundleData)
		{
			int num = signedBundleData.Length - (s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return false;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(s_magicBundleSignaturePreamble);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (signedBundleData[num + i] != bytes[i])
				{
					return false;
				}
			}
			SHA256 sHA = SHA256.Create();
			sHA.Initialize();
			sHA.TransformBlock(signedBundleData, 0, num, null, 0);
			string s = "Blizzard Certificate Bundle";
			byte[] bytes2 = Encoding.ASCII.GetBytes(s);
			sHA.TransformBlock(bytes2, 0, bytes2.Length, null, 0);
			sHA.TransformFinalBlock(new byte[1], 0, 0);
			byte[] hash = sHA.Hash;
			byte[] array = new byte[256];
			Array.Copy(signedBundleData, num + s_magicBundleSignaturePreamble.Length, array, 0, 256);
			List<RSAParameters> list = new List<RSAParameters>();
			RSAParameters item = default(RSAParameters);
			item.Modulus = s_standardPublicModulus;
			item.Exponent = s_standardPublicExponent;
			list.Add(item);
			bool result = false;
			for (int j = 0; j < list.Count; j++)
			{
				if (VerifySignedHash(list[j], hash, array))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private static bool VerifySignedHash(RSAParameters key, byte[] hash, byte[] signature)
		{
			byte[] array = new byte[key.Modulus.Length];
			byte[] array2 = new byte[key.Exponent.Length];
			byte[] array3 = new byte[signature.Length];
			Array.Copy(key.Modulus, array, key.Modulus.Length);
			Array.Copy(key.Exponent, array2, key.Exponent.Length);
			Array.Copy(signature, array3, signature.Length);
			Array.Reverse(array);
			Array.Reverse(array2);
			Array.Reverse(array3);
			BigInteger mod = new BigInteger(array);
			BigInteger exp = new BigInteger(array2);
			BigInteger value = BigInteger.PowMod(new BigInteger(array3), exp, mod);
			byte[] array4 = new byte[key.Modulus.Length];
			byte[] array5 = new byte[19]
			{
				48, 49, 48, 13, 6, 9, 96, 134, 72, 1,
				101, 3, 4, 2, 1, 5, 0, 4, 32
			};
			if (!MakePKCS1SignatureBlock(hash, hash.Length, array5, array5.Length, array4, key.Modulus.Length))
			{
				return false;
			}
			byte[] array6 = new byte[array4.Length];
			Array.Copy(array4, array6, array4.Length);
			Array.Reverse(array6);
			return new BigInteger(array6).CompareTo(value) == 0;
		}

		private static bool MakePKCS1SignatureBlock(byte[] hash, int hashSize, byte[] id, int idSize, byte[] signature, int signatureSize)
		{
			int num = 3 + idSize + hashSize;
			if (num > signatureSize)
			{
				return false;
			}
			int num2 = signatureSize - num;
			int num3 = 0;
			for (int i = 0; i < hashSize; i++)
			{
				signature[num3++] = hash[hashSize - i - 1];
			}
			for (int j = 0; j < idSize; j++)
			{
				signature[num3++] = id[idSize - j - 1];
			}
			signature[num3++] = 0;
			for (int k = 0; k < num2; k++)
			{
				signature[num3++] = byte.MaxValue;
			}
			signature[num3++] = 1;
			signature[num3++] = 0;
			if (num3 != signatureSize)
			{
				return false;
			}
			return true;
		}

		private bool OnValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			CertValidationResult certValidationResult = IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
			if (certValidationResult == CertValidationResult.FAILED_CERT_BUNDLE)
			{
				SslSocket sslSocket = s_streamValidationContexts[m_sslStream].m_sslSocket;
				foreach (SslCertBundle item in DownloadCertBundles(sslSocket.m_bundleSettings.bundleDownloadConfig))
				{
					sslSocket.m_bundleSettings.bundle = item;
					certValidationResult = IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
					if (certValidationResult == CertValidationResult.OK)
					{
						m_fileUtil.StoreToDrive(item.CertBundleBytes, GetBundleStoragePath(), overwrite: true, compress: true);
						break;
					}
				}
			}
			return certValidationResult == CertValidationResult.OK;
		}

		private CertValidationResult IsServerCertificateValid(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			SslSocket sslSocket = s_streamValidationContexts[m_sslStream].m_sslSocket;
			SslCertBundleSettings bundleSettings = sslSocket.m_bundleSettings;
			if (bundleSettings.bundle == null || !bundleSettings.bundle.IsUsingCertBundle)
			{
				return CertValidationResult.FAILED_CERT_BUNDLE;
			}
			List<string> commonNamesFromCertSubject = GetCommonNamesFromCertSubject(certificate.Subject);
			BundleInfo info = default(BundleInfo);
			byte[] unsignedBundleBytes = bundleSettings.bundle.CertBundleBytes;
			if (bundleSettings.bundle.isCertBundleSigned)
			{
				if (!VerifyBundleSignature(bundleSettings.bundle.CertBundleBytes))
				{
					return CertValidationResult.FAILED_CERT_BUNDLE;
				}
				unsignedBundleBytes = GetUnsignedBundleBytes(bundleSettings.bundle.CertBundleBytes);
			}
			if (!GetBundleInfo(unsignedBundleBytes, out info))
			{
				return CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag = false;
			byte[] publicKey = certificate.GetPublicKey();
			foreach (string item in commonNamesFromCertSubject)
			{
				if (IsAllowlistedInCertBundle(info, item, publicKey))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag2 = IsCertSignedByBlizzard(certificate);
			bool flag3 = BattleNet.Client().GetRuntimeEnvironment() == constants.RuntimeEnvironment.Mono;
			bool flag4 = !flag2 && flag3 && chain.ChainElements.Count == 1;
			try
			{
				if (sslPolicyErrors != 0)
				{
					SslPolicyErrors sslPolicyErrors2 = ((flag2 || flag4) ? (SslPolicyErrors.RemoteCertificateNotAvailable | SslPolicyErrors.RemoteCertificateChainErrors) : SslPolicyErrors.None);
					if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0 && sslSocket.m_connection.MatchSslCertName(commonNamesFromCertSubject))
					{
						sslPolicyErrors2 |= SslPolicyErrors.RemoteCertificateNameMismatch;
					}
					if ((sslPolicyErrors & ~sslPolicyErrors2) != 0)
					{
						s_log.LogWarning("Failed policy check. sslPolicyErrors: {0}, expectedPolicyErrors: {1}", sslPolicyErrors, sslPolicyErrors2);
						return CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				if (chain.ChainElements == null)
				{
					s_log.LogWarning("ChainElements is null");
					return CertValidationResult.FAILED_SERVER_RESPONSE;
				}
				X509ChainElementEnumerator enumerator2 = chain.ChainElements.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					X509ChainElement current2 = enumerator2.Current;
					s_log.LogDebug("Certificate Thumbprint: {0}", current2.Certificate.Thumbprint);
					X509ChainStatus[] chainElementStatus = current2.ChainElementStatus;
					foreach (X509ChainStatus x509ChainStatus in chainElementStatus)
					{
						s_log.LogDebug("  Certificate Status: {0}", x509ChainStatus.Status);
					}
				}
				bool flag5 = false;
				if (flag2)
				{
					if (chain.ChainElements.Count == 1)
					{
						chain.ChainPolicy.ExtraStore.Add(s_rootCertificate);
						chain.Build(new X509Certificate2(certificate));
						flag5 = true;
					}
				}
				else if (flag4 && info.bundleCerts != null)
				{
					foreach (X509Certificate2 bundleCert in info.bundleCerts)
					{
						chain.ChainPolicy.ExtraStore.Add(bundleCert);
					}
					chain.Build(new X509Certificate2(certificate));
					flag5 = true;
				}
				for (int j = 0; j < chain.ChainElements.Count; j++)
				{
					if (chain.ChainElements[j] == null)
					{
						s_log.LogWarning("ChainElements[" + j + "] is null");
						return (!flag5) ? CertValidationResult.FAILED_SERVER_RESPONSE : CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				if (flag2)
				{
					string text = ((BattleNet.Client().GetMobileEnvironment() != constants.MobileEnv.PRODUCTION) ? "C0805E3CF51F1A56CE9E6E35CB4F4901B68128B7" : "673D9D1072B625CAD95CB47BF0F0F512233E39FD");
					if (chain.ChainElements[1].Certificate.Thumbprint != text)
					{
						s_log.LogWarning("Root certificate thumb print check failure");
						s_log.LogWarning("  expected: {0}", text);
						s_log.LogWarning("  received: {0}", chain.ChainElements[1].Certificate.Thumbprint);
						return (!flag5) ? CertValidationResult.FAILED_SERVER_RESPONSE : CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				for (int k = 0; k < chain.ChainElements.Count; k++)
				{
					if (DateTime.Now > chain.ChainElements[k].Certificate.NotAfter)
					{
						s_log.LogWarning("ChainElements[" + k + "] certificate is expired.");
						return (!flag5) ? CertValidationResult.FAILED_SERVER_RESPONSE : CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				enumerator2 = chain.ChainElements.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					X509ChainStatus[] chainElementStatus = enumerator2.Current.ChainElementStatus;
					for (int i = 0; i < chainElementStatus.Length; i++)
					{
						X509ChainStatus x509ChainStatus2 = chainElementStatus[i];
						if (!(flag2 || flag5) || x509ChainStatus2.Status != X509ChainStatusFlags.UntrustedRoot)
						{
							s_log.LogWarning("Found unexpected chain error={0}.", x509ChainStatus2.Status);
							return (!flag5) ? CertValidationResult.FAILED_SERVER_RESPONSE : CertValidationResult.FAILED_CERT_BUNDLE;
						}
					}
				}
			}
			catch (Exception ex)
			{
				s_log.LogWarning("Exception while trying to validate certificate. {0}", ex);
				return CertValidationResult.FAILED_CERT_BUNDLE;
			}
			return CertValidationResult.OK;
		}

		private static List<SslCertBundle> DownloadCertBundles(UrlDownloaderConfig dlConfig)
		{
			List<SslCertBundle> dlBundles = new List<SslCertBundle>();
			List<string> list = new List<string>();
			if (BattleNet.Client().GetMobileEnvironment() != constants.MobileEnv.PRODUCTION)
			{
				list.Add("http://nydus-qa.web.blizzard.net/Bnet/zxx/client/bgs-key-fingerprint");
			}
			list.Add("http://nydus.battle.net/Bnet/zxx/client/bgs-key-fingerprint");
			IUrlDownloader urlDownloader = BattleNet.Client().GetUrlDownloader();
			int numDownloadsRemaining = list.Count;
			foreach (string item2 in list)
			{
				urlDownloader.Download(item2, delegate(bool success, byte[] bytes)
				{
					if (success)
					{
						SslCertBundle item = new SslCertBundle(bytes);
						dlBundles.Add(item);
					}
					numDownloadsRemaining--;
				}, dlConfig);
			}
			while (numDownloadsRemaining > 0)
			{
				Thread.Sleep(15);
			}
			return dlBundles;
		}

		private void OnAuthenticateAsClient(IAsyncResult ar)
		{
			bool connectFailed = false;
			try
			{
				if (m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				m_sslStream.EndAuthenticateAsClient(ar);
				s_log.LogDebug("Authentication completed IsEncrypted = {0}, IsSigned = {1}", m_sslStream.IsEncrypted, m_sslStream.IsSigned);
			}
			catch (AuthenticationException ex)
			{
				s_log.LogError("Authentication Exception while ending client authentication. {0}.", ex);
				connectFailed = true;
				if (m_fileUtil.RemoveFromDrive(GetBundleStoragePath()))
				{
					s_log.LogWarning("Removed saved cert bundles as they may be invalid.");
				}
				else
				{
					s_log.LogInfo("Saved cert bundles were not removed if they exist.");
				}
			}
			catch (Exception ex2)
			{
				s_log.LogError("Exception while ending client authentication. {0}", ex2);
				connectFailed = true;
			}
			ExecuteBeginConnectDelegate(connectFailed);
		}

		private void ExecuteBeginConnectDelegate(bool connectFailed)
		{
			m_bundleSettings = null;
			if (m_beginConnectDelegate != null)
			{
				bool flag = false;
				bool flag2 = false;
				if (m_sslStream != null)
				{
					flag = m_sslStream.IsEncrypted;
					flag2 = m_sslStream.IsSigned;
				}
				m_beginConnectDelegate(connectFailed, flag, flag2);
				m_beginConnectDelegate = null;
				s_log.LogDebug("Connected={0} isEncrypted={1} isSigned={2}", !connectFailed, flag, flag2);
			}
		}
	}
}
