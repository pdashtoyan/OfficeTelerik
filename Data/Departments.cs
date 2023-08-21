using DataAccessLayer.DBTools;
using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using OfficeTelerik.Data;

namespace Office.Data
{
    public class Departments : IErrMsg
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public uint? rowCount { get; set; }
        public int? startIndex { get; set; }
        public int? viewCount { get; set; }
        public string ErrMsg { get; set; }
        public int? SuperVizorID { get; set; } = null;
        public List<int?> EmployeeIDs { get; set; } = new List<int?>();
       

        public Departments()
        {

        }

        public Departments(int? id = null, string? name = null, string? location = null,
            string? phone = null, int? supervizorId = null, List<int?> employeeIds = null
            )
        {
            ID = id;
            Name = name;
            Location = location;
            Phone = phone;
            SuperVizorID = supervizorId;
            EmployeeIDs = employeeIds;
        }



        public async Task<List<Departments>> GetDepartmentFilterAsync(int? viewCount, int? page)
        {

            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("name",this.Name),
                    new SPParam("location",this.Location),
                    new SPParam("phone",this.Phone),
                    new SPParam("startIndex",page-1),
                    new SPParam("viewCount",viewCount),
                };
                List<Departments> items = new List<Departments>();



                (items, rowCount) = await MySQLDataAccess<Departments>.ExecuteSPListByPagingAsync("GetDepartmentListByFilter", par);
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

        public async Task<Departments> AddDepartmentAsync()
        {

            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("departName",this.Name),
                    new SPParam("location",this.Location),
                    new SPParam("phone",this.Phone),
                    new SPParam("headId",this.SuperVizorID),
                    new SPParam("employeesList",JsonConvert.SerializeObject(EmployeeIDs)),
                };

                Departments item = await MySQLDataAccess<Departments>.ExecuteSPItemAsync("AddDepartment", par);
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






        public async Task<Departments> DeleteDepartmentAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID),

                };

                Departments item = await MySQLDataAccess<Departments>.ExecuteSPItemAsync("DeleteDepartment", par);
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



        public async Task<Departments> GetDepartmentByIdAsync()
        {

            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID),

                };

                Departments item = await MySQLDataAccess<Departments>.ExecuteSPItemAsync("GetDepartmentById", par);
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


        public async Task<List<Employees>> GetCurrentEmployessAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("departmentId",this.ID),
                };

                List<Employees> item = await MySQLDataAccess<Employees>.ExecuteSPListAsync("GetEmployeesByDepartment", par);
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
        public async Task<Employees> GetCurrentSupervizorAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("departmentId",this.ID),

                };
                Employees item = await MySQLDataAccess<Employees>.ExecuteSPItemAsync("GetSupervizorByDepartment", par);
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

        public async Task<Departments> UpdateDepartmentAsync()
        {
            try
            {
                List<SPParam> par = new List<SPParam>
                {
                    new SPParam("id",this.ID),
                    new SPParam("name",this.Name),
                    new SPParam("location",this.Location),
                    new SPParam("phone",this.Phone),
                    new SPParam("headId",this.SuperVizorID),
                    new SPParam("employeeList",JsonConvert.SerializeObject(EmployeeIDs)),
                };

                Departments item = await MySQLDataAccess<Departments>.ExecuteSPItemAsync("UpdateDepartment", par);
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
