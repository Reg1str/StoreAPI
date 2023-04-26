namespace StoreAPI.Models;

public class ChangePasswordModel
{
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}