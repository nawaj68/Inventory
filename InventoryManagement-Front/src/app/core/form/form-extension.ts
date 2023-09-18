import {FormGroup, ValidationErrors} from "@angular/forms";
import * as moment from "moment";

export class FormExtension {
  public static toFormData(formGroup: FormGroup): FormData {
    const obj = formGroup.getRawValue();
    const formData = new FormData();
    for (const key of Object.keys(obj)) {
      let value = (obj as any)[key];

      // form data can not convert to iso format for dotnet core
      // form data need to parse iso string
      // json data auto format to iso string
      if(moment.isMoment(value))
        value = moment(value).toISOString();
      formData.append(key, value);
    }

    console.log("formData", formData);
    return formData;
  }

  public static clearErrors(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      control?.setErrors(null);
    });
  }

  public static markAsAllRead(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      console.log("control", control?.value, control);
      control?.markAsTouched({onlySelf: true});
    });
  }

  public static markAllAsUntoched(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      console.log("control", control?.value, control);
      control?.markAsPristine();
      control?.markAsUntouched();
    });
  }

  public static getFormValidationErrors(formGroup: FormGroup): FormErrors[] {
    var errors: FormErrors[] = [];
    Object.keys(formGroup.controls).forEach((key) => {
      const control = formGroup.get(key);
      const validationErrors = control?.errors;
      if(validationErrors){
        const controlErrors: ValidationErrors  = validationErrors;
        if (controlErrors != null) {
          Object.keys(controlErrors).forEach((keyError) => {
            errors.push(new FormErrors(key, keyError, controlErrors[keyError], control?.touched, control?.valid));
            //console.log("Key control: " + key + ", keyError: " + keyError + ", err value: ", controlErrors[keyError]);
          });
        }
      }
    });

    console.log("errors", errors);
    return errors;
  }


}

export class FormErrors {
  key: string;
  error: string;
  value: string;
  touched: any;
  valid: any;

  constructor(key: string, error: string, value: string, touched: any, valid: any) {
    this.key = key;
    this.error = error;
    this.value = value;
    this.touched = touched;
    this.valid = valid;
  }
}
