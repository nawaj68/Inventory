import { AbstractControl } from '@angular/forms';

export function PhoneValidator(control: AbstractControl) {
  const phoneRegex = /^((01)|(8801))([0-9]{9})$/;

  if (phoneRegex.test(control.value) || control.value === '') {
    return null;
  }

  return {
    phoneValid: true
  };
}
