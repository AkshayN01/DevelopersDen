using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Library.Generic
{
    public static class Utility
    {
        public static Expression<Func<T, bool>> CombineConditions<T>(List<Expression<Func<T, bool>>> conditions)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var condition in conditions)
            {
                body = body == null ? Expression.Invoke(condition, param) : Expression.AndAlso(body, Expression.Invoke(condition, param));
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static bool IsUniqueConstraintViolation(Exception ex)
        {
            if (ex.InnerException is PostgresException postgresException)
            {
                // PostgreSQL error code for unique constraint violation is 23505
                return postgresException.SqlState == "23505";
            }
            return false;
        }
    }
}
