using System.ComponentModel;

namespace MyWebApplication.Features.User.Authentication.Login;

/// <summary>
/// login request
/// </summary>
public class Request
{
    /// <summary>
    /// username
    /// </summary>
    [DefaultValue(nameof(Email))]
    public string Email { get; set; } = null!;

    /// <summary>
    /// password
    /// </summary>
    [DefaultValue(nameof(Password))]
    public string Password { get; set; } = null!;
}
