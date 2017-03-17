using System;
using System.Linq;
using MockProject;
using MockProject.DataBase;


namespace MockProject.Services
{
    public class UserSevice
    {
   
        public static PagedSearchList<Account> Search(string userName, string fullName, string phoneNumber, int pageIndex)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())

            {
                var query = context.Accounts.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(fullName))
                {
                    query = query.Where(x => x.Fullname.Contains(fullName));
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    query = query.Where(x => x.Username.Contains(userName));
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    query = query.Where(x => x.PhoneNumber.Contains(phoneNumber));
                }
                query = query.OrderByDescending(x => x.Id);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Account>(query, pageIndex, pageSize);
            }
        }
        public static Account GetById(long id)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var query = context.Accounts.AsNoTracking().AsQueryable();
     

                return query.FirstOrDefault(x => x.Id == id);
            }
        }
        public static CommandResult Create(Account c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
               
                context.Accounts.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(Account c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var Account = context.Accounts.First(x => x.Id == c.Id);
              
                Account.Username = c.Username;
                Account.Fullname = c.Fullname;
                Account.Password = c.Password;
                Account.PhoneNumber = c.PhoneNumber;
                Account.Image = c.Image;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
