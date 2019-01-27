using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class DataSetRepository : IDataSetRepository {
        private readonly questionnaireContext _context;

        public DataSetRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (DataSet dataSet) {
            await _context.DataSets.AddAsync (dataSet);
            await _context.SaveChangesAsync ();
        }
        public async Task UpdateAsync (DataSet dataSet) {
            _context.DataSets.Update (dataSet);
            await _context.SaveChangesAsync ();
        }
    }
}