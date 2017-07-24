using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：IOHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 17:12:46         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 17:12:46          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class IOHelper
    {
        /// <summary>
        /// 根据文件路径，创建文件对应的文件夹，若已存在则跳过
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreateDirectory(string filepath)
        {
            try
            {
                string dir = System.IO.Path.GetDirectoryName(filepath);
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);
                
            }
            catch (Exception exp)
            {
                throw new Exception("路径" + filepath, exp);
            }
        }

        /// <summary>
        /// 目录拷贝
        /// 不支持父子目录拷贝，否则出现死循环递归
        /// </summary>
        /// <param name="srcDir"></param>
        /// <param name="tgtDir"></param>
        public static bool CopyDirectory(string srcDir, string tgtDir, bool isOverWrite = true)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            bool hasCopyFile = false;

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                throw new Exception("来源目录不存在！");
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                string sourceFile = files[i].FullName;
                string toFile = target.FullName + @"\" + files[i].Name;
                //强制覆盖或者目标文件不存在或者hash指纹不匹配，则重新拷贝
                if (isOverWrite == true || !File.Exists(toFile) || !isValidFileContent(sourceFile, toFile))
                {
                    File.Copy(sourceFile, toFile, true);
                    hasCopyFile = true;
                }
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                var hasCopy = CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name, isOverWrite);
                if (hasCopy == true)
                    hasCopyFile = true;
            }
            return hasCopyFile;
        }

        /// <summary>
        /// 判断文件内容是否相同
        /// 哈市
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool isValidFileContent(string filePath1, string filePath2)
        {
            //创建一个哈希算法对象
            using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create())
            {
                using (FileStream file1 = new FileStream(filePath1, FileMode.Open), file2 = new FileStream(filePath2, FileMode.Open))
                {
                    byte[] hashByte1 = hash.ComputeHash(file1);//哈希算法根据文本得到哈希码的字节数组
                    byte[] hashByte2 = hash.ComputeHash(file2);
                    string str1 = BitConverter.ToString(hashByte1);//将字节数组装换为字符串
                    string str2 = BitConverter.ToString(hashByte2);
                    return (str1 == str2);//比较哈希码
                }
            }
        }

        /// <summary>
        /// 获取目录大小 递归
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            // 所有文件大小.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // 遍历出当前目录的所有文件夹.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);   //这就用到递归了，调用父方法,注意，这里并不是直接返回值，而是调用父返回来的
            }
            return (Size);
        }
    }
}
