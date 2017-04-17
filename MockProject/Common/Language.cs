using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Ajax.Utilities;
using MockProject.Resources;

namespace MockProject.Common
{

    public class Language
    {
        #region Khai báo từ khóa ngôn ngữ

        public string Password = GetLanguageValue(nameof(Password));
        public string RememberMe = GetLanguageValue(nameof(RememberMe));
        public string UserName = GetLanguageValue(nameof(UserName));
       
        #endregion

        private static Language _instance;

        public static Language Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Language();
                }
                return _instance;
            }
        }
        public void SetNull()
        {
            _instance = null;
        }

        private static string GetLanguageValue(string languageKey)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "vi-VN";

            string result;
            switch (cultureName)
            {
                case "en-US":
                    result = Language_en.ResourceManager.GetString(languageKey);
                    break;
                default:
                    result = Language_vi.ResourceManager.GetString(languageKey);
                    break;
            }

            return string.IsNullOrEmpty(result) ? languageKey : result;
        }

        public static string T(string vnText, string key)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "vi-VN";

            string result;
            switch (cultureName)
            {
                case "en-US":
                    result = Language_en.ResourceManager.GetString(key);
                    break;
                default:
                    result = vnText;
                    break;
            }

            return result;
        }
    }

}