using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.UtilitiesGlobal
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(84|0[35789])[0-9]{8}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize, out int totalItem)
        {
            totalItem = source.Count();
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static string HashPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool IsPasswordValid(this string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
