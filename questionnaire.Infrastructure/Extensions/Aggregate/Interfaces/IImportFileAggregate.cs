using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Infrastructure.DTO.ImportFile;
using Microsoft.AspNetCore.Http;

namespace questionnaire.Infrastructure.Extensions.Aggregate.Interfaces {
    public interface IImportFileAggregate {
        Task<string> UploadFileAndGetFullFileLocationAsync (IFormFile file);
        Task<IEnumerable<UnregisteredUserDto>> ImportExcelFileAndGetImportDataAsync (string fullFileLocation);
    }
}