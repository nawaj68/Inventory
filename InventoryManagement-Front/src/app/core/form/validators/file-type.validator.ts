import { FormControl } from "@angular/forms";

export function FileTypeValidator(type: string) {
  return function (control: FormControl): any {
    const file = control.value;
    if (file) {
      const extension = file.name.split('.')[1].toLowerCase();
      if (type.toLowerCase() !== extension.toLowerCase()) {
        return {
          fileTypeValid: true
        };
      }

      return null;
    }

    return null;
  };
}
