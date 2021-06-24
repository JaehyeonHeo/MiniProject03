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
        internal static List<Settings> GetSettings()
        {
            List<MRPAPP.Model.Settings> settings;

            using (var ctx = new MRPEntities())     // using을 사용하면 알아서 메모리 낭비를 막아준다.
                settings = ctx.Settings.ToList();

            return settings;
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
    }
}
