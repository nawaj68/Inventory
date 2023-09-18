// import { Messages } from './../message/messages';
// import {Injectable} from "@angular/core";
// import {MessageService} from "../message/message.service";

// @Injectable({
//   providedIn: "root",
// })
// export class FileService {
//   private fileOptions: IFileOptions;
//   constructor(public messageService: MessageService, public message: Messages) {}

//   formData(): FormData {
//     const formData = new FormData();

//     return formData;
//   }

//   validator(file: File, options: IFileOptions) {
//     if (file != null) {
//       const ext = file.name.substring(file.name.lastIndexOf(".") + 1, file.name.length);
//       const size = file.size / 1024; // file in kb
//       this.fileOptions = options;
//       if (
//         this.fileOptions.hasOwnProperty("validators") &&
//         this.fileOptions.validators != null &&
//         this.fileOptions.validators.length > 0 &&
//         this.fileOptions.validators[0].hasOwnProperty("fileType")
//       ) {
//         let configs: any;
//         if (this.fileOptions.hasOwnProperty("validatorKey") && this.fileOptions.validatorKey) {
//           configs = this.fileOptions.validators.filter((x) => x.validatorKey === this.fileOptions.validatorKey);
//           if (configs != null && configs.length > 0) {
//             this.fileOptions.acctptedTypes = configs[0].fileType;
//             this.fileOptions.maxSizeKB = configs[0].fileSize;
//           }
//         } else {
//           configs = this.fileOptions.validators;

//           if (configs != null && configs.length > 0) {
//             let fileTypes = "";
//             configs.forEach((x:any) => {
//               fileTypes += x.fileType + ",";
//             });
//             fileTypes = fileTypes.substring(0, fileTypes.lastIndexOf(","));

//             this.fileOptions.acctptedTypes = fileTypes;

//             const tconfigs = configs.filter((x:any) => x.fileType.split(",").includes(ext));
//             if (tconfigs != null && tconfigs.length > 0) {
//               this.fileOptions.maxSizeKB = tconfigs[0].fileSize;
//             }
//           }
//         }
//       }

//       if (
//         this.fileOptions.acctptedTypes &&
//         (!this.fileOptions.acctptedTypes.split(",").includes(ext) ||
//           (this.fileOptions.hasOwnProperty("maxSizeKB") )) //&& this.fileOptions.maxSizeKB < size
//       ) {
//         this.showMessage();
//         return false;
//       } else {
//         return true;
//       }
//     } else {
//       this.messageService.show(this.message.noFileFound, "warning");
//       return false;
//     }
//   }

//   private showMessage() {
//     let sizeError = "";
//     if (this.fileOptions.hasOwnProperty("maxSizeKB")) {
//       sizeError = ` & max ${this.fileOptions.maxSizeKB} KB size of file are supported`;
//     }
//     const message = `File type should be ${this.fileOptions.acctptedTypes}${sizeError}.`;
//     this.messageService.show(message, "warning");
//   }

//   fileValidate(file: File, allowFileType?: string, allowMaxSize?: number) {
//     let extention = "";
//     let size = 0;
//     let valid = false;

//     if (file == null) {
//       return valid;
//     }

//     extention = this.fileExtention(file.name);
//     size = this.fileToMB(file.size);

//     if (!allowFileType || (extention && allowFileType.includes(extention))) {
//       if (!allowMaxSize || (size && size <= allowMaxSize)) {
//         valid = true;
//       }
//     }
//     return valid;
//   }

//   fileExtention(filename: string) {
//     return filename.substring(filename.lastIndexOf(".") + 1, filename.length) || filename;
//   }

//   fileToMB(size: number) {
//     if (size <= 0) {
//       return 0;
//     }

//     return Math.ceil(size / (1024 * 1024));
//   }


//   viewDocument(file: any) {
//     const fileURL = URL.createObjectURL(file);
//     window.open(fileURL, "_blank");
//   }

//   iePdf(response: any, filename?: string) {
//     const byteCharacters = atob(response);
//     const byteNumbers = new Array(byteCharacters.length);
//     for (let i = 0; i < byteCharacters.length; i++) {
//       byteNumbers[i] = byteCharacters.charCodeAt(i);
//     }
//     const byteArray = new Uint8Array(byteNumbers);
//     const blob = new Blob([byteArray], {type: "application/pdf"});
//     filename = filename != null ? filename : "report.pdf";
//     window.navigator.msSaveOrOpenBlob(blob, filename);
//   }

//   showPdf(blobResponse: any, filename: string) {
//     const ieEDGE = navigator.userAgent.match(/Edge/g);
//     const ie = navigator.userAgent.match(/.NET/g); // IE 11+
//     const oldIE = navigator.userAgent.match(/MSIE/g);

//     const blob = new window.Blob([blobResponse], {type: "application/pdf"});

//     if (ie || oldIE || ieEDGE) {
//       window.navigator.msSaveBlob(blob, filename);
//     } else {
//       const fileURL = URL.createObjectURL(blob);
//       window.open(fileURL);
//     }
//   }

//   download(blobResponse: any, filename: string, contentType: string) {
//     const ieEDGE = navigator.userAgent.match(/Edge/g);
//     const ie = navigator.userAgent.match(/.NET/g); // IE 11+
//     const oldIE = navigator.userAgent.match(/MSIE/g);

//     const blob = new window.Blob([blobResponse], {type: contentType});

//     if (ie || oldIE || ieEDGE) {
//       window.navigator.msSaveBlob(blob, filename);
//     } else {
//       const element = document.createElement("a");
//       element.href = URL.createObjectURL(blob);
//       element.download = filename;
//       document.body.appendChild(element);
//       element.click();
//     }
//   }

//   downloadPdf(blobResponse: any, filename: string) {
//     this.download(blobResponse, filename, "application/pdf");
//   }

//   downloadExcel(blobResponse: any, filename: string) {
//     this.download(blobResponse, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
//   }

//   downloadText(blobResponse: any, filename: string) {
//     this.download(blobResponse, filename, "text/plain");
//   }
// }

// export interface IFileOptions {
//   acctptedTypes: string;
//   maxSizeKB?: number;
//   validators?: any[];
//   validatorKey?: string;
// }
