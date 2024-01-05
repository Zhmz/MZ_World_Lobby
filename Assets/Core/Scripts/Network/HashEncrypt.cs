using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tuwan
{
    public class HashEncrypt
    {
        //private string strIN;
        //private bool isReturnNum;
        //private bool isCaseSensitive;

        /// <summary>
        /// 类初始化，此类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。
        /// </summary>
        /// <param name="IsCaseSensitive">是否区分大小写</param>
        /// <param name="IsReturnNum">是否返回为加密后字符的Byte代码</param>
        //public HashEncrypt( bool IsReturnNum)
        //{
        //    this.isCaseSensitive = IsCaseSensitive;
        //}

        //private string getstrIN(string strIN)
        //{
        //    //string strIN = strIN;
        //    if (strIN.Length == 0)
        //    {
        //        strIN = "~NULL~";
        //    }
        //    if (isCaseSensitive == false)
        //    {
        //        strIN = strIN.ToUpper();
        //    }
        //    return strIN;
        //}


        public static readonly byte[] iv = new byte[] { 5, 11, 7, 230, 15, 99, 137, 64, 87, 174, 63, 89, 75, 61, 27, 89 };

        public static readonly byte[] iv_string = new byte[] { 2, 88, 51, 204, 15, 99, 137, 243, 82, 127, 63, 89, 35, 65, 23, 49 };

        public static string MD5Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(strIN));
            md5.Clear();

            return GetStringValue(tmpByte);

        }


        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static string GetMD5HashFromFile(byte[] file)
        {
            try
            {
                //FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                // file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }


        public static string SHA1Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(strIN));
            sha1.Clear();

            return GetStringValue(tmpByte);

        }

        public static string SHA256Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();

            return GetStringValue(tmpByte);

        }


        public static string SHA512Encrypt(string strIN)
        {
            //strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(strIN));
            sha512.Clear();

            return GetStringValue(tmpByte);

        }


        public static string AESEncrypt(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = iv_string;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string AESDecrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = iv_string;

            ICryptoTransform cTransform = rDel.CreateDecryptor();

            byte[] resultArray = new byte[1];

            try
            {

                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch
            {

            }

            return UTF8Encoding.UTF8.GetString(resultArray);
        }




        public static byte[] AESEncrypt(byte[] toEncryptArray, byte[] keyArray)
        {

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = iv;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return resultArray;
        }

        public static byte[] AESDecrypt(byte[] toEncryptArray, byte[] keyArray)
        {
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;

            rDel.IV = iv;

            ICryptoTransform cTransform = rDel.CreateDecryptor();

            byte[] resultArray = new byte[1];

            try
            {

                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch
            {

            }

            return resultArray;
        }




        /// <summary>
        /// 使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string originalValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(originalValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return ToBase64String(ms.ToArray());
        }

        public static string DESEncrypt(string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }

        /// <summary>
        /// 使用DES解密
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">m初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();

            byt = FromBase64String(encryptedValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        public static string DESDecrypt(string encryptedValue, string key)
        {
            return DESDecrypt(encryptedValue, key, key);
        }

        public static string GetStringValue(byte[] Byte)
        {
            string tmpString = "";
            //if (this.isReturnNum == true)
            //{
            //    ASCIIEncoding Asc = new ASCIIEncoding();
            //    tmpString = Asc.GetString(Byte);
            //}
            //else
            //{
            int iCounter;
            for (iCounter = 0; iCounter < Byte.Length; iCounter++)
            {
                tmpString = tmpString + Byte[iCounter].ToString("x2");
            }
            //}
            return tmpString;
        }

        private static byte[] GetKeyByteArray(string strKey)
        {

            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen];

            //tmpByte = Asc.GetBytes(strKey);

            tmpByte = UTF8Encoding.UTF8.GetBytes(strKey);

            return tmpByte;

        }

        public static byte[] HexToByte(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                hexString = "00";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string ToBase64String(byte[] token)
        {
            return ToBase64String(token, 0, token.Length);
        }

        public static string ToBase64String(byte[] token, int oft, int len)
        {
            return Convert.ToBase64String(token, oft, len).Replace('+', '-').Replace('/', '_');
        }

        public static byte[] FromBase64String(string str)
        {
            return Convert.FromBase64String(str.Replace('-', '+').Replace('_', '/'));
        }


        public static string RSAEncrypt(string content)
        {
            string publickey = @"<RSAKeyValue><Modulus>1iIVik8WnAbQgUv+5I9DEwqfrPz/VmVCarIwINgtxhlezzXEaCLWJe7ckI4YBf3enqY3502UtegZJ4V1iiTWqUurMUEJmnuZ2cg+lIGX2u+gS4ckkus/riDDA14enaJJppmu5gMGTRIKgZNGSyoMNbYkVJZKPPAkN0xUqakqJuk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return ToBase64String(cipherbytes);
        }



        private static string PemToRSAKey(string pemKey, bool isPrivateKey)
        {
            string rsaKey = string.Empty;
            object pemObject = null;
            RSAParameters rsaPara = new RSAParameters();
            using (StringReader sReader = new StringReader(pemKey))
            {
                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sReader);
                pemObject = pemReader.ReadObject();
            }
            //RSA私钥
            if (isPrivateKey)
            {
                RsaPrivateCrtKeyParameters key = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)pemObject).Private;
                rsaPara = new RSAParameters
                {
                    Modulus = key.Modulus.ToByteArrayUnsigned(),
                    Exponent = key.PublicExponent.ToByteArrayUnsigned(),
                    D = key.Exponent.ToByteArrayUnsigned(),
                    P = key.P.ToByteArrayUnsigned(),
                    Q = key.Q.ToByteArrayUnsigned(),
                    DP = key.DP.ToByteArrayUnsigned(),
                    DQ = key.DQ.ToByteArrayUnsigned(),
                    InverseQ = key.QInv.ToByteArrayUnsigned(),
                };
            }
            //RSA公钥
            else
            {
                RsaKeyParameters key = (RsaKeyParameters)pemObject;
                rsaPara = new RSAParameters
                {
                    Modulus = key.Modulus.ToByteArrayUnsigned(),
                    Exponent = key.Exponent.ToByteArrayUnsigned(),
                };
            }
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaPara);
            using (StringWriter sw = new StringWriter())
            {
                sw.Write(rsa.ToXmlString(isPrivateKey ? true : false));
                rsaKey = sw.ToString();
            }
            return rsaKey;
        }

        public static string RSAEncrypt(string content, string key, bool isPrivateKey = false)  // source为要加密的字符串
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(PemToRSAKey(key, isPrivateKey));
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        //public static string RSAEncrypt(string content, string key)  // source为要加密的字符串
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    byte[] cipherbytes;
        //    rsa.FromXmlString(key);
        //    cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

        //    return Convert.ToBase64String(cipherbytes);
        //}


        public static byte[] RSAEncryptByte(string content)
        {
            string publickey = @"<RSAKeyValue><Modulus>1iIVik8WnAbQgUv+5I9DEwqfrPz/VmVCarIwINgtxhlezzXEaCLWJe7ckI4YBf3enqY3502UtegZJ4V1iiTWqUurMUEJmnuZ2cg+lIGX2u+gS4ckkus/riDDA14enaJJppmu5gMGTRIKgZNGSyoMNbYkVJZKPPAkN0xUqakqJuk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return cipherbytes;
        }


        public static string[] GenerateKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

    }
}
