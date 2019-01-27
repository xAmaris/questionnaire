using System.IO;
using Microsoft.AspNetCore.Http;

namespace questionnaire.Infrastructure.Commands.ImportFile {
    public class ImportFile {
        public IFormFile File { get; set; }

    }
}