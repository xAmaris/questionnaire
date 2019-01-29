using questionnaire.Infrastructure.Commands.Survey;
using System;
using System.Collections.Generic;
using System.Text;

namespace questionnaire.Tests.Mocks
{
   public static class SurveyToAddMock
    {
        public static SurveyToAdd GetSurveyToAdd()
        {
            return new SurveyToAdd()
        {
            Title = "surveyToAdd title",
                Questions = new List<QuestionToAdd>() {
                new QuestionToAdd () {
                QuestionPosition = 0,
                Content = "1",
                Select = "short-answer",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                Input = ""
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 1,
                Content = "2",
                Select = "long-answer",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                Input = ""
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 2,
                Content = "3",
                Select = "single-choice",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                ChoiceOptions = new List<ChoiceOptionToAdd> () {
                new ChoiceOptionToAdd () {
                OptionPosition = 0,
                Value = false,
                ViewValue = "opcja 1"
                }
                }
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 3,
                Content = "4",
                Select = "multiple-choice",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                ChoiceOptions = new List<ChoiceOptionToAdd> () {
                new ChoiceOptionToAdd () {
                OptionPosition = 0,
                Value = false,
                ViewValue = "opcja 1"
                }
                }
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 4,
                Content = "5",
                Select = "dropdown-menu",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                ChoiceOptions = new List<ChoiceOptionToAdd> () {
                new ChoiceOptionToAdd () {
                OptionPosition = 0,
                Value = false,
                ViewValue = "opcja 1"
                }
                }
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 5,
                Content = "6",
                Select = "linear-scale",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                MinValue = 1,
                MaxValue = 5,
                MinLabel = "minLabel",
                MaxLabel = "maxLabel",
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 6,
                Content = "7",
                Select = "single-grid",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                ChoiceOptions = new List<ChoiceOptionToAdd> () {
                new ChoiceOptionToAdd () {
                OptionPosition = 0,
                Value = false,
                ViewValue = "opcja 1"
                }
                },
                Rows = new List<RowToAdd> () {
                new RowToAdd () {
                RowPosition = 0,
                Input = "wiersz 1"
                }
                }
                }
                }
                },
                new QuestionToAdd () {
                QuestionPosition = 7,
                Content = "8",
                Select = "multiple-grid",
                IsRequired = true,
                FieldData = new List<FieldDataToAdd> () {
                new FieldDataToAdd () {
                ChoiceOptions = new List<ChoiceOptionToAdd> () {
                new ChoiceOptionToAdd () {
                OptionPosition = 0,
                Value = false,
                ViewValue = "opcja 1"
                }
                },
                Rows = new List<RowToAdd> () {
                new RowToAdd () {
                RowPosition = 0,
                Input = "wiersz 1"
                }
                }
                }
                }
                }
                }
            };
    }
    }
}
