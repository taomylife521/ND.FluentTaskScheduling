using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

//**********************************************************************
//
// 文件名称(File Name)：ComputerHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 11:39:28         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 11:39:28          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class ComputerHelper
    {
    public string CpuID;
    public string MacAddress;
    public string DiskID;
    public string IpAddress;
    public string LoginUserName;
    public string ComputerName;
    public string SystemType;
    public string TotalPhysicalMemory; //单位：M 
    private static ComputerHelper _instance;
    public static ComputerHelper Instance()
    {
        if (_instance == null)
            _instance = new ComputerHelper();
        return _instance;
    }
    public ComputerHelper()
    {
        CpuID = GetCpuID();
        MacAddress = GetMacAddress();
        DiskID = GetDiskID();
        IpAddress = GetIPAddress();
        LoginUserName = GetUserName();
        SystemType = GetSystemType();
        TotalPhysicalMemory = GetTotalPhysicalMemory();
        ComputerName = GetComputerName();
    }
    #region  获取cpu序列号
    /// <summary>
    /// 获取cpu序列号
    /// </summary>
    /// <returns></returns>
    public string GetCpuID()
    {
        try
        {
            //获取CPU序列号代码 
            string cpuInfo = "";//cpu序列号 
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            moc = null;
            mc = null;
            return cpuInfo;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion

    #region GetMacAddress
    /// <summary>
    /// 获取物理地址
    /// </summary>
    /// <returns></returns>
    public string GetMacAddress()
    {
        try
        {
            //获取网卡硬件地址 
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            moc = null;
            mc = null;
            return mac;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion

    #region 获取ip地址
    /// <summary>
    /// 获取ip地址
    /// </summary>
    /// <returns></returns>
    public string GetIPAddress()
    {
        try
        {
            //获取IP地址 
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    //st=mo["IpAddress"].ToString(); 
                    System.Array ar;
                    ar = (System.Array)(mo.Properties["IpAddress"].Value);
                    st = ar.GetValue(0).ToString();
                    break;
                }
            }
            moc = null;
            mc = null;
            return st;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion

    #region 获取磁盘id
    /// <summary>
    /// 获取磁盘id
    /// </summary>
    /// <returns></returns>
    public string GetDiskID()
    {
        try
        {
            //获取硬盘ID 
            String HDid = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                HDid = (string)mo.Properties["Model"].Value;
            }
            moc = null;
            mc = null;
            return HDid;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion

    #region 操作系统的登录用户名
    /// <summary> 
    /// 操作系统的登录用户名 
    /// </summary> 
    /// <returns></returns> 
    public string GetUserName()
    {
        try
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {

                st = mo["UserName"].ToString();

            }
            moc = null;
            mc = null;
            return st;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion


    #region PC类型
    /// <summary> 
    /// PC类型 
    /// </summary> 
    /// <returns></returns> 
    public string GetSystemType()
    {
        try
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {

                st = mo["SystemType"].ToString();

            }
            moc = null;
            mc = null;
            return st;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }

    } 
    #endregion

    #region GetTotalPhysicalMemory
    /// <summary> 
    /// 物理内存 
    /// </summary> 
    /// <returns></returns> 
    public string GetTotalPhysicalMemory()
    {
        try
        {

            string st = "";
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {

                st = mo["TotalPhysicalMemory"].ToString();

            }
            moc = null;
            mc = null;
            return st;
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }
    } 
    #endregion

    #region 获取电脑名称
    /// <summary> 
    /// 获取电脑名称
    /// </summary> 
    /// <returns></returns> 
    public string GetComputerName()
    {
        try
        {
            return System.Environment.GetEnvironmentVariable("ComputerName");
        }
        catch
        {
            return "unknow";
        }
        finally
        {
        }
    } 
    #endregion

    }
}
