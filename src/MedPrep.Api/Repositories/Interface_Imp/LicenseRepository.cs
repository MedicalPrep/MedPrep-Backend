using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPrep.Api.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly MedPrepContext context;

        public LicenseRepository(MedPrepContext context)
        {
            this.context = context;
        }

        public void UploadLicense(License license)
        {
            context.Licenses.Add(license);
        }

        public void UpdateLicense(License license, License newLicense)
        {
            var existingLicense = GetLicenseBy(license.LicenseNumber);
            if (existingLicense != null)
            {
                existingLicense.Country = newLicense.Country;
                existingLicense.ProvinceOrState = newLicense.ProvinceOrState;
                existingLicense.LicenseNumber = newLicense.LicenseNumber;
                existingLicense.LicenseName = newLicense.LicenseName;
                existingLicense.AccreditationType = newLicense.AccreditationType;
                existingLicense.LicenseSource = newLicense.LicenseSource;

                context.Entry(existingLicense).State = EntityState.Modified;
            }
        }

        public License GetLicenseById(string licenseId)
        {
            return context.Licenses.FirstOrDefault(l => l.LicenseNumber == licenseId);
        }

        public IEnumerable<License> GetAllLicenses()
        {
            return context.Licenses.ToList();
        }

        public void DeleteLicense(string licenseId)
        {
            var license = GetLicenseById(licenseId);
            if (license != null)
            {
                context.Licenses.Remove(license);
            }
        }


    }
}
