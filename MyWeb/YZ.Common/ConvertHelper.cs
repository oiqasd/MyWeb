using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
   public static class ConvertHelper
    {
        #region TryPrase数据类型转换
        public static int TryPraseInt(string inValue, int defaultValue = default(int))
        {

            int ret = defaultValue;
            int.TryParse(inValue, out ret);
            return ret;
        }
        public static decimal TryPraseDecimal(string inValue, decimal defaultValue = default(decimal))
        {
            decimal ret = defaultValue;
            decimal.TryParse(inValue, out ret);
            return ret;
        }
        public static DateTime TryPraseDateTime(string inValue, DateTime defaultValue = default(DateTime))
        {
            DateTime ret = defaultValue;
            DateTime.TryParse(inValue, out ret);
            return ret;
        }

        //public static string ConvertDtToString(DateTime dateTime, DtType dtType = DtType.Type1)
        //{
        //    switch (dtType)
        //    {
        //        case DtType.Type1:
        //            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        //        case DtType.Type2:
        //            return dateTime.ToString("yyyy-MM-dd");
        //        case DtType.Type3:
        //            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //        case DtType.Type4:
        //            return dateTime.ToString("yyyy年MM月dd日");
        //        case DtType.Type5:
        //            return dateTime.ToString("yyyy年MM月dd日HH时mm分ss秒");
        //    }
        //    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        //}

        #endregion

        /// <summary>
        /// 返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(object param, T defaultValue = default(T))
        {
            try
            {
                return (T)Convert.ChangeType(param, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 	Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///     If the <paramref name="value"/> can't be convert even if the types are 
        ///     convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo_old<T>(object value)
        {
            return ConvertTo(value, default(T));
        }

        /// <summary>
        /// 	Converts an object to the specified target type or returns the default value.
        ///     <para>Any exceptions are ignored. </para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertToAndIgnoreException<T>(object value)
        {
            return ConvertToAndIgnoreException(value, default(T));
        }

        /// <summary>
        /// 	Converts an object to the specified target type or returns the default value.
        ///     <para>Any exceptions are ignored. </para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertToAndIgnoreException<T>(object value, T defaultValue)
        {
            return ConvertTo(value, defaultValue, true);
        }

        /// <summary>
        /// 	Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///     If the <paramref name="value"/> can't be convert even if the types are 
        ///     convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo_old<T>(object value, T defaultValue)
        {
            try
            {
                if (value != null)
                {
                    var targetType = typeof(T);

                    if (value.GetType() == targetType) return (T)value;

                    var converter = TypeDescriptor.GetConverter(value);
                    if (converter != null)
                    {
                        if (converter.CanConvertTo(targetType))
                            return (T)converter.ConvertTo(value, targetType);
                    }
                    return defaultValue;

                    //converter = TypeDescriptor.GetConverter(targetType);
                    //if (converter != null)
                    //{
                    //    if (converter.CanConvertFrom(value.GetType()))
                    //        return (T)converter.ConvertFrom(value);
                    //}
                }
            }
            catch (Exception fe)
            {
                throw new Exception(string.Format("ConvertTo：{0}【{1}】转换成【{2}】失败", fe.Message, value, typeof(T).Name));
            }
            return defaultValue;
        }

        /// <summary>
        /// 	Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>Any exceptions are optionally ignored (<paramref name="ignoreException"/>).</para>
        ///     <para>
        ///     If the exceptions are not ignored and the <paramref name="value"/> can't be convert even if 
        ///     the types are convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <param name = "ignoreException">if set to <c>true</c> ignore any exception.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(object value, T defaultValue, bool ignoreException)
        {
            if (ignoreException)
            {
                try
                {
                    return ConvertTo<T>(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return ConvertTo<T>(value);
        }

        /// <summary>
        /// 	Determines whether the value can (in theory) be converted to the specified target type.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can be convert to the specified target type; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanConvertTo<T>(object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return true;
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                        return true;
                }
            }
            return false;
        }


        #region DataType Convert
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 0)
                return null;

            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取T的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建T的实例
                T ob = (T)Activator.CreateInstance(t);
                //new T();
                //找到对应的数据  并赋值
                //lamda表达式：  (参数名)=>{方法体};  调用：  委托变量（参数值）;
                prlist.ForEach(p =>
                {
                    if (row[p.Name] != DBNull.Value)
                    {
                        switch (p.PropertyType.ToString())
                        {
                            case "System.String"://字符串类型
                                p.SetValue(ob, row[p.Name].ToString(), null);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(row[p.Name].ToString(), out dateV);
                                p.SetValue(ob, dateV, null);
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(row[p.Name].ToString(), out boolV);
                                p.SetValue(ob, boolV, null);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(row[p.Name].ToString(), out intV);
                                p.SetValue(ob, intV, null);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(row[p.Name].ToString(), out doubV);
                                p.SetValue(ob, doubV, null);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        p.SetValue(ob, null, null);
                    }
                });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// List转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> value)
        {
            if (value == null)
                return null;

            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in value)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>  
        /// 将泛型集合类转换成DataTable  
        /// </summary>  
        /// <typeparam name="T">集合项类型</typeparam>  
        /// <param name="list">集合</param>  
        /// <param name="propertyName">需要返回的列的列名</param>  
        /// <returns>数据集(表)</returns>  
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();

            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            DataTable result = new DataTable();

            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// DataSet装换为泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableIndex">待转换数据表索引</param>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo p in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值
                        if (p.Name.ToLower().Equals(p_Data.Columns[i].ColumnName.Replace("_", "").ToLower()))
                        {
                            // 数据库NULL值单独处理
                            if (p_Data.Rows[j][i] != DBNull.Value)
                            {
                                switch (p.PropertyType.ToString())
                                {
                                    case "System.String"://字符串类型
                                        p.SetValue(p_Data, p_Data.Rows[j][i].ToString(), null);
                                        break;
                                    case "System.DateTime"://日期类型
                                        DateTime dateV;
                                        DateTime.TryParse(p_Data.Rows[j][i].ToString(), out dateV);
                                        p.SetValue(p_Data, dateV, null);
                                        break;
                                    case "System.Boolean"://布尔型
                                        bool boolV = false;
                                        bool.TryParse(p_Data.Rows[j][i].ToString(), out boolV);
                                        p.SetValue(p_Data, boolV, null);
                                        break;
                                    case "System.Int16"://整型
                                    case "System.Int32":
                                    case "System.Int64":
                                    case "System.Byte":
                                        int intV = 0;
                                        int.TryParse(p_Data.Rows[j][i].ToString(), out intV);
                                        p.SetValue(p_Data, intV, null);
                                        break;
                                    case "System.Decimal"://浮点型
                                    case "System.Double":
                                        double doubV = 0;
                                        double.TryParse(p_Data.Rows[j][i].ToString(), out doubV);
                                        p.SetValue(p_Data, doubV, null);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            //pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                            {
                                p.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary>
        /// DataSet装换为泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableName">待转换数据表名称</param>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }

        /// <summary>   
        /// 把一个一维数组转换为DataTable   
        /// </summary>   
        /// <param name="ColumnName">列名</param>   
        /// <param name="Array">一维数组</param>   
        /// <example>
        /// <code>
        /// Convert("columnName", new string[] { "1", "2", "3", "4", "5", "6" });
        /// </code>
        /// </example>
        /// <returns>返回DataTable</returns>
        public static DataTable ConvertToDataTable(string ColumnName, string[] Array)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(ColumnName, typeof(string));

            for (int i = 0; i < Array.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr[ColumnName] = Array[i].ToString();
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>   
        /// 反一个M行N列的二维数组转换为DataTable   
        /// </summary>   
        /// <param name="ColumnNames">一维数组，代表列名，不能有重复值</param>   
        /// <param name="Arrays">M行N列的二维数组</param>
        ///  <example>
        /// <code>
        /// string[,] array3D = {    
        ///                        { "1", "数组转DataTable 1", "0"},    
        ///                     { "2", "数组转DataTable 2", "1"},    
        ///                     { "3", "数组转DataTable 3", "1"},    
        ///                     { "4", "数组转DataTable 4", "2"},    
        ///                     { "5", "数组转DataTable 5", "2"},    
        ///                     { "6", "数组转DataTable 6", "5"},    
        ///                     };   
        ///   Convert(new string[] { "haha1", "haha2", "haha3" }, array3D); 
        /// </code>
        /// </example>
        /// <returns>返回DataTable</returns>
        public static DataTable ConvertToDataTable(string[] ColumnNames, string[,] Arrays)
        {
            DataTable dt = new DataTable();

            foreach (string ColumnName in ColumnNames)
            {
                dt.Columns.Add(ColumnName, typeof(string));
            }

            for (int i = 0; i < Arrays.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnNames.Length; j++)
                {
                    dr[j] = Arrays[i, j].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;

        }

        /// <summary>   
        /// 反一个M行N列的二维数组转换为DataTable   
        /// </summary>   
        /// <param name="Arrays">M行N列的二维数组</param>   
        /// <example>
        /// <code>
        /// string[,] array3D = {    
        ///                   { "1", "数组转DataTable 1", "0"},    
        ///                   { "2", "数组转DataTable 2", "1"},    
        ///                   { "3", "数组转DataTable 3", "1"},    
        ///                   { "4", "数组转DataTable 4", "2"},    
        ///                   { "5", "数组转DataTable 5", "2"},    
        ///                   { "6", "数组转DataTable 6", "5"},    
        ///                   };   
        ///  Convert(array3D);                 
        /// </code>
        /// </example>
        /// <returns>返回DataTable</returns> 
        public static DataTable ConvertToDataTable(string[,] Arrays)
        {
            DataTable dt = new DataTable();

            int a = Arrays.GetLength(0);
            for (int i = 0; i < Arrays.GetLength(1); i++)
            {
                dt.Columns.Add("col" + i.ToString(), typeof(string));
            }

            for (int i = 0; i < Arrays.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < Arrays.GetLength(1); j++)
                {
                    dr[j] = Arrays[i, j].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion
    }
}
