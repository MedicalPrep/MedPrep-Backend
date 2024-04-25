using System;
using System.Collections.Generic;
using MedPrep.Api.Models;


namespace MedPrep.Api.Repositories
{
    public interface ILicenseRepository   {
        void UploadLicense(License license);
        void UpdateLicense(License license, License Newlicense);
        License GetLicenseById(int licenseId);
        IEnumerable<License> GetAllLicenses();
        void DeleteLicense(int licenseId);
        void save();
    }
}
