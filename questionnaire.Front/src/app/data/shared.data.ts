// tslint:disable:max-classes-per-file
interface IConfirmDataDialog {
  dialogTitle: string;
  boldContent: string;
  boldWarn: boolean;
  buttonText: string;
}
export class SendSurveyDialog implements IConfirmDataDialog {
  dialogTitle = 'Wysyłanie';
  boldContent = 'wysłać';
  boldWarn = false;
  buttonText = 'Wyślij';
}
export class DeleteTemplateDialog implements IConfirmDataDialog {
  dialogTitle = 'Usuwanie';
  boldContent = 'trwale usunąć';
  boldWarn = true;
  buttonText = 'Tak, usuń';
}
