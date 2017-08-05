using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using System.Reflection;
using System.IO;

namespace Midea.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return objectToJson(obj);
        }

        /// <summary>
        /// Json转对象
        /// </summary>
        /// <param name="T"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static object JsonToObject(Type T, string jsonString)
        {
            object _t;
            if (T == typeof(DataTable))
            {
                _t = (object)JsonToDataTable(jsonString);
            }
            else if (T.FullName.StartsWith("System.Collections.Generic.Dictionary`2"))
            {
                _t = JsonToDictionary(T, jsonString);
            }
            else if (T.FullName.StartsWith("System.Collections.Generic.List`1"))
            {
                _t = JsonToList(T, jsonString);
            }
            else if (T.IsSubclassOf(typeof(System.ValueType)) || T == typeof(System.ValueType))
            {
                _t = T.InvokeMember("Parse", BindingFlags.InvokeMethod, null, T, new object[] { jsonString });
            }
            else if (T.FullName.StartsWith("System.String"))
            {
                _t = jsonString;
            }
            else
            {
                try
                {
                    _t = Jayrock.Json.Conversion.JsonConvert.Import(T, jsonString);
                    if (_t.ToString().Trim() != jsonString.Trim())
                    {
                        _t = jsonString.Trim();
                    }
                    else
                    {
                        if (_t is JsonNumber)
                        {
                            _t = ((JsonNumber)_t).ToDouble();
                        }
                        else if (_t is JsonBoolean)
                        {
                            _t = Convert.ToBoolean(_t.ToString());
                        }
                        else if (_t is JsonString || _t is string)
                        {
                            _t = jsonString.Trim();
                        }
                    }
                }
                catch
                {
                    _t = jsonString;
                }

            }

            return _t;

        }

        private static string objectToJson(object obj)
        {
            if (obj is System.ValueType || obj is string)//认为是值类型
            {
                if (obj != null)
                    return obj.ToString();
                else
                    return "";
            }
            else
            {
                Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
                Jayrock.Json.Conversion.JsonConvert.Export(enableJson(obj), writer);
                string str = writer.ToString();
                return str;
            }
        }

        public static string ObjectToJson<T>(T _t)
        {
            return objectToJson(_t);
        }

        public static T JsonToObject<T>(string jsonString)
        {
            T _t = (T)JsonToObject(typeof(T), jsonString);

            return _t;
        }

        /// <summary>
        /// 将对象中包含的Entity转成可完全输出成Json串的对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object enableJson(object obj)
        {
            if (obj is System.ValueType || obj is string || obj is DataTable || obj is DataSet)//认为是值类型
            {
                return obj;
            }
            else if (obj is IDictionary)
            {
                object[] ieKeys = new object[((IDictionary)obj).Keys.Count];
                ((IDictionary)obj).Keys.CopyTo(ieKeys, 0);
                Dictionary<object, object> newDic = new Dictionary<object, object>();
                foreach (object key in ieKeys)
                {
                    newDic[key] = enableJson(((IDictionary)obj)[key]);
                }
                return newDic;
            }
            else if (obj is IList)
            {
                object[] objArray = new object[((IList)obj).Count];
                for (int n = 0; n < ((IList)obj).Count; n++)
                {
                    objArray[n] = enableJson(((IList)obj)[n]);
                }
                return objArray;
            }
            else if (obj != null)
            {
                System.Reflection.PropertyInfo[] propertys = obj.GetType().GetProperties();
                if (propertys.Length > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    object objValue = null;
                    foreach (System.Reflection.PropertyInfo pi in propertys)
                    {
                        // 属性与字段名称一致的进行赋值 
                        objValue = pi.GetValue(obj, null);
                        dic.Add(pi.Name, enableJson(objValue));
                    }
                    return dic;
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 实体转成JSON串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_t"></param>
        /// <returns></returns>
        public static string EntityToJson<T>(T _t)
        {
            System.Reflection.PropertyInfo[] propertys = _t.GetType().GetProperties();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            object objValue = null;
            foreach (System.Reflection.PropertyInfo pi in propertys)
            {
                if (pi.Name.ToLower() == "dirty")
                {
                    break;
                }
                // 属性与字段名称一致的进行赋值 
                objValue = pi.GetValue(_t, null);

                dic.Add(pi.Name, objValue);



            }
            JsonObject jo = new JsonObject(dic);
            return jo.ToString();

        }

        /// <summary>
        /// JSON串转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonToEntity<T>(string jsonString)
        {
            T _t = (T)Jayrock.Json.Conversion.JsonConvert.Import(typeof(T), jsonString);

            return _t;

        }

        /// <summary>
        /// JSON串转Dictionary
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static object JsonToDictionary(Type dictionaryType, string jsonString)
        {
            object _t = dictionaryType.Assembly.CreateInstance(dictionaryType.FullName);
            Type[] types = dictionaryType.GetGenericArguments();
            JsonObject objJson = (JsonObject)JsonConvert.Import(jsonString);
            foreach (var pair in objJson)
            {
                object name, value;
                if (types[0] == typeof(string))
                {
                    name = pair.Name;
                }
                else
                {
                    name = JsonHelper.JsonToObject(types[0], pair.Name);
                }
                if (pair.Value == null || types[1] == pair.Value.GetType())
                {
                    value = pair.Value;
                }
                else
                {
                    value = JsonHelper.JsonToObject(types[1], pair.Value.ToString());
                }
                _t.GetType().GetMethod("Add").Invoke(_t, new object[] { name, value });
            }

            return _t;
        }

        /// <summary>
        /// JSON串转List
        /// </summary>
        /// <param name="listType">List 的Type</param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static object JsonToList(Type listType, string jsonString)
        {
            object _t = listType.Assembly.CreateInstance(listType.FullName);
            Type[] types = listType.GetGenericArguments();
            if (types.Length == 1)
            {
                JsonArray arrJson = (JsonArray)JsonConvert.Import(jsonString);
                foreach (var arr in arrJson)
                {
                    _t.GetType().GetMethod("Add").Invoke(_t, new object[] { JsonHelper.JsonToObject(types[0], arr.ToString()) });
                }
            }

            return _t;
        }

        /// <summary>
        /// JSON串转DataTable
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string jsonString)
        {
            DataTable table = new DataTable();
            JsonArray arrJson = (JsonArray)JsonConvert.Import(jsonString);
            foreach (var itemRow in arrJson)
            {
                JsonObject jsonRow = (JsonObject)JsonConvert.Import(itemRow.ToString());
                DataRow row = table.NewRow();
                foreach (var pair in jsonRow)
                {
                    if (!table.Columns.Contains(pair.Name))
                    {
                        if (pair.Value is JsonBoolean)
                        {
                            table.Columns.Add(pair.Name, typeof(Boolean));
                        }
                        else if (pair.Value is JsonNumber)
                        {
                            //DateTime dt = ((JsonNumber)pair.Value).ToDateTime();
                            //if(dt > Convert.ToDateTime(
                            table.Columns.Add(pair.Name, typeof(Decimal));
                        }
                        else if (pair.Value is JsonNull)
                        {
                            table.Columns.Add(pair.Name, typeof(Nullable));
                        }
                        else
                        {
                            table.Columns.Add(pair.Name, typeof(string));
                        }
                    }
                    //填充Row中的值
                    if (table.Columns[pair.Name].DataType == typeof(Boolean))
                    {
                        row[pair.Name] = Convert.ToBoolean(pair.Value);
                    }
                    else if (table.Columns[pair.Name].DataType == typeof(Decimal))
                    {
                        row[pair.Name] = Convert.ToDecimal(pair.Value);
                    }
                    else
                    {
                        row[pair.Name] = Convert.ToString(pair.Value);
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary> 
        /// DataSet转换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="p_DataSet">DataSet</param> 
        /// <param name="p_TableIndex">待转换数据表索引</param> 
        /// <returns></returns> 
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            // 返回值初始化 
            IList<T> result = new List<T>();

            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return result;

            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return result;

            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];

            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        string columnName = p_Data.Columns[i].ColumnName.ToUpper();
                        if (pi.Name.ToUpper().Equals(columnName) || pi.Name.ToUpper().Equals(columnName.Replace("_", string.Empty)))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value && p_Data.Rows[j][i] != null)
                            {
                                if (pi.PropertyType == typeof(Boolean))
                                {
                                    pi.SetValue(_t, Convert.ToBoolean(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Int16))
                                {
                                    pi.SetValue(_t, Convert.ToInt16(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Int32))
                                {
                                    pi.SetValue(_t, Convert.ToInt32(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Decimal))
                                {
                                    pi.SetValue(_t, Convert.ToDecimal(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(Int32?))
                                {
                                    if (!string.IsNullOrEmpty(p_Data.Rows[j][i].ToString()))
                                        pi.SetValue(_t, Convert.ToInt32(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(DateTime))
                                {
                                    pi.SetValue(_t, Convert.ToDateTime(p_Data.Rows[j][i]), null);
                                }
                                else if (pi.PropertyType == typeof(DateTime?))
                                {
                                    if (!string.IsNullOrEmpty(p_Data.Rows[j][i].ToString()))
                                        pi.SetValue(_t, Convert.ToDateTime(p_Data.Rows[j][i]), null);
                                    
                                }
                                else if (pi.PropertyType == typeof(Decimal?))
                                {
                                    if (!string.IsNullOrEmpty(p_Data.Rows[j][i].ToString()))
                                        pi.SetValue(_t, Convert.ToDecimal(p_Data.Rows[j][i]), null);

                                }
                                else
                                {
                                    pi.SetValue(_t, p_Data.Rows[j][i], null);
                                }
                            }
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }

            return result;
        }

        #region GetPagedTable DataTable分页
        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从0开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {

            DataTable newdt = dt.Copy();
            newdt.Clear();

            int rowbegin = PageIndex * PageSize;
            int rowend = (PageIndex + 1) * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;

            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                newdt.ImportRow(dt.Rows[i]);
            }

            return newdt;
        }
        #endregion

        /// <summary>
        /// list转datatable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable convert2Table<T>(List<T> list)
        {
            DataTable table = new DataTable();

            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                List<string> columns = new List<string>();
                foreach (PropertyInfo pi in properties)
                {
                    table.Columns.Add(pi.Name);
                    columns.Add(pi.Name);
                }
                foreach (T item in list)
                {
                    object[] cells = getValues(columns, item);
                    table.Rows.Add(cells);
                }
            }
            return table;

        }

        private static object[] getValues(List<string> columns, object instance)
        {
            object[] ret = new object[columns.Count];

            for (int n = 0; n < ret.Length; n++)
            {
                try
                {
                    PropertyInfo pi = instance.GetType().GetProperty(columns[n]);
                    object value = pi.GetValue(instance, null);
                    ret[n] = value;
                }
                catch
                {
                    ret[n] = "";
                }
            }
            return ret;
        }



        /// <summary>
        /// 将DataSet转化成JSON数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        /// <remarks>
        ///  Author:tina 2012-4-1
        /// </remarks>
        public static string DataSetToJson(DataSet ds)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            string json = string.Empty;

            if (ds.Tables.Count == 0)
                throw new Exception("DataSet中Tables为0");
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string tableName = "T" + i.ToString();
                object[] rows = new object[ds.Tables[i].Rows.Count];


                for (int j = 0; j < rows.Length; j++)
                {
                    Dictionary<string, object> dicRow = new Dictionary<string, object>();
                    DataRow row = ds.Tables[i].Rows[j];
                    foreach (DataColumn col in ds.Tables[i].Columns)
                    {
                        string value = row[col.ColumnName] == DBNull.Value ? "" : Convert.ToString(row[col.ColumnName]);
                        dicRow.Add(col.ColumnName, value);
                    }
                    rows[j] = dicRow;

                }

                dic.Add(tableName, rows);
            }

            JsonObject jo = new JsonObject(dic);
            json = jo.ToString();
            return json;

        }

        /// <summary>
        /// 将JSON解析成DataSet只限标准的JSON数据
        /// 例如：Json＝{t0:[{name:'数据name',type:'数据type'}]} 或 Json＝{t1:[{name:'数据name',type:'数据type'}],t2:[{id:'数据id',gx:'数据gx',val:'数据val'}]}
        /// </summary>
        /// <param name="Json">Json字符串</param>
        /// <returns>DataSet</returns>
        /// <remarks>Author:Tina 2012-4-1</remarks>
        public static DataSet JsonToDataSet(string Json)
        {

            try
            {
                DataSet ds = new DataSet();
                JsonObject objJson = (JsonObject)JsonConvert.Import(Json);
                foreach (var item in objJson)
                {
                    DataTable dt = new DataTable(item.Name);
                    if (item.Value.GetType() == typeof(JsonArray))
                    {
                        foreach (var itemRow in ((JsonArray)item.Value))
                        {
                            JsonObject jsonRow = (JsonObject)JsonConvert.Import(itemRow.ToString());
                            DataRow dr = dt.NewRow();
                            foreach (var pair in jsonRow)
                            {
                                if (!dt.Columns.Contains(pair.Name))
                                {
                                    dt.Columns.Add(pair.Name);
                                }
                                dr[pair.Name] = Convert.ToString(pair.Value);
                            }
                            dt.Rows.Add(dr);
                        }
                    }

                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 将DataTable转化成JSON数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <remarks>
        ///  Author:Crystal 2012-10-29
        /// </remarks>
        public static string DataTableToJson(DataTable dt)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string json = string.Empty;


            string tableName = dt.TableName;
            object[] rows = new object[dt.Rows.Count];


            for (int j = 0; j < rows.Length; j++)
            {
                Dictionary<string, object> dicRow = new Dictionary<string, object>();
                DataRow row = dt.Rows[j];
                foreach (DataColumn col in dt.Columns)
                {
                    string value = row[col.ColumnName] == DBNull.Value ? "" : Convert.ToString(row[col.ColumnName]);
                    dicRow.Add(col.ColumnName, value);
                }
                rows[j] = dicRow;
            }

            dic.Add(tableName, rows);

            JsonObject jo = new JsonObject(dic);
            json = jo.ToString();
            return json;
        }
    }
}
