namespace models
{
    public class UserDTO
    {
        public UserDTO(User user) => (Id, Name, Email) = (user.Id, user.Name, user.Email);

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
