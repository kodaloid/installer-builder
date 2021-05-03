using System.ComponentModel.DataAnnotations;

namespace InstallerBuilder.Includes
{
    public enum SoftwareType
    {
        [Display(Name="C/C++ Application", Description="Application written using the C or C++ programming language.")]
        CppApplication,

        [Display(Name=".NET Core/Framework Applicaton", Description="Application written in .NET Core or .NET Framework.")]
        DotNetApplication,

        [Display(Name="Electron Application", Description="Application written using the Electron web framework.")]
        ElectronApplication,

        [Display(Name="Python Application", Description="Application written using the Python programming language.")]
        PythonApplication,

        [Display(Name="HaXe Applicatiion", Description="Application written using the HaXe programming language.")]
        HaxeApplication,

        [Display(Name="Love2D Game", Description="A game written using the Love2D game engine.")]
        Love2dGame,

        [Display(Name="Xentu Game", Description="A game written using the Xentu game engine.")]
        XentuGame
    }
}