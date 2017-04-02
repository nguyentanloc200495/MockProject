using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using CommonData;
using System.ComponentModel.DataAnnotations;
using MockProject.DataBase;

namespace MockProject
{
    public static class DbExtension
    {
        //public static void Log<T>(this DbContext context, T entity, LogType logType, int userId, string description, NameValueCollection rawData, bool saveChange = true) where T : class
        //{
        //    var data = JsonConvert.SerializeObject(NvcToDictionary(rawData, true));
        //    var rowId = typeof(T).Name + "-" + GetPrimaryKeyString(entity, context);
        //    using (var logContext = new GST_MockProjectEntities())
        //    {
        //        logContext.ActivityLogs.Add(new ActivityLog()
        //        { 
        //            ActivityLogType = (int)logType,
        //            Description = description,
        //            RawData = data,
        //            RowId = rowId,
        //            UserId = userId,
        //            CreatedOn = DateTime.Now,
        //        });
        //        if (saveChange)
        //        {
        //            logContext.SaveChanges();
        //        }
        //    }
        //}

        //public static void Log<T>(this DbContext context, T entity, LogType logType, int userId, string description, object objLog, bool saveChange = true) where T : class
        //{
        //    var rowId = typeof(T).Name + "-" + GetPrimaryKeyString(entity, context);
        //    using (var logContext = new CTGroupBOProductEntities())
        //    {
        //        logContext.ActivityLogs.Add(new ActivityLog()
        //        {
        //            ActivityLogType = (int)logType,
        //            Description = description,
        //            RawData = JsonConvert.SerializeObject(objLog, Newtonsoft.Json.Formatting.None,
        //                    new JsonSerializerSettings
        //                    {
        //                        NullValueHandling = NullValueHandling.Ignore,
        //                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //                        //PreserveReferencesHandling = PreserveReferencesHandling.None,
        //                        MissingMemberHandling = MissingMemberHandling.Ignore,
        //                    }),
        //            RowId = rowId,
        //            UserId = userId,
        //            CreatedOn = DateTime.Now,
        //        });
        //        if (saveChange)
        //        {
        //            logContext.SaveChanges();
        //        }
        //    }
        //}

        //public static void LogForType(this DbContext context, Type t, string id, LogType logType, int userId, string description)
        //{
        //    using (var logContext = new CTGroupBOProductEntities())
        //    {
        //        logContext.ActivityLogs.Add(new ActivityLog()
        //        {
        //            ActivityLogType = (int)logType,
        //            Description = description,
        //            RawData = "",
        //            RowId = t.Name + "-" + id,
        //            UserId = userId,
        //            CreatedOn = DateTime.Now,
        //        });
        //        logContext.SaveChanges();
        //    }
        //}


        //public static List<ActivityLog> GetLogs<T>(this DbContext context, T entity) where T : class
        //{
        //    var rowId = typeof(T).Name + "-" + GetPrimaryKeyString(entity, context);
        //    using (var logContext = new CTGroupBOProductEntities())
        //    {
        //        return logContext.ActivityLogs.Include(x => x.User).Where(x => x.RowId == rowId).OrderByDescending(x => x.CreatedOn).ToList();
        //    }
        //}

        //public static List<ActivityLog> GetLogsById<T>(object id) where T : class
        //{
        //    var rowId = typeof(T).Name + "-" + id;
        //    using (var logContext = new CTGroupBOProductEntities())
        //    {
        //        return logContext.ActivityLogs.Include(x=>x.User).Where(x => x.RowId == rowId).OrderByDescending(x=>x.CreatedOn).ToList();
        //    }
        //}

        //public static List<ActivityLog> GetLogsById<T>(List<string> id) where T : class
        //{
        //    var rowId = id.Select(x => typeof(T).Name + "-" + x);
        //    using (var logContext = new CTGroupBOProductEntities())
        //    {
        //        return logContext.ActivityLogs.Include(x => x.User).Where(x => rowId.Contains(x.RowId)).OrderByDescending(x => x.CreatedOn).ToList();
        //    }
        //}

        static string GetPrimaryKeyString(object obj, DbContext db)
        {
            var entry = db.Entry(obj);
            var objectStateEntry = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return string.Join("-", objectStateEntry.EntityKey.EntityKeyValues.Select(x => x.Value));
        }

        //public static string GetModifiedLog(this DbContext context, object entry)
        //{
        //    context.ChangeTracker.Entries();
        //    var myObjectState = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry);
        //    var typename = myObjectState.EntitySet.ElementType.Name;

