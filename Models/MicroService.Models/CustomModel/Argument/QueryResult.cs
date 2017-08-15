using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;
using System.Collections;

namespace MicroService.Models
{
    [DataContract]
    public class QueryResult<Model>
    {
        //[DataMember]
        //public int PageIndex { get; set; }
        //[DataMember]
        //public int PageSize { get; set; }
        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public List<Model> ModelList { get; set; }
        public QueryResult()
        {
            this.ModelList = new List<Model>();
        }


    }

    public class TableQueryResult : QueryResult<DataTable>
    {
        public new DataTable ModelList { get; set; }
        public static TableQueryResult New(int totalCount, DataTable table)
        {
            return new TableQueryResult()
            {
                ModelList = table,
                TotalCount = totalCount

            };
        }
    }
}
