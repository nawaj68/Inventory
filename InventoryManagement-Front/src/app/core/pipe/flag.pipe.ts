import {environment} from "src/environments/environment";
import {Pipe, PipeTransform} from "@angular/core";
@Pipe({name: "flag"})
export class FlagPipe implements PipeTransform {
  transform(value: string): string {
    return `<span class="fi fi-${value}"></span>`;
  }
}
