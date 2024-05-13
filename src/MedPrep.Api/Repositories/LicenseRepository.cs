namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;

public class LicenseRepository : ILicenseRepository
{
    private readonly MedPrepContext context;

    public LicenseRepository(MedPrepContext context)
    {
        this.context = context;
    }

    public void UploadLicense(License license) => this.context.License.Add(license);

    public void UpdateLicense(License license, License newlicense)
    {
        var existingLicense = this.GetLicenseById(license.Id);
        if (existingLicense != null)
        {
            existingLicense.Country = newlicense.Country;
            existingLicense.ProvinceOrState = newlicense.ProvinceOrState;
            existingLicense.LicenseNumber = newlicense.LicenseNumber;
            existingLicense.LicenseName = newlicense.LicenseName;
            existingLicense.AccreditationType = newlicense.AccreditationType;
            existingLicense.LicenseSource = newlicense.LicenseSource;
        }
    }

    public License? GetLicenseById(Guid licenseId) =>
        this.context.License.Where(l => l.Id == licenseId).FirstOrDefault();

    public IEnumerable<License> GetAllLicenses() => this.context.License.ToList();

    public void DeleteByIdAsync(Guid licenseId)
    {
        var license = this.GetLicenseById(licenseId);
        if (license != null)
        {
            _ = this.context.License.Remove(license);
        }
    }
}
