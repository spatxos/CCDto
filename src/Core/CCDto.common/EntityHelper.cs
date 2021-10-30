using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CCDto.common
{
    public static class EntityHelper
    {
        public static string GetTableName<T>()
        {
            return GetPropertySignName<T>(p => p);
        }

        public static PropertyInfo GetSingleKey<T>()
        {
            var Attribute = (typeof(T)).GetType().GetProperties().FirstOrDefault(e => e.Name == "ExplicitKey");
            //var Sign = (DataMemberAttribute)Attribute.GetCustomAttributes(typeof(DataMemberAttribute), true)[0];
            return Attribute;
        }


        public static string GetPropertySignName<T>(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.CustomAttributes.FirstOrDefault().NamedArguments.FirstOrDefault().TypedValue.Value.ToString();
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.CustomAttributes.FirstOrDefault().NamedArguments.FirstOrDefault().TypedValue.Value.ToString();
            }
            return rtn;
        }
    }
}
