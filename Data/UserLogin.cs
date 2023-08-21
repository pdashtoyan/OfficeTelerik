using DataAccessLayer.DBTools;
using DataAccessLayer.Interfaces;
using Office.Data;
using Telerik.SvgIcons;

namespace OfficeTelerik.Data
{
    public class UserLogin : IErrMsg
    {
        public int? ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ErrMsg { get; set; }
        public string Email { get; set; }

        public UserLogin()
        {

        }

        public UserLogin(string username = null, string password = null, string email = null)
        {
            UserName = username;
            Password = password;
            Email = email;
        }

        public async Task<UserLogin> GetUsersInfo()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("username",this.UserName),
                    new SPParam("pass",this.Password),
                };
                UserLogin item = await MySQLDataAccess<UserLogin>.ExecuteSPItemAsync("GetUser", par);
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: AddCategory, Ex:{ex.Message}");
                return null;
            }
            finally
            {
            }
        }

        public async Task<UserLogin> AddNewUserAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("username",this.UserName),
                    new SPParam("password",this.Password),
                    new SPParam("email",this.Email),
                };

                UserLogin item = await MySQLDataAccess<UserLogin>.ExecuteSPItemAsync("AddNewUser", par);
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: AddCategory, Ex:{ex.Message}");
                return null;
            }
            finally
            {
            }
        }

        public async Task<UserLogin> GetUserByEmail()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("email",this.Email),

                };
                UserLogin item = await MySQLDataAccess<UserLogin>.ExecuteSPItemAsync("GetUserByEmail", par);
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: AddCategory, Ex:{ex.Message}");
                return null;
            }
            finally
            {
            }
        }
        public int MyProperty { get; set; }
    }
}
