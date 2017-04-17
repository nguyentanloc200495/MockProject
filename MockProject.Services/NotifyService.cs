using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockProject.DataBase;
using System.Data.Entity;

namespace MockProject.Services
{
    public class NotifyService
    {
        public static PagedSearchList<NOTIFY> Search(string Title,DateTime? FromDate,DateTime? ToDate,Notifi_Type? type ,Notifi_Status? status, int pageIndex)
        {

            using (var context = new GST_MockProjectEntities())

            {

                var query = context.NOTIFies.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(Title))
                {
                    query = query.Where(x => x.Title.Contains(Title));
                }
           
                if (FromDate.HasValue)
                {
                    query = query.Where(x => x.TimeCreate >= FromDate);
                }
                if (ToDate.HasValue)
                {
                    ToDate = ToDate.Value.AddDays(1);
                    query = query.AsNoTracking().Where(x => x.TimeCreate < ToDate);
                }
                if (type.HasValue)
                {
                    query = query.Where(x => x.Type == type.Value);
                }
                if (status.HasValue)
                {
                    query = query.Where(x => x.Status == status.Value);
                }

                query = query.AsNoTracking().OrderByDescending(x => x.TimeCreate);

                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<NOTIFY>(query, pageIndex, pageSize);
            }
        }

        public static NOTIFY GetById(int id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.NOTIFies.Include(x => x.USER).AsNoTracking().AsQueryable();


                return query.FirstOrDefault(x => x.ID == id);
            }
        }




        //phần thông báo của nhân viên

        public static PagedSearchList<NOTIFY> Search2(string Title, DateTime? FromDate, DateTime? ToDate, Notifi_Type? type, int pageIndex)
        {

            using (var context = new GST_MockProjectEntities())

            {
                var userID = UserService.GetUserInfo().ID;

                var query = context.NOTIFies.AsNoTracking().AsQueryable();
         
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    query = query.Where(x => x.Title.Contains(Title));
                }

                if (FromDate.HasValue)
                {
                    query = query.Where(x => x.TimeCreate >= FromDate);
                }
                if (ToDate.HasValue)
                {
                    ToDate = ToDate.Value.AddDays(1);
                    query = query.AsNoTracking().Where(x => x.TimeCreate < ToDate);
                }
                if (type.HasValue)
                {
                    query = query.Where(x => x.Type == type.Value);
                }
              
                query = query.AsNoTracking().Where(x=>x.UserCreate==userID)
                    .OrderByDescending(x => x.TimeCreate);

                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<NOTIFY>(query, pageIndex, pageSize);
            }
        }


        public static CommandResult Create(NOTIFY c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.UserCreate = UserService.GetUserInfo().ID;
                c.TimeCreate=DateTime.Now;
                c.Status=Notifi_Status.ChuaXem;
                context.NOTIFies.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        public static void ChangeStatus()
        {
            using (var context = new GST_MockProjectEntities())
            {
                var model = context.NOTIFies.Where(x=>x.Status == Notifi_Status.ChuaXem);
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        item.Status = Notifi_Status.DaxXem;
                    }
                    context.SaveChanges();
                }

            }
        }

        public static void ChangeStatusNotifyId(long? id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var model = context.NOTIFies.Where(x => x.ID == id).FirstOrDefault();
                if (model != null)
                {
                    model.Status = Notifi_Status.AnXem;
                    context.SaveChanges();
                }

            }
        }


        public static List<NOTIFY> GetNotify()
        {
            using (var context = new GST_MockProjectEntities())
            { 
                var model = context.NOTIFies.AsNoTracking()
                    .Where(x => x.Status != Notifi_Status.AnXem).OrderByDescending(x => x.TimeCreate).ToList();
                return model;
            }
        }
    }
}
