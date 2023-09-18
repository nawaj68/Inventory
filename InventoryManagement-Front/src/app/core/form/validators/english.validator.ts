import { AbstractControl } from "@angular/forms";

export function EnglishValidator(control: AbstractControl) {
  const englishRegex = /^[\x00-\x7F]*$/;

  if (englishRegex.test(control.value)) {
    return null;
  }

  return {
    englishValid: true
  };
}
