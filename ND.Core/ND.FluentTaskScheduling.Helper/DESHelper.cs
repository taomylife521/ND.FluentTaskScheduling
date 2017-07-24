using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DESCryptionHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 15:17:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 15:17:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class DESHelper
    {
        
        /// <summary>
         /// 使用DES加密
         /// </summary>
         /// <param name="plain">明文</param>
         /// <param name="key">加密钥匙</param>
         /// <param name="iv">向量</param>
         /// <returns>返回密文</returns>
         public static string Encrypt(string plain, string key, string iv)
         {
             //把密钥转换成字节数组
             byte[] keyBytes = Encoding.ASCII.GetBytes(key);
 
             //把向量转换成字节数组
             byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
 
             //声明1个新的DES对象 
             DESCryptoServiceProvider des = new DESCryptoServiceProvider();
 
             //开辟一块内存流
             MemoryStream msEncrypt = new MemoryStream();
 
             //把内存流对象包装成加密流对象 
             CryptoStream csEncrypt = new CryptoStream(msEncrypt, des.CreateEncryptor(keyBytes, ivBytes), CryptoStreamMode.Write);
 
             //把加密流对象包装成写入流对象
             StreamWriter swEncrypt = new StreamWriter(csEncrypt);
 
             //写入流对象写入明文  
             swEncrypt.WriteLine(plain);
 
             //写入流关闭  
             swEncrypt.Close();
 
             //加密流关闭  
             csEncrypt.Close();
 
             //把内存流转换成字节数组，内存流现在已经是密文了  
             byte[] bytesCipher=msEncrypt.ToArray();
 
             //内存流关闭 
             msEncrypt.Close();
             //将字节数组转化成Base64字符串
             return Convert.ToBase64String(bytesCipher);
         }

         public static string Decrypt(string cipher, string key, string iv)
         {
             //将密文通过Base64位还原成字节数组
             byte[] cipherByte = Convert.FromBase64String(cipher);
 
             //把密钥转换成字节数组
             byte[] keyBytes = Encoding.ASCII.GetBytes(key);
 
             //把向量转换成字节数组
             byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
 
             //声明1个新的DES对象 
             DESCryptoServiceProvider des = new DESCryptoServiceProvider();
 
             //开辟一块内存流，并存放密文字节数组
             MemoryStream msDecrypt = new MemoryStream(cipherByte);
 
             //把内存流对象包装成解密流对象 
             CryptoStream csDecrypt = new CryptoStream(msDecrypt, des.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
 
             //把解密流对象包装成写入流对象
             StreamReader srDecrypt = new StreamReader(csDecrypt);
 
             //明文=读出流的读出内容   
             string strPlainText=srDecrypt.ReadLine();
 
             //读出流关闭  
             srDecrypt.Close();
 
             //解密流关闭  
             csDecrypt.Close();
 
             //内存流关闭  
             msDecrypt.Close();
 
             //返回明文  
             return strPlainText;
         }
    }
}
