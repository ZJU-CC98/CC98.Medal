using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CC98.Medal.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CC98.Medal.Components
{
	public class EnumSelectorViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(ModelExpression aspFor)
		{
			var enumType = aspFor.Metadata.UnderlyingOrModelType;
			var items = GetEnumItemNameAndDescription(enumType);

			return View(new EnumSelectorViewModel { AspFor = aspFor, Items = items });
		}

		private static IEnumerable<EnumSelectorItem> GetEnumItemNameAndDescription(Type enumType)
		{
			var itemNames = enumType.GetEnumNames();

			foreach (var itemName in itemNames)
			{
				var item = enumType.GetField(itemName,
					BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

				if (item == null)
				{
					continue;
				}

				var displayAttr = item.GetCustomAttribute<DisplayAttribute>();

				yield return
					displayAttr != null
						? new EnumSelectorItem(itemName, displayAttr.Name ?? itemName, displayAttr.Description)
						: new EnumSelectorItem(itemName, itemName, null);
			}
		}
	}
}
