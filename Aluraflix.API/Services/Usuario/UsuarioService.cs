using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aluraflix.API.Entities;
using Aluraflix.API.Helpers;

namespace Aluraflix.API.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Authenticate(string username, string password);
        Task<IEnumerable<Usuario>> GetAll();
    }

    public class UsuarioService : IUsuarioService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Usuario> _users = new List<Usuario>
        {
            new Usuario { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public async Task<Usuario> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}