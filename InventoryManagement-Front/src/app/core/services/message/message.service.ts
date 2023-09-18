import Swal, {SweetAlertIcon} from "sweetalert2";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class MessageService {

  private toastConfig: any;

  constructor() {
    this.toastConfig = Swal.mixin({
      toast: true,
      position: "top-end",
      showConfirmButton: false,
      timer: 5000,
      timerProgressBar: true,
      // iconColor: 'white',
      // customClass: {
      //   popup: 'colored-toast'
      // },
      didOpen: (toast) => {
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
      showCancelButton: false
    });
  }

  success(message: string) {
    Swal.fire({
      icon: 'success',
      text: message,
      showConfirmButton: false,
      showCloseButton: true,
      showCancelButton: false
    });
  }

  error(message: string) {
    Swal.fire({
      icon: 'error',
      text: message,
      showConfirmButton: false,
      showCloseButton: true,
      showCancelButton: false
    });
  }

  warn(message: string) {
    Swal.fire({
      icon: 'warning',
      text: message,
      showConfirmButton: false,
      showCloseButton: true,
      showCancelButton: false
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
      text: message,
      showConfirmButton: true,
      showCloseButton: true,
      showCancelButton: true
    });
  }

  toast = (message: string, icon: SweetAlertIcon): void => {
    this.toastConfig.fire({
      icon,
      text: message,
    });
  };
}
