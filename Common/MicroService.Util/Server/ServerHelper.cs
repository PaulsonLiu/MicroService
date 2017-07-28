using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MicroService.Util
{
    public class ServerHelper
    {
        public ServerHelper()
        {

        }
        public ServerHelper(string server, string database, string username, string password)
        {
            ServerUrl = server;
            DataBase = database;
            UserName = username;
            PassWord = password;
        }
        public string ServerUrl { get; set; }
        public string DataBase { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsCanConnect { get; set; }
        public string ConnectString {
            get
            {
                return GetConnectString(ServerUrl, DataBase, UserName, PassWord);
            }
            set
            {
                ServerUrl = Regex.Match(value, @"(?<=Server=).*(?=\;Database)").Value;
                DataBase = Regex.Match(value, @"(?<=Database=)\w+").Value;
                UserName = Regex.Match(value, @"(?<=User ID=)\w+").Value;
                PassWord = Regex.Match(value, @"(?<=Password=)\w+").Value;
            }
        }

        #region connection
        private string GetConnectString(string server, string database, string username, string password)
        {
            string str = string.Format("Server={0};Database={1};User ID={2};Password={3}", server, database, username, password);
            return str;
        }
        #endregion

        #region ConnectionTest
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public void ConnectionTest()
        {
            IsCanConnect = ConnectionTest(ConnectString);
        }
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        private bool ConnectionTest(string connectString)
        {
            SqlConnection mySqlConnection;
            bool IsCanConnectioned = false;

            //创建连接对象
            mySqlConnection = new SqlConnection(connectString);
            //ConnectionTimeout 在.net 1.x 可以设置 在.net 2.0后是只读属性，则需要在连接字符串设置
            //如：server=.;uid=sa;pwd=;database=PMIS;Integrated Security=SSPI; Connection Timeout=30
            //mySqlConnection.ConnectionTimeout = 1;//设置连接超时的时间
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                mySqlConnection.Close();
            }
            //mySqlConnection   is   a   SqlConnection   object 
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                //Connection   is   not   available  
                return IsCanConnectioned;
            }
            else
            {
                //Connection   is   available  
                return IsCanConnectioned;
            }
        }
        #endregion

    }
}
