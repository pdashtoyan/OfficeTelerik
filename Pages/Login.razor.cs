using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using OfficeTelerik.Data;
using System.Security.Claims;
using Telerik.SvgIcons;

namespace OfficeTelerik.Pages
{
    public class Loginz:ComponentBase
    {
        public string Email { get; set; }
        protected string username { get; set; }
        protected string password { get; set; } 
        public string ConfirmPassword { get; set; }
        public string RegistrationMessage { get; set; }
        public string RegMessageColor { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
        public bool ShowErrorMessage { get; set; } = false;
         
        
        protected async Task LoginUser()
        {
            UserLogin us = await new UserLogin(username, password).GetUsersInfo();
            if (us?.ID != null)
            {
                Employees emp = await new Employees(email: us.Email).GetEmployeeByEmailAsync();
                await ((CustomAuthStatePrvider)authenticationStateProvider).LogIn(us.UserName,emp?.Position !=null? emp.Position.ToString() : null );
                   navigationManager.NavigateTo("/index");
            }
            else
            {
                    ShowErrorMessage = true;
            }
            SetNulls();
        }
        public void forgit()
        {

        }

        protected async Task LogOutUser()
        {
               await ((CustomAuthStatePrvider)authenticationStateProvider).Logout();
               navigationManager.NavigateTo("/");
        }
        public void  GoToRegisterPage()
        {
           
            if (navigationManager.Uri.Contains("/sign-up"))
            {
                navigationManager.NavigateTo("/", true);
                return;
            }
            navigationManager.NavigateTo("/sign-up", true);
        }

        public async Task Sign_up()
        {
            Employees emp = await new Employees(email:Email).GetEmployeeByEmailAsync();
            UserLogin userCheck = await new UserLogin(email: Email).GetUserByEmail(); ; 
            if(emp.Email != null && password==ConfirmPassword && userCheck.Email ==null)
            {
                await new UserLogin(username:username,password:password,email:Email).AddNewUserAsync();
                RegMessageColor = "green";
                RegistrationMessage = "Account has succesfull created";
                StateHasChanged();
                await Task.Delay(4000);
                GoToRegisterPage();
            }else if (userCheck.Email ==Email)
            {
                RegMessageColor = "red";
                RegistrationMessage = $"{Email} already have an account";
            }
            else
            {
                RegMessageColor = "red";
                RegistrationMessage = "Something Went Wrong,check your Email or password conformation";
            }
            

        }

        public async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if(e.Key == "Enter")
            {
                await LoginUser();
            }
        }


      
        public void SetNulls()
        {
            username =null;
            password = null;
            Email = null;
            ConfirmPassword = null;
        }
    }
}
