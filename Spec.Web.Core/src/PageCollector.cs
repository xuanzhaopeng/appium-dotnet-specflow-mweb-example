using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Spec.Web.Core.Pages;
using System;
using System.Collections.Generic;

namespace Spec.Web.Core
{
    public class PageCollector
    {
        private Dictionary<Type, BasePage> pages;

        public PageCollector()
        {
            pages = new Dictionary<Type, BasePage>();
        }

        public T GetPage<T>(IWebDriver driver) where T : BasePage
        {
            var pageClassType = typeof(T);
            if (!pages.ContainsKey(pageClassType))
            {
                //FIX: PageFactory.InitElements<T>(driver) doesn't work
                T page = (T)Activator.CreateInstance(typeof(T));
                PageFactory.InitElements(driver, page);
                pages.Add(pageClassType, page);
            }
            return pages[pageClassType] as T;
        }
    }
}
