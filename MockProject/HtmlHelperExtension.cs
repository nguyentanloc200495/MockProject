using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MockProject
{
    public static class HtmlHelperExtension
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static MvcHtmlString CheckBoxListEnum(this HtmlHelper htmlHelper, string name, Type enumType)
        {
            var sb = new StringBuilder();

            IEnumerable<Enum> values = Enum.GetValues(enumType).Cast<Enum>();
            sb.Append("<table>");
            foreach (var value in values)
            {
                if (!value.ToString().ToUpper().Contains("DELETE"))
                {
                    string text = value.GetString();
                    if (string.IsNullOrWhiteSpace(text)) text = value.ToString();

                    sb.Append("<tr>");
                    //var checkbox = htmlHelper.CheckBox(name, new { value = (int)Enum.Parse(enumType, value.ToString()) }).ToHtmlString();
                    sb.AppendFormat("<td><input type='checkbox' name='{0}' id='{3}' value='{1}'/><label for='{3}'>{2}</label></td>", name,
                        (int)Enum.Parse(enumType, value.ToString()), text, enumType + ((int)Enum.Parse(enumType, value.ToString())).ToString());
                    sb.Append("</tr>");
                }

            }

            sb.Append("</table>");
            return MvcHtmlString.Create(sb.ToString());
        }

        // Heo.sua
        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, object selectValue, Type enumType, bool createEmptyItem = true, string emptyItemValue = "", object htmlAttributes = null, string emptyItemText = "")
        {

            int outPut;
            var isNotEnum = selectValue == null || int.TryParse(selectValue.ToString(), out outPut);
            // build item list
            IEnumerable<Enum> values = Enum.GetValues(enumType).Cast<Enum>();

            var items = new List<SelectListItem>();
            foreach (var value in values)
            {
                string text = value.GetString();
                if (string.IsNullOrWhiteSpace(text))
                    text = value.ToString();
                items.Add(new SelectListItem
                {
                    Text = text,
                    Value = isNotEnum ? ((int)Enum.Parse(enumType, value.ToString())).ToString() : Enum.Parse(enumType, value.ToString()).ToString(),
                    Selected = selectValue != null && ((int)Enum.Parse(enumType, value.ToString())) == ((int)selectValue)
                });
            }

            if (createEmptyItem)
            {
                items.Insert(0, new SelectListItem { Text = string.IsNullOrWhiteSpace(emptyItemText) ? @"--- Vui lòng chọn ---" : emptyItemText, Value = emptyItemValue });
            }

            return htmlHelper.DropDownList(name, items.AsEnumerable(), htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownListExcludeValue(this HtmlHelper htmlHelper, string name, object selectValue, Type enumType, bool createEmptyItem = true, string emptyItemValue = "", object htmlAttributes = null, string emptyItemText = "", List<Enum> excludeValue = null)
        {

            int outPut;
            var isNotEnum = selectValue == null || int.TryParse(selectValue.ToString(), out outPut);
            // build item list
            IEnumerable<Enum> values = Enum.GetValues(enumType).Cast<Enum>();

            var items = new List<SelectListItem>();
            foreach (var value in values)
            {
                if (excludeValue != null && excludeValue.Contains(value))
                    continue;
                string text = value.GetString();
                if (string.IsNullOrWhiteSpace(text))
                    text = value.ToString();
                items.Add(new SelectListItem
                {
                    Text = text,
                    Value = isNotEnum ? ((int)Enum.Parse(enumType, value.ToString())).ToString() : Enum.Parse(enumType, value.ToString()).ToString(),
                    Selected = selectValue != null && ((int)Enum.Parse(enumType, value.ToString())) == ((int)selectValue)
                });
            }

            if (createEmptyItem)
            {
                items.Insert(0, new SelectListItem { Text = string.IsNullOrWhiteSpace(emptyItemText) ? @"--- Vui lòng chọn ---" : emptyItemText, Value = emptyItemValue });
            }

            return htmlHelper.DropDownList(name, items.AsEnumerable(), htmlAttributes);
        }

        //public static MvcHtmlString DropDownListDepartment(this HtmlHelper htmlHelper, string name, bool buildEmptyOption = true, object htmlAttributes = null, string textEmpty = "-- Tất cả --")
        //{
        //    var departmentSelectList = DepartmentService.GetAllDepartmentActive().Select(x => new SelectListItem
        //    {
        //        Value = x.LSLevel2ID,
        //        Text = x.VNName
        //    }).ToList();
        //    if (buildEmptyOption)
        //    {
        //        departmentSelectList.Insert(0, new SelectListItem
        //        {
        //            Text = textEmpty,
        //            Value = ""
        //        });
        //    }

        //    return htmlHelper.DropDownList(name, departmentSelectList, htmlAttributes);
        //}


    }
}