using Aluraflix.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aluraflix.API.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Usuario> WithoutPasswords(this IEnumerable<Usuario> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Usuario WithoutPassword(this Usuario user)
        {
            user.Password = null;
            return user;
        }
    }
}
