using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
///MyDBUtils 的摘要说明
/// </summary>
public class MyDBUtils
{
	public MyDBUtils()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static ArrayList DataTable2ArrayList(DataTable data)
    {
        var array = new ArrayList();
        for (var i = 0; i < data.Rows.Count; i++)
        {
            var row = data.Rows[i];

            var record = new Hashtable();
            for (var j = 0; j < data.Columns.Count; j++)
            {
                var cellValue = row[j];
                if (cellValue.GetType() == typeof(DBNull))
                {
                    cellValue = null;
                }
                record[data.Columns[j].ColumnName] = cellValue;
            }
            array.Add(record);
        }
        return array;
    }

}