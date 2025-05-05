namespace User.Application;

public interface ILoginService
{
    public string Login(string username, string password);
}