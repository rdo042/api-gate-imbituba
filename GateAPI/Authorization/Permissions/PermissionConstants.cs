namespace GateAPI.Authorization.Permissions
{
    public static class PermissionConstants
    {
        public const string Usuario = "Usuario";
        public const string Perfil = "Perfil";
        public const string TipoLacre = "TipoLacre";
        public const string TipoAvaria = "TipoAvaria";
        public const string LocalAvaria = "LocalAvaria";

        public static readonly string[] GetAll =
        [
            Usuario, Perfil, TipoLacre, TipoAvaria, LocalAvaria
        ];
    }
}
