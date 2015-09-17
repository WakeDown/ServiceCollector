using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner
{
    class Helpers
    {
        public static string[] GetIpAdressFromRange(string fromIp, string toIp)
        {
            List<string> ips = new List<string>();
            IPAddress tmpIP = null;

            //generate only for valid set of ips
            //Todo: check for toip >fromIp
            if (IPAddress.TryParse(fromIp, out tmpIP) == true &&
            IPAddress.TryParse(toIp, out tmpIP) == true)
            {


                string[] fromOct = fromIp.Split('.');

                string[] toOct = toIp.Split('.');

                string a = null, b = null, c = null, d = null;

                int startA = Convert.ToInt32(fromOct[0]);
                int startB = Convert.ToInt32(fromOct[1]);
                int startC = 1;
                int startD = 1;

                int endA = Convert.ToInt32(toOct[0]);
                int endB = 255;
                int endC = 255;
                int endD = 255;


                for (int intA = startA; intA <= endA; intA++)
                {
                    a = intA.ToString();

                    startB = intA == Convert.ToInt32(fromOct[0]) ? Convert.ToInt32(fromOct[1]) : 1;
                    endB = intA == Convert.ToInt32(toOct[0]) ? Convert.ToInt32(toOct[1]) : 255;

                    for (int intB = startB; intB <= endB; intB++)
                    {
                        b = intB.ToString();

                        startC = (intA == Convert.ToInt32(fromOct[0])) && (intB == Convert.ToInt32(fromOct[1])) ?
                        Convert.ToInt32(fromOct[2]) : 1;
                        endC = (intA == Convert.ToInt32(toOct[0])) && (intB == Convert.ToInt32(toOct[1])) ?
                        Convert.ToInt32(toOct[2]) : 255;


                        for (int intC = startC; intC <= endC; intC++)
                        {
                            c = intC.ToString();

                            startD = (intA == Convert.ToInt32(fromOct[0])) && (intB == Convert.ToInt32(fromOct[1])) && (intC == Convert.ToInt32(fromOct[2])) ?
                            Convert.ToInt32(fromOct[3]) : 1;
                            endD = (intA == Convert.ToInt32(toOct[0])) && (intB == Convert.ToInt32(toOct[1])) && (intC == Convert.ToInt32(toOct[2])) ?
                            Convert.ToInt32(toOct[3]) : 255;


                            for (int intD = startD; intD <= endD; intD++)
                            {
                                d = intD.ToString();
                                ips.Add(a + "." + b + "." + c + "." + d);
                                //Console.WriteLine(a + "." + b + "." + c + "." + d);
                            }
                        }
                    }
                }


            }
            return ips.ToArray();

        }
        private static Int32 ConvertIPv4AddressToInt32(IPAddress ipAddr)
        {
            Byte[] ipBytes = ipAddr.GetAddressBytes();

            Int32 ipInteger = (((Int32)ipBytes[0]) << 24) + (((Int32)ipBytes[1]) << 16) + (((Int32)ipBytes[2]) << 8) + ((Int32)ipBytes[3]);

            return ipInteger;
        }

        private static IPAddress ConvertInt32ToIPv4Address(Int32 ipInteger)
        {
            Byte[] ipBytes = new Byte[4];

            ipBytes[0] = (Byte)((ipInteger >> 24) & 0xFF);
            ipBytes[1] = (Byte)((ipInteger >> 16) & 0xFF);
            ipBytes[2] = (Byte)((ipInteger >> 8) & 0xFF);
            ipBytes[3] = (Byte)(ipInteger & 0xFF);

            return new IPAddress(ipBytes);
        }

        private static IPAddress IncrementIPAddress(IPAddress ipAddr)
        {
            Int32 ipInteger = ConvertIPv4AddressToInt32(ipAddr);

            ipInteger++;

            return ConvertInt32ToIPv4Address(ipInteger);
        }

        public byte[] EncryptFile(string text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.GenerateIV();
            byte[] iv = aes.IV;
            aes.GenerateKey();
            byte[] key = aes.Key;
            ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(text);
            byte[] enryptText = memoryStream.ToArray();
            streamWriter.Dispose();
            cryptoStream.Dispose();
            memoryStream.Dispose();

            return enryptText;
        }

        public string DecryptFile(byte[] data)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.GenerateIV();
            byte[] iv = aes.IV;
            aes.GenerateKey();
            byte[] key = aes.Key;
            ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter(cryptoStream);
            memoryStream = new MemoryStream(data);
            cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            string decryptText = streamReader.ReadToEnd();
            streamWriter.Dispose();
            cryptoStream.Dispose();
            memoryStream.Dispose();

            return decryptText;
        }
    }
}