        //    var modifiedProperties = myObjectState.GetModifiedProperties();
        //    var sb = new StringBuilder();
        //    foreach (var propName in modifiedProperties)
        //    {
        //        sb.Append(
        //            $"Cập nhật {Utility.GetTitle(typename, propName).ToLower()} từ [{myObjectState.OriginalValues[propName]}] thành [{myObjectState.CurrentValues[propName]}]\n");
        //    }
        //    var logDescription = sb.ToString();
        //    return logDescription;
        //}

        public static string GetAddDeleteLog<T>(this DbContext context, Func<T, object> property)
        {
            var sb = new StringBuilder();
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (ObjectContext.GetObjectType(entry.Entity.GetType()) == typeof(T))
                {
                    if (entry.State == EntityState.Added)
                    {
                        sb.AppendLine($"Thêm mới {typeof(T).Name} [{property((T)entry.Entity)}]");
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        sb.AppendLine($"Xóa {typeof(T).Name} [{property((T)entry.Entity)}]");
                    }
                }
            }
            var logDescription = sb.ToString();
            return logDescription;
        }


        static Dictionary<string, object> NvcToDictionary(NameValueCollection nvc, bool handleMultipleValuesPerKey)
        {
            var result = new Dictionary<string, object>();
            foreach (string key in nvc.Keys)
            {
                if (handleMultipleValuesPerKey)
                {
                    string[] values = nvc.GetValues(key);
                    if (values.Length == 1)
                    {
                        result.Add(key, values[0]);
                    }
                    else
                    {
                        result.Add(key, values);
                    }
                }
                else
                {
                    result.Add(key, nvc[key]);
                }
            }

            return result;
        }

    }

    public enum LogType
    {
        [Description("Đăng nhập")]
        User_Login = 1000,
        [Description("Cập nhật user")]
        User_Edit = 1001,
        [Description("Đồng bộ từ HR")]
        User_SyncHr = 1002,
        [Description("Khóa user")]
        User_Block = 1003,
        [Description("Mở khóa user")]
        User_Unblock = 1004,
        [Description("Cập nhật đường dẫn lưu trữ")]
        User_EditPositionCode = 1005,
        [Description("Tạo đường dẫn lưu trữ")]
        User_CreatePositionCode = 1006,
        [Description("Tạo đề nghị nghỉ phép")]
        User_LeaveRecord_Create = 1007,
        [Description("Cập nhật đề nghị nghỉ phép")]
        User_LeaveRecord_Edit = 1008,

        // product
        [Description("Tạo sản phẩm")]
        Product_Create = 2000,

        [Description("Cập nhật sản phẩm")]
        Product_Edit = 2001,

        [Description("Tạo dự án")]
        Product_CreateProject = 2002,

        [Description("Cập nhật dự án")]
        Product_EditProject = 2003,

        [Description("Tạo tiến độ dự án")]
        Product_CreateProcessProject = 2004,

        [Description("Cập nhật tiến độ dự án")]
        Product_EditProcessProject = 2005,

        [Description("Tạo hợp đồng mẫu")]
        ContractTemplate_Create = 2006,

        [Description("Sửa hợp đồng mẫu")]
        ContractTemplate_Edit = 2007,

        [Description("Đăng kí cho sale")]
        Product_AssignSale = 2008,

        // customer
        [Description("Tạo khách hàng")]
        Customer_Create = 3000,

        [Description("Chỉnh sửa thông tin khách hàng")]
        Customer_Edit = 3001,

        [Description("Tạo ứng viên")]
        Candidate_Create = 3002,

        [Description("Chỉnh sửa thông tin ứng viên")]
        Candidate_Edit = 3002,
        //
        [Description("Tạo hợp đồng hoa hồng cho sale")]
        ContractCommission_Create = 4000,
        [Description("Sửa hợp đồng hoa hồng cho sale")]
        ContractCommission_Edit = 4001,
        [Description("Tạo hợp đồng khách hàng")]
        CustomerContract_Create = 4002,
        [Description("Sửa hợp đồng khách hàng")]
        CustomerContract_Edit = 4003,
        [Description("Tạo tiến độ thanh toán hợp đồng")]
        CustomerContract_CreatePaymentProcess = 4004,
        [Description("Cập nhật tiến độ thanh toán hợp đồng")]
        CustomerContract_EditPaymentProcess = 4005,
        [Description("Upload file hợp đồng")]
        CustomerContract_UploadFile = 4006,
        [Description("Chuyển nhượng hợp đồng")]
        CustomerContract_Transfer = 4007,


        // hệ thống,
        [Description("Cài đặt email")]
        SettingEmail = 5000,
    }




    
}
