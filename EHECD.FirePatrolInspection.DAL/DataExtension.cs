using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EHECD.FirePatrolInspection.DAL
{
    public static class DataExtension
    {
        /// <summary>
        /// 返回数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> List<T>(this DataTable dt)
        {
            var list = new List<T>();
            if (dt.Rows.Count > 0)
            {
                var plist = new List<PropertyInfo>(typeof (T).GetProperties());

                foreach (DataRow item in dt.Rows)
                {
                    T t = System.Activator.CreateInstance<T>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        PropertyInfo info =
                            plist.Find(
                                p =>
                                    p.Name.ToUpper().Replace("_", string.Empty) ==
                                    dt.Columns[i].ColumnName.ToUpper().Replace("_", string.Empty));
                        if (info == null) continue;
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(t, item[i], null);
                        }
                    }
                    list.Add(t);
                }
            }
            return list;
        }

        /// <summary>
        /// 返回单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T SingleOrDefault<T>(this DataRow dr)
        {
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            T t = System.Activator.CreateInstance<T>();
            if (dr != null)
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo info =
                        plist.Find(
                            p =>
                                p.Name.ToUpper().Replace("_", string.Empty) ==
                                dr.Table.Columns[i].ColumnName.ToUpper().Replace("_", string.Empty));
                    if (info == null) continue;
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        info.SetValue(t, dr[i], null);
                    }
                }
            }
            return t;
        }
    }
}
