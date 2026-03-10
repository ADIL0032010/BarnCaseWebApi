namespace BarnCaseWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty; 
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public decimal Balance { get; set; } = 100.00m; 
        public List<Animal> Animals { get; set; } = new();
    }



    //DENEME 


}