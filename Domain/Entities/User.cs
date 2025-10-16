using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Username { get; private set; } // Setters privates para modificar solo las propiedades con métodos con reglas de negocio
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? ProfilePictureUrl { get; private set; }
        public string? Bio { get; private set; }
        public bool IsPrivate { get; private set; }

        //private readonly List<Post> _posts = new();
        //public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

        //private readonly List<Follow> _following = new();
        //public IReadOnlyCollection<Follow> Following => _following.AsReadOnly();

        //private readonly List<Follow> _followers = new();
        //public IReadOnlyCollection<Follow> Followers => _followers.AsReadOnly();

        private User() { }

        public static User Create(string username, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username no puede estar vacío", nameof(username));

            if (username.Length < 3 || username.Length > 30)
                throw new ArgumentException("Username debe tener entre 3 y 30 caracteres", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email no puede estar vacío", nameof(email));

            if (!email.Contains('@'))
                throw new ArgumentException("Email inválido", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("PasswordHash no puede estar vacío", nameof(passwordHash));

            return new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                IsPrivate = false
            };
        }

        public void UpdateProfile(string? bio, string? profilePictureUrl)
        {
            if (bio?.Length > 500)
                throw new ArgumentException("Bio no puede superar los 500 caracteres", nameof(bio));

            Bio = bio;
            ProfilePictureUrl = profilePictureUrl;
        }

        public void SetPrivacy(bool isPrivate)
        {
            IsPrivate = isPrivate;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("PasswordHash no puede estar vacío", nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
        }
    }
}
