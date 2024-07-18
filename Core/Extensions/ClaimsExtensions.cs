using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static void AddName(this ICollection<Claim> claims,string name)
        {
            claims.Add(new Claim(ClaimTypes.Name,name));//Claims sınıfını genişlettik
        }
        public static void AddCustomer(this  ICollection<Claim> claims,string customer)
        {
            claims.Add(new Claim("CustomerId", customer));
        }

        public static void AddRoles(this ICollection<Claim> claims,string[] roles)
        {
            roles.ToList().ForEach(roles=>claims.Add(new Claim(ClaimTypes.Role,roles)));
        }

        public static void AddCustomerName(this ICollection<Claim> claims, string customerName)
        {
            claims.Add(new Claim(ClaimTypes.IsPersistent, customerName));
        }

    }
}
