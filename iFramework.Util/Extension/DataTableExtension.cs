using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace iFramework.Util
{
    public static class  DataTableExtension
    {
        /// <summary>
        /// 拷贝行值，注意：目标行和被拷贝行不能为空，且其拥有表也不能为空.
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="SourceRow"></param>
        /// <returns></returns>
        public static void CopyFrom(this DataRow Row,DataRow SourceRow)
        {
            if (Row == null || Row.Table == null ||
                SourceRow==null || SourceRow.Table==null)
            {
                throw new Exception("Row is null or the table of row is null!");
            }
            foreach(DataColumn theCol in Row.Table.Columns)
            {
                if (SourceRow.Table.Columns.Contains(theCol.ColumnName))
                {
                    Row[theCol.ColumnName] = SourceRow[theCol.ColumnName];
                }
            }
        }
    }
}
