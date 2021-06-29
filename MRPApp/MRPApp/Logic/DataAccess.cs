using MRPAPP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    }
}
