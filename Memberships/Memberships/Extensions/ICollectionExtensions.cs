using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Extensions
{
    public static class ICollectionExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this ICollection<T> items, int selectedValue)
        {
            //code goes here
            return from item in items
                   select new SelectListItem
                   {
                       // cannot get to properties directly because the item is of type T!
                       // we can use reflection to solve this... let's call the GetPropertyValue method
                       // which return a string containing the property value.
                       Text = item.GetPropertyValue("Title"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                   };
        }
    }
}