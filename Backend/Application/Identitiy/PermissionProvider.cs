


namespace FormulaOne.Application.Identity;

public class PermissionProvider : IPermissionProvider
{
    public static IReadOnlyDictionary<string, Enums.Permissions[]> PermissionPairs = new Dictionary<string, Enums.Permissions[]>() 
    {
        ["Admin"] = Enum.GetValues<Enums.Permissions>(),

        ["SimpleUser"] = new[]
        {
            Enums.Permissions.Read_Cars,
            Enums.Permissions.Read_Circuit,
            Enums.Permissions.Read_Drivers,
            Enums.Permissions.Read_DriversCSh,
            Enums.Permissions.Read_Races,
            Enums.Permissions.Read_TeamCSh,
            Enums.Permissions.Read_Season,
            Enums.Permissions.Read_Teams
        },
        ["Moderator"] = new[]
        {
            Enums.Permissions.Read_Cars,
            Enums.Permissions.Read_Circuit,
            Enums.Permissions.Read_Drivers,
            Enums.Permissions.Read_DriversCSh,
            Enums.Permissions.Read_Races,
            Enums.Permissions.Read_TeamCSh,
            Enums.Permissions.Read_Season,
            Enums.Permissions.Read_Teams,
            Enums.Permissions.Create_Cars,
            Enums.Permissions.Create_TeamCSh,
            Enums.Permissions.Create_DriversCSh,

            Enums.Permissions.User_Manage
        }
    };

    public IReadOnlyCollection<Enums.Permissions> GetPermissionsForRole(string role)
    {
        return PermissionPairs.TryGetValue(role,out var permissions) ? permissions : Array.Empty<Enums.Permissions>();
    }
}
