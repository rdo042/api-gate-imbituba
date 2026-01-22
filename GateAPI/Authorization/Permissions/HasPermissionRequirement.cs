using Microsoft.AspNetCore.Authorization;

namespace GateAPI.Authorization.Permissions
{
    public class HasPermissionRequirement(string permissao) : IAuthorizationRequirement
    {
        public string Permissao { get; } = permissao;
    }
}
