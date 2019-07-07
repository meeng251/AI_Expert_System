using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ExpertSystem.DAO;

namespace ExpertSystem
{
    class Rule_xl
    {
        //Connect cn = new Connect();
        DataProvider provider = new DataProvider();

        public void Insert(Rule r)
        {
            String sql = "Insert into tblluat values('" + r.RuleID + "',N'" + r.Content + "')";
            //cn.Run(sql);
            provider.Run(sql);
        }

        public void Update(Rule r)
        {
            String sql = "Update tblluat set content = N'" + r.Content + "' where maluat = '" + r.RuleID + "' ";
            //cn.Run(sql);
            provider.Run(sql);
        }

        public void Deleted(String ruleID)
        {
            String sql = "Delete from tblluat where maluat = '" + ruleID + "' ";
            //cn.Run(sql);
            provider.Run(sql);
        }

        public DataTable Search(String ruleID)
        {
            String sql = "Select * from tbbluat where maluat ='" + ruleID + "'";
            //return cn.GetTable(sql);
            return provider.ExcuteQuery(sql);
        }

        public DataTable loadRule()
        {
            String sql = "Select * from tblluat";
            //return cn.GetTable(sql);
            return provider.ExcuteQuery(sql);
        }
    }
}
