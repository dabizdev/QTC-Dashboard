using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qtc.Dashboard.BusinessLayer.ApiEntityClasses
{
    public class AuthUserPermissions
    {
        public string Application { get; set; }
        public string User { get; set; }
        public List<RolePermission> Roles { get; set; }

        public bool HasPermission(string value)
        {
            if (String.IsNullOrEmpty(value)) return false;
            if (this.Roles == null) return false;

            foreach (var role in Roles)
            {
                if (role.Permissions == null) return false;

                if (role.Permissions.Any(x => x == value)) return true;
            }

            return false;
        }

        public bool HasRole(string value)
        {
            if (String.IsNullOrEmpty(value)) return false;
            if (this.Roles == null) return false;

            foreach (var role in Roles)
            {
                if (String.CompareOrdinal(role.Name, value) == 0) return true;
            }

            return false;
        }

        //Check this permission for Access to the Medical Records Page
        public bool HasMedicalRecordsAccess => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.MedicalRecordsAccess);

        //Check this role for to see if the user has the Medical Records role
        public bool HasMedicalRecordsRole => HasRole(AuthorizationConstants.MedicalRecordsRoleName);

        //UI elements level permissions
        //public bool CanViewMrFile => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.ViewMrFile);
        //public bool CanScanMrFile => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.ViewMrFile);
        //public bool CanSearchMrFile => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.ViewMrFile);
        //public bool CanUploadMrFile => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.ViewMrFile);
        //public bool CanRefreshMedicalRecords => HasMedicalRecordsRole && HasPermission(AuthorizationConstants.RefreshMedicalRecords);
    }



    public class AuthorizationConstants
    {
        public const string GuestUser = "GUEST_USER";

        //Role
        public const string MedicalRecordsRoleName = "Medical Records";
        //Role permissions
        //public const string ViewMrFile = "ViewMrFile";
        //public const string ScanMrFile = "ScanMrFile";
        //public const string SearchMrFile = "SearchMrFile";
        //public const string UploadMrFile = "UploadMrFile";
        public const string MedicalRecordsAccess = "MedicalRecordsAccess";
        public const string NoPermission = "NoPermission";
        //public const string RefreshMedicalRecords = "RefreshMedicalRecords";

    }

    public class RolePermission
    {
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}
