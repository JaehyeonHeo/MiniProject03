using MRPAPP.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace MRPApp.Logic
{
    public class DataAccess
    {
        // settings 테이블에서 데이터 가져오기
        public static List<Settings> GetSettings()
        {
            List<MRPAPP.Model.Settings> list;

            using (var ctx = new MRPEntities())     // using을 사용하면 알아서 메모리 낭비를 막아준다.
                list = ctx.Settings.ToList();

            return list;
        }

        internal static int SetSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item); // INSERT or update

                return ctx.SaveChanges(); // commit
            }
        }

        internal static int DelSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                var obj = ctx.Settings.Find(item.BasicCode); // 검색한 실제 데이터를 찾아서,,,,
                ctx.Settings.Remove(obj);  // DELETE (삭제)
                return ctx.SaveChanges();
            }
        }

        // Schedules 테이블에서 데이터 가져오기
        internal static List<Schedules> GetSchedules()
        {
            List<MRPAPP.Model.Schedules> list;

            using (var ctx = new MRPEntities())     // using을 사용하면 알아서 메모리 낭비를 막아준다.
                list = ctx.Schedules.ToList();

            return list;
        }

        // Schedule 테이블에 데이터 입력/수정
        internal static int SetSchedule(Schedules item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item); // INSERT or update
                return ctx.SaveChanges(); // commit
            }
        }

        // Process 테이블에서 데이터 가져오기
        internal static List<Process> GetProcess()
        {
            List<MRPAPP.Model.Process> list;

            using (var ctx = new MRPEntities())
                list = ctx.Process.ToList(); //SELECT

            return list;
        }

        // Process 테이블에 데이터 입력/수정
        internal static int SetProcess(Process item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Process.AddOrUpdate(item);  // INSERT | UPDATE
                return ctx.SaveChanges();       // COMMIT
            }
        }

        // Report라는 가상의 테이블에서 데이터를 가져오기 위한 메서드
        internal static List<Report> GetReportDatas(string startDate, string endDate, string plantCode)
        {
            var connString = ConfigurationManager.ConnectionStrings["MRPConnString"].ToString();
            var list = new List<MRPAPP.Model.Report>(); // Model폴더에 Report클래스에서 가져와 list에 저장
            //var lastObj = new MRPAPP.Model.Report();    // 추가 : 최종 report값넣는 변수

            using (var conn = new SqlConnection(connString))
            {
                conn.Open(); // 중요 !! close()는 using문이 알아서 해준다
                // 조인 쿼리문 넣기!!
                var sqlQuery = $@"SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate,
                                           prc.PrcOKAmount, prc.PrcFailAmount
                                    FROM Schedules AS sch
                                   INNER JOIN (
	                                     SELECT smr.SchIdx, smr.PrcDate, 
		                                       SUM(smr.PrcOK) AS PrcOKAmount, SUM(smr.PrcFail) AS PrcFailAmount
	                                       FROM (
			                                    SELECT p.SchIdx, p.PrcDate, 
				                                       CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
				                                       CASE p.prcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
			                                      FROM Process AS p
		                                       ) AS smr
	                                    GROUP BY smr.SchIdx, smr.PrcDate
                                     ) AS prc
                                       ON sch.SchIdx = prc.SchIdx
                                    WHERE sch.PlantCode = '{plantCode}'
                                      AND prc.PrcDate BETWEEN '{startDate}' AND '{endDate}'";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var tmp = new Report
                    {
                        SchIdx = (int)reader["SchIdx"],
                        PlantCode = reader["PlantCode"].ToString(),
                        PrcDate = DateTime.Parse(reader["PrcDate"].ToString()),
                        SchAmount = (int)reader["SchAmount"],
                        PrcOKAmount = (int)reader["PrcOKAmount"],
                        PrcFailAmount = (int)reader["PrcFailAmount"]
                    };
                    list.Add(tmp);
                    //lastObj = tmp; // 마지막 값을 할당
                }

                // 시작일부터 종료일까지 없는 날짜값 만들어주는 로직


            }
            return list;
        }
    }
}
