import { AbstractControl, ValidationErrors } from "@angular/forms";

export function UniqueInFormArray(
  control: AbstractControl
): ValidationErrors | null {
  if (control.parent && control.parent.parent) {
    const duplicateObj = control.parent.parent.value.find(
      (x) => x.benefitId === control.value && x.id !== 0
    );

    if (duplicateObj) {
      return { notUnique: true } as ValidationErrors;
    }
    return null;
  }
}

// const valueArr = value.map((item) => { return item.benefitId });
// const hasDuplicate = valueArr.some((item, idx) => {
//   return valueArr.indexOf(item) != idx
// });
