using Microsoft.AspNetCore.Components;
using Office.Data;
using OfficeTelerik.Data;
using Telerik.Blazor.Components;

namespace OfficeTelerik.Pages
{
    public class Department : ComponentBase
    {
        public List<Employees> OnlyEmployees { get; set; } = new List<Employees>();
        public List<Employees> OnlySupervizors { get; set; } = new List<Employees>();
        public Employees CurrentSupervizor { get; set; } = new Employees();
        public List<Employees> CurrentEmployees { get; set; } = new List<Employees>();
        protected async Task ReadItems(GridReadEventArgs args = null)
        {
            Departments item = new Departments();
            args.Data = await item.GetDepartmentFilterAsync(args?.Request?.PageSize ?? 0, args?.Request?.Page ?? 1);
            if(item.rowCount != null)
            {
                args.Total = (int)item.rowCount;
            }
        }

        protected async Task CreateHandler(GridCommandEventArgs args)
        {
            Departments item = args.Item as Departments;
            if (item != null)
            {
                await item.AddDepartmentAsync();
            }
        }



        public async Task DeleteHandler(GridCommandEventArgs args)
        {
            Departments item = args.Item as Departments;
            if (item != null)
            {
                await item.DeleteDepartmentAsync();
            }
        }

        public async Task UpdateHandler(GridCommandEventArgs args)
        {
            Departments item = args.Item as Departments;
            if (item != null)
            {
                await item.UpdateDepartmentAsync();
            }
        }

        public async Task OnCreate()
        {
            Employees itemz = new Employees();
            OnlyEmployees = await itemz.GetOnlyEmployeeAsync();
            OnlySupervizors = await itemz.GetOnlySupervizorsAync();
        }

        public async Task EditHandler(GridCommandEventArgs args)
        {

            Departments item = args.Item as Departments;
            if (item != null)
            {
                await OnCreate();
                CurrentSupervizor = await item.GetCurrentSupervizorAsync();
                CurrentEmployees = await item.GetCurrentEmployessAsync();
                item.EmployeeIDs = CurrentEmployees.Select(e => e.ID).ToList();
                foreach (var itemz in CurrentEmployees)
                {
                    OnlyEmployees.Add(itemz);
                }
            }
        }
    }
}
