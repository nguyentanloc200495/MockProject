using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockProject.DataBase;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace MockProject.Services
{
    public class ThongKeService
    {
        public static List<ChartThongKe> GetDataChartForDay(DateTime? Tungay, DateTime? Denngay)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.BILLs.AsNoTracking()
                   
                    .Where(x =>x.CreateDate >= Tungay.Value && x.CreateDate <= Denngay.Value).AsQueryable();
        return query.GroupBy(x => EntityFunctions.TruncateTime(x.CreateDate.Value))
                        .Select(
                            x =>
                                new ChartThongKe()
                                {
                                    Year = x.Key.Value.Year,
                                    Month = x.Key.Value.Month,
                                    Day = x.Key.Value.Day,
                                    Ban =x.Sum(y=>y.Amount),
                                   
                                }).ToList();

            }

        }

        public static ChartTotalThongKe GetDataChartTotalThongKe(DateTime? Tungay, DateTime? Denngay)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var model = new ChartTotalThongKe();
                var db = context.BILLs.AsNoTracking()
                    .Where(x => x.CreateDate >= Tungay.Value && x.CreateDate <= Denngay.Value).AsQueryable();
                model.Ban = db.Sum(y => y.Amount);
                return model;
            }
        }

        public static List<ChartThongKe> GetDataChartForMonth(DateTime? fromDate, DateTime? toDate)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.BILLs.AsNoTracking()
                    
                    .Where(x =>x.CreateDate >= fromDate.Value && x.CreateDate <= toDate.Value).AsQueryable();

                return query.GroupBy(x => new { x.CreateDate.Value.Year, x.CreateDate.Value.Month })
                        .Select(
                            x =>
                                new ChartThongKe()
                                {
                                    Year = x.Key.Year,
                                    Month = x.Key.Month,
                                    Day = 1,
                                    Ban = x.Sum(y => y.Amount),
                                }).ToList();
            }

        }

        public static List<ChartThongKe> GetDataChartForYear(DateTime? fromDate, DateTime? toDate)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.BILLs.AsNoTracking()
                    .Where(x =>x.CreateDate >= fromDate.Value && x.CreateDate <= toDate.Value).AsQueryable();
                return query.GroupBy(x => x.CreateDate.Value.Year)
                        .Select(
                            x =>
                                new ChartThongKe()
                                {
                                    Year = x.Key,
                                    Month = 1,
                                    Day = 1,
                                    Ban = x.Sum(y => y.Amount),
                                }).ToList();
            }
        }
    }


   


    public class ChartThongKe
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public decimal? Ban { get; set; }
       
    }
    public class ChartTotalThongKe
    {
        public decimal? Ban { get; set; }
        
    }
}
