using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using ExpertSystem.DAO;

namespace ExpertSystem
{
    class SuyDienTien
    {
        #region KhaiBao
        //private Connect cn = new Connect();
        private DataProvider cn = new DataProvider();
        List<RuleDefine> bin = new List<RuleDefine>();
        List<RuleDefine> SAT = new List<RuleDefine>();
        private int demluat = 0;
        #endregion

        public void readFile()
        {
            string qr = "Select noidung from tblluat";
            //DataTable tbLuat = cn.GetTable(qr);
            DataTable tbLuat = cn.ExcuteQuery(qr);
            for (int i=0; i<tbLuat.Rows.Count; i++)
            {
                string buff = tbLuat.Rows[i][0].ToString();
                RuleDefine luatTG = new RuleDefine();
                char[] delimiterChars = { '>' };
                string[] tg = buff.Split(delimiterChars);

                //left
                char[] delimiterChars1 = { '^' };
                string[] left = tg[0].Split(delimiterChars1);
                int j = 0;
                string buff1 = left[0];
                while (buff1 != null)
                {
                    luatTG.left.Add(buff1);
                    j++;
                    try
                    {
                        buff1 = left[j];
                    }
                    catch { buff1 = null; }
                }
                j = 0;

                //right
                char[] delimiterChars2 = { ',' };
                string[] right = tg[1].Split(delimiterChars2);

                buff1 = right[0];
                while(buff1 != null)
                {
                    luatTG.right.Add(buff1);
                    j++;
                    try
                    {
                        buff1 = right[j];
                    }
                    catch { buff1 = null; }
                }

                bin.Add(luatTG);
                demluat++;
            }
        }

        public string printRule(List<RuleDefine> mangluat)
        {
            string tg = "";
            foreach (RuleDefine r in mangluat)
            {
                foreach (string s in r.left)
                {
                    tg += s + "^";
                }
                tg += "->";
                foreach (string s in r.right)
                {
                    tg += s + "^";
                }
                tg += "\n";
            }
            return tg;
        }

        public bool CheckIn(List<string> a, List<string> b)
        {
            int cnt = 0;
            foreach (string tg1 in a)
            {
                foreach (string tg2 in b)
                {
                    if (tg1 == tg2)
                        cnt++;
                }
            }
            if (cnt == a.Count)
                return true;
            else
                return false;
        }

        public void TimTapSat(List<string> L,List<RuleDefine> mangluat)
        {
            foreach (RuleDefine lTG in mangluat)
            {
                if (CheckIn(lTG.left, L) == true && !SAT.Contains(lTG))
                {
                    SAT.Add(lTG);
                }
            }
        }

        public bool SuyDien(List<string> left, List<string> right)
        {
            List<RuleDefine> mangluat = new List<RuleDefine>();
            mangluat = bin;
            List<string> KL = right;
            List<string> TG = left;
            TimTapSat(TG, mangluat);

            while (SAT.Count > 0 && CheckIn(KL, TG) == false)
            {
                //Lấy luật r cuối cùng ra áp dụng
                RuleDefine r = SAT.ElementAt(0);
                mangluat.Remove(r);
                SAT.RemoveAt(0);
                //Thêm cái chưa có vào TG
                foreach (string tg in r.right)
                {
                    if (!TG.Contains(tg))
                    {
                        TG.Add(tg);
                        Console.WriteLine(tg);
                    }
                }

                TimTapSat(TG, mangluat);
            }

            if (CheckIn(KL, TG) == false)
                return false;
            else
                return true;
        }
    }
}
