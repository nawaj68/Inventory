import * as moment from "moment";

import { FormGroup } from "@angular/forms";

// export function DateCompareValidator(control: AbstractControl) {

//     return {
//         phoneValid: true
//     };
// }

// export function DateCompareValidator = (fg: FormGroup): ValidatorFn => {
//     const start = fg.get('rangeStart').value;
//     const end = fg.get('rangeEnd').value;
//     return start !== null && end !== null && start < end
//         ? null
//         : { range: true };
// };

export function DateCompareValidator(startDate: string, endDate: string) {
  return (formGroup: FormGroup) => {
    const startDateControl = formGroup.controls[startDate];
    const endDateControl = formGroup.controls[endDate];

    if (endDateControl.errors && !endDateControl.errors.dateCompare) {
      return;
    }

    if (moment(startDateControl.value) > moment(endDateControl.value)) {
      endDateControl.setErrors({ dateCompare: true });
    } else {
      endDateControl.setErrors(null);
    }
  };
}
