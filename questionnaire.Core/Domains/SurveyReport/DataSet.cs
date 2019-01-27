using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace questionnaire.Core.Domains.SurveyReport
{
    public class DataSet
    {
        public int Id { get; private set; }
        public string Label { get; private set; }
        [NotMapped]
        public List<string> _data { get; set; } = new List<string>();
        public string Data
        {
            get { return string.Join(",", _data); }
            set { _data = value.Split(',').ToList(); }
        }
        public int QuestionReportId { get; private set; }
        public QuestionReport QuestionReport { get; private set; }

        public DataSet () { }
        public DataSet (string label){
            SetLabel(label);
        }

        public void SetLabel(string label) {
            Label = label;
        }

        public void AddData(string data)
        {
            _data.Add(data);
        }
    }
}