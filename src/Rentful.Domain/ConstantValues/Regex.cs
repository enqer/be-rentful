namespace Rentful.Domain.ConstantValues
{
    public static class Regex
    {
        public const string EmailPattern = @"^[\w\-.]+@([\w-]+\.)+[a-zA-Z]{2,}$";
        public const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}:<>?\-=[\]\\;',./`~])[A-Za-z\d!@#$%^&*()_+{}:<>?\-=[\]\\;',./`~]{8,}$";
        public const string PhoneNumberPattern = @"^\d{9}$";
        public const string ZipCodePattern = @"^\d{5}$";
    }
}
