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

                T page = PageFactory.InitElements<T>(driver);
                pages.Add(pageClassType, page);
            }
            return pages[pageClassType] as T;
        }
    }
}
