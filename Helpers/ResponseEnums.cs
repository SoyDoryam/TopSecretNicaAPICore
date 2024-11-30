namespace TopSecretNicaAPICore.Helpers
{
    public enum ResponseStatus
    {
        Success = 200,
        InternalServerError = 500
    }

    // Clase estática para los mensajes
    public static class ResponseMessages
    {
        public const string Ok = "ok";
        public const string Added = "agregado";
        public const string Edited = "editado";
        public const string Deleted = "eliminado";  
        public const string Error = "error";
    }
}
