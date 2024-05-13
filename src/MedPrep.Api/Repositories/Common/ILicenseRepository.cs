namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface ILicenseRepository
{
    void UploadLicense(License license);
    void UpdateLicense(License license, License newlicense);
    License? GetLicenseById(Guid licenseId);
    IEnumerable<License> GetAllLicenses();
    void DeleteByIdAsync(Guid licenseId);
}
