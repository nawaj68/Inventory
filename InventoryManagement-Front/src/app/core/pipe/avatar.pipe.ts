import {environment} from "src/environments/environment";
import {Pipe, PipeTransform} from "@angular/core";
@Pipe({name: "avatar"})
export class AvatarPipe implements PipeTransform {
  transform(value: string, alt: string = ""): string {
    const url = `${environment.baseUrl}/${value}`;
    return `<img src=${url} alt=${alt} />`;
  }
}
