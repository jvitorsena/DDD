using Api.Domain.Models;

namespace Domain.Models;

public class UserModel : BaseModel
{
    private string _name;
    private string _email;
    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }
}