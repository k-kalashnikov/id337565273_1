namespace SP.Contract.Rest.Function.Test.Models
{
    public class SignInSmsCodeViewModel
    {
        public SignInSmsCodeViewModel(string login, int code)
        {
            Login = login;
            Code = code;
        }

        public string Login { get; private set; }

        public int Code { get; private set; }

        public static SignInSmsCodeViewModel Create(string login, int code)
            => new SignInSmsCodeViewModel(login, code);
    }
}
