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
        public const string USER_IMAGES_FOLDER_NAME = "Images";
        public const string RESOURCES = "Resources";
        public const string SESSION_CART = "session_cart";

        public class AppSettings
        {
            public const string Token = "access_token";
            public const string BaseAddress = "BaseAddress";
        }

        public class ErrorMessage
        {
            public const string LoginFail = "Login fail";
            //Product
            public const string ProductNotFound = "Product does not exists";
            public const string ProductNameExists = "Product name already exists";
            public const string ProductNoComment = "Product have no comment";

            //Category
            public const string CategoryNotFound = "Category does not exists";
            public const string CategoryNameExists = "Category name already exists";

            //User
            public const string WrongPasswordConfirm = "Your password and confirmation password do not match!";
            public const string UserNameExists = "Username already exists!";
            public const string UserCreateFail = "User creation failed! Please check user details and try again.";
        }

        public class SuccessMessage
        {
            public const string UserCreated = "User created successfully! Let login to your account";
        }

        public class Status_code
        {
            public const string Success = "Success";
            public const string Error = "Error";
        }
    }
}
