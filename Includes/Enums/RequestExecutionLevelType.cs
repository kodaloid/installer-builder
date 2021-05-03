using System.ComponentModel.DataAnnotations;

namespace InstallerBuilder.Includes
{
    public enum RequestExecutionLevelType
    {
        [Display(Name="None", Description="No elevated execution level request will be made.")]
        None,

        [Display(Name = "User", Description = "Standard UAC execution level.")]
        User,

        [Display(Name = "Highest", Description = "Requests the Highest UAC execution level.")]
        Highest,

        [Display(Name = "Admin", Description = "Requests the Admin UAC execution level.")]
        Admin
    }
}