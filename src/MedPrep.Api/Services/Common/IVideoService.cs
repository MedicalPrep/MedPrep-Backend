namespace MedPrep.Api.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static MedPrep.Api.Services.Contracts.IVideoServiceContracts;


public interface IVideoService
{
    Task<string> UploadVideo(VideoUploadRequest videoUploadRequest);
}
