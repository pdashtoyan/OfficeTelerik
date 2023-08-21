using DataAccessLayer.DBTools;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using static OfficeTelerik.Pages.Employee;

namespace OfficeTelerik.Data
{
    public class Employees : IErrMsg
    {
        public int? ID { get; set; }
        [Required]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Dont use whitespaces")]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public Positions? Position { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string ErrMsg { get; set; }
        public string? Name { get; set; }
        public uint? rowCount { get; set; }
        public string ForSupervizors { get; set; }



        public Employees()
        {
        }
        public Employees(int? id = null, string? firstname = null, string? lastName = null,
                         string? title = null, string? name = null, Positions? position = null,
                         string? phone = null, string? email = null
                        )
        {
            ID = id;
            FirstName = firstname;
            LastName = lastName;
            Title = title;
            Name = name;
            Position = position;
            Phone = phone;
            Email = email;
        }

        public async Task<List<Employees>> GetEmployeeFilterAsync(int? viewCount, int? page)
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("firstName",this.FirstName),
                    new SPParam("lastName",this.LastName),
                    new SPParam("title",this.Title),
                    new SPParam("name",this.Name),
                    new SPParam("position",this.Position),
                    new SPParam("forsupervizors",this.ForSupervizors),
                    new SPParam("phone",this.Phone),
                    new SPParam("email",this.Email),
                    new SPParam("startIndex",page-1),
                    new SPParam("viewCount",viewCount),

                };
                List<Employees> items = new List<Employees>();
                (items, rowCount) = await MySQLDataAccess<Employees>.ExecuteSPListByPagingAsync("GetEmployeeListByFilter", par);
                return items;
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


        public async Task<Employees> AddEmployeeAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("firstName",this.FirstName),
                    new SPParam("lastName",this.LastName),
                    new SPParam("title",this.Title),
                    new SPParam("position",this.Position),
                    new SPParam("forsupervizors",this.ForSupervizors),
                    new SPParam("phone",this.Phone),
                    new SPParam("email",this.Email)
                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("AddEmployee", par);
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

        public async Task<Employees> GetEmployeeByIdAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID)
                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("GetEmployeeById", par);
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


        public async Task<Employees> DeleteEmployeeAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID)
                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("DeleteEmployee", par);
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


        public async Task<Employees> UpdateEmployeeAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID),
                    new SPParam("firstName",this.FirstName),
                    new SPParam("lastName",this.LastName),
                    new SPParam("title",this.Title),
                    new SPParam("forsupervizor",this.ForSupervizors),
                    new SPParam("position",this.Position),
                    new SPParam("phone",this.Phone),
                    new SPParam("email",this.Email),
                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("UpdateEmployee", par);
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


        public async Task<List<Employees>> GetOnlyEmployeeAsync()
        {
            try
            {
                List<Employees> item = await MySQLDataAccess<Employees>.ExecuteSPListAsync("GetOnlyEmployees", null);
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

        public async Task<List<Employees>> GetOnlySupervizorsAync()
        {
            try
            {
                List<Employees> item = await MySQLDataAccess<Employees>.ExecuteSPListAsync("GetSupervizors", null);
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


        public async Task<Employees> GetEmployeeByEmailAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("email",this.Email)
                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("GetEmployeeByEmail", par);
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




    }
}