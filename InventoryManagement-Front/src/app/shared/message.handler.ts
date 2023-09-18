import Swal, {SweetAlertIcon} from "sweetalert2";

import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class MessageHandler {
  msgSave = "Saved Successfully";
  msgUpdate = "Updated Successfully";
  msgDelete = "Deleted Successfully";
  msgNotfound = "Data not found";
  msgConfirmDelete = "Are you sure to delete";
  msgFileNotFound = "No file found";
  otpSendSuccess = "OTP Send Successfully";
  msgAssigned = "Assigned Successfully";
  resetSuccess = "Password reset successfully.";
  approveSuccess = "Approve successfully";
  rejectSuccess = "Reject successfully";
  approveConfirmation = "Are you sure you want to approve?";
  submitConfirmation = "Are you sure you want to submit?";
  rejectConfirmation = "Are you sure you want to reject?";
  msgClaimRelease = "Release successfully.";
  msgClaimFault = "Unable to release.";
  msgUnableToSave = "Unable to Save";
  msgConfirmUpdate = "Are you sure you want to update?";
  private toastConfig: any = null;

  constructor() {
    this.toastConfig = Swal.mixin({
      toast: true,
      position: "top-end",
      showConfirmButton: false,
      timer: 1000,
      // timerProgressBar: true,
      didOpen: (toast: any) => {
        toast.addEventListener("mouseenter", Swal.stopTimer);
        toast.addEventListener("mouseleave", Swal.resumeTimer);
      },
    });
  }

  show(message: string, icon: SweetAlertIcon) {
    Swal.fire({
      icon,
      text: message,
      showConfirmButton: false,
      showCloseButton: true,
      //showCancelButton: true
      // timer: 1500
    });
  }

  confirm(message?: string) {
    return Swal.fire({
      text: message ? message : "Are you sure?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes",
      cancelButtonText: "No",
      reverseButtons: true,
    });
  }

  inputValue(message: string, title: string) {
    return Swal.fire({
      title,
      input: "number",
      showConfirmButton: true,
      // showCloseButton: true,
      showCancelButton: true,
      // timer: 1500
    });
  }

  showToast = (message: string, icon: SweetAlertIcon): void => {
    this.toastConfig.fire({
      icon,
      text: message,
    });
  };
}
