using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Commands.SurveyAnswer;

namespace questionnaire.Infrastructure.Validators.Survey {
    public class FieldDataValidator : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            ICollection<string> ErrorPositions = new List<string> ();
            bool counter = false;
            var Question = (QuestionAnswerToAdd) validationContext.ObjectInstance;
            if (Question.IsRequired) {
                foreach (var fieldDataAnswerToAdd in Question.FieldData) {
                    switch (Question.Select) {
                        case "short-answer":
                        case "long-answer":
                            {
                                if (fieldDataAnswerToAdd.Input == "") {
                                    ErrorPositions.Add (Question.QuestionPosition.ToString ());
                                }
                            }
                            break;
                        case "single-choice":
                        case "multiple-choice":
                            {
                                bool count = false;
                                foreach (var choiceOption in fieldDataAnswerToAdd.ChoiceOptions) {
                                    if (choiceOption.Value) {
                                        count = true;
                                    }
                                }
                                if (!count) {
                                    ErrorPositions.Add (Question.QuestionPosition.ToString ());
                                }
                            }
                            break;
                        case "dropdown-menu":
                            {
                                bool count = false;
                                foreach (var choiceOption in fieldDataAnswerToAdd.ChoiceOptions) {
                                    if (choiceOption.Value) {
                                        count = true;
                                    }
                                }
                                if (!count) {
                                    ErrorPositions.Add (Question.QuestionPosition.ToString ());
                                }
                            }
                            break;
                        case "linear-scale":
                            {
                                bool count = false;
                                foreach (var choiceOption in fieldDataAnswerToAdd.ChoiceOptions) {
                                    if (choiceOption.Value) {
                                        count = true;
                                    }
                                }
                                if (!count) {
                                    ErrorPositions.Add (Question.QuestionPosition.ToString ());
                                }
                            }
                            break;
                        case "single-grid":
                        case "multiple-grid":
                            {
                                bool count = false;
                                foreach (var row in fieldDataAnswerToAdd.Rows) {
                                    foreach (var choiceOption in row.ChoiceOptions) {
                                        if (choiceOption.Value) {
                                            count = true;
                                        }
                                    }
                                }
                                if (!count) {
                                    ErrorPositions.Add (Question.QuestionPosition.ToString ());
                                }
                            }
                            break;
                        default:
                            throw new Exception ("invalid select value");
                    }
                }
                foreach (var position in ErrorPositions) {
                    counter = true;
                }
            }
            return counter ? new ValidationResult ("following qustions contain empty fields", ErrorPositions) : ValidationResult.Success;
        }
    }
}