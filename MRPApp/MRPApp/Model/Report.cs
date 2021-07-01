using System;

namespace MRPAPP.Model
{
    public class Report
    {
        // 조인쿼리로 만든 가상의 테이블 (실제 DB에는 없음)
        public int SchIdx { get; set; }
        public string PlantCode { get; set; }
        public Nullable<int> SchAmount { get; set; }
        public System.DateTime PrcDate { get; set; }
        public Nullable<int> PrcOKAmount { get; set; }
        public Nullable<int> PrcFailAmount { get; set; }
    }
}
