using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace PacmanGame
{
    public class RecordData
    {
        public int levelIndex;//已经通过的最高关卡索引0开始，-1没通过任何关卡
    }

    public class IOManager
    {

        string fileName = "amazinggame.txt";
        string filePath;
        string pKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        public RecordData recordData = null;


        public void Init()
        {
            if (Application.platform == RuntimePlatform.Android)
                filePath = Application.persistentDataPath;
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                filePath = Application.dataPath;
            else
                filePath = Application.dataPath;

            StreamReader sr = null;
            try
            {
                sr = File.OpenText(filePath + "/" + fileName);
            }
            catch (Exception e)
            {
                recordData = new RecordData();
                recordData.levelIndex = -1;
                return;
            }
            string str = sr.ReadToEnd();
            str = RijndaelDecrypt(str, pKey);
            JsonData jd = JsonMapper.ToObject(str);
            recordData = JsonMapper.ToObject<RecordData>(str);
            sr.Close();
            sr.Dispose();
        }

        public void FlushToFile()
        {
            StreamWriter sw;
            FileInfo f = new FileInfo(filePath + "/" + fileName);
            if (f.Exists)
            {
                sw = f.CreateText();
            }
            else
            {
                sw = f.CreateText();
            }
            string str = JsonMapper.ToJson(recordData);
            str = RijndaelEncrypt(str, pKey);
            sw.WriteLine(str);
            sw.Close();
            sw.Dispose();
        }


        /// Rijndael加密算法
        /// </summary>
        /// <param name="pString">待加密的明文</param>
        /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
        /// <param name="iv">iv向量,长度为128（byte[16])</param>
        /// <returns></returns>
        private static string RijndaelEncrypt(string pString, string pKey)
        {
            //密钥
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
            //待加密明文数组
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);

            //Rijndael解密算法
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();

            //返回加密后的密文
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// ijndael解密算法
        /// </summary>
        /// <param name="pString">待解密的密文</param>
        /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
        /// <param name="iv">iv向量,长度为128（byte[16])</param>
        /// <returns></returns>
        private static String RijndaelDecrypt(string pString, string pKey)
        {
            //解密密钥
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
            //待解密密文数组
            byte[] toEncryptArray = Convert.FromBase64String(pString);

            //Rijndael解密算法
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();

            //返回解密后的明文
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static IOManager _Instance = null;
        private IOManager() { }
        public static IOManager Instance
		{
            get
            {
                if (_Instance == null)
                    _Instance = new IOManager();
                return _Instance;
            }
		}
    } 

}


