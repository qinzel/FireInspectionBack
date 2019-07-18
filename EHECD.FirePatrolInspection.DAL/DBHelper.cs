using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Globalization;
using System.Web.Caching;
using Dapper;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 数据库操作组件
    /// </summary>
    public class DBHelper
    {
        private static readonly DbProviderFactory fact =
            DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["DBProvider"] ?? "System.Data.SqlClient");


        private static readonly string defaultConnString = "DefaultConn".ReadConnectString();

        /// <summary>
        /// 根据链接字符串，创建数据库链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection(string connString)
        {
            IDbConnection conn = fact.CreateConnection();
            conn.ConnectionString = connString;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }


        /// <summary>
        /// 获取Cache依赖Command
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static SqlCommand GetCacheDependencyCommand(string commandText)
        {
            return new SqlCommand(commandText);
        }

        /// <summary>
        /// 根据模型获取全部数据
        /// 前提：表名必须和实体类名一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> QueryAll<T>()
        {
            return Query<T>("Select * From " + typeof (T).Name);
        }

        /// <summary>
        /// 根据sql获取相关数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return Query<T>(defaultConnString, sql, param);
        }

        /// <summary>
        /// 根据sql获取相关数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string connString, string sql, object param = null)
        {
            using (var conn = GetConnection(connString))
            {
                return conn.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// 返回用于缓存的查询结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryForCache<T>(string sql, out SqlCacheDependency dependency)
        {
            return QueryForCache<T>(defaultConnString, sql, out dependency);
        }

        /// <summary>
        /// 返回用于缓存的查询结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryForCache<T>(string connString, string sql, out SqlCacheDependency dependency)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                dependency = new SqlCacheDependency(sqlCmd);
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dt);
                return dt.List<T>();
            }
        }

        /// <summary>
        /// 返回用于缓存的查询结果
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="dependency"></param>
        public static void QueryForCache<T1, T2>(string sql, IEnumerable<T1> TResult1, IEnumerable<T2> TResult2,
            out SqlCacheDependency dependency)
        {
            QueryForCache<T1, T2>(defaultConnString, sql, TResult1, TResult2, out dependency);
        }

        /// <summary>
        /// 返回用于缓存的查询结果
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="dependency"></param>
        public static void QueryForCache<T1, T2>(string connString, string sql, IEnumerable<T1> TResult1,
            IEnumerable<T2> TResult2, out SqlCacheDependency dependency)
        {
            DataSet ds = new DataSet();
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                dependency = new SqlCacheDependency(sqlCmd);
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(ds);
                TResult1.AsList().AddRange(ds.Tables[0].List<T1>());
                TResult2.AsList().AddRange(ds.Tables[1].List<T2>());
            }
        }

        /// <summary>
        /// 根据主键获取单条数据记录
        /// 前提：表名必须和实体类名一致，并且主键名必须为ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(object primaryKey)
        {
            return QuerySingle<T>(string.Format("Select * From {0} Where ID = @ID", typeof (T).Name),
                new {ID = primaryKey});
        }

        /// <summary>
        /// 根据sql和参数获取单条数据记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(string sql, object param = null)
        {
            return QuerySingle<T>(defaultConnString, sql, param);
        }


        /// <summary>
        /// 根据sql和参数获取单条数据记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(string connString, string sql, object param = null)
        {
            using (var conn = GetConnection(connString))
            {
                return conn.QuerySingleOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 根据sql返回Datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable QueryAsTable(string sql, object param = null)
        {
            return QueryAsTable(defaultConnString, sql, param);
        }

        /// <summary>
        /// 根据sql返回Datatable
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable QueryAsTable(string connString, string sql, object param = null)
        {
            using (var conn = GetConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connString);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 执行数据库写操作并返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return Execute(defaultConnString, sql, param, commandType);
        }

        /// <summary>
        /// 执行数据库写操作并返回影响行数
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static int Execute(string connString, string sql, object param = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var conn = GetConnection(connString))
                {
                    return conn.Execute(sql, param, commandType: commandType);
                }
            }
            catch (Exception e)
            {
                TTracer.WriteLog(e.ToString());
                return 0;
            }

        }

        /// <summary>
        /// 利用事务执行数据库批量操作并返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteOnTransaction(string sql, object param = null,
            CommandType commandType = CommandType.Text)
        {
            return ExecuteOnTransaction(defaultConnString, sql, param, commandType);
        }


        /// <summary>
        /// 利用事务执行数据库批量操作并返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteOnTransaction(string connString, string sql, object param = null,
            CommandType commandType = CommandType.Text)
        {
            using (var conn = GetConnection(connString))
            {
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int iResult =  conn.Execute(sql, param, tran, commandType: commandType);
                        tran.Commit();
                        return iResult;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        TTracer.WriteLog(e.ToString());
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 将数据BULK的方式插入数据表中
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        public static void ExecuteBulkToTable(string tableName, DataTable dt)
        {
            SqlConnection sqlCon = new SqlConnection(defaultConnString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlCon);
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception e)
            {
                TTracer.WriteLog(e.ToString());
            }
            finally
            {
                sqlCon.Close();
                if(bulkCopy != null)
                {
                    bulkCopy.Close();
                }
            }
        }


    /// <summary>
        /// 执行数据库写操作并返回首行首列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return ExecuteScalar(defaultConnString, sql, param, commandType);
        }

        /// <summary>
        /// 执行数据库写操作并返回首行首列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connString, string sql, object param = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var conn = GetConnection(connString))
                {
                    return conn.ExecuteScalar(sql, param, commandType: commandType);
                }
            }
            catch (Exception e)
            {
                TTracer.WriteLog(e.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 利用事务执行数据库批量操作并返回首行首列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalarOnTransaction(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return ExecuteScalarOnTransaction(defaultConnString, sql, param, commandType);
        }

        /// <summary>
        /// 利用事务执行数据库批量操作并返回首行首列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalarOnTransaction(string connString, string sql, object param = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var conn = GetConnection(connString))
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        return conn.ExecuteScalar(sql, param, tran, commandType: commandType);
                    }
                }
            }
            catch (Exception e)
            {
                TTracer.WriteLog(e.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 单表写入操作
        /// 前提：表名和实体类名一致，数据库字段名称和实体类属性名一致，并且主键名为ID（自增长）,并且插入时间字段为dInsertTime或者dCreateTime（数据库有默认值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public static int  Insert<T>(T entity)
        {
            string sql = "Insert Into [{0}]({1}) Values ({2})";
            StringBuilder fieldName = new StringBuilder();
            StringBuilder fieldParam = new StringBuilder();
            foreach (var property in typeof (T).GetProperties())
            {
                if (property.Name.ToLower() == "id" || property.Name.ToLower() == "dinserttime" || property.Name.ToLower() == "dcreatetime")
                {
                    continue;
                }
                fieldName.Append(string.Format("[{0}]",property.Name));
                fieldName.Append(",");

                fieldParam.Append("@");
                fieldParam.Append(string.Format("{0}", property.Name));
                fieldParam.Append(",");
            }


            return Execute(string.Format(sql, typeof (T).Name, fieldName.ToString().TrimEnd(','),
                fieldParam.ToString().TrimEnd(',')), entity);
        }


        /// <summary>
        /// 根据多条查询语句，处理不同数据列表
        /// <example>
        ///    helper.Query(conn,"select sName,sControllerName from EHECD_Menu;select sActionName from EHECD_MenuAction");
        ///   action例子：
        ///   s => {
        ///        var menulist =s.Read<EHECD_Menu>().ToList();
        ///        var actionList = s.Read<EHECD_MenuAction>().ToList();
        ///    });
        /// </example>
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="action">数据处理</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void QueryMultiple( string sql, Action<SqlMapper.GridReader> action,
            object param = null)
        {
            QueryMultiple(defaultConnString, sql, action, param);
        }

        /// <summary>
        /// 根据多条查询语句，处理不同数据列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="action">数据处理</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void QueryMultiple(string connString,string sql, Action<SqlMapper.GridReader> action,
            object param = null)
        {
            using (var conn = GetConnection(connString))
            {
                var dapper = conn.QueryMultiple(sql, param);
                action(dapper);
            }
        }



        //删除查询关键字SELECT或select
        // string Sql = string.Format(@" 
        // WITH t AS (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS IDX, {1}) 
        // SELECT (SELECT MAX(IDX) FROM t) AS TOTAL,* 
        // FROM t WHERE IDX > {2} {3}",sSort,sSelectSql, start, limit == -1 ? "" : " AND IDX <= " + (limit + start));

        /// <summary>
        /// 执行Sql语句 带有dapper 参数化，分页查询结果集 
        /// </summary>
        /// <code>
        /// 示例：
        /// //查询条件
        /// long iTotal = 0;
        /// int iPageSize = 10,iPageIndex = 1;
        /// string sSql = "SELECT * FROM EHECD_Client",sSort="ID ASC";
        /// //string sSql = "* FROM EHECD_Client",sSort="ID ASC";
        /// //执行分页查询
        /// var data = DBHelper.QueryRunSqlByPager<object>(sSql, sSort, iPageSize, iPageIndex, ref iTotal);
        /// </code>
        /// <typeparam name="T">查询对象,可以Object</typeparam>
        /// <param name="strSql">执行查询语句 正常查询语句 如 SELECT * FROM Table1 </param>
        /// <param name="sSort">排序字段可多字段排序  如 ID DESC,dAddTime ASC 或者 dAddTime DESC</param>
        /// <param name="iPageSize">分页大小</param>
        /// <param name="iPageIndex">页码</param> 
        /// <param name="iTotalcount">获取查询结果总记录数 iTotalcount传入值为0则查询总记录数 iTotalcount 不等于零则不查询总记录数 </param>
        /// <param name="parameters">执行带有dapper 参数化执行Sql语句的参数 DynamicParameters dp = new DynamicParameters(); 累加参数 </param>
        /// <returns></returns>
        public static IEnumerable<T> QueryRunSqlByPager<T>(string strSql, int iPageIndex, int iPageSize,
            ref int iTotalcount, string sSort, object parameters = null)
        {
            return QueryRunSqlByPager<T>(defaultConnString, strSql, iPageIndex, iPageSize, ref iTotalcount, sSort,
                parameters);
        }

        /// <summary>
        /// 执行Sql语句 带有dapper 参数化，分页查询结果集 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conString"></param>
        /// <param name="strSql"></param>
        /// <param name="sSort"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iTotalcount"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryRunSqlByPager<T>(string conString, string strSql, int iPageIndex,
            int iPageSize, ref int iTotalcount, string sSort, object parameters = null)
        {

            int start = (iPageIndex - 1)*iPageSize;
            int limit = start + iPageSize;

            //删除查询关键字SELECT或select
            strSql = strSql.Trim();
            string sSelectSql = strSql.StartsWith("SELECT", true, CultureInfo.CurrentCulture)
                ? strSql.Substring(6)
                : strSql;

            string baseSql = string.Format(@" WITH t AS (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS IDX, {1}) ", sSort,
                sSelectSql);

            IEnumerable<T> datas = null;

            string runSql = string.Format(@" {0} SELECT * FROM t WHERE IDX > {1} {2} ;", baseSql, start,
                (limit == -1 ? "" : " AND IDX <= " + limit));
            int _iTotalcount = 0;
            if (iTotalcount == 0)
            {
                string totalSql = string.Format(@" {0} SELECT COUNT(0) FROM t ; ", baseSql);
                runSql = string.Format(" {0} {1} ", runSql, totalSql);
                QueryMultiple(runSql, s =>
                {
                    datas = s.Read<T>();
                    if (null != datas && datas.Count() > 0) //有数据的情况下才能对统计行数操作
                    {
                        _iTotalcount = s.Read<int>().FirstOrDefault();
                    }
                }, parameters);
            }
            else
            {
                using (var conn = GetConnection(conString))
                {
                    datas = conn.Query<T>(runSql, parameters);
                }
            }
            iTotalcount = _iTotalcount;

            return datas;
        }

    }
}
