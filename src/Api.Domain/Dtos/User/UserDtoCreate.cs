using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User;

public class UserDtoCreate
{
    [Required(ErrorMessage = "O nome é um campo obrigatório.")]
    [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public string Name { get; set; }
    
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    [Required(ErrorMessage = "Email é campo obrigatório")]
    [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
    public string Email { get; set; }
}