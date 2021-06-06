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
	// Token: 0x02000265 RID: 613
	public class SslSocket
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x0008425D File Offset: 0x0008245D
		public SslSocket(IFileUtil fileUtil, IJsonSerializer jsonSerializer)
		{
			this.m_fileUtil = fileUtil;
			this.m_jsonSerializer = jsonSerializer;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00084288 File Offset: 0x00082488
		static SslSocket()
		{
			byte[] array = new byte[4];
			array[0] = 1;
			array[2] = 1;
			SslSocket.s_standardPublicExponent = array;
			SslSocket.s_standardPublicModulus = new byte[]
			{
				53,
				byte.MaxValue,
				23,
				231,
				51,
				196,
				211,
				212,
				240,
				55,
				164,
				181,
				124,
				27,
				240,
				78,
				49,
				232,
				byte.MaxValue,
				179,
				12,
				30,
				136,
				16,
				77,
				175,
				19,
				11,
				88,
				86,
				88,
				25,
				88,
				55,
				21,
				249,
				235,
				236,
				152,
				203,
				157,
				204,
				253,
				24,
				241,
				71,
				9,
				27,
				227,
				123,
				56,
				40,
				158,
				14,
				155,
				31,
				159,
				149,
				218,
				157,
				97,
				117,
				242,
				31,
				160,
				61,
				162,
				153,
				189,
				178,
				29,
				14,
				105,
				202,
				188,
				115,
				27,
				229,
				235,
				15,
				231,
				251,
				43,
				123,
				178,
				53,
				5,
				143,
				245,
				181,
				154,
				59,
				18,
				173,
				161,
				164,
				140,
				247,
				144,
				102,
				136,
				23,
				214,
				31,
				147,
				132,
				16,
				174,
				242,
				239,
				42,
				122,
				95,
				65,
				123,
				92,
				128,
				210,
				94,
				26,
				253,
				219,
				16,
				118,
				147,
				188,
				139,
				213,
				230,
				178,
				80,
				245,
				81,
				155,
				3,
				226,
				83,
				155,
				168,
				176,
				177,
				55,
				213,
				37,
				102,
				69,
				8,
				129,
				32,
				15,
				136,
				97,
				174,
				187,
				245,
				68,
				245,
				132,
				158,
				118,
				39,
				21,
				116,
				23,
				198,
				183,
				143,
				224,
				45,
				55,
				92,
				248,
				82,
				49,
				50,
				63,
				250,
				68,
				127,
				239,
				36,
				61,
				91,
				89,
				249,
				253,
				80,
				80,
				202,
				160,
				54,
				77,
				98,
				217,
				68,
				13,
				105,
				166,
				239,
				43,
				206,
				204,
				194,
				163,
				188,
				245,
				162,
				28,
				238,
				119,
				69,
				228,
				51,
				240,
				87,
				32,
				191,
				46,
				7,
				134,
				43,
				149,
				187,
				58,
				252,
				4,
				60,
				69,
				63,
				0,
				52,
				11,
				54,
				187,
				75,
				193,
				15,
				149,
				24,
				195,
				217,
				250,
				54,
				66,
				202,
				150,
				170,
				236,
				122,
				46,
				136,
				130,
				60,
				29,
				152,
				148
			};
			SslSocket.s_log = new LogThreadHelper("SslSocket");
			string s;
			if (BattleNet.Client().GetMobileEnvironment() == constants.MobileEnv.PRODUCTION)
			{
				s = "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----";
			}
			else
			{
				s = "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----";
			}
			SslSocket.s_rootCertificate = new X509Certificate2(Encoding.ASCII.GetBytes(s));
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00084315 File Offset: 0x00082515
		public static void Process()
		{
			SslSocket.s_log.Process();
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00084321 File Offset: 0x00082521
		public bool Connected
		{
			get
			{
				return this.Socket != null && this.Socket.Connected;
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00084338 File Offset: 0x00082538
		public static string GetBundleStoragePath()
		{
			string text = BattleNet.Client().GetBasePersistentDataPath();
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text + "dlcertbundle";
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00084378 File Offset: 0x00082578
		public void BeginConnect(string address, uint port, SslCertBundleSettings bundleSettings, SslSocket.BeginConnectDelegate connectDelegate, int tryCount)
		{
			this.m_beginConnectDelegate = connectDelegate;
			this.m_bundleSettings = bundleSettings;
			this.m_connection.LogDebug = new Action<string>(SslSocket.s_log.LogDebug);
			this.m_connection.LogWarning = new Action<string>(SslSocket.s_log.LogWarning);
			this.m_connection.OnFailure = delegate()
			{
				this.ExecuteBeginConnectDelegate(true);
			};
			this.m_connection.OnSuccess = new Action(this.ConnectCallback);
			this.m_connection.Connect(address, port, tryCount);
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00084408 File Offset: 0x00082608
		public void BeginSend(byte[] bytes, SslSocket.BeginSendDelegate sendDelegate)
		{
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_canSend = false;
				this.m_sslStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.WriteCallback), sendDelegate);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call BeginWrite. {0}", new object[]
				{
					ex
				});
				if (sendDelegate != null)
				{
					sendDelegate(false);
				}
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00084488 File Offset: 0x00082688
		public void BeginReceive(byte[] buffer, int size, SslSocket.BeginReceiveDelegate beginReceiveDelegate)
		{
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_sslStream.BeginRead(buffer, 0, size, new AsyncCallback(this.ReadCallback), beginReceiveDelegate);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call BeginRead. {0}", new object[]
				{
					ex
				});
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000844FC File Offset: 0x000826FC
		public void Close()
		{
			SslStream sslStream = this.m_sslStream;
			this.m_sslStream = null;
			try
			{
				this.m_connection.Disconnect();
				if (sslStream != null)
				{
					sslStream.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00084540 File Offset: 0x00082740
		private void ConnectCallback()
		{
			try
			{
				this.ResolveSSLAddress();
				byte[] certBundleBytes;
				if (this.m_fileUtil.LoadFromDrive(SslSocket.GetBundleStoragePath(), out certBundleBytes))
				{
					this.m_bundleSettings.bundle = new SslCertBundle(certBundleBytes);
				}
				RemoteCertificateValidationCallback userCertificateValidationCallback = new RemoteCertificateValidationCallback(this.OnValidateServerCertificate);
				this.m_sslStream = new SslStream(new NetworkStream(this.Socket, true), false, userCertificateValidationCallback);
				SslSocket.SslStreamValidateContext sslStreamValidateContext = new SslSocket.SslStreamValidateContext();
				sslStreamValidateContext.m_sslSocket = this;
				SslSocket.s_streamValidationContexts.Add(this.m_sslStream, sslStreamValidateContext);
				this.m_sslStream.BeginAuthenticateAsClient(this.m_address, new AsyncCallback(this.OnAuthenticateAsClient), null);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogError("Exception while trying to authenticate. {0}", new object[]
				{
					ex
				});
				this.ExecuteBeginConnectDelegate(true);
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00084610 File Offset: 0x00082810
		private void ResolveSSLAddress()
		{
			string text;
			if (UriUtils.GetHostAddressAsIp(this.m_connection.Host, out text))
			{
				this.m_address = ((this.m_connection.ResolvedAddress.AddressFamily == AddressFamily.InterNetworkV6) ? this.m_connection.ResolvedAddress.ToString() : ("::ffff:" + this.m_connection.ResolvedAddress.ToString()));
			}
			else
			{
				this.m_address = this.m_connection.Host;
			}
			SslSocket.s_log.LogInfo("ResolveSSLAddress address: {0}", new object[]
			{
				this.m_address
			});
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000846A8 File Offset: 0x000828A8
		private void WriteCallback(IAsyncResult ar)
		{
			SslSocket.BeginSendDelegate beginSendDelegate = (SslSocket.BeginSendDelegate)ar.AsyncState;
			if (this.Socket == null || this.m_sslStream == null)
			{
				if (beginSendDelegate != null)
				{
					beginSendDelegate(false);
				}
				return;
			}
			try
			{
				this.m_sslStream.EndWrite(ar);
				this.m_canSend = true;
				if (beginSendDelegate != null)
				{
					beginSendDelegate(true);
				}
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call EndWrite. {0}", new object[]
				{
					ex
				});
				if (beginSendDelegate != null)
				{
					beginSendDelegate(false);
				}
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00084734 File Offset: 0x00082934
		private void ReadCallback(IAsyncResult ar)
		{
			SslSocket.BeginReceiveDelegate beginReceiveDelegate = (SslSocket.BeginReceiveDelegate)ar.AsyncState;
			if (this.Socket == null || this.m_sslStream == null)
			{
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
				return;
			}
			try
			{
				int bytesReceived = this.m_sslStream.EndRead(ar);
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(bytesReceived);
				}
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call EndRead. {0}", new object[]
				{
					ex
				});
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000847B8 File Offset: 0x000829B8
		private static SslSocket.HexStrToBytesError HexStrToBytes(string hex, out byte[] outBytes)
		{
			outBytes = null;
			int length = hex.Length;
			if (length % 2 == 1)
			{
				return SslSocket.HexStrToBytesError.ODD_NUMBER_OF_DIGITS;
			}
			outBytes = new byte[length / 2];
			int i = 0;
			int num = 0;
			while (i < length)
			{
				string value = hex.Substring(i, 2);
				outBytes[num] = Convert.ToByte(value, 16);
				i += 2;
				num++;
			}
			return SslSocket.HexStrToBytesError.OK;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0008480C File Offset: 0x00082A0C
		private static List<string> GetCommonNamesFromCertSubject(string certSubject)
		{
			List<string> list = new List<string>();
			char[] separator = new char[]
			{
				','
			};
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

		// Token: 0x0600255A RID: 9562 RVA: 0x0008486C File Offset: 0x00082A6C
		private bool GetBundleInfo(byte[] unsignedBundleBytes, out SslSocket.BundleInfo info)
		{
			info.bundleKeyHashs = new List<byte[]>();
			info.bundleUris = new List<string>();
			info.bundleCerts = new List<X509Certificate2>();
			string text = null;
			string @string = Encoding.ASCII.GetString(unsignedBundleBytes);
			try
			{
				SslSocket.CertBundle certBundle = this.m_jsonSerializer.Deserialize<SslSocket.CertBundle>(@string);
				foreach (SslSocket.CertBundle.PublicKey publicKey in certBundle.PublicKeys)
				{
					byte[] item = null;
					SslSocket.HexStrToBytesError hexStrToBytesError = SslSocket.HexStrToBytes(publicKey.ShaHashPublicKeyInfo, out item);
					if (hexStrToBytesError != SslSocket.HexStrToBytesError.OK)
					{
						text = EnumUtils.GetString<SslSocket.HexStrToBytesError>(hexStrToBytesError);
						break;
					}
					info.bundleKeyHashs.Add(item);
					info.bundleUris.Add(publicKey.Uri);
				}
				foreach (SslSocket.CertBundle.SigningCertificate signingCertificate in certBundle.SigningCertificates)
				{
					X509Certificate2 item2 = new X509Certificate2(Encoding.ASCII.GetBytes(signingCertificate.RawData));
					info.bundleCerts.Add(item2);
				}
			}
			catch (Exception ex)
			{
				text = ex.ToString();
			}
			if (text != null)
			{
				int num = 1024;
				string text2 = (@string.Length < num) ? @string : (@string.Substring(0, num) + " | .... [remaining output truncated]");
				SslSocket.s_log.LogWarning("Exception while trying to parse certificate bundle:\nerrorString={0}\ncertBundleString={1}", new object[]
				{
					text,
					text2
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000849C4 File Offset: 0x00082BC4
		private static bool IsAllowlistedInCertBundle(SslSocket.BundleInfo bundleInfo, string uri, byte[] publicKey)
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

		// Token: 0x0600255C RID: 9564 RVA: 0x00084A20 File Offset: 0x00082C20
		private static bool IsCertSignedByBlizzard(X509Certificate cert)
		{
			string issuer = cert.Issuer;
			char[] separator = new char[]
			{
				','
			};
			string[] array = issuer.Split(separator);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("CN=Battle.net Certificate Authority");
			foreach (string item in array)
			{
				hashSet.Remove(item);
			}
			return hashSet.Count == 0;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00084AA8 File Offset: 0x00082CA8
		private static byte[] GetUnsignedBundleBytes(byte[] signedBundleBytes)
		{
			int num = signedBundleBytes.Length - (SslSocket.s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			Array.Copy(signedBundleBytes, array, num);
			return array;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00084AE0 File Offset: 0x00082CE0
		private static bool VerifyBundleSignature(byte[] signedBundleData)
		{
			int num = signedBundleData.Length - (SslSocket.s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return false;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(SslSocket.s_magicBundleSignaturePreamble);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (signedBundleData[num + i] != bytes[i])
				{
					return false;
				}
			}
			SHA256 sha = SHA256.Create();
			sha.Initialize();
			sha.TransformBlock(signedBundleData, 0, num, null, 0);
			string s = "Blizzard Certificate Bundle";
			byte[] bytes2 = Encoding.ASCII.GetBytes(s);
			sha.TransformBlock(bytes2, 0, bytes2.Length, null, 0);
			sha.TransformFinalBlock(new byte[1], 0, 0);
			byte[] hash = sha.Hash;
			byte[] array = new byte[256];
			Array.Copy(signedBundleData, num + SslSocket.s_magicBundleSignaturePreamble.Length, array, 0, 256);
			List<RSAParameters> list = new List<RSAParameters>();
			list.Add(new RSAParameters
			{
				Modulus = SslSocket.s_standardPublicModulus,
				Exponent = SslSocket.s_standardPublicExponent
			});
			bool result = false;
			for (int j = 0; j < list.Count; j++)
			{
				if (SslSocket.VerifySignedHash(list[j], hash, array))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00084C10 File Offset: 0x00082E10
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
			byte[] array5 = new byte[]
			{
				48,
				49,
				48,
				13,
				6,
				9,
				96,
				134,
				72,
				1,
				101,
				3,
				4,
				2,
				1,
				5,
				0,
				4,
				32
			};
			if (!SslSocket.MakePKCS1SignatureBlock(hash, hash.Length, array5, array5.Length, array4, key.Modulus.Length))
			{
				return false;
			}
			byte[] array6 = new byte[array4.Length];
			Array.Copy(array4, array6, array4.Length);
			Array.Reverse(array6);
			return new BigInteger(array6).CompareTo(value) == 0;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00084D18 File Offset: 0x00082F18
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
			return num3 == signatureSize;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00084DB8 File Offset: 0x00082FB8
		private bool OnValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			SslSocket.CertValidationResult certValidationResult = this.IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
			if (certValidationResult == SslSocket.CertValidationResult.FAILED_CERT_BUNDLE)
			{
				SslSocket sslSocket = SslSocket.s_streamValidationContexts[this.m_sslStream].m_sslSocket;
				foreach (SslCertBundle sslCertBundle in SslSocket.DownloadCertBundles(sslSocket.m_bundleSettings.bundleDownloadConfig))
				{
					sslSocket.m_bundleSettings.bundle = sslCertBundle;
					certValidationResult = this.IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
					if (certValidationResult == SslSocket.CertValidationResult.OK)
					{
						this.m_fileUtil.StoreToDrive(sslCertBundle.CertBundleBytes, SslSocket.GetBundleStoragePath(), true, true);
						break;
					}
				}
			}
			return certValidationResult == SslSocket.CertValidationResult.OK;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00084E74 File Offset: 0x00083074
		private SslSocket.CertValidationResult IsServerCertificateValid(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			SslSocket sslSocket = SslSocket.s_streamValidationContexts[this.m_sslStream].m_sslSocket;
			SslCertBundleSettings bundleSettings = sslSocket.m_bundleSettings;
			if (bundleSettings.bundle == null || !bundleSettings.bundle.IsUsingCertBundle)
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			List<string> commonNamesFromCertSubject = SslSocket.GetCommonNamesFromCertSubject(certificate.Subject);
			SslSocket.BundleInfo bundleInfo = default(SslSocket.BundleInfo);
			byte[] unsignedBundleBytes = bundleSettings.bundle.CertBundleBytes;
			if (bundleSettings.bundle.isCertBundleSigned)
			{
				if (!SslSocket.VerifyBundleSignature(bundleSettings.bundle.CertBundleBytes))
				{
					return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
				}
				unsignedBundleBytes = SslSocket.GetUnsignedBundleBytes(bundleSettings.bundle.CertBundleBytes);
			}
			if (!this.GetBundleInfo(unsignedBundleBytes, out bundleInfo))
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag = false;
			byte[] publicKey = certificate.GetPublicKey();
			foreach (string uri in commonNamesFromCertSubject)
			{
				if (SslSocket.IsAllowlistedInCertBundle(bundleInfo, uri, publicKey))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag2 = SslSocket.IsCertSignedByBlizzard(certificate);
			bool flag3 = BattleNet.Client().GetRuntimeEnvironment() == constants.RuntimeEnvironment.Mono;
			bool flag4 = !flag2 && flag3 && chain.ChainElements.Count == 1;
			try
			{
				if (sslPolicyErrors != SslPolicyErrors.None)
				{
					SslPolicyErrors sslPolicyErrors2 = (flag2 || flag4) ? (SslPolicyErrors.RemoteCertificateNotAvailable | SslPolicyErrors.RemoteCertificateChainErrors) : SslPolicyErrors.None;
					if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != SslPolicyErrors.None && sslSocket.m_connection.MatchSslCertName(commonNamesFromCertSubject))
					{
						sslPolicyErrors2 |= SslPolicyErrors.RemoteCertificateNameMismatch;
					}
					if ((sslPolicyErrors & ~(sslPolicyErrors2 != SslPolicyErrors.None)) != SslPolicyErrors.None)
					{
						SslSocket.s_log.LogWarning("Failed policy check. sslPolicyErrors: {0}, expectedPolicyErrors: {1}", new object[]
						{
							sslPolicyErrors,
							sslPolicyErrors2
						});
						return SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				if (chain.ChainElements == null)
				{
					SslSocket.s_log.LogWarning("ChainElements is null");
					return SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
				}
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					SslSocket.s_log.LogDebug("Certificate Thumbprint: {0}", new object[]
					{
						x509ChainElement.Certificate.Thumbprint
					});
					foreach (X509ChainStatus x509ChainStatus in x509ChainElement.ChainElementStatus)
					{
						SslSocket.s_log.LogDebug("  Certificate Status: {0}", new object[]
						{
							x509ChainStatus.Status
						});
					}
				}
				bool flag5 = false;
				if (flag2)
				{
					if (chain.ChainElements.Count == 1)
					{
						chain.ChainPolicy.ExtraStore.Add(SslSocket.s_rootCertificate);
						chain.Build(new X509Certificate2(certificate));
						flag5 = true;
					}
				}
				else if (flag4 && bundleInfo.bundleCerts != null)
				{
					foreach (X509Certificate2 certificate2 in bundleInfo.bundleCerts)
					{
						chain.ChainPolicy.ExtraStore.Add(certificate2);
					}
					chain.Build(new X509Certificate2(certificate));
					flag5 = true;
				}
				for (int j = 0; j < chain.ChainElements.Count; j++)
				{
					if (chain.ChainElements[j] == null)
					{
						SslSocket.s_log.LogWarning("ChainElements[" + j + "] is null");
						return flag5 ? SslSocket.CertValidationResult.FAILED_CERT_BUNDLE : SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				if (flag2)
				{
					string text;
					if (BattleNet.Client().GetMobileEnvironment() == constants.MobileEnv.PRODUCTION)
					{
						text = "673D9D1072B625CAD95CB47BF0F0F512233E39FD";
					}
					else
					{
						text = "C0805E3CF51F1A56CE9E6E35CB4F4901B68128B7";
					}
					if (chain.ChainElements[1].Certificate.Thumbprint != text)
					{
						SslSocket.s_log.LogWarning("Root certificate thumb print check failure");
						SslSocket.s_log.LogWarning("  expected: {0}", new object[]
						{
							text
						});
						SslSocket.s_log.LogWarning("  received: {0}", new object[]
						{
							chain.ChainElements[1].Certificate.Thumbprint
						});
						return flag5 ? SslSocket.CertValidationResult.FAILED_CERT_BUNDLE : SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				for (int k = 0; k < chain.ChainElements.Count; k++)
				{
					if (DateTime.Now > chain.ChainElements[k].Certificate.NotAfter)
					{
						SslSocket.s_log.LogWarning("ChainElements[" + k + "] certificate is expired.");
						return flag5 ? SslSocket.CertValidationResult.FAILED_CERT_BUNDLE : SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				foreach (X509ChainElement x509ChainElement2 in chain.ChainElements)
				{
					foreach (X509ChainStatus x509ChainStatus2 in x509ChainElement2.ChainElementStatus)
					{
						if ((!flag2 && !flag5) || x509ChainStatus2.Status != X509ChainStatusFlags.UntrustedRoot)
						{
							SslSocket.s_log.LogWarning("Found unexpected chain error={0}.", new object[]
							{
								x509ChainStatus2.Status
							});
							return flag5 ? SslSocket.CertValidationResult.FAILED_CERT_BUNDLE : SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
						}
					}
				}
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to validate certificate. {0}", new object[]
				{
					ex
				});
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			return SslSocket.CertValidationResult.OK;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000853B8 File Offset: 0x000835B8
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
			using (List<string>.Enumerator enumerator = list.GetEnumerator())
			{
				UrlDownloadCompletedCallback <>9__0;
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					IUrlDownloader urlDownloader2 = urlDownloader;
					string url = text;
					UrlDownloadCompletedCallback cb;
					if ((cb = <>9__0) == null)
					{
						cb = (<>9__0 = delegate(bool success, byte[] bytes)
						{
							if (success)
							{
								SslCertBundle item = new SslCertBundle(bytes);
								dlBundles.Add(item);
							}
							int numDownloadsRemaining = numDownloadsRemaining;
							numDownloadsRemaining--;
						});
					}
					urlDownloader2.Download(url, cb, dlConfig);
				}
				goto IL_AD;
			}
			IL_A6:
			Thread.Sleep(15);
			IL_AD:
			if (numDownloadsRemaining <= 0)
			{
				return dlBundles;
			}
			goto IL_A6;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00085494 File Offset: 0x00083694
		private void OnAuthenticateAsClient(IAsyncResult ar)
		{
			bool connectFailed = false;
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_sslStream.EndAuthenticateAsClient(ar);
				SslSocket.s_log.LogDebug("Authentication completed IsEncrypted = {0}, IsSigned = {1}", new object[]
				{
					this.m_sslStream.IsEncrypted,
					this.m_sslStream.IsSigned
				});
			}
			catch (AuthenticationException ex)
			{
				SslSocket.s_log.LogError("Authentication Exception while ending client authentication. {0}.", new object[]
				{
					ex
				});
				connectFailed = true;
				if (this.m_fileUtil.RemoveFromDrive(SslSocket.GetBundleStoragePath()))
				{
					SslSocket.s_log.LogWarning("Removed saved cert bundles as they may be invalid.");
				}
				else
				{
					SslSocket.s_log.LogInfo("Saved cert bundles were not removed if they exist.");
				}
			}
			catch (Exception ex2)
			{
				SslSocket.s_log.LogError("Exception while ending client authentication. {0}", new object[]
				{
					ex2
				});
				connectFailed = true;
			}
			this.ExecuteBeginConnectDelegate(connectFailed);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00085590 File Offset: 0x00083790
		private void ExecuteBeginConnectDelegate(bool connectFailed)
		{
			this.m_bundleSettings = null;
			if (this.m_beginConnectDelegate == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (this.m_sslStream != null)
			{
				flag = this.m_sslStream.IsEncrypted;
				flag2 = this.m_sslStream.IsSigned;
			}
			this.m_beginConnectDelegate(connectFailed, flag, flag2);
			this.m_beginConnectDelegate = null;
			SslSocket.s_log.LogDebug("Connected={0} isEncrypted={1} isSigned={2}", new object[]
			{
				!connectFailed,
				flag,
				flag2
			});
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00085619 File Offset: 0x00083819
		private Socket Socket
		{
			get
			{
				return this.m_connection.Socket;
			}
		}

		// Token: 0x04000F91 RID: 3985
		private readonly IFileUtil m_fileUtil;

		// Token: 0x04000F92 RID: 3986
		private readonly IJsonSerializer m_jsonSerializer;

		// Token: 0x04000F93 RID: 3987
		private string m_address;

		// Token: 0x04000F94 RID: 3988
		public SslCertBundleSettings m_bundleSettings;

		// Token: 0x04000F95 RID: 3989
		private static Map<SslStream, SslSocket.SslStreamValidateContext> s_streamValidationContexts = new Map<SslStream, SslSocket.SslStreamValidateContext>();

		// Token: 0x04000F96 RID: 3990
		private static string s_magicBundleSignaturePreamble = "NGIS";

		// Token: 0x04000F97 RID: 3991
		private const int PUBKEY_MODULUS_SIZE_BITS = 2048;

		// Token: 0x04000F98 RID: 3992
		private const int PUBKEY_MODULUS_SIZE_BYTES = 256;

		// Token: 0x04000F99 RID: 3993
		private const int PUBKEY_EXP_SIZE_BYTES = 4;

		// Token: 0x04000F9A RID: 3994
		private static byte[] s_standardPublicExponent;

		// Token: 0x04000F9B RID: 3995
		private static byte[] s_standardPublicModulus;

		// Token: 0x04000F9C RID: 3996
		private static LogThreadHelper s_log;

		// Token: 0x04000F9D RID: 3997
		private TcpConnection m_connection = new TcpConnection();

		// Token: 0x04000F9E RID: 3998
		private SslStream m_sslStream;

		// Token: 0x04000F9F RID: 3999
		private SslSocket.BeginConnectDelegate m_beginConnectDelegate;

		// Token: 0x04000FA0 RID: 4000
		private static X509Certificate2 s_rootCertificate;

		// Token: 0x04000FA1 RID: 4001
		public bool m_canSend = true;

		// Token: 0x020006E4 RID: 1764
		private class SslStreamValidateContext
		{
			// Token: 0x0400227C RID: 8828
			public SslSocket m_sslSocket;
		}

		// Token: 0x020006E5 RID: 1765
		// (Invoke) Token: 0x06006338 RID: 25400
		public delegate void BeginConnectDelegate(bool connectFailed, bool isEncrypted, bool isSigned);

		// Token: 0x020006E6 RID: 1766
		// (Invoke) Token: 0x0600633C RID: 25404
		public delegate void BeginSendDelegate(bool wasSent);

		// Token: 0x020006E7 RID: 1767
		// (Invoke) Token: 0x06006340 RID: 25408
		public delegate void BeginReceiveDelegate(int bytesReceived);

		// Token: 0x020006E8 RID: 1768
		private enum HexStrToBytesError
		{
			// Token: 0x0400227E RID: 8830
			[Description("OK")]
			OK,
			// Token: 0x0400227F RID: 8831
			[Description("Hex string has an odd number of digits")]
			ODD_NUMBER_OF_DIGITS,
			// Token: 0x04002280 RID: 8832
			[Description("Unknown error parsing hex string")]
			UNKNOWN
		}

		// Token: 0x020006E9 RID: 1769
		private struct BundleInfo
		{
			// Token: 0x04002281 RID: 8833
			public List<byte[]> bundleKeyHashs;

			// Token: 0x04002282 RID: 8834
			public List<string> bundleUris;

			// Token: 0x04002283 RID: 8835
			public List<X509Certificate2> bundleCerts;
		}

		// Token: 0x020006EA RID: 1770
		[Serializable]
		private class CertBundle
		{
			// Token: 0x04002284 RID: 8836
			public SslSocket.CertBundle.PublicKey[] PublicKeys;

			// Token: 0x04002285 RID: 8837
			public SslSocket.CertBundle.SigningCertificate[] SigningCertificates;

			// Token: 0x0200070D RID: 1805
			[Serializable]
			public class PublicKey
			{
				// Token: 0x040022E8 RID: 8936
				public string Uri;

				// Token: 0x040022E9 RID: 8937
				public string ShaHashPublicKeyInfo;
			}

			// Token: 0x0200070E RID: 1806
			[Serializable]
			public class SigningCertificate
			{
				// Token: 0x040022EA RID: 8938
				public string RawData;
			}
		}

		// Token: 0x020006EB RID: 1771
		private enum CertValidationResult
		{
			// Token: 0x04002287 RID: 8839
			OK,
			// Token: 0x04002288 RID: 8840
			FAILED_SERVER_RESPONSE,
			// Token: 0x04002289 RID: 8841
			FAILED_CERT_BUNDLE
		}
	}
}
