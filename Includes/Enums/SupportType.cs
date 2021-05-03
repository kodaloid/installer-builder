using System.ComponentModel.DataAnnotations;

namespace InstallerBuilder.Includes
{
    public enum SupportType
    {
        [Display(Name="None", Description="Select an option to include a framework.")]
        None,

        [Display(Name=".NET 5.0", Description="Includes the .NET 5.0 Redistributable")]
        DotNet5_0,

        [Display(Name=".NET Core 3.1", Description="Includes the .NET Core 3.1 Redistributable")]
        DotNetCore3_1,

        [Display(Name=".NET Core 3.0", Description="Includes the .NET Core 3.0 Redistributable")]
        DotNetCore3_0,

        [Display(Name=".NET Core 2.2", Description="Includes the .NET Core 2.2 Redistributable")]
        DotNetCore2_2,

        [Display(Name=".NET Core 2.1", Description="Includes the .NET Core 2.1 Redistributable")]
        DotNetCore2_1,

        [Display(Name=".NET Core 2.0", Description="Includes the .NET Core 2.0 Redistributable")]
        DotNetCore2_0,

        [Display(Name=".NET Core 1.1", Description="Includes the .NET Core 1.1 Redistributable")]
        DotNetCore1_1,

        [Display(Name=".NET Core 1.0", Description="Includes the .NET Core 1.0 Redistributable")]
        DotNetCore1_0,

        [Display(Name=".NET Framework 4.8", Description="Includes the .NET Framework 4.8 Redistributable")]
        DotNetFrameWork480,

        [Display(Name=".NET Framework 4.7.2", Description="Includes the .NET Framework 4.7.2 Redistributable")]
        DotNetFrameWork472,

        [Display(Name=".NET Framework 4.7.1", Description="Includes the .NET Framework 4.7.1 Redistributable")]
        DotNetFrameWork471,

        [Display(Name=".NET Framework 4.7", Description="Includes the .NET Framework 4.7 Redistributable")]
        DotNetFrameWork470,

        [Display(Name=".NET Framework 4.6.2", Description="Includes the .NET Framework 4.6.2 Redistributable")]
        DotNetFrameWork462,

        [Display(Name=".NET Framework 4.6.1", Description="Includes the .NET Framework 4.6.1 Redistributable")]
        DotNetFrameWork461,

        [Display(Name=".NET Framework 4.6", Description="Includes the .NET Framework 4.6 Redistributable")]
        DotNetFrameWork460,

        [Display(Name=".NET Framework 4.5.2", Description="Includes the .NET Framework 4.5.2 Redistributable")]
        DotNetFrameWork452,

        [Display(Name=".NET Framework 4.5.1", Description="Includes the .NET Framework 4.5.1 Redistributable")]
        DotNetFrameWork451,

        [Display(Name=".NET Framework 4.5", Description="Includes the .NET Framework 4.5 Redistributable")]
        DotNetFramework450,

        [Display(Name=".NET Framework 4.0", Description="Includes the .NET Framework 4.0 Redistributable")]
        DotNetFramework400
    }
}