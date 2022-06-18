using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "eCommerceDb";
       
        public class AppSettings
        {
            public const string Token = "access_token";
            public const string BaseAddress = "BaseAddress";
        }

        public class ErrorMessage
        {
            public const string LoginFail = "Login fail";
            public const string ProductNotFound = "Product does not exists";
            public const string ProductNameExists = "Product name already exists";
            public const string WrongPasswordConfirm = "Your password and confirmation password do not match!";
            public const string UserNameExists = "Username already exists!";
            public const string UserCreateFail = "User creation failed! Please check user details and try again.";
        }

        public class SuccessMessage
        {
            public const string UserCreated = "User created successfully!";
        }

        public class Status_code
        {
            public const string Success = "Success";
            public const string Error = "Error";
        }
    }
}
