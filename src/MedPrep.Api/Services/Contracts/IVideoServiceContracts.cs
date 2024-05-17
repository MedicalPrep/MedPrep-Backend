using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPrep.Api.Services.Contracts;

public class IVideoServiceContracts
{
    public record VideoUploadRequest
    {
        public IList<IFormFile> Video { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid CourseModuleId { get; set; }
    }
}
