using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
   public class ErrorCode
    {
       public const string Success = "Cập nhật thành công";
       public const string Error = "Lỗi, vui lòng thử lại";
       public const string AccountRegistered = "Tài khoản đã được đăng ký";
       public const string EmailRegistered = "Email đã được đăng ký";
       public const string Null = "Giá trị rỗng";
       public const string OK = "ok";
       public const string UserNameNull = "Chưa nhập UserName";
       public const string PasswordNull = "Chưa nhập Password";
       public const string AddressNull = "Chưa nhập địa chỉ";
       public const string differentPassword = "Mật khẩu không khớp";
       public const string existUserName = "User Name này đã có người đăng ký";
       public const string existEmail = "Email này đã có người đăng ký";
       public const string NotValidEmail = "Email này không hợp lệ";
       public const string NotValidPhoneNumber = "Số điện thoại này không hợp lệ";
       //more.....
    }
}
