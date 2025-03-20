namespace RestauranteBackend.Interfaces
{
    public interface IAsignaciones
    {
        string GenerateNewId();
        DateTime GetCurrentDateTime();
        string EncriptPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        string GenerateJwtToken<T>(T data);
    }
}
