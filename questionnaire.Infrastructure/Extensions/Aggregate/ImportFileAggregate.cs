using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Infrastructure.DTO.ImportFile;
using questionnaire.Infrastructure.Extensions.Aggregate.Interfaces;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace questionnaire.Infrastructure.Extensions.Aggregate {
    public class ImportFileAggregate : IImportFileAggregate {
        private readonly IUnregisteredUserRepository _unregisteredUserRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public ImportFileAggregate (IUnregisteredUserRepository unregisteredUserRepository,
            IHostingEnvironment hostingEnvironment,
            IMapper mapper) {
            _unregisteredUserRepository = unregisteredUserRepository;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }

        public async Task<string> UploadFileAndGetFullFileLocationAsync (IFormFile file) {
            if (file == null && file.Length < 0)
                throw new Exception ("File not selected");

            if (!Directory.Exists ("wwwroot")) {
                Directory.CreateDirectory ("wwwroot");
            }

            var path = Path.Combine (_hostingEnvironment.WebRootPath, file.FileName).ToLower ();

            if (!Directory.Exists (path)) {
                Directory.CreateDirectory (path);
            }

            string fullFileLocation = Path.Combine (path, file.FileName).ToLower ();

            using (var fileStream = new FileStream (fullFileLocation, FileMode.Create)) {
                await file.CopyToAsync (fileStream);
            }
            return fullFileLocation;
        }

        public async Task<IEnumerable<UnregisteredUserDto>> ImportExcelFileAndGetImportDataAsync (string fullFileLocation) {
            FileInfo fileInfo = new FileInfo (fullFileLocation);
            List<UnregisteredUserDto> importDataListDto = new List<UnregisteredUserDto> ();

            using (ExcelPackage package = new ExcelPackage (fileInfo)) {
                var workSheet = package.Workbook.Worksheets[1];
                int totalRows = workSheet.Dimension.Rows;

                List<UnregisteredUser> importDataList = new List<UnregisteredUser> ();

                for (int i = 2; i <= totalRows; i++) {
                    var importData = new UnregisteredUser ();
                    importData.SetName (workSheet.Cells[i, 1].Value.ToString ());
                    importData.SetSurname (workSheet.Cells[i, 2].Value.ToString ());
                    importData.SetCourse (workSheet.Cells[i, 3].Value.ToString ());
                    importData.SetDateOfCompletion (Convert.ToDateTime (workSheet.Cells[i, 4].Value.ToString ()));
                    importData.SetTypeOfStudy (workSheet.Cells[i, 5].Value.ToString ());
                    importData.SetEmail (workSheet.Cells[i, 6].Value.ToString ().ToLowerInvariant ());
                    importDataList.Add (importData);

                    importDataListDto.Add (_mapper.Map<UnregisteredUserDto> (importData));
                }

                await _unregisteredUserRepository.AddAllAsync (importDataList);
            }
            Directory.Delete (fileInfo.DirectoryName, true);
            return importDataListDto;
        }

    }
}