using System.ComponentModel.DataAnnotations;

namespace InstallerBuilder.Includes
{
    public enum InstallerType
    {
        [Display(Name="NSIS by Nullsoft", Description="The NSIS Installer system, created by NullSoft for WinAmp.")]
        NSIS,

        /* [Display(Name="InnoSetup", Description="")]
        InnoSetup,

        [Display(Name = "Advanced Installer", Description = "")]
        AdvancedInstaller,

        [Display(Name="WinRAR (SFX)", Description="WinRAR's SFX system allows you to build basic installers too!")]
        WinRarSfx,

        [Display(Name = "IzPack", Description = "")]
        IzPack */
    }
}