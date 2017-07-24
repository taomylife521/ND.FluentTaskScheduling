using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {

            using (FlightOrderDBEntities db = new FlightOrderDBEntities())
            {

                DateTime start = Convert.ToDateTime("2016-01-01");
                DateTime end = Convert.ToDateTime("2017-01-01");
                List<tb_orders> ordList=db.tb_orders.Where(x => x.createTime >= start && x.createTime < end && x.isOnline==4 && !x.remarkTravel.Contains("旅游")).Take(500).ToList();

                List<tb_orders> ordList2 = db.tb_orders.Where(x => x.createTime >= start && x.delDegree==1 && x.createTime < end && x.isOnline == 4 && x.remarkTravel.Contains("旅游")).ToList();
                StringBuilder strSql = new StringBuilder();
                foreach (var tbOrderse in ordList)
                {
                   tb_orders ord= weightRandom(ordList2);
                    ord.hotelName = tbOrderse.hotelName;
                    ord.incomeTotal = tbOrderse.incomeTotal;
                    ord.incomeFirst = tbOrderse.incomeFirst;
                    ord.expendFirst = tbOrderse.expendFirst;
                    ord.expendTotal = tbOrderse.expendTotal;
                    strSql.AppendLine("update dbo.tb_orders set delDegree=0 , hotename='" + tbOrderse.hotelName + "',incomeTotal='" + tbOrderse.incomeTotal + "',incomeFirst='" + tbOrderse.incomeFirst + "',expendFirst='" + tbOrderse.expendFirst + "',expendTotal='" + tbOrderse.expendTotal + "' where ordernum='" + ord.orderNum + "'");

                }
                string ss=ordList.Sum(x => x.incomeTotal).ToString();
                File.WriteAllText("e://1.txt",strSql.ToString());
                Console.WriteLine(ss);
                Console.ReadKey();
            }
        }

        #region 加权随机算法
        public static tb_orders weightRandom(List<tb_orders> list)
        {
            //獲取ip列表list


            int randomPos = random.Next(0,list.Count);
            return list[randomPos];
            
        }
        #endregion
    }
}
