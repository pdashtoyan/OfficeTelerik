using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using OfficeTelerik.Data;
using OfficeTelerik;
using System.ComponentModel.DataAnnotations;
using Telerik.Blazor.Components;


namespace OfficeTelerik.Pages
{
    public class Employee : ComponentBase
    {
        public List<Positions> PositionsData { get; set; } = new List<Positions> {Positions.Supervizor,Positions.Employee };
        public enum Positions
        {
            Employee = 1,
            Supervizor = 2,
        }

        protected async Task ReadItems(GridReadEventArgs args = null)
        {
            Employees item = new Employees();
            args.Data = await item.GetEmployeeFilterAsync(args?.Request?.PageSize ?? 0, args?.Request?.Page ?? 1);
            if(item.rowCount != null)
            {
                args.Total = (int)item.rowCount;
            }
        }

        public async Task CreateHandler( GridCommandEventArgs args)
        {
            Employees item = args.Item as Employees;
            if(item != null)
            {
                await item.AddEmployeeAsync();
            }
        }

        public async Task UpdateHandler(GridCommandEventArgs args)
        {
            Employees item = args.Item as Employees;
            if (item != null)
            {
                await item.UpdateEmployeeAsync();
            }
        }


        public async Task DeleteHandler(GridCommandEventArgs args)
        {
            Employees item = args.Item as Employees;
            if (item != null)   
            {
                await item.DeleteEmployeeAsync();
            }
        }
    }
}
