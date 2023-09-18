import * as moment from "moment";

import { FormControl, Validators } from "@angular/forms";

export class DateValidator extends Validators {
  static custom = (fValue: FormControl) => {
    if (fValue.touched && !moment.isMoment(fValue.value))
      return { invalid: true };
  };
}
