using Microsoft.AspNetCore.Html;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CarInsurance.ConstantsAndHelpers.HtmlExtensions
{
    public static class HtmlHelperExtensions
    {

        public static HtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var description = metadata.Description;
            return new HtmlString(description);
        }
    }
}